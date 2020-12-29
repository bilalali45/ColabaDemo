import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { UserActions } from '../../../Store/actions/UserActions';
import App from '../../../App';
import { StoreProvider } from '../../../Store/Store';
import Organization from '../../../Entities/Models/Organization';
import { LOSOrganization } from './Organization';
jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/OrganizationActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Utils/LocalDB');

describe('Loan origination System Organization Tab', () => {
    test('should render Only organization tab', async () => {
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
            let rows = getAllByTestId('input-rows');
            expect(rows).toHaveLength(3);
            expect(rows[0].children[1]).toHaveTextContent('AHCLending');
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).toBeNull();
        });
        
    });
    test('should render Only organization tab for system role', async () => {
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
            let rows = getAllByTestId('input-rows');
            expect(rows).toHaveLength(3);
            expect(rows[0].children[1]).toHaveTextContent('AHCLending');
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).toBeNull();
        });
        
    });
    test('should render Only Organization tab with store provider', async () => {
        const { getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <LOSOrganization/>
            </StoreProvider>
        );
        await waitFor(() => {
            const losName = getByTestId('th-templateName');
            expect(losName).toHaveTextContent('Name');
            const losByteOrgCode = getByTestId('th-byteOrgCode');
            expect(losByteOrgCode).toHaveTextContent('Byte Organization Code');
            let rows = getAllByTestId('input-rows');
            expect(rows).toHaveLength(3);
            expect(rows[0].children[1]).toHaveTextContent('AHCLending');
            let saveBtn = screen.queryByTestId('save-btn');
            expect(saveBtn).toBeNull();
            let cancelBtn = screen.queryByTestId('cancel-btn');
            expect(cancelBtn).toBeNull();
        })
        


    });
})