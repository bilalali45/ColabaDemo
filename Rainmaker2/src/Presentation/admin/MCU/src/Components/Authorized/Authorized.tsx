import * as React from 'react';
import * as ReactDOM from 'react-dom';
import {Route, Redirect, RouteProps} from 'react-router-dom';
import {LocalDB} from '../../Utils/LocalDB';

interface MyRouteProps extends RouteProps {
  component: any;
  props?: any;
}

export const Authorized: React.SFC<MyRouteProps> = ({
  component: Component,
  ...props
}) => {
  return (
    <Route
      {...props}
      render={(props) => {
        if (LocalDB.checkAuth()) {
          return <Component {...props} />;
        }
        return <Redirect to="/Login/LogOff" />;
      }}
    />
  );
};
