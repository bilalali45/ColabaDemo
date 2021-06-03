import SignIn from "../pages/SignIn/SignIn";
import { SignUp } from "../pages/SignUp";
import React, { useContext } from "react";
import { BrowserRouter as Router, Switch } from "react-router-dom";
import ForgotPassword from "../pages/ForgotPassword/ForgotPassword";
import ResetPassword from "../pages/ResetPassword/ResetPassword";

import { Store } from '../Store/Store';
import Footer, { FooterType } from '../components/Footer';
import { Homepage } from "../pages/HomePage/_HomePage";
import { Dashboard } from "../pages/Dashboard/_Dashboard";
import { useEffect } from "react";
import { LocalDB } from "../lib/localStorage";
import { UserActionsType } from "../Store/reducers/UserReducer";
import { Fragment } from "react";
import { PrivateRoute } from "../components/PrivateRoute";
import NotFound from "../components/NotFound";
import { ApplicationEnv } from "../lib/appEnv";
import Loader from "../Shared/Components/Loader";
import { PublicRoute } from "../components/PublicRoute";


window.envConfig = window.envConfig || {};

console.log('process.env.NODE_ENV', process.env.NODE_ENV);
type Props = {
  tenantSettings: any
}

const App = ({ tenantSettings }: Props) => {

  const { dispatch } = useContext(Store);

  const baseUrl = process.env.NODE_ENV == "production" ? window.location.pathname.toLowerCase().split('/app/')[0] + ApplicationEnv.ApplicationBasePath : "/"
  console.log("baseUrl", baseUrl)
  useEffect(() => {
    getTenantSetting()
    window.scrollTo(0, 0)
  }, [])

  const getTenantSetting = () => {
    if (tenantSettings) {
      LocalDB.storeCookiePath(tenantSettings.cookiePath || "/")
      dispatch({ type: UserActionsType.SetTenantInfo, payload: tenantSettings });
      document.title = tenantSettings.dbaName;
    }

  }


  return (
    <Fragment>
      <React.Suspense fallback={<Loader type="page"/>}>
        {/* <LoanApplicationPortal /> */}
        <Router basename={baseUrl}>
          <Switch>
            <PrivateRoute
              footer={<Footer type={FooterType.LoanApplication}/>}
              component={Homepage}
              path="/loanapplication">
            </PrivateRoute>

            <PrivateRoute
              footer={<Footer type={FooterType.WithoutCaptcha}/>}
              component={Dashboard}
              path="/dashboard">
            </PrivateRoute>

            <PrivateRoute
              footer={<Footer type={FooterType.WithoutCaptcha}/>}
              component={Homepage}
              path="/homepage">
            </PrivateRoute>

            <PublicRoute
              footer={<Footer type={FooterType.WithCaptcha}/>}
              path="/signin"
              component={SignIn}
            />

            <PublicRoute
              footer={<Footer type={FooterType.WithCaptcha}/>}
              path="/signup"
              component={SignUp}
            />
            <PublicRoute
              footer={<Footer type={FooterType.WithoutCaptcha}/>}
              path="/forgotPassword"
              component={ForgotPassword}
            />
            <PublicRoute
              footer={<Footer type={FooterType.WithoutCaptcha}/>}
              exact
              path="/resetPassword"
              component={ResetPassword}
            />
            <PrivateRoute
              footer={<Footer/>}
              exact
              path="/"
              component={Homepage}
            />
            <PublicRoute footer={FooterType.WithoutCaptcha} component={NotFound} />
          </Switch>

          

        </Router>
        {/* <Footer /> */}
      </React.Suspense>
    </Fragment>

  )
};

export default App;
