import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  render,
  waitFor,
} from "@testing-library/react";
import { MemoryRouter, useLocation } from "react-router-dom";
import { EmploymentHistoryDetails } from "./EmploymentHistoryDetails";
import { NavigationHandler } from "../../../../../../Utilities/Navigation/NavigationHandler";
import { MockSessionStorage } from "../../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../../store/store";
import { MockEnvConfig } from "../../../../../../test_utilities/EnvConfigMock";

// jest.mock('../../lib/localStorage');
jest.mock("lodash/isEmpty")
jest.mock("../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../store/actions/EmploymentHistoryActions");
jest.mock("../../../../../../store/actions/EmploymentActions");

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
    incomeInfo:{
      incomeId:2102,
      incomeTypeId:1
    },
    primaryBorrowerInfo: {
      id: 31450, 
      name: "khalid"
    } 
    
  },
  commonManager: {},
  employment:{},
  business:{},
  employmentHistory:{
  },
  militaryIncomeManager: {},
  otherIncomeManager:{},
  assetsManager: {}
};
const dispatch = jest.fn();


beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
  NavigationHandler.enableFeature = jest.fn((name)=>{});
  NavigationHandler.disableFeature = jest.fn((name)=>{});
  NavigationHandler.navigateToPath = jest.fn((path)=>{})
});

describe("Employment History Details ", () => {
  test("Should show Previous employment pop up ", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
          <MemoryRouter initialEntries={['/']}>
            <EmploymentHistoryDetails />
          </MemoryRouter>
      </Store.Provider>
    );

    let popUp:HTMLElement;
    await waitFor(() => {
        popUp = getByTestId("pop-modal");
      expect(popUp).toBeInTheDocument();
    });
  });

});
