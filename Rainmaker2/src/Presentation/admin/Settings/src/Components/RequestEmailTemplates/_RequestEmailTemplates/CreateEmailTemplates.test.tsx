import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import App from '../../../App';
import { StoreProvider } from '../../../Store/Store';
import { CreateEmailTemplates } from './CreateEmailTemplates';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NotificationActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Store/actions/TemplateActions');
jest.mock('../../../Store/actions/RequestEmailTemplateActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Setting/RequestEmailTemplates');
});

describe('Create Request Email Template', () => {

    test('Should click on menu of Email template and click on Add Email Template', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
               <App/>
            </MemoryRouter>
           );
          
           //Test setting text and sideBar menu is rendered
           const mainHead = getByTestId('main-header');
           expect(mainHead).toHaveTextContent('Settings');
           expect(getByTestId('sideBar')).toBeInTheDocument();
    
           const navs = getAllByTestId('sidebar-navDiv');
           expect(navs[1]).toHaveTextContent('Needs List');
    
           fireEvent.click(navs[1]);
           let navsLink: any;
           
           //Test Request Email template rendered on menu
           await waitFor(() => {
             navsLink = getAllByTestId('sidebar-nav');
             expect(navsLink[2]).toHaveTextContent('Request Email Templates');      
           });
    
            fireEvent.click(navsLink[2]);
            let addEmailTemplateBtn: any;
            //Test click on menu and render add email template rendered 
            await waitFor(() => {
             addEmailTemplateBtn = getByTestId('addNewTemplate-btn');
             expect(addEmailTemplateBtn).toBeInTheDocument();
            });

          fireEvent.click(addEmailTemplateBtn);
          
          await waitFor(() => {
              const formBody = getByTestId('create-form');
              expect(formBody).toBeInTheDocument();
          });
      });

    test('Should rendered sub header and back button', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
               <App/>
            </MemoryRouter>
           );
          
           //Test setting text and sideBar menu is rendered
           const mainHead = getByTestId('main-header');
           expect(mainHead).toHaveTextContent('Settings');
           expect(getByTestId('sideBar')).toBeInTheDocument();
    
           const navs = getAllByTestId('sidebar-navDiv');
           expect(navs[1]).toHaveTextContent('Needs List');
    
           fireEvent.click(navs[1]);
           let navsLink: any;
           
           //Test Request Email template rendered on menu
           await waitFor(() => {
             navsLink = getAllByTestId('sidebar-nav');
             expect(navsLink[2]).toHaveTextContent('Request Email Templates');      
           });
    
            fireEvent.click(navsLink[2]);
            let addEmailTemplateBtn: any;
            //Test click on menu and render add email template rendered 
            await waitFor(() => {
             addEmailTemplateBtn = getByTestId('addNewTemplate-btn');
             expect(addEmailTemplateBtn).toBeInTheDocument();
            });

          fireEvent.click(addEmailTemplateBtn);
          
          await waitFor(() => {
              const formBody = getByTestId('sub-header');
              expect(formBody).toBeInTheDocument();
              const backBtn = getByTestId('subHeader-backBtn')
              expect(backBtn).toHaveTextContent('Back');
          });
          
        });

    test('Should rendered form and all related fields with label', async () => {
            const { getByTestId, getAllByTestId } = render(
                <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
                   <App/>
                </MemoryRouter>
               );
              
               //Test setting text and sideBar menu is rendered
               const mainHead = getByTestId('main-header');
               expect(mainHead).toHaveTextContent('Settings');
               expect(getByTestId('sideBar')).toBeInTheDocument();
        
               const navs = getAllByTestId('sidebar-navDiv');
               expect(navs[1]).toHaveTextContent('Needs List');
        
               fireEvent.click(navs[1]);
               let navsLink: any;
               
               //Test Request Email template rendered on menu
               await waitFor(() => {
                 navsLink = getAllByTestId('sidebar-nav');
                 expect(navsLink[2]).toHaveTextContent('Request Email Templates');      
               });
        
                fireEvent.click(navsLink[2]);
                let addEmailTemplateBtn: any;
                //Test click on menu and render add email template rendered 
                await waitFor(() => {
                 addEmailTemplateBtn = getByTestId('addNewTemplate-btn');
                 expect(addEmailTemplateBtn).toBeInTheDocument();
                });
    
              fireEvent.click(addEmailTemplateBtn);
              
              await waitFor(() => {
                const formBody = getByTestId('create-form');
                const templateNameDv = getByTestId('dv-templateName');
                const templateNameInput = getByTestId('templateName-input');
                const templateNameDescDv = getByTestId('dv-templateDesc');
                const templateDescInput = getByTestId('templateName-desc');
                const fromAddressDv = getByTestId('dv-fromAddress');
                const fromEmailInput = getByTestId('from-email');
                const ccAddressDv = getByTestId('dv-ccAddress');
                const ccEmailInput = getByTestId('cc-email');
                const subjectLineDv = getByTestId('dv-subline');
                const subjectInput = getByTestId('subject-input');
                const textEditorLabel = getByTestId('email-body-label');
                const saveBtn = getByTestId('save-btn');              
                const cancelBtn = getByTestId('cancel-btn');
                

                expect(formBody).toBeInTheDocument();
                expect(templateNameDv).toHaveTextContent('Template Name');
                expect(templateNameInput).toBeInTheDocument();
                expect(templateNameDescDv).toHaveTextContent('Template Description');
                expect(templateDescInput).toBeInTheDocument();
                expect(fromAddressDv).toHaveTextContent('Default From Address');
                expect(fromEmailInput).toBeInTheDocument();
                expect(ccAddressDv).toHaveTextContent('Default CC Address');
                expect(ccEmailInput).toBeInTheDocument();
                expect(subjectLineDv).toHaveTextContent('Subject Line');
                expect(subjectInput).toBeInTheDocument();
                expect(textEditorLabel).toBeInTheDocument();
                expect(saveBtn).toBeInTheDocument();
                expect(cancelBtn).toBeInTheDocument();

              });
              
            });

    test('Should goto template list page on click back button', async () => {
        const { getByTestId, getAllByTestId } = render(
        <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
             <App/>
        </MemoryRouter>
        );
                  
        //Test setting text and sideBar menu is rendered
        const mainHead = getByTestId('main-header');
        expect(mainHead).toHaveTextContent('Settings');
        expect(getByTestId('sideBar')).toBeInTheDocument();
            
        const navs = getAllByTestId('sidebar-navDiv');
        expect(navs[1]).toHaveTextContent('Needs List');
            
        fireEvent.click(navs[1]);
        let navsLink: any;
                   
        //Test Request Email template rendered on menu
        await waitFor(() => {
        navsLink = getAllByTestId('sidebar-nav');
        expect(navsLink[2]).toHaveTextContent('Request Email Templates');      
        });
            
        fireEvent.click(navsLink[2]);
        let addEmailTemplateBtn: any;
        //Test click on menu and render add email template rendered 
        await waitFor(() => {
        addEmailTemplateBtn = getByTestId('addNewTemplate-btn');
        expect(addEmailTemplateBtn).toBeInTheDocument();
        });
        
        fireEvent.click(addEmailTemplateBtn);
        let  backBtn : any;

        await waitFor(() => {
        const formBody = getByTestId('sub-header');
        expect(formBody).toBeInTheDocument();
         backBtn = getByTestId('subHeader-backBtn')
        expect(backBtn).toHaveTextContent('Back');
        });
            
        fireEvent.click(backBtn);

        const formBody = screen.queryByTestId('create-form');
        expect(formBody).toBeNull();

    });

    test('Should goto template list page on click cancel button', async () => {
        const { getByTestId, getAllByTestId } = render(
        <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
             <App/>
        </MemoryRouter>
        );
                  
        //Test setting text and sideBar menu is rendered
        const mainHead = getByTestId('main-header');
        expect(mainHead).toHaveTextContent('Settings');
        expect(getByTestId('sideBar')).toBeInTheDocument();
            
        const navs = getAllByTestId('sidebar-navDiv');
        expect(navs[1]).toHaveTextContent('Needs List');
            
        fireEvent.click(navs[1]);
        let navsLink: any;
                   
        //Test Request Email template rendered on menu
        await waitFor(() => {
        navsLink = getAllByTestId('sidebar-nav');
        expect(navsLink[2]).toHaveTextContent('Request Email Templates');      
        });
            
        fireEvent.click(navsLink[2]);
        let addEmailTemplateBtn: any;
        //Test click on menu and render add email template rendered 
        await waitFor(() => {
        addEmailTemplateBtn = getByTestId('addNewTemplate-btn');
        expect(addEmailTemplateBtn).toBeInTheDocument();
        });
        
        fireEvent.click(addEmailTemplateBtn);
        let  backBtn : any;

        await waitFor(() => {
        const formBody = getByTestId('sub-header');
        expect(formBody).toBeInTheDocument();
         backBtn = getByTestId('subHeader-backBtn')
        expect(backBtn).toHaveTextContent('Back');
        });
            
        const cancelBtn = getByTestId('cancel-btn');
        fireEvent.click(cancelBtn);
      
        await waitFor(() => {
        const formBody = screen.queryByTestId('create-form');
        expect(formBody).toBeNull();
       });
    });
 
    test('show insert token button on focus on selected buttons and disable on selected buttons', async () => {
        const { getByTestId, getAllByTestId } = render(
        <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
             <App/>
        </MemoryRouter>
        );
                  
        //Test setting text and sideBar menu is rendered
        const mainHead = getByTestId('main-header');
        expect(mainHead).toHaveTextContent('Settings');
        expect(getByTestId('sideBar')).toBeInTheDocument();
            
        const navs = getAllByTestId('sidebar-navDiv');
        expect(navs[1]).toHaveTextContent('Needs List');
            
        fireEvent.click(navs[1]);
        let navsLink: any;
                   
        //Test Request Email template rendered on menu
        await waitFor(() => {
        navsLink = getAllByTestId('sidebar-nav');
        expect(navsLink[2]).toHaveTextContent('Request Email Templates');      
        });
            
        fireEvent.click(navsLink[2]);
        let addEmailTemplateBtn: any;
        //Test click on menu and render add email template rendered 
        await waitFor(() => {
        addEmailTemplateBtn = getByTestId('addNewTemplate-btn');
        expect(addEmailTemplateBtn).toBeInTheDocument();
        });
        
        fireEvent.click(addEmailTemplateBtn);
        let  backBtn : any;

        await waitFor(() => {
        const formBody = getByTestId('sub-header');
        expect(formBody).toBeInTheDocument();
        backBtn = getByTestId('subHeader-backBtn')
        expect(backBtn).toHaveTextContent('Back');
        });
            
        const insertTokenBtn = screen.queryByTestId('insertToken-btn');
        expect(insertTokenBtn).toBeNull();

        const fromEmail = getByTestId('from-email');

        fireEvent.click(fromEmail);

        await waitFor(() => {
         let insertTokenBtn = screen.queryByTestId('insertToken-btn');
         expect(insertTokenBtn).not.toBeNull();
        });

        const templateNameInput = getByTestId('templateName-input');
        fireEvent.focus(templateNameInput);

        await waitFor(() => {
            let insertTokenBtn = screen.queryByTestId('insertToken-btn');
            expect(insertTokenBtn).toBeNull();
           });
      });
    
    test('Should check validation on save button click', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
               <App/>
            </MemoryRouter>
           );
          
           //Test setting text and sideBar menu is rendered
           const mainHead = getByTestId('main-header');
           expect(mainHead).toHaveTextContent('Settings');
           expect(getByTestId('sideBar')).toBeInTheDocument();
    
           const navs = getAllByTestId('sidebar-navDiv');
           expect(navs[1]).toHaveTextContent('Needs List');
    
           fireEvent.click(navs[1]);
           let navsLink: any;
           
           //Test Request Email template rendered on menu
           await waitFor(() => {
             navsLink = getAllByTestId('sidebar-nav');
             expect(navsLink[2]).toHaveTextContent('Request Email Templates');      
           });
    
            fireEvent.click(navsLink[2]);
            let addEmailTemplateBtn: any;
            //Test click on menu and render add email template rendered 
            await waitFor(() => {
             addEmailTemplateBtn = getByTestId('addNewTemplate-btn');
             expect(addEmailTemplateBtn).toBeInTheDocument();
            });

          fireEvent.click(addEmailTemplateBtn);
          
          await waitFor(() => {
              const formBody = getByTestId('create-form');
              expect(formBody).toBeInTheDocument();
          });

          const templateNameErrorLabel = screen.queryByTestId('templateName-error');
          const templateDescErrorLabel = screen.queryByTestId('templateDesc-error');
          const fromEmailErrorLabel = screen.queryByTestId('fromEmail-error');
          const ccEmailErrorLabel = screen.queryByTestId('ccEmail-error');
          const subjectErrorLabel = screen.queryByTestId('subjectLine-error');
          const emailBodyErrorLabel = screen.queryByTestId('emailBody-error');

          expect(templateNameErrorLabel).toBeNull()
          expect(templateDescErrorLabel).toBeNull()
          expect(fromEmailErrorLabel).toBeNull()
          expect(ccEmailErrorLabel).toBeNull()
          expect(subjectErrorLabel).toBeNull()
          expect(emailBodyErrorLabel).toBeNull()

          const saveBtn = getByTestId('save-btn');
          fireEvent.click(saveBtn);

          await waitFor(() => {
            let templateNameErrorLabel = screen.queryByTestId('templateName-error');
            let templateDescErrorLabel = screen.queryByTestId('templateDesc-error');
            let fromEmailErrorLabel = screen.queryByTestId('fromEmail-error');
            let ccEmailErrorLabel = screen.queryByTestId('ccEmail-error');
            let subjectErrorLabel = screen.queryByTestId('subjectLine-error');
            let emailBodyErrorLabel = screen.queryByTestId('emailBody-error');

            expect(templateNameErrorLabel).not.toBeNull()
            expect(templateDescErrorLabel).not.toBeNull()
            expect(fromEmailErrorLabel).not.toBeNull()
            expect(ccEmailErrorLabel).toBeNull()
            expect(subjectErrorLabel).not.toBeNull()
            expect(emailBodyErrorLabel).not.toBeNull()
         });

      });
    
    test('Should fire validation on empty fields', async () => {
      const { getByTestId, getAllByTestId } = render(
        <StoreProvider>
          <CreateEmailTemplates/>
        </StoreProvider>
       );
                                                          
          const formBody = getByTestId('create-form');
          expect(formBody).toBeInTheDocument();

          const templateNameErrorLabel = screen.queryByTestId('templateName-error');
          const templateDescErrorLabel = screen.queryByTestId('templateDesc-error');
          const fromEmailErrorLabel = screen.queryByTestId('fromEmail-error');
          const ccEmailErrorLabel = screen.queryByTestId('ccEmail-error');
          const subjectErrorLabel = screen.queryByTestId('subjectLine-error');
          const emailBodyErrorLabel = screen.queryByTestId('emailBody-error');

          expect(templateNameErrorLabel).toBeNull()
          expect(templateDescErrorLabel).toBeNull()
          expect(fromEmailErrorLabel).toBeNull()
          expect(ccEmailErrorLabel).toBeNull()
          expect(subjectErrorLabel).toBeNull()
          expect(emailBodyErrorLabel).toBeNull()

          const saveBtn = getByTestId('save-btn');
          fireEvent.click(saveBtn);

          await waitFor(() => {
            let templateNameErrorLabel = screen.queryByTestId('templateName-error');
            let templateDescErrorLabel = screen.queryByTestId('templateDesc-error');
            let fromEmailErrorLabel = screen.queryByTestId('fromEmail-error');
            let ccEmailErrorLabel = screen.queryByTestId('ccEmail-error');
            let subjectErrorLabel = screen.queryByTestId('subjectLine-error');
            let emailBodyErrorLabel = screen.queryByTestId('emailBody-error');

            expect(templateNameErrorLabel).not.toBeNull()
            expect(templateDescErrorLabel).not.toBeNull()
            expect(fromEmailErrorLabel).not.toBeNull()
            expect(ccEmailErrorLabel).toBeNull()
            expect(subjectErrorLabel).not.toBeNull()
            expect(emailBodyErrorLabel).not.toBeNull()
         });

      });
   
    test('should fire validation "token not allowed" on entering token on restricted fields', async ()=> {
   
      const { container, getByTestId } = render(  <CreateEmailTemplates/>);

      const templateName = container.querySelector(
        "input[name='templateName']"
      );
      if(templateName){
        fireEvent.input(templateName, {
          target: {
            value: "###LoginUserEmail###"
          }
        });
      }
      
      const saveBtn = getByTestId('save-btn');
      fireEvent.click(saveBtn);

      await waitFor(() => {
      const tokenError = getByTestId('token-error');
       expect(tokenError).toBeInTheDocument();
      });
     
         
      });
   
    test('Should input values on fields and save data', async () => {
        const { getByTestId, getAllByTestId, container } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
               <App/>
            </MemoryRouter>
           );
          
           //Test setting text and sideBar menu is rendered
           const mainHead = getByTestId('main-header');
           expect(mainHead).toHaveTextContent('Settings');
           expect(getByTestId('sideBar')).toBeInTheDocument();
    
           const navs = getAllByTestId('sidebar-navDiv');
           expect(navs[1]).toHaveTextContent('Needs List');
    
           fireEvent.click(navs[1]);
           let navsLink: any;
           
           //Test Request Email template rendered on menu
           await waitFor(() => {
             navsLink = getAllByTestId('sidebar-nav');
             expect(navsLink[2]).toHaveTextContent('Request Email Templates');      
           });
    
            fireEvent.click(navsLink[2]);
            let addEmailTemplateBtn: any;
            //Test click on menu and render add email template rendered 
            await waitFor(() => {
             addEmailTemplateBtn = getByTestId('addNewTemplate-btn');
             expect(addEmailTemplateBtn).toBeInTheDocument();
            });

          fireEvent.click(addEmailTemplateBtn);
          
          await waitFor(() => {
              const formBody = getByTestId('create-form');
              expect(formBody).toBeInTheDocument();
          });

         //Inserting values to template name and description input fields
         let templateNameInput = getByTestId('templateName-input');
         let templateDesc  = getByTestId('templateName-desc');

         fireEvent.input(templateNameInput, {
          target: {
            value: "Template By Test"
          }
        });

        fireEvent.input(templateDesc, {
          target: {
            value: "Template description here"
          }
        });

        // inserting values into fromEmail and ccEmail by clicking on token list
        const fromEmail = getByTestId('from-email');        
        fireEvent.click(fromEmail);
        let insertTokenBtn: any;

        await waitFor(() => {
          insertTokenBtn = screen.queryByTestId('insertToken-btn');
         expect(insertTokenBtn).not.toBeNull();    
        });

        fireEvent.click(insertTokenBtn);

        await waitFor(() => {
          let tokenPopup = getByTestId('token-popup');
          expect(tokenPopup).toBeInTheDocument();
         });

        const tokens = getAllByTestId('tb-tr');
        fireEvent.click(tokens[0]);
      
        await waitFor(() => {
          const fromEmailInput: any = container.querySelector(
            "input[name='fromEmail']"
          );
          if(fromEmailInput){
            expect(fromEmailInput.value).toEqual("###LoginUserEmail###");
          }        
        
         });
         const ccEmail = getByTestId('cc-email');        
         fireEvent.click(ccEmail);
        
         let insertTokenBtn2 = getByTestId('insertToken-btn');
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
            expect(ccEmailInput.value).toEqual("###LoginUserEmail###");
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
        expect(emailBody.value).toEqual("###DoucmentList###");
      });
     
       const saveButton = getByTestId('save-btn');
       fireEvent.click(saveButton);
  
        await waitFor(() => {
         let templateForm = screen.queryByTestId('create-form');
         expect(getAllByTestId('td-template-name')).toHaveLength(5);      
         expect(templateForm).toBeNull()
     });

      });

    });