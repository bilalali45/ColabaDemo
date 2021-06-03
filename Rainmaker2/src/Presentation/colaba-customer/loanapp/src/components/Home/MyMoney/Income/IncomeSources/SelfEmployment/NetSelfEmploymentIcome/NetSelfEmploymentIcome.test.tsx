import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  render,
  waitFor,
} from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { MockEnvConfig } from "../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../../../../store/store";
import { NetSelfEmploymentIcome } from "./NetSelfEmploymentIcome";

// jest.mock('../../lib/localStorage');
jest.mock("../../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../../store/actions/EmploymentActions");
jest.mock('../../../../../../../store/actions/SelfEmploymentActions');

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
      incomeTypeId:null
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
  NavigationHandler.getNavigationStateAsString =  jest.fn(()=> "")
  NavigationHandler.navigateToPath = jest.fn(()=>{})
});

describe("Previous Employment Income ", () => {
  test("Should change Annual Amount", async () => {
    const { getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
        <NetSelfEmploymentIcome selfIncome={{jobTitle:''}}  updateFormValuesOnChange={()=>{}} />
      </Store.Provider>
    );

    let annualIncome:HTMLElement[];
    await waitFor(() => {
        annualIncome = getAllByTestId("annualIncome");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(annualIncome[1], { target: { value: "1234" } })
      
    });
    await waitFor(() => {
        expect(annualIncome[1]).toHaveDisplayValue(["1,234"]);
    })
  });

  

  test("Should show errors on empty form", async () => {
    const { getByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
        <NetSelfEmploymentIcome selfIncome={{jobTitle:''}} updateFormValuesOnChange={()=>{}} />
      </Store.Provider>
    );

    let SubmitBtn:HTMLElement;
    await waitFor(() => {
        SubmitBtn = getByTestId("self-emp-net-income");
    //   
    fireEvent.click(SubmitBtn);
      
    });
    await waitFor(() => {
        expect(getByTestId("annualIncome-error")).toBeInTheDocument();
    })
  });

  test("Should submit form if all fields are filled", async () => {
    const { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
        <NetSelfEmploymentIcome selfIncome={{jobTitle:''}} updateFormValuesOnChange={()=>{}}  />
      </Store.Provider>
    );
    await waitFor(() => {
      fireEvent.change(getAllByTestId("annualIncome")[1], { target: { value: "2323123" } })

    })
    let SubmitBtn:HTMLElement;
    await waitFor(() => {
        SubmitBtn = getByTestId("self-emp-net-income");
    //   
    fireEvent.click(SubmitBtn);
      
    });
  });


});
