import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
    screen
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { AssetsHome } from "./AssetsHome";
import { MockEnvConfig } from "../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";
import { BrowserRouter as Router } from 'react-router-dom';
import { LocalDB } from "../../../../../lib/LocalDB";
import { Store } from "../../../../../store/store";
import AssetsActions from "../../../../../store/actions/AssetsActions";
import { APIResponse } from "../../../../../Entities/Models/APIResponse";


// Loading manual mocks for modules
jest.mock("../../../../../store/actions/BusinessActions");
jest.mock("../../../../../store/actions/TransactionProceedsActions");
jest.mock("../../../../../store/actions/OtherAssetsActions");
jest.mock("../../../../../store/actions/GiftAssetActions");
jest.mock("../../../../../store/actions/IncomeActions");
jest.mock("../../../../../store/actions/AssetsActions");
jest.mock("../../../../../lib/LocalDB")


// Defining state for component
const state = {
    leftMenu: {
        navigation: null,
        leftMenuItems: [],
        notAllowedItems: [],
    },
    error: {},
    loanManager: {
        loanInfo: {
            loanApplicationId: 9823,
            loanPurposeId: null,
            loanGoalId: null,
            borrowerId: 10790,
            ownTypeId: 2,
            borrowerName: "Carter",
        },
        primaryBorrowerInfo: {
            id: 10790,
            name: "Lisa",
        },
    },
    commonManager: {},
    employment: {},
    business: {},
    employmentHistory: {},
    militaryIncomeManager: {},
    otherIncomeManager: {},
    assetsManager: {}
};

const dispatch = jest.fn();

const totalNumbersOfBorrowers = 2;

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {

    var { getByTestId, getAllByTestId } = render(
        <Router>
            <Store.Provider value={{ state, dispatch }}>
                <AssetsHome />
            </Store.Provider>
        </Router>

    );

    return { getByTestId, getAllByTestId };
};

const shouldEditAssetByCategory = async (categoryId: number) => {
    mockData(categoryId);

    var { getByTestId, getAllByTestId } = await initiateComponent();

    await waitFor(async () => {
        let listCards = getAllByTestId('assetsHomeListCard');
        expect(listCards).toHaveLength(totalNumbersOfBorrowers);
    });

    await waitFor(async () => {
        const dropdown = getByTestId('dropdown');
        const display1 = dropdown.children[0];
        fireEvent.click(display1);
        const editAsset = getByTestId('edit-asset');
        fireEvent.click(editAsset);
    });
};

const mockData = (categoryId) => {
    let d = { ...assetData };
    d.borrowers[0].borrowerAssets[0].assetCategoryId = categoryId;
    AssetsActions.GetMyMoneyHomeScreen = jest.fn((loanApplicationId: number) => {
        console.log("!!! RUNNING MOCK " + d.borrowers[0].borrowerAssets[0].assetCategoryId);
        return new APIResponse(200, d)
    });
};

const assetData = {
    borrowers: [
        {
            "borrowerId": 14897,
            "borrowerName": "Quemby Kelly",
            "ownTypeId": 1,
            "ownTypeName": "Primary Contact",
            "ownTypeDisplayName": "Primary Contact",
            "borrowerAssets": [
                {
                    "assetName": "Eum magna dolorem at",
                    "assetValue": 123,
                    "assetId": 2342,
                    "assetTypeID": 2,
                    "assetCategoryId": 1,
                    "assetCategoryName": "Bank Account",
                    "assetTypeName": "Savings Accoount"
                }
            ],
            "assetsValue": 123
        },
        {
            "borrowerId": 14899,
            "borrowerName": "Brianna Chan",
            "ownTypeId": 2,
            "ownTypeName": "Secondary Contact",
            "ownTypeDisplayName": "Secondary Contact",
            "borrowerAssets": [],
            "assetsValue": 0
        }
    ],
    totalAssetsValue: 0
}




// Used to render component by loading data. Call on each it/test
beforeEach(async () => {
    MockEnvConfig();
    MockSessionStorage();
    NavigationHandler.getNavigationStateAsString = jest.fn(() => "{MockNavigationString: true}");
    NavigationHandler.navigateToPath = jest.fn((path) => { console.log("Navigated to path: " + path) });
    NavigationHandler.moveNext = jest.fn(() => { console.log("Moved Next") });

});


describe("Assets Home ", () => {

    it("Should render basic information correctly", async () => {
        LocalDB.setLoanAppliationId("9823");

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            let listCards = getAllByTestId('assetsHomeListCard');
            expect(listCards).toHaveLength(totalNumbersOfBorrowers);
        });

        await waitFor(async () => {
            const header = getByTestId("head");
            expect(header).toHaveTextContent("Assets");

            let tooltip = getByTestId("tooltip")
            expect(tooltip).toHaveTextContent("please tell us about your qualifying income");

            let totalAssetsValue = getByTestId("totalAssetsValue")
            expect(totalAssetsValue).toHaveTextContent("0.00");

            let addAssetBtns = getAllByTestId('add-asset');
            expect(addAssetBtns.length).not.toBe(0);
        });
    });

    it("Should render Add Asset", async () => {
        LocalDB.setLoanAppliationId("9823");

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            let listCards = getAllByTestId('assetsHomeListCard');
            expect(listCards).toHaveLength(totalNumbersOfBorrowers);
        });

        await waitFor(async () => {
            act(() => {
                fireEvent.click(getAllByTestId('add-asset')[0]);
            })

            const dropdown = getByTestId('dropdown');

            const display1 = dropdown.children[0];
            fireEvent.click(display1);
            const editAsset = getByTestId('edit-asset');
            fireEvent.click(editAsset);
        });
    });

    //for (var i = 0; i < 8; i++) {
    it("Should render edit by category DetailsOfBankAccount", async () => {
        LocalDB.setLoanAppliationId("9823");
        await shouldEditAssetByCategory(1);
    });

    it("Should render edit by category FinancialAssetsDetail", async () => {
        LocalDB.setLoanAppliationId("9823");
        await shouldEditAssetByCategory(2);
    });

    it("Should render edit by category RetirementAccountDetails", async () => {
        LocalDB.setLoanAppliationId("9823");
        await shouldEditAssetByCategory(3);
    });

    it("Should render edit by category GiftFundsDetails", async () => {
        LocalDB.setLoanAppliationId("9823");
        await shouldEditAssetByCategory(4);
    });

    it("Should render edit without category id", async () => {
        LocalDB.setLoanAppliationId("9823");
        await shouldEditAssetByCategory(null);
    });
    //}

    it("Should render delete asset", async () => {
        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            let listCards = getAllByTestId('assetsHomeListCard');
            expect(listCards).toHaveLength(totalNumbersOfBorrowers);
        });

        await waitFor(async () => {
            const dropdown = getByTestId('dropdown');
            const display1 = dropdown.children[0];
            fireEvent.click(display1);
            const editAsset = getByTestId('delete-asset');
            fireEvent.click(editAsset);
        });
    });
});