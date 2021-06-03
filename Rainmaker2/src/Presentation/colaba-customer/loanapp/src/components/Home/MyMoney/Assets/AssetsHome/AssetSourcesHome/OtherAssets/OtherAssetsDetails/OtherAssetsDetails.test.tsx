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
import { NavigationHandler } from "../../../../../../../../Utilities/Navigation/NavigationHandler";
import { MockEnvConfig } from "../../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../../../../store/store";
import { LoanNagivator } from "../../../../../../../../Utilities/Navigation/LoanNavigator";

import AssetsActions from "../../../../../../../../store/actions/__mocks__/AssetsActions";
import { ContextExclusionPlugin } from "webpack";
import { OtherAssetsDetails } from "./OtherAssetsDetails";



const dispatch = jest.fn();
jest.mock("../../../../../../../../store/actions/OtherAssetsActions");
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
  NavigationHandler.navigateToPath = jest.fn(() => { });
  MockEnvConfig();
  MockSessionStorage();
});
const history = createMemoryHistory()
const collateralAssetTypes = [{ "id": 1, "name": "House", "displayName": "House" }, { "id": 2, "name": "Automobile", "displayName": "Automobile" }, { "id": 3, "name": "Financial Account", "displayName": "Financial Account" }, { "id": 4, "name": "Other", "displayName": "Other" }];


describe("Other Asset test", () => {
  test("render test section", async () => {
    const { getByTestId } = render(

      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <OtherAssetsDetails  />
        </Router>
      </Store.Provider>
    );
    await waitFor(() => {
      expect(getByTestId('asset-type')).toBeInTheDocument();
    });
  });


  test("Test for fields visibility on asset type selection", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <OtherAssetsDetails  />
        </Router>
      </Store.Provider>
    );
    await waitFor(() => {
      expect(getByTestId('asset-type')).toBeInTheDocument();
    });
    let assetType: HTMLElement = getByTestId("asset-type");
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[0])
      })
      expect(getByTestId('InstitutionName-15-0')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-15-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-15-2')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[1])
      })
      expect(getByTestId('InstitutionName-16-0')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-16-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-16-2')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[2])
      })
      expect(getByTestId('InstitutionName-17-0')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-17-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-17-2')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[3])
      })
      expect(getByTestId('InstitutionName-18-0')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-18-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-18-2')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[4])
      })
      expect(getByTestId('Value-19-2')).toBeInTheDocument(); 
    });

    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[5])
      })
      expect(getByTestId('Value-20-1')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[6])
      })
      expect(getByTestId('Value-21-2')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[7])
      })
      expect(getByTestId('Value-22-2')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[8])
      })
      expect(getByTestId('Value-23-2')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[9])
      })
      expect(getByTestId('Value-24-2')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[10])
      })
      expect(getByTestId('Description-25-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-25-2')).toBeInTheDocument(); 
    });
    /*   let submitBtn = getByTestId('submitBtn');
      fireEvent.click(submitBtn);
*/
  });





  test("Test for fields validation on submit", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <OtherAssetsDetails  />
        </Router>
      </Store.Provider>
    );
    await waitFor(() => {
      expect(getByTestId('asset-type')).toBeInTheDocument();
    });
    let assetType: HTMLElement = getByTestId("asset-type");
    let submitBtn = getByTestId('saveBtn');
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[0])
      })
      expect(getByTestId('InstitutionName-15-0')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-15-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-15-2')).toBeInTheDocument(); 
      fireEvent.click(submitBtn);
      
      expect(getByTestId('InstitutionName-error')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-error')).toBeInTheDocument(); 
      expect(getByTestId('Value-error')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[1])
      })
      expect(getByTestId('InstitutionName-16-0')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-16-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-16-2')).toBeInTheDocument(); 
      fireEvent.click(submitBtn);
      expect(getByTestId('InstitutionName-error')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-error')).toBeInTheDocument(); 
      expect(getByTestId('Value-error')).toBeInTheDocument();  
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[2])
      })
      expect(getByTestId('InstitutionName-17-0')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-17-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-17-2')).toBeInTheDocument(); 
      fireEvent.click(submitBtn);
      expect(getByTestId('InstitutionName-error')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-error')).toBeInTheDocument(); 
      expect(getByTestId('Value-error')).toBeInTheDocument();  
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[3])
      })
      expect(getByTestId('InstitutionName-18-0')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-18-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-18-2')).toBeInTheDocument(); 
      fireEvent.click(submitBtn);
      expect(getByTestId('InstitutionName-error')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-error')).toBeInTheDocument(); 
      expect(getByTestId('Value-error')).toBeInTheDocument();  
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[4])
      })
      expect(getByTestId('Value-19-2')).toBeInTheDocument(); 
      fireEvent.click(submitBtn);
      expect(getByTestId('Value-error')).toBeInTheDocument();  
    });

    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[5])
      })
      expect(getByTestId('Value-20-1')).toBeInTheDocument(); 
       fireEvent.click(submitBtn);
      expect(getByTestId('Value-error')).toBeInTheDocument();  
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[6])
      })
      expect(getByTestId('Value-21-2')).toBeInTheDocument(); 
      fireEvent.click(submitBtn);
      expect(getByTestId('Value-error')).toBeInTheDocument();  
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[7])
      })
      expect(getByTestId('Value-22-2')).toBeInTheDocument(); 
      /* fireEvent.click(submitBtn);
      expect(getByTestId('Value-error')).toBeInTheDocument(); */
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[8])
      })
      expect(getByTestId('Value-23-2')).toBeInTheDocument(); 
      fireEvent.click(submitBtn);
      expect(getByTestId('Value-error')).toBeInTheDocument();  
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[9])
      })
      expect(getByTestId('Value-24-2')).toBeInTheDocument(); 
      fireEvent.click(submitBtn);
      expect(getByTestId('Value-error')).toBeInTheDocument(); 
    });
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[10])
      })
      expect(getByTestId('Description-25-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-25-2')).toBeInTheDocument(); 
      fireEvent.click(submitBtn);
      expect(getByTestId('Value-error')).toBeInTheDocument(); 
      expect(getByTestId('Description-error')).toBeInTheDocument(); 
    });
    
  });

 
  test("Test for form submit", async () => {
    const { getByTestId, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <OtherAssetsDetails  />
        </Router>
      </Store.Provider>
    );
    let submitBtn = getByTestId('saveBtn');
    let financialInstitution: HTMLElement;
    let accountNumber: HTMLElement;
    let cashOrMarketValue: HTMLElement;

    await waitFor(() => {
      expect(getByTestId('asset-type')).toBeInTheDocument();
    });
    let assetType: HTMLElement = getByTestId("asset-type");
    await waitFor(async () => {
      fireEvent.click(assetType);
      await waitFor(async () => {
        let assetTypeoptions: HTMLElement[] = getAllByTestId("assetType-options");
        await fireEvent.click(assetTypeoptions[0])
      })
      expect(getByTestId('InstitutionName-15-0')).toBeInTheDocument(); 
      expect(getByTestId('AccountNumber-15-1')).toBeInTheDocument(); 
      expect(getByTestId('Value-15-2')).toBeInTheDocument(); 

      financialInstitution = getByTestId("InstitutionName-15-0");
      accountNumber = getByTestId("AccountNumber-15-1");
      cashOrMarketValue = getByTestId("Value-15-2");
      fireEvent.change(financialInstitution, { target: { value: "Test Name" } })
      fireEvent.change(accountNumber, { target: { value: "TEST" } })
      fireEvent.change(cashOrMarketValue, { target: { value: "1" } })
      fireEvent.click(submitBtn);
      expect(NavigationHandler.navigateToPath).toHaveBeenCalled();
    });
  });
});