import React from "react";
import { fireEvent, render, waitFor } from "@testing-library/react";
import {HowCanWeHelp} from './HowCanWeHelp';
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";

jest.mock('../../../../Store/actions/GettingStartedActions');
jest.mock('../../../../lib/LocalDB');

beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
  // NavigationHandler.navigation.moveNext = jest.fn(()=>{})
  NavigationHandler.getNavigationStateAsString = jest.fn(() => "");
  NavigationHandler.navigateToPath = jest.fn(() => {});
  NavigationHandler.moveNext = jest.fn(() => {});
});

describe("Getting Started Steps ", () => {
  test("Should render loan purpose", async () => {
    const { getByTestId, getAllByTestId } = render(
      <HowCanWeHelp  />
    );

    await waitFor(() => {
       let head = getByTestId('head');
       let toolTip = getByTestId('tooltip');
       let list = getByTestId('purpose-list');

       expect(head).toBeInTheDocument();
       expect(toolTip).toBeInTheDocument();
       expect(list).toBeInTheDocument();

       let items = getAllByTestId('list-item');
       expect(items).toHaveLength(3);

       let artWork = getByTestId('art-work');
       expect(artWork).toBeInTheDocument();

     });

});

test("Should click continue", async () => {
  const { getAllByTestId } = render(
    <HowCanWeHelp />
  );
  
  await waitFor(() => {
    let list:HTMLElement[] = getAllByTestId("list-item")
    expect(list[0]).toBeInTheDocument();
    fireEvent.click(list[0])
  })

});


});
