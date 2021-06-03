import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
    screen
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { GiftFundsDetails } from "./GiftFundsDetails";
import { MockEnvConfig } from "../../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../../../../../store/store";
import GiftAssetActions, { GiftAssetActionMockDataCollection } from "../../../../../../../../store/actions/__mocks__/GiftAssetActions";

// Loading manual mocks for modules
jest.mock("../../../../../../../../store/actions/GiftAssetActions");

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
        assetInfo: {
            borrowerAssetId: 1
        }
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

const totalNumberOfSources = 9;

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {

    var { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
            <GiftFundsDetails />
        </Store.Provider>

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


describe("Gift Funds Details", () => {

    it("Should render basic information correctly", async () => {

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            const title = getByTestId("title");
            expect(title).toHaveTextContent("What type of gift was this?");

            const cashGift = getAllByTestId('cash-gift')[0];;
            const grant = getAllByTestId('grant')[0];
        });
    });

    it("Should select and render Cash Gift", async () => {

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {

            const cashGift = getAllByTestId('cash-gift')[0];

            fireEvent.click(cashGift);

            var cashLabel = getByTestId('value')

            expect(cashLabel).toBeInTheDocument();
            expect(cashLabel).toHaveTextContent('Cash Value')

            expect(getByTestId('cash-hasdeposited')).toHaveTextContent('Has this gift been deposited already?');
        });

        await waitFor(async () => {
            var input = getByTestId('value-input')
            fireEvent.change(input, { target: { value: "400" } });
        });

        await waitFor(() => {
            var input = getByTestId('value-input')
            expect(input).toHaveDisplayValue(["400"]);
        });

        await waitFor(async () => {
            var input = getByTestId('value-input')
            fireEvent.change(input, { target: { value: "asd" } });
        });

        await waitFor(() => {
            var input = getByTestId('value-input')
            expect(input).not.toHaveDisplayValue(["asd"]);
        });

        await waitFor(async () => {
            var input = getByTestId('value-input')
            fireEvent.change(input, { target: { value: "0" } });
        });

        await waitFor(() => {
            var input = getByTestId('value-input')
            expect(input).not.toHaveDisplayValue(["0"]);
        });
        

        await waitFor(() => {
            const isDeposited = getAllByTestId('isDeposited')[0];
            act(() => {
                fireEvent.click(isDeposited);
            });
        });

        await waitFor(() => {
            var dtp = getByTestId('valueDate');
            act(() => {
                fireEvent.change(dtp, {
                    target: { value: "05/03/2021" }
                });
            });
        });


    });


    it("Should select and render Grant", async () => {

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {

            const cashGift = getAllByTestId('grant')[0];

            fireEvent.click(cashGift);

            var cashLabel = getByTestId('value')

            expect(cashLabel).toBeInTheDocument();
            expect(cashLabel).toHaveTextContent('Market Value')

        });

        await waitFor(async () => {
            var input = getByTestId('value-input')
            fireEvent.change(input, { target: { value: "400" } });
        });

        await waitFor(() => {
            var input = getByTestId('value-input')
            expect(input).toHaveDisplayValue(["400"]);
        });
    });

    it("Should render gift details on edit and update", async () => {
        var data = { ...GiftAssetActionMockDataCollection.GiftAssetData };

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            const title = getByTestId("title");
            expect(title).toHaveTextContent("What type of gift was this?");
        });

        await waitFor(async () => {
            const cashGift = getAllByTestId('cash-gift')[0];;
            const grant = getAllByTestId('grant')[0];

            var saveBtn = getByTestId("saveBtn")
            expect(saveBtn).not.toBeDisabled();

            fireEvent.click(saveBtn);
        })
    });
});