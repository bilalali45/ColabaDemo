import React from "react";
import { fireEvent, render, waitFor,screen } from "@testing-library/react";
import {PurchaseProcessState} from './PurchaseProcessState';
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { LocalDB } from "../../../../lib/LocalDB";

jest.mock('../../../../Store/actions/GettingStartedActions');
//jest.mock('../../../../lib/LocalDB');

beforeEach(() => {
  MockEnvConfig();
  MockSessionStorage();
  NavigationHandler.getNavigationStateAsString = jest.fn(() => "");
  NavigationHandler.navigateToPath = jest.fn(() => {});
  NavigationHandler.moveNext = jest.fn(() => {});
  NavigationHandler.moveBack = jest.fn(() => {});
  NavigationHandler.enableFeature = jest.fn(() => {});
  NavigationHandler.disableFeature=jest.fn(() => {});
  LocalDB.getLoanAppliationId = jest.fn(()=> 12);
});

describe("Getting Started Steps ", () => {
    test("Should render loan purpose", async () => {
      const { getByTestId, getAllByTestId } = render(
        <PurchaseProcessState  />
      );
  
      await waitFor(() => {
         let head = getByTestId('head');
         let toolTip = getByTestId('tooltip');
         let list = getByTestId('goal-div');
  
         expect(head).toBeInTheDocument();
         expect(toolTip).toBeInTheDocument();
         expect(list).toBeInTheDocument();
  
         let items = getAllByTestId('list-item');
         expect(items).toHaveLength(4);
  
         let artWork = getByTestId('art-work');
         expect(artWork).toBeInTheDocument();
           fireEvent.click(items[0]);
       });
       await waitFor(() => {

       });
  
  });

  test("Should moveback", async () => {

    NavigationHandler.isNavigatedBack = jest.fn(()=> true);
    NavigationHandler.resetNavigationBack = jest.fn(() => {});

    const { getByTestId, getAllByTestId } = render(
      <PurchaseProcessState  />
    );

    await waitFor(() => {
      let  mainDiv= screen.queryByTestId('purchase-process-state')
       expect(mainDiv).not.toBeInTheDocument();     
     });

    });
  
  test("Should render only loan goal", async () => {    
      const { getByTestId, getAllByTestId } = render(
        <PurchaseProcessState  />
      );
  
       await waitFor(() => {
         
       });
  
  });

  });

  