import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { UserActions } from '../../Store/actions/UserActions';
import { NotificationActions } from '../../Store/actions/NotificationActions';
import App from '../../App';
import {NotificationHeader} from './_Notification/NotificationHeader';
import { StoreProvider } from '../../Store/Store';
import { NotificationBody } from './_Notification/NotificationBody';


jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');

beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Notification');
});

describe('Notification Settings', () => {
   
   test('should render Only Notifications Header', async () => {
      const { getByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']}>
            <NotificationHeader/>
          </MemoryRouter>
      );

      const notificationHeader = getByTestId('notification-header');
      expect(notificationHeader).toHaveTextContent('Notifications');
  });

   test('should render Notifications text', async () => {
        const { getByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

        const notificationHeader = getByTestId('notification-header');
        expect(notificationHeader).toHaveTextContent('Notifications');
    });

    test('should render ToolTip', async () => {
        const { getByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

        const notificationHeader = getByTestId('header-toolTip');
        expect(notificationHeader.children[0]).toHaveClass("info-display")
    });

    test('should click on toolTip and open dropdown', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

        const notificationHeaderToolTip = getAllByTestId('toolTip');
        fireEvent.click(notificationHeaderToolTip[0]);

        await waitFor(() => {
        const notificationHeaderToolTipDrpbx = getByTestId('toolTip-dropdown');
        expect(notificationHeaderToolTipDrpbx).toHaveTextContent('Notification settings');
        });

    });

    test('should close tooltip when click on body', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

        const notificationHeaderToolTip = getAllByTestId('toolTip');
        let body = getByTestId('app-body');
        fireEvent.click(notificationHeaderToolTip[0]);
       
        await waitFor(() => {
        const notificationHeaderToolTipDrpbx = getByTestId('toolTip-dropdown');
        expect(notificationHeaderToolTipDrpbx).toHaveTextContent('Notification settings');
        fireEvent.click(body);
        
        });
        
        const dropDown = screen.queryByText('Notification settings')
        expect(dropDown).toBeNull() 
      
    });

    test('should render Table heads', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

        const tableRows = getAllByTestId('table-row');
        expect(tableRows[0].children[0]).toHaveTextContent("Type");
        expect(tableRows[0].children[1]).toHaveTextContent("Set your preferences");
    });

    test('should render Table rows Heads', async () => {
        const { getByTestId, getAllByTestId } = render(
         <StoreProvider>
            <NotificationBody/>
         </StoreProvider>
        );

        await waitForDomChange();

        const tableRows = getAllByTestId('table-row');
        expect(tableRows[1].children[0]).toHaveTextContent("Document Submit");
        expect(tableRows[2].children[0]).toHaveTextContent("Loan Application Submitted");
        expect(tableRows[3].children[0]).toHaveTextContent("Loan Funded");
       
    });

    test('should render 3 rows', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

        const tableRows = getAllByTestId('table-row');
        expect(tableRows).toHaveLength(4);
       
    });

    test('should render "Documen submits" rows options and  toolTip', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

        const tableRows = getAllByTestId('table-row');
        expect(tableRows[1].children[1]).toHaveTextContent("Off");
        expect(tableRows[1].children[1]).toHaveTextContent("Immediate");
        expect(tableRows[1].children[1]).toHaveTextContent("Delayed");
        expect(tableRows[1].children[2].children[0]).toHaveClass("info-display ");
       
    });

    test('should render "Loan Application submitted" rows options and  toolTip', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

        const tableRows = getAllByTestId('table-row');
        expect(tableRows[2].children[1]).toHaveTextContent("Off");
        expect(tableRows[2].children[1]).toHaveTextContent("Immediate");
        expect(tableRows[2].children[1]).toHaveTextContent("Delayed");
        expect(tableRows[2].children[2].children[0]).toHaveClass("info-display ");
       
    });

    test('should render "Loan funded" rows options and  toolTip', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

        const tableRows = getAllByTestId('table-row');
        expect(tableRows[3].children[1]).toHaveTextContent("Off");
        expect(tableRows[3].children[1]).toHaveTextContent("Immediate");
        expect(tableRows[3].children[1]).toHaveTextContent("Delayed");
        expect(tableRows[3].children[2].children[0]).toHaveClass("info-display ");
       
    });

    test('check input type is radio button', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

       const input = getByTestId('Document Submit-3-input-check');
       expect(input).toHaveAttribute('type', 'radio');
       
    });

    test('check "Document Submit" "Immediate" radio button is selected', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

       const input = getByTestId('Document Submit-1-input-check');
       expect(input).toBeChecked();
       
    });

    test('check "Loan Application Submitted" "Off" radio button is selected', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

       const input = getByTestId('Loan Application Submitted-3-input-check');
       expect(input).toBeChecked();
       
    });
    
    test('check "Loan Funded" "Delayed" radio button is selected', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

       const input = getByTestId('Loan Funded-2-input-check');
       expect(input).toBeChecked();
       
    });

    test('check "Loan Funded" "Delayed" radio button is selected and delayed timer dropdown is rendered', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

       const input = getByTestId('Loan Funded-2-input-check');
       const dropDown = screen.queryByTestId('Loan Funded-input-select');
       expect(input).toBeChecked();
       expect(dropDown).not.toBeNull();          
    });

    test('check "Loan Funded" "Delayed" radio button is selected and delayed timer given value is selected', async () => {
        const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Notification']} >
            <App/>
         </MemoryRouter>
        );

        await waitForDomChange();

       const input = getByTestId('Loan Funded-2-input-check');  
       let options : any = getAllByTestId('Loan Funded-select-option');

       expect(input).toBeChecked();    
       expect(options[3].selected).toBeTruthy();                          
    });

    test('check onchange functionality on selecting radio button', async () => {  
      const { getByTestId, getAllByTestId } = render(
       <MemoryRouter initialEntries={['/Notification']} >
          <App/>
       </MemoryRouter>
      );

      await waitForDomChange();

      const inputImmediate: any = getByTestId('Document Submit-1-input-check');
      expect(inputImmediate.value).toBe('Immediate')
     
      let radio: any = getByTestId('Document Submit-3-input-check');
      fireEvent.change(radio, { target: { value: "Off" } });
    
      await waitFor(() => {     
      expect(radio.value).toBe("Off");
      })                      
  });

    test('Selecting "delayed" options should render delayed timer dropdown ', async () => {
     const { getByTestId, getAllByTestId } = render(
      <MemoryRouter initialEntries={['/Notification']} >
       <App/>
      </MemoryRouter>
     );

     await waitForDomChange();

     let radio: any;
      await waitFor(() => {
         radio = getByTestId('Document Submit-2-input-check');
      })
         
      await waitFor(() => {     
      fireEvent.change(radio, { target: { checked: true } });
      expect(radio).toBeChecked(); 
     
   });

    expect(getByTestId('Document Submit-input-select')).toBeInTheDocument();

  });
   
    test('change dropdown value', async () => {
     const { getByTestId, getAllByTestId } = render(
    <MemoryRouter initialEntries={['/Notification']} >
       <App/>
    </MemoryRouter>
     );

   await waitForDomChange();
   const drpdwn: any = getByTestId('Loan Funded-input-select');
   fireEvent.change(drpdwn, { target: { value: "30" } });
 
   await waitFor(() => { 
      let options : any = getAllByTestId('Loan Funded-select-option');
       expect(drpdwn).toHaveValue('30');  
       expect(options[5].selected).toBeTruthy();   
     
   })                      
});

});