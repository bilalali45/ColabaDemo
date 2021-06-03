import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  render,
  waitFor,
  screen
} from "@testing-library/react";
import { MockEnvConfig } from "../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../test_utilities/SessionStoreMock";
import {Military} from './Military';
import { Store } from "../../../../../../store/store";
import { MemoryRouter } from "react-router-dom";


jest.mock("../../../../../../store/actions/MilitaryIncomeActions");
jest.mock("../../../../../../store/actions/BorrowerActions");
jest.mock("../../../../../../store/actions/BusinessActions");
jest.mock("../../../../../../store/actions/EmploymentActions");
jest.mock("../../../../../../store/actions/EmploymentHistoryActions");
jest.mock("../../../../../../store/actions/GettingStartedActions");
jest.mock("../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../store/actions/IncomeActions");
jest.mock("../../../../../../store/actions/IncomeReviewActions");
jest.mock("../../../../../../store/actions/MaritalStatusActions");
jest.mock("../../../../../../store/actions/MyNewMortgageActions");
jest.mock("../../../../../../store/actions/MyNewPropertyAddressActions");
jest.mock("../../../../../../store/actions/RetirementIncomeActions");
jest.mock("../../../../../../store/actions/SelfEmploymentActions");




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
    incomeInfo: {
      incomeId: 2167,
      incomeTypeId: null
    },
    states: [

    ],
    countries: [ ],
  },
  commonManager: {},
  employment: {},
  business: {},
  employmentHistory: {},
  militaryIncomeManager: {
    militaryEmployer :{
        EmployerName:"US Army",
        JobTitle:"Major",
        startDate: "11/11/2019",
        YearsInProfession: 5,
    }
  },
  otherIncomeManager:{},
  assetsManager: {}
};
const dispatch = jest.fn();

beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
});


describe("Military Section ", () => {

  test("Get Military income info and dispatch", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <MemoryRouter initialEntries={['/']}>
           <Military />   
           </MemoryRouter>
      </Store.Provider>
      
    );

    await waitFor(() => {
      

    });

   });
  
});


