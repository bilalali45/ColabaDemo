import React from "react";
import { useHistory, useLocation } from "react-router-dom";

import { useAuth } from "../components/AuthContext";

function LoginPage() {
  let history = useHistory();
  let location = useLocation();
  let auth: any = useAuth();

  let { from }: any = location.state || { from: { pathname: "/" } };
  let login = () => {
    auth.signin(() => {
      history.replace(from);
    });
  };

  return (
    <div>
      <p>You must log in to view the page at {from.pathname}</p>
      <button className="btn btn-primary" onClick={login}>Log in</button>
    </div>
  );
}

export default LoginPage;
