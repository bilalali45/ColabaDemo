import React from "react";
import {
  fireEvent,
  render,
  waitFor,
} from "@testing-library/react";

import { Store } from "../../../../store/store";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { SubjectPropertyAddress } from "./SubjectPropertyAddress";

// jest.mock('../../lib/localStorage');
jest.mock("../../../../Store/Actions/GettingToKnowYouActions");
jest.mock('../../../../Store/actions/MyNewPropertyAddressActions');

const state = {
  leftMenu: {
    navigation: null,
    leftMenuItems: [],
    notAllowedItems: [],
  },
  error: {},
  loanManager: {
    loanInfo:{
      loanApplicationId: 41313,
      loanPurposeId: null,
      loanGoalId: null,
      borrowerId: 31494,
      ownTypeId: 2,
      borrowerName: "second",
    },
    primaryBorrowerInfo: {
      id: 31450, 
      name: "khalid"
    } 
    
  },
  commonManager: {},
  employment:{},
  business:{},
  employmentHistory:{},
  militaryIncomeManager: {},
  otherIncomeManager:{},
  assetsManager: {}
};
const dispatch = jest.fn();

const setupGoogleMock = () => {
  /*** Mock Google Maps JavaScript API ***/
  const google = {
    maps: {
      places: {
        AutocompleteService: () => {},
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
      Geocoder: () => {},
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

describe("Personal information Subject Home Address ", () => {
  test("Should show Page heading ", async () => {
    const { getByTestId } = render(
      <SubjectPropertyAddress/>
    );

    await waitFor(() => {
      const resetPassHeader:HTMLElement = getByTestId("page-title");
      // expect(resetPassHeader).toBeInTheDocument();

      expect(resetPassHeader).toHaveTextContent("Subject Property");
    });
  });

  test("Should show back Button ", async () => {
    const { getByTestId } = render(
        <SubjectPropertyAddress/>
    );

    await waitFor(() => {
      const resetPassHeader:HTMLElement = getByTestId("back-btn-txt");
      expect(resetPassHeader).toBeInTheDocument();

      expect(resetPassHeader).toHaveTextContent("Back");
    });
  });

  test("Should show states drop down and select option ", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
      <SubjectPropertyAddress/>
    </Store.Provider>
    );

    await waitFor(() => {
      const StatesDD:HTMLElement = getByTestId("state");
      expect(StatesDD).toBeInTheDocument();

      fireEvent.click(StatesDD);
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
        <SubjectPropertyAddress/>
      </Store.Provider>
    );

    await waitFor(() => {
      const CountriesDD:HTMLElement = getByTestId("country");
      expect(CountriesDD).toBeInTheDocument();

      fireEvent.click(CountriesDD);
    });
    await waitFor(() => {
      // let housingStatusOptions = getAllByTestId("country-option")
      // expect(housingStatusOptions[0]).toBeInTheDocument()
      // fireEvent.click(housingStatusOptions[0])
      // fireEvent.change(CountriesDD, { target: { value: "Own" } })
    });

    // expect(resetPassHeader).toHaveTextContent("Back")
  });

  test("Should submit form on button click ", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SubjectPropertyAddress/>
      </Store.Provider>
    );

    await waitFor(() => {
        let street_address:HTMLElement[] = getAllByTestId("street_address")
        fireEvent.change(street_address[1], { target: { value: "street_address" } })
      });

    await waitFor(() => {
        let state:HTMLElement = getByTestId("state")
        expect(state).toBeInTheDocument()
        const stateTxt = state?.children[0]?.children[0]
        fireEvent.change(stateTxt, { target: { value: "test" } })
      });

    await waitFor(() => {
      const SubAddressProperty:HTMLElement = getByTestId("subject-address-btn");
      expect(SubAddressProperty).toBeInTheDocument();

      fireEvent.click(SubAddressProperty);
    });
    
  });
});
