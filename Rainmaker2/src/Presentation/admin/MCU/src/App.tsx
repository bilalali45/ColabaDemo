import React, { useEffect, useState } from "react";
//import logo from './logo.svg';
import "./App.scss";
import { MCUHome } from "./Components/MCUHome/MCUHome";
import { RainMakerHeader } from "./Components/RainMakerHeader/RainMakerHeader";
import { RainMakerSidebar } from "./Components/RainMakerSidebar/RainMakerSidebar";
import { RainMakerFooter } from "./Components/RainMakerFooter/RainMakerFooter";
import { BrowserRouter as Router, Route } from "react-router-dom";
import { StoreProvider } from "./Store/Store";
import { UserActions } from "./Store/actions/UserActions";
import { LocalDB } from "./Utils/LocalDB";
import { ParamsService } from "./Utils/helpers/ParamService";
import IdleTimer from "react-idle-timer";
import { Authorized } from "./Components/Authorized/Authorized";

declare global {
  interface Window {
    envConfig: any;
  }
}
window.envConfig = window.envConfig || {};

const App = () => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  useEffect(() => {
    console.log("MCU App Version", "0.0.1");
    authenticate();
    ParamsService.storeParams(["loanApplicationId", "tenantId"]);
    // component unmount
    return () => {
      LocalDB.removeAuth();
    };
  }, []);

  const authenticate = async () => {
    console.log("Before Authorize");
    let isAuth = await UserActions.authorize();
    setAuthenticated(Boolean(isAuth));
    console.log("After Authorize");
    UserActions.addExpiryListener();
    UserActions.keepAliveParentApp();
  };

  const onIdle = (e: any) => {
    console.log("Idle time meet");
    LocalDB.removeAuth();
    //window.open("/Login/LogOff", "_self");
    window.top.location.href = "/Login/LogOff";
  };

  console.log("Authorize User is authenticated", authenticated);
  if (!authenticated) {
    return null;
  }

  return (
    <div className="App">
      <IdleTimer
        element={document}
        onIdle={onIdle}
        debounce={250}
        timeout={1000 * 60 * window.envConfig.IDLE_TIMER}
      />
      {/* <RainMakerHeader /> */}
      <section className="d-layout">
        {/* <RainMakerSidebar /> */}
        <main className="main-layout">
          <StoreProvider>
            <Router basename="/DocumentManagement">
              <Authorized path="/" component={MCUHome} />
              <RainMakerFooter />
            </Router>
          </StoreProvider>
        </main>
      </section>
    </div>
  );
};

export default App;
