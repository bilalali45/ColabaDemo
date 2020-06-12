import React, { useState } from 'react'
import { Auth } from '../../services/auth/Auth';
import { UserActions } from '../../store/actions/UserActions';
import { useHistory } from 'react-router-dom';

export const Loading = () => {
    const [authenticated, setAuthenticated] = useState<boolean>(false);

    const history = useHistory();

    const isAuthenticated = async () => {

        let auth = Auth.checkAuth();
        let isAuthenticated: any = null;
        if (!auth) {
            isAuthenticated = await UserActions.authenticate();
            if (isAuthenticated && isAuthenticated.token !== undefined) {
                Auth.saveAuth(isAuthenticated.token);
                setAuthenticated(true);
                setTimeout(() => {
                    history.push('/home/activity')
                }, 0);
            } else {
                setTimeout(() => {
                    window.open('http://localhost:5000/app', '_self');
                }, 0);
            }
        } else {
            setTimeout(() => {
                history.push('/home/activity')
            }, 0);
        }


    }

    isAuthenticated();

    return (
        <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', height: '100vh', flexDirection: 'column' }}>
            <h1>Please wait...</h1>
            <h3 style={{ marginTop: '20px' }}>The system is verifying your account</h3>
        </div>
    )
}
