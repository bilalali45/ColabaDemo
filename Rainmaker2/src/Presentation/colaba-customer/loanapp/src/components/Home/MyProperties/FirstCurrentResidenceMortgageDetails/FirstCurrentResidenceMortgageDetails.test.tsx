import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  render,
  waitFor,
} from "@testing-library/react";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../store/store";
import { FirstCurrentResidenceMortgageDetails } from "./FirstCurrentResidenceMortgageDetails";

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
        <FirstCurrentResidenceMortgageDetails address={""}/>
      </Store.Provider>
    );

    let first_Payment: HTMLElement[];
    await waitFor(() => {
      first_Payment = getAllByTestId("first_Payment");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(first_Payment[1], { target: { value: "100" } });
    });
    await waitFor(() => {
      expect(first_Payment[1]).toHaveDisplayValue(["100"]);
    });
  });

  test("Should change first payment Balance", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <FirstCurrentResidenceMortgageDetails address={""}/>
      </Store.Provider>
    );

    let first_pay_bal: HTMLElement[];
    await waitFor(() => {
      first_pay_bal = getAllByTestId("first_pay_bal");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(first_pay_bal[1], { target: { value: "400" } });
    });
    await waitFor(() => {
      expect(first_pay_bal[1]).toHaveDisplayValue(["400"]);
    });
  });

  test("Should change Property Tax", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <FirstCurrentResidenceMortgageDetails address={""}/>
      </Store.Provider>
    );

    let prop_tax: HTMLElement[];
    await waitFor(() => {
      prop_tax = getAllByTestId("prop_tax");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(prop_tax[1], { target: { value: "200" } });
    });
    await waitFor(() => {
      expect(prop_tax[1]).toHaveDisplayValue(["200"]);
    });
  });

  test("Should change tax included in payment", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <FirstCurrentResidenceMortgageDetails address={""}/>
      </Store.Provider>
    );

    let tax_inc_in_pay: HTMLElement;
    await waitFor(() => {
      tax_inc_in_pay = getByTestId("tax_inc_in_pay");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.click(tax_inc_in_pay);
    });
  });

  test("Should change Property Insurance", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <FirstCurrentResidenceMortgageDetails address={""}/>
      </Store.Provider>
    );

    let prop_insurance: HTMLElement[];
    await waitFor(() => {
      prop_insurance = getAllByTestId("prop_insurance");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(prop_insurance[1], { target: { value: "300" } });
    });
    await waitFor(() => {
      expect(prop_insurance[1]).toHaveDisplayValue(["300"]);
    });
  });

  test("Should change Insurance included in payment", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <FirstCurrentResidenceMortgageDetails address={""} />
      </Store.Provider>
    );

    let ins_inc_in_pay: HTMLElement;
    await waitFor(() => {
      ins_inc_in_pay = getByTestId("ins_inc_in_pay");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.click(ins_inc_in_pay);
    });
  });

  test("Should change has HELOC", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <FirstCurrentResidenceMortgageDetails address={""}/>
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
        <FirstCurrentResidenceMortgageDetails address={""}/>
      </Store.Provider>
    );
    
    let SubmitBtn;
    await waitFor(() => {
      SubmitBtn = getByTestId("first_mortgage_save");
      //
      fireEvent.click(SubmitBtn);
    });
  });
});
