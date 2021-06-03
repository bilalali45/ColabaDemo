import React from "react";
import {
  fireEvent,
  render,
  waitFor,
} from "@testing-library/react";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../store/store";
import { SecondCurrentResidenceMortgageDetails } from "./SecondCurrentResidenceMortgageDetails";

// jest.mock('../../lib/localStorage');
jest.mock("../../../../test_utilities/lodashMock");
jest.mock("../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../store/actions/MyPropertyActions");
jest.mock("../../../../lib/LocalDB")

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
    // incomeInfo:{
    //   incomeId:2102,
    //   incomeTypeId:null
    // },
    primaryBorrowerInfo: {
      id: 31450,
      name: "khalid",
    },
  },
  commonManager: {},
  employment: {},
  business: {},
  employmentHistory: {},
  militaryIncomeManager: {},
  otherIncomeManager: {},
  assetsManager: {},
};
const dispatch = jest.fn();

beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
  // NavigationHandler.navigation.moveNext = jest.fn(()=>{})
  NavigationHandler.getNavigationStateAsString = jest.fn(() => "");
  NavigationHandler.navigateToPath = jest.fn(() => {});
  NavigationHandler.moveNext = jest.fn(() => {});
});

describe("First Current Residence Mortgage", () => {
  test("Should change first payment", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SecondCurrentResidenceMortgageDetails />
      </Store.Provider>
    );

    let second_Payment: HTMLElement[];
    await waitFor(() => {
        second_Payment = getAllByTestId("second_Payment");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(second_Payment[1], { target: { value: "100" } });
    });
    await waitFor(() => {
      expect(second_Payment[1]).toHaveDisplayValue(["100"]);
    });
  });

  test("Should change first payment Balance", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SecondCurrentResidenceMortgageDetails />
      </Store.Provider>
    );

    let second_pay_bal: HTMLElement[];
    await waitFor(() => {
        second_pay_bal = getAllByTestId("second_pay_bal");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(second_pay_bal[1], { target: { value: "400" } });
    });
    await waitFor(() => {
      expect(second_pay_bal[1]).toHaveDisplayValue(["400"]);
    });
  });

  test("Should change has HELOC", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SecondCurrentResidenceMortgageDetails />
      </Store.Provider>
    );

    let mortgagte_no: HTMLElement;
    await waitFor(() => {
      mortgagte_no = getByTestId("heloc");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.click(mortgagte_no);
    });

    let credit_limit: HTMLElement[];
    await waitFor(() => {
      credit_limit = getAllByTestId("credit_limit");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(credit_limit[1], { target: { value: "432423" } });
    });
    await waitFor(() => {
      expect(credit_limit[1]).toHaveDisplayValue(["432,423"]);
    });
  });

  test("Should submit form if all fields are filled", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SecondCurrentResidenceMortgageDetails />
      </Store.Provider>
    );
    
    let SubmitBtn;
    await waitFor(() => {
      SubmitBtn = getByTestId("second_mortgage_save");
      //
      fireEvent.click(SubmitBtn);
    });
  });
});
