import React, { useEffect, useState } from "react";
import img from './t.jpg';
import { LocalDB } from "../lib/localStorage";

import "./envconfig";
import AuthExample from "./Routes";
import "./rs-authorization";


declare global {
  interface Window {
    envConfig: any;
    Authorization: any;
  }
}

window.envConfig = window.envConfig || {};


const App = () => {
  const [authenticated, setAuthenticated] = useState(false);

  useEffect(() => {
    const authenticate = async () => {
      if (process.env.NODE_ENV === "development") {
        await window.Authorization.authorize();
      }

      if (LocalDB.getAuthToken()) {
        setAuthenticated(Boolean(true));
      }
    };

    authenticate();

    return () => {
      LocalDB.removeAuth();
    };
  }, []);

  if (!authenticated) {
    return null;
  }

  return (
    <>
    <img src={img} />
      <AuthExample />
    </>
  );
};

export default App;
