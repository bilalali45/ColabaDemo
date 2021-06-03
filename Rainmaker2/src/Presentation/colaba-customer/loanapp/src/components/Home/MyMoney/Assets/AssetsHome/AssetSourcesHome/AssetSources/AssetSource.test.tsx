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
import {AssetSource} from './AssetSource';
import { NavigationHandler } from "../../../../../../../Utilities/Navigation/NavigationHandler";
import { MockEnvConfig } from "../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../test_utilities/SessionStoreMock";

const dispatch = jest.fn();
jest.mock("../../../../../../../store/actions/AssetsActions");
jest.mock("../../../../../../../lib/LocalDB");

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


  
  describe("Asset Source List", () => {
    test("Should render Asset Source List", async () => {
      const { getByTestId , getAllByTestId} = render(
        <MemoryRouter initialEntries={["/"]}>
          <AssetSource />
          </MemoryRouter>
      );

      let radioBoxes: any;
      await waitFor(() => {      
        expect(getAllByTestId('list-div')).toHaveLength(6);
         radioBoxes = getAllByTestId('radioBox');
        expect(radioBoxes).toHaveLength(6);
        });
        await waitFor(() => {
              fireEvent.click(radioBoxes[0]);  
        });
      });
  });