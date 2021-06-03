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
import { PreviousEmployment } from "./PreviousEmployment";
import { Store } from "../../../../../../store/store";

// jest.mock('../../lib/localStorage');
jest.mock("lodash/isEmpty")
jest.mock("../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../store/actions/EmploymentActions");
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
});

describe("Employment Income ", () => {
  test("Should change Employer Name", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <PreviousEmployment />
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
        <PreviousEmployment />
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

  test("Should change End Date", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <PreviousEmployment />
      </Store.Provider>
    );

    let endDate:HTMLElement;
    await waitFor(() => {
        endDate = getByTestId("endDate");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(endDate, { target: { value: "11/11/2019" } })
      
    });
    await waitFor(() => {
        expect(endDate).toHaveDisplayValue("11/11/2019");
    })
  });

 
  test("Should show ownership percentage on yes", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <PreviousEmployment />
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
  //       <PreviousEmployment />
  //     </Store.Provider>
  //   );

  //   let SubmitBtn:HTMLElement;
  //   await waitFor(() => {
  //       SubmitBtn = getByTestId("previous-employer-info-next");
  //   //   
  //   fireEvent.click(SubmitBtn);
      
  //   });
  //   await waitFor(() => {
  //       expect(getByTestId("EmployerName-error")).toBeInTheDocument();
  //       expect(getByTestId("JobTitle-error")).toBeInTheDocument();
  //       expect(getByTestId("startDate-error")).toBeInTheDocument();
  //       expect(getByTestId("endDate-error")).toBeInTheDocument();
  //       expect(getByTestId("ownershipInterest-error")).toBeInTheDocument();
  //   })
  // });

  test("Should submit form if all fields are filled", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <PreviousEmployment />
      </Store.Provider>
    );
    
    await waitFor(() => {
      fireEvent.change(getByTestId("employer-name"), { target: { value: "Employer 1" } })
     
        fireEvent.change(getByTestId("job-title"), { target: { value: "Software Engineer" } })
        
        fireEvent.change(getByTestId("startDate"), { target: { value: "11/11/2019" } })
        fireEvent.change(getByTestId("endDate"), { target: { value: "11/11/2019" } })
    fireEvent.click(getByTestId("ownership-interest-no"))

    })
    let SubmitBtn:HTMLElement;
    await waitFor(() => {
        SubmitBtn = getByTestId("previous-employer-info-next");
    //   
    fireEvent.click(SubmitBtn);
      
    });
  });


});
