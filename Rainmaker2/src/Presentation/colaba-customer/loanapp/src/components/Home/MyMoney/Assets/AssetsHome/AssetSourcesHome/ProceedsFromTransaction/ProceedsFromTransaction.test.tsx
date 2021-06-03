import React, { createContext, useReducer, ReactFragment } from "react";
import { MemoryRouter, Router } from "react-router-dom";
import { createMemoryHistory } from "history";
import {
  act,
  fireEvent,
  getAllByTestId,
  getByTestId,
  render,
  screen,
  waitFor,
} from "@testing-library/react";
import { ProceedsFromTransaction } from './ProceedsFromTransaction';
import { NavigationHandler } from "../../../../../../../Utilities/Navigation/NavigationHandler";
import { MockEnvConfig } from "../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../../../store/store";
import { LoanNagivator } from "../../../../../../../Utilities/Navigation/LoanNavigator";
import { ApplicationEnv } from "../../../../../../../lib/appEnv";
import { debug } from "webpack";


const dispatch = jest.fn();
jest.mock("../../../../../../../store/actions/AssetsActions");
jest.mock("../../../../../../../store/actions/TransactionProceedsActions");

jest.mock("../../../../../../../lib/LocalDB");

const stateRealEstate = {
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
      assetTypeId: 13,
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

const stateNonRealEstate = {
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
      assetTypeId: 14,
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


const stateLoan = {
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

const noSelectionState = {
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


const history = createMemoryHistory()
beforeEach(() => {
  NavigationHandler.enableFeature = jest.fn(() => { });
  NavigationHandler.disableFeature = jest.fn(() => { });
  NavigationHandler.moveNext = jest.fn(() => { });
  NavigationHandler.isFieldVisible = jest.fn(() => true);
  NavigationHandler.getNavigationStateAsString = jest.fn(() => "");
  NavigationHandler.navigateToPath = jest.fn(() => {
    history.push(`${ApplicationEnv.ApplicationBasePath}/MyMoney/Assets/AssetsHome/AssetSourcesHome/ProceedsFromTransaction/DetailsOfProceedsFromTransaction`);
  });

  MockEnvConfig();
  MockSessionStorage();
});


describe("Proceeds from transactions when no selection is made", () => {
  test("render test section", async () => {
    const state = noSelectionState;
    const { getByTestId } = render(

      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
  });
  test("Check if 3 boxes are creating", async () => {
    const state = noSelectionState;
    const { getByTestId, debug, getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    waitFor(async () => {
      act(() => {
        let incomesourcesScreen: HTMLElement = getByTestId("incomesources-screen");
        expect(incomesourcesScreen).toBeInTheDocument();
        let threeBoxes: HTMLElement[] = getAllByTestId("list-item");
        expect(incomesourcesScreen).toHaveLength(3)
      });
    });
  });
});


describe("Proceeds from transactions when real estate is slected", () => {
  test("Test popup title exist", async () => {
    const state = stateRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    waitFor(async () => {
      act(() => {
        let popupTitle: HTMLElement = getByTestId("popup-title");
        expect(popupTitle).toHaveTextContent('Proceeds from Transactions');
      });
    });
   
  });

  test("Test State RealEstate and NonReal Estate test", async () => {
    const state = stateRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    waitFor(async () => {
      act(() => {
        let expectedProceeds: HTMLElement = getByTestId("expected-proceeds");
        let assetDescription: HTMLElement = getByTestId("asset-description");
        expect(expectedProceeds).toBeInTheDocument();
        expect(assetDescription).toBeInTheDocument();
      });
    });
  });
  test("Test State RealEstate and NonReal Estate test submit without filling ", async () => {
    const state = stateRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    waitFor(async () => {
      act(() => {
        let expectedProceeds: HTMLElement = getByTestId("expected-proceeds");
        let assetDescription: HTMLElement = getByTestId("asset-description");
        expect(expectedProceeds).toBeInTheDocument();
        expect(assetDescription).toBeInTheDocument();
      });

      let SubmitBtn: HTMLElement;
      SubmitBtn = getByTestId("net-annual-income-submit");
      act(() => {
        fireEvent.click(SubmitBtn);
      });
      
      await waitFor(() => {
        expect(getByTestId("expectedProceeds-error")).toBeInTheDocument();
        expect(getByTestId("assetDescription-error")).toBeInTheDocument();
      })
    });
  });
  test("Test State RealEstate and NonReal Estate test submit with filling ", async () => {
    const state = stateRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    let expectedProceeds: HTMLElement
    let assetDescription: HTMLElement
    waitFor(async () => {
      expectedProceeds = getByTestId("net-annual-income");
      assetDescription = getByTestId("asset-description");
      fireEvent.change(expectedProceeds, { target: { value: "1111" } })
      fireEvent.change(assetDescription, { target: { value: "Test" } })

      let SubmitBtn: HTMLElement;
      SubmitBtn = getByTestId("net-annual-income-submit");
      
      act(() => {
        fireEvent.click(SubmitBtn);
      });
    });
  });

  
   test("Test State RealEstate and NonReal Estate test submitted form data ", async () => {
    const state = stateRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    let expectedProceeds: HTMLElement
    let assetDescription: HTMLElement
    waitFor(async () => {
      expectedProceeds = getByTestId("net-annual-income");
      assetDescription = getByTestId("asset-description");
      fireEvent.change(expectedProceeds, { target: { value: "1111" } })
      fireEvent.change(assetDescription, { target: { value: "Test" } })

      let SubmitBtn: HTMLElement;
      SubmitBtn = getByTestId("net-annual-income-submit");
      act(() => {
        fireEvent.click(SubmitBtn);
      });
      expect(NavigationHandler.navigateToPath).toHaveBeenCalled();
      
    });
  }); 
  test("Test For Navigation ", async () => {
    const state = stateRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    let expectedProceeds: HTMLElement
    let assetDescription: HTMLElement
    waitFor(async () => {
      expectedProceeds = getByTestId("net-annual-income");
      assetDescription = getByTestId("asset-description");
      fireEvent.change(expectedProceeds, { target: { value: "1111" } })
      fireEvent.change(assetDescription, { target: { value: "Test" } })

      let SubmitBtn: HTMLElement;
      SubmitBtn = getByTestId("net-annual-income-submit");
      act(() => {
        fireEvent.click(SubmitBtn);
      });
      expect(NavigationHandler.navigateToPath).toHaveBeenCalled();
      
    });
  });  
});








describe("Proceeds from transactions when non real estate is slected", () => {
  test("Test popup title exist", async () => {
    const state = stateNonRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    waitFor(async () => {
      act(() => {
        let popupTitle: HTMLElement = getByTestId("popup-title");
        expect(popupTitle).toHaveTextContent('Proceeds from Transactions');
      });
    });
   
  });

  test("Test State RealEstate and NonReal Estate test", async () => {
    const state = stateNonRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    waitFor(async () => {
      act(() => {
        let expectedProceeds: HTMLElement = getByTestId("expected-proceeds");
        let assetDescription: HTMLElement = getByTestId("asset-description");
        expect(expectedProceeds).toBeInTheDocument();
        expect(assetDescription).toBeInTheDocument();
      });
    });
  });
  test("Test State RealEstate and NonReal Estate test submit without filling ", async () => {
    const state = stateNonRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    waitFor(async () => {
      act(() => {
        let expectedProceeds: HTMLElement = getByTestId("expected-proceeds");
        let assetDescription: HTMLElement = getByTestId("asset-description");
        expect(expectedProceeds).toBeInTheDocument();
        expect(assetDescription).toBeInTheDocument();
      });

      let SubmitBtn: HTMLElement;
      SubmitBtn = getByTestId("net-annual-income-submit");
      act(() => {
        fireEvent.click(SubmitBtn);
      });
      
      await waitFor(() => {
        expect(getByTestId("expectedProceeds-error")).toBeInTheDocument();
        expect(getByTestId("assetDescription-error")).toBeInTheDocument();
      })
    });
  });
  test("Test State RealEstate and NonReal Estate test submit with filling ", async () => {
    const state = stateNonRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    let expectedProceeds: HTMLElement
    let assetDescription: HTMLElement
    waitFor(async () => {
      expectedProceeds = getByTestId("net-annual-income");
      assetDescription = getByTestId("asset-description");
      fireEvent.change(expectedProceeds, { target: { value: "1111" } })
      fireEvent.change(assetDescription, { target: { value: "Test" } })

      let SubmitBtn: HTMLElement;
      SubmitBtn = getByTestId("net-annual-income-submit");
      
      act(() => {
        fireEvent.click(SubmitBtn);
      });
    });
  });

  
   test("Test State RealEstate and NonReal Estate test submitted form data ", async () => {
    const state = stateNonRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    let expectedProceeds: HTMLElement
    let assetDescription: HTMLElement
    waitFor(async () => {
      expectedProceeds = getByTestId("net-annual-income");
      assetDescription = getByTestId("asset-description");
      fireEvent.change(expectedProceeds, { target: { value: "1111" } })
      fireEvent.change(assetDescription, { target: { value: "Test" } })

      let SubmitBtn: HTMLElement;
      SubmitBtn = getByTestId("net-annual-income-submit");
      act(() => {
        fireEvent.click(SubmitBtn);
      });
      expect(NavigationHandler.navigateToPath).toHaveBeenCalled();
      
    });
  }); 
  test("Test For Navigation ", async () => {
    const state = stateRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    let expectedProceeds: HTMLElement
    let assetDescription: HTMLElement
    waitFor(async () => {
      expectedProceeds = getByTestId("net-annual-income");
      assetDescription = getByTestId("asset-description");
      fireEvent.change(expectedProceeds, { target: { value: "1111" } })
      fireEvent.change(assetDescription, { target: { value: "Test" } })

      let SubmitBtn: HTMLElement;
      SubmitBtn = getByTestId("net-annual-income-submit");
      act(() => {
        fireEvent.click(SubmitBtn);
      });
      expect(NavigationHandler.navigateToPath).toHaveBeenCalled();
      
    });
  });  
});








describe("Proceeds from transactions when loan slected", () => {
  test("Test popup title exist", async () => {
    const state = stateLoan;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    waitFor(async () => {
      act(() => {
        let popupTitle: HTMLElement = getByTestId("popup-title");
        expect(popupTitle).toHaveTextContent('Proceeds from a Loan');
      });
    });
   
  });

  test("Test State loan test", async () => {
    const state = stateLoan;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    waitFor(async () => {
      act(() => {
        let netAnnualIncome: HTMLElement = getByTestId("net-annual-income");
        let procedingCheckboxYes: HTMLElement = getByTestId("proceding-checkbox-yes");
        let procedingCheckboxNo: HTMLElement = getByTestId("proceding-checkbox-no");
        expect(netAnnualIncome).toBeInTheDocument();
        expect(procedingCheckboxYes).toBeInTheDocument();
        expect(procedingCheckboxNo).toBeInTheDocument();
      });
    });
  });
  test("Test State loan test submit without filling ", async () => {
    const state = stateLoan;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    waitFor(async () => {
      act(() => {
        let netAnnualIncome: HTMLElement = getByTestId("net-annual-income");
        let procedingCheckboxYes: HTMLElement = getByTestId("proceding-checkbox-yes");
        let procedingCheckboxNo: HTMLElement = getByTestId("proceding-checkbox-no");
        expect(netAnnualIncome).toBeInTheDocument();
        expect(procedingCheckboxYes).toBeInTheDocument();
        expect(procedingCheckboxNo).toBeInTheDocument();
      });

      let SubmitBtn: HTMLElement;
      SubmitBtn = getByTestId("net-annual-income-submit");
      act(() => {
        fireEvent.click(SubmitBtn);
      });
      
      await waitFor(() => {
        expect(getByTestId("expectedProceeds-error")).toBeInTheDocument();
        expect(getByTestId("securedByAnAsset-error-error")).toBeInTheDocument();
      })
    });
  });
  

  
    test("Test For Navigation ", async () => {
    const state = stateRealEstate;
    const { getByTestId, debug } = render(
      <Store.Provider value={{ state, dispatch }}>
        <Router history={history}>
          <ProceedsFromTransaction />
        </Router>
      </Store.Provider>
    );
    let expectedProceeds: HTMLElement
    let assetDescription: HTMLElement
    waitFor(async () => {
      expectedProceeds = getByTestId("net-annual-income");
      assetDescription = getByTestId("asset-description");
      fireEvent.change(expectedProceeds, { target: { value: "1111" } })
      fireEvent.change(assetDescription, { target: { value: "Test" } })

      let SubmitBtn: HTMLElement;
      SubmitBtn = getByTestId("net-annual-income-submit");
      act(() => {
        fireEvent.click(SubmitBtn);
      });
      expect(NavigationHandler.navigateToPath).toHaveBeenCalled();
      
    });
  });  
});