import { Actions, ActionMap } from "./reducers";
import { ContactUs } from "../../entities/Models/ContactU";
import { LoanApplication } from "../../entities/Models/LoanApplication";

type LOImage = {
    src: string
}

export enum LoanActionsType {
    FetchLoanInfo = 'FETCH_LOAN_INFO',
    FetchLoanOfficer = 'FETCH_LOAN_OFFICER',
    FetchLOImage = 'FETCH_LO_IMAGE'
}

export type LoanType = {
    loanOfficer: ContactUs | null,
    loanInfo: LoanApplication | null,
    loImage: LOImage
}


export type LoanActionPayload = {
    [LoanActionsType.FetchLoanInfo]: LoanApplication,
    [LoanActionsType.FetchLoanOfficer]: ContactUs,
    [LoanActionsType.FetchLOImage]: LOImage
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

        default:
            return state;
    }
}