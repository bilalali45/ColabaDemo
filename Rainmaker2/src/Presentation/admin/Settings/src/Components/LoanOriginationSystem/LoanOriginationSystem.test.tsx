import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import {RequestEmailTemplatesHeader} from '../RequestEmailTemplates/_RequestEmailTemplates/RequestEmailTemplatesHeader';
import App from '../../App';
import { StoreProvider } from '../../Store/Store';



jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/TemplateActions');
jest.mock('../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Setting/RequestEmailTemplates');
});

describe('Request Email Template', () => {
  test('Should render "Request Email Templates" on header', async () => {

    const { getByTestId } = render(
        <StoreProvider>
          <RequestEmailTemplatesHeader/>
        </StoreProvider>
       );

       const templateHeader = getByTestId('header-title-text');
       expect(templateHeader).toHaveTextContent('Request Email Templates');
  });

  test('Should render Tooltip on header', async () => {

    const { getByTestId } = render(
        <StoreProvider>
           <RequestEmailTemplatesHeader/>
        </StoreProvider>
       );

       const notificationHeader = getByTestId('header-toolTip');
        expect(notificationHeader.children[0]).toHaveClass("info-display")
  });

  test('Should open dropdown on hover and vise versa', async () => {
    const { getByTestId } = render(
      <StoreProvider>
        <RequestEmailTemplatesHeader/>
      </StoreProvider>
    );


    const templateHeaderToolTip = getByTestId('toolTip');
    fireEvent.mouseEnter(templateHeaderToolTip);

    await waitFor(() => {
    const templateHeaderToolTipDrpbx = getByTestId('toolTip-dropdown');
    expect(templateHeaderToolTipDrpbx).toBeInTheDocument();
    });

 });

  test('Should click on menu of Email template and  render "Add email Template" text on header', async () => {
    const { getByTestId, getAllByTestId } = render(
        <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
           <App/>
        </MemoryRouter>
       );
      
       //Test setting text and sideBar menu is rendered
       const mainHead = getByTestId('main-header');
       expect(mainHead).toHaveTextContent('Settings');
       expect(getByTestId('sideBar')).toBeInTheDocument();

       const navs = getAllByTestId('sidebar-navDiv');
       expect(navs[1]).toHaveTextContent('Needs List');

       fireEvent.click(navs[1]);
       let navsLink: any;
       
       //Test Request Email template rendered on menu
       await waitFor(() => {
         navsLink = getAllByTestId('sidebar-nav');
         expect(navsLink[2]).toHaveTextContent('Request Email Templates');      
       });

        fireEvent.click(navsLink[2]);
        
        //Test click on menu and render add email template rendered 
        await waitFor(() => {
         const addEmailTemplateBtn = getByTestId('add-emailTemplate');
         expect(addEmailTemplateBtn).toBeInTheDocument();
         expect(addEmailTemplateBtn).toHaveTextContent('Add Email Template');
        });
      

  });

});