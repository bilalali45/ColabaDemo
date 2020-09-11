import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor, waitForElement } from '@testing-library/react'
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

describe('LoanStatus', () => {
    test('should render LoanStatus" ', async () => {

        const { getByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <App />
            </MemoryRouter>
        );

        await waitForDomChange();

        const loanStatus = getByTestId('loanStatus');

        expect(loanStatus).toHaveTextContent('Street 2 # 52 Houston, TEXAS 77098');
        expect(loanStatus).toHaveTextContent('Single Family Detached');
        expect(loanStatus).toHaveTextContent('Purchase a home');
        expect(loanStatus).toHaveTextContent('$288,000');

    });

})