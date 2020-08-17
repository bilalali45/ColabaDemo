import React from "react";
import { BrowserRouter, Route, Switch } from "react-router-dom";

import { HomePage } from "../pages/HomePage";

export const AppRoutes = () => {
  return (
    <BrowserRouter>
      <Switch>
        <Route exact path="/" component={HomePage} />
      </Switch>
    </BrowserRouter>
  );
};
