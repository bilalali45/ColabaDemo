import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
} from "@testing-library/react";

import AdditionalPropertyAddress from "./AdditionalPropertyAddress";
import { InitialStateType, Store } from "../../../../store/store";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { LocalDB } from "../../../../lib/LocalDB";
import { LoanApplicationsType } from "../../../../store/reducers/LoanApplicationReducer";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import CommonTestData from '../../../../test_utilities/CommonTestData'

// Loading manual mocks for modules
jest.mock("../../../../store/actions/GettingToKnowYouActions");
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
        myPropertyInfo: {
            primaryPropertyTypeId: 2
        }
    },
    states: CommonTestData.StatesCollection,
    countries: CommonTestData.CountriesCollection,
    commonManager: {},
    employment: {},
    business: {},
    employmentHistory: {},
    militaryIncomeManager: {},
    otherIncomeManager: {},
    assetsManager: {}
};

const dispatch = jest.fn();

// const totalNumbersOfUsages = 2;
// let title = "My Properties";
// let animatedText = "Animated Text";
// let propertyId = 2;
// let address = "12300 Bermuda Rd, Henderson, Nevada, 89044"

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {
    var { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
            <AdditionalPropertyAddress setAddress={() => {
                console.log("!! Set Address Invoked")
            }} />
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

const setupGoogleMock = () => {
    /*** Mock Google Maps JavaScript API ***/
    const google = {
        maps: {
            places: {
                AutocompleteService: () => { },
                PlacesServiceStatus: {
                    INVALID_REQUEST: "INVALID_REQUEST",
                    NOT_FOUND: "NOT_FOUND",
                    OK: "OK",
                    OVER_QUERY_LIMIT: "OVER_QUERY_LIMIT",
                    REQUEST_DENIED: "REQUEST_DENIED",
                    UNKNOWN_ERROR: "UNKNOWN_ERROR",
                    ZERO_RESULTS: "ZERO_RESULTS",
                },
            },
            Geocoder: () => { },
            GeocoderStatus: {
                ERROR: "ERROR",
                INVALID_REQUEST: "INVALID_REQUEST",
                OK: "OK",
                OVER_QUERY_LIMIT: "OVER_QUERY_LIMIT",
                REQUEST_DENIED: "REQUEST_DENIED",
                UNKNOWN_ERROR: "UNKNOWN_ERROR",
                ZERO_RESULTS: "ZERO_RESULTS",
            },
        },
    };
    // window.google = google;
};

describe("My Properties > Additional Property Address", () => {
    // LocalDB.setLoanAppliationId("1");

    it("Should Render", async () => {

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            const header = getByTestId("head");
            expect(header).toHaveTextContent("My Properties");

            let tooltip = getByTestId("tooltip")
            expect(tooltip).toHaveTextContent("Excellent! Please let us know about additional property address.");
        });

        // await waitFor(async () => {
        //     const btnSave = getByTestId("btn-save");
        //     act(() => {
        //         fireEvent.click(btnSave)
        //     });
        // });
    });
});
