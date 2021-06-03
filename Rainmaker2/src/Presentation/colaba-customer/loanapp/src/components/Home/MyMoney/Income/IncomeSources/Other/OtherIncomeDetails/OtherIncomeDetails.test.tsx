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
import { Store } from "../../../../../../../store/store";
import {OtherIncomeDetails} from "./OtherIncomeDetails";


jest.mock("../../../../../../../store/actions/MilitaryIncomeActions");
jest.mock("../../../../../../../store/actions/BorrowerActions");
jest.mock("../../../../../../../store/actions/BusinessActions");
jest.mock("../../../../../../../store/actions/EmploymentActions");
jest.mock("../../../../../../../store/actions/EmploymentHistoryActions");
jest.mock("../../../../../../../store/actions/GettingStartedActions");
jest.mock("../../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../../store/actions/IncomeActions");
jest.mock("../../../../../../../store/actions/IncomeReviewActions");
jest.mock("../../../../../../../store/actions/MaritalStatusActions");
jest.mock("../../../../../../../store/actions/MyNewMortgageActions");
jest.mock("../../../../../../../store/actions/MyNewPropertyAddressActions");
jest.mock("../../../../../../../store/actions/RetirementIncomeActions");
jest.mock("../../../../../../../store/actions/SelfEmploymentActions");



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
    otherIncomeManager:{
        otherIncomeTypeList: [
          {
              "incomeGroupId": 1,
              "incomeGroupName": "Family",
              "imageUrl": null,
              "incomeGroupDescription": null,
              "incomeGroupDisplayOrder": 0,
              "incomeTypes": [
                  {
                      "id": 11,
                      "name": "Alimony",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 12,
                      "name": "Child Support",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 13,
                      "name": "Separate Maintenance",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 14,
                      "name": "Foster Care ",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  }
              ],
              "incomeGroupDisplayName": "Family"
          },
          {
              "incomeGroupId": 2,
              "incomeGroupName": "Investments",
              "imageUrl": null,
              "incomeGroupDescription": null,
              "incomeGroupDisplayOrder": 0,
              "incomeTypes": [
                  {
                      "id": 15,
                      "name": "Annuity",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":true\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 16,
                      "name": "Capital Gains",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":false\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":true\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 17,
                      "name": "Interest / Dividends",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":false\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":true\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 18,
                      "name": "Notes Receivable",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 19,
                      "name": "Trust",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  }
              ],
              "incomeGroupDisplayName": "Investments"
          },
          {
              "incomeGroupId": 3,
              "incomeGroupName": "Housing",
              "imageUrl": null,
              "incomeGroupDescription": null,
              "incomeGroupDisplayOrder": 0,
              "incomeTypes": [
                  {
                      "id": 20,
                      "name": "Housing Or Parsonage",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 21,
                      "name": "Mortgage Credit Certificate",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 22,
                      "name": "Mortgage Diąerential Payments",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  }
              ],
              "incomeGroupDisplayName": "Housing"
          },
          {
              "incomeGroupId": 4,
              "incomeGroupName": "Government",
              "imageUrl": null,
              "incomeGroupDescription": null,
              "incomeGroupDisplayOrder": 0,
              "incomeTypes": [
                  {
                      "id": 23,
                      "name": "Public Assistance",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 24,
                      "name": "Unemployment Benefits",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 25,
                      "name": "VA Compensation",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  }
              ],
              "incomeGroupDisplayName": "Government"
          },
          {
              "incomeGroupId": 5,
              "incomeGroupName": "Miscellaneous",
              "imageUrl": null,
              "incomeGroupDescription": null,
              "incomeGroupDisplayOrder": 0,
              "incomeTypes": [
                  {
                      "id": 26,
                      "name": "Automobile Allowance",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 27,
                      "name": "Boarder Income",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 28,
                      "name": "Royalty Payments",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  },
                  {
                      "id": 29,
                      "name": "Disability",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
                  }
              ],
              "incomeGroupDisplayName": "Miscellaneous"
          },
          {
              "incomeGroupId": 6,
              "incomeGroupName": "Other",
              "imageUrl": null,
              "incomeGroupDescription": null,
              "incomeGroupDisplayOrder": 0,
              "incomeTypes": [
                  {
                      "id": 30,
                      "name": "Other Income Source",
                      "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":false\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":true\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":true\r\n\t\t}\r\n\t]\r\n}"
                  }
              ],
              "incomeGroupDisplayName": "Other"
          }
      ],
      selectedOtherIncome: {
        "id": 11,
        "name": "Alimony",
        "fieldsInfo": "{\r\n\t\"fieldsInfo\": [\r\n  \t{\r\n\t\t\t\"name\": \"monthlyBaseIncome\",\r\n\t\t\t\"caption\": \"Monthly Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 0,\r\n      \"Enabled\":true\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"name\": \"annualBaseIncome\",\r\n\t\t\t\"caption\": \"Annual Income\",\r\n\t\t\t\"datatype\": \"decimal\",\r\n\t\t\t\"displayOrder\": 1,\r\n      \"Enabled\":false\r\n\t\t},\r\n  \t{\r\n\t\t\t\"name\": \"description\",\r\n\t\t\t\"caption\": \"Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLendth\": 150,\r\n\t\t\t\"displayOrder\": 2,\r\n      \"Enabled\":false\r\n\t\t}\r\n\t]\r\n}"
    }
    },
    assetsManager: {}
  };
  const dispatch = jest.fn();
  

beforeEach(() => {
    MockEnvConfig();
    MockSessionStorage();
  });


  describe("Other Income Screen", () => {
    test("check other income row", async () => {
        const { getByTestId , getAllByTestId} = render(
          
    <Store.Provider value={{ state, dispatch }}>
       <OtherIncomeDetails/>
    </Store.Provider>
        );

        await waitFor(() => {
          expect(getByTestId("otherIncome-info-form")).toBeInTheDocument();
          expect(getAllByTestId("monthlyBaseIncome")).toHaveLength(2);
          expect(screen.queryByTestId("description")).toBeNull();
          expect(screen.queryByTestId("annualBaseIncome")).toBeNull();     
        })

        let inputElement = getAllByTestId("monthlyBaseIncome");
        fireEvent.change(inputElement[1], { target: { value: "90000" } });

        await waitFor(() => {
            expect(inputElement[1]).toHaveDisplayValue("90,000");
          });
    });
  });


