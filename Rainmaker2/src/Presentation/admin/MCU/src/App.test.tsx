import React from 'react';
import {render, waitForDomChange, fireEvent} from '@testing-library/react';
import App from './App';
import {MockEnvConfig} from './test_utilities/EnvConfigMock';
import {MockLocalStorage} from './test_utilities/LocalStoreMock';
import {MemoryRouter} from 'react-router-dom';

jest.mock('axios');
jest.mock('./Store/actions/UserActions');
jest.mock('./Store/actions/NeedListActions');
jest.mock('./Utils/LocalDB');

beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();
});

test('renders learn react link', async () => {
  const {getByText, getByTestId} = render(
    <MemoryRouter initialEntries={['/DocumentManagement']}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  // const linkElement = getByText('Manage Document Template');
  // expect(linkElement).toBeInTheDocument();

  // fireEvent.click(linkElement);

  // expect(getByTestId('tempate-manager')).toHaveTextContent('Add Documents');
});
