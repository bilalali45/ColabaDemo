import React from 'react';
import ManageDocumentTemplate from '../ManageDocumentTemplate';

import {
  render,
  cleanup,
  waitForDomChange,
  fireEvent,
  waitFor,
  waitForElement,
  findByTestId,
  act,
  waitForElementToBeRemoved,
  wait,
  getByText,
  screen,
  getByTestId
} from '@testing-library/react';
import {createMemoryHistory} from 'history';
import {EnvConfigMock} from '../../../test_utilities/EnvConfigMock';
import {LocalStorageMock} from '../../../test_utilities/LocalStorageMock';
import {StoreProvider} from '../../../Store/Store';
import {MemoryRouter} from 'react-router-dom';
import App from '../../../App';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NotificationActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Store/actions/TemplateActions');
jest.mock('../../../Utils/LocalDB');

beforeEach(() => {
  EnvConfigMock();
  LocalStorageMock();
  const history = createMemoryHistory();
  history.push('/ManageDocumentTemplate');
});

describe('ManageDocumentTemplateBody', async () => {
  test('ManageDocumentTemplateBody : Render and Delete First', async () => {
    const {getByTestId, getAllByTestId, debug} = render(
      <StoreProvider>
        <ManageDocumentTemplate />
      </StoreProvider>
    );
    expect(getByTestId('loader')).toBeTruthy();
    await waitFor(() => {
      expect(getByTestId('ManageDocumentTemplateBody')).toBeTruthy();
    });
    fireEvent.click(getAllByTestId('settings__delete-element')[0]);
  });

  test('ManageDocumentTemplateBody : Add Document Popup Open', async () => {
    const {getByTestId, getAllByTestId, debug} = render(
      <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']}>
        <StoreProvider>
          <ManageDocumentTemplate />
        </StoreProvider>
      </MemoryRouter>
    );
    expect(getByTestId('loader')).toBeTruthy();
    await waitFor(() => {
      expect(getByTestId('ManageDocumentTemplateBody')).toBeTruthy();
    });
    fireEvent.click(getByTestId('addDocsHandler-accordionMyTemplates'));
  });

  test('Should show Add Document Overlay', async () => {
    const {getByText, getByTestId, getAllByTestId} = render(
      <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']}>
        <StoreProvider>
          <ManageDocumentTemplate />
        </StoreProvider>
      </MemoryRouter>
    );
    let addDocumentText: any;
    await waitFor(() => {
      addDocumentText = getByText('Add Document');
      expect(addDocumentText).toBeInTheDocument();
    });

    fireEvent.click(addDocumentText);
    let docPopOver;
    await waitFor(() => {
      docPopOver = getByTestId('popup-add-doc');
      expect(docPopOver).toBeInTheDocument();
    });

    let docCats = getAllByTestId('doc-cat');

    expect(docCats[2]).toHaveTextContent("Liabilities")

    fireEvent.click(docCats[2]);

    const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');

    expect(selectedCatDocsContainer).toHaveTextContent('Liabilities');

    const itemsToClick = getAllByTestId('doc-item');

    expect(itemsToClick[2]).toHaveTextContent('Property Tax Statement');

    await fireEvent.click(itemsToClick[2]);

    fireEvent.click(document.body);
  });

  test('Should select document from commonly used', async () => {
    const {getByText, getByTestId, getAllByTestId} = render(
      <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']}>
        <StoreProvider>
          <ManageDocumentTemplate />
        </StoreProvider>
      </MemoryRouter>
    );
    let addDocumentText: any;
    await waitFor(() => {
      addDocumentText = getByText('Add Document');
      expect(addDocumentText).toBeInTheDocument();
    });
    fireEvent.click(addDocumentText);
    let docPopOver;
    await waitFor(() => {
      docPopOver = getByTestId('popup-add-doc');
      expect(docPopOver).toBeInTheDocument();
    });

    let docCats = getByTestId('all-docs');

    expect(docCats).toHaveTextContent("All")
    fireEvent.click(docCats);


    let  itemsToClick:any;
    await waitFor(()=>{

      itemsToClick = getAllByTestId('doc-item');
    })

    expect(itemsToClick[2]).toHaveTextContent(
      'Purchase Contract Deposit Check'
    );

    await fireEvent.click(itemsToClick[2]);

    fireEvent.click(document.body);
  });

  test('Should show add custom document header', async () => {
    const {getByText, getByTestId, getAllByTestId} = render(
      <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']}>
        <StoreProvider>
          <ManageDocumentTemplate />
        </StoreProvider>
      </MemoryRouter>
    );
    let addDocumentText: any;
    await waitFor(() => {
      addDocumentText = getByText('Add Document');
      expect(addDocumentText).toBeInTheDocument();
    });
    fireEvent.click(addDocumentText);
    let docPopOver;
    await waitFor(() => {
      docPopOver = getByTestId('popup-add-doc');
      expect(docPopOver).toBeInTheDocument();
    });

    let docCats = getAllByTestId('doc-cat');

    fireEvent.click(docCats[5]);

    const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');

    expect(selectedCatDocsContainer).toHaveTextContent('Other');

    const customeDocHeader = getByTestId('custom-doc-header');

    expect(customeDocHeader).toHaveTextContent('Add Custom Document');
  });

  test('Should search a document from search textbox', async () => {
    const { getByTestId, getAllByTestId, getByText } = render(
      <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']}>
      <StoreProvider>
        <ManageDocumentTemplate />
      </StoreProvider>
    </MemoryRouter>
    );

    let addDocumentText:any;
    await waitFor(() => {
            addDocumentText  = getByText('Add Document');
            expect(addDocumentText).toBeInTheDocument();

        })

        fireEvent.click(addDocumentText);


        let docPopOver;
        await waitFor(() => {
            docPopOver = getByTestId('popup-add-doc');
            expect(docPopOver).toBeInTheDocument();
        });

        expect(getByText("All")).toBeInTheDocument();

        const searchTextBox = getByTestId("search-doc-name")
        expect(searchTextBox).toBeInTheDocument();

        fireEvent.change(searchTextBox, { target: { value: "Rental Agreement" } });

        expect(getByText("Search Result")).toBeInTheDocument();

        const docs = getAllByTestId("doc-item");
        expect(docs[0]).toHaveTextContent("Rental Agreement");

    });

  test('ManageDocumentTemplateHeader render list', async () => {
    const {getByTestId, getAllByTestId, container} = render(
      <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']}>
        <StoreProvider>
          <App />
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
      expect(navsLink[1]).toHaveTextContent('Document Templates');
    });

    fireEvent.click(navsLink[1]);

    await waitFor(() => {
      let Header = getAllByTestId('header-title-text');
      expect(Header[0].children[0]).toHaveTextContent('Document Templates');
      expect(getByTestId('addNewTemplate-btn')).toBeInTheDocument();
      //Checking ToolTip
      const notificationHeader = getByTestId('header-toolTip');
      expect(notificationHeader.children[0]).toHaveClass('info-display');
    });

    const templateHeaderToolTip = getByTestId('toolTip');
    fireEvent.mouseEnter(templateHeaderToolTip);

    await waitFor(() => {
      const templateHeaderToolTipDrpbx = getByTestId('infoDropdown');
      expect(templateHeaderToolTipDrpbx).toHaveTextContent('Document Templates');

    });
    
      //Test My template Section
      expect(getByTestId('myTemplate')).toHaveTextContent('My Templates');
      expect(getByTestId('myTemplate-sec')).toBeInTheDocument();
      expect(getAllByTestId('myTemplate-list')).toHaveLength(6);

      //Test System Template Section
    expect(getByTestId('systemTemplate')).toHaveTextContent('System Templates');
    expect(getByTestId('systemTemplate-sec')).toBeInTheDocument();
    expect(getAllByTestId('tenantTemplate-list')).toHaveLength(8);
    });

    test('should opened and listed documents of first item in template list', async () => {
 
      const {getByTestId, getAllByTestId, container} = render(
        <MemoryRouter initialEntries={['/Setting/RequestEmailTemplates']}>
          <StoreProvider>
            <App />
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
        expect(navsLink[1]).toHaveTextContent('Document Templates');
      });
      fireEvent.click(navsLink[1]);

      await waitFor(() => {
        let Header = getAllByTestId('header-title-text');
        expect(Header[0].children[0]).toHaveTextContent('Document Templates');
        expect(getByTestId('addNewTemplate-btn')).toBeInTheDocument();
        //Checking ToolTip
        const notificationHeader = getByTestId('header-toolTip');
        expect(notificationHeader.children[0]).toHaveClass('info-display');
      });

    
       await waitFor(() => {
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
});
