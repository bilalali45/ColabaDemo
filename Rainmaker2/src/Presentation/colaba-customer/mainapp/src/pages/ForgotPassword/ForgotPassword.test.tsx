import React from 'react';
import { fireEvent, render, waitFor } from "@testing-library/react";
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../Store/Store';
import ForgotPassword from './ForgotPassword';

jest.mock('../../lib/localStorage');
jest.mock('../../Store/actions/UserActions');
// jest.mock('../../../../../Store/actions/TemplateActions');

const Url = '/forgotPassword/'

beforeEach(() => {
    // MockEnvConfig();
    // MockLocalStorage();
});

describe('Forgot Password Section ', () => {

    test('Should show Forgot Password heading ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ForgotPassword></ForgotPassword>
                </MemoryRouter>
            </StoreProvider>
        );

        expect(getByTestId('forgot-pass-header')).toBeInTheDocument();

        await waitFor(() => {
        const forgotPassHeader = getByTestId("forgot-pass-header");
        expect(forgotPassHeader).toBeInTheDocument();

        expect(forgotPassHeader).toHaveTextContent("Password Assistance")
        })
    });

    test('Should show Forgot Password tag line ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ForgotPassword></ForgotPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const forgotPassTagLine = getByTestId("forgot-pass-tagline");
                expect(forgotPassTagLine).toBeInTheDocument();

                expect(forgotPassTagLine).toHaveTextContent("Enter your email to reset your password.")
            })
    });

    test('Should show Reset Password Btn ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ForgotPassword></ForgotPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const resetPasswordBtn = getByTestId("reset-pass-btn");
                expect(resetPasswordBtn).toBeInTheDocument();

                expect(resetPasswordBtn).toHaveTextContent("RESET PASSWORD")
            })
    });

    test('Should show Email txt ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ForgotPassword></ForgotPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const EmailTxt = getByTestId("email");


                expect(EmailTxt).toBeInTheDocument();

                expect(EmailTxt).toHaveTextContent("Email")
            })
    });


    test('Should show Required field error on reset password click ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ForgotPassword></ForgotPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const resetPasswordBtn = getByTestId("reset-pass-btn");

                fireEvent.click(resetPasswordBtn)

                const EmailError = getByTestId("email-error");
                expect(EmailError).toHaveTextContent("This field is required.")
            })
    });


    test('Should show invalid Email error ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ForgotPassword></ForgotPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const EmailTxt = getByTestId("forgot-password-input");
                expect(EmailTxt).toBeInTheDocument();

                fireEvent.change(EmailTxt, { target: { value: "abcd" } });

                const resetPasswordBtn = getByTestId("reset-pass-btn");

                fireEvent.click(resetPasswordBtn)

                const EmailError = getByTestId("email-error");
                expect(EmailError).toHaveTextContent("Please enter a valid email address.")
            })
    });

    test('Should show invalid Email error ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ForgotPassword></ForgotPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const EmailTxt = getByTestId("forgot-password-input");
                expect(EmailTxt).toBeInTheDocument();

                fireEvent.change(EmailTxt, { target: { value: "abcd" } });

                const resetPasswordBtn = getByTestId("reset-pass-btn");

                fireEvent.click(resetPasswordBtn)

                const EmailError = getByTestId("email-error");
                expect(EmailError).toHaveTextContent("Please enter a valid email address.")
            })
    });

    test('Should show success message on valid email ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <ForgotPassword></ForgotPassword>
                </MemoryRouter>
            </StoreProvider>
        );

            await waitFor(() => {
                const EmailTxt = getByTestId("forgot-password-input");
                expect(EmailTxt).toBeInTheDocument();

                fireEvent.change(EmailTxt, { target: { value: "test@test.com" } });

                const resetPasswordBtn = getByTestId("reset-pass-btn");

                fireEvent.click(resetPasswordBtn)

            })
            await waitFor(() => {
                const successMsg = getByTestId("email-success-msg")
                expect(successMsg).toHaveTextContent("An email with instructions to create a new password has been sent to you.")
            })
            
    });

})