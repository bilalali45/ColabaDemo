import React from 'react';
import { fireEvent, render, waitFor } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../Store/Store';
import ResetPassword from './ResetPassword';

jest.mock('../../lib/localStorage');
jest.mock('../../Store/actions/UserActions');
// jest.mock('../../../../../Store/actions/TemplateActions');

const Url = '/ResetPassword?key=MTF8YjE2OGZiMzItNTdkZi00Y2Y2LTk1NjEtMjc3YzVhMmY1OGNl'

beforeEach(() => {
    // MockEnvConfig();
    // MockLocalStorage();
});

describe('Reset Password Section ', () => {

    test('Should show Reset Password heading ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ResetPassword ></ResetPassword>
                </MemoryRouter>
            </StoreProvider>
        );
        
        expect(getByTestId('reset-pass-header')).toBeInTheDocument();
        await waitFor(() => {
        const resetPassHeader = getByTestId("reset-pass-header");
        expect(resetPassHeader).toBeInTheDocument();

        expect(resetPassHeader).toHaveTextContent("Create a New Password")
        })
    });

    test('Should show Reset Password Btn ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                <ResetPassword ></ResetPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const resetPasswordBtn = getByTestId("reset-pass-btn");
                expect(resetPasswordBtn).toBeInTheDocument();

                expect(resetPasswordBtn).toHaveTextContent("RESET PASSWORD")
            })
    });

    test('Should show Password txt ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ResetPassword ></ResetPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const PasswordTxt = getByTestId("password");
                expect(PasswordTxt).toBeInTheDocument();

                expect(PasswordTxt).toHaveTextContent("Password")
            })
    });

    test('Should show Confirm Password txt ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ResetPassword ></ResetPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const confirmPasswordTxt = getByTestId("confirmPassword");
                expect(confirmPasswordTxt).toBeInTheDocument();

                expect(confirmPasswordTxt).toHaveTextContent("Confirm Password")
            })
    });

    test('Should show password input ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                <ResetPassword></ResetPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const EmailTxt = getByTestId("reset-password-input");
                expect(EmailTxt).toBeInTheDocument();

                fireEvent.change(EmailTxt, { target: { value: "abcd" } });

                
            })
            const resetPasswordBtn = getByTestId("reset-pass-btn");
            fireEvent.click(resetPasswordBtn)
    });

    test('Should show password reset msg', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                <ResetPassword></ResetPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const passwordTxt = getByTestId("reset-password-input");
                expect(passwordTxt).toBeInTheDocument();

                fireEvent.change(passwordTxt, { target: { value: "cuasdf1!" } });
                const confirmPasswordTxt = getByTestId("confirm-password-input");
                fireEvent.change(confirmPasswordTxt, { target: { value: "cuasdf1!" } });
            })

            const resetPasswordBtn = getByTestId("reset-pass-btn");
            fireEvent.click(resetPasswordBtn)

            await waitFor(()=>{
                expect(getByTestId("reset-password-success")).toBeInTheDocument()
            })


    });

    test('Should show password help (minimum pass Length) ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                <ResetPassword></ResetPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const EmailTxt = getByTestId("reset-password-input");
                expect(EmailTxt).toBeInTheDocument();

                fireEvent.change(EmailTxt, { target: { value: "pkxatybml" } });
            })

            await waitFor(()=>{
                expect(getByTestId("pass-help")).toBeInTheDocument()
            })
    });

    
})