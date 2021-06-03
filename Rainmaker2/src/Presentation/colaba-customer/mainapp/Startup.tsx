import * as React from "react";
import * as ReactDOM from "react-dom";
import App from "./src/app/App";


// if (process.env.NODE_ENV == 'development') {
//   import('./Master').then((module)=>{
//       console.log('Master SCSS Loaded!!!')
//   })
// }

import './Master';
import './src/assets/scss/colaba.scss';
import { LocalDB } from "./src/lib/localStorage";
import { UserActions } from "./src/Store/actions/UserActions";
import { applyTheme } from "./Utilities/helpers/CommonFunc";
import { StoreProvider } from "./src/Store/Store";
import { Fragment } from "react";

LocalDB.getcaptchaCode(UserActions.getTenantSettings, null).
then((res)=>{
    if(res){
      applyTheme(res.data.color)
      LocalDB.storeCookiePath(res.data.cookiePath)
    }
    ReactDOM.render( <StoreProvider><App tenantSettings={res.data}/></StoreProvider>, document.getElementById("root"));
  })
  .catch(()=>{
    ReactDOM.render(<Fragment><div><p>Something went wrong</p></div></Fragment>, document.getElementById("root"));
  })
  



// import './src/assets/scss/colaba.scss';
