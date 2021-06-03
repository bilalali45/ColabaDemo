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

import { NavigationHandler } from "../../../../../../../Utilities/Navigation/NavigationHandler";
import { ModeOfEmploymentIncomePayment } from "./ModeOfEmploymentIncomePayment";

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

describe("Mode oF Employment Income Payment  ", () => {
  test("Should select salary ", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <ModeOfEmploymentIncomePayment />
      </Store.Provider>
    );

    let modeOfPaymentSalary:HTMLElement;
    await waitFor(() => {
        modeOfPaymentSalary = getByTestId("mode-of-payment-salary");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.click(modeOfPaymentSalary)
      
    });
    let annualBaseSalary:HTMLElement;
    await waitFor(()=>{
        annualBaseSalary = getByTestId("annual-base-salary");
        expect(annualBaseSalary).toBeInTheDocument();

        fireEvent.change(annualBaseSalary, { target: { value: "12344" } })
    })
    await waitFor(() => {
        expect(annualBaseSalary).toHaveDisplayValue(["12,344"]);
    })
  });

  test("Should select hourly  ", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <ModeOfEmploymentIncomePayment />
      </Store.Provider>
    );

    let modeOfPaymentHourly:HTMLElement;
    await waitFor(() => {
        modeOfPaymentHourly = getByTestId("mode-of-payment-hourly");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.click(modeOfPaymentHourly)
      
    });
    let hourlyRate:HTMLElement;
    await waitFor(()=>{
        hourlyRate = getByTestId("hourly-rate");
        expect(hourlyRate).toBeInTheDocument();

        fireEvent.change(hourlyRate, { target: { value: "12344" } })
    })
    await waitFor(() => {
        expect(hourlyRate).toHaveDisplayValue(["12,344"]);
    })

    let hoursPerWeek:HTMLElement;
    await waitFor(()=>{
        hoursPerWeek = getByTestId("hours-per-week");
        expect(hoursPerWeek).toBeInTheDocument();

        fireEvent.change(hoursPerWeek, { target: { value: "32" } })
    })
    await waitFor(() => {
        expect(hoursPerWeek).toHaveDisplayValue(["32"]);
    })
  });
//  

  

  // test("Should show errors on empty form", async () => {
  //   const { getByTestId } = render(
  //     <Store.Provider value={{ state, dispatch }}>
  //       <ModeOfEmploymentIncomePayment />
  //     </Store.Provider>
  //   );

  //   let SubmitBtn:HTMLElement;
  //   await waitFor(() => {
  //       SubmitBtn = getByTestId("mode-of-payment-nxt");
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
        <ModeOfEmploymentIncomePayment />
      </Store.Provider>
    );
    await waitFor(() => {
      fireEvent.click(getByTestId("mode-of-payment-hourly"))
     
        fireEvent.change(getByTestId("hourly-rate"), { target: { value: "1234" } })
        
        fireEvent.change(getByTestId("hours-per-week"), { target: { value: "32" } })

    })
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("mode-of-payment-nxt");
    //   
    fireEvent.click(SubmitBtn);
      
    });
  });

  test("Should submit form for salary if all fields are filled", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <ModeOfEmploymentIncomePayment />
      </Store.Provider>
    );
    await waitFor(() => {
      fireEvent.click(getByTestId("mode-of-payment-salary"))
     
        fireEvent.change(getByTestId("annual-base-salary"), { target: { value: "1234" } })

    })
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("mode-of-payment-nxt");
    //   
    fireEvent.click(SubmitBtn);
      
    });
  });

});
