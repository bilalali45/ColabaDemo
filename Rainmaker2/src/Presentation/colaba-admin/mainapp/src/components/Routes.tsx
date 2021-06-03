import React, { useContext, createContext, useState } from "react";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
  Redirect,
  useHistory,
  useLocation,
} from "react-router-dom";

import LoginPage from "../pages/LoginPage";
import Notifications from "../pages/Notifications";
import { ProvideAuth, useAuth } from "./AuthContext";
import Footer from "./Footer";
import Header from "./Header";

const MCU = React.lazy(() => import("mfmcu/mcu"));
const Settings = React.lazy(()=> import("mfsettings/settings"));
const Notificatoins = React.lazy(()=> import("mfnotifications/notifications" ))
const DocManager = React.lazy(()=> import("mfdocmanager/docmanager"))

export default function AuthExample() {
  return (
    <ProvideAuth>
      <Router>
        <div>
          <Header />
          <AuthButton />
          <ul>
            <li>
              <Link to="/DocumentManagement/2515">MCU</Link>
            </li>

            <li>
              <Link to="/Settings/2515">Settings</Link>
            </li>

            <li>
              <Link to="/Notifications/2515">Notifications</Link>
            </li>

            <li>
              <Link to="/DocManager/2515">Doc Manager</Link>
            </li>

            <li>
              <Link to="/protected">Protected Page</Link>
            </li>
          </ul>
          <Switch>
          <Route
          path="/DocumentManagement/:id"
          component={MCUApp}
        />
        <Route
          exact
          path="/:id"
          component={MCUApp}
        />
            <Route path="/login">
              <LoginPage />
            </Route>
            <Route path="/Settings/:id" component={SettingsApp}/>
            <Route path="/Notifications/:id" component={NotificationsApp}/>
            <Route path="/DocManager/:id" component={DocManagerApp}/>
              
            <PrivateRoute exact path="/protected">
              <Notifications />
            </PrivateRoute>
          </Switch>
          <Footer />
        </div>
      </Router>
    </ProvideAuth>
  );
}

function AuthButton() {
  let history = useHistory();
  let auth: any = useAuth();

  return auth.user ? (
    <p>
      Welcome!
      <button
       className="btn btn-danger"
        onClick={() => {
          auth.signout(() => history.push("/"));
        }}
      >
        Sign out
      </button>
    </p>
  ) : (
    <p>You are not logged in.</p>
  );
}

function PrivateRoute({ children, ...rest }) {
  let auth: any = useAuth();

  return (
    <Route
      {...rest}
      render={({ location }) =>
        auth.user ? (
          children
        ) : (
          <Redirect
            to={{
              pathname: "/login",
              state: { from: location },
            }}
          />
        )
      }
    />
  );
}

function MCUApp() {
  return (
    <React.Suspense fallback="Loading...">
      <MCU />
    </React.Suspense>
  )
}


function SettingsApp() {
  return (
    <React.Suspense fallback="Loading...">
      <Settings />
    </React.Suspense>
  )
}

function NotificationsApp() {
  return (
    <React.Suspense fallback="Loading...">
      <Notificatoins />
    </React.Suspense>
  )
}

function DocManagerApp() {
  return (
    <React.Suspense fallback="Loading...">
      <DocManager />
    </React.Suspense>
  )
}