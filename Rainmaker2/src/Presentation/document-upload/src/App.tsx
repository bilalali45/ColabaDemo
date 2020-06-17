import React, { useEffect, useState } from 'react';
import './App.scss';
import { BrowserRouter as Router, Switch, Route, useHistory } from 'react-router-dom';
import { Home } from './components/Home/Home';
import Header from './shared/Components/Header/Header';
import Footer from './shared/Components/Footer/Footer';
import DummyLogin from './components/DummyLogin/DummyLoging';
import { StoreProvider } from './store/store';
import { Loading } from './components/Loading/Loading';
import { RainsoftRcHeader, RainsoftRcFooter } from 'rainsoft-rc';
import { UserActions } from './store/actions/UserActions';
import ImageAssets from './utils/image_assets/ImageAssets';
import { ParamsService } from './utils/ParamsService';
import { Auth } from './services/auth/Auth';
import { useCookies } from 'react-cookie';
import { Authorized } from './shared/Components/Authorized/Authorized';

const App = () => {

  const currentyear = new Date().getFullYear();
  const history = useHistory();
  const [cookies, setCookie] = useCookies(['Rainmaker2Token']);
  const [authenticated, setAuthenticated] = useState<boolean>(false);


  useEffect(() => {
    authenticate();
  }, [localStorage])

  const authenticate = async () => {


    if (!Auth.checkAuth()) {
     
      if (process.env.NODE_ENV === 'development') {
        let token = await UserActions.authenticate();
        if (token) {
          Auth.saveAuth(token);
          setAuthenticated(true);
        }

      }

      if (cookies != undefined && cookies.Rainmaker2Token != undefined) {
        Auth.saveAuth(cookies.Rainmaker2Token);
        setAuthenticated(true);
      }
    } else {
      setAuthenticated(true);
    }
  }

  const gotoDashboardHandler = () => {
    window.open('/Dashboard', '_self');
  }
  const changePasswordHandler = () => {
    window.open('/Account/SendResetPasswordRequest', '_self');
  }
  const signOutHandler = () => {
    UserActions.logout();
    window.open('/Account/Login', '_self');
  }
  const headerDropdowmMenu = [
    { name: 'Dashboard', callback: gotoDashboardHandler },
    { name: 'Change Password', callback: changePasswordHandler },
    { name: 'Sign Out', callback: signOutHandler }
  ]
  const footerContent = "Copyright 2002 – " + currentyear + ". All rights reserved. American Heritage Capital, LP. NMLS 277676";

  ParamsService.storeParams(['loanApplicationId', 'tenantId', 'businessUnitId']);

  if (!authenticated) {
    return <></>
  }

  UserActions.getUserInfo();

  return (
    <div className="app">
      <StoreProvider>
        <RainsoftRcHeader
          logoSrc={ImageAssets.header.logoheader}
          displayName={'Jehangir Babul'}
          displayNameOnClick={gotoDashboardHandler}
          options={headerDropdowmMenu}
        />

        <Router basename="/DocumentManagement" >
          <Switch>
            <Authorized path="/" component={Home} />
          </Switch>
        </Router>
        <RainsoftRcFooter
          content={footerContent}
        />
      </StoreProvider>
    </div>
  );
}

export default App;
