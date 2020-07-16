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
    ParamsService.storeParams([
      "loanApplicationId",
      "tenantId",
      "businessUnitId",
    ]);
    // component unmount
    return () => {
      LocalDB.removeAuth();
    };
  }, []);

  const authenticate = async () => {
    let isAuth = await UserActions.authorize();
    setAuthenticated(Boolean(isAuth));
    UserActions.addExpiryListener();
    UserActions.keepAliveParentApp();
  };

  const onIdle = (e) => {
    console.log("Idle time meet");
    LocalDB.removeAuth();
    window.open("/Account/LogOff", "_self");
  };

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
      <RainMakerHeader />
      <section className="d-layout">
        <RainMakerSidebar />
        <main className="main-layout hascollapssidebar">
          <StoreProvider>
            <Router basename="/DocumentManagement">
              <Route path="/" component={MCUHome} />
              <RainMakerFooter />
            </Router>
          </StoreProvider>
        </main>
      </section>
    </div>
  );
};

export default App;
