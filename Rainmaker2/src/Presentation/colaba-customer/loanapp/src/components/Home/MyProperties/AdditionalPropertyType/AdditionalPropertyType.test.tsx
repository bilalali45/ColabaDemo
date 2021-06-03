import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
    screen
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { AdditionalPropertyType } from "./AdditionalPropertyType";
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

const totalNumbersOfPropertyTypes = 8;

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {

    var { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
            <AdditionalPropertyType />
        </Store.Provider>
    );

    await waitFor(async () => {
        let radioBox = getAllByTestId('list-item');
        expect(radioBox).toHaveLength(totalNumbersOfPropertyTypes);
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


describe("My Properties > Addtional Property ", () => {

    it("Should render all property types", async () => {
        LocalDB.setBorrowerId(null);
        LocalDB.setLoanAppliationId("9823");
        LocalDB.setMyPropertyTypeId(null);

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            const header = getByTestId("head");
            expect(header).toHaveTextContent("Additional Property");

            let tooltip = getByTestId("tooltip")
            expect(tooltip).toHaveTextContent("Please tell us more about this property");
            let radioBox = getAllByTestId('list-item');
            expect(radioBox).toHaveLength(totalNumbersOfPropertyTypes);

            radioBox.forEach(element => {
                expect(element).not.toHaveClass('active');
            });

            let saveBtn = getByTestId("save-btn");
            expect(saveBtn).toBeInTheDocument();

            expect(saveBtn).toBeDisabled();
        });
    });

    it("Should select property type", async () => {
        LocalDB.setBorrowerId(null);
        LocalDB.setLoanAppliationId(null);
        LocalDB.setMyPropertyTypeId(null);

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            let radioBox = getAllByTestId('list-item');
            act(() => {
                fireEvent.click(radioBox[1]);
            })
            await waitFor(() => {
                expect(radioBox[0]).not.toHaveClass('active');
                expect(radioBox[1]).toHaveClass('active');
            });

            act(() => {
                fireEvent.click(getByTestId("save-btn"));
            })
        });

        let saveBtn = getByTestId("save-btn");
        expect(saveBtn).toBeInTheDocument();
        expect(saveBtn).toBeEnabled();
    });

    it("Should fetch and render borrower property", async () => {
        LocalDB.setBorrowerId("10790");
        LocalDB.setLoanAppliationId("9823");
        LocalDB.setAdditionalPropertyTypeId(1053);

        var { getByTestId, getAllByTestId } = await initiateComponent();

        let radioBoxes = getAllByTestId('list-item');

        console.log("!!!!!")
        var isActive = false;
        radioBoxes.forEach(x => {
            if (!isActive) {
                isActive = x.classList.contains('active');
            }
        });

        expect(isActive).toBe(true);

        act(() => {
            fireEvent.click(getByTestId("save-btn"));
        })
    })
});