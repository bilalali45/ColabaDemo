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
import { ProceedsFromLoan } from "./ProceedsFromLoan";
import AssetsActions from "../../../../../../../../store/actions/__mocks__/AssetsActions";
import { ContextExclusionPlugin } from "webpack";



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

describe("Proceeds from loan", () => {
  test("render test section", async () => {
    const { getByTestId } = render(

      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromLoan collateralAssetTypes={collateralAssetTypes} transactionProceedsDTO={null} updateFormValuesOnChange={async (data) => { console.log("Data  >>>>>>>>> ", data) }} />
        </Router>
      </Store.Provider>
    );

    await waitFor(() => {
      /* expect(getByTestId('popup-title')).toHaveTextContent('Proceeds from Transactions'); */
      expect(getByTestId('net-annual-income')).toBeInTheDocument();
      expect(getByTestId('proceding-checkbox-yes')).toBeInTheDocument();
      expect(getByTestId('proceding-checkbox-no')).toBeInTheDocument();
      /*expect(getByTestId('subtitle')).toHaveTextContent('Have you made an earnest money deposit on this purchase?');
      expect(getByTestId('earnestMoney')).toBeInTheDocument();
      expect(getByTestId('earnestMoneyDAmount-input')).toBeInTheDocument();
      expect(getByTestId('earnestMoney-2')).toBeInTheDocument();
      expect(getByTestId('submitBtn')).toBeInTheDocument(); */

    });

    await waitFor(() => {
      let noRadiobtn = getByTestId("net-annual-income");
      /* let noRadiobtn = getByTestId("net-annual-income"); */

      //fireEvent.change(noRadiobtn, { target: { value: 900000 } })

    });

    /*   let submitBtn = getByTestId('submitBtn');
      fireEvent.click(submitBtn);
*/
  });

  test("render test if yes is selected", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromLoan collateralAssetTypes={collateralAssetTypes} transactionProceedsDTO={null} updateFormValuesOnChange={async (data) => { console.log("Data  >>>>>>>>> ", data) }} />
        </Router>
      </Store.Provider>
    );

    let procedingOption: HTMLElement = getByTestId("proceding-checkbox-yes");
    await waitFor(() => {
      fireEvent.click(procedingOption);
      expect(getByTestId('asset-type')).toBeInTheDocument();
    });


    /*   let submitBtn = getByTestId('submitBtn');
      fireEvent.click(submitBtn);
*/
  });
  test("render test if no is selected", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromLoan collateralAssetTypes={collateralAssetTypes} transactionProceedsDTO={null} updateFormValuesOnChange={async (data) => { console.log("Data  >>>>>>>>> ", data) }} />
        </Router>
      </Store.Provider>
    );

    let procedingOption: HTMLElement = getByTestId("proceding-checkbox-no");
    await waitFor(() => {
      fireEvent.click(procedingOption);
      expect(() => getByTestId('assetType')).toThrow('Unable to find an element');
    });
  });

  test("render test if other text box is generating if Other is selected", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromLoan collateralAssetTypes={collateralAssetTypes} transactionProceedsDTO={null} updateFormValuesOnChange={async (data) => { console.log("Data  >>>>>>>>> ", data) }} />
        </Router>
      </Store.Provider>
    );

    let procedingOption: HTMLElement = getByTestId("proceding-checkbox-yes");

    await waitFor(() => {
      fireEvent.click(procedingOption);
      expect(getByTestId('asset-type')).toBeInTheDocument();
    });
    await waitFor(async () => {
      let businessType: HTMLElement = getByTestId("asset-type");
      fireEvent.click(businessType);
      let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
      await waitFor(async () => {
        await fireEvent.click(assetTypeoptions[3])
      })
      expect(getByTestId('asset-description')).toBeInTheDocument();
    });


    /*   let submitBtn = getByTestId('submitBtn');
      fireEvent.click(submitBtn);
*/
  });

  test("render test if submit when empty", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromLoan collateralAssetTypes={collateralAssetTypes} transactionProceedsDTO={null} updateFormValuesOnChange={async (data) => { console.log("Data  >>>>>>>>> ", data) }} />
        </Router>
      </Store.Provider>
    );

    let procedingOption: HTMLElement = getByTestId("proceding-checkbox-yes");

    await waitFor(() => {
      fireEvent.click(procedingOption);
      expect(getByTestId('asset-type')).toBeInTheDocument();
    });
    await waitFor(async () => {
      let businessType: HTMLElement = getByTestId("asset-type");
      fireEvent.click(businessType);
      let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
      await waitFor(async () => {
        await fireEvent.click(assetTypeoptions[3])
      })
      expect(getByTestId('asset-description')).toBeInTheDocument();
    });

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


  test("render test if form is submitting with no issues", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromLoan collateralAssetTypes={collateralAssetTypes} transactionProceedsDTO={null} updateFormValuesOnChange={async (data) => { console.log("Data  >>>>>>>>> ", data) }} />
        </Router>
      </Store.Provider>
    );

    let procedingOption: HTMLElement = getByTestId("proceding-checkbox-yes");

    await waitFor(() => {
      fireEvent.click(procedingOption);
      expect(getByTestId('asset-type')).toBeInTheDocument();
    });
    await waitFor(async () => {
      let businessType: HTMLElement = getByTestId("asset-type");
      fireEvent.click(businessType);
      let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
      await waitFor(async () => {
        await fireEvent.click(assetTypeoptions[3])
      })
      expect(getByTestId('asset-description')).toBeInTheDocument();
    });

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







  test("render test if form is submitting with no issues", async () => {
    const onSubmit = jest
    .fn()
    .mockImplementation((data) => {
        console.log("Data >>>>>>>>>>>>> ",data);
          return data;}
    ) 
    const { getByTestId,debug,getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
        <ProceedsFromLoan collateralAssetTypes={collateralAssetTypes} transactionProceedsDTO={null} updateFormValuesOnChange={onSubmit} />
        </Router>
      </Store.Provider>
    );
    let procedingOption: HTMLElement = getByTestId("proceding-checkbox-yes");

    await waitFor(() => {
      fireEvent.click(procedingOption);
      expect(getByTestId('asset-type')).toBeInTheDocument();
    });
    await waitFor(async () => {
      let businessType: HTMLElement = getByTestId("asset-type");
      fireEvent.click(businessType);
      let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
      await waitFor(async () => {
        await fireEvent.click(assetTypeoptions[3])
      })
      expect(getByTestId('asset-description')).toBeInTheDocument();
    });


    let expectedProceeds: HTMLElement;
    let assetDescription: HTMLElement;
    await waitFor(() => {
      expectedProceeds = getByTestId("net-annual-income");
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
      expect(onSubmit).toBeCalledWith({
      expectedProceeds: '1,111',
      securedByAnAsset: 'Yes',
      assetType: 'Other',
      assetDescription: 'Test',
      selectedAssetTypeId: 4
    });

      debug()
    });
  });
});