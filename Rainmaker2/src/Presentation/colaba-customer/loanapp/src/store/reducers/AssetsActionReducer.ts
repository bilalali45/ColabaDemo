import { ActionMap, Actions } from "./reducers";
import {
    TransactionProceedsFromLoanDTO,
    TransactionProceedsFromRealAndNonRealEstateDTO
} from "../../Entities/Models/TransactionProceeds";
import { AssetSourceType, FinancialAssets } from '../../Entities/Models/types';

export enum AssetsActionTypes {
    setTransactionProceedsFromRealAndNonRealEstate = 'SET_TRANSACTION_PROCEED_FROM_REAL_NON_REAT_ESTATE',
    setTransactionProceedsFromLoan = 'SET_TRANSACTION_PROCEED_FROM_LOAN',
    setAssetSourceList = 'SET_ASSET_SOURCE_LIST',
    setGiftFundSourceId = 'SET_GIFT_FUND_SOURCE_ID',
    setFinancialAssetItem = 'SET_FINANCIAL_ASSET_LIST_ITEM'
}

export type AssetsType = {
    setTransactionProceedsFromRealAndNonRealEstate: TransactionProceedsFromRealAndNonRealEstateDTO,
    setTransactionProceedsFromLoan: TransactionProceedsFromLoanDTO,
    assetSourceList: AssetSourceType,
    giftFundSourceId: number,
    financialAssetItem: FinancialAssets
};

export type AssetsActionPayload = {
    [AssetsActionTypes.setTransactionProceedsFromRealAndNonRealEstate]: TransactionProceedsFromRealAndNonRealEstateDTO,
    [AssetsActionTypes.setTransactionProceedsFromLoan]: TransactionProceedsFromLoanDTO,
    [AssetsActionTypes.setAssetSourceList]: AssetSourceType,
    [AssetsActionTypes.setGiftFundSourceId]: number,
    [AssetsActionTypes.setFinancialAssetItem]: FinancialAssets
};

export type AssetsActions = ActionMap<AssetsActionPayload>[keyof ActionMap<AssetsActionPayload>];

export const AssetsActionReducer = (
    state: AssetsType | {},
    { type, payload }: Actions
) => {
    switch (type) {
        case AssetsActionTypes.setTransactionProceedsFromRealAndNonRealEstate:
            return {
                ...state,
                setTransactionProceedsFromRealAndNonRealEstate: payload,
            };
        case AssetsActionTypes.setTransactionProceedsFromLoan:
            return {
                ...state,
                setTransactionProceedsFromLoan: payload,
            };
        case AssetsActionTypes.setAssetSourceList:
            return {
                ...state,
                assetSourceList: payload,
            };
        case AssetsActionTypes.setGiftFundSourceId:
            return {
                ...state,
                giftFundSourceId: payload,
            };
        case AssetsActionTypes.setFinancialAssetItem:
            return {
                ...state,
                financialAssetItem: payload,
            };

        default:
            return state;
    }
};
