import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { UserActions } from '../../../Store/actions/UserActions';
import App from '../../../App';
import { StoreProvider } from '../../../Store/Store';
import { LoanOriginationSystemBody } from './LoanOriginationSystemBody';
import { AssignedRoleActions } from '../../../Store/actions/AssignedRoleActions';
import { LoanOfficersActions } from "../../../Store/actions/LoanOfficersActions";
import { LOSUsers } from './Users';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/LoanOfficersActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Utils/LocalDB');

beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/LoanOriginationSystem');
});
describe('Loan origination System Users Tab', () => {
    test('should render Only users tab', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/LoanOriginationSystem']}>
                <App />
            </MemoryRouter>
        );
        //Test setting text and sideBar menu is rendered
        const mainHead = getByTestId('main-header');
        expect(mainHead).toHaveTextContent('Settings');
        expect(getByTestId('sideBar')).toBeInTheDocument();

        const navs = getAllByTestId('sidebar-navDiv');
        expect(navs[2]).toHaveTextContent('Integrations');

        fireEvent.click(navs[2]);
        let navsLink: any;
        //Test LoanOriginationSystem rendered on menu
        await waitFor(() => {
            navsLink = getAllByTestId('sidebar-nav');
            expect(navsLink[3]).toHaveTextContent('Loan Origination System');
        });
        fireEvent.click(navsLink[3]);

        const loanoriginationsystemHeader = getByTestId('header-title-text');
        expect(loanoriginationsystemHeader).toHaveTextContent('Byte Software Integration Setting');

        const losUser = getByTestId('los-menu-user');
        expect(losUser).toHaveTextContent('Users');
        const losOrg = getByTestId('los-menu-org');
        expect(losOrg).toHaveTextContent('Organization');
        const losName = getByTestId('th-templateName');
        expect(losName).toHaveTextContent('Name');
        const losByteUserName = getByTestId('th-byteusername');
        expect(losByteUserName).toHaveTextContent('Byte User Name');


        await waitFor(() => {
            let rows = getAllByTestId('input-rows');
            let inputs = getAllByTestId('input-text');
            expect(rows).toHaveLength(2);
            expect(rows[0].children[1]).toHaveTextContent('minaz.karim');
            expect(inputs).toHaveLength(2);
        })


    });
    test('users tab input focus and update', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/LoanOriginationSystem']}>
                <App />
            </MemoryRouter>
        );
        //Test setting text and sideBar menu is rendered
        const mainHead = getByTestId('main-header');
        expect(mainHead).toHaveTextContent('Settings');
        expect(getByTestId('sideBar')).toBeInTheDocument();

        const navs = getAllByTestId('sidebar-navDiv');
        expect(navs[2]).toHaveTextContent('Integrations');

        fireEvent.click(navs[2]);
        let navsLink: any;
        //Test LoanOriginationSystem rendered on menu
        await waitFor(() => {
            navsLink = getAllByTestId('sidebar-nav');
            expect(navsLink[3]).toHaveTextContent('Loan Origination System');
        });
        fireEvent.click(navsLink[3]);

        await waitFor(() => {
            let byteUserInputb = getAllByTestId('input-text');
            fireEvent.focus(byteUserInputb[0]);
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).not.toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).not.toBeNull();
            
        });
        let byteUserInput = getAllByTestId('input-text');
        fireEvent.input(byteUserInput[0], {
            target: {
              value: "Hammad test"
            }
        });
        const saveButton = getByTestId('save-btn');
        fireEvent.click(saveButton);
        
        await waitFor(() =>{
            let inputs = getAllByTestId('input-text');
            expect(inputs[0]).toHaveValue('Hammad test');
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).toBeNull();
        });

    });
    test('users tab input focus and cancel', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/LoanOriginationSystem']}>
                <App />
            </MemoryRouter>
        );
        //Test setting text and sideBar menu is rendered
        const mainHead = getByTestId('main-header');
        expect(mainHead).toHaveTextContent('Settings');
        expect(getByTestId('sideBar')).toBeInTheDocument();

        const navs = getAllByTestId('sidebar-navDiv');
        expect(navs[2]).toHaveTextContent('Integrations');

        fireEvent.click(navs[2]);
        let navsLink: any;
        //Test LoanOriginationSystem rendered on menu
        await waitFor(() => {
            navsLink = getAllByTestId('sidebar-nav');
            expect(navsLink[3]).toHaveTextContent('Loan Origination System');
        });
        fireEvent.click(navsLink[3]);

        await waitFor(() => {
            let byteUserInputc = getAllByTestId('input-text');
            fireEvent.focus(byteUserInputc[0]);
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).not.toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).not.toBeNull();
            
        });
        let byteUserInput = getAllByTestId('input-text');
        fireEvent.input(byteUserInput[0], {
            target: {
              value: "Hammad test"
            }
        });
        const cancelButton = getByTestId('cancel-btn');
        fireEvent.click(cancelButton);
        
        await waitFor(() =>{
            let inputs = getAllByTestId('input-text');
            expect(inputs[0]).toHaveValue('Minaz Karim');
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).toBeNull();
        });

    });
    test('users tab switch organization tab', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/LoanOriginationSystem']}>
                <App />
            </MemoryRouter>
        );
        //Test setting text and sideBar menu is rendered
        const mainHead = getByTestId('main-header');
        expect(mainHead).toHaveTextContent('Settings');
        expect(getByTestId('sideBar')).toBeInTheDocument();

        const navs = getAllByTestId('sidebar-navDiv');
        expect(navs[2]).toHaveTextContent('Integrations');

        fireEvent.click(navs[2]);
        let navsLink: any;
        //Test LoanOriginationSystem rendered on menu
        await waitFor(() => {
            navsLink = getAllByTestId('sidebar-nav');
            expect(navsLink[3]).toHaveTextContent('Loan Origination System');
        });
        fireEvent.click(navsLink[3]);

        const losOrg = getByTestId('los-menu-org');
        expect(losOrg).toHaveTextContent('Organization');
        fireEvent.click(losOrg.children[0]);
        await waitFor(() =>{
            const losName = getByTestId('th-templateName');
            expect(losName).toHaveTextContent('Name');
            const losByteOrgCode = getByTestId('th-byteOrgCode');
            expect(losByteOrgCode).toHaveTextContent('Byte Organization Code');

        });
        
    });
    test('should render Only users tab with store provider', async () => {
        const { getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <LOSUsers/>
            </StoreProvider>
        );
        await waitFor(() => {
            const losName = getByTestId('th-templateName');
            expect(losName).toHaveTextContent('Name');
            const losByteUserName = getByTestId('th-byteusername');
            expect(losByteUserName).toHaveTextContent('Byte User Name');

            let rows = getAllByTestId('input-rows');
            let inputs = getAllByTestId('input-text');
            expect(rows).toHaveLength(2);
            expect(rows[0].children[1]).toHaveTextContent('minaz.karim');
            expect(inputs).toHaveLength(2);
        })
        


    });
    test('users tab input focus and update with store provider', async () => {
        const { getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <LOSUsers/>
            </StoreProvider>
        );
   
        await waitFor(() => {
            let byteUserInputd = getAllByTestId('input-text');
            fireEvent.focus(byteUserInputd[0]);
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).not.toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).not.toBeNull();
            
        });
        let byteUserInput = getAllByTestId('input-text');
        fireEvent.input(byteUserInput[0], {
            target: {
              value: "Hammad test"
            }
        });
        const saveButton = getByTestId('save-btn');
        fireEvent.click(saveButton);
        
        await waitFor(() =>{
            let inputs = getAllByTestId('input-text');
            expect(inputs[0]).toHaveValue('Hammad test');
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).toBeNull();
        });

    });
    test('users tab input focus and cancel with store provider', async () => {
        const { getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <LOSUsers/>
            </StoreProvider>
        );

        await waitFor(() => {
            let byteUserInpute = getAllByTestId('input-text');
            fireEvent.focus(byteUserInpute[0]);
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).not.toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).not.toBeNull();
            
        });
        let byteUserInput = getAllByTestId('input-text');
        fireEvent.input(byteUserInput[0], {
            target: {
              value: "Hammad test"
            }
        });
        const cancelButton = getByTestId('cancel-btn');
        fireEvent.click(cancelButton);
        
        await waitFor(() =>{
            let inputs = getAllByTestId('input-text');
            expect(inputs[0]).toHaveValue('Minaz Karim');
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).toBeNull();
        });

    });
    
})