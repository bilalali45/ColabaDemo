import React, { useState } from 'react';
import ContentBody from '../../Shared/ContentBody';
import { EmailInputBox } from '../../Shared/EmailInputBox';
import { TextEditor } from '../../Shared/TextEditor';
import { Store } from '../../../Store/Store';
import { RequestEmailTemplate } from '../../../Entities/Models/RequestEmailTemplate';
import { RequestEmailTemplateActionsType } from '../../../Store/reducers/RequestEmailTemplateReducer';
import { RequestEmailTemplateActions } from '../../../Store/actions/RequestEmailTemplateActions';
import { Tokens } from '../../../Entities/Models/Token';
import { disableBrowserPrompt,enableBrowserPrompt} from '../../../Utils/helpers/Common';
import { useForm } from 'react-hook-form';
import ContentFooter from '../../Shared/ContentFooter';
import LoanStatusUpdateModel, { LoanStatus } from '../../../Entities/Models/LoanStatusUpdate';
import { LoanStatusUpdateActionsType } from '../../../Store/reducers/LoanStatusUpdateReducer';
import { LoanStatusUpdateActions } from '../../../Store/actions/LoanStatusUpdateActions';

import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { UserActions } from '../../../Store/actions/UserActions';
import App from '../../../App';
import { StoreProvider } from '../../../Store/Store';
import { AssignedRoleActions } from '../../../Store/actions/AssignedRoleActions';
import { LocalDB } from '../../../Utils/LocalDB';
import LoanStatusUpdate from '../LoanStatusUpdate';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NotificationActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Store/actions/LoanStatusUpdateActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/LoanStatusUpdate');
});

describe('ColabaStatusEmailTemplateContent', () => {

    test('ColabaStatusEmailTemplateContent : Render', async () => {
        const { getByTestId, debug } = render(<StoreProvider><App/></StoreProvider>);
        expect(getByTestId('loader')).toBeTruthy();    
        await waitFor(()=>{                                   
            expect(getByTestId('contentHeader')).toBeTruthy();  
            expect(getByTestId('contentSubHeader')).toBeTruthy(); 
        });             
    });

    test('Colaba email content', async () => {
        const { getByTestId, getAllByTestId,container } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
                 <StoreProvider>
                 <App/>
                 </StoreProvider>
               
            </MemoryRouter>
            );

            const mainHead = getByTestId('main-header');
            expect(mainHead).toHaveTextContent('Settings');
            expect(getByTestId('sideBar')).toBeInTheDocument();
                
            const navs = getAllByTestId('sidebar-navDiv');
            expect(navs[1]).toHaveTextContent('Needs List');
                
            fireEvent.click(navs[1]);
            let navsLink: any;
            await waitFor(() => {
                navsLink = getAllByTestId('sidebar-nav');
                expect(navsLink[5]).toHaveTextContent('Loan Status Update');      
                });
            fireEvent.click(navsLink[5]);

            await waitFor(() => {
                let Header = getAllByTestId('header-title-text');         
                expect(Header[0].children[0]).toHaveTextContent('Loan Status Update');    
                expect(getByTestId('new-template-container')).toBeInTheDocument();                          
                });
                
                let loanStatusSection = getAllByTestId('loan-statuses');
                expect(loanStatusSection).toBeTruthy();
                let fromStatuses = getAllByTestId('loan-statuses-from');
                expect(fromStatuses).toHaveLength(3);
                
                fireEvent.click(fromStatuses[0]);

                let toHeader : any;
                await waitFor(() => {
                    toHeader = getByTestId('to-header');
                    expect(screen.queryByTestId('new-template-container')).toBeNull();
                });

                fireEvent.click(toHeader);

                let toStatuses: any;
                await waitFor(() => {
                    toStatuses  = getAllByTestId('loan-statuses-to');
                    expect(toStatuses).toHaveLength(2);
                    expect(toStatuses[1]).toHaveTextContent('Underwriting');
                });
                
                fireEvent.click(toStatuses[1]);

                await waitFor(() => {
                    let insertTokenBtn = screen.queryByTestId('insertToken-btn');
                    expect(insertTokenBtn).toBeNull();
                });   
                
                let ccEmail = getByTestId('cc-email');        
                fireEvent.click(ccEmail);

                let insertTokenBtn2: any;
                await waitFor(() => {
                    insertTokenBtn2  = screen.queryByTestId('insertToken-btn');
                    expect(insertTokenBtn2).not.toBeNull();
                });

                fireEvent.click(insertTokenBtn2);
         
                await waitFor(() => {
                let tokenPopup = getByTestId('token-popup');
                expect(tokenPopup).toBeInTheDocument();
                 });

                 const tokensList = getAllByTestId('tb-tr');
                
                 fireEvent.click(tokensList[0]);

               await waitFor(() => {
               const ccEmailInput: any = container.querySelector(
              "input[name='cCEmail']"
               );
  
               if(ccEmailInput){
                expect(ccEmailInput.value).toEqual("cc@gmail.com,###Co-BorrowerEmailAddress###");
               }                              
           });
 
           let toggler = getAllByTestId('togglerDefault');
           fireEvent.click(toggler[1]);

        });

    test('Colaba email content field edit', async () => {
            const { getByTestId, getAllByTestId,container } = render(
                <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
                     <StoreProvider>
                     <App/>
                     </StoreProvider>
                   
                </MemoryRouter>
                );
    
                const mainHead = getByTestId('main-header');
                expect(mainHead).toHaveTextContent('Settings');
                expect(getByTestId('sideBar')).toBeInTheDocument();
                    
                const navs = getAllByTestId('sidebar-navDiv');
                expect(navs[1]).toHaveTextContent('Needs List');
                    
                fireEvent.click(navs[1]);
                let navsLink: any;
                await waitFor(() => {
                    navsLink = getAllByTestId('sidebar-nav');
                    expect(navsLink[5]).toHaveTextContent('Loan Status Update');      
                    });
                fireEvent.click(navsLink[5]);
    
                await waitFor(() => {
                    let Header = getAllByTestId('header-title-text');         
                    expect(Header[0].children[0]).toHaveTextContent('Loan Status Update');    
                    expect(getByTestId('new-template-container')).toBeInTheDocument();                          
                    });
                    
                    let loanStatusSection = getAllByTestId('loan-statuses');
                    expect(loanStatusSection).toBeTruthy();
                    let fromStatuses = getAllByTestId('loan-statuses-from');
                    expect(fromStatuses).toHaveLength(3);
                    expect(fromStatuses[1]).toHaveTextContent('Processing');

                    fireEvent.click(fromStatuses[1]);
    
                    let toHeader : any;
                    await waitFor(() => {
                        toHeader = getByTestId('to-header');
                        //expect(screen.queryByTestId('new-template-container')).toBeNull();
                    });
    
                    fireEvent.click(toHeader);
    
                    let toStatuses: any;
                    await waitFor(() => {
                        toStatuses  = getAllByTestId('loan-statuses-to');
                        expect(toStatuses).toHaveLength(2);
                        expect(toStatuses[1]).toHaveTextContent('Underwriting');
                    });
                    
                    fireEvent.click(toStatuses[1]);
                    
                    await waitFor(() => {
                       expect(getByTestId('save-btn')).toBeInTheDocument();
                       expect(screen.queryByTestId('fromEmail-error')).toBeNull();
                    });

                    fireEvent.click(getByTestId('save-btn'));

                    await waitFor(() => {
                        let error = screen.queryByTestId('fromEmail-error');
                        expect(error).not.toBeInTheDocument();
                     });


    
                    await waitFor(() => {
                        let insertTokenBtn = screen.queryByTestId('insertToken-btn');
                        expect(insertTokenBtn).toBeNull();
                    });   
                        let fromEmail = getByTestId('from-email');        
                        fireEvent.click(fromEmail);

                        let insertTokenBtn2: any;
                    await waitFor(() => {
                        insertTokenBtn2  = screen.queryByTestId('insertToken-btn');
                        expect(insertTokenBtn2).not.toBeNull();
                    });

                     fireEvent.click(insertTokenBtn2);
                     await waitFor(() => {
                      let tokenPopup = getByTestId('token-popup');
                      expect(tokenPopup).toBeInTheDocument();
                     });

                    const tokensList = getAllByTestId('tb-tr');
                    
                     fireEvent.click(tokensList[0]);

                 await waitFor(() => {
                   const fromEmailInput: any = container.querySelector(
                  "input[name='fromEmail']"
                   );

                    if(fromEmailInput){
                    expect(fromEmailInput.value).toEqual("###RequestorUserEmail###");
                   }  
                });
                               
                    let ccEmail = getByTestId('cc-email');

                    fireEvent.click(ccEmail);

                   await waitFor(() => {
                    expect(screen.queryByTestId('insertToken-btn')).not.toBeNull();
                   })

                    fireEvent.click(screen.getByTestId('insertToken-btn'));

                    await waitFor(() => {
                     let tokenPopup = getByTestId('token-popup');
                     expect(tokenPopup).toBeInTheDocument();
                    });

                     const tokensList2 = getAllByTestId('tb-tr');
                    
                     fireEvent.click(tokensList2[0]);
    
                   await waitFor(() => {
                   const ccEmailInput: any = container.querySelector(
                     "input[name='cCEmail']"
                   );
      
                   if(ccEmailInput){
                    expect(ccEmailInput.value).toEqual("###Co-BorrowerEmailAddress###");
                   }                              
               }); 
               
               //Inserting values on subject field
               const subjectInput: any = container.querySelector(
                "input[name='subjectLine']"
                  );
  
                fireEvent.input(subjectInput, {
                target: {
                value: "Test subject"
               }
            });

            //  Inserting values on email body field
        const textEditorDv = getByTestId('text-editor-dv');        
        fireEvent.focus(textEditorDv);

        await waitFor(() => {
         let tokenPopupScreen = getByTestId('token-popup');
         expect(tokenPopupScreen).toBeInTheDocument();
        });

        const token = getAllByTestId('tb-tr');
        fireEvent.click(token[2]);
    
      let emailBody: any;
      await waitFor(() => {
        emailBody = container.querySelector(
          "input[name='emailBody']"
        );   
        expect(emailBody.value).toEqual("###BusinessUnitPhoneNumber###");
      });

      const saveButton = getByTestId('save-btn');
      fireEvent.click(saveButton);
    
    });
    
    test('Colaba email cancel', async () => {
        const { getByTestId, getAllByTestId,container } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
                 <StoreProvider>
                 <App/>
                 </StoreProvider>
               
            </MemoryRouter>
            );

            const mainHead = getByTestId('main-header');
            expect(mainHead).toHaveTextContent('Settings');
            expect(getByTestId('sideBar')).toBeInTheDocument();
                
            const navs = getAllByTestId('sidebar-navDiv');
            expect(navs[1]).toHaveTextContent('Needs List');
                
            fireEvent.click(navs[1]);
            let navsLink: any;
            await waitFor(() => {
                navsLink = getAllByTestId('sidebar-nav');
                expect(navsLink[5]).toHaveTextContent('Loan Status Update');      
                });
            fireEvent.click(navsLink[5]);

            await waitFor(() => {
                let Header = getAllByTestId('header-title-text');         
                expect(Header[0].children[0]).toHaveTextContent('Loan Status Update');    
                expect(getByTestId('new-template-container')).toBeInTheDocument();                          
                });
                
                let loanStatusSection = getAllByTestId('loan-statuses');
                expect(loanStatusSection).toBeTruthy();
                let fromStatuses = getAllByTestId('loan-statuses-from');
                expect(fromStatuses).toHaveLength(3);
                
                fireEvent.click(fromStatuses[0]);

                let toHeader : any;
                await waitFor(() => {
                    toHeader = getByTestId('to-header');
                    expect(screen.queryByTestId('new-template-container')).toBeNull();
                });

                fireEvent.click(toHeader);

                let toStatuses: any;
                await waitFor(() => {
                    toStatuses  = getAllByTestId('loan-statuses-to');
                    expect(toStatuses).toHaveLength(2);
                    expect(toStatuses[1]).toHaveTextContent('Underwriting');
                });
                
                fireEvent.click(toStatuses[1]);

                await waitFor(() => {
                    let insertTokenBtn = screen.queryByTestId('insertToken-btn');
                    expect(insertTokenBtn).toBeNull();
                });   

                const subjectInput: any = container.querySelector(
                    "input[name='subjectLine']"
                      );

                fireEvent.focus(subjectInput);

                await waitFor(() => {
                    fireEvent.input(subjectInput, {
                        target: {
                        value: "Test subject"
                       }
                    });
                }); 

                fireEvent.blur(subjectInput);
                
                let ccEmail = getByTestId('cc-email');        
                fireEvent.click(ccEmail);

                let insertTokenBtn2: any;
                await waitFor(() => {
                    insertTokenBtn2  = screen.queryByTestId('insertToken-btn');
                    expect(insertTokenBtn2).not.toBeNull();
                });

                fireEvent.click(insertTokenBtn2);
         
                await waitFor(() => {
                let tokenPopup = getByTestId('token-popup');
                expect(tokenPopup).toBeInTheDocument();
                 });

                 const tokensList = getAllByTestId('tb-tr');
                
                 fireEvent.click(tokensList[0]);

               await waitFor(() => {
               const ccEmailInput: any = container.querySelector(
                 "input[name='cCEmail']"
                );
  
               if(ccEmailInput){
                expect(ccEmailInput.value).toEqual("cc@gmail.com,###Co-BorrowerEmailAddress###");
               }                              
           });
 
           fireEvent.click(getByTestId('cancel-btn'));

        });

})