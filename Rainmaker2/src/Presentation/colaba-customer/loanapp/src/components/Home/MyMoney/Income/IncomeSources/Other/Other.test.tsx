import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  render,
  waitFor,
  screen,
} from "@testing-library/react";
import { NavigationHandler } from "../../../../../../Utilities/Navigation/NavigationHandler";
import { MockSessionStorage } from "../../../../../../test_utilities/SessionStoreMock";
import { MockEnvConfig } from "../../../../../../test_utilities/EnvConfigMock";
import { MemoryRouter } from "react-router";
import { Other } from "./Other";
import { Store } from "../../../../../../store/store";

jest.mock("../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../store/actions/BusinessActions");
jest.mock("../../../../../../store/actions/IncomeActions");
jest.mock("../../../../../../lib/LocalDB");


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
    },
    commonManager: {},
    employment: {},
    business: {},
    employmentHistory: {
    },
    militaryIncomeManager: {},
    otherIncomeManager: {},
    assetsManager: {},
  };
  const dispatch = jest.fn();


beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
  NavigationHandler.getNavigationStateAsString = jest.fn(() => "");
  NavigationHandler.navigateToPath = jest.fn(() => {});
  NavigationHandler.moveNext = jest.fn(() => {});
});

describe("Other Screen ", () => {
  test("should render Other", async () => {
    const { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
      <MemoryRouter initialEntries={["/"]}>
        <Other />
      </MemoryRouter>
      </Store.Provider>
    );

    await waitFor(() => {});
  });
});
