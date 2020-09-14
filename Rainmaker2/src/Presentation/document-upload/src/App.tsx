import React, { useEffect, useState } from "react";
import "./App.scss";
import { BrowserRouter as Router, Switch, useHistory } from "react-router-dom";
import { Home } from "./components/Home/Home";
import { StoreProvider } from "./store/store";
import { RainsoftRcHeader, RainsoftRcFooter } from "rainsoft-rc";
import { UserActions } from "./store/actions/UserActions";
import ImageAssets from "./utils/image_assets/ImageAssets";
import { Authorized } from "./shared/Components/Authorized/Authorized";
import HeaderContent from "./utils/header_footer_utils/HeaderContent";
import { Auth } from "./services/auth/Auth";
import { LaonActions } from "./store/actions/LoanActions";
import IdleTimer from "react-idle-timer";

declare global {
  interface Window {
    envConfig: any;
  }
}

const App = () => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  const [footerText, setFooterText] = useState("");

  const history = useHistory()

  useEffect(() => {
    // history?.push(`/${Auth.getLoanAppliationId()}`);
    // console.log("Document Management App Version", "0.1.3");
    authenticate();
    // component unmount
    return () => {
      Auth.removeAuth();
    };
  }, []);

  const authenticate = async () => {
    let isAuth = await UserActions.authorize();
    if (isAuth) {
      setAuthenticated(Boolean(isAuth));
      getFooterText();
      addExpiryListener();
      keepAliveParentApp();
    } else {
      Auth.removeAuth();
      if (window.open) {
        window.open("/Account/LogOff", "_self");
      }
    }
  };

  const getFooterText = async () => {
    let applicationId = Auth.getLoanAppliationId();
    let footerText = await LaonActions.getFooter(applicationId);
    setFooterText(footerText);
  };

  const addExpiryListener = () => {
    if (Auth.getUserPayload()) {
      // console.log("addExpiryListener called from APP tsx");
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
    // console.log("Idle time meet");
    window.onbeforeunload = null;
    Auth.removeAuth();
    if (window.open) {
      window.open("/Account/LogOff", "_self");
    }
  };

  // console.log("Application is ", authenticated);
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
          timeout={1000 * 60 * window?.envConfig?.IDLE_TIMER}
        />
        <RainsoftRcHeader
          logoSrc={ImageAssets.header.logoheader}
          displayName={UserActions.getUserName()}
          // displayNameOnClick={HeaderContent.gotoDashboardHandler}
          options={HeaderContent.headerDropdowmMenu}
        />
        <Router basename="/LoanPortal">
          <Switch>
            {process.env.NODE_ENV === 'test' ? <Authorized
              path="/"
              component={Home}
            /> : <Authorized
                path="/:navigation/:loanApplicationId"
                component={Home}
              />
            }
            <Authorized path="/:loanApplicationId" component={Home} />
          </Switch>
        </Router>
        <RainsoftRcFooter content={footerText} />
      </StoreProvider>
    </div>
  );
};

export default App;
