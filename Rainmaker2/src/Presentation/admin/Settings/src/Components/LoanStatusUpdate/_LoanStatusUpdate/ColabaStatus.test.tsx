import React, { useState } from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, getByText, screen } from '@testing-library/react'
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { StoreProvider } from '../../../Store/Store';
import LoanStatusUpdate from '../LoanStatusUpdate';
import { MemoryRouter } from 'react-router-dom';
import App from '../../../App';


jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NotificationActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Store/actions/LoanStatusUpdateActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

describe('Loan Status Update List', ()=>{

    it('ColabaStatus', async()=>{
        const {getAllByTestId, getByText, debug} = render(<StoreProvider><LoanStatusUpdate/></StoreProvider>);
        debug();
        
        expect(getAllByTestId('loader')).toBeTruthy();
    
        await waitFor(()=>{
            expect(getAllByTestId('contentHeader')).toBeTruthy();
            expect(getByText('Colaba Status')).toBeVisible();
        })
    });

    test('Loan Status Update status List', async () => {
        const { getByTestId, getAllByTestId,container } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
                 <StoreProvider>
                 <App/>
                 </StoreProvider>
               
            </MemoryRouter>
            );

            const mainHead = getByTestId('main-header');
            expect(mainHead).toHaveTextContent('Settings');
            expect(getByTestId('sideBar')).toBeInTheDocument();
                
            const navs = getAllByTestId('sidebar-navDiv');
            expect(navs[1]).toHaveTextContent('Needs List');
                
            fireEvent.click(navs[1]);
            let navsLink: any;
            await waitFor(() => {
                navsLink = getAllByTestId('sidebar-nav');
                expect(navsLink[5]).toHaveTextContent('Loan Status Update');      
                });
            fireEvent.click(navsLink[5]);

            await waitFor(() => {
                let Header = getAllByTestId('header-title-text');         
                expect(Header[0].children[0]).toHaveTextContent('Loan Status Update');    
                expect(getByTestId('new-template-container')).toBeInTheDocument();                          
                });
                
                let loanStatusSection = getAllByTestId('loan-statuses');
                expect(loanStatusSection).toBeTruthy();
                let fromStatuses = getAllByTestId('loan-statuses-from');
                expect(fromStatuses).toHaveLength(3);
                
                fireEvent.click(fromStatuses[0]);
                let toHeader : any;
                await waitFor(() => {
                    toHeader = getByTestId('to-header');
                    expect(screen.queryByTestId('new-template-container')).toBeNull();
                });
                fireEvent.click(toHeader);
                let toStatuses: any;
                await waitFor(() => {
                    toStatuses  = getAllByTestId('loan-statuses-to');
                    expect(toStatuses).toHaveLength(2);
                });
                expect(toStatuses[1]).toHaveTextContent('Underwriting');
                fireEvent.click(toStatuses[1]);
                let fromHeader: any;

                await waitFor(() => {
                 fromHeader = getByTestId('from-header');
                 let fromStatusText = getByTestId('from-status-text');               
                 expect(fromStatusText).toHaveTextContent('Application Submitted');
                });

                fireEvent.click(fromHeader);

                await waitFor(() => {
                 let activeHeader = getAllByTestId('contentSubHeader');
                 expect(activeHeader[0]).toHaveClass('active');
                 
                });

                fromStatuses = getAllByTestId('loan-statuses-from');
                expect(fromStatuses[1]).toHaveTextContent('Processing');
                expect(screen.queryByTestId('alert-box')).toBeNull();
                fireEvent.click(fromStatuses[1]);
                
                await waitFor(() => {
                    expect(screen.queryByTestId('alert-box')).not.toBeNull();
                   });

                fireEvent.click(getByTestId('btnno'));

                await waitFor(() => {
                    expect(screen.queryByTestId('alert-box')).toBeNull();
                   });

                fireEvent.click(fromStatuses[1]);

                await waitFor(() => {
                    expect(screen.queryByTestId('alert-box')).not.toBeNull();
                   });

                fireEvent.click(getByTestId('btnyes'));
                await waitFor(() => {         
                    expect(getByTestId('from-status-text')).toHaveTextContent('Processing');
                });
        });
});

