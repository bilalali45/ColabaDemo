import React from "react";
import {
  render,
  waitFor,
} from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { NavigationHandler } from "../../../../../../Utilities/Navigation/NavigationHandler";
import { MockSessionStorage } from "../../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../../store/store";
import { MockEnvConfig } from "../../../../../../test_utilities/EnvConfigMock";
import { SelfEmployment } from "./SelfEmployment";

jest.mock("lodash/isEmpty")
jest.mock("../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../store/actions/SelfEmploymentActions");

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

describe("Employment Pop up", () => {
  test("Should show employment pop up ", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
          <MemoryRouter initialEntries={['/']}>
            <SelfEmployment />
          </MemoryRouter>
      </Store.Provider>
    );

    
    await waitFor(() => {
        
    });
  });

});
