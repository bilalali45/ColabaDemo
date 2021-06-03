import React from "react";
import { fireEvent, render, waitFor } from "@testing-library/react";
import { MockEnvConfig } from "../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../Utilities/Navigation/NavigationHandler";
import { GettingStarted } from "./GettingStarted";
import { MemoryRouter } from "react-router";


jest.mock('../../../Store/actions/GettingStartedActions');
jest.mock('../../../Store/actions/CommonActions');
jest.mock('../../../Store/actions/BorrowerActions');
jest.mock('../../../Store/actions/MaritalStatusActions');

jest.mock('../../../lib/LocalDB');

beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
  // NavigationHandler.navigation.moveNext = jest.fn(()=>{})
  NavigationHandler.getNavigationStateAsString = jest.fn(() => "");
  NavigationHandler.navigateToPath = jest.fn(() => {});
  NavigationHandler.moveNext = jest.fn(() => {});
});

describe("Getting Started Steps ", () => {
  test("Should render Loan officer detail", async () => {
    const { getByTestId } = render(
        <MemoryRouter initialEntries={["/"]}>
      <GettingStarted />
      </MemoryRouter>
    );
    await waitFor(() => {
        const container:HTMLElement = getByTestId("gs-container");
        
        expect(container).toBeInTheDocument();
    })

  });

});
