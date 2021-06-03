import React from 'react';
import { render, fireEvent, waitFor, waitForElement, findByTestId } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { createMemoryHistory } from 'history'
import { MockEnvConfig } from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import App from '../../../../App';
import { Console } from 'console';
import { StoreProvider } from '../../../../store/store';


jest.mock('axios');
jest.mock('../../../../store/actions/UserActions');
jest.mock('../../../../store/actions/LoanActions');
jest.mock('../../../../store/actions/DocumentActions');
jest.mock('../../../../services/auth/Auth');

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Documents Status', () => {
    test('should render DocumentsStatus" ', async () => {
        const { getByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            const pendingDocs = getByTestId('borrower-pending-docs');
            expect(pendingDocs).toHaveTextContent('You have 3 items to complete');
        })



    });

    test('should show the list of pending documents" ', async () => {
        const { getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            const pendingDocs = getAllByTestId('borrower-pending-doc');

            expect(pendingDocs[0]).toHaveTextContent('Alimony Income Verification');
            expect(pendingDocs[1]).toHaveTextContent('Bank Statement');
            expect(pendingDocs[2]).toHaveTextContent('Salary Slip');
        })


    });

    test('should render Get Started button" ', async () => {
        const { getByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);

            const documentsRequest = getByTestId('selected-doc-title');
            expect(documentsRequest).toHaveTextContent('Alimony Income Verification');
        })


    });

})