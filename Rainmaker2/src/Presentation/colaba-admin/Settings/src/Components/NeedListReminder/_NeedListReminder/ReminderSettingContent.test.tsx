import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, getByText, screen, getByTestId } from '@testing-library/react';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { StoreProvider } from '../../../Store/Store';
import NeedListReminder from '../NeedListReminder';
import { ReminderSettingContent } from './ReminderSettingContent';
import { MemoryRouter } from 'react-router-dom';
import App from '../../../App';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/ReminderEmailsActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});
let props =  {
    setShowFooter: ()=>{},
    cancelClick : true,
    setCancelClick: ()=>{},
    enableDisableClick: ()=>{}
}


describe('Need List Reminder Emails List', () => {

    it('Test Reminder Email list render', async()=> {
        let needListProps = {
            backHandler: ()=>{}
        }
        const { getByTestId, getAllByTestId } = render(
       <StoreProvider>
           <NeedListReminder {...needListProps}><ReminderSettingContent {...props}/></NeedListReminder>
            
       </StoreProvider> 
        );

     await waitFor(() => {
        const listText = getByTestId('reminder-list-text');         
        expect(listText).toHaveTextContent('Reminder Emails'); 
        const reminderList = getAllByTestId('emailreminder-list'); 
        expect(reminderList).toHaveLength(3);  
        const addReminderBtn = getByTestId('add-reminder-btn'); 
        expect(addReminderBtn).toBeInTheDocument();
        expect(addReminderBtn).toHaveTextContent('Add Reminder Email')
       })
        
    });


    it('Click on Reminder List item', async()=> {
        const { getByTestId, getAllByTestId } = render(
       <StoreProvider>        
           <ReminderSettingContent {...props} />           
       </StoreProvider> 
        );
        let listItemBtns: any;
     await waitFor(() => {         
        const reminderList = getAllByTestId('emailreminder-list'); 
        expect(reminderList).toHaveLength(3);  
        listItemBtns = getAllByTestId('toggle-drpdwn-btn');
        expect(listItemBtns).toHaveLength(3);  
       })
        fireEvent.click(listItemBtns[0]);
        await waitFor(() => {     
            const itemControl = getByTestId('item-control');
            expect(itemControl).toBeInTheDocument();
        });
    });
 
    test('Click on cancel button', async () => {
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
        let togglerBtn: any;
       
        //Test click on menu and render Reminder email template rendered 
        await waitFor(() => {
        togglerBtn = getByTestId('togglerDefault');
        expect(togglerBtn).toBeInTheDocument();
        const subHeader = getAllByTestId('contentSubHeader');         
        expect(subHeader[1].children[0]).toHaveTextContent('Reminder Email - 01');
             
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

          let cancelBtn: any;
        await waitFor(() => {
           cancelBtn = getByTestId('cancel-btn');
          expect(cancelBtn).toBeInTheDocument();
        });      
         fireEvent.click(cancelBtn);
         let addReminderBtn = getByTestId('add-reminder-btn')
         expect(addReminderBtn).toBeInTheDocument();
         fireEvent.click(addReminderBtn);
    }); 

    test('Add time, enable/disable on item Reminder email', async () => {
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
        listItemBtns = getAllByTestId('toggle-drpdwn-btn');
        expect(listItemBtns).toHaveLength(3);  
        const itemControl = screen.queryByTestId('item-control');
        expect(itemControl).toBeNull();      
        });

        fireEvent.click(listItemBtns[0]);
        let meriTimeToggler: any;
        await waitFor(() => {
         const itemControl = screen.queryByTestId('item-control');
         expect(itemControl).not.toBeNull();  
         meriTimeToggler = getByTestId('togglerTextBy');
         expect(meriTimeToggler).not.toBeNull();
        });
        fireEvent.click(meriTimeToggler);
        let dropdown:any;

        await waitFor(() => {
          dropdown = getAllByTestId('dropDownClick');
          const dropDownMenu = screen.queryByTestId('dropDownMenu');
          expect(dropDownMenu).toBeNull();            
        });

        fireEvent.click(dropdown[0]);
        let menuItems:any; 
        await waitFor(() => {
            let dropDownMenu = screen.queryByTestId('dropDownMenu');
            expect(dropDownMenu).not.toBeNull();   
            menuItems = getAllByTestId('dropDownMenuItem');        
        });

        fireEvent.click(menuItems[0]);
        await waitFor(() => {
            let dropDownMenu = screen.queryByTestId('dropDownMenu');
            expect(dropDownMenu).toBeNull();   
        });
        fireEvent.click(dropdown[1]);

        await waitFor(() => {
            let dropDownMenu = screen.queryByTestId('dropDownMenu');
            expect(dropDownMenu).not.toBeNull();   
            menuItems = getAllByTestId('dropDownMenuItem');        
        });
        fireEvent.click(menuItems[0]);
    }); 


});