import React, { createContext, useReducer } from "react";
import { mainReducer } from "./reducers/reducers";
import { Http } from "rainsoft-js";
import { LoanType } from "./reducers/loanReducer";
import { DocumentsType } from "./reducers/documentReducer";
import { Auth } from "../services/auth/Auth";

const httpClient = new Http();
let baseUrl: any = window.envConfig.API_BASE_URL;
let auth = Auth.getAuth();

httpClient.setBaseUrl(baseUrl);
if (auth) httpClient.setAuth(auth);

export type InitialStateType = {
  loan: LoanType | {};
  documents: DocumentsType | {};
};

export const initialState = {
  loan: {},
  documents: {}
};

const Store = createContext<{
  state: InitialStateType;
  dispatch: React.Dispatch<any>;
}>({
  state: initialState,
  dispatch: () => null,
});

const StoreProvider: React.FC = ({ children }) => {
  const [state, dispatch] = useReducer(mainReducer, initialState );

  return (
    <Store.Provider value={{ state, dispatch }}>{children}</Store.Provider>
  );
};

export { Store, StoreProvider };
