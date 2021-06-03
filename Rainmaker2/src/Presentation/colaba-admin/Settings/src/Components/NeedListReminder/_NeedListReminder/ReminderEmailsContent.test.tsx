import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, getByText, screen, getByTestId } from '@testing-library/react';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { StoreProvider } from '../../../Store/Store';
import { ReminderEmailSubHeader } from './ReminderEmailSubHeader';
import { ReminderEmailsContent } from './ReminderEmailsContent';
import { ReminderEmails } from './ReminderEmails';
import App from '../../../App';
import { MemoryRouter } from 'react-router-dom';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/ReminderEmailsActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

const  props = {
 
    insertTokenClick:()=>{}, 
    showinsertToken: true, 
    setSelectedField: ()=>{}, 
    showFooter: true, 
    setShowFooter: ()=>{},
    setCancelClick: ()=>{}
  };

describe('Need List Reminder Emails Content', () => {

    it('Test Email Content Inputs', async()=> {
        const { getByTestId, getAllByTestId } = render(
       <StoreProvider>
           <ReminderEmailsContent {...props}/>
            
       </StoreProvider> 
        );

        await waitFor(() => {                   
            const fromAddressDv = getByTestId('dv-fromAddress');
            const fromEmailInput = getByTestId('from-email');
            const ccAddressDv = getByTestId('dv-ccAddress');
            const ccEmailInput = getByTestId('cc-email');
            const subjectLineDv = getByTestId('dv-subline');
            const subjectInput = getByTestId('subject-input');
            const textEditorLabel = getByTestId('email-body-label');
                                
            expect(fromAddressDv).toHaveTextContent('From Address');
            expect(fromEmailInput).toBeInTheDocument();
            expect(ccAddressDv).toHaveTextContent('CC Address');
            expect(ccEmailInput).toBeInTheDocument();
            expect(subjectLineDv).toHaveTextContent('Subject Line');
            expect(subjectInput).toBeInTheDocument();
            expect(textEditorLabel).toBeInTheDocument();          
          });            
    });

    test('Reminder show insert token button on focus on selected buttons and disable on selected buttons', async () => {
        const { getByTestId, getAllByTestId } = render(
        <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
             <StoreProvider>
             <App/>
             </StoreProvider>
           
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
                   
        //Test Reminder Email template rendered on menu
        await waitFor(() => {
        navsLink = getAllByTestId('sidebar-nav');
        expect(navsLink[3]).toHaveTextContent('Needs List Reminder Emails');      
        });
            
        fireEvent.click(navsLink[3]);
        let togglerBtn: any;
        //Test click on menu and render Reminder email template rendered 
        await waitFor(() => {
        togglerBtn = getByTestId('togglerDefault');
        expect(togglerBtn).toBeInTheDocument();
        const subHeader = getAllByTestId('contentSubHeader');         
        expect(subHeader[1].children[0]).toHaveTextContent('Reminder Email - 01');
        });
          
        const insertTokenBtn = screen.queryByTestId('insertToken-btn');
        expect(insertTokenBtn).toBeNull();

        const fromEmail = getByTestId('from-email');
        fireEvent.click(fromEmail);

        await waitFor(() => {
         let insertTokenBtn = screen.queryByTestId('insertToken-btn');
         expect(insertTokenBtn).not.toBeNull();
        });

        const toggler = getByTestId('togglerDefault');
        expect(toggler).toBeInTheDocument();
       
        fireEvent.click(toggler);

       await waitFor(() => {
        const fromAddressDv = screen.queryByTestId('dv-fromAddress');
        expect(fromAddressDv).toBeNull();
       });

    });

    test('Check default values for email fields', async () => {
        const { getByTestId, getAllByTestId,container } = render(
        <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
             <StoreProvider>
             <App/>
             </StoreProvider>
           
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
                   
        //Test Reminder Email template rendered on menu
        await waitFor(() => {
        navsLink = getAllByTestId('sidebar-nav');
        expect(navsLink[3]).toHaveTextContent('Needs List Reminder Emails');      
        });
            
        fireEvent.click(navsLink[3]);
        let listItemBtns: any;
       
        await waitFor(() => {
        const subHeader = getAllByTestId('contentSubHeader');         
        expect(subHeader[1].children[0]).toHaveTextContent('Reminder Email - 01');    
        });
                                                   
        const insertTokenBtn = screen.queryByTestId('insertToken-btn');
        expect(insertTokenBtn).toBeNull();

        const ccEmail = getByTestId('cc-email');        
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
              expect(ccEmailInput.value).toEqual("###Co-BorrowerEmailAddress###");
            }                 
           });
    }); 


});