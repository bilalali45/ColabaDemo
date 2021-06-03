import React from "react";
import {
    act,
    fireEvent,
    getAllByTestId,
    render,
    waitFor,
} from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { AboutYourSelf } from "./AboutYourSelf";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../store/store";
//import BorrowerActions from "../../../../store/actions/BorrowerActions";
import { APIResponse } from "../../../../Entities/Models/APIResponse";
import { LoanApplicationsType } from "../../../../store/reducers/LoanApplicationReducer";
import BorrowerActions from "../../../../store/actions/__mocks__/BorrowerActions";
import { LocalDB } from "../../../../lib/LocalDB";


// jest.mock('../../lib/localStorage');
jest.mock("../../../../test_utilities/lodashMock")
jest.mock("../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../store/actions/BorrowerActions");
jest.mock("../../../../lib/LocalDB")
// jest.mock('../../../../../Store/actions/TemplateActions');

const state = {
    leftMenu: {
        navigation: null,
        leftMenuItems: [],
        notAllowedItems: [],
    },
    error: {},
    loanManager: {
        loanInfo: {
            //   loanApplicationId: 41313,
            //   loanPurposeId: null,
            //   loanGoalId: null,
            //   borrowerId: 31494,
            //   ownTypeId: 2,
            //   borrowerName: "second",
        },
        // incomeInfo:{
        //   incomeId:2102,
        //   incomeTypeId:null
        // },
        primaryBorrowerInfo: {
            id: 31450,
            name: "khalid"
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


beforeEach(() => {
    MockEnvConfig();
    MockSessionStorage();
    // NavigationHandler.navigation.moveNext = jest.fn(()=>{})
    NavigationHandler.getNavigationStateAsString = jest.fn(() => "")
    NavigationHandler.navigateToPath = jest.fn(() => { })
    NavigationHandler.isFieldVisible = jest.fn(() => true)
    LocalDB.setLoanAppliationId("2");
});

describe("About yourself ", () => {

    test("Should change Legal First Name", async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );

        // BorrowerActions.getAllBorrower = jest.fn((loanApplicationId: number)=>{return new APIResponse(200, [])})
        let LegalLastName: HTMLElement;
        await waitFor(() => {
            LegalLastName = getByTestId("LegalFirstName");
            fireEvent.change(LegalLastName, { target: { value: "test" } })

        });
        await waitFor(() => {
            expect(LegalLastName).toHaveDisplayValue("test");
        })

    });

    test("Should change Middle Name", async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );

        let MiddleName: HTMLElement;
        await waitFor(() => {
            MiddleName = getByTestId("MiddleName");
            // expect(resetPassHeader).toBeInTheDocument();
            fireEvent.change(MiddleName, { target: { value: "test" } })

        });
        await waitFor(() => {
            expect(MiddleName).toHaveDisplayValue("test");
        })
    });

    test("Should change Legal Last Name", async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );

        let LegalLastName: HTMLElement;
        await waitFor(() => {
            LegalLastName = getByTestId("LegalLastName");
            // expect(resetPassHeader).toBeInTheDocument();
            fireEvent.change(LegalLastName, { target: { value: "test" } })

        });
        await waitFor(() => {
            expect(LegalLastName).toHaveDisplayValue("test");
        })
    });

    test("Should change Suffix", async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );

        let Suffix: HTMLElement;
        await waitFor(() => {
            Suffix = getByTestId("Suffix");
            // expect(resetPassHeader).toBeInTheDocument();
            fireEvent.change(Suffix, { target: { value: "Sr." } })

        });
        await waitFor(() => {
            expect(Suffix).toHaveDisplayValue([""]);
        })
    });

    test("Should change Email Address", async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );

        let EmailAddress: HTMLElement;
        await waitFor(() => {
            EmailAddress = getByTestId("EmailAddress");
            // expect(resetPassHeader).toBeInTheDocument();
            fireEvent.change(EmailAddress, { target: { value: "abc@test.com" } })

        });
        await waitFor(() => {
            expect(EmailAddress).toHaveDisplayValue([""]);
        })
    });

    test("Should change Home Phone Number", async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );

        let HomePhoneNumber: HTMLElement;
        await waitFor(() => {
            HomePhoneNumber = getByTestId("HomePhoneNumber");
            // expect(resetPassHeader).toBeInTheDocument();
            fireEvent.change(HomePhoneNumber, { target: { value: "123456789" } })

        });
        await waitFor(() => {
            expect(HomePhoneNumber).toHaveDisplayValue("(123) 456-789");
        })
    });

    test("Should change Work Phone Number", async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );

        let WorkPhoneNumber: HTMLElement;
        await waitFor(() => {
            WorkPhoneNumber = getByTestId("WorkPhoneNumber");
            // expect(resetPassHeader).toBeInTheDocument();
            fireEvent.change(WorkPhoneNumber, { target: { value: "123456789" } })

        });
        await waitFor(() => {
            expect(WorkPhoneNumber).toHaveDisplayValue("(123) 456-789");
        })
    });

    test("Should change Cell Phone Number", async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );

        let CellPhoneNumber: HTMLElement;
        await waitFor(() => {
            CellPhoneNumber = getByTestId("CellPhoneNumber");
            // expect(resetPassHeader).toBeInTheDocument();
            fireEvent.change(CellPhoneNumber, { target: { value: "123456789" } })

        });
        await waitFor(() => {
            // expect(CellPhoneNumber).toHaveTextContent([""]);
        })
    });

    test("Should show error on wrong Cell Phone Number", async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );

        let CellPhoneNumber: HTMLElement;
        await waitFor(() => {
            CellPhoneNumber = getByTestId("CellPhoneNumber");
            // expect(resetPassHeader).toBeInTheDocument();
            fireEvent.change(CellPhoneNumber, { target: { value: "(111) 111-111" } })

        });


        let SubmitBtn: HTMLElement;
        await waitFor(() => {
            SubmitBtn = getByTestId("ays-submit-btn");
            //   
            fireEvent.click(SubmitBtn);

        });

        await waitFor(() => {
            expect(getByTestId("cellPhoneNumber-error")).toBeInTheDocument();
            expect(getByTestId("cellPhoneNumber-error")).toHaveTextContent("Please enter US Phone Number only");
        })

    });


    test("Should submit form if all fields are filled", async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );
        await waitFor(() => {
            fireEvent.change(getByTestId("LegalFirstName"), { target: { value: "test" } })

            fireEvent.change(getByTestId("MiddleName"), { target: { value: "middle" } })

            fireEvent.change(getByTestId("LegalLastName"), { target: { value: "last" } })

            fireEvent.change(getByTestId("EmailAddress"), { target: { value: "abc@test.com" } })

            fireEvent.change(getByTestId("HomePhoneNumber"), { target: { value: "123456789" } })
            fireEvent.change(getByTestId("WorkPhoneNumber"), { target: { value: "123456789" } })
            fireEvent.change(getByTestId("CellPhoneNumber"), { target: { value: "123456789" } })

        })
        let SubmitBtn;
        await waitFor(() => {
            SubmitBtn = getByTestId("ays-submit-btn");
            //   
            fireEvent.click(SubmitBtn);

        });
    });

    test("Should check borrower exists", async () => {
        var st = { ...state };
        var loanManager = st.loanManager as LoanApplicationsType;
        loanManager.loanInfo = {
            loanApplicationId: 41313,
            loanPurposeId: null,
            loanGoalId: null,
            borrowerId: 31494,
            ownTypeId: 2,
            borrowerName: "second",
        }
        st.loanManager = loanManager;

        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );
    });

    test("Should check borrower not exists", async () => {
        LocalDB.setLoanAppliationId(null);
        var st = { ...state };
        var loanManager = st.loanManager as LoanApplicationsType;
        loanManager.loanInfo = {
            loanApplicationId: 41313,
            loanPurposeId: null,
            loanGoalId: null,
            borrowerId: 31494,
            ownTypeId: 1,
            borrowerName: "second",
        }
        st.loanManager = loanManager;

        //BorrowerActions.AllBorrower = null;
        //BorrowerActions.getAllBorrower = jest.fn(() => {
        //    return new APIResponse(200, null);
        //});

        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );
    });

    test("Should check on update", async () => {
        LocalDB.setLoanAppliationId("2");
        var st = { ...state };
        var loanManager = st.loanManager as LoanApplicationsType;
        loanManager.loanInfo = {
            loanApplicationId: 41313,
            loanPurposeId: null,
            loanGoalId: null,
            borrowerId: 31494,
            ownTypeId: 1,
            borrowerName: "second",
        }
        st.loanManager = loanManager;

        const { getByTestId, getAllByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutYourSelf />
            </Store.Provider>
        );


        await waitFor(() => {
            let SubmitBtn = getByTestId("ays-submit-btn");
            fireEvent.click(SubmitBtn);
        })
    });
});
