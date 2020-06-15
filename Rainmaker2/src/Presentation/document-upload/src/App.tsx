import React from 'react';
import './App.scss';
import { BrowserRouter as Router, Switch, Route, useHistory } from 'react-router-dom';
import { Home } from './components/Home/Home';
import Header from './shared/Components/Header/Header';
import Footer from './shared/Components/Footer/Footer';
import DummyLogin from './components/DummyLogin/DummyLoging';
import { StoreProvider } from './store/store';
import { Loading } from './components/Loading/Loading';
import { PageNotFound } from './shared/Errors/PageNotFound';
import { RainsoftRcHeader, RainsoftRcFooter } from 'rainsoft-rc';
import { UserActions } from './store/actions/UserActions';
import ImageAssets from './utils/image_assets/ImageAssets';

const App = () => {

  const currentyear = new Date().getFullYear();

  const history = useHistory();

  const gotoDashboardHandler = () => {
    console.log('gotoDashboardHandler')
    window.open('https://alphatx.rainsoftfn.com/Dashboard', '_self');
  }
  const changePasswordHandler = () => {
    console.log('changePasswordHandler')
    window.open('https://alphatx.rainsoftfn.com/Account/SendResetPasswordRequest', '_self');
  }
  const signOutHandler = () => {
    UserActions.logout();
    window.open('https://alphatx.rainsoftfn.com/', '_self');
  }
  const headerDropdowmMenu = [
    { name: 'Dashboard', callback: gotoDashboardHandler },
    { name: 'Change Password', callback: changePasswordHandler },
    { name: 'Sign Out', callback: signOutHandler }
  ]
  const footerContent = "Copyright 2002 – " + currentyear + ". All rights reserved. American Heritage Capital, LP. NMLS 277676";

  return (
    <div className="app">
      <StoreProvider>
        <RainsoftRcHeader
          logoSrc={ImageAssets.header.logoheader}
          displayName={'Jehangir Babul'}
          displayNameOnClick={gotoDashboardHandler}
          options={headerDropdowmMenu}
        />

        <Router basename="/documentmanagement" >
          <Switch>
            <Route  path="/" component={Home} />
            <Route path={"*"} component={PageNotFound} />
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
