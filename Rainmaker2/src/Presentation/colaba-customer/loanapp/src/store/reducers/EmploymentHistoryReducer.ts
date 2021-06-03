import { EmployerOfficeAddress } from "../../Entities/Models/Employment";
import { BorrowerIncome, EmployerHistoryInfo, WayOfHistoryIncome } from "../../Entities/Models/EmploymentHistory";
import { IncomeInfo } from "../../Entities/Models/types";
import { ActionMap, Actions } from "./reducers";


export enum EmploymentHistoryActionTypes {
    SetPreviousEmployerInfo = 'SET_PREVIOUS_EMPLOYER_INFO',
    SetPreviousEmployerAddress = 'SET_PREVIOUS_EMPLOYER_ADDRESS',
    SetPreviousEmploymentIncome = 'SET_PREVIOUS_EMPLOYMENT_INCOME',
    SetBorrowersId = "SET_BORROWERS_ID",
    SetBorrowersIncome = "SET_BORROWERS_INCOME",
    SetUpdatedBorrowerIncome = "SET_UPDATED_BORROWER_INCOME",
    SetIncomeInformation = "SET_INCOME_INFORMATION"
}

export type EmploymentHistoryType = {
    previousEmployerInfo: EmployerHistoryInfo,
    previousEmployerAddress: EmployerOfficeAddress,
    previousEmploymentIncome: WayOfHistoryIncome,
    borrowersId: number[],
    borrowersIncome: BorrowerIncome[],
    incomeInformation: IncomeInfo
};

export type EmploymentHistoryActionPayload = {
    [EmploymentHistoryActionTypes.SetPreviousEmployerInfo]: EmployerHistoryInfo,
    [EmploymentHistoryActionTypes.SetPreviousEmployerAddress]: EmployerOfficeAddress,
    [EmploymentHistoryActionTypes.SetPreviousEmploymentIncome]: WayOfHistoryIncome,
    [EmploymentHistoryActionTypes.SetBorrowersId]: number[],
    [EmploymentHistoryActionTypes.SetBorrowersIncome]: BorrowerIncome[],
    [EmploymentHistoryActionTypes.SetUpdatedBorrowerIncome]: BorrowerIncome[],
    [EmploymentHistoryActionTypes.SetIncomeInformation]: IncomeInfo,
    
};

export type EmployerHistoryActions = ActionMap<EmploymentHistoryActionPayload>[keyof ActionMap<EmploymentHistoryActionPayload>];

export const EmployerHistoryActionReducer = (
    state: EmploymentHistoryType | {},
    { type, payload }: Actions
) => {
    switch (type) {
        case EmploymentHistoryActionTypes.SetPreviousEmployerInfo:
            return {
                ...state,
                previousEmployerInfo: payload,
            };
        case EmploymentHistoryActionTypes.SetPreviousEmployerAddress:
            return {
                ...state,
                previousEmployerAddress: payload,
            };
        case EmploymentHistoryActionTypes.SetPreviousEmploymentIncome:
            return {
                ...state,
                previousEmploymentIncome: payload,
            };
        case EmploymentHistoryActionTypes.SetBorrowersId:
            return {
                ...state,
                borrowersId: payload,
            };
        case EmploymentHistoryActionTypes.SetBorrowersIncome:
            return {
                ...state,
                borrowersIncome: payload,
            };
        case EmploymentHistoryActionTypes.SetUpdatedBorrowerIncome:
            let updatedBorrowerIncome = [...state['borrowersIncome'], payload]
            return {
                ...state,
                borrowersIncome: updatedBorrowerIncome,
            };
        case EmploymentHistoryActionTypes.SetIncomeInformation:
            return {
                ...state,
                incomeInformation: payload,
            };

        default:
            return state;
    }
};