import { OtherIncomeListType, SelectedOtherIncomeType} from "../../Entities/Models/types";
import { ActionMap, Actions } from "./reducers";

export enum OtherIncomeActionTypes {
   SetSelectedOtherIncomeType = "SET_SELECTED_OTHER_INCOME_TYPE",
   SetOtherIncomeTypeList = "SET_OTHER_INCOME_TYPE_LIST"
  }

  export type OtherIncomeType = {
    selectedOtherIncome : SelectedOtherIncomeType,
    otherIncomeTypeList: OtherIncomeListType
  };

  export type OtherIncomeActionPayload = {
   [OtherIncomeActionTypes.SetSelectedOtherIncomeType]: SelectedOtherIncomeType,
   [OtherIncomeActionTypes.SetOtherIncomeTypeList]: OtherIncomeListType
  };
  
  export type OtherIncomeActions = ActionMap<OtherIncomeActionPayload>[keyof ActionMap<OtherIncomeActionPayload>];

  export const OtherIncomeActionReducer = (
    state: OtherIncomeType | {},
    { type, payload }: Actions
  ) => {
    switch (type) {
          case OtherIncomeActionTypes.SetSelectedOtherIncomeType:
        return {
            ...state,
            selectedOtherIncome: payload,
          };

          case OtherIncomeActionTypes.SetOtherIncomeTypeList:
            return {
                ...state,
                otherIncomeTypeList: payload,
              };
  
      default:
        return state;
    }
  };
  