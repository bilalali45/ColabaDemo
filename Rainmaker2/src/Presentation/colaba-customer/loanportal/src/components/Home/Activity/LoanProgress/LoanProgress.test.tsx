import React from 'react';
import { render, fireEvent, waitFor, waitForElement, findByTestId } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { createMemoryHistory } from 'history'
import { MockEnvConfig } from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import App from '../../../../App';
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

describe('Loan Progress', () => {
    test('should render LoanProgress" ', async () => {
        const { getByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                 <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            const loanProgress = getByTestId('loan-progress-heading');

            expect(loanProgress).toHaveTextContent('Your Loan Status');
        })


    });

    test('should show color blue for the completed steps" ', async () => {
        const { getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                 <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            const stepIcons = getAllByTestId('steps-icon');

            expect(stepIcons[0].innerHTML).toBe('<i class="zmdi zmdi-check"></i>');
            expect(stepIcons[1].innerHTML).toBe('<i class="zmdi zmdi-check"></i>');
            // expect(stepIcons[2].innerHTML).toBe('<i class="zmdi zmdi-male-alt"></i>');
            expect(stepIcons[2].innerHTML).toBe('<i class="zmdi zmdi-check"></i>');
        })


    });

    test('should move to the clicked step" ', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );
        let stepIcons: any;
        let loanProgress: any
        await waitFor(() => {
            stepIcons = getAllByTestId('steps-icon');
            loanProgress = getByTestId('loan-progress');
        })

        fireEvent.click(stepIcons[0]);
        await waitFor(() => {
            expect(loanProgress).toHaveTextContent('Fill out application');
        })

        fireEvent.click(stepIcons[1]);
        await waitFor(() => {
            expect(loanProgress).toHaveTextContent('Review and submit application');
        })

        fireEvent.click(stepIcons[2]);
        await waitFor(() => {
            expect(loanProgress).toHaveTextContent('Application received');
        })
    });

})