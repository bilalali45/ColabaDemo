import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
    screen
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { CurrentResidence } from "./CurrentResidence";
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
        myPropertyInfo:{
            primaryPropertyTypeId:2
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

const totalNumbersOfPropertyTypes = 7;

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {

    var { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
            <CurrentResidence setAddress={""} />
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


describe("My Properties > Current Residence ", () => {

    it("Should render all property types", async () => {
        LocalDB.setBorrowerId(null);
        LocalDB.setLoanAppliationId(null);
        LocalDB.setMyPropertyTypeId(null);

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            const header = getByTestId("head");
            expect(header).toHaveTextContent("My Current Residence");

            console.log("LocalDB: " + LocalDB)
            let tooltip = getByTestId("tooltip")            
            expect(tooltip).toHaveTextContent("Please tell us more about this property");
            //expect(tooltip).toHaveTextContent("Great. What type of property is");
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

    it("Should select property type \"without\" Rental", async () => {
        LocalDB.setBorrowerId(null);
        LocalDB.setLoanAppliationId("9823");
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

        var rentalIncome: HTMLElement = null;

        try {
            var rentalIncome = getByTestId('rental-income')
        } catch (e) {
            // Success if test id is not found
            console.log(e);
        }

        expect(rentalIncome).toBe(null);

        let saveBtn = getByTestId("save-btn");
        expect(saveBtn).toBeInTheDocument();
        expect(saveBtn).toBeEnabled();
    });

    it("Should select property type \"with\" Rental", async () => {
        LocalDB.setBorrowerId(null);
        LocalDB.setLoanAppliationId("9823");
        LocalDB.setMyPropertyTypeId(null);

        var { getByTestId, getAllByTestId } = await initiateComponent();

        await waitFor(async () => {
            let radioBox = getAllByTestId('list-item');
            expect(radioBox).toHaveLength(totalNumbersOfPropertyTypes);
        });

        await waitFor(async () => {
            let radioBox = getAllByTestId('list-item');
            act(() => {
                fireEvent.click(radioBox[6]);
            })
            await waitFor(() => {
                expect(radioBox[0]).not.toHaveClass('active');
                expect(radioBox[6]).toHaveClass('active');
            });
            act(() => {
                fireEvent.click(getByTestId("save-btn"));
            })
        });

        var rentalIncome = screen.getByTestId('rental-income')
        expect(rentalIncome).toBeInTheDocument();

        let saveBtn = getByTestId("save-btn");
        expect(saveBtn).toBeInTheDocument();
        expect(saveBtn).toBeEnabled();
    });

    it("Should fetch and render borrower property", async () => {
        LocalDB.setBorrowerId("10790");
        LocalDB.setLoanAppliationId("9823");
        LocalDB.setMyPropertyTypeId(1053);

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