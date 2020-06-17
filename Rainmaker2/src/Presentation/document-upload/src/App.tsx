import React from 'react';
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

const App = () => {

  const currentyear = new Date().getFullYear();
  const tokenData : any = UserActions.getUserInfo();
  const history = useHistory();
  
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

  return (
    <div className="app">
      <StoreProvider>
        <RainsoftRcHeader
          logoSrc={ImageAssets.header.logoheader}
          displayName={tokenData.UserName}
          displayNameOnClick={gotoDashboardHandler}
          options={headerDropdowmMenu}
        />

        <Router basename="/DocumentManagement" >
          <Switch>
            <Route path="/" component={Home} />
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
