import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  render,
  waitFor,
  screen
} from "@testing-library/react";
import { NavigationHandler } from "../../../../../../Utilities/Navigation/NavigationHandler";
import { MockSessionStorage } from "../../../../../../test_utilities/SessionStoreMock";
import { MockEnvConfig } from "../../../../../../test_utilities/EnvConfigMock";
import { Retirement } from "./Retirement";
import { MemoryRouter } from "react-router";


jest.mock("../../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../../store/actions/RetirementIncomeActions");
jest.mock("../../../../../../store/actions/IncomeActions");
jest.mock("../../../../../../lib/LocalDB")

beforeEach(() => {
    MockEnvConfig();
    MockSessionStorage();
    NavigationHandler.getNavigationStateAsString =  jest.fn(()=> "")
  NavigationHandler.navigateToPath = jest.fn(()=>{})
  NavigationHandler.moveNext = jest.fn(()=>{})
  });


  describe("Retirement Screen ", () => {
    test("should render Retirement", async () => {
        const { getByTestId , getAllByTestId} = render(
            <MemoryRouter initialEntries={["/"]}>
          <Retirement/>
          </MemoryRouter>
        );
        
        await waitFor(() => {
         
        
      });

    });
  });


