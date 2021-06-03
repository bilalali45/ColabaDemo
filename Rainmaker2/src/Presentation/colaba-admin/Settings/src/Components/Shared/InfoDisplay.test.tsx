import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { SVGInfo } from './SVG';
import { ToolTipData } from './ToolTipData';

import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import App from '../../App';
import { RequestEmailTemplateActions } from '../../Store/actions/RequestEmailTemplateActions';
import { LocalDB } from '../../Utils/LocalDB';
import { UserActions } from '../../Store/actions/UserActions';

jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/TemplateActions');
jest.mock('../../Store/actions/RequestEmailTemplateActions');
jest.mock('../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

describe('Info Display Test', () => {
    test('Test', async () => {
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
            let addEmailTemplateBtn: any;
            //Test click on menu and render add email template rendered 
            await waitFor(() => {
             addEmailTemplateBtn = getByTestId('addNewTemplate-btn');
             expect(addEmailTemplateBtn).toBeInTheDocument();
            });

          fireEvent.click(addEmailTemplateBtn);
          
          await waitFor(() => {
              const formBody = getByTestId('create-form');
              expect(formBody).toBeInTheDocument();
          });
      });

});