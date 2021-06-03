import React, { useEffect } from 'react'
import '../../lib/rs-authorization.js'

export const Authorize = ({ children }) => {
    
    useEffect(() => {
        authenticate();
    }, []);

    const authenticate = async () => {  
        await window.Authorization.authorize();
    };
    console.log("window.Authorization.checkAuth()", window.Authorization.checkAuth())
    if (window.Authorization.checkAuth()) {
        return (
            <React.Fragment>
                {children}
            </React.Fragment>
        )
    } else {
        return (
            <React.Fragment>
                <h2>Authorization Failed</h2>
            </React.Fragment>
        )
    }

}
