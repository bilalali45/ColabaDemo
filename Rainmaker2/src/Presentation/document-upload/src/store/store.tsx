import React, { createContext, useReducer, ReactFragment } from 'react'
import { authReducer } from './reducers/aauthReducer';
import { userReducer } from './reducers/userReducer';
import { mainReducer } from './reducers/reducers';
import { Http } from '../services/http/Http';

const httpClient = new Http();

httpClient.setBaseUrl('http://localhost:5000');

export type InitialStateType = {
    auth: {} | null,
    user: {} | null
}

export const initialState = {
    auth: {},
    user: {}
};

const Store = createContext<{
    state: InitialStateType,
    dispatch: React.Dispatch<any>
}>({
    state: initialState,
    dispatch: () => null
});


const StoreProvider: React.FC = ({children}) => {

    const [state, dispatch] = useReducer(mainReducer, initialState);

    return (
        <Store.Provider value={{state, dispatch}} >
            {children}
        </Store.Provider>
    )
}

export {Store, StoreProvider}