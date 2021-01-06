import React, { useEffect, useState } from 'react';
import logo from './logo.svg';
import './App.scss';
import { Header } from './components/Header/Header';
import { Home } from './components/Home/Home';
import { Footer } from './components/Footer/Footer';
import { StoreProvider } from './Store/Store';
import {BrowserRouter as Router, Route, Switch} from 'react-router-dom';
// import { LocalDB } from './Assets/js/rs-authorization';
import { UserActions } from './Store/actions/UserActions';
import { Authorized } from './components/Authorized/Authorized';
import IdleTimer from 'react-idle-timer';
import { LocalDB } from './Utilities/LocalDB';

let baseUrl: any = window?.envConfig?.API_BASE_URL;
declare global {
  interface Window {
    envConfig: any;
    Authorization: any;
  }
} // Change for pull request 3333
// Change for pull request 3333
window.envConfig = window.envConfig || {};
// conflict change
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

  const authenticate = async () => {
    console.log('Before Authorize');
    //if (process.env.NODE_ENV === 'development') {
      await window.Authorization.authorize();
    //}
    if (LocalDB.getAuthToken()) {
      console.log('After Authorize');
      setAuthenticated(Boolean(true));
      UserActions.keepAliveParentApp();
    }
  };

  const onIdle = (e: any) => {
    console.log('Idle time meet');
    //window.onbeforeunload = null;
    //LocalDB.removeAuth();
    //window.open("/Login/LogOff", "_self");
    //window.top.location.href = '/Login/LogOff';
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
    <>
      <StoreProvider>
        <Router basename="/DocManager">
          <Switch>
            <div data-testid="app-body" className="settings__wrapper">
              <Header />
              <div className="settings__body">
                
                <Route path="/:loanApplicationId/" component={Home} />
              </div>
            </div>
          </Switch>
        </Router>
      </StoreProvider>
      <Footer/>
    </>
  );
};


export default App;
