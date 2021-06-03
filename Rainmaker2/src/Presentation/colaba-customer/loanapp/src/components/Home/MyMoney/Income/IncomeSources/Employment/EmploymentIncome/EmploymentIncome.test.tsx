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
import { Store } from "../../../../../../../store/store";
import { EmploymentIncome } from "./EmploymentIncome";

import { NavigationHandler } from "../../../../../../../Utilities/Navigation/NavigationHandler";

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

describe("Employment Income ", () => {
  test("Should change Employer Name", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentIncome />
      </Store.Provider>
    );

    let employerName:HTMLElement;
    await waitFor(() => {
        employerName = getByTestId("employer-name");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(employerName, { target: { value: "Employer 1" } })
      
    });
    await waitFor(() => {
        expect(employerName).toHaveDisplayValue("Employer 1");
    })
  });

  test("Should change Job Title", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentIncome />
      </Store.Provider>
    );

    let jobTitle:HTMLElement;
    await waitFor(() => {
        jobTitle = getByTestId("job-title");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(jobTitle, { target: { value: "Software Engineer" } })
      
    });
    await waitFor(() => {
        expect(jobTitle).toHaveDisplayValue("Software Engineer");
    })
  });

  test("Should change Start Date", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentIncome />
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
  
  test("Should change Years in Profession", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentIncome />
      </Store.Provider>
    );

    let YearsInProfession:HTMLElement;
    await waitFor(() => {
        YearsInProfession = getByTestId("years-in-profession");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(YearsInProfession, { target: { value: "11" } })
      
    });
    await waitFor(() => {
        expect(YearsInProfession).toHaveDisplayValue("11");
    })
  });

  test("Should change Employer Phone number", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentIncome />
      </Store.Provider>
    );

    let EmployerPhoneNumber:HTMLElement;
    await waitFor(() => {
        EmployerPhoneNumber = getByTestId("employer-phone-number");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(EmployerPhoneNumber, { target: { value: "123456789" } })
      
    });
    await waitFor(() => {
        expect(EmployerPhoneNumber).toHaveDisplayValue("(123) 456-789");
    })
  });

  test("Should show error on wrong Employer Phone number", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentIncome />
      </Store.Provider>
    );

    let EmployerPhoneNumber:HTMLElement;
    await waitFor(() => {
        EmployerPhoneNumber = getByTestId("employer-phone-number");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(EmployerPhoneNumber, { target: { value: "(111) 111-111" } })
      
    });
    
    
    let SubmitBtn:HTMLElement;
    await waitFor(() => {
        SubmitBtn = getByTestId("employer-info-next");
    //   
    fireEvent.click(SubmitBtn);
      
    });

    await waitFor(() => {
        expect(getByTestId("EmployerPhoneNumber-error")).toBeInTheDocument();
        expect(getByTestId("EmployerPhoneNumber-error")).toHaveTextContent("Please enter US Phone Number only");
    })

    

  });


  test("Should change Employed Status", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentIncome />
      </Store.Provider>
    );

    let employedStatus:HTMLElement;
    await waitFor(() => {
        employedStatus = getByTestId("employed-yes");
    //   
    fireEvent.click(employedStatus);
      
    });
    
  });

  test("Should show ownership percentage on yes", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentIncome />
      </Store.Provider>
    );

    let ownershipInterest:HTMLElement;
    await waitFor(() => {
        ownershipInterest = getByTestId("ownership-interest-yes");
    //   
    fireEvent.click(ownershipInterest);
      
    });

    let ownershipPercentage:HTMLElement;
    await waitFor(() => {
        ownershipPercentage = getByTestId("ownership-percentage");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(ownershipPercentage, { target: { value: "13" } })
      
    });
    await waitFor(() => {
        expect(ownershipPercentage).toHaveDisplayValue("13");
    })

  });

  // test("Should show errors on empty form", async () => {
  //   const { getByTestId } = render(
  //     <Store.Provider value={{ state, dispatch }}>
  //       <EmploymentIncome />
  //     </Store.Provider>
  //   );

  //   let SubmitBtn:HTMLElement;
  //   await waitFor(() => {
  //       SubmitBtn = getByTestId("employer-info-next");
  //   //   
  //   fireEvent.click(SubmitBtn);
      
  //   });
  //   await waitFor(() => {
  //       expect(getByTestId("EmployerName-error")).toBeInTheDocument();
  //       expect(getByTestId("JobTitle-error")).toBeInTheDocument();
  //       expect(getByTestId("startDate-error")).toBeInTheDocument();
  //       expect(getByTestId("YearsInProfession-error")).toBeInTheDocument();
  //       expect(getByTestId("EmployerPhoneNumber-error")).toBeInTheDocument();
  //       expect(getByTestId("employed-error")).toBeInTheDocument();
  //       expect(getByTestId("ownershipInterest-error")).toBeInTheDocument();
  //   })
  // });

  test("Should submit form if all fields are filled", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <EmploymentIncome />
      </Store.Provider>
    );
    await waitFor(() => {
      fireEvent.change(getByTestId("employer-name"), { target: { value: "Employer 1" } })
     
        fireEvent.change(getByTestId("job-title"), { target: { value: "Software Engineer" } })
        
        fireEvent.change(getByTestId("startDate"), { target: { value: "11/11/2019" } })
       
        fireEvent.change(getByTestId("years-in-profession"), { target: { value: "11" } })
       
        fireEvent.change(getByTestId("employer-phone-number"), { target: { value: "123456789" } })
          
    fireEvent.click(getByTestId("employed-no")); 
    fireEvent.click(getByTestId("ownership-interest-no"))

    })
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("employer-info-next");
    //   
    fireEvent.click(SubmitBtn);
      
    });
  });


});
