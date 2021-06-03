import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  getByTestId,
  render,
  waitFor,
} from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { CurrentResidenceDetails } from "./CurrentResidenceDetails";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../store/store";

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
    myPropertyInfo:{
      primaryPropertyTypeId:2
  },
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

describe("Current Property Value", () => {
  test("Should change Property Val", async () => {
    const { getByTestId } = render(
      
        <CurrentResidenceDetails />
     
    );

    let prop_val: HTMLElement;
    await waitFor(() => {
      prop_val = getByTestId("prop_val-input");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(prop_val, { target: { value: "43243" } });
    });
    await waitFor(() => {
      expect(prop_val).toHaveDisplayValue(["43,243"]);
    });
  });

  test("Should change owner dues", async () => {
    const { getByTestId } = render(
     
        <CurrentResidenceDetails />
     
    );

    let prop_dues: HTMLElement;
    await waitFor(() => {
      prop_dues = getByTestId("prop_dues-input");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.change(prop_dues, { target: { value: "432423" } });
    });
    await waitFor(() => {
      expect(prop_dues).toHaveDisplayValue(["432,423"]);
    });
  });

  test("Should change selling option ", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <CurrentResidenceDetails />
      </Store.Provider>
    );

    let selling_yes: HTMLElement;
    await waitFor(() => {
      selling_yes = getByTestId("selling_yes");
      // expect(resetPassHeader).toBeInTheDocument();
      fireEvent.click(selling_yes);
    });
  });

  test("Should submit form if all fields are filled", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <CurrentResidenceDetails />
      </Store.Provider>
    );
    await waitFor(() => {
      // fireEvent.change(getAllByTestId("prop_val")[1], {
      //   target: { value: "34234" },
      // });

      // fireEvent.change(getAllByTestId("prop_dues")[1], {
      //   target: { value: "76876" },
      // });

      // fireEvent.click(getByTestId("selling_yes"));
    });
    let SubmitBtn;
    await waitFor(() => {
      SubmitBtn = getByTestId("prop_val_save");
      //
      fireEvent.click(SubmitBtn);
    });
  });
});
