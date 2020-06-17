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
import {FooterContents} from './utils/header_footer_utils/FooterContent';
import HeaderContent from './utils/header_footer_utils/HeaderContent';

const App = () => {


  const tokenData : any = UserActions.getUserInfo();
  const history = useHistory();
  
    ParamsService.storeParams(['loanApplicationId', 'tenantId', 'businessUnitId']);

  return (
    <div className="app">
      <StoreProvider>
        <RainsoftRcHeader
          logoSrc={ImageAssets.header.logoheader}
          displayName={tokenData?.UserName}
          displayNameOnClick={HeaderContent.gotoDashboardHandler}
          options={HeaderContent.headerDropdowmMenu}
        />

        <Router basename="/DocumentManagement" >
          <Switch>
            <Route path="/" component={Home} />
          </Switch>
        </Router>
        <RainsoftRcFooter
          content={FooterContents.footerContent}
        />
      </StoreProvider>
    </div>
  );
}

export default App;
