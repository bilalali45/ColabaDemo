import React, { createContext, useReducer, ReactFragment } from "react";
import { authReducer } from "./reducers/aauthReducer";
import { userReducer } from "./reducers/userReducer";
import { mainReducer } from "./reducers/reducers";
import { ContactUs } from "../entities/Models/ContactU";
import { LoanType } from "./reducers/loanReducer";
import { DocumentsType } from "./reducers/documentReducer";
import { Auth } from "../services/auth/Auth";
import { MockEnvConfig } from "../services/test_helpers/EnvConfigMock";
import { ApplicationEnv } from "../utils/helpers/AppEnv";

let baseUrl: any = window?.envConfig?.API_BASE_URL;
if (process.env.NODE_ENV !== 'test') {
  import('rainsoft-js').then(rainsoftJs => {
    let baseUrl: any = window?.envConfig?.API_BASE_URL;
    let { Http } = rainsoftJs;
    process.env.NODE_ENV === 'development' ? new Http(baseUrl, "Rainmaker2Token", ApplicationEnv.ColabaWebUrl) : new Http(baseUrl, "Rainmaker2Token", location.href);

  });
}

export type InitialStateType = {
  loan: LoanType | {};
  documents: DocumentsType | {};
};

export const initialState = {
  loan: {},
  documents: {},
};

const Store = createContext<{
  state: InitialStateType;
  dispatch: React.Dispatch<any>;
}>({
  state: initialState,
  dispatch: () => null,
});

const StoreProvider: React.FC = ({ children }) => {
  const [state, dispatch] = useReducer(mainReducer, initialState);

  return (
    <Store.Provider value={{ state, dispatch }}>{children}</Store.Provider>
  );
};

export { Store, StoreProvider };
