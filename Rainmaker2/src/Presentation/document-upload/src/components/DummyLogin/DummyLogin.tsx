import React, { useContext } from "react";
import { Http } from "../../services/http/Http";
import { Redirect, useHistory } from "react-router-dom";
import { Auth } from "../../services/auth/Auth";
import { AuthActions } from "../../store/actions/User";
import { Store } from "../../store/store";
import { AuthTypes } from "../../store/reducers/aauthReducer";

const DummyLogin = () => {
  const { dispatch } = useContext(Store);

  const login = async () => {
    let res = await AuthActions.login({});
    if (res) {
      dispatch({ type: AuthTypes.Save, payload: { token: res } });
    }
  };

  if (Auth.checkAuth()) {
    return <Redirect to="/" />;
  }

  return (
    <div>
      <input type="email" value="test@test.com" />
      <input type="password" value="test123" />
      <button onClick={login}>Login</button>
    </div>
  );
};

export default DummyLogin;
