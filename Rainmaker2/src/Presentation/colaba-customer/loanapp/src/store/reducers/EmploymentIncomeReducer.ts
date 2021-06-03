import { EmployerOfficeAddress, EmployerInfo, WayOfIncome, EmploymentOtherIncomes } from "../../Entities/Models/Employment";
import { ActionMap, Actions } from "./reducers";

export enum EmploymentIncomeActionTypes {
  SetEmployerInfo = 'SET_EMPLOYER_INFO',
  SetEmployerAddress = 'SET_EMPLOYER_ADDRESS',
  SetWayOfIncome = 'SET_WAY_OF_INCOME',
  SetAdditionIncome = 'SET_ADDITIONAL_INCOME'
}

export type EmploymentIncomeType = {
  employerInfo: EmployerInfo,
  employerAddress: EmployerOfficeAddress,
  wayOfIncome: WayOfIncome,
  additionalIncome: EmploymentOtherIncomes[]
};

export type EmploymentIncomeActionPayload = {
  [EmploymentIncomeActionTypes.SetEmployerInfo]: EmployerInfo,
  [EmploymentIncomeActionTypes.SetEmployerAddress]: EmployerOfficeAddress,
  [EmploymentIncomeActionTypes.SetWayOfIncome]: WayOfIncome,
  [EmploymentIncomeActionTypes.SetAdditionIncome]: EmploymentOtherIncomes,
};

export type EmploymentIncomeActions = ActionMap<EmploymentIncomeActionPayload>[keyof ActionMap<EmploymentIncomeActionPayload>];

export const EmploymentActionReducer = (
  state: EmploymentIncomeType | {},
  { type, payload }: Actions
) => {
  switch (type) {
    case EmploymentIncomeActionTypes.SetEmployerInfo:
      return {
        ...state,
        employerInfo: payload,
      };
    case EmploymentIncomeActionTypes.SetEmployerAddress:
      return {
        ...state,
        employerAddress: payload,
      };
    case EmploymentIncomeActionTypes.SetWayOfIncome:
      return {
        ...state,
        wayOfIncome: payload,
      };
    case EmploymentIncomeActionTypes.SetAdditionIncome:
      return {
        ...state,
        additionalIncome: payload,
      };


    default:
      return state;
  }
};
