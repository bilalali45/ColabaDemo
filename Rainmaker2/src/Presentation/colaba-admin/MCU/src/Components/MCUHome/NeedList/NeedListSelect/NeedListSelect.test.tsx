import React from 'react';
import Dropdown from 'react-bootstrap/Dropdown';
import {Template} from '../../../../Entities/Models/Template';
import {
  MyTemplate,
  TenantTemplate
} from '../../TemplateManager/TemplateHome/TemplateListContainer/TemplateListContainer';
import {Link, useLocation} from 'react-router-dom';
import {TemplateDocument} from '../../../../Entities/Models/TemplateDocument';
import {Store} from '../../../../Store/Store';
import {TemplateActionsType} from '../../../../Store/reducers/TemplatesReducer';
import {LocalDB} from '../../../../Utils/LocalDB';
import {NeedListActionsType} from '../../../../Store/reducers/NeedListReducer';
import Overlay from 'react-bootstrap/Overlay';
import Popover from 'react-bootstrap/Popover';

import {
  render,
  waitForDomChange,
  fireEvent,
  getQueriesForElement,
  waitFor,
  getAllByTestId
} from '@testing-library/react';
import App from '../../../../App';
import {MockEnvConfig} from '../../../../test_utilities/EnvConfigMock';
import {MockLocalStorage} from '../../../../test_utilities/LocalStoreMock';
import {MemoryRouter} from 'react-router-dom';

jest.mock('axios');
jest.mock('../../../../Store/actions/UserActions');
jest.mock('../../../../Store/actions/NeedListActions');
jest.mock('../../../../Utils/LocalDB');
jest.mock('../../../../Store/actions/TemplateActions');

const Url = '/DocumentManagement/needList/3';

beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();
});

describe('Add New Template Button', () => {
  test('should render add Button', async () => {
    const {getByText, getByTestId} = render(
      <MemoryRouter initialEntries={[Url]}>
        <App />
      </MemoryRouter>
    );

    await waitForDomChange();

    const addTemplate = getByTestId('addTemplate');
    expect(addTemplate.children[0]).toHaveTextContent('Add');
    expect(addTemplate.childNodes[0]).toContainHTML(
      '<em class="zmdi zmdi-plus"></em>'
    );
  });

  test('should render template popup on add button click', async () => {
    const {getByText, getByTestId} = render(
      <MemoryRouter initialEntries={[Url]}>
        <App />
      </MemoryRouter>
    );

    await waitForDomChange();

    const addTemplate = getByTestId('addTemplate');
    const addButton = addTemplate.children[0];
    expect(addButton).toContainHTML('<em class="zmdi zmdi-plus"></em>')
    fireEvent.click(addButton);

    await waitFor(() => {
      const addTemplateDropDown = getByTestId('addTemplateDropDown');

      expect(addTemplateDropDown.children[0]).toHaveTextContent(
        'Select a needs list Template'
      );
      expect(addTemplateDropDown.children[3]).toHaveTextContent(
        'Start from new list'
      );
      expect(addTemplateDropDown.children[3].children[0]).toHaveAttribute(
        'href',
        '/DocumentManagement/newNeedList/3'
      );

      const templates = addTemplateDropDown.children[1];
      expect(templates.children[0]).toHaveTextContent('My Templates');

      const templatesByTenant = addTemplateDropDown.children[2];
      expect(templatesByTenant.children[0]).toHaveTextContent(
        'System Templates'
      );
      //   expect(templatesByTenant.children).toHaveTextContent('test')
    });
  });

  test('should show continue with template button on template check', async () => {
    const {getByText, getByTestId} = render(
      <MemoryRouter initialEntries={[Url]}>
        <App />
      </MemoryRouter>
    );

    await waitForDomChange();

    const addTemplate = getByTestId('addTemplate');
    const addButton = addTemplate.children[0];
    fireEvent.click(addButton);

    let templates;

    templates = getByTestId('templates');

    fireEvent.click(templates.children[1].children[0].children[0].children[0]);

    await waitForDomChange();

    const continueWithTempBtn = getByTestId('continue-with-temp-btn');
    expect(continueWithTempBtn).toBeInTheDocument();

    expect(continueWithTempBtn).toContainHTML('<i class="zmdi zmdi-plus"></i>');

    fireEvent.click(continueWithTempBtn)

    await waitFor(() => {
      expect(getByText('Request Needs List'));
    });
  });

  test('should redirect to request need list page', async () => {

    const {getByText, getByTestId} = render(
      <MemoryRouter initialEntries={[Url]}>
        <App />
      </MemoryRouter>
    );

    await waitForDomChange();

    const addTemplate = getByTestId('addTemplate');
    const addButton = addTemplate.children[0];
    fireEvent.click(addButton);

    let templates;

    templates = getByTestId('templates');

    fireEvent.click(templates.children[1].children[0].children[0].children[0]);

    await waitForDomChange();

    const continueWithTempBtn = getByTestId('continue-with-temp-btn');
    expect(continueWithTempBtn).toHaveTextContent('Continue with Template')
    expect(continueWithTempBtn).toBeInTheDocument();

    fireEvent.click(continueWithTempBtn.children[0])

    await waitFor(() => {
      expect(getByText('Request Needs List'));
    });
  });

  test('should show and hide pop over on hover for tenants templates', async () => {

    const {getAllByTestId, getByTestId} = render(
      <MemoryRouter initialEntries={[Url]}>
        <App />
      </MemoryRouter>
    );

    await waitForDomChange();

    const addTemplate = getByTestId('addTemplate');
    const addButton = addTemplate.children[0];
  
    fireEvent.click(addButton);

    await waitFor(() => {
      const addTemplateDropDown = getByTestId('addTemplateDropDown');

      const templates = addTemplateDropDown.children[1];

      const templatesByTenant = addTemplateDropDown.children[2];
      expect(templatesByTenant.children[0]).toHaveTextContent(
        'System Templates'
      );
    });
      
      const templateItem = getAllByTestId("templatesByTenant");
      const templateNames = getAllByTestId("tenant-template");

      expect(templateNames[0].children[0]).toBeInTheDocument();
      // expect(templateNames[0].children).toBeInTheDocument();
      fireEvent.mouseEnter(templateNames[0])

      let popOver;
      await waitFor(() => {
        popOver = getAllByTestId("popup-temp")
expect(popOver).toHaveLength(2);
expect(popOver[0].children[1]).toHaveTextContent("Standard");
expect(popOver[0].children[2].children[0]).toHaveTextContent("Bank Statements - Two Months");      

      });

      fireEvent.mouseLeave(templateNames[0])
      await waitFor(() => {
      
      });
  });

  test('should show and hide pop over on hover for my templates', async () => {

    const {getAllByTestId, getByTestId} = render(
      <MemoryRouter initialEntries={[Url]}>
        <App />
      </MemoryRouter>
    );

    await waitForDomChange();

    const addTemplate = getByTestId('addTemplate');
    const addButton = addTemplate.children[0];
  
    fireEvent.click(addButton);

    await waitFor(() => {
      const addTemplateDropDown = getByTestId('addTemplateDropDown');      

      const templates = addTemplateDropDown.children[1];
      expect(templates.children[0]).toHaveTextContent('My Templates');
    });
      
      const templateNames = getAllByTestId("my-template");

      expect(templateNames[0].children[0]).toBeInTheDocument();
      // expect(templateNames[0].children).toBeInTheDocument();
      fireEvent.mouseEnter(templateNames[0])

      let popOver;
      await waitFor(() => {
        popOver = getAllByTestId("popup-temp")
expect(popOver).toHaveLength(2);
expect(popOver[0].children[1]).toHaveTextContent("New Template 3");
expect(popOver[0].children[2].children[0]).toHaveTextContent("Purchase Contract Deposit Check");      

      });

      fireEvent.mouseLeave(templateNames[0])
      await waitFor(() => {
      
      });

      fireEvent.mouseMove(templateNames[0])

    
      await waitFor(() => {
        popOver = getAllByTestId("popup-temp")
expect(popOver).toHaveLength(2);
expect(popOver[0].children[1]).toHaveTextContent("New Template 3");
expect(popOver[0].children[2].children[0]).toHaveTextContent("Purchase Contract Deposit Check");      

      });
  });

  test('should show nothing meassage if no doc in template on popover', async () => {

    const {getAllByTestId, getByTestId} = render(
      <MemoryRouter initialEntries={[Url]}>
        <App />
      </MemoryRouter>
    );

    await waitForDomChange();

    const addTemplate = getByTestId('addTemplate');
    const addButton = addTemplate.children[0];
  
    fireEvent.click(addButton);

    await waitFor(() => {
      const addTemplateDropDown = getByTestId('addTemplateDropDown');      

      const templates = addTemplateDropDown.children[1];
      expect(templates.children[0]).toHaveTextContent('My Templates');
    });
      
      const templateNames = getAllByTestId("my-template");

      expect(templateNames[0].children[0]).toBeInTheDocument();
      // expect(templateNames[0].children).toBeInTheDocument();
      fireEvent.mouseEnter(templateNames[1])

      let popOver;
      await waitFor(() => {
        popOver = getAllByTestId("popup-temp")
expect(popOver).toHaveLength(2);
expect(popOver[0].children[1]).toHaveTextContent("New Template 2");
expect(popOver[0].children[2].children[0]).toHaveTextContent("Nothing in template.");      

      });

      fireEvent.mouseLeave(templateNames[0])
      await waitFor(() => {
      
      });


  });
});
