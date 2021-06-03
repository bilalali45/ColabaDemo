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
import { FirstCurrentResidenceMortgage } from "./FirstCurrentResidenceMortgage";

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
  assetsManager: {}
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
  test("Should change has Mortgage", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <FirstCurrentResidenceMortgage address={""}/>
      </Store.Provider>
    );

    let mortgagte_no: HTMLElement;
    await waitFor(() => {
        mortgagte_no = getByTestId("mortgage_no");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.click(mortgagte_no)
    });

    let prop_tax: HTMLElement[];
    await waitFor(() => {
        prop_tax = getAllByTestId("prop_tax");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(prop_tax[1], { target: { value: "432423" } });
    });
    await waitFor(() => {
      expect(prop_tax[1]).toHaveDisplayValue(["432,423"]);
    });

    let insurance: HTMLElement[];
    await waitFor(() => {
        insurance = getAllByTestId("insurance");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(insurance[1], { target: { value: "432423" } });
    });
    await waitFor(() => {
      expect(insurance[1]).toHaveDisplayValue(["432,423"]);
    });
})
   

  

  test("Should submit form if all fields are filled", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <FirstCurrentResidenceMortgage address={""} />
      </Store.Provider>
    );
    await waitFor(() => {
     
      fireEvent.click(getByTestId("mortgage_yes"));
    });
    let SubmitBtn;
    await waitFor(() => {
      SubmitBtn = getByTestId("have_mortgage_save");
      //
      fireEvent.click(SubmitBtn);
    });
  });
});
