import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  render,
  waitFor,
  screen
} from "@testing-library/react";
import { MockEnvConfig } from "../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../store/store";
import {IncomeSources} from './IncomeSources';
import IncomeActions from "../../../../../store/actions/IncomeActions";
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";


jest.mock("../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../store/actions/IncomeActions");
jest.mock("../../../../../lib/LocalDB")

IncomeActions
beforeEach(() => {
    MockEnvConfig();
    MockSessionStorage();
    NavigationHandler.getNavigationStateAsString =  jest.fn(()=> "")
  NavigationHandler.navigateToPath = jest.fn(()=>{})
  NavigationHandler.moveNext = jest.fn(()=>{})
  });


  describe("Income Source List Screen ", () => {
    test("Check popup modal and list", async () => {
        const { getByTestId , getAllByTestId} = render(
          <IncomeSources/>
        );
        
        await waitFor(() => {
          expect(getAllByTestId('list-div')).toHaveLength(7);
        
      });

      let list = getAllByTestId('list-item');
      fireEvent.click(list[0]);

      await waitFor(() => {
        expect(screen.getByTestId('incomesources-screen')).toBeNull();
      
      });

    });
  });


