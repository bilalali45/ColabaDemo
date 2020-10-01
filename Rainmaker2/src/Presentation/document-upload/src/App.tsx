import React, { useEffect, useState,useLayoutEffect } from "react";
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
import logoHeaderSrc from './assets/images/texasWhiteHeader.png';
declare global {
  interface Window {
    envConfig: any;
    isMobile:any;
    width:any
  }
}
window.envConfig = window.envConfig || {};
function getFaviconEl() {
  return document.getElementById("favicon");
}

const App = () => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  const [footerText, setFooterText] = useState("");
  const [companyLogoSrc, setcompanyLogoSrc] = useState("");
  const [favIconSrc, setfavIconSrc] = useState("");


  
  const useWindowSize = () => {
    const [size, setSize] = useState([0, 0]);
  useLayoutEffect(() => {
    function updateSize() {
      setSize([window.innerWidth, window.innerHeight]);
    }
    window.addEventListener('resize', updateSize);
    updateSize(); 
    return () => window.removeEventListener('resize', updateSize);
  }, []);
  return size;
}

let TRUTHY_VALUES = [true, 'true', 1];
const getBoolean=(a) => {
    return TRUTHY_VALUES.some(function(t) {
        return t === a;
    });
}
  const [width, height] = useWindowSize();
  useEffect(() => {
    let Mobile = width <= 767;
    sessionStorage.setItem('isMobile', String(Mobile));
    sessionStorage.setItem('width', String(width));
   // window.isMobile = sessionStorage.getItem("isMobile")
   // alert("useeffect"+ window.isMobile)
    console.log("ismobile:"+window.isMobile,typeof(window.isMobile));
  }, [width, height])

  const history = useHistory()

  useEffect(() => {
    console.log("Document Management App Version", "0.1.3");
    console.log('Logo Src',logoHeaderSrc)
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
      getCompanyLogoSrc();
      setFavIcon();
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

  const getCompanyLogoSrc = async () => {
    let applicationId = Auth.getLoanAppliationId();
    let logoSrc = await LaonActions.getCompanyLogoSrc(applicationId);
    let logo =`data:image/png;base64,${logoSrc}`
    setcompanyLogoSrc(logo);
  }

  const setFavIcon = async  () => {
    let applicationId = Auth.getLoanAppliationId();
    const favicon:any = getFaviconEl(); // Accessing favicon element
    let favIconSrc = await LaonActions.getCompanyFavIconSrc(applicationId);
    let logo =`data:image/png;base64,${favIconSrc}`
    if(favicon){
      favicon.href=logo;
    }
  }


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
          logoSrc={companyLogoSrc}
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
