import React from "react";
import {
    act,
    fireEvent,
    getAllByTestId,
    render,
    waitFor,
} from "@testing-library/react";
import {MemoryRouter} from "react-router-dom";
import {MockEnvConfig} from "../../../../../../../test_utilities/EnvConfigMock";
import {MockSessionStorage} from "../../../../../../../test_utilities/SessionStoreMock";

import {InitialStateType, Store} from "../../../../../../../store/store";
import {NavigationHandler} from "../../../../../../../Utilities/Navigation/NavigationHandler";
import {BusinessRevenue} from "./BusinessRevenue";

// jest.mock('../../lib/localStorage');
jest.mock("../../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../../store/actions/BusinessActions");
jest.mock('../../../../../../../store/actions/EmploymentHistoryActions');

const state: InitialStateType = {
    leftMenu: {
        navigation: null,
        leftMenuItems: [],
        notAllowedItems: [],
    },
    error: {},
    loanManager: {
        loanInfo: {
            loanApplicationId: 41313,
            loanPurposeId: null,
            loanGoalId: null,
            borrowerId: 31494,
            ownTypeId: 2,
            borrowerName: "second",
        },
        primaryBorrowerInfo: {
            id: 31450,
            name: "Qumber"
        },
        incomeInfo: {
            incomeId:1,
            incomeTypeId: 3
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


beforeEach(() => {
    MockEnvConfig();
    MockSessionStorage();
    NavigationHandler.getNavigationStateAsString = jest.fn(() => "")
    NavigationHandler.navigateToPath = jest.fn(() => {
    })
});

describe("Previous Employment Income ", () => {
    test("Should change Annual Amount", async () => {
        const { getAllByTestId} = render(
            <Store.Provider value={{state, dispatch}}>
                <BusinessRevenue/>
            </Store.Provider>
        );

        let annualIncome: HTMLElement[]

        await waitFor(() => {
            annualIncome = getAllByTestId("net-annual-income");
            // expect(ssnInput).toBeInTheDocument();
            act(() => {
                fireEvent.change(annualIncome[0] , {target: {value: "879878979"}});
            });
        });

        await waitFor(() => {
            expect(annualIncome[0]).toHaveDisplayValue("879,878,979");
        })
    });


    test("Should show errors on empty form", async () => {
        const {getByTestId} = render(
            <Store.Provider value={{state, dispatch}}>
                <BusinessRevenue/>
            </Store.Provider>
        );

        let SubmitBtn: HTMLElement;
        await waitFor(() => {
            SubmitBtn = getByTestId("net-annual-income-submit");
            //
            fireEvent.click(SubmitBtn);

        });
        await waitFor(() => {
            expect(getByTestId("netAnnualIncome-error")).toBeInTheDocument();
        })
    });

    test("Should submit form if field is filled", async () => {
        const {getByTestId} = render(
            <Store.Provider value={{state, dispatch}}>
                <BusinessRevenue/>
            </Store.Provider>
        );
        await waitFor(() => {
            fireEvent.change(getByTestId("net-annual-income"), {target: {value: "1"}})

        })
        let SubmitBtn: HTMLElement;
        await waitFor(() => {
            SubmitBtn = getByTestId("net-annual-income-submit");
            //
            fireEvent.click(SubmitBtn);

        });
    });


});
