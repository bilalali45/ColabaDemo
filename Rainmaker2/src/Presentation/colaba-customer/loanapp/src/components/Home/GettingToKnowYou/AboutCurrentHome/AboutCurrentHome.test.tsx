import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor,
} from "@testing-library/react";

import AboutCurrentHome from "./AboutCurrentHome";
import { InitialStateType, Store } from "../../../../store/store";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { LocalDB } from "../../../../lib/LocalDB";
import { LoanApplicationsType } from "../../../../store/reducers/LoanApplicationReducer";

// jest.mock('../../lib/localStorage');
jest.mock("../../../../Store/Actions/GettingToKnowYouActions");
// jest.mock('../../../../../Store/actions/TemplateActions');

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
            ownTypeId: 1,
            borrowerName: "second",
        },
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

beforeEach(() => {
    setupGoogleMock();
    MockEnvConfig();
    MockSessionStorage();
});

describe("Personal information Current Home Address ", () => {
    LocalDB.setLoanAppliationId("1");

    test("Should show Page heading ", async () => {
        const { getByTestId } = render(
            <AboutCurrentHome currentStep={""} setcurrentStep={""} />
        );

        await waitFor(() => {
            const resetPassHeader: HTMLElement = getByTestId("page-title");
            // expect(resetPassHeader).toBeInTheDocument();

            expect(resetPassHeader).toHaveTextContent("Personal Information");
        });
    });

    test("Should show back Button ", async () => {
        const { getByTestId } = render(
            <AboutCurrentHome currentStep={""} setcurrentStep={""} />
        );

        await waitFor(() => {
            const resetPassHeader: HTMLElement = getByTestId("back-btn-txt");
            expect(resetPassHeader).toBeInTheDocument();

            expect(resetPassHeader).toHaveTextContent("Back");
        });
    });

    test("Should show states drop down and select option ", async () => {
        const { getByTestId, getAllByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutCurrentHome currentStep={""} setcurrentStep={""} />
            </Store.Provider>
        );

        await waitFor(() => {
            const StatesDD: HTMLElement = getByTestId("state");
            expect(StatesDD).toBeInTheDocument();

            act(() => {
                fireEvent.click(StatesDD);
            })
        });
        await waitFor(() => {
            // let housingStatusOptions = getAllByTestId("states-option")
            // expect(housingStatusOptions[0]).toBeInTheDocument()
            // fireEvent.click(housingStatusOptions[0])
            // fireEvent.change(StatesDD, { target: { value: "Own" } })
        });

        // expect(resetPassHeader).toHaveTextContent("Back")
    });

    test("Should show countries drop down and select option ", async () => {
        const { getByTestId, getAllByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutCurrentHome currentStep={""} setcurrentStep={""} />
            </Store.Provider>
        );

        await waitFor(() => {
            const CountriesDD: HTMLElement = getByTestId("country");
            expect(CountriesDD).toBeInTheDocument();

            act(() => {
                fireEvent.click(CountriesDD);
            })
        });
        await waitFor(() => {
            // let housingStatusOptions = getAllByTestId("country-option")
            // expect(housingStatusOptions[0]).toBeInTheDocument()
            // fireEvent.click(housingStatusOptions[0])
            // fireEvent.change(CountriesDD, { target: { value: "Own" } })
        });

        // expect(resetPassHeader).toHaveTextContent("Back")
    });

    test("Should show housing status drop down and select option ", async () => {

        const { getByTestId, getAllByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutCurrentHome currentStep={""} setcurrentStep={""} />
            </Store.Provider>
        );

        await waitFor(() => {
            const housingStatusDD: HTMLElement = getByTestId("Housing-Status");
            expect(housingStatusDD).toBeInTheDocument();
            act(() => {
                fireEvent.click(housingStatusDD);
            })
        });
        // await waitFor(() => {
        //   let housingStatusOptions = getAllByTestId("housingStatus-option");
        //   expect(housingStatusOptions[0]).toBeInTheDocument();

        //   fireEvent.click(housingStatusOptions[0]);
        //   // fireEvent.change(housingStatusDD, { target: { value: "Own" } })
        // });

        // expect(resetPassHeader).toHaveTextContent("Back")
    });

    test("Should submit form on button click ", async () => {
        const { getByTestId, getAllByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutCurrentHome currentStep={""} setcurrentStep={""} />
            </Store.Provider>
        );

        await waitFor(() => {
            let street_address: HTMLElement[] = getAllByTestId("street_address")
            act(() => {
                if (street_address)
                    fireEvent.change(street_address[1], { target: { value: "street_address" } })
            })
        });

        await waitFor(() => {
            let state: HTMLElement = getByTestId("state")
            if (state) {
                expect(state).toBeInTheDocument()
                const stateTxt: Element = state?.children[0]?.children[0]
                act(() => {
                    fireEvent.change(stateTxt, { target: { value: "test" } })
                })
            }
        });

        await waitFor(() => {
            const CurrentHomeBtn: HTMLElement = getByTestId("current-home-btn");
            if (CurrentHomeBtn) {
                expect(CurrentHomeBtn).toBeInTheDocument();
                expect(CurrentHomeBtn).not.toBeDisabled();
                //act(() => {
                fireEvent.click(CurrentHomeBtn);
                //})
            }
        });

    });

    test("Should submit form on button click on Current Borrower ", async () => {
        const { getByTestId, getAllByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutCurrentHome currentStep={""} setcurrentStep={""} />
            </Store.Provider>
        );

        await waitFor(() => {
            const CurrentHomeBtn: HTMLElement = getByTestId("current-home-btn");
            if (CurrentHomeBtn) {
                expect(CurrentHomeBtn).toBeInTheDocument();
                expect(CurrentHomeBtn).not.toBeDisabled();
                //act(() => {
                fireEvent.click(CurrentHomeBtn);
                //})
            }
        });
    });

    test("Should render for own type co-applicant ", async () => {
        var st = { ...state };
        var loanManager = st.loanManager as LoanApplicationsType;
        loanManager.loanInfo.ownTypeId = 2;
        st.loanManager = loanManager;
        const { getByTestId, getAllByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutCurrentHome currentStep={""} setcurrentStep={""} />
            </Store.Provider>
        );
    });

    test("Should submit form on button click on Primary Borrower ", async () => {
        var st = { ...state };
        var loanManager = st.loanManager as LoanApplicationsType;
        loanManager.loanInfo.borrowerId = 0;
        st.loanManager = loanManager;
        const { getByTestId, getAllByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
                <AboutCurrentHome currentStep={""} setcurrentStep={""} />
            </Store.Provider>
        );
    });
});
