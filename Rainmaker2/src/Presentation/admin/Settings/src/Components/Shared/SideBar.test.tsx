import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import {
    MemoryRouter,
    BrowserRouter as Router,
    Switch,
    Route,
    Redirect,
    Link
  } from 'react-router-dom';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen, getByRole, getByTestId } from '@testing-library/react'
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { SVGUser, SVGTodoList, SVGUsers, SVGManageUsers, SVGTeamRoles, SVGNeedList, SVGDocumentTemplates, SVGRequestEmailTemplates, SVGIntegrations, SVGLoanOriginationSystem, SVGNeedListReminderEmails, SVGLoanStatusUpdate } from './SVG';
import { SideBar } from './SideBar';
import ContextAwareToggle from './ContextAwareToggle';
import App from '../../App';
import { StoreProvider } from '../../Store/Store';
import { renderIntoDocument } from 'react-dom/test-utils';


// beforeEach(() => {
//     EnvConfigMock();
//     LocalStorageMock();
// });

describe('Sidebar', ()=>{

    test('Sidebar : Render', async()=>{
        const sidebar = render(<StoreProvider>
            <Router basename="/Setting">
              <Switch>
                <SideBar />
              </Switch>
            </Router>
          </StoreProvider>)
        
        // expect(getByTestId('sideBar')).toBeTruthy();
        expect(sidebar).toBeTruthy();
    });

    // test('ContextAwareToggle : Sidebar', async()=>{
    //     const {getByTestId,getByRole, debug} = render(<StoreProvider>
    //         <MemoryRouter initialEntries={["/Setting/NeedsListReminder"]}>
    //             <ContextAwareToggle/>
    //         </MemoryRouter>
    //     </StoreProvider>);
    //     expect(getByRole('button')).toBeTruthy();
    //     expect(getByRole('button').children[0]).toBeTruthy();
        
    //     fireEvent.click(getByRole('button'));
        
    //     await waitFor(()=>{
    //         expect(getByRole('button')).toHaveClass('settings__accordion-signable-header');
    //     });
    // });

    // test('SideBar : Render', async()=>{
    //     const {getByTestId,getByRole, debug} = render(
    //     <StoreProvider>
    //         <MemoryRouter initialEntries={["/Setting/NeedsListReminder"]}>
    //             <SideBar/>
    //         </MemoryRouter>
    //     </StoreProvider>);
    //     expect(getByTestId('sideBar')).toBeTruthy();

    //     await waitFor(()=>{
    //         render(<SVGUser />);
    //         expect(getByRole('svg')).toBeTruthy()
    //         debug()
    //     })
    // })
})