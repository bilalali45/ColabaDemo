import * as React from "react";
import * as ReactDOM from "react-dom";
import App from "./App";


if (process.env.NODE_ENV == 'development') {
    import('./Master').then(() => {
        console.log('Master SCSS Loaded!!!');
    });
}

ReactDOM.render(<App />, document.getElementById("loanportalapp"));
