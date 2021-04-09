import React, { useEffect, useState } from 'react';
import { BrowserRouter as Router, Route, Switch } from 'react-router-dom';
import { SignalRHub } from 'rainsoft-js';
import IdleTimer from 'react-idle-timer';

import './App.scss';
import { MCUHome } from './Components/MCUHome/MCUHome';
import { RainMakerFooter } from './Components/RainMakerFooter/RainMakerFooter';
import { StoreProvider } from './Store/Store';
import { UserActions } from './Store/actions/UserActions';
import { LocalDB } from './Utils/LocalDB';

import { Authorized } from './Components/Authorized/Authorized';
import { useCookies } from 'react-cookie';

let baseUrl: any = window?.envConfig?.API_BASE_URL;
declare global {
  interface Window {
    envConfig: any;
    Authorization: any;
  }
}
window.envConfig = window.envConfig || {};

const App = (props: any) => {
  const [authenticated, setAuthenticated] = useState<boolean>(false);
  const hubUrl: string = baseUrl + '/serverhub';
  const [rainmakerCookie, setCookieAccess] = useCookies(['Rainmaker2Token']);
  const [rainmakerRefreshCookie, setCookieRefresh] = useCookies(['Rainmaker2RefreshToken']);

  useEffect(() => {
    console.log('MCU App Version', '0.0.1');
    if (props.authToken && props.refreshToken) {
      setCookieAccess('Rainmaker2Token', props.authToken, { path: '/', secure: true, sameSite: 'none', domain: window.location.host });
      setCookieRefresh('Rainmaker2RefreshToken', props.refreshToken, { path: '/', secure: true, sameSite: 'none', domain: window.location.host });
    }

    authenticate();
    // component unmount
    return () => {
      LocalDB.removeAuth();
    };
  }, []);

  const authenticate = async () => {
    console.log('Before Authorize');
    //if (process.env.NODE_ENV === 'development') {
    await window.Authorization.authorize();
    //}
    if (LocalDB.getAuthToken()) {
      setAuthenticated(Boolean(true));
      UserActions.keepAliveParentApp();
    }
    console.log('After Authorize');

  };

  const onIdle = (e: any) => {
    console.log('Idle time meet');
    // window.onbeforeunload = null;
    // LocalDB.removeAuth();
    // //window.open("/Login/LogOff", "_self");
    // window.top.location.href = '/Login/LogOff';
  };

  console.log(
    'Authorize User is authenticated',
    authenticated,
    window?.envConfig?.IDLE_TIMER
  );
  if (!authenticated) {
    return null;
  }
  return (
    <div className="App">
      {process.env.NODE_ENV !== 'test' && (
        <IdleTimer
          element={document}
          onIdle={onIdle}
          debounce={250}
          timeout={1000 * 60 * window?.envConfig?.IDLE_TIMER}
        />
      )}
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


              </Switch>
            </Router>
            <RainMakerFooter />
          </StoreProvider>
        </main>
      </section>
    </div>
  );
};

export default App;
