import React from "react";
import { fireEvent, render, waitFor } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { LoanOfficer } from "./LoanOfficer";
import { LocalDB } from "../../../../lib/LocalDB";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";


jest.mock('../../../../Store/actions/GettingStartedActions');
jest.mock('../../../../Store/actions/BorrowerActions');
jest.mock('../../../../lib/LocalDB');

beforeEach(() => {
    MockEnvConfig();
    MockSessionStorage();
    NavigationHandler.getNavigationStateAsString = jest.fn(() => "{called: getNavigationStateAsString}");
    NavigationHandler.navigateToPath = jest.fn((path) => { console.log("Navigated to path: " + path) });
    NavigationHandler.moveNext = jest.fn(() => { console.log("Moved Next") });
});

describe("Getting Started Steps ", () => {
    test("Should render Loan officer detail", async () => {
        const { getByTestId } = render(
            <LoanOfficer />
        );
        LocalDB.getLoanGoalId = jest.fn(() => (undefined))
        await waitFor(() => {
            const loHeader = getByTestId("lo-header");
            expect(loHeader).toHaveTextContent("If You Have Any Questions, We Are Here To Help");

            let loBox = getByTestId("lo-box")
            expect(loBox).toBeInTheDocument();

            let loName = getByTestId('lo-name');
            expect(loName.children[0]).toHaveTextContent('Aliya');
            expect(loName.children[1]).toHaveTextContent('Your Loan Officer');

            let btn = getByTestId('lo-continue-btn');
            expect(btn).toBeInTheDocument();
        });

    });

    test("Should click continue", async () => {
        LocalDB.setLoanAppliationId("2");
        const { getByTestId } = render(
            <LoanOfficer />
        );
        LocalDB.getLoanGoalId = jest.fn(() => (undefined))
        await waitFor(() => {
            let ContinueBtn: HTMLElement = getByTestId("lo-continue-btn")
            expect(ContinueBtn).toBeInTheDocument();
            fireEvent.click(ContinueBtn)
        })

    });


    test("Should render Loan officer detail", async () => {
        const { getByTestId } = render(
            <LoanOfficer />
        );
    });

    test("Should invoke Loan officer reset", async () => {
        LocalDB.setLoanGoalId("2");
        LocalDB.setLoanAppliationId("2");

        const { getByTestId } = render(
            <LoanOfficer />
        );
        LocalDB.getLoanGoalId = jest.fn(() => (undefined))
        await waitFor(() => {
            const loHeader = getByTestId("lo-header");
            expect(loHeader).toHaveTextContent("If You Have Any Questions, We Are Here To Help");

            let loBox = getByTestId("lo-box")
            expect(loBox).toBeInTheDocument();

            let loName = getByTestId('lo-name');
            expect(loName.children[0]).toHaveTextContent('Aliya');
            expect(loName.children[1]).toHaveTextContent('Your Loan Officer');

            let btn = getByTestId('lo-continue-btn');
            expect(btn).toBeInTheDocument();
        });

    });
});
