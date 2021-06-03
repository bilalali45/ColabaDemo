import React from 'react'
import { Redirect, Route} from 'react-router'
import { NavigationHandler } from '../NavigationHandler'
import { NavType } from '../NavItem'
import { UrlQueryManager } from '../UrlQueryManager'


const addQueryToPath = (path, search) => {
    return `${path}?${search}`;
}

export const IsRouteAllowed = ({ component, path, ...customProps }) => {
    let Comp = component;
    let query = `q=${UrlQueryManager.queryEncoded}`;

    if (NavigationHandler.isStepDisabled(path)) {
        let stepFound = NavigationHandler.getStepByPath(path);
        let redirectPath = '';

        if (stepFound.type === NavType && stepFound.isDisabled) {
            if (stepFound.navItems.length) {
              
                let lastSubNav = stepFound.navItems[stepFound.navItems.length - 1];
                redirectPath = lastSubNav.steps[lastSubNav.steps.length - 1].nextPath;
          
            } else {

                redirectPath = stepFound.steps[stepFound.steps.length - 1]?.nextPath;
            }
        } else {
            
            redirectPath = stepFound?.nextPath;

        }
        
        return <Redirect to={addQueryToPath(redirectPath, query)} />
    }
    return <Route path={path} render={(props) => <Comp {...props} {...customProps} />} />
}
