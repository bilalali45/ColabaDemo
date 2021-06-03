import React from "react";
import {
  fireEvent,
  getAllByTestId,
  getByPlaceholderText,
  render,
  waitFor,
} from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import Path from "path";
import { MockEnvConfig } from "../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../../../store/store";
import { EmployerAddress } from "./EmployerAddress";

// jest.mock('../../lib/localStorage');
jest.mock("../../../../../../../store/actions/GettingToKnowYouActions");
// jest.mock('../../../../Store/actions/MyNewPropertyAddressActions');

const state = {
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
      name: "khalid",
    },
    states: [
      { id: 1, countryId: 1, name: "Alaska", shortCode: "AK" },
      { id: 41, countryId: 1, name: "Rhode Island", shortCode: "RI" },
      { id: 42, countryId: 1, name: "South Carolina", shortCode: "SC" },
      { id: 43, countryId: 1, name: "South Dakota", shortCode: "SD" },
      { id: 44, countryId: 1, name: "Tennessee", shortCode: "TN" },
      { id: 45, countryId: 1, name: "Texas", shortCode: "TX" },
      { id: 46, countryId: 1, name: "Utah", shortCode: "UT" },
    ],
    countries: [
      { id: 1, name: "United States", shortCode: "US" },
      { id: 2, name: "Andorra", shortCode: "AD" },
      { id: 3, name: "United Arab Emirates", shortCode: "AE" },
      { id: 4, name: "Afghanistan", shortCode: "AF" },
      { id: 5, name: "Antigua and Barbuda", shortCode: "AG" },
      { id: 6, name: "Anguilla", shortCode: "AI" },
      { id: 7, name: "Albania", shortCode: "AL" },
      { id: 8, name: "Armenia", shortCode: "AM" },
      { id: 9, name: "Angola", shortCode: "AO" },
      { id: 250, name: "Zambia", shortCode: "ZM" },
      { id: 251, name: "Zimbabwe", shortCode: "ZW" },
      { id: 252, name: "Serbia and Montenegro", shortCode: "CS" },
      { id: 253, name: "Netherlands Antilles", shortCode: "AN" },
    ],
  },
  commonManager: {},
  employment: {
    employerInfo:{
      EmployerName: "Abacusoft",
      JobTitle: "Senior Software Engineer",
      StartDate: "2018-01-08T00:00:00",
      EndDate: "2018-08-31T00:00:00",
      YearsInProfession: 15,
      EmployerPhoneNumber: "1234567890",
      EmployedByFamilyOrParty: null,
      HasOwnershipInterest: true,
      OwnershipInterest: "21.00",
      IncomeInfoId: 66
    },
    employerAddress:{
      streetAddress: "Defence Phase VI",
      unitNo: "4th Floor",
      cityId: 1,
      cityName: "Thatta",
      countryId: 1,
      stateId: 1,
      stateName: "Sindh",
      zipCode: "73130"
    },
    wayOfIncome:{
      isPaidByMonthlySalary: false,
      hourlyRate: null,
      hoursPerWeek: null,
      employerAnnualSalary:""
    }, 
    additionalIncome:[{
      incomeTypeId:2,
      annualIncome:3232,
      name:null, 
      displayName:null
    }]
  },
  business: {},
  employmentHistory: {},
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
  test("Should change street Address", async () => {
    const { getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmployerAddress />
      </Store.Provider>
    );

    let streetAddress;
    await waitFor(() => {
      streetAddress = getAllByTestId("street_address");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(streetAddress[1], { target: { value: "Street 1" } });
    });
    await waitFor(() => {
      expect(streetAddress[1]).toHaveDisplayValue("Street 1");
    });
  });

  test("Should change unit field", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmployerAddress />
      </Store.Provider>
    );

    let unit;
    await waitFor(() => {
      unit = getByTestId("Unit");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(unit, { target: { value: "unit 123" } });
    });
    await waitFor(() => {
      expect(unit).toHaveDisplayValue("unit 123");
    });
  });

  test("Should change city field", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmployerAddress />
      </Store.Provider>
    );

    let city;
    await waitFor(() => {
      city = getByTestId("City");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(city, { target: { value: "Austin" } });
    });
    await waitFor(() => {
      expect(city).toHaveDisplayValue("Austin");
    });
  });

  test("Should change state field", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmployerAddress />
      </Store.Provider>
    );

    let stateName;
    let stateTxt;
    await waitFor(() => {
      stateName = getByTestId("state");
      stateTxt = stateName.children[0].children[0];
      fireEvent.change(stateTxt, { target: { value: "Texas" } });
    });
    await waitFor(() => {
      expect(stateTxt).toHaveDisplayValue("Texas");
    });
  });

  test("Should change zip code field", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmployerAddress />
      </Store.Provider>
    );

    let zipCode;
    await waitFor(() => {
      zipCode = getByTestId("Zip-Code");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(zipCode, { target: { value: "78717" } });
    });
    await waitFor(() => {
      expect(zipCode).toHaveDisplayValue("78717");
    });
  });

  test("Should change country field", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmployerAddress />
      </Store.Provider>
    );

    let country;
    let countryTxt;
    await waitFor(() => {
      country = getByTestId("country");
      // expect(resetPassHeader).toBeInTheDocument();
      countryTxt = country.children[0].children[0];
      fireEvent.change(countryTxt, { target: { value: "United States" } });
    });
    await waitFor(() => {
      expect(countryTxt).toHaveDisplayValue("United States");
    });
  });

  test("Should show errors on empty form", async () => {
    const { getByTestId } = render(<EmployerAddress />);

    let SubmitBtn;
    await waitFor(() => {
      SubmitBtn = getByTestId("employer-address-next");
      //
      fireEvent.click(SubmitBtn);
    });
    await waitFor(() => {
      expect(getByTestId("street_address-error")).toBeInTheDocument();
      expect(getByTestId("city-error")).toBeInTheDocument();
      expect(getByTestId("state-error")).toBeInTheDocument();
      expect(getByTestId("zip_code-error")).toBeInTheDocument();
    });
  });

  test("Should submit form if all fields are filled", async () => {
    const { getByTestId, getAllByTestId } = render(<EmployerAddress />);
    await waitFor(() => {
      fireEvent.change(getAllByTestId("street_address")[1], {
        target: { value: "Street 1" },
      });

      fireEvent.change(getByTestId("City"), { target: { value: "Austin" } });
      let stateName = getByTestId("state");
      let stateTxt = stateName.children[0].children[0];
      fireEvent.change(stateTxt, { target: { value: "Texas" } });

      fireEvent.change(getByTestId("Zip-Code"), { target: { value: "78717" } });

      let country = getByTestId("country");
      // expect(resetPassHeader).toBeInTheDocument();
      let countryTxt:Element = country?.children[0]?.children[0];
      fireEvent.change(countryTxt, { target: { value: "United States" } });
    });
    let SubmitBtn;
    await waitFor(() => {
      SubmitBtn = getByTestId("employer-address-next");
      //
      fireEvent.click(SubmitBtn);
    });
  });

});
