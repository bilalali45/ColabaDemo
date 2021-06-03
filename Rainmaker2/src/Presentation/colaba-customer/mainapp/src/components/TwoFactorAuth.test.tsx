import React from 'react';
import { fireEvent, render, screen, waitFor } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../Store/Store';
import { UserActions } from '../Store/actions/UserActions';
import SignIn from '../pages/SignIn/SignIn';


jest.mock('../lib/localStorage');
jest.mock('../Store/actions/UserActions');

const loginData =  {
    "status": "Success",
    "data": {
        "tenant2FaStatus": 3,
        "userPreference": null,
        "isLoggedIn": false,
        "phoneNoMissing": false,
        "requiresTwoFa": true,
        "phoneNo": "+12254165226",
        "verificationSid": null,
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsIlVzZXJQcm9maWxlSWQiOiI5IiwiQ29udGFjdElkIjoiMTIiLCJFbWFpbCI6ImplaGFuZ2lyQGdtYWlsLmNvbSIsImV4cCI6MTYxNTk3NjA0MywiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.NOW1UMcaQjsOHFm4HYgSKjN5XwyjwHj2HkmGnIp431M",
        "userProfileId": 9,
        "userName": "jehangir@gmail.com",
        "validFrom": "0001-01-01T00:00:00",
        "validTo": "2021-03-17T10:14:03Z",
        "verify_attempts_count": 6,
        "otp_valid_till": "0001-01-01T00:10:00",
        "next2FaInSeconds": 120,
        "twoFaRecycleMinutes": 10,
        "sendAttemptsCount": null
    },
    "message": null,
    "code": "200"
} 


const loginDataOtp =  {
    "status": "Success",
    "data": {
        "tenant2FaStatus": 3,
        "userPreference": null,
        "isLoggedIn": false,
        "phoneNoMissing": false,
        "requiresTwoFa": true,
        "phoneNo": "+12254165226",
        "verificationSid": null,
        "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsIlVzZXJQcm9maWxlSWQiOiI5IiwiQ29udGFjdElkIjoiMTIiLCJFbWFpbCI6ImplaGFuZ2lyQGdtYWlsLmNvbSIsImV4cCI6MTYxNTk3NjA0MywiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.NOW1UMcaQjsOHFm4HYgSKjN5XwyjwHj2HkmGnIp431M",
        "userProfileId": 9,
        "userName": "jehangir@gmail.com",
        "validFrom": "0001-01-01T00:00:00",
        "validTo": "2021-03-17T10:14:03Z",
        "verify_attempts_count": 2,
        "otp_valid_till": "0001-01-01T00:10:00",
        "next2FaInSeconds": 120,
        "twoFaRecycleMinutes": 10,
        "sendAttemptsCount": [
            {
                "attempt_sid": "VL92876fb75acece6346b8ee7a430e63ee",
                "channel": "sms",
                "time": "2021-03-15T07:27:15Z"
            }
        ],
    },
    "message": null,
    "code": "200"
} 

describe('<--------------Two Factor Auth-----------------> ', () => {

    test('show timer when verify attempt count exceed in login', async() => {

        UserActions.signIn = jest.fn(() => Promise.resolve(loginData));

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
                expect(screen.queryByTestId('signIn-form-step1')).not.toBeInTheDocument(); 
                expect(getByTestId('signIn-form-step3')).toBeInTheDocument();            
         });
      
    });

    test('move to otp screen when login login', async() => {

        UserActions.signIn = jest.fn(() => Promise.resolve(loginDataOtp));

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
                expect(screen.queryByTestId('signIn-form-step1')).not.toBeInTheDocument(); 
                expect(getByTestId('signIn-form-step3')).toBeInTheDocument();            
         });
      
    });

});