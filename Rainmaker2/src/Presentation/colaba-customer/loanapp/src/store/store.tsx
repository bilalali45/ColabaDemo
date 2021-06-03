import React, { createContext, useReducer } from "react";
import { mainReducer } from "./reducers/reducers";
import { LeftMenuType } from "./reducers/leftMenuReducer";
import { LoanApplicationsType } from "./reducers/LoanApplicationReducer";
import { ApplicationEnv } from "../lib/appEnv";
import { ErrorType } from "./reducers/ErrorReducer";
import { LoanNagivator } from "../Utilities/Navigation/LoanNavigator";
import { CurrentEmploymentDetails } from "../Entities/Models/Employment";
import { CurrentBusinessDetails } from "../Entities/Models/Business";
import { MilitaryIncomeType } from "./reducers/MilitaryIncomeReducer";
import { OtherIncomeType } from "./reducers/OtherIncomeReducer";
import { EmploymentHistoryDetails } from "../Entities/Models/EmploymentHistory";
import {TransactionProceeds} from "../Entities/Models/types";
// import { Http } from 'rainsoft-js';



if (process.env.NODE_ENV !== 'test') {
  import('rainsoft-js').then(rainsoftJs => {
    let baseUrl: any = window?.envConfig?.API_BASE_URL;
    let { Http } = rainsoftJs;
    process.env.NODE_ENV === 'development' ? new Http(baseUrl, "Rainmaker2Token", ApplicationEnv.ColabaWebUrl) : new Http(baseUrl, "Rainmaker2Token", location.href);
  });
}

// let baseUrl: any = window?.envConfig?.API_BASE_URL;
// process.env.NODE_ENV === 'development' ? new Http(baseUrl, "Rainmaker2Token", ApplicationEnv.ColabaWebUrl) : new Http(baseUrl, "Rainmaker2Token", location.href);
export type InitialStateType = {
  leftMenu: LeftMenuType | {
    leftMenuItems: any[],
    notAllowedItems: any[],
    navigation: LoanNagivator,
  },
  error: ErrorType | {},
  loanManager: LoanApplicationsType | {},
  commonManager: any,
  employment:CurrentEmploymentDetails |{}
  business:CurrentBusinessDetails |{}
  employmentHistory:EmploymentHistoryDetails |{}
  militaryIncomeManager: MilitaryIncomeType | {},
  otherIncomeManager: OtherIncomeType | {}
  assetsManager: TransactionProceeds | {}
};

export const initialState = {
  leftMenu: {
    navigation: null,
    leftMenuItems: [],
    notAllowedItems: []

  },
  error: {},
  loanManager: {},
  commonManager: {},
  employment:{},
  business: {},
  employmentHistory: {},
  militaryIncomeManager: {},
  otherIncomeManager: {},
  assetsManager: {}
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
