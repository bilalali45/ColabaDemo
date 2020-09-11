import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { createMemoryHistory } from 'history'
import { MockEnvConfig } from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import App from '../../../../App';


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
                <App />
            </MemoryRouter>
        );

        await waitForDomChange();


        const loanProgress = getByTestId('loan-progress');

        expect(loanProgress).toHaveTextContent('Your Loan Progress');
    });

    test('should show color blue for the completed steps" ', async () => {
        const { getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <App />
            </MemoryRouter>
        );

        await waitForDomChange();


        const stepIcons = getAllByTestId('steps-icon');

        expect(stepIcons[0].innerHTML).toBe('<i class="zmdi zmdi-check"></i>');
        expect(stepIcons[1].innerHTML).toBe('<i class="zmdi zmdi-check"></i>');
        expect(stepIcons[2].innerHTML).toBe('<i class="zmdi zmdi-male-alt"></i>');
    });

})