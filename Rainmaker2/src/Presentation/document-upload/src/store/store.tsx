import React, { createContext, useReducer, ReactFragment } from "react";
import { authReducer } from "./reducers/aauthReducer";
import { userReducer } from "./reducers/userReducer";
import { mainReducer } from "./reducers/reducers";
import { Http } from "rainsoft-js";
import { ContactUs } from "../entities/Models/ContactU";
import { LoanType } from "./reducers/loanReducer";
import { DocumentsType } from "./reducers/documentReducer";
import { Auth } from "../services/auth/Auth";
import { MockEnvConfig } from "../services/test_helpers/EnvConfigMock";

let baseUrl: any = window?.envConfig?.API_BASE_URL;
const httpClient = new Http(baseUrl, "Rainmaker2Token");

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
