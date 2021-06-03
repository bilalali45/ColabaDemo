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
import { SelfEmploymentIncome } from "./SelfEmploymentIncome";
import { Store } from "../../../../../../../store/store";

// jest.mock('../../lib/localStorage');
jest.mock("../../../../../../../test_utilities/lodashMock")
jest.mock("../../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../../store/actions/EmploymentActions");
// jest.mock('../../../../../Store/actions/TemplateActions');

const state = {
  leftMenu: {
    navigation:null,
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
    // incomeInfo:{
    //   incomeId:2102,
    //   incomeTypeId:null
    // },
    primaryBorrowerInfo: {
      id: 31450, 
      name: "khalid"
    } 
    
  },
  commonManager: {},
  employment:{
    employerInfo:{
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
    employerAddress:{
      streetAddress: "Defence Phase VI",
      unitNo: "4th Floor",
      cityId: 1,
      cityName: "Thatta",
      countryId: 1,
      stateId: 1,
      stateName: "Sindh",
      zipCode: "73130"
    },
    wayOfIncome:{
      isPaidByMonthlySalary: false,
      hourlyRate: null,
      hoursPerWeek: null,
      employerAnnualSalary:""
    }, 
    additionalIncome:[{
      incomeTypeId:2,
      annualIncome:3232,
      name:null, 
      displayName:null
    }]
  },
  business:{},
  employmentHistory:{},
  militaryIncomeManager: {},
  otherIncomeManager:{},
  assetsManager: {}
};
const dispatch = jest.fn();


beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
  // NavigationHandler.navigation.moveNext = jest.fn(()=>{})
  NavigationHandler.getNavigationStateAsString =  jest.fn(()=> "")
  NavigationHandler.navigateToPath = jest.fn(()=>{})
});

describe("Self Employment Income ", () => {
  test("Should change Business Name", async () => {
    const { getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SelfEmploymentIncome selfIncome={{}} setSelfIncomeFormData={{}} updateFormValuesOnChange={{}} />
      </Store.Provider>
    );

    let businessName:HTMLElement[];
    await waitFor(() => {
        businessName = getAllByTestId("businessName");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(businessName[1], { target: { value: "Business 1" } })
      
    });
    await waitFor(() => {
        expect(businessName[1]).toHaveDisplayValue("Business 1");
    })
  });

  test("Should change Job Title", async () => {
    const { getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SelfEmploymentIncome selfIncome={{}} setSelfIncomeFormData={{}} updateFormValuesOnChange={{}} />
      </Store.Provider>
    );

    let jobTitle:HTMLElement[];
    await waitFor(() => {
        jobTitle = getAllByTestId("jobTitle");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(jobTitle[1], { target: { value: "Software Engineer" } })
      
    });
    await waitFor(() => {
        expect(jobTitle[1]).toHaveDisplayValue("Software Engineer");
    })
  });

  test("Should change Start Date", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SelfEmploymentIncome selfIncome={{}} setSelfIncomeFormData={{}} updateFormValuesOnChange={{}} />
      </Store.Provider>
    );

    let startDate:HTMLElement;
    await waitFor(() => {
        startDate = getByTestId("startDate");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(startDate, { target: { value: "11/11/2019" } })
      
    });
    await waitFor(() => {
        expect(startDate).toHaveDisplayValue("11/11/2019");
    })
  });
  

  test("Should change Business Phone number", async () => {
    const { getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SelfEmploymentIncome selfIncome={{}} setSelfIncomeFormData={{}} updateFormValuesOnChange={{}} />
      </Store.Provider>
    );

    let businessPhone:HTMLElement[];
    await waitFor(() => {
        businessPhone = getAllByTestId("businessPhone");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(businessPhone[1], { target: { value: "123456789" } })
      
    });
    await waitFor(() => {
        expect(businessPhone[1]).toHaveDisplayValue("(123) 456-789");
    })
  });

  test("Should show error on wrong Business Phone number", async () => {
    const { getAllByTestId, getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SelfEmploymentIncome selfIncome={{}} setSelfIncomeFormData={{}} updateFormValuesOnChange={{}} />
      </Store.Provider>
    );

    let businessPhone:HTMLElement[];
    await waitFor(() => {
        businessPhone = getAllByTestId("businessPhone");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(businessPhone[1], { target: { value: "(111) 111-111" } })
      
    });
    
    
    let SubmitBtn:HTMLElement;
    await waitFor(() => {
        SubmitBtn = getByTestId("self_employment_next");
    //   
    fireEvent.click(SubmitBtn);
      
    });

    await waitFor(() => {
        expect(getByTestId("businessPhone-error")).toBeInTheDocument();
        expect(getByTestId("businessPhone-error")).toHaveTextContent("111111111 is not a valid phone number");
    })

  });


  test("Should submit form if all fields are filled", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <SelfEmploymentIncome selfIncome={{}} setSelfIncomeFormData={{}} updateFormValuesOnChange={{}} />
      </Store.Provider>
    );
    await waitFor(() => {
      fireEvent.change(getAllByTestId("businessName")[1], { target: { value: "Business 1" } })
     
        fireEvent.change(getAllByTestId("jobTitle")[1], { target: { value: "Software Engineer" } })
        
        fireEvent.change(getByTestId("startDate"), { target: { value: "11/11/2019" } })
       
        fireEvent.change(getAllByTestId("businessPhone")[1], { target: { value: "123456789" } })
    })
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("self_employment_next");
    //   
    fireEvent.click(SubmitBtn);
      
    });
  });


});
