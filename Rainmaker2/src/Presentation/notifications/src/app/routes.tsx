import React from 'react';
import {BrowserRouter, Route, Switch} from 'react-router-dom';

import {HomePage} from '../pages/HomePage';
import {NotFoundPage} from '../pages/NotFoundPage';

export const AppRoutes = () => {
  return (
    <BrowserRouter>
      <Switch>
        <Route path="/" component={HomePage} />
        <Route component={NotFoundPage} />
      </Switch>
    </BrowserRouter>
  );
};
