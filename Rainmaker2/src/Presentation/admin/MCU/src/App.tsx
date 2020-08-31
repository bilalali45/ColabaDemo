import React, { useEffect, useState } from "react";
import { BrowserRouter as Router, Route } from "react-router-dom";
import { SignalRHub } from 'rainsoft-js';
import IdleTimer from "react-idle-timer";

import "./App.scss";
import { MCUHome } from "./Components/MCUHome/MCUHome";
import { RainMakerFooter } from "./Components/RainMakerFooter/RainMakerFooter";
import { StoreProvider } from "./Store/Store";
import { UserActions } from "./Store/actions/UserActions";
import { LocalDB } from "./Utils/LocalDB";

import { Authorized } from "./Components/Authorized/Authorized";

let baseUrl : any = window.envConfig.API_BASE_URL; 
declare global {
  interface Window {
    envConfig: any;
  }
}
window.envConfig = window.envConfig || {};

const App = () => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  const hubUrl: string =  baseUrl+'/serverhub';
  
  useEffect(() => {
    console.log("MCU App Version", "0.0.1");
    authenticate();
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
    const accessToken: string = LocalDB.getAuthToken() || '';
    SignalRHub.configureHubConnection(hubUrl, accessToken, eventsRegister);
    UserActions.addExpiryListener();
    UserActions.keepAliveParentApp();
  };

  const onIdle = (e: any) => {
    console.log("Idle time meet");
    window.onbeforeunload = null;
    LocalDB.removeAuth();
    //window.open("/Login/LogOff", "_self");
    window.top.location.href = "/Login/LogOff";
  };


  const eventsRegister = () => {
    console.log('signalR eventsRegister on Client', SignalRHub.hubConnection)
    SignalRHub.hubConnection.on('TestSignalR', (data: string) => {
      console.log(`TestSignalR`,data);
    }); 
    SignalRHub.hubConnection.on('SendNotification', (data: any) => {
      console.log('Notification comes from SignalR on Client',data)
    }); 
    
    SignalRHub.hubConnection.onclose((e: any) => {
      console.log(`SignalR disconnected on Client`,e);
      const auth = LocalDB.getAuthToken();
      if(auth){
        SignalRHub.signalRHubResume();
      }
    });

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
              <Authorized
                exact
                path="/:loanApplicationId"
                component={MCUHome}
              />
              <Authorized
                path="/:activity/:loanApplicationId/"
                component={MCUHome}
              />
              <RainMakerFooter />
            </Router>
          </StoreProvider>
        </main>
      </section>
    </div>
  );
};

export default App;
