import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, getByText, getByLabelText } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { MockEnvConfig } from '../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../test_utilities/LocalStoreMock';
import App from '../../../App';


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

describe('Header components rendering', () => {
    test('should render logo" ', async () => {
        const { getByAltText, getByText } = render(
            <MemoryRouter initialEntries={[Url]}>
                <App />
            </MemoryRouter>
        );
        await waitForDomChange();
        const logo = getByAltText(/Texas Trust Home Loans/);
        expect(logo).toBeInTheDocument(); 

        const logoLink = getByText((content, element) => element.className === "logo-link")
        expect(logoLink).toHaveAttribute('href', "/")
    });

    test('should render hello user" ', async () => {
        const { getByText } = render(
            <MemoryRouter initialEntries={[Url]}>
                <App />
            </MemoryRouter>
        );
        await waitForDomChange();
        const helloMsg = getByText(/Hello, /);
        expect(helloMsg).toBeInTheDocument(); 
    });

    test('should render dropdown menu', async () => {
        const { getByText, getAllByText, findByLabelText } = render(
            <MemoryRouter initialEntries={[Url]}>
                <App />
            </MemoryRouter>
        );
        await waitForDomChange();

        const helloMsg = getByText(/Hello, /);
        fireEvent.click(helloMsg);

        await waitForDomChange();

        const DashBoardText  =  getAllByText('Dashboard');
        const ChangePasswordText  =  getByText('Change Password');
        const SignOutText  =  getByText('Sign Out');

        expect(DashBoardText[0]).toBeInTheDocument();
        expect(DashBoardText[0]).toHaveAttribute('href', "#") 
        expect(ChangePasswordText).toBeInTheDocument();
        expect(ChangePasswordText).toHaveAttribute('href', "#") 
        expect(SignOutText).toBeInTheDocument();
        expect(SignOutText).toHaveAttribute('href', "#") 
    });

})

