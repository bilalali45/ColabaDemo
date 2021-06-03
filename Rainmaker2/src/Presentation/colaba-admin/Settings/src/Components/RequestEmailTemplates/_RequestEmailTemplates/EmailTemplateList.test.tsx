import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import App from '../../../App';
import { RequestEmailTemplateActions } from '../../../Store/actions/RequestEmailTemplateActions';
import { LocalDB } from '../../../Utils/LocalDB';


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


describe('Email Template List', () => {

    test('Check Table Header Text', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
                <App />
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

        // table thead text check
        expect(getByTestId('thead-template-name')).toHaveTextContent('Template Name');
        expect(getByTestId('thead-description')).toHaveTextContent('Description');
        expect(getByTestId('thead-subject')).toHaveTextContent('Subject');
        expect(getByTestId('thead-sort-order')).toHaveTextContent('Sort Order');

        await waitFor(() => {
            expect(getAllByTestId('td-template-name')[0]).toHaveTextContent('Template #3');
            expect(getAllByTestId('td-description')[0]).toHaveTextContent('Kindly upload rent detail statement');
            expect(getAllByTestId('td-subject')[0]).toHaveTextContent('You have new tasks to complete for your ###BusinessUnitN...');
            expect(getAllByTestId('td-sort-order')[0]).toContainHTML('<button class="settings-btn settings-btn-sort settings-btn-disabled"><i class="zmdi zmdi-chevron-up"></i></button><button class="settings-btn settings-btn-sort "><i class="zmdi zmdi-chevron-down"></i></button>');
            expect(getAllByTestId('td-delete')[0]).toContainHTML('<button class="settings-btn settings-btn-delete"><i class="zmdi zmdi-close"></i></button>');

            expect(getAllByTestId('td-template-name')[1]).toHaveTextContent('Template #2');
            expect(getAllByTestId('td-description')[1]).toHaveTextContent('Please upload Bank statement');
            expect(getAllByTestId('td-subject')[1]).toHaveTextContent('You have new tasks to complete for your ###BusinessUnitN...');
            expect(getAllByTestId('td-sort-order')[1]).toContainHTML('<td data-testid="td-sort-order"><button class="settings-btn settings-btn-sort "><i class="zmdi zmdi-chevron-up"></i></button><button class="settings-btn settings-btn-sort "><i class="zmdi zmdi-chevron-down"></i></button></td>');
            expect(getAllByTestId('td-delete')[1]).toContainHTML('<button class="settings-btn settings-btn-delete"><i class="zmdi zmdi-close"></i></button>');
        });     
    });
   
    test('Should move to edit email template on double click on row and update data', async () => {
        const { getByTestId, getAllByTestId, container } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
                <App />
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

        // table thead text check
        expect(getByTestId('thead-template-name')).toHaveTextContent('Template Name');
        expect(getByTestId('thead-description')).toHaveTextContent('Description');
        expect(getByTestId('thead-subject')).toHaveTextContent('Subject');
        expect(getByTestId('thead-sort-order')).toHaveTextContent('Sort Order');     
        expect(getAllByTestId('td-template-name')).toHaveLength(4);    
        
        fireEvent.doubleClick(getAllByTestId('td-template-name')[0]);

        await waitFor(() => {
            let templateForm = screen.queryByTestId('create-form');      
            expect(templateForm).toBeInTheDocument();
        });
        let templateNameInput: any;
        await waitFor(() => {
            templateNameInput = container.querySelector(
              "input[name='templateName']"
            );
            if(templateNameInput){
              expect(templateNameInput.value).toEqual("Template #3");
            }        
          
           });

           fireEvent.input(templateNameInput, {
            target: {
              value: "Template No 3"
            }
          });

          const saveButton = getByTestId('save-btn');
          fireEvent.click(saveButton);
  
        await waitFor(() => {
         let templateForm2 = screen.queryByTestId('create-form');     
         expect(templateForm2).toBeNull();
         expect(getAllByTestId('td-template-name')[0]).toHaveTextContent('Template No 3');
       });
        
    });

    test('Should delete template item on clicking on delete button', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
                <App />
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

       let deleteButtons = getAllByTestId('td-delete');
       fireEvent.click(deleteButtons[0].children[0]);
    
       await waitFor(() => {
        let removeDv = screen.queryByTestId('remove-button-dv');
        expect(removeDv).toBeInTheDocument();
      });

      let deleteButton = getByTestId('delete-yes');
      fireEvent.click(deleteButton);
       
      await waitFor(() => {
        let tdsName = getAllByTestId('td-template-name');
        expect(tdsName).toHaveLength(3);
      });
    });

    test('Should Sort item on clicking on sort icon', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']} >
                <App />
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

       let sortButtons = getAllByTestId('td-sort-order');
       fireEvent.click(sortButtons[0].children[1]);
    
       await waitFor(() => {
        expect(getAllByTestId('td-template-name')[0]).toHaveTextContent('Template #2');
      });
      
      fireEvent.click(sortButtons[3].children[0]);
      await waitFor(() => {
        expect(getAllByTestId('td-template-name')[3]).toHaveTextContent('Template #1');
      });
      

    });
});