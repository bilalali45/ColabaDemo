import React from "react";
import { Route, RouteProps } from "react-router-dom";

interface PublicRouteProps extends RouteProps {
    component: React.ComponentType;
    footer: React.ReactNode;
    props?: any;
    // any other props that come into the component
}

export const PublicRoute: React.FC<PublicRouteProps> = ({
    component: Component,
    footer: Footer,
    ...props
}) => {
    return (
        <>
        <Route
            {...props}
            render={() =>
            <Component {...props} />
            }
        />
        {Footer}
        </>
    );
}
