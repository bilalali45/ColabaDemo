import React from "react";
import {
  fireEvent,
  getAllByTestId,
  getByPlaceholderText,
  getByTestId,
  render,
  waitFor,
} from "@testing-library/react";
import { MockEnvConfig } from "../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../../../store/store";
import { ModeOfMilitaryServicePayment } from "./ModeOfMilitaryServicePayment";
import { NavigationHandler } from "../../../../../../../Utilities/Navigation/NavigationHandler";




jest.mock("../../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../../store/actions/MilitaryIncomeActions");
jest.mock("../../../../../../../lib/LocalDB");


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
    }
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
    },
    militaryServiceAddress: {
        street:"Street 1",
        unit: "unit 123",
        city: "Austin", 
        zipCode:"7887",
        countryId: 5,
        countryName: "United States Of America",
        stateId: 4,
        stateName: "Texas",
    },
    militaryPaymentMode: {
      baseSalary: 20000,
      entitlementL: 50000
    }
  },
  otherIncomeManager:{},
  assetsManager: {}
};
const dispatch = jest.fn();


  
beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
  NavigationHandler.enableFeature = jest.fn(()=>{})
    NavigationHandler.disableFeature = jest.fn(()=>{})
    NavigationHandler.moveNext = jest.fn(()=>{})
    NavigationHandler.isFieldVisible = jest.fn(()=>true)
    NavigationHandler.getNavigationStateAsString = jest.fn(()=>"")
    NavigationHandler.navigateToPath= jest.fn(()=>{})
  
});

describe("Military Mode of Payment", () => {
  test("Should Monthly salary", async () => {
    const { getAllByTestId, getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <ModeOfMilitaryServicePayment />
      </Store.Provider>
    );
    let entitlement;
    let monthlysalary;
    await waitFor(() => {
      expect(getByTestId("military-info-form")).toBeInTheDocument();
       entitlement = getByTestId("entitlement");
       monthlysalary = getByTestId("monthyBaseSalary");

      fireEvent.change(entitlement, { target: { value: "90000" } });
      fireEvent.change(monthlysalary, { target: { value: "70000" } });
    })

    await waitFor(() => {
      expect(entitlement).toHaveDisplayValue("90,000");
      expect(monthlysalary).toHaveDisplayValue("70,000");
    });

    let SubmitBtn;
    await waitFor(() => {
      SubmitBtn = getByTestId("modeOfPayement");
      fireEvent.click(SubmitBtn);
    });

  });

  

});
