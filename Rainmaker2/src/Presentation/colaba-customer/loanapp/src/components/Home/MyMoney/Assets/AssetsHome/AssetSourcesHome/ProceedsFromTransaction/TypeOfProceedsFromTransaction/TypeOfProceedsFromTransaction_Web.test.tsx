import React, { createContext, useReducer, ReactFragment, useState } from "react";
import { MemoryRouter, Router } from "react-router-dom";
import { createMemoryHistory } from "history";
import {
  act,
  fireEvent,
  getAllByTestId,
  getByText,
  render,
  waitFor,
} from "@testing-library/react";
import { ProceedsFromTransaction } from '../ProceedsFromTransaction';
import { NavigationHandler } from "../../../../../../../../Utilities/Navigation/NavigationHandler";
import { MockEnvConfig } from "../../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../../../../store/store";
import { LoanNagivator } from "../../../../../../../../Utilities/Navigation/LoanNavigator";

import AssetsActions from "../../../../../../../../store/actions/__mocks__/AssetsActions";
import { ContextExclusionPlugin } from "webpack";
import { TypeOfProceedsFromTransaction_Web } from "./TypeOfProceedsFromTransaction_Web";



const dispatch = jest.fn();
jest.mock("../../../../../../../../store/actions/AssetsActions");
jest.mock("../../../../../../../../store/actions/TransactionProceedsActions");

jest.mock("../../../../../../../../lib/LocalDB");

const state = {
  leftMenu: {
    navigation: null,
    leftMenuItems: [],
    notAllowedItems: [],
  },
  error: {},
  loanManager: {
    loanInfo: {
      loanApplicationId: 6357,
      loanPurposeId: 1,
      loanGoalId: 4,
      borrowerId: 6615,
      ownTypeId: 1,
      borrowerName: "Qumber"
    },
    primaryBorrowerInfo: {
      id: 6615,
      firstName: "Qumber",
      lastName: "Kazmi",
      middleName: "",
      suffix: "",
      email: "qumber@gmail.com",
      homePhone: "",
      workPhone: "",
      workExt: "",
      cellPhone: "2142259077",
      ownTypeId: 1,
      name: "Qumber"
    },
    assetInfo: {
      borrowerName: "Qumber Kazmi",
      borrowerAssetId: 2272,
      assetCategoryId: 6,
      assetTypeId: 12,
      displayName: "Proceeds from Transactions"
    }
  },
  commonManager: {},
  employment: {},
  business: {},
  employmentHistory: {},
  militaryIncomeManager: {},
  otherIncomeManager: {},
  assetsManager: {},
};

beforeEach(() => {
  NavigationHandler.enableFeature = jest.fn(() => { });
  NavigationHandler.disableFeature = jest.fn(() => { });
  NavigationHandler.moveNext = jest.fn(() => { });
  NavigationHandler.isFieldVisible = jest.fn(() => true);
  NavigationHandler.getNavigationStateAsString = jest.fn(() => "");
  /* NavigationHandler.navigateToPath = jest.fn(() => { }); */

  MockEnvConfig();
  MockSessionStorage();
});
const history = createMemoryHistory()
const assetTypesByCategoryList = [{"id":12,"assetCategoryId":6,"name":"Proceeds from a Loan","displayName":"Proceeds from a Loan","tenantAlternateName":null,"fieldsInfo":"{\r\n\t\"fieldsInfo\": [\r\n\t\t{\r\n\t\t\t\"id\": 0,\r\n\t\t\t\"name\": \"InstitutionName\",\r\n\t\t\t\"caption\": \"Financial Institution\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 0,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Financial Institution Name\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 1,\r\n\t\t\t\"name\": \"AccountNumber\",\r\n\t\t\t\"caption\": \"Account Number\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 1,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9]{1,99}$\",\r\n\t\t\t\"placeHolder\": \"XXX-XXX-XXXX\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 2,\r\n\t\t\t\"name\": \"Value\",\r\n\t\t\t\"caption\": \"Cash or Market Value\",\r\n\t\t\t\"datatype\": \"decimal\",\t\t\t\r\n\t\t\t\"displayOrder\": 2,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[0-9,.]{1,9}$\",\r\n\t\t\t\"placeHolder\": \"Amount\",\r\n\t\t\t\"icon\": \"<i className='zmdi zmdi-money'></i>\",\r\n\t\t\t\"onBlur\": \"addProceedingZeros\",\r\n\t\t\t\"formatter\": \"formateNumber\",\r\n\t\t\t\"onFocus\": \"removeCommaFormatting\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t\t{\r\n\t\t\t\"id\": 3,\r\n\t\t\t\"name\": \"Description\",\r\n\t\t\t\"caption\": \"Asset Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 500,\r\n\t\t\t\"displayOrder\": 3,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Description\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t}\r\n\t\t\r\n\t]\r\n}"},{"id":13,"assetCategoryId":6,"name":"Proceeds from selling non-real estate assets","displayName":"Proceeds from selling non-real estate assets","tenantAlternateName":null,"fieldsInfo":"{\r\n\t\"fieldsInfo\": [\r\n\t\t{\r\n\t\t\t\"id\": 0,\r\n\t\t\t\"name\": \"InstitutionName\",\r\n\t\t\t\"caption\": \"Financial Institution\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 0,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Financial Institution Name\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 1,\r\n\t\t\t\"name\": \"AccountNumber\",\r\n\t\t\t\"caption\": \"Account Number\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 1,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9]{1,99}$\",\r\n\t\t\t\"placeHolder\": \"XXX-XXX-XXXX\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 2,\r\n\t\t\t\"name\": \"Value\",\r\n\t\t\t\"caption\": \"Cash or Market Value\",\r\n\t\t\t\"datatype\": \"decimal\",\t\t\t\r\n\t\t\t\"displayOrder\": 2,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[0-9,.]{1,9}$\",\r\n\t\t\t\"placeHolder\": \"Amount\",\r\n\t\t\t\"icon\": \"<i className='zmdi zmdi-money'></i>\",\r\n\t\t\t\"onBlur\": \"addProceedingZeros\",\r\n\t\t\t\"formatter\": \"formateNumber\",\r\n\t\t\t\"onFocus\": \"removeCommaFormatting\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t\t{\r\n\t\t\t\"id\": 3,\r\n\t\t\t\"name\": \"Description\",\r\n\t\t\t\"caption\": \"Asset Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 500,\r\n\t\t\t\"displayOrder\": 3,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Description\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t}\r\n\t\t\r\n\t]\r\n}"},{"id":14,"assetCategoryId":6,"name":"Proceeds from selling real estate","displayName":"Proceeds from selling real estate","tenantAlternateName":null,"fieldsInfo":"{\r\n\t\"fieldsInfo\": [\r\n\t\t{\r\n\t\t\t\"id\": 0,\r\n\t\t\t\"name\": \"InstitutionName\",\r\n\t\t\t\"caption\": \"Financial Institution\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 0,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Financial Institution Name\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 1,\r\n\t\t\t\"name\": \"AccountNumber\",\r\n\t\t\t\"caption\": \"Account Number\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 1,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9]{1,99}$\",\r\n\t\t\t\"placeHolder\": \"XXX-XXX-XXXX\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 2,\r\n\t\t\t\"name\": \"Value\",\r\n\t\t\t\"caption\": \"Cash or Market Value\",\r\n\t\t\t\"datatype\": \"decimal\",\t\t\t\r\n\t\t\t\"displayOrder\": 2,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[0-9,.]{1,9}$\",\r\n\t\t\t\"placeHolder\": \"Amount\",\r\n\t\t\t\"icon\": \"<i className='zmdi zmdi-money'></i>\",\r\n\t\t\t\"onBlur\": \"addProceedingZeros\",\r\n\t\t\t\"formatter\": \"formateNumber\",\r\n\t\t\t\"onFocus\": \"removeCommaFormatting\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t\t{\r\n\t\t\t\"id\": 3,\r\n\t\t\t\"name\": \"Description\",\r\n\t\t\t\"caption\": \"Asset Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 500,\r\n\t\t\t\"displayOrder\": 3,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Description\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t}\r\n\t\t\r\n\t]\r\n}"}];

describe("Proceeds from loan", () => {
  test("render test section", async () => {
    const { getByTestId } = render(

      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <TypeOfProceedsFromTransaction_Web assetTypesByCategoryList={assetTypesByCategoryList} setSelectedPropertyType={null} selectedPropertyType={12} />
        </Router>
      </Store.Provider>
    );

    await waitFor(() => {
      
      /* expect(getByTestId('popup-title')).toHaveTextContent('Proceeds from Transactions'); */
      expect(getByTestId('IconRadioBox-12')).toBeInTheDocument();
      expect(getByTestId('IconRadioBox-13')).toBeInTheDocument();
      expect(getByTestId('IconRadioBox-14')).toBeInTheDocument();
      //console.log(incomesourcesScreen);
      //expect(incomesourcesScreen).toHaveTextContent('Which transaction are these proceeds from?');
      /*expect(getByTestId('earnestMoney')).toBeInTheDocument();
      expect(getByTestId('earnestMoneyDAmount-input')).toBeInTheDocument();
      expect(getByTestId('earnestMoney-2')).toBeInTheDocument();
      expect(getByTestId('submitBtn')).toBeInTheDocument(); */

    });

    await waitFor(()=>{
      expect(getByTestId('IconRadioBox-12')).toBeChecked();
    })

    /*   let submitBtn = getByTestId('submitBtn');
      fireEvent.click(submitBtn);
*/
  });
/* 
  test("render test if yes is selected", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <TypeOfProceedsFromTransaction_Web assetTypesByCategoryList = {[{"id":12,"assetCategoryId":6,"name":"Proceeds from a Loan","displayName":"Proceeds from a Loan","tenantAlternateName":null,"fieldsInfo":"{\r\n\t\"fieldsInfo\": [\r\n\t\t{\r\n\t\t\t\"id\": 0,\r\n\t\t\t\"name\": \"InstitutionName\",\r\n\t\t\t\"caption\": \"Financial Institution\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 0,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Financial Institution Name\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 1,\r\n\t\t\t\"name\": \"AccountNumber\",\r\n\t\t\t\"caption\": \"Account Number\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 1,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9]{1,99}$\",\r\n\t\t\t\"placeHolder\": \"XXX-XXX-XXXX\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 2,\r\n\t\t\t\"name\": \"Value\",\r\n\t\t\t\"caption\": \"Cash or Market Value\",\r\n\t\t\t\"datatype\": \"decimal\",\t\t\t\r\n\t\t\t\"displayOrder\": 2,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[0-9,.]{1,9}$\",\r\n\t\t\t\"placeHolder\": \"Amount\",\r\n\t\t\t\"icon\": \"<i className='zmdi zmdi-money'></i>\",\r\n\t\t\t\"onBlur\": \"addProceedingZeros\",\r\n\t\t\t\"formatter\": \"formateNumber\",\r\n\t\t\t\"onFocus\": \"removeCommaFormatting\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t\t{\r\n\t\t\t\"id\": 3,\r\n\t\t\t\"name\": \"Description\",\r\n\t\t\t\"caption\": \"Asset Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 500,\r\n\t\t\t\"displayOrder\": 3,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Description\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t}\r\n\t\t\r\n\t]\r\n}"},{"id":13,"assetCategoryId":6,"name":"Proceeds from selling non-real estate assets","displayName":"Proceeds from selling non-real estate assets","tenantAlternateName":null,"fieldsInfo":"{\r\n\t\"fieldsInfo\": [\r\n\t\t{\r\n\t\t\t\"id\": 0,\r\n\t\t\t\"name\": \"InstitutionName\",\r\n\t\t\t\"caption\": \"Financial Institution\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 0,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Financial Institution Name\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 1,\r\n\t\t\t\"name\": \"AccountNumber\",\r\n\t\t\t\"caption\": \"Account Number\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 1,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9]{1,99}$\",\r\n\t\t\t\"placeHolder\": \"XXX-XXX-XXXX\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 2,\r\n\t\t\t\"name\": \"Value\",\r\n\t\t\t\"caption\": \"Cash or Market Value\",\r\n\t\t\t\"datatype\": \"decimal\",\t\t\t\r\n\t\t\t\"displayOrder\": 2,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[0-9,.]{1,9}$\",\r\n\t\t\t\"placeHolder\": \"Amount\",\r\n\t\t\t\"icon\": \"<i className='zmdi zmdi-money'></i>\",\r\n\t\t\t\"onBlur\": \"addProceedingZeros\",\r\n\t\t\t\"formatter\": \"formateNumber\",\r\n\t\t\t\"onFocus\": \"removeCommaFormatting\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t\t{\r\n\t\t\t\"id\": 3,\r\n\t\t\t\"name\": \"Description\",\r\n\t\t\t\"caption\": \"Asset Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 500,\r\n\t\t\t\"displayOrder\": 3,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Description\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t}\r\n\t\t\r\n\t]\r\n}"},{"id":14,"assetCategoryId":6,"name":"Proceeds from selling real estate","displayName":"Proceeds from selling real estate","tenantAlternateName":null,"fieldsInfo":"{\r\n\t\"fieldsInfo\": [\r\n\t\t{\r\n\t\t\t\"id\": 0,\r\n\t\t\t\"name\": \"InstitutionName\",\r\n\t\t\t\"caption\": \"Financial Institution\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 0,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Financial Institution Name\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 1,\r\n\t\t\t\"name\": \"AccountNumber\",\r\n\t\t\t\"caption\": \"Account Number\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 150,\r\n\t\t\t\"displayOrder\": 1,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9]{1,99}$\",\r\n\t\t\t\"placeHolder\": \"XXX-XXX-XXXX\",\r\n\t\t\t\"icon\": \"\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": 2,\r\n\t\t\t\"name\": \"Value\",\r\n\t\t\t\"caption\": \"Cash or Market Value\",\r\n\t\t\t\"datatype\": \"decimal\",\t\t\t\r\n\t\t\t\"displayOrder\": 2,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[0-9,.]{1,9}$\",\r\n\t\t\t\"placeHolder\": \"Amount\",\r\n\t\t\t\"icon\": \"<i className='zmdi zmdi-money'></i>\",\r\n\t\t\t\"onBlur\": \"addProceedingZeros\",\r\n\t\t\t\"formatter\": \"formateNumber\",\r\n\t\t\t\"onFocus\": \"removeCommaFormatting\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t},\r\n\t\t\t{\r\n\t\t\t\"id\": 3,\r\n\t\t\t\"name\": \"Description\",\r\n\t\t\t\"caption\": \"Asset Description\",\r\n\t\t\t\"datatype\": \"string\",\r\n\t\t\t\"maxLength\": 500,\r\n\t\t\t\"displayOrder\": 3,\r\n\t\t\t\"Enabled\":true,\r\n\t\t\t\"value\": \"\",\r\n\t\t\t\"regex\":\"^[a-zA-Z0-9 . -,]{1,150}$\",\r\n\t\t\t\"placeHolder\": \"Description\",\r\n\t\t\t\"rules\":{\"required\": \"This field is required.\"}\r\n\t\t}\r\n\t\t\r\n\t]\r\n}"}]} setSelectedPropertyType = {()=>{}} selectedPropertyType = {12} />
        </Router>
      </Store.Provider>
    );

    let procedingOption: HTMLElement = getByTestId("proceding-checkbox-yes");
    await waitFor(() => {
      fireEvent.click(procedingOption);
      expect(getByTestId('asset-type')).toBeInTheDocument();
    });


       let submitBtn = getByTestId('submitBtn');
      fireEvent.click(submitBtn);
  
  }); */
});