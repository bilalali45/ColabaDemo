import { Actions, ActionMap } from "./reducers";
import { ContactUs } from "../../entities/Models/ContactU";
import { LoanApplication } from "../../entities/Models/LoanApplication";
import { LoanProgress } from "../../entities/Models/LoanProgress";

type LOImage = {
    src: string
}

export enum LoanActionsType {
    FetchLoanInfo = 'FETCH_LOAN_INFO',
    FetchLoanOfficer = 'FETCH_LOAN_OFFICER',
    FetchLOImage = 'FETCH_LO_IMAGE',
    FetchLoanProgress = 'FETCH_LOAN_PROGRESS',
    SetIsMobile = 'SET_IS_MOBILE',
}

export type LoanType = {
    loanOfficer: ContactUs | null,
    loanInfo: LoanApplication | null,
    loImage: LOImage,
    loanProgress: LoanProgress[] | null
    isMobile: {
        value: Boolean
    },
}


export type LoanActionPayload = {
    [LoanActionsType.FetchLoanInfo]: LoanApplication,
    [LoanActionsType.FetchLoanOfficer]: ContactUs,
    [LoanActionsType.FetchLOImage]: LOImage
    [LoanActionsType.FetchLoanProgress]: LoanProgress[],
    [LoanActionsType.SetIsMobile]: { value: Boolean },
}

export type LoanActions = ActionMap<LoanActionPayload>[keyof ActionMap<LoanActionPayload>];

export const loanReducer = (state: LoanType | {}, { type, payload }: Actions) => {
    switch (type) {
        case LoanActionsType.FetchLoanOfficer:
            return {
                ...state,
                loanOfficer: { ...payload }
            };

        case LoanActionsType.FetchLOImage:
            return {
                ...state,
                loImage: { ...payload }
            };

        case LoanActionsType.FetchLoanInfo:
            return {
                ...state,
                loanInfo: { ...payload }
            };
        case LoanActionsType.FetchLoanProgress:
            return {
                ...state,
                loanProgress: payload
            }
        case LoanActionsType.SetIsMobile:
            return {
                ...state,
                isMobile: {...payload}
            }

        default:
            return state;
    }
}