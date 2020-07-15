import React, { createContext, useReducer } from 'react'
import { mainReducer } from './reducers/reducers'
import { TemplateType } from './reducers/TemplatesReducer'
import { Http } from 'rainsoft-js';
import { LocalDB } from '../Utils/LocalDB';
const httpClient = new Http();
let baseUrl : any = window.envConfig.API_BASE_URL; 
let auth = LocalDB.getAuthToken();

httpClient.setBaseUrl(baseUrl);
if(auth) httpClient.setAuth(auth)

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