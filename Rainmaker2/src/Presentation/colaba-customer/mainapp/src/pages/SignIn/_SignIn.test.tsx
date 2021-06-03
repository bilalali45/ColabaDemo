import React from 'react';
import { fireEvent, render, waitFor } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../Store/Store';
import SignIn from './SignIn';

import { UserActions } from '../../Store/actions/UserActions';

jest.mock('../../lib/localStorage');
jest.mock('../../Store/actions/UserActions');

const loginDataMock = {
    "status": "Success",
    "data": {
        "isLoggedIn": true,
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsIlVzZXJJZCI6IjkiLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiamVoYW5naXJAZ21haWwuY29tIiwiRmlyc3ROYW1lIjoiSmVoYW5naXIiLCJMYXN0TmFtZSI6IkJhYnVsIiwiVGVuYW50Q29kZSI6ImxlbmRvdmEiLCJCcmFuY2hDb2RlIjoibGVuZG92YSIsImV4cCI6MTYxNjE4MzY1MiwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.EjcX5v0arV_uxQYyz4nDEBBgmFAoLG2tY3f8imJL1zA",
        "refreshToken": "5N8kt+xpDxA4Kjw2UeiYIGRNdSCK8H2HOT5BX02X/ag=",
        "validFrom": "0001-01-01T00:00:00",
        "validTo": "2021-03-19T19:54:12Z",
        "cookiePath": "/lendova/"
    },
    "message": null,
    "code": "200"
}

const loginInvalidMockData = {
    "status": null,
    "data": null,
    "message": "User does not exist",
    "code": "400"
}

const loginWithSkip = {
    "status": "Success",
    "data": {
        "tenant2FaStatus": 3,
        "userPreference": null,
        "isLoggedIn": false,
        "phoneNoMissing": true,
        "requiresTwoFa": true,
        "phoneNo": null,
        "verificationSid": null,
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsIlVzZXJQcm9maWxlSWQiOiI5IiwiQ29udGFjdElkIjoiMTIiLCJFbWFpbCI6ImplaGFuZ2lyQGdtYWlsLmNvbSIsImV4cCI6MTYxNTk3NjA0MywiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.NOW1UMcaQjsOHFm4HYgSKjN5XwyjwHj2HkmGnIp431M",
        "userProfileId": 9,
        "userName": "jehangir@gmail.com",
        "validFrom": "0001-01-01T00:00:00",
        "validTo": "2021-03-17T10:14:03Z",
        "verify_attempts_count": 1,
        "otp_valid_till": "0001-01-01T00:10:00",
        "next2FaInSeconds": 120,
        "twoFaRecycleMinutes": 10,
        "sendAttemptsCount": null
    },
    "message": null,
    "code": "200"
} 


describe('<--------------_SigIn Section-----------------> ', () => {

    test('should login on click on login', async() => {
        UserActions.signIn = jest.fn(() => Promise.resolve(loginDataMock));
        const {getByTestId, container} = render(
            <StoreProvider> 
                <MemoryRouter initialEntries={["/"]}>
                        <SignIn></SignIn>
                    </MemoryRouter>
            </StoreProvider>
           );


        const emailInput = container.querySelector(
            "input[name='email']"
          );
          if(emailInput){
            fireEvent.input(emailInput, {
              target: {
                value: "jehangir@gmail.com"
              }
            });
          }

          const password = container.querySelector(
            "input[name='password']"
          );
          if(password){
            fireEvent.input(password, {
              target: {
                value: "test123"
              }
            });
          }


          let loginBtn = getByTestId('login-btn');
          fireEvent.click(loginBtn);

          await waitFor(() => {
                expect(getByTestId('signIn-form-step1')).toBeInTheDocument();            
         });
      
     });

     test('should show error on invalid username or password', async() => {
        UserActions.signIn = jest.fn(() => Promise.resolve(loginInvalidMockData));
        const {getByTestId, container} = render(
            <StoreProvider> 
                <MemoryRouter initialEntries={["/"]}>
                        <SignIn></SignIn>
                    </MemoryRouter>
            </StoreProvider>
           );


        const emailInput = container.querySelector(
            "input[name='email']"
          );
          if(emailInput){
            fireEvent.input(emailInput, {
              target: {
                value: "jehangir@gmail.com"
              }
            });
          }

          const password = container.querySelector(
            "input[name='password']"
          );
          if(password){
            fireEvent.input(password, {
              target: {
                value: "test123456"
              }
            });
          }


          let loginBtn = getByTestId('login-btn');
          fireEvent.click(loginBtn);

          await waitFor(() => {
                expect(getByTestId('password-error')).toBeInTheDocument();            
         });
      
     });

     test('Show skip button', async() => {
        UserActions.signIn = jest.fn(() => Promise.resolve(loginWithSkip));
        const {getByTestId, container} = render(
            <StoreProvider> 
                <MemoryRouter initialEntries={["/"]}>
                        <SignIn></SignIn>
                    </MemoryRouter>
            </StoreProvider>
           );


        const emailInput = container.querySelector(
            "input[name='email']"
          );
          if(emailInput){
            fireEvent.input(emailInput, {
              target: {
                value: "jehangir@gmail.com"
              }
            });
          }

          const password = container.querySelector(
            "input[name='password']"
          );
          if(password){
            fireEvent.input(password, {
              target: {
                value: "test123"
              }
            });
          }


          let loginBtn = getByTestId('login-btn');
          fireEvent.click(loginBtn);

          await waitFor(() => {
                expect(getByTestId('signIn-form-step2')).toBeInTheDocument();  
                expect(getByTestId('skip-btn')).toBeInTheDocument();            
         });

         fireEvent.click(getByTestId('skip-btn'));

         await waitFor(() => {
            expect(getByTestId('signIn-form-step2')).toBeInTheDocument();            
       });
      
     });
});