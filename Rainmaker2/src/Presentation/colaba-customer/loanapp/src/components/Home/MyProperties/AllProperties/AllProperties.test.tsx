import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
    screen
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { AllProperties } from "./AllProperties";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../store/store";
import { LocalDB } from '../../../../lib/LocalDB';
import MyPropertyActions from "../../../../store/actions/MyPropertyActions";
import { APIResponse } from "../../../../Entities/Models/APIResponse";


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

const totalNumbersOfProperties = 1;

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {

    var { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
            <AllProperties setEditting={() => { console.log("Set Editing Called") }} />
        </Store.Provider>
    );

    await waitFor(async () => {
        const header = getByTestId("head");
        expect(header).toHaveTextContent("My Properties");

        let tooltip = getByTestId("tooltip")
        expect(tooltip).toHaveTextContent("please list all properties you own.");
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


describe("My Properties > All Properties", () => {

    it("Should render properties list", async () => {
        LocalDB.setLoanAppliationId("2");

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(() => {
            expect(getByTestId("properties-container")).toBeInTheDocument();
            var propertyItems = getAllByTestId("property-item");
            expect(propertyItems).toHaveLength(totalNumbersOfProperties)
        });
    });


    it("Should edit and delete a property", async () => {
        LocalDB.setLoanAppliationId("2");

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(() => {
            let editBtns = getAllByTestId("edit-btn");
            expect(editBtns[0]).toBeInTheDocument();

            act(() => {
                fireEvent.click(editBtns[0]);
            });
        });

        await waitFor(() => {
            let deleteBtns = getAllByTestId("delete-btn");
            expect(deleteBtns[0]).toBeInTheDocument();

            act(() => {
                fireEvent.click(deleteBtns[0]);
            });
        });
    });

    it("Should delete with error", async () => {
        MyPropertyActions.deleteProperty = jest.fn((loanApplicationId: number, borrowerPropertyId: number) => {
            console.log("!!! Running inline mocked property delete function")
            throw new Error("Some error");
        });

        LocalDB.setLoanAppliationId("2");

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(() => {
            let deleteBtns = getAllByTestId("delete-btn");
            expect(deleteBtns[0]).toBeInTheDocument();

            act(() => {
                fireEvent.click(deleteBtns[0]);
            });
        });
    });


    it("Should click skip and add additional property", async () => {
        LocalDB.setLoanAppliationId("2");

        var { getByTestId, getAllByTestId } = await initiateComponent();


        await waitFor(() => {
            let nextBtn = getByTestId("no-additional-properties");
            expect(nextBtn).toBeInTheDocument();

            act(() => {
                fireEvent.click(nextBtn);
            });
        });

        await waitFor(() => {
            let addBtn = getByTestId("add-additional-properties");
            expect(addBtn).toBeInTheDocument();

            act(() => {
                fireEvent.click(addBtn);
            });
        });
    });

    it("Should render with no property", async () => {
        MyPropertyActions.getPropertyList = jest.fn((loanApplicationId: number, borrowerPropertyId: number) => {
            console.log("!!! Running inline mocked getPropertyList function")
            return new APIResponse(200, null);
        });


        LocalDB.setLoanAppliationId("2");

        var { getByTestId, getAllByTestId } = await initiateComponent();


        await waitFor(() => {
            let nextBtn = getByTestId("no-additional-properties");
            expect(nextBtn).toBeInTheDocument();

            act(() => {
                fireEvent.click(nextBtn);
            });
        });

        await waitFor(() => {
            let addBtn = getByTestId("add-additional-properties");
            expect(addBtn).toBeInTheDocument();

            act(() => {
                fireEvent.click(addBtn);
            });
        });
    });


});