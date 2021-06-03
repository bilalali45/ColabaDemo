import * as React from "react";
import * as ReactDOM from "react-dom";
import App from "./App";
import { StoreProvider } from "./store/store";

if (process.env.NODE_ENV == 'development') {
    import('./Master').then((module) => {
        console.log('Master SCSS Loaded!!!');
    });
}

// import './../../assets-hub/scss/master.scss';
import './assets/scss/loanportal.scss';

ReactDOM.render(<App />, document.getElementById("loanportalapp"));
