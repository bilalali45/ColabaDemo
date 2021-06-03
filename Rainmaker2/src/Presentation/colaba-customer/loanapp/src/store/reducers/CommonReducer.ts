import { ActionMap, Actions } from "./reducers";

export enum CommonType {
  SetBorrowerConsent = 'SET_BORROWERCONSENT_ERROR',
  SetIncomePopupTitle = "SET_INCOME_TITLE",
  SetAssetPopupTitle = "SET_ASSET_TITLE",
  
}

export type CommonActionsType = {
  borrowerconsenterror: boolean,
  incomePopupTitle: string,
  assetPopupTitle: string
};

export type CommonActionPayload = {
  [CommonType.SetBorrowerConsent]: boolean,
  [CommonType.SetIncomePopupTitle]: string
  [CommonType.SetAssetPopupTitle]: string
};

export type CommonActions = ActionMap<CommonActionPayload>[keyof ActionMap<CommonActionPayload>];

export const commonActionReducer = (
  state: CommonActionsType | {},
  { type, payload }: Actions
) => {
  switch (type) {
    case CommonType.SetBorrowerConsent:
      return {
        ...state,
        borrowerconsenterror: payload,
      };
    case CommonType.SetIncomePopupTitle:
      return {
        ...state,
        incomePopupTitle: payload,
      }
    case CommonType.SetAssetPopupTitle:
      return {
        ...state,
        assetPopupTitle: payload,
      }

    default:
      return state;
  }
};
