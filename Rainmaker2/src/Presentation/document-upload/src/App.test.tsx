import React from 'react';
import { render, waitFor, waitForDomChange, waitForElement, waitForElementToBeRemoved, wait } from '@testing-library/react';
import App from './App';
import { MockLocalStorage } from './test_utilities/LocalStoreMock';
import { MockEnvConfig } from './test_utilities/EnvConfigMock';
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history'
import { FormatAmountByCountry } from 'rainsoft-js';

jest.mock('axios');
jest.mock('./store/actions/UserActions');
jest.mock('./store/actions/LoanActions');
jest.mock('./store/actions/DocumentActions');
jest.mock('./services/auth/Auth');

beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();
});

test('Should render borrower name in the header', async () => {
  const { getByText } = render(
    <MemoryRouter initialEntries={['/loanportal/3']}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();
  const header = getByText('Hello, John Doe');

  expect(header).toBeInTheDocument();

});

test('Should convert a number into US currency seperated by comma', async () => {

  const amount = 32094802;

  const formatted = FormatAmountByCountry(amount);

  expect(formatted).toEqual('32,094,802');

});