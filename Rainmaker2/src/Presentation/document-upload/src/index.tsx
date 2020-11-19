import React from "react";
import ReactDOM from "react-dom";
import "./index.css";
import App from "./App";
import * as serviceWorker from "./serviceWorker";
import { StoreProvider } from "./store/store";

import LogRocket from 'logrocket';
import Cookies from "universal-cookie";
import { Auth } from "./services/auth/Auth";
import { debug } from "console";
const cookies = new Cookies();

if(window.envConfig.LOGROCKET_ENABLE){
    LogRocket.init('evhjvq/borrowerside-react', {
      // dom: {
      //     textSanitizer: true,
      //     inputSanitizer: true,
      // },
      network: {
          responseSanitizer: (response : any) => {
            if (response.headers['x-secret']) {
              // removes all response data
              return null;
            }

            // scrubs response body
            response.body = null;
            return response;
          },
      },
  });

  const visitorId = cookies.get("RsVid");
  const oppId = cookies.get("RsOpid");
  const sessionId = cookies.get("ASP.NET_SessionId");
  //const loanApplicationId = cookies.get("RsLoanApplicationId");
  const loanApplicationId = Auth.getLoanApplicationFromUrl(window.location.pathname);
  const identification = {
    UserSessionId: sessionId || '',
    VisitorId: visitorId || '',
    OpportunityId: oppId || '',
    LoanApplicationId: loanApplicationId || ''
  };
  LogRocket.identify(`${visitorId}-${loanApplicationId != "" ? loanApplicationId : ""}`, identification)
}


ReactDOM.render(<StoreProvider><App /></StoreProvider>, document.getElementById("root"));

// If you want your app to work offline and load faster, you can change
// unregister() to register() below. Note this comes with some pitfalls.
// Learn more about service workers: https://bit.ly/CRA-PWA
serviceWorker.unregister();
