import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
    screen
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { AssetSourcesHome } from "./AssetSourcesHome";
import { MockEnvConfig } from "../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../../../Utilities/Navigation/NavigationHandler";
import { BrowserRouter as Router } from 'react-router-dom';
import { LocalDB } from "../../../../../../lib/LocalDB";
import { Store } from "../../../../../../store/store";


// Loading manual mocks for modules
jest.mock("../../../../../../store/actions/TransactionProceedsActions");
jest.mock("../../../../../../store/actions/OtherAssetsActions");
jest.mock("../../../../../../store/actions/GiftAssetActions");
jest.mock("../../../../../../store/actions/AssetsActions");
jest.mock("../../../../../../lib/LocalDB")


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
            ownTypeId: 1,
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

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {

    var { getByTestId, getAllByTestId } = render(
        <Router>
            <Store.Provider value={{ state, dispatch }}>
                <AssetSourcesHome />
            </Store.Provider>
        </Router>

    );

    return { getByTestId, getAllByTestId };
};

// Used to render component by loading data. Call on each it/test
beforeEach(async () => {
    MockEnvConfig();
    MockSessionStorage();
    NavigationHandler.getNavigationStateAsString = jest.fn(() => "{MockNavigationString: true}");
    NavigationHandler.navigateToPath = jest.fn((path) => { console.log("Navigated to path: " + path) });
    NavigationHandler.moveNext = jest.fn(() => { console.log("Moved Next") });

});


describe("Asset Home Sources", () => {

    it("Should render basic information correctly", async () => {
        LocalDB.setLoanAppliationId("9823");

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            const title = getByTestId("popup-title");
            expect(title).toHaveTextContent("What type of asset is this?");
        });
    });
});