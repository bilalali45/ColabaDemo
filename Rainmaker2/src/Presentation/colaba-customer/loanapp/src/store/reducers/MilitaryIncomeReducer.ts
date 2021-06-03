import {MilitaryIncomeEmployer, ServiceLocationAddressObject, MilitaryPaymentMode} from "../../Entities/Models/types";
import { ActionMap, Actions } from "./reducers";

export enum MilitaryIncomeActionTypes {
 SetMilitaryEmployer = 'SET_MILITARY_EMPLOYER',
 SetMilitaryServiceAddress = "SET_MILITARY_SERVICE_ADDRESS",
 SetMilitaryPaymentMode = "SET_MILITARY_PAYMENT_MODE"
  }

  export type MilitaryIncomeType = {
    militaryEmployer: MilitaryIncomeEmployer,
    militaryServiceAddress: ServiceLocationAddressObject,
    militaryPaymentMode: MilitaryPaymentMode
  };

  export type MilitaryIncomeActionPayload = {
   [MilitaryIncomeActionTypes.SetMilitaryEmployer]: MilitaryIncomeEmployer,
   [MilitaryIncomeActionTypes.SetMilitaryServiceAddress]: ServiceLocationAddressObject,
   [MilitaryIncomeActionTypes.SetMilitaryPaymentMode]: MilitaryPaymentMode,
  };
  
  export type MilitaryIncomeActions = ActionMap<MilitaryIncomeActionPayload>[keyof ActionMap<MilitaryIncomeActionPayload>];

  export const MilitaryIncomeActionReducer = (
    state: MilitaryIncomeType | {},
    { type, payload }: Actions
  ) => {
    switch (type) {
     case MilitaryIncomeActionTypes.SetMilitaryEmployer:
        return {
            ...state,
            militaryEmployer: payload,
          };
          case MilitaryIncomeActionTypes.SetMilitaryServiceAddress:
        return {
            ...state,
            militaryServiceAddress: payload,
          };
          case MilitaryIncomeActionTypes.SetMilitaryPaymentMode:
        return {
            ...state,
            militaryPaymentMode: payload,
          };
  
      default:
        return state;
    }
  };
  