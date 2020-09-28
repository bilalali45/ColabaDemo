import React, {useEffect, useState} from 'react';
import {BrowserRouter as Router, Route, Switch} from 'react-router-dom';
import {SignalRHub} from 'rainsoft-js';
import IdleTimer from 'react-idle-timer';

import './App.scss';
import {MCUHome} from './Components/MCUHome/MCUHome';
import {RainMakerFooter} from './Components/RainMakerFooter/RainMakerFooter';
import {StoreProvider} from './Store/Store';
import {UserActions} from './Store/actions/UserActions';
import {LocalDB} from './Utils/LocalDB';

import {Authorized} from './Components/Authorized/Authorized';

let baseUrl: any = window?.envConfig?.API_BASE_URL;
declare global {
  interface Window {
    envConfig: any;
  }
}
window.envConfig = window.envConfig || {};

const App = () => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  const hubUrl: string = baseUrl + '/serverhub';

  useEffect(() => {
    console.log('MCU App Version', '0.0.1');
    authenticate();
    // component unmount
    return () => {
      LocalDB.removeAuth();
    };
  }, []);

  // console.log(
  //   '******************************************',
  //   window.location.pathname
  // );

  const authenticate = async () => {
    console.log('Before Authorize');
    let isAuth = await UserActions.authorize();
    setAuthenticated(Boolean(isAuth));
    console.log('After Authorize');
    UserActions.addExpiryListener();
    UserActions.keepAliveParentApp();
  };

  const onIdle = (e: any) => {
    console.log('Idle time meet');
    window.onbeforeunload = null;
    LocalDB.removeAuth();
    //window.open("/Login/LogOff", "_self");
    window.top.location.href = '/Login/LogOff';
  };

  console.log(
    'Authorize User is authenticated',
    authenticated,
    window?.envConfig?.IDLE_TIMER
  );
  if (!authenticated) {
    return null;
  }
  // console.log('Node Env ++++++++++++++++++++++', process.env.NODE_ENV);
  return (
    <div className="App">
      {process.env.NODE_ENV !== 'test' && <IdleTimer
        element={document}
        onIdle={onIdle}
        debounce={250}
        timeout={1000 * 60 * window?.envConfig?.IDLE_TIMER}
      />}
      {/* <RainMakerHeader /> */}
      <section className="d-layout">
        {/* <RainMakerSidebar /> */}
        <main className="main-layout">
          <StoreProvider>
            <Router basename="/DocumentManagement">
              <Switch>
                {process.env.NODE_ENV === 'test' ? (
                  <Authorized path="/" component={MCUHome} />
                ) : (
                  <Authorized
                    path="/:activity/:loanApplicationId/"
                    component={MCUHome}
                  />
                )}

                <Authorized
                  exact
                  path="/:loanApplicationId"
                  component={MCUHome}
                />

                <RainMakerFooter />
              </Switch>
            </Router>
          </StoreProvider>
        </main>
      </section>
    </div>
  );
};

export default App;
