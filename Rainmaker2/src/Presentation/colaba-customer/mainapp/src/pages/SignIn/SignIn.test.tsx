import React from 'react';
import { fireEvent, render, screen, waitFor } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../Store/Store';
import SignIn from './SignIn';
import { act } from 'react-dom/test-utils';

jest.mock('../../lib/localStorage');
jest.mock('../../Store/actions/UserActions');



describe('<--------------SigIn Section-----------------> ', () => {

    test('Should show "Login To Your Account" header text', async() => {
       const {getByTestId} = render(
        <StoreProvider> 
            <MemoryRouter initialEntries={["/"]}>
                    <SignIn></SignIn>
                </MemoryRouter>
        </StoreProvider>
       );
       expect(getByTestId('signIn-step1-header')).toBeInTheDocument();
       
    });

    test('Should render email and password and related Elements', async() => {
        const {getByTestId} = render(
         <StoreProvider> 
             <MemoryRouter initialEntries={["/"]}>
                     <SignIn></SignIn>
                 </MemoryRouter>
         </StoreProvider>
        );
        let headerElements = getByTestId('signIn-step1-header');
        expect(headerElements.children[0]).toHaveTextContent('Login To Your Account');
        expect(headerElements.children[1]).toHaveTextContent('Welcome Back! Sign In To Continue Your Mortgage Process.');
        expect(getByTestId('signinEmail-input')).toBeInTheDocument();
        expect(getByTestId('signinPassword-input')).toBeInTheDocument();
        expect(getByTestId('forgot-password-link')).toBeInTheDocument();
        expect(getByTestId('login-btn')).toBeInTheDocument();
        expect(getByTestId('dont-have-account-link')).toBeInTheDocument();
        expect(getByTestId('signUp-link')).toBeInTheDocument();
     });

     test('should validate input', async() => {
        const {getByTestId} = render(
            <StoreProvider> 
                <MemoryRouter initialEntries={["/"]}>
                        <SignIn></SignIn>
                    </MemoryRouter>
            </StoreProvider>
           );

           const emailError = screen.queryByTestId('email-error');
           const passwordError = screen.queryByTestId('password-error');
           expect(emailError).not.toBeInTheDocument();
           expect(passwordError).not.toBeInTheDocument();
          
           let loginBtn = getByTestId('login-btn');
           fireEvent.click(loginBtn);

           await waitFor(() => {
             let emailError2 = getByTestId('email-error');
             let passwordError2 = getByTestId('password-error');
             expect(emailError2).toBeInTheDocument();
             expect(passwordError2).toBeInTheDocument();
           });
     });

    test('should move to step 2 on login button click', async() => {
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
                expect(screen.queryByTestId('signIn-form-step2')).toBeInTheDocument();               
         });
      
     });

     test('Should show "Phone Verification And Consent To Contact" header text and related fields', async() => {
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
                expect(screen.queryByTestId('signIn-form-step2')).toBeInTheDocument();               
         });
         let setp2Header = getByTestId('step2-header');
         expect(setp2Header.children[0]).toHaveTextContent('Phone Verification And Consent To Contact');
         expect(setp2Header.children[1]).toHaveTextContent('Enter your phone number below and we\'ll send you a verification code.');
         expect(getByTestId('phone')).toBeInTheDocument();
         expect(getByTestId('read-agreement-link')).toBeInTheDocument();
         expect(getByTestId('agree-and-continue-btn')).toBeInTheDocument();
         expect(screen.queryByTestId('skip-btn')).not.toBeInTheDocument();
         expect(getByTestId('back-btn')).toBeInTheDocument();
         expect(getByTestId('signup-btn')).toBeInTheDocument();

         fireEvent.click(getByTestId('read-agreement-link'));

         await waitFor(() => {
            expect(getByTestId('modal')).toBeInTheDocument();
         });

         fireEvent.click(getByTestId('modal-close'));

         await waitFor(() => {
            expect(screen.queryByTestId('modal')).not.toBeInTheDocument();
         });

         fireEvent.click(getByTestId('back-btn'));

         await waitFor(() => {
            expect(screen.queryByTestId('signIn-form-step1')).toBeInTheDocument();
            expect(screen.queryByTestId('signIn-form-step2')).not.toBeInTheDocument();  
         });

     });

     test('should move to step 3 on providing phone and agree button click', async() => {
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
                expect(screen.queryByTestId('signIn-form-step2')).toBeInTheDocument();               
         });
      
         const phoneInput = container.querySelector(
            "input[name='mobile']"
          );
          if(phoneInput){
            fireEvent.input(phoneInput, {
              target: {
                value: "2254165226"
              }
            });
          }
        await waitFor(() => {
            expect(getByTestId('agree-and-continue-btn'));
        });

        act(() => {
            fireEvent.click(getByTestId('agree-and-continue-btn'));
        });
          
          await waitFor(() => {
             expect(screen.queryByTestId('signIn-form-step2')).not.toBeInTheDocument(); 
             expect(screen.queryByTestId('signIn-form-step3')).toBeInTheDocument();          
        });

        expect(getByTestId('2fa-header')).toBeInTheDocument();   
        expect(getByTestId('encoded-number')).toHaveTextContent('Enter the code sent to (***) ***-5226');
        expect(getByTestId('otp-input')).toBeInTheDocument();
        expect(screen.queryByTestId('tick')).not.toBeInTheDocument();
        expect(screen.queryByTestId('otp-error')).not.toBeInTheDocument();
        expect(screen.queryByTestId('timer-div')).not.toBeInTheDocument();
        expect(getByTestId('send2fa-btn')).toBeInTheDocument();
        expect(getByTestId('send2fa-btn').children[0]).toHaveClass('text-disabled');
        expect(getByTestId('dnt-ask-check')).toBeInTheDocument();
        expect(getByTestId('verify-btn')).toBeInTheDocument();
        expect(getByTestId('verify-btn')).toHaveProperty('disabled');
        expect(getByTestId('back-btn')).toBeInTheDocument();
        expect(getByTestId('signup-btn')).toBeInTheDocument();
        
     });
    

     test('should move to desktop on providing otp and verify button click', async() => {
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
                expect(screen.queryByTestId('signIn-form-step2')).toBeInTheDocument();               
         });
      
         const phoneInput = container.querySelector(
            "input[name='mobile']"
          );
          if(phoneInput){
            fireEvent.input(phoneInput, {
              target: {
                value: "2254165226"
              }
            });
          }
        await waitFor(() => {
            expect(getByTestId('agree-and-continue-btn'));
        });

        act(() => {
            fireEvent.click(getByTestId('agree-and-continue-btn'));
        });
          
          await waitFor(() => {
             expect(screen.queryByTestId('signIn-form-step2')).not.toBeInTheDocument(); 
             expect(screen.queryByTestId('signIn-form-step3')).toBeInTheDocument();          
        });
 
        const otpInput = container.querySelector(
            "input[name='otp']"
        );
        if(otpInput){
            fireEvent.input(otpInput, {
                   target: { value: "555555"}
            });
        }

        await waitFor(() => {
            expect(getByTestId('tick')).toBeInTheDocument();
            expect(getByTestId('verify-btn')).not.toBeDisabled();
        });
     
        fireEvent.click(getByTestId('verify-btn'));

        await waitFor(() => {
            expect(screen.queryByTestId('timer-div')).toBeInTheDocument();
        });
        
     });
    
     test('should show error on invalid number', async() => {
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
            expect(screen.queryByTestId('signIn-form-step2')).toBeInTheDocument();               
     });
  
     const phoneInput = container.querySelector(
        "input[name='mobile']"
      );
      if(phoneInput){
        fireEvent.input(phoneInput, {
          target: {
            value: "22541"
          }
        });
      }
    await waitFor(() => {
        expect(getByTestId('agree-and-continue-btn'));
    });

    act(() => {
        fireEvent.click(getByTestId('agree-and-continue-btn'));
    });
      
      await waitFor(() => {
         expect(getByTestId('signIn-form-step2')).toBeInTheDocument(); 
         expect(getByTestId('phone-error')).toBeInTheDocument();          
      });
    });

    test('should move to forgot password on click on forgot password link', async() => {
      const {getByTestId} = render(
          <StoreProvider> 
              <MemoryRouter initialEntries={["/"]}>
                      <SignIn></SignIn>
                  </MemoryRouter>
          </StoreProvider>
         );
     let forgotPasswordBtn = getByTestId('forgotpassword-btn');
     fireEvent.click(forgotPasswordBtn);
     
     await waitFor(() => {
      expect(getByTestId('signIn-form-step1')).not.toBeInTheDocument();
     });
   });

   test('should work back button', async() => {
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
            expect(screen.queryByTestId('signIn-form-step2')).toBeInTheDocument();               
     });
  
     const phoneInput = container.querySelector(
        "input[name='mobile']"
      );
      if(phoneInput){
        fireEvent.input(phoneInput, {
          target: {
            value: "2254165226"
          }
        });
      }
    await waitFor(() => {
        expect(getByTestId('agree-and-continue-btn'));
    });

    act(() => {
        fireEvent.click(getByTestId('agree-and-continue-btn'));
    });
      
      await waitFor(() => {
         expect(screen.queryByTestId('signIn-form-step2')).not.toBeInTheDocument(); 
         expect(screen.queryByTestId('signIn-form-step3')).toBeInTheDocument();          
    });

     fireEvent.click(getByTestId('back-btn'));
   
     await waitFor(() => {
      expect(screen.queryByTestId('signIn-form-step2')).toBeInTheDocument(); 
    });

 });


});