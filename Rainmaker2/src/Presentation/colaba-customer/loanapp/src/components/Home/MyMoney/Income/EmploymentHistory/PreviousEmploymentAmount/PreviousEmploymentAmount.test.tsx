import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  render,
  waitFor,
} from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { MockEnvConfig } from "../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../test_utilities/SessionStoreMock";
import { PreviousEmploymentAmount } from "./PreviousEmploymentAmount";
import { Store } from "../../../../../../store/store";
import { NavigationHandler } from "../../../../../../Utilities/Navigation/NavigationHandler";

// jest.mock('../../lib/localStorage');
jest.mock("../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../store/actions/EmploymentActions");
jest.mock('../../../../../../store/actions/EmploymentHistoryActions');

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
      previousEmployerInfo :{
          
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
    previousEmployerAddress: {
        streetAddress: "Defence Phase VI",
        unitNo: "4th Floor",
        cityId: 1,
        cityName: "Thatta",
        countryId: 1,
        stateId: 1,
        stateName: "Sindh",
        zipCode: "73130"
    },
    previousEmploymentIncome:{
        isPaidByMonthlySalary: false,
        hourlyRate: null,
        hoursPerWeek: null,
        employerAnnualSalary:""
    }
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
    const { getByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
        <PreviousEmploymentAmount />
      </Store.Provider>
    );

    let annualIncome:HTMLElement;
    await waitFor(() => {
        annualIncome = getByTestId("net-annual-income");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(annualIncome, { target: { value: "1234" } })
      
    });
    await waitFor(() => {
        expect(annualIncome).toHaveDisplayValue(["1,234"]);
    })
  });

  

  test("Should show errors on empty form", async () => {
    const { getByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
        <PreviousEmploymentAmount />
      </Store.Provider>
    );

    let SubmitBtn:HTMLElement;
    await waitFor(() => {
        SubmitBtn = getByTestId("prev-emp-amt-btn");
    //   
    fireEvent.click(SubmitBtn);
      
    });
    await waitFor(() => {
        expect(getByTestId("netAnnualIncome-error")).toBeInTheDocument();
    })
  });

  test("Should submit form if all fields are filled", async () => {
    const { getByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
        <PreviousEmploymentAmount />
      </Store.Provider>
    );
    await waitFor(() => {
      fireEvent.change(getByTestId("net-annual-income"), { target: { value: "2323123" } })

    })
    let SubmitBtn:HTMLElement;
    await waitFor(() => {
        SubmitBtn = getByTestId("prev-emp-amt-btn");
    //   
    fireEvent.click(SubmitBtn);
      
    });
  });


});
