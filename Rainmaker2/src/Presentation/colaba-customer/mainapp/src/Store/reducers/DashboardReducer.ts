import { LoanApplicationType } from "../../Entities/Models/LoanApplicationType";
import { ActionMap, Actions } from "./reducers";


export enum DashboardActionsTypes {
    GetLoanApplications = "GET_LOGGED_IN_USER_LOAN_APPLICATIONS",
    SetloaderStatus = "LOAN_APPLICATIONS_LOADER_STATUS"
}

export type LoggedInUserLoanApplicationsType = {
    loanApplications: LoanApplicationType[] | null,
    isLoaded: boolean
}

export type DashboardActionPayload = {
    [DashboardActionsTypes.GetLoanApplications]: LoanApplicationType[];
    [DashboardActionsTypes.SetloaderStatus]: boolean
}

export type DashboardActions = ActionMap<DashboardActionPayload>[keyof ActionMap<DashboardActionPayload>];

export const dashboardReducer = (state : LoggedInUserLoanApplicationsType | {} , {type, payload} : Actions) => {
    
    switch (type) {
        case DashboardActionsTypes.GetLoanApplications:
            return {
                ...state,
                loanApplications: payload
            }
        case DashboardActionsTypes.SetloaderStatus:
            return {
                ...state,
                isLoaded: payload
            }
        default:
            return state
    }
}