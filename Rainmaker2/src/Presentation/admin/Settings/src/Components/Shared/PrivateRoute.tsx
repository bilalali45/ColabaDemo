import React from 'react'
import { Route, Redirect } from 'react-router-dom';
import { LocalDB} from '../../Utils/LocalDB'


export const PrivateRoute = ({ component: Component, ...rest } : any) => (
    // <div></div>
    <Route
    {...rest}
    render={props =>
            LocalDB.getAuthentication(props.location.pathname) 
            ? (<Component {...props} />) 
            : (<Redirect to={{ pathname: "/PageNotFound"}}/>)
        }
    />
    );