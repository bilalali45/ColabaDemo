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
import { Authorized } from './shared/Components/Authorized/Authorized';
import { FooterContents } from './utils/header_footer_utils/FooterContent';
import HeaderContent from './utils/header_footer_utils/HeaderContent';

const App = () => {

  const currentyear = new Date().getFullYear();
  // const [cookies, setCookie] = useCookies(['Rainmaker2Token']);
  const [authenticated, setAuthenticated] = useState<boolean>(false);

  const tokenData: any = UserActions.getUserInfo();
  const history = useHistory();

  // setCookie('Rainmaker2Token', Auth.checkAuth());
  useEffect(() => {
    
    // console.log(Cookies.get('Rainmaker2Token'))
    console.log("Document Management App Version", "0.1.2");
    authenticate();
    UserActions.getUserInfo();
    ParamsService.storeParams(['loanApplicationId', 'tenantId', 'businessUnitId']);
  }, [localStorage])

  const authenticate = async () => {
    let isAuth = await Auth.authenticate();
    setAuthenticated(Boolean(isAuth));
  }

  if (!authenticated) {
    return <></>
  }
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
            <Authorized path="/" component={Home} />
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
