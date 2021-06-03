import {BusinessAddressProto, CurrentBusinessDetails} from "../../Entities/Models/Business";
import {ActionMap, Actions} from "./reducers";

export enum BusinessActionTypes {
    SetCurrentBusinessDetails = 'SET_BUSINESS_INFO',
    SetCurrentBusinessAddress = 'SET_BUSINESS_Address'
}

export type BusinessType = {
    businessInfo: CurrentBusinessDetails,
    businessAddress: BusinessAddressProto
};

export type BusinessActionPayload = {
    [BusinessActionTypes.SetCurrentBusinessDetails]: CurrentBusinessDetails,
    [BusinessActionTypes.SetCurrentBusinessAddress]: BusinessAddressProto
};

export type BusinessActions = ActionMap<BusinessActionPayload>[keyof ActionMap<BusinessActionPayload>];

export const BusinessActionReducer = (
    state: BusinessType | {},
    {type, payload}: Actions
) => {
    switch (type) {
        case BusinessActionTypes.SetCurrentBusinessDetails:
            return {
                ...state,
                businessInfo: payload,
            };
        case BusinessActionTypes.SetCurrentBusinessAddress:
            return {
                ...state,
                businessAddress: payload,
            };

        default:
            return state;
    }
};
  