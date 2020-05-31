import React from 'react';
import { Http } from '../../services/http/Http';
import { Redirect, useHistory } from 'react-router-dom';
import { Auth } from '../../services/auth/Auth';

const httpClient = new Http();


const DummyLogin = () => {
    
    console.log(httpClient);
    let history = useHistory();

    const login = async () => {

        const res: any = await httpClient.post('/login', { email: 'test@test.com', password: 'test123' });
        let token = res?.data?.token;
        if (token) {
            Auth.saveAuth(token);
            history.push('/');
        }
        console.log(res);
    }

    if (Auth.checkAuth()) {
        return <Redirect to="/" />
    }


    return (
        <div>

            <input type="email" value="test@test.com" />
            <input type="password" value="test123" />
            <button onClick={login}>Login</button>
        </div>
    )
}

export default DummyLogin;