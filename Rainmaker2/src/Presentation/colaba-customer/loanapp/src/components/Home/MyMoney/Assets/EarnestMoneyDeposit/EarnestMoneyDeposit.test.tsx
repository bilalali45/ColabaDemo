import React, { createContext, useReducer, ReactFragment } from "react";
import { MemoryRouter } from "react-router-dom";
import { createMemoryHistory } from "history";
import {
  fireEvent,
  getAllByTestId,
  getByTestId,
  render,
  screen,
  waitFor,
} from "@testing-library/react";
import {EarnestMoneyDeposit} from './EarnestMoneyDeposit';
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";
import { MockEnvConfig } from "../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../store/store";
import { LoanNagivator } from "../../../../../Utilities/Navigation/LoanNavigator";



const dispatch = jest.fn();
jest.mock("../../../../../store/actions/AssetsActions");
jest.mock("../../../../../lib/LocalDB");

const state = {
    leftMenu: {
      leftMenuItems: [],
      notAllowedItems: [],
      navigation: LoanNagivator
    },
    error: {},
    loanManager: {
      loanInfo:{
        loanApplicationId: 41313,
        loanPurposeId: null,
        loanGoalId: null,
        borrowerId: 31494,
        ownTypeId: 2,
        borrowerName: "jehangir",
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
    militaryIncomeManager: {},
    otherIncomeManager: {},
    assetsManager: {}
  };

  
beforeEach(() => {
    NavigationHandler.enableFeature = jest.fn(() => {});
    NavigationHandler.disableFeature = jest.fn(() => {});
    NavigationHandler.moveNext = jest.fn(() => {});
    NavigationHandler.isFieldVisible = jest.fn(() => true);
    NavigationHandler.getNavigationStateAsString = jest.fn(() => "");
    NavigationHandler.navigateToPath = jest.fn(() => {});
    
    MockEnvConfig();
    MockSessionStorage();
  });

  describe("Asset Earnest Money", () => {
      test("Should render earnest section", async () => {
        const { getByTestId } = render(
          <MemoryRouter initialEntries={["/"]}>
            <EarnestMoneyDeposit />
            </MemoryRouter>
        );
    
        await waitFor(() => {
          expect(getByTestId('page-title')).toHaveTextContent('Assets');
          expect(getByTestId('earnest-money-form')).toBeInTheDocument();
          expect(getByTestId('subtitle')).toHaveTextContent('Have you made an earnest money deposit on this purchase?');
          expect(getByTestId('earnestMoney')).toBeInTheDocument();
          expect(getByTestId('earnestMoneyDAmount-input')).toBeInTheDocument();
          expect(getByTestId('earnestMoney-2')).toBeInTheDocument();
          expect(getByTestId('submitBtn')).toBeInTheDocument();

          });

          await waitFor(() => {
            let noRadiobtn = getByTestId("earnestMoneyDAmount-input");
            fireEvent.change(noRadiobtn, { target: { value: 900000 } })
      
          });

          let submitBtn = getByTestId('submitBtn');
          fireEvent.click(submitBtn);

        });
    });