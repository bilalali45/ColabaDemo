import React, {createContext, useReducer} from 'react';
import {mainReducer} from './reducers/reducers';
import {TemplateType} from './reducers/TemplatesReducer';
import {Http} from 'rainsoft-js';
import {LocalDB} from '../Utils/LocalDB';
import {NeedListType} from './reducers/NeedListReducer';
const baseUrl: any = window?.envConfig?.API_BASE_URL;
const httpClient = new Http(baseUrl, 'token');

//let auth = LocalDB.getAuthToken();s

//httpClient.setBaseUrl(baseUrl);
//if (auth) httpClient.setAuth(auth);

export type InitialStateType = {
  user: {
    userInfo: {};
  };
  needListManager: NeedListType | {};
  templateManager: TemplateType | {};
};

export const InitialState = {
  user: {
    userInfo: {}
  },
  needListManager: {},
  templateManager: {}
};

const Store = createContext<{
  state: InitialStateType;
  dispatch: React.Dispatch<any>;
}>({
  state: InitialState,
  dispatch: () => null
});

const StoreProvider: React.FC = ({children}) => {
  const [state, dispatch] = useReducer(mainReducer, InitialState);

  return <Store.Provider value={{state, dispatch}}>{children}</Store.Provider>;
};

export {Store, StoreProvider};
