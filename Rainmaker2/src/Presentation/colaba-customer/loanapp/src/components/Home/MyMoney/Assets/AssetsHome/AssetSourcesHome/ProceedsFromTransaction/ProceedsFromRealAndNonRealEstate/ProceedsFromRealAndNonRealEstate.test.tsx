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
import { ProceedsFromRealAndNonRealEstate } from "./ProceedsFromRealAndNonRealEstate";



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
const collateralAssetTypes = [{ "id": 1, "name": "House", "displayName": "House" }, { "id": 2, "name": "Automobile", "displayName": "Automobile" }, { "id": 3, "name": "Financial Account", "displayName": "Financial Account" }, { "id": 4, "name": "Other", "displayName": "Other" }];

describe("Proceeds from ProceedsFromRealAndNonRealEstate", () => {
  test("render test section", async () => {
    const { getByTestId } = render(

      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromRealAndNonRealEstate transactionProceedsDTO={null} updateFormValuesOnChange={async (data) => { console.log("Data  >>>>>>>>> ", data) }} />
        </Router>
      </Store.Provider>
    );
    await waitFor(() => {
      expect(getByTestId('expected-proceeds')).toBeInTheDocument();
      expect(getByTestId('asset-description')).toBeInTheDocument();
    });
  });


  test("Should show error on submit", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromRealAndNonRealEstate transactionProceedsDTO={null} updateFormValuesOnChange={async (data) => { console.log("Data  >>>>>>>>> ", data) }} />
        </Router>
      </Store.Provider>
    );
    let SubmitBtn: HTMLElement;
    await waitFor(() => {
      SubmitBtn = getByTestId("net-annual-income-submit");
      fireEvent.click(SubmitBtn);
    });
    await waitFor(() => {
      expect(getByTestId("expectedProceeds-error")).toBeInTheDocument();
      expect(getByTestId("assetDescription-error")).toBeInTheDocument();
    })

  });

  test("Chnage values and submit", async () => {
    const onSubmit = jest
    .fn()
    .mockImplementation((data) => {
        console.log("Data >>>>>>>>>>>>> ",data);
          return data;}
    ) 
    const handleSubmit = jest.fn();
    const { getByTestId,debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromRealAndNonRealEstate transactionProceedsDTO={null} updateFormValuesOnChange={onSubmit} />
        </Router>
      </Store.Provider>
    );

    let expectedProceeds: HTMLElement;
    let assetDescription: HTMLElement;
    await waitFor(() => {
      expectedProceeds = getByTestId("expected-proceeds");
      assetDescription = getByTestId("asset-description");
      fireEvent.change(expectedProceeds, { target: { value: "1111" } })
      fireEvent.change(assetDescription, { target: { value: "Test" } })

    });
    let SubmitBtn: HTMLElement;
    await waitFor(() => {
      SubmitBtn = getByTestId("net-annual-income-submit");
      fireEvent.click(SubmitBtn);
    });
    await waitFor(async () => {
      //await expect(onSubmit()).toHaveValue("test");
      //expect(onSubmit).toHaveBeenCalled();
      expect(onSubmit).toBeCalledWith({ expectedProceeds: '1,111', assetDescription: 'Test' });

      debug()
    });
  });

});