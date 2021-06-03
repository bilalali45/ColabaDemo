import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { PropertyMortgageFirstStepDetails } from "./PropertyMortgageFirstStepDetails";
import { MockEnvConfig } from "../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../../store/store";
import MyPropertyActions from "../../../../../store/actions/MyPropertyActions";
import { APIResponse } from "../../../../../Entities/Models/APIResponse";
import MockMyPropertyActions from "../../../../../store/actions/__mocks__/MyPropertyActions";


// Loading manual mocks for modules
jest.mock("../../../../../store/actions/MyPropertyActions");
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
        myPropertyInfo: {
            primaryPropertyTypeId: 2
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

const totalNumbersOfUsages = 2;
let title = "My Properties";
let animatedText = "Animated Text";
let propertyId = 2;
let address = "12300 Bermuda Rd, Henderson, Nevada, 89044"

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {

    var { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
            <PropertyMortgageFirstStepDetails title={title} animatedText={animatedText} propertyId={propertyId} address={address} />
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


describe("My Properties > Property Mortgage First Step Details", () => {

    it("Should Render with property id", async () => {

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            const header = getByTestId("head");
            expect(header).toHaveTextContent(title);

            let tooltip = getByTestId("tooltip")
            expect(tooltip).toHaveTextContent(animatedText);
        });
    });

    it("Should Render without property id", async () => {
        propertyId = null;
        var { getByTestId, getAllByTestId } = await initiateComponent();
    });

    it("Should Render with api issue", async () => {
        propertyId = 1;
        MyPropertyActions.getPropertyValue = jest.fn((loanApplicationId: number, borrowerPropertyId: number) => {
            return null;
        });
        var { getByTestId, getAllByTestId } = await initiateComponent();
    });

    it("Should Render with is selling on", async () => {
        propertyId = 1;
        MyPropertyActions.getPropertyValue = jest.fn((loanApplicationId: number, borrowerPropertyId: number) => {
            return new APIResponse(200, { isSelling: true });
        });
        var { getByTestId, getAllByTestId } = await initiateComponent();
    });

    it("Should Render with is selling off", async () => {
        propertyId = 1;
        MyPropertyActions.getPropertyValue = jest.fn((loanApplicationId: number, borrowerPropertyId: number) => {
            var d = MockMyPropertyActions.PropertyValData;
            d.isSelling = false;
            return new APIResponse(200, d);
        });
        var { getByTestId, getAllByTestId } = await initiateComponent();
    });

    it("Should Save", async () => {
        propertyId = 1;
        var { getByTestId, getAllByTestId } = await initiateComponent();
        // CommonTestMethods.RenderButtonandClick(getByTestId, "first_mortgage_save");
        const input = getByTestId("first_mortgage_save");
        await waitFor(() => {
            act(() => {
                fireEvent.click(input);
            })
        });
    });
});