import React from 'react';
import { render, waitForDomChange } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { createMemoryHistory } from 'history'
import { MockEnvConfig } from '../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../test_utilities/LocalStoreMock';
import App from '../../../App';
import { Console } from 'console';  


jest.mock('axios');
jest.mock('../../../store/actions/UserActions');
jest.mock('../../../store/actions/LoanActions');
jest.mock('../../../store/actions/DocumentActions');
jest.mock('../../../services/auth/Auth');

const Url = '/loanportal/activity/3'


beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Footer components rendering', () => {
    test('should render Footer" ', async () => {
        const { getByText} = render(
            <MemoryRouter initialEntries={[Url]}>
                <App />
            </MemoryRouter>
        );
        await waitForDomChange();
        const footerText = getByText(/Copyright 2002 – /);
        expect(footerText).toBeInTheDocument();
    });
})