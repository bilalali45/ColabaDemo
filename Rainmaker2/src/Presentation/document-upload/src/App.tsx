import React, { useEffect, useState } from "react";
import "./App.scss";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  useHistory,
} from "react-router-dom";
import { Home } from "./components/Home/Home";
import { StoreProvider } from "./store/store";
import { RainsoftRcHeader, RainsoftRcFooter } from "rainsoft-rc";
import { UserActions } from "./store/actions/UserActions";
import ImageAssets from "./utils/image_assets/ImageAssets";
import { ParamsService } from "./utils/ParamsService";
import { Authorized } from "./shared/Components/Authorized/Authorized";
import { FooterContents } from "./utils/header_footer_utils/FooterContent";
import HeaderContent from "./utils/header_footer_utils/HeaderContent";
import { Auth } from "./services/auth/Auth";
import { LaonActions } from "./store/actions/LoanActions";
import IdleTimer from "react-idle-timer";

const mvcDashBoardUrl = `Dashboard`;
// const mvcDashBoardUrlHttps = 'https://qatx.rainsoftfn.com/Dashboard';

declare global {
  interface Window {
    envConfig: any;
  }
}
window.envConfig = window.envConfig || {};

const App = () => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  const [expListnerAdded, setExpListnerAdded] = useState(false);
  const [footerText, setFooterText] = useState("");
  const tokenData: any = UserActions.getUserInfo();
  const displayName = " " + tokenData?.FirstName + " " + tokenData?.LastName;
  const history = useHistory();

  useEffect(() => {
    console.log("Document Management App Version", "0.1.3");
    authenticate();
    ParamsService.storeParams([
      "loanApplicationId",
      "tenantId",
      "businessUnitId",
    ]);
    // component unmount
    return () => {
      Auth.removeAuth();
    };
  }, []);

  const authenticate = async () => {
    let isAuth = await UserActions.authorize();
    setAuthenticated(Boolean(isAuth));
    getFooterText();
    addExpiryListener();
    keepAliveParentApp();
  };

  const getFooterText = async () => {
    const tenantId = Auth.getTenantId();
    const businessUnitId = Auth.getBusinessUnitId();
    console.log(
      "getFooterText TenantID",
      tenantId,
      "Business Unit id",
      businessUnitId
    );
    let footerText = await LaonActions.getFooter(tenantId, businessUnitId);
    setFooterText(footerText);
  };

  const addExpiryListener = () => {
    if (Auth.getUserPayload()) {
      console.log("addExpiryListener called from APP tsx");
      UserActions.addExpiryListener(Auth.getUserPayload());
      // setExpListnerAdded(true);
    }
  };

  const keepAliveParentApp = () => {
    if (process.env.NODE_ENV === "production") {
      setInterval(() => {
        UserActions.refreshParentApp();
      }, 60 * 1000);
    }
  };

  const onIdle = (e) => {
    console.log("Idle time meet");
    Auth.removeAuth();
    window.open("/Account/LogOff", "_self");
  };

  if (!authenticated) {
    return null;
  }

  return (
    <div className="app">
      <StoreProvider>
        <IdleTimer
          element={document}
          onIdle={onIdle}
          debounce={250}
          timeout={1000 * 60 * window.envConfig.IDLE_TIMER}
        />
        <RainsoftRcHeader
          logoSrc={ImageAssets.header.logoheader}
          displayName={UserActions.getUserName()}
          displayNameOnClick={HeaderContent.gotoDashboardHandler}
          options={HeaderContent.headerDropdowmMenu}
        />

        <Router basename="/DocumentManagement">
          <Switch>
            <Authorized path="/" component={Home} />
          </Switch>
        </Router>
        <RainsoftRcFooter content={footerText} />
      </StoreProvider>
    </div>
  );
};

export default App;
