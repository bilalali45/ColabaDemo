import { InitialStateType } from "../store";
import { LeftMenuActions, leftMenuReducer } from "./leftMenuReducer";
import { LoanApplicationActions, loanApplicationReducer } from "./LoanApplicationReducer";
import {CommonActions, commonActionReducer} from "./CommonReducer";
import { ErrorActions, errorReducer } from "./ErrorReducer";
import { EmploymentActionReducer, EmploymentIncomeActions } from "./EmploymentIncomeReducer";
import { BusinessActionReducer, BusinessActions } from "./BusinessIncomeReducer";
import { EmployerHistoryActionReducer, EmployerHistoryActions } from "./EmploymentHistoryReducer";
import {MilitaryIncomeActions, MilitaryIncomeActionReducer} from "./MilitaryIncomeReducer";
import {OtherIncomeActions, OtherIncomeActionReducer} from "./OtherIncomeReducer";
import {AssetsActionReducer,AssetsActions} from "./AssetsActionReducer";

export type ActionMap<M extends { [index: string]: any }> = {
    [Key in keyof M]: M[Key] extends undefined
    ? {
        type: Key;
    }
    : {
        type: Key;
        payload: M[Key];
    }
};

export type Actions = LeftMenuActions | LoanApplicationActions | CommonActions | OtherIncomeActions | ErrorActions | EmploymentIncomeActions | BusinessActions | EmployerHistoryActions | MilitaryIncomeActions | AssetsActions

export const mainReducer = ({leftMenu, loanManager, commonManager, error, employment, business, employmentHistory, militaryIncomeManager, otherIncomeManager, assetsManager} : InitialStateType , action: Actions) => ({
    leftMenu: leftMenuReducer(leftMenu, action),
    loanManager: loanApplicationReducer(loanManager, action),
    commonManager: commonActionReducer(commonManager, action),
    employment: EmploymentActionReducer(employment, action),
    business: BusinessActionReducer(business, action),
    error: errorReducer(error, action),
    employmentHistory:EmployerHistoryActionReducer(employmentHistory, action),
    militaryIncomeManager: MilitaryIncomeActionReducer(militaryIncomeManager, action),
    otherIncomeManager: OtherIncomeActionReducer(otherIncomeManager, action),
    assetsManager: AssetsActionReducer(assetsManager, action)
});

