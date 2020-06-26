import React, { useEffect, useState } from 'react';
import './App.scss';
import { BrowserRouter as Router, Switch, Route, useHistory } from 'react-router-dom';
import { Home } from './components/Home/Home';
import { StoreProvider } from './store/store';
import { RainsoftRcHeader, RainsoftRcFooter } from 'rainsoft-rc';
import { UserActions } from './store/actions/UserActions';
import ImageAssets from './utils/image_assets/ImageAssets';
import { ParamsService } from './utils/ParamsService';
import { Authorized } from './shared/Components/Authorized/Authorized';
import { FooterContents } from './utils/header_footer_utils/FooterContent';
import HeaderContent from './utils/header_footer_utils/HeaderContent';
import { Auth } from './services/auth/Auth';
import { Http } from './services/http/Http';
import { LaonActions } from './store/actions/LoanActions';
const httpClient = new Http();
const App = () => {


  const [authenticated, setAuthenticated] = useState<boolean>(false);
  const [expListnerAdded, setExpListnerAdded] = useState(false);
  const [footerText, setFooterText] = useState('');
  const tokenData: any = UserActions.getUserInfo();
  const displayName = ' '+tokenData?.FirstName+' '+tokenData?.LastName;
  const history = useHistory();

  useEffect(() => {
    console.log("Document Management App Version", "0.1.3");
    authenticate();
    getFooterText();
    ParamsService.storeParams(['loanApplicationId', 'tenantId', 'businessUnitId']);
  }, [])

  const authenticate = async () => {
    let isAuth = await UserActions.authorize();
    setAuthenticated(Boolean(isAuth));
  }

  const getFooterText = async () => {
   let footerText = await  LaonActions.getFooter(Auth.getTenantId(), Auth.getBusinessUnitId());
   setFooterText(footerText);
  }

  if (!authenticated) {
    return null;
  }

  if (Auth.getUserPayload()) {
    UserActions.addExpiryListener(Auth.getUserPayload());
    // setExpListnerAdded(true);
  }

  return (
    <div className="app">
      <StoreProvider>
        <RainsoftRcHeader
          logoSrc={ImageAssets.header.logoheader}
          displayName={displayName}
          displayNameOnClick={HeaderContent.gotoDashboardHandler}
          options={HeaderContent.headerDropdowmMenu}
        />

        <Router basename="/DocumentManagement" >
          <Switch>
            <Authorized path="/" component={Home} />
          </Switch>
        </Router>
        <RainsoftRcFooter
          content={footerText}
        />
      </StoreProvider>
    </div>
  );
}

export default App;
