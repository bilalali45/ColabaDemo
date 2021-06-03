import React, { createContext, useReducer, Dispatch } from 'react';
import { mainReducer } from './reducers/reducers';
import { ErrorType } from './reducers/ErrorReducer';
import { LoggedInUserLoanApplicationsType } from './reducers/DashboardReducer';




import { ApplicationEnv } from '../lib/appEnv';
import { UserType } from './reducers/UserReducer';

if (process.env.NODE_ENV !== 'test') {
    import('rainsoft-js').then(rainsoftJs => {
        let baseUrl: any = window?.envConfig?.API_BASE_URL;
        let { Http } = rainsoftJs;
        process.env.NODE_ENV === 'development' ? new Http(baseUrl, "Rainmaker2Token", ApplicationEnv.ColabaWebUrl) : new Http(baseUrl, "Rainmaker2Token", location.href);
    });
}

export type InitialStateType = {
    user: UserType,
    error: ErrorType | {},
    dashboard: LoggedInUserLoanApplicationsType | {}

};

export const InitialState: InitialStateType = {
    user: {
        userInfo: {},
        tenantInfo: { logo: '', cookiePath: '' }
    },
    error: {},
    dashboard: {
        loanApplications: null,
        isLoaded: false
    }
};

const Store = createContext<{
    state: InitialStateType;
    dispatch: Dispatch<any>;
}>({
    state: InitialState,
    dispatch: () => null
});

const StoreProvider: React.FC = ({ children }) => {
    const [state, dispatch] = useReducer(mainReducer, InitialState);
    return <Store.Provider value={{ state, dispatch }}>{children}</Store.Provider>
};

export { Store, StoreProvider };