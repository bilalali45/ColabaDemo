import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, getByText } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { MockEnvConfig } from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import App from '../../../../App';
import { Console } from 'console';  


jest.mock('axios');
jest.mock('../../../../store/actions/UserActions');
jest.mock('../../../../store/actions/LoanActions');
jest.mock('../../../../store/actions/DocumentActions');
jest.mock('../../../../services/auth/Auth');

const Url = '/loanportal/activity/3'


beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('ContactUs components rendering', () => {
    test('should render ContactUs" ', async () => {
        const { getByTestId} = render(
            <MemoryRouter initialEntries={[Url]}>
                <App />
            </MemoryRouter>
        );
        await waitForDomChange();
        const conatctUsdiv = getByTestId('contact-us');
        expect(conatctUsdiv).toHaveTextContent('Contact Us');

    });

    test('should render image avatar" ', async () => {
        const { getByAltText } = render(
            <MemoryRouter initialEntries={[Url]}>
                <App />
            </MemoryRouter>
        );
        await waitForDomChange();
        const imageAvatar = getByAltText('contact us user image');
        expect(imageAvatar).toBeInTheDocument(); 
    });

    test('should render company Name" ', async () => {
        const { getByTitle } = render(
            <MemoryRouter initialEntries={[Url]}>
                <App />
            </MemoryRouter>
        );
        await waitForDomChange();
        const companyName = getByTitle('http://www.johndoe.com');
        expect(companyName).toBeInTheDocument(); 
        expect(companyName).toHaveAttribute('href', "http://www.johndoe.com")
        expect(companyName).toHaveTextContent('John Doe');
    });

    test('should render contact number" ', async () => {
        const { getByText, getByTitle} = render(
            <MemoryRouter initialEntries={[Url]}>
                <App />
            </MemoryRouter>
        );

        await waitForDomChange();
        const phoneIcon = getByText((content, element) => element.className === "zmdi zmdi-phone")
        expect(phoneIcon).toBeInTheDocument()

        const phoneNumber = getByTitle('123456789');
        expect(phoneNumber).toBeInTheDocument(); 
        expect(phoneNumber).toHaveAttribute('href', "tel:123456789")
        expect(phoneNumber).toHaveTextContent('(123) 456-789');
    });

    test('should render contact email" ', async () => {
        const { getByText, getByTitle} = render(
            <MemoryRouter initialEntries={[Url]}>
                <App />
            </MemoryRouter>
        );

        await waitForDomChange();
        const emailIcon = getByText((content, element) => element.className === "zmdi zmdi-email")
        expect(emailIcon).toBeInTheDocument()
        const email = getByTitle('john@doe.com');
        expect(email).toBeInTheDocument(); 
        expect(email).toHaveAttribute('href', "mailto:john@doe.com")
        expect(email).toHaveTextContent('john@doe.com');
    });

    test('should render website link" ', async () => {
        const { getByText, getByTitle} = render(
            <MemoryRouter initialEntries={[Url]}>
                <App />
            </MemoryRouter>
        );

        await waitForDomChange();
        const websiteIcon = getByText((content, element) => element.className === "zmdi zmdi-globe-alt")
        expect(websiteIcon).toBeInTheDocument()
        const website = getByTitle('www.johndoe.com');
        expect(website).toBeInTheDocument(); 
        expect(website).toHaveAttribute('href', "http://www.johndoe.com")
        expect(website).toHaveTextContent('www.johndoe.com');
        expect(website).not.toHaveTextContent("http://")
    });

});



