import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
    screen
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { AdditionalPropertyDetails } from "./AdditionalPropertyDetails";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../store/store";
import { LocalDB } from '../../../../lib/LocalDB';


// Loading manual mocks for modules
jest.mock("../../../../store/actions/CommonActions");
jest.mock("../../../../store/actions/MyPropertyActions");
jest.mock("../../../../store/actions/MyNewMortgageActions");
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

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {

    var { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
            <AdditionalPropertyDetails address={"Test Address"} />
        </Store.Provider>
    );

    await waitFor(async () => {
        const header = getByTestId("head");
        expect(header).toHaveTextContent("My Properties");

        let tooltip = getByTestId("tooltip")
        expect(tooltip).toHaveTextContent("Excellent! Please let us know about additional property address.");
    });

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


describe("My Properties > Additional Property Details", () => {

    it("Should render new, fill and save", async () => {
        LocalDB.setLoanAppliationId("2");
        LocalDB.setAdditionalPropertyTypeId(null);

        var { getByTestId, getAllByTestId } = await initiateComponent();

        let valueInput = getByTestId("value-input");
        let duesInput = getByTestId("dues-input");
        var saleYes = getByTestId('saleYes')
        let saveBtn = getByTestId("btn-save");

        await waitFor(() => {

            expect(valueInput).toBeInTheDocument();

            expect(duesInput).toBeInTheDocument();

            act(() => {
                fireEvent.change(valueInput, { target: { value: "600" } });
            })

            act(() => {
                fireEvent.change(duesInput, { target: { value: "300" } });
            })

            expect(saleYes).toBeInTheDocument();

            act(() => {
                fireEvent.click(saleYes)
            });
        });

        await waitFor(() => {
            expect(valueInput).toHaveDisplayValue(["600"]);

            expect(duesInput).toHaveDisplayValue(["300"]);
        });

        await waitFor(() => {
            expect(saveBtn).toBeInTheDocument();

            expect(saveBtn).not.toBeDisabled();

            act(() => {
                fireEvent.click(saveBtn);
            })
        });
    });

    it("Should render existing data, update fields and save", async () => {
        LocalDB.setLoanAppliationId("2");
        LocalDB.setAdditionalPropertyTypeId(2);

        var { getByTestId, getAllByTestId } = await initiateComponent();

        let valueInput = getByTestId("value-input");
        let duesInput = getByTestId("dues-input");
        var saleYes = getByTestId('saleYes')
        let saveBtn = getByTestId("btn-save");

        await waitFor(() => {
            expect(valueInput).toBeInTheDocument();

            expect(duesInput).toBeInTheDocument();

            act(() => {
                fireEvent.change(valueInput, { target: { value: "600" } });
            })

            act(() => {
                fireEvent.change(duesInput, { target: { value: "300" } });
            })

            expect(saleYes).toBeInTheDocument();

            act(() => {
                fireEvent.click(saleYes)
            });
        });

        await waitFor(() => {

            expect(valueInput).toHaveDisplayValue(["600"]);

            expect(duesInput).toHaveDisplayValue(["300"]);
        });

        await waitFor(() => {
            expect(saveBtn).toBeInTheDocument();

            expect(saveBtn).not.toBeDisabled();

            act(() => {
                fireEvent.click(saveBtn);
            })
        });
    });
});