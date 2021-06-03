import React, { createContext, useReducer, ReactFragment } from "react";


import { mainReducer } from "../reducers/reducers";
import { InitialStateType } from "../store";
// import { Http } from 'rainsoft-js';


// let baseUrl: any = window?.envConfig?.API_BASE_URL;
// process.env.NODE_ENV === 'development' ? new Http(baseUrl, "Rainmaker2Token", ApplicationEnv.ColabaWebUrl) : new Http(baseUrl, "Rainmaker2Token", location.href);

export const initialState = {
    leftMenu: {
        navigation: null,
        leftMenuItems: [],
        notAllowedItems: []

    },
    error: {},
    loanManager: {
        loanInfo: {
            loanApplicationId: 6357,
            loanPurposeId: 1,
            loanGoalId: 4,
            borrowerId: 6615,
            ownTypeId: 1,
            borrowerName: "Qumber"
        },
        primaryBorrowerInfo: {
            id: 6615,
            firstName: "Qumber",
            lastName: "Kazmi",
            middleName: "",
            suffix: "",
            email: "qumber@gmail.com",
            homePhone: "",
            workPhone: "",
            workExt: "",
            cellPhone: "2142259077",
            ownTypeId: 1,
            name: "Qumber"
        },
        assetInfo: {
            borrowerName: "Qumber Kazmi",
            borrowerAssetId: 2272,
            assetCategoryId: 6,
            assetTypeId: 12,
            displayName: "Proceeds from Transactions"
        },
        homeOwnershipTypes: [
            "{id: 1, name: \"Own\"}",
            "{id: 2, name: \"Rent\"}",
            "{id: 3, name: \"Other\"}"
        ],
        countries: [],
        states: []
    },
    commonManager: {},
    employment: {},
    business: {},
    employmentHistory: {},
    militaryIncomeManager: {},
    otherIncomeManager: {},
    assetsManager: {}
};

const Store = createContext<{
    state: InitialStateType;
    dispatch: React.Dispatch<any>;
}>({
    state: initialState,
    dispatch: () => null,
});

const StoreProvider: React.FC = ({ children }) => {
    const [state, dispatch] = useReducer(mainReducer, initialState);

    return (
        <Store.Provider value={{ state, dispatch }}>{children}</Store.Provider>
    );
};

export { Store, StoreProvider };
