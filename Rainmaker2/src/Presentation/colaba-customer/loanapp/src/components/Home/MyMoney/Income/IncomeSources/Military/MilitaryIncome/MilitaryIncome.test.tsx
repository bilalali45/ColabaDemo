import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  render,
  waitFor,
  screen
} from "@testing-library/react";
import { MockEnvConfig } from "../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../test_utilities/SessionStoreMock";
import { MilitaryIncome } from "./MilitaryIncome";

jest.mock("../../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../../store/actions/MilitaryActions");

const state = {
  leftMenu: {
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
  employmentHistory:{},
  militaryIncomeManager: {}
};
const dispatch = jest.fn();


beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
});


describe("Military income step 1 ", () => {

  test("Show Employers field and test working", async () => {
    const { getByTestId, getAllByTestId } = render(
      <MilitaryIncome />
    );

    await waitFor(() => {
      expect(getByTestId('employer-info-form')).toBeInTheDocument();

    });

    let employerName: HTMLElement;
    await waitFor(() => {
      employerName = getByTestId("employer-name");
      fireEvent.change(employerName, { target: { value: "US Army" } })

    });
    await waitFor(() => {
      expect(employerName).toHaveDisplayValue("US Army");
    })
  });

  test("Should change Job Title", async () => {
    const { getByTestId } = render(
      <MilitaryIncome />
    );

    let jobTitle:HTMLElement;
    await waitFor(() => {
        jobTitle = getByTestId("job-title");
        fireEvent.change(jobTitle, { target: { value: "Major" } })
      
    });
    await waitFor(() => {
        expect(jobTitle).toHaveDisplayValue("Major");
    })
  });

  test("Should change Start Date", async () => {
    const { getByTestId } = render(
      <MilitaryIncome />
    );

    let startDate:HTMLElement;
    await waitFor(() => {
        startDate = getByTestId("startDate");
        fireEvent.change(startDate, { target: { value: "11/11/2019" } })
      
    });
    await waitFor(() => {
        expect(startDate).toHaveDisplayValue("11/11/2019");
    })
  });

  test("Should change Years in Profession", async () => {
    const { getByTestId } = render(
      <MilitaryIncome />
    );

    let YearsInProfession:HTMLElement;
    await waitFor(() => {
        YearsInProfession = getByTestId("years-in-profession");
        fireEvent.change(YearsInProfession, { target: { value: "11" } })
      
    });
    await waitFor(() => {
        expect(YearsInProfession).toHaveDisplayValue("11");
    })
  });

  test("Should show errors on empty form", async () => {
    const { getByTestId } = render(
      <MilitaryIncome />
    );

    let SubmitBtn:HTMLElement;
    await waitFor(() => {
        SubmitBtn = getByTestId("employer-info-next");
    //   
    fireEvent.click(SubmitBtn);
      
    });
    await waitFor(() => {
        expect(getByTestId("EmployerName-error")).toBeInTheDocument();
        expect(getByTestId("JobTitle-error")).toBeInTheDocument();
        expect(getByTestId("startDate-error")).toBeInTheDocument();
        expect(getByTestId("YearsInProfession-error")).toBeInTheDocument();
    })
  });


  test("Should submit form if all fields are filled", async () => {
    const { getByTestId } = render(
      <MilitaryIncome />
    );
    await waitFor(() => {
      fireEvent.change(getByTestId("employer-name"), { target: { value: "Employer 1" } })
     
        fireEvent.change(getByTestId("job-title"), { target: { value: "Software Engineer" } })
        
        fireEvent.change(getByTestId("startDate"), { target: { value: "11/11/2019" } })
       
        fireEvent.change(getByTestId("years-in-profession"), { target: { value: "11" } })
       
    })
    let SubmitBtn;
    await waitFor(() => {
        SubmitBtn = getByTestId("employer-info-next"); 
    fireEvent.click(SubmitBtn);
      
    });
  });




});


