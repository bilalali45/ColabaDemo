import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
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

describe('Contact Us', () => {
    test('should render ContactUs" ', async () => {
        const { getByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <App />
            </MemoryRouter>
        );

        await waitForDomChange();


        const contactUs = getByTestId('contact-us');

        expect(contactUs).toHaveTextContent('Contact Us');
        expect(contactUs).toHaveTextContent('(123) 456-789');
        expect(contactUs).toHaveTextContent('john@doe.com');
        expect(contactUs).toHaveTextContent('www.johndoe.com');
    });
})