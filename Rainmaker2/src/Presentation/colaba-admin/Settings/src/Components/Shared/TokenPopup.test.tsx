import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock'
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import App from '../../App';
import { RequestEmailTemplateActions } from '../../Store/actions/RequestEmailTemplateActions';

jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/TemplateActions');
jest.mock('../../Store/actions/RequestEmailTemplateActions');
jest.mock('../../Utils/LocalDB');

beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Setting/RequestEmailTemplates');
});

describe('Token Popup', () => {
    
    test('should open token popup on clicking on insert token button', async () => {
        
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
        let insertTokenBtn2: any;

        await waitFor(() => {
          insertTokenBtn2 = screen.queryByTestId('insertToken-btn');
         expect(insertTokenBtn2).not.toBeNull();
        });

        fireEvent.click(insertTokenBtn2);

        await waitFor(() => {
            let tokenPopup = getByTestId('token-popup');
            expect(tokenPopup).toBeInTheDocument();
           });

        const insertTokenBtn3 = getByTestId('insertToken-btn');
        fireEvent.click(insertTokenBtn3);

        await waitFor(() => {
            let tokenPopup = screen.queryByTestId('token-popup');
            expect(tokenPopup).toBeNull();
           });

      });

    test('should render header and tooltip of token popup on clicking on insert token button', async () => {
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
        let insertTokenBtn2: any;

        await waitFor(() => {
          insertTokenBtn2 = screen.queryByTestId('insertToken-btn');
         expect(insertTokenBtn2).not.toBeNull();
        });

        fireEvent.click(insertTokenBtn2);
        let toolTip: any;
        await waitFor(() => {
            let tokenPopup = getByTestId('token-popup');
            let tokenPopupHeader = getByTestId('popup-header');
             toolTip = getAllByTestId('toolTip');

            expect(tokenPopup).toBeInTheDocument();
            expect(tokenPopupHeader).toHaveTextContent('Token List');
            expect(toolTip[1]).toBeInTheDocument();
           });

        fireEvent.mouseEnter(toolTip[1]);

        await waitFor(() => {
            const templateHeaderToolTipDrpbx = getByTestId('toolTip-dropdown');
            expect(templateHeaderToolTipDrpbx).toBeInTheDocument();
            });

      });
   
    test('should open token popup on clicking on insert token button and list tokens', async () => {
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
        let insertTokenBtn2: any;

        await waitFor(() => {
          insertTokenBtn2 = screen.queryByTestId('insertToken-btn');
         expect(insertTokenBtn2).not.toBeNull();
        });

        fireEvent.click(insertTokenBtn2);

        await waitFor(() => {
            let tokenPopup = getByTestId('token-popup');
            expect(tokenPopup).toBeInTheDocument();
           });

          const tableHeads = getByTestId('tb-heads');
          const tokenList = getAllByTestId('tb-td-detail');
          const tokenSymbol = getAllByTestId('tb-td-symbol');

          expect(tableHeads.children[0]).toHaveTextContent('Token Details');
          expect(tableHeads.children[1]).toHaveTextContent('Symbol'); 
          expect(tokenList).toHaveLength(4);
          expect(tokenList[0].children[0]).toHaveTextContent('Login User Email');
          expect(tokenList[0].children[1]).toHaveTextContent('Key for enabling user email address');
          expect(tokenSymbol[0].children[0]).toHaveTextContent('###LoginUserEmail###');
      });

    });
