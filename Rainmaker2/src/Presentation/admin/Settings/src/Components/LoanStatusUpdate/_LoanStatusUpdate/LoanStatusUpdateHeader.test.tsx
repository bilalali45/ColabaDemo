import React, {useState} from 'react';
import { render, fireEvent, waitFor, screen } from '@testing-library/react'
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import App from '../../../App';
import { StoreProvider } from '../../../Store/Store';
import {LoanStatusUpdateHeader} from './LoanStatusUpdateHeader';
import { MemoryRouter } from 'react-router';


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

describe('Loan Status Update : Header', ()=>{

    
    test('Loan Status Update Header : Heading', async () => {
        const { getByText } = render(<StoreProvider><LoanStatusUpdateHeader/></StoreProvider>);       
        const linkElement = getByText(/LOAN STATUS UPDATE/i);
        expect(linkElement).toBeInTheDocument();
             
    });

    test('Loan Status Update Header : Info Component', async () => {
        const { getByTestId, debug } = render(<StoreProvider><LoanStatusUpdateHeader/></StoreProvider>);       
        expect(getByTestId('toolTip')).toBeTruthy();
        expect(getByTestId('toolTip')).toHaveClass("info-display--icon");      
    });

    test('Loan Status Update Header : Disable/Enable Toggler', async () => {
        const { getByTestId, debug } = render(<StoreProvider><LoanStatusUpdateHeader/></StoreProvider>);     
        expect(getByTestId('disEna')).toBeTruthy();
        expect(getByTestId('disEna')).toHaveTextContent('Disable/Enable');
        fireEvent.click(getByTestId('togglerDefault').children[0]);
        debug(); 
        expect(getByTestId('togglerDefault').children[0]).toHaveProperty('checked');
    });

    test('Loan Status Update Header : Disable/Enable Toggler All', async () => {
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
            let toggler: any;

            await waitFor(() => {
                let Header = getAllByTestId('header-title-text');         
                expect(Header[0].children[0]).toHaveTextContent('Loan Status Update');
                toggler = getByTestId('togglerDefault');
                expect(toggler).toBeInTheDocument();                   
                });
                fireEvent.click(toggler);
    });

    test('Loan Status Update status selection', async () => {
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
    });
})