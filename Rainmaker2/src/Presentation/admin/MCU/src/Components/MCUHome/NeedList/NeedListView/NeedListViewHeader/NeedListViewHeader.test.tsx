import React from 'react';
import {render, waitForDomChange, fireEvent} from '@testing-library/react';
import App from '../../../../../App';
import {MockEnvConfig} from '../../../../../test_utilities/EnvConfigMock';
import {MockLocalStorage} from '../../../../../test_utilities/LocalStoreMock';
import {MemoryRouter} from 'react-router-dom';

jest.mock('axios');
jest.mock('../../../../../Store/actions/UserActions');
jest.mock('../../../../../Store/actions/NeedListActions');
jest.mock('../../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();
});

test('should render main heading', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const manageDocumentTemplateBtn = getByText('Needs List');
  expect(manageDocumentTemplateBtn).toBeInTheDocument();

});

test('should render need List Switch Label', async () => {
    const {getByTestId} = render(
      <MemoryRouter initialEntries={[Url]}>
        <App />
      </MemoryRouter>
    );
  
    await waitForDomChange();
  
    const needListSwitchLabel = getByTestId('needListSwitchLabel');
    expect(needListSwitchLabel).toHaveTextContent("All");
    expect(needListSwitchLabel).toHaveTextContent("Pending");
  
  });


  test('should render need List Switch', async () => {
    const {getByTestId} = render(
      <MemoryRouter initialEntries={[Url]}>
        <App />
      </MemoryRouter>
    );
  
    await waitForDomChange();
  
    const needListSwitch = getByTestId('needListSwitch');
    expect(needListSwitch).toHaveAttribute("Checked")
    fireEvent.click(needListSwitch)
    expect(needListSwitch).not.toHaveAttribute("Checked")
  
  });

  
  test('should render add button', async () => {
    const {getByText} = render(
      <MemoryRouter initialEntries={[Url]}>
        <App />
      </MemoryRouter>
    );
  
    await waitForDomChange();
  
    const addButton = getByText('Add');
    expect(addButton).toBeInTheDocument();
    
    const addButtonIcon = getByText((content, element) => element.className === "zmdi zmdi-plus")
    expect(addButtonIcon).toBeInTheDocument();
  });


