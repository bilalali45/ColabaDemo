import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor } from '@testing-library/react';
import App from './App';
import { MockEnvConfig } from './test_utilities/EnvConfigMock';
import { MockLocalStorage } from './test_utilities/LocalStoreMock';
import { MemoryRouter } from 'react-router-dom';

jest.mock('axios');
jest.mock('./Store/actions/UserActions');
jest.mock('./Store/actions/NeedListActions');
jest.mock('./Utils/LocalDB');
jest.mock('./Store/actions/ReviewDocumentActions');

beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();


});

test('renders learn react link', async () => {
  const { getByText, getByTestId } = render(
    <MemoryRouter initialEntries={['/DocumentManagement/needList/3']}>
      <App />
    </MemoryRouter>
  );

  const linkElement = getByTestId('template-link');
  await waitFor(() => {
    expect(linkElement).toBeInTheDocument();
  })

  fireEvent.click(linkElement);

  await waitFor(() => {
    let templateHeader = getByTestId('tempate-header');
    expect(templateHeader).toHaveTextContent('Manage Document Templates');
  })
  
});
