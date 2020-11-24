import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { UserActions } from '../../Store/actions/UserActions';
import App from '../../App';
import { StoreProvider } from '../../Store/Store';
import { AssignedRoleActions } from '../../Store/actions/AssignedRoleActions';
import { LocalDB } from '../../Utils/LocalDB';
import ManageDocumentTemplateHeader from './_ManageDocumentTemplate/ManageDocumentTemplateHeader';
import ManageDocumentTemplate from './ManageDocumentTemplate';




jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/TemplateActions');
jest.mock('../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Profile');
});


describe('Manage Document Template', () => {

  test('Should render "Manage Document Templates" on header', async () => {

    const { getByTestId } = render(
        <StoreProvider>
          <ManageDocumentTemplateHeader/>
        </StoreProvider>
       );

       const templateHeader = getByTestId('notification-header');
       expect(templateHeader).toHaveTextContent('Manage Document Templates');
  });
 
  test('Should render Tooltip on header', async () => {

    const { getByTestId, getAllByTestId } = render(
        <StoreProvider>
          <ManageDocumentTemplateHeader/>
        </StoreProvider>
       );

       const notificationHeader = getByTestId('header-toolTip');
        expect(notificationHeader.children[0]).toHaveClass("info-display")
  });

  test('should click on toolTip and open dropdown', async () => {
    const { getByTestId, getAllByTestId } = render(
      <StoreProvider>
        <ManageDocumentTemplateHeader/>
      </StoreProvider>
    );


    const templateHeaderToolTip = getByTestId('toolTip');
    fireEvent.click(templateHeaderToolTip);

    await waitFor(() => {
    const templateHeaderToolTipDrpbx = getByTestId('toolTip-dropdown');
    expect(templateHeaderToolTipDrpbx).toHaveTextContent('Manage Document settings');
    });

 });

 test('Should render "Add New Template" on header', async () => {

  const { getByTestId, getAllByTestId } = render(
      <StoreProvider>
        <ManageDocumentTemplateHeader/>
      </StoreProvider>
     );

     const templateHeader = getByTestId('notification-header');
     expect(templateHeader).toHaveTextContent('Add New Template');
});

 test('should render Manage Template and template list onclicking on navigation', async () => {
 
  const { getByTestId, getAllByTestId } = render(
         <MemoryRouter initialEntries={['/Profile']} >
            <App/>
         </MemoryRouter>
  );

  await  waitForDomChange();
  
   const mainHead = getByTestId('main-header');
   expect(mainHead).toHaveTextContent('Settings');
   expect(getByTestId('sideBar')).toBeInTheDocument();

   const navs = getAllByTestId('sidebar-navDiv');
   expect(navs[1]).toHaveTextContent('Document Templates');

   const navsLink = getAllByTestId('sidebar-nav');
   fireEvent.click(navsLink[1]);

   await waitFor(() => {
    const templateHeader = getByTestId('notification-header');
    expect(templateHeader).toHaveTextContent('Manage Document Templates');
    //Test My template Section
    expect(getByTestId('myTemplate')).toHaveTextContent('My Templates');
    expect(getByTestId('myTemplate-sec')).toBeInTheDocument();
    expect(getAllByTestId('myTemplate-list')).toHaveLength(6);

    //Test System Template Section
    expect(getByTestId('systemTemplate')).toHaveTextContent('System Templates');
    expect(getByTestId('systemTemplate-sec')).toBeInTheDocument();
    expect(getAllByTestId('syetem-Template-list')).toHaveLength(8);
   });
  
  });
   
 test('should opened and listed documents of first item in template list', async () => {
 
    const { getByTestId, getAllByTestId } = render(
           <MemoryRouter initialEntries={['/Profile']} >
              <App/>
           </MemoryRouter>
    );
  
    await  waitForDomChange();
    
     const mainHead = getByTestId('main-header');
     expect(mainHead).toHaveTextContent('Settings');
     expect(getByTestId('sideBar')).toBeInTheDocument();
  
     const navs = getAllByTestId('sidebar-navDiv');
     expect(navs[1]).toHaveTextContent('Document Templates');
  
     const navsLink = getAllByTestId('sidebar-nav');
     fireEvent.click(navsLink[1]);
  
     await waitFor(() => {
      const templateHeader = getByTestId('notification-header');
      expect(templateHeader).toHaveTextContent('Manage Document Templates');
      const templateList = getAllByTestId('myTemplate-list');

      // Testing first template is open and all others are close
      expect(templateList[0]).toHaveClass('open');
      expect(templateList[1]).toHaveClass('close');
      expect(templateList[2]).toHaveClass('close');
      expect(templateList[3]).toHaveClass('close');
      expect(templateList[4]).toHaveClass('close');
      expect(templateList[5]).toHaveClass('close');

      //Test Document list is rendered
      expect(getByTestId('doc-list-div')).toBeInTheDocument();
      expect(getAllByTestId('doc-list')).toHaveLength(2);
      expect(getByTestId('add-doc-btnMcu')).toBeInTheDocument();
     });
    
  
    });

  test('Should show a new template on "Add New Template" click', async () => {
      const { getByTestId, getAllByTestId } = render(
        <MemoryRouter initialEntries={['/Profile']} >
           <App/>
        </MemoryRouter>
         );

       await  waitForDomChange();

       const navsLink = getAllByTestId('sidebar-nav');
       fireEvent.click(navsLink[1]);
       let newTempBtn: any;
       
       await waitFor(() => {
          newTempBtn = getByTestId('addNewTemplate-btn');
        });
        
        fireEvent.click(newTempBtn);

      await waitFor(() => {
          let tempNameInput = getByTestId('new-template-input');
          expect(getByTestId('new-template-container')).toHaveTextContent('Add document after template is created');
          expect(tempNameInput).toBeInTheDocument();
          expect(tempNameInput).toHaveFocus();
      });

      
  })
  
  test('Should add a new template on "Add New Template" click', async () => {
    const { getByTestId, getAllByTestId } = render(
      <MemoryRouter initialEntries={['/Profile']} >
         <App/>
      </MemoryRouter>
       );

     let newTempBtn: any;
     let tempNameInput: any = null;

     await waitFor(() => {
        newTempBtn = getByTestId('addNewTemplate-btn');
      });
      
      fireEvent.click(newTempBtn);

    await waitFor(() => {
         tempNameInput = getByTestId('new-template-input');
        expect(tempNameInput).toBeInTheDocument();
       
      });

    await waitFor(() => {
        fireEvent.blur(tempNameInput);
       });

       let templateList: any;

    await waitFor(() => {
        templateList = getAllByTestId('myTemplate-list');
     });

     expect(templateList).toHaveLength(7);

  });
   
  test('Should insert the clicked document into the current template', async () => {
    const { getByTestId, getAllByTestId, findByText } = render(
        <StoreProvider>
            <MemoryRouter initialEntries={['/Profile']}>
                <ManageDocumentTemplate></ManageDocumentTemplate>
            </MemoryRouter>
        </StoreProvider>
    );
      
    await waitForDomChange();

       let templateList: any;

    
        templateList = getAllByTestId('myTemplate-list');
        expect(templateList).toHaveLength(6);
        expect(templateList[0]).toHaveClass('open')
        const addBtn = getByTestId('add-doc-btnMcu');
        fireEvent.click(addBtn);
        let docPopOver: any;

        await waitFor(() => {
        docPopOver = getByTestId('popup-add-doc');
        expect(docPopOver).toBeInTheDocument();
        });

         let docCats = getAllByTestId('doc-cat');
         
         fireEvent.click(docCats[3]);

         const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');

        expect(selectedCatDocsContainer).toHaveTextContent('Liabilities');

        const itemsToClick = getAllByTestId('doc-item');        
        expect(itemsToClick[1]).toHaveTextContent('Bankruptcy Papers');

        fireEvent.click(itemsToClick[1]);
        fireEvent.click(document.body);

        let tempDocs : any = [];
        await waitFor(() => {
            tempDocs = getAllByTestId('doc-list');          
            expect(tempDocs[1]).toHaveTextContent('Property Tax Statement');
        });
});

});