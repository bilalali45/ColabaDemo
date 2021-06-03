import {
    AdditionalPropertyInfo,
    AssetInfo,
    Country,
    HomeOwnershipTypes, IncomeInfo,
    LoanInfoType,
    MyPropertyInfo,
    PrimaryBorrowerInfo,
    State,
} from "../../Entities/Models/types";
import { ActionMap, Actions } from "./reducers";

export enum LoanApplicationActionsType {
    SetLoanInfo = "SET_LOAN_INFO",
    SetStates = "SET_STATES",
    SetCountries = "SET_COUNTRIES",
    SetHomeOwnershiptypes = "SET_HOME_OWNERSHIP_TYPES",
    SetPrimaryBorrowerInfo = "SET_PRIMARY_BORROWER_INFO",
    SetIncomeInfo = "SET_INCOME_INFO",
    SetMyPropertyInfo = "SET_MY_PROPERTY_INFO",
    SetAssetInfo = "SET_ASSET_INFO",
    SetAdditionalPropertyInfo = "SET_ADDITIONAL_PROPERTY_INFO"
}

export type LoanApplicationsType = {
    loanInfo: LoanInfoType;
    states: State[];
    countries: Country[];
    homeOwnershipTypes: HomeOwnershipTypes;
    primaryBorrowerInfo: PrimaryBorrowerInfo;
    incomeInfo: IncomeInfo;
    myPropertyInfo: MyPropertyInfo;
    assetInfo: AssetInfo,
    additionalPropertyInfo: AdditionalPropertyInfo
};

export type LoanApplicationPayload = {
    [LoanApplicationActionsType.SetLoanInfo]: LoanInfoType;
    [LoanApplicationActionsType.SetStates]: State;
    [LoanApplicationActionsType.SetCountries]: Country;
    [LoanApplicationActionsType.SetHomeOwnershiptypes]: HomeOwnershipTypes;
    [LoanApplicationActionsType.SetPrimaryBorrowerInfo]: PrimaryBorrowerInfo;
    [LoanApplicationActionsType.SetIncomeInfo]: IncomeInfo;
    [LoanApplicationActionsType.SetMyPropertyInfo]: MyPropertyInfo;
    [LoanApplicationActionsType.SetAssetInfo]: AssetInfo
    [LoanApplicationActionsType.SetAdditionalPropertyInfo]: AdditionalPropertyInfo
};

export type LoanApplicationActions = ActionMap<LoanApplicationPayload>[keyof ActionMap<LoanApplicationPayload>];

export const loanApplicationReducer = (
    state: LoanApplicationsType | {},
    { type, payload }: Actions
) => {
    switch (type) {
        case LoanApplicationActionsType.SetLoanInfo:
            return {
                ...state,
                loanInfo: payload,
            };

        case LoanApplicationActionsType.SetStates:
            return {
                ...state,
                states: payload,
            };
        case LoanApplicationActionsType.SetCountries:
            return {
                ...state,
                countries: payload,
            };
        case LoanApplicationActionsType.SetHomeOwnershiptypes:
            return {
                ...state,
                homeOwnershipTypes: payload,
            };

        case LoanApplicationActionsType.SetPrimaryBorrowerInfo:
            return {
                ...state,
                primaryBorrowerInfo: payload,
            };

        case LoanApplicationActionsType.SetIncomeInfo:
            return {
                ...state,
                incomeInfo: payload,
            };
        case LoanApplicationActionsType.SetAssetInfo:
            return {
                ...state,
                assetInfo: payload,
            };
        case LoanApplicationActionsType.SetMyPropertyInfo:
            return {
                ...state,
                myPropertyInfo: payload
            }
        case LoanApplicationActionsType.SetAdditionalPropertyInfo:
            return {
                ...state,
                additionalPropertyInfo: payload
            }
        default:
            return state;
    }
};
