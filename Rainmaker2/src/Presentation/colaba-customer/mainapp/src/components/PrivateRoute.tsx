import React from "react";
import { Redirect, Route, RouteProps } from "react-router-dom";

interface MyRouteProps extends RouteProps {
    component: React.ComponentType;
    footer?: React.ReactNode;
    props?: any;
    // any other props that come into the component
}

export const PrivateRoute: React.FC<MyRouteProps> = ({
    component: Component,
    footer: Footer,
    ...props
}) => {
    let auth: boolean = window.Authorization.checkAuth();
    return (
        <>
        <Route
            {...props}
            render={({ location }) =>
                auth ? (
                    <Component {...props} />
                ) : (
                    <Redirect
                        to={{
                            pathname: "/signin",
                            state: { from: location },
                        }}
                    />
                )
            }
        />
        {Footer}
        </>
    );
}
