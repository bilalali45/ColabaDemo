import React, { useEffect, useState, useLayoutEffect, useContext } from "react";
import "../public/envconfig.js";
import '../public/rs-authorization.js';
import '../public/manifest.json';
import { BrowserRouter as Router, Switch, useHistory } from "react-router-dom";
import Home from "./components/Home/Home";
import { Store, StoreProvider } from "./store/store";
import { RainsoftRcHeader, RainsoftRcFooter } from "rainsoft-rc";
import { UserActions } from "./store/actions/UserActions";
import ImageAssets from "./utils/image_assets/ImageAssets";
import { Authorized } from "./shared/Components/Authorized/Authorized";
import HeaderContent from "./utils/header_footer_utils/HeaderContent";
import { Auth } from "./services/auth/Auth";
import { LaonActions } from "./store/actions/LoanActions";
import IdleTimer from "react-idle-timer";
import { LoanActionsType } from "./store/reducers/loanReducer";



declare global {
  interface Window {
    envConfig: any;
    isMobile: any;
    width: any;
    Authorization: any
  }
}
window.envConfig = window.envConfig || {};

console.log('process.env.NODE_ENV', process.env.NODE_ENV);

const App = () => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  const [footerText, setFooterText] = useState("");
  const [companyLogoSrc, setcompanyLogoSrc] = useState("");
  const [favIconSrc, setfavIconSrc] = useState("");
  const history = useHistory()
  const { state, dispatch } = useContext(Store)
  const laon: any = state.loan;
  const { isMobile } = laon;
  const getWindowSize = () => {
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

  const checkBrowser = () => {
    let agent = navigator.userAgent;
    var isSafari = /^((?!chrompp|android).)*safari/i.test(agent);

    let browser = '';
    if ((agent.indexOf("Opera") || agent.indexOf("OPR")) != -1) {
      browser = 'opera';
    } else if (isSafari) {
      browser = 'safari';
    } else if (agent.indexOf("Chrome")) {
      browser = 'chrome';
    } else if (agent.indexOf("Firefox")) {
      browser = 'firefox';
    } else if (agent.indexOf("MSIE") != -1) {
      browser = 'msie';
    } else {
      browser = 'unknown';
    }
    document.body.classList.add(browser);
  }

  const [width, height] = getWindowSize();
  useEffect(() => {
    let isMobile = width <= 767;
    if (width != 0) {
      sessionStorage.setItem('isMobile', String(isMobile));
      sessionStorage.setItem('width', String(width));
    }

    dispatch({ type: LoanActionsType.SetIsMobile, payload: { value: isMobile } })
    checkBrowser();
    if (isMobile) {
      document.body.classList.add('mobile');
    } else {
      document.body.classList.remove('mobile');
    }
  }, [width, height]);



  useEffect(() => {
    window.resizeTo(400, 600);
    console.log("Document Management App Version", "0.1.3");
    authenticate();
    if (window.envConfig.LOGROCKET_ENABLE) {
      UserActions.testHttpRequest();
    }

    // component unmount
    return () => {
      Auth.removeAuth();
    };
  }, []);

  function getFaviconEl() {
    return document.getElementById("favicon");
  }


  const authenticate = async () => {
    //if (process.env.NODE_ENV === "development") {
    await window?.Authorization?.authorize();
    //}
    if (true) {
      setAuthenticated(Boolean(true));
      getCompanyLogoSrc();
      setFavIcon();
      getFooterText();
      //addExpiryListener();
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
    let logo = `data:image/png;base64,${logoSrc}`
    let isMobile = sessionStorage.getItem('isMobile');
    isMobile === "true" ? setcompanyLogoSrc(logo) : setcompanyLogoSrc(logo);
    setcompanyLogoSrc(logo);
  };

  const setFavIcon = async () => {
    let applicationId = Auth.getLoanAppliationId();
    const favicon: any = getFaviconEl(); // Accessing favicon element
    let favIconSrc = await LaonActions.getCompanyFavIconSrc(applicationId);
    let logo = `data:image/png;base64,${favIconSrc}`
    if (favicon) {
      favicon.href = logo;
    }
  }

  const keepAliveParentApp = () => {
    if (process.env.NODE_ENV === "production") {
      setInterval(() => {
        UserActions.refreshParentApp();
      }, 60 * 1000);
    }
  };

  const onIdle = (e) => {
    window.onbeforeunload = null;
    Auth.removeAuth();
    if (window.open) {
      window.open("/Account/LogOff", "_self");
    }
  };

  if (!authenticated) {
    return null;
  }

  return (
    <StoreProvider>
      <div className="app">
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
        <Router basename="/loanportal">
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

        {
          isMobile?.value ? null : <RainsoftRcFooter content={footerText} />
        }

      </div>
    </StoreProvider>
  );
};

export default App;
