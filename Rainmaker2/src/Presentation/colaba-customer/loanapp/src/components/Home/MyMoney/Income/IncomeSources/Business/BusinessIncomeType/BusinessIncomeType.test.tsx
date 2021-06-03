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
import { BusinessIncomeType } from "./BusinessIncomeType";
import { Store } from "../../../../../../../store/store";

// jest.mock('../../lib/localStorage');
jest.mock("../../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../../store/actions/BusinessActions");
// jest.mock('../../../../../Store/actions/TemplateActions');

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
      name: "Qumber",
    },
    incomeInfo: {
      incomeId: 1,
      incomeTypeId: 3,
    },
  },
  commonManager: {},
  employment: {},
  business: {},
  employmentHistory: {},
  militaryIncomeManager: {},
  otherIncomeManager: {},
  assetsManager: {},
};
const dispatch = jest.fn();

beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
});

describe("Business", () => {
  test("Edit Case", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <BusinessIncomeType />
      </Store.Provider>
    );

    let businessType: HTMLElement;
    let businessName: HTMLElement;
    let businessPhoneNumber: HTMLElement;
    let businessStartDate: HTMLElement;
    let businessJobTitle: HTMLElement;
    let businessOwnerShip: HTMLElement;
    let businessSubmit: HTMLElement;

    await waitFor(() => {
      businessType = getByTestId("business-type");
      businessName = getByTestId("business-name");
      businessPhoneNumber = getByTestId("business-phone-number");
      businessStartDate = getByTestId("startDate");
      businessJobTitle = getByTestId("job-title");
      businessOwnerShip = getByTestId("ownership-percentage");
      businessSubmit = getByTestId("business-info-next");

      expect(businessName).toHaveDisplayValue("Test Business Name");
      fireEvent.change(businessName, { target: { value: "New Name Test" } });
    });
    await waitFor(() => {
      console.log(businessName);
      expect(businessName).toHaveDisplayValue("New Name Test");
    });
  });

  test("Should change Business Phone number", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <BusinessIncomeType />
      </Store.Provider>
    );

    let businessPhoneNumber:HTMLElement;
    await waitFor(() => {
      businessPhoneNumber = getByTestId("business-phone-number");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(businessPhoneNumber, { target: { value: "123456789" } })
      
    });
    await waitFor(() => {
        expect(businessPhoneNumber).toHaveDisplayValue("(123) 456-789");
    })
  });

  test("Should show error on wrong Business Phone number", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <BusinessIncomeType />
      </Store.Provider>
    );

    let businessPhoneNumber:HTMLElement;
    await waitFor(() => {
      businessPhoneNumber = getByTestId("business-phone-number");
      // expect(resetPassHeader).toBeInTheDocument();
        fireEvent.change(businessPhoneNumber, { target: { value: "(111) 111-111" } })
      
    });
    let SubmitBtn:HTMLElement;
    await waitFor(() => {
        SubmitBtn = getByTestId("business-info-next");
    //   
    fireEvent.click(SubmitBtn);
      
    });

    await waitFor(() => {
        expect(getByTestId("businessPhoneNumber-error")).toBeInTheDocument();
        expect(getByTestId("businessPhoneNumber-error")).toHaveTextContent("Please enter US Phone Number only");
    })

  });

  test("Should submit form if all fields are filled", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <BusinessIncomeType />
      </Store.Provider>
    );
    await waitFor(() => {
      fireEvent.click(getByTestId("business-type"))
      fireEvent.click(getAllByTestId("businessType-options")[1])

      fireEvent.change(getByTestId("business-name"), { target: { value: "Business 1" } })
     
        fireEvent.change(getByTestId("job-title"), { target: { value: "Software Engineer" } })
        
        fireEvent.change(getByTestId("startDate"), { target: { value: "11/11/2019" } })
       
        fireEvent.change(getByTestId("ownership-percentage"), { target: { value: "11" } })
       
        fireEvent.change(getByTestId("business-phone-number"), { target: { value: "123456789" } })
          
    // fireEvent.click(getByTestId("employed-no")); 
    // fireEvent.click(getByTestId("ownership-interest-no"))

    })
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("business-info-next");
    //   
    fireEvent.click(SubmitBtn);
      
    });
  });
    

});
