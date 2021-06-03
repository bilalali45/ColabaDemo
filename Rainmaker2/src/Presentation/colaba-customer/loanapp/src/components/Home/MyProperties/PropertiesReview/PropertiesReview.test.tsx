import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { PropertiesReview } from "./PropertiesReview";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../store/store";
import { LocalDB } from '../../../../lib/LocalDB';

// Loading manual mocks for modules
jest.mock("../../../../store/actions/MyPropertyActions");
jest.mock("../../../../lib/LocalDB")


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


// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {

    var { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
            <PropertiesReview />
        </Store.Provider>
    );

    await waitFor(async () => {
        const container = getByTestId("propertyReviewList");
        expect(container).toBeInTheDocument();
    });

    return { getByTestId, getAllByTestId };
};

beforeEach(() => {
    MockEnvConfig();
    MockSessionStorage();
    NavigationHandler.getNavigationStateAsString = jest.fn(() => console.log("{MockNavigationString: true}"));
    NavigationHandler.navigateToPath = jest.fn((path) => { console.log("Navigated to path: " + path) });
    NavigationHandler.moveNext = jest.fn(() => { console.log("Moved Next") });
});



describe("My Properties > Review and Continue ", () => {

    it("Should render properties for review", async () => {
        LocalDB.setLoanAppliationId("9823");

        var { getAllByTestId } = await initiateComponent();

        await waitFor(async () => {

            const reviewCards = getAllByTestId("propertyViewCard");

            expect(reviewCards.length > 0).toBe(true);

            expect(reviewCards.length).toBe(2);
        });
    });

    it("Should Edit Properties", async () => {
        LocalDB.setLoanAppliationId("9823");

        var { getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            const editButons = getAllByTestId("btn-edit");

            expect(editButons.length > 0).toBe(true);

            expect(editButons.length).toBe(2);

            editButons.forEach(btn => {
                act(() => {
                    fireEvent.click(btn);
                })
            });
        });
    });


    it("Should Save and Continue", async () => {
        LocalDB.setLoanAppliationId("9823");

        var { getByTestId } = await initiateComponent();

        await waitFor(async () => {

            act(() => {
                fireEvent.click(getByTestId("save-btn"));
            })
        });
    });
});