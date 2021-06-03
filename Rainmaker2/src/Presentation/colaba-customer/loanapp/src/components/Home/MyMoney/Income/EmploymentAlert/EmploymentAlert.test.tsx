import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  render,
  waitFor,
} from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { EmploymentAlert } from "./EmploymentAlert";
import { MockEnvConfig } from "../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../store/store";
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";
import EmploymentHistoryActions from "../../../../../store/actions/__mocks__/EmploymentHistoryActions";
import { APIResponse } from "../../../../../Entities/Models/APIResponse";

// jest.mock('../../lib/localStorage');
jest.mock("lodash/isEmpty")
jest.mock("../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../store/actions/EmploymentHistoryActions");
// jest.mock('../../../../../Store/actions/TemplateActions');

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
  NavigationHandler.enableFeature = jest.fn((name)=>{});
  NavigationHandler.disableFeature = jest.fn((name)=>{});
  NavigationHandler.navigateToPath = jest.fn((path)=>{})
});

describe("Employment Alert ", () => {
  test("Should show Employment Alert text", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentAlert />
      </Store.Provider>
    );

    let alertText:HTMLElement;
    await waitFor(() => {
        alertText = getByTestId("alert-text");
      expect(alertText).toBeInTheDocument();
    });
  });

  test("Should click continue", async () => {
    
    
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentAlert />
      </Store.Provider>
    );
    let alertBtn:HTMLElement;
    await waitFor(() => {
        alertBtn = getByTestId("alert-continue-btn");
      expect(alertBtn).toBeInTheDocument();
      fireEvent.click(alertBtn)
    });
  });
});
