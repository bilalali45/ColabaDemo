import React from 'react';
import { render, waitFor, waitForDomChange, waitForElement, waitForElementToBeRemoved, wait } from '@testing-library/react';
import App from './App';
import { MockLocalStorage } from './test_utilities/LocalStoreMock';
import { MockEnvConfig } from './test_utilities/EnvConfigMock';
import { Home } from './components/Home/Home';
import { MemoryRouter } from 'react-router-dom';
import { act } from '@testing-library/react'
import { createMemoryHistory } from 'history'

jest.mock('axios');
jest.mock('./store/actions/UserActions');
jest.mock('./store/actions/LoanActions');
jest.mock('./services/auth/Auth');

beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();
  const history = createMemoryHistory()
  history.push('/');
});

test('Should render borrower name in the header', async () => {

  const { getByText, getByTestId } = render(<App />, { wrapper: MemoryRouter });
  await act(async () => {
  })
  const header = getByText('Hello, John Doe');

  expect(header).toHaveClass('d-name d-none d-sm-block dropdown-toggle');

});


test('Should render activity header', async () => {

  const { getByTestId } = render(<App />, { wrapper: MemoryRouter });
  await waitForDomChange();
  const activityHeader = getByTestId('activity-header');
  expect(activityHeader).toHaveTextContent('Dashboard');
  expect(activityHeader).toHaveClass('activityHeader');

});

test('Should render loan status', async () => {

  const { getByTestId } = render(<App />, { wrapper: MemoryRouter });
 
  await waitForDomChange();
  const loan = getByTestId('loanStatus');
  expect(loan).toHaveTextContent('45,645');

});

