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
import { AdditionalIncome } from "./AdditionalIncome";

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
  NavigationHandler.enableFeature = jest.fn(()=>{})
    NavigationHandler.disableFeature = jest.fn(()=>{})
    NavigationHandler.moveNext = jest.fn(()=>{})
    NavigationHandler.isFieldVisible = jest.fn(()=>true)

});

describe("Employment Income ", () => {
  test("Should select bonus from Additionl Income ", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <AdditionalIncome />
      </Store.Provider>
    );

    let Bonus:HTMLElement;
    await waitFor(() => {
        Bonus = getByTestId("Bonus");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.click(Bonus)
      
    });
    let annualBonusInc:HTMLElement[]
    await waitFor(() => {
        annualBonusInc = getAllByTestId("annualBonusInc");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(annualBonusInc[1], { target: { value: "12344" } })
      
    });
    await waitFor(() => {
        expect(annualBonusInc[1]).toHaveDisplayValue(["12,344"]);
    })
   
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("additional-income-submit");
    //   
    fireEvent.click(SubmitBtn);
    })
  });

  test("Should not add alpha numeric ", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <AdditionalIncome />
      </Store.Provider>
    );

    let Bonus:HTMLElement;
    await waitFor(() => {
        Bonus = getByTestId("Bonus");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.click(Bonus)
      
    });
    let annualBonusInc:HTMLElement[]
    await waitFor(() => {
        annualBonusInc = getAllByTestId("annualBonusInc");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(annualBonusInc[1], { target: { value: "dsfsd" } })
      
    });
    await waitFor(() => {
        expect(annualBonusInc[1]).toHaveDisplayValue([""]);
    })
   
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("additional-income-submit");
    //   
    fireEvent.click(SubmitBtn);
    })
  });


  test("Should select annual CommissionInc from Additionl Income ", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <AdditionalIncome />
      </Store.Provider>
    );

    let Bonus:HTMLElement;
    await waitFor(() => {
        Bonus = getByTestId("Commission");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.click(Bonus)
      
    });
    let annualCommissionInc:HTMLElement[]
    await waitFor(() => {
        annualCommissionInc = getAllByTestId("annualCommissionInc");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(annualCommissionInc[1], { target: { value: "12344" } })
      
    });
    await waitFor(() => {
        expect(annualCommissionInc[1]).toHaveDisplayValue(["12,344"]);
    })
   
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("additional-income-submit");
    //   
    fireEvent.click(SubmitBtn);
    })
  });

  test("Should select Overtime from Additionl Income ", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <AdditionalIncome />
      </Store.Provider>
    );

    let Overtime:HTMLElement;
    await waitFor(() => {
        Overtime = getByTestId("Overtime");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.click(Overtime)
      
    });
    let annualOvertimeInc:HTMLElement
    await waitFor(() => {
        annualOvertimeInc = getAllByTestId("annualOvertimeInc")[1];
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(annualOvertimeInc, { target: { value: "12344" } })
      
    });
    await waitFor(() => {
        expect(annualOvertimeInc).toHaveDisplayValue(["12,344"]);
    })
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("additional-income-submit");
    //   
    fireEvent.click(SubmitBtn);
    })
  });
  
  test("Should submit form if all fields are filled", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <AdditionalIncome />
      </Store.Provider>
    );
    await waitFor(() => {
    })
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("additional-income-submit");
    //   
    fireEvent.click(SubmitBtn);
      
    });
  });


});
