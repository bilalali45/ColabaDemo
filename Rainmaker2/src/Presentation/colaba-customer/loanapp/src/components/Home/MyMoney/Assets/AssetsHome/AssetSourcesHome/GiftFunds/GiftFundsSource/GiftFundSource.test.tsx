import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
    screen
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { GiftFundSource } from "./GiftFundSource";
import { MockEnvConfig } from "../../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../../../../../store/store";

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

const totalNumberOfSources = 9;

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {

    var { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
            <GiftFundSource />
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


describe("Gift Fund Sources", () => {

    it("Should render basic information correctly and select first source", async () => {        

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            const title = getByTestId("title");
            expect(title).toHaveTextContent("Where Is This Gift From?");

            const sources = getAllByTestId('list-item');
            expect(sources).toHaveLength(totalNumberOfSources);

            act(() => {
                fireEvent.click(sources[0]);
            })
        });
    });
});