import React, { createContext, useReducer } from 'react'
import { mainReducer } from './reducers/reducers'
import { TemplateType } from './reducers/TemplatesReducer'
import {Http} from 'rainsoft-js'
import { LocalDB } from '../Utils/LocalDB';

const http = new Http();
http.setBaseUrl('https://alphamaingateway.rainsoftfn.com');
http.setAuth(LocalDB.getAuthToken());

export type InitialStateType = {
    user: {
        userInfo: {}
    },
    templateManager: TemplateType | {},
}

export const InitialState = {
    user: {
        userInfo: {}
    },
    templateManager: {},
}

const Store = createContext<{
    state: InitialStateType,
    dispatch: React.Dispatch<any>
}>({
    state: InitialState,
    dispatch: () => null
})

const StoreProvider: React.FC = ({ children }) => {

    const [state, dispatch] = useReducer(mainReducer, InitialState)

    return (
        <Store.Provider value={{ state, dispatch }}>
            {children}
        </Store.Provider>
    )
}

export {
    Store, StoreProvider
}