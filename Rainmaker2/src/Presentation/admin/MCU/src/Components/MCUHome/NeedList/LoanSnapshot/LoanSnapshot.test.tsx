import React from 'react';
import {render, waitForDomChange, fireEvent} from '@testing-library/react';
import App from '../../../../App';
import {MockEnvConfig} from '../../../../test_utilities/EnvConfigMock';
import {MockLocalStorage} from '../../../../test_utilities/LocalStoreMock';
import {MemoryRouter} from 'react-router-dom';

jest.mock('axios');
jest.mock('../../../../Store/actions/UserActions');
jest.mock('../../../../Store/actions/NeedListActions');
jest.mock('../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();
});

test('should render loan no', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const loanNoLabel = getByText('Loan No.');
  expect(loanNoLabel).toBeInTheDocument();

  const loanNoVal = getByText('50020000155');
  expect(loanNoVal).toBeInTheDocument();

});

test('should render Primary & co-Borrower', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const borrowerLabel = getByText('Primary & co-Borrower');
  expect(borrowerLabel).toBeInTheDocument();

  const borrowerVal = getByText('Taruf Ali, Co Borr Last Name');
  expect(borrowerVal).toBeInTheDocument();

});


test('should render Est. Closing Date', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const estClosingDateLabel = getByText('Est. Closing Date');
  expect(estClosingDateLabel).toBeInTheDocument();

  const estClosingDateText = getByText('Aug 29, 2020');
  expect(estClosingDateText).toBeInTheDocument();

});


test('should render loan Purpose', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const loanPurposeLabel = getByText('Loan Purpose');
  expect(loanPurposeLabel).toBeInTheDocument();

  const loanPurposeText = getByText('Purchase a home');
  expect(loanPurposeText).toBeInTheDocument();

});

test('should render Property Value', async () => {
  const {getByText, getByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const propValLabel = getByText('Property Value');
  expect(propValLabel).toBeInTheDocument();

  const propertyValText = getByTestId('propertyVal');
  expect(propertyValText).toHaveTextContent('55,000');
  expect(propertyValText).toHaveTextContent('$');

});

test('should render loan Amount', async () => {
  const {getByText, getByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const loanAmtLabel = getByText('Loan Amount');
  expect(loanAmtLabel).toBeInTheDocument();

  const loanAmtText = getByTestId('loanAmt');
  expect(loanAmtText).toHaveTextContent('45,000');
  expect(loanAmtText).toHaveTextContent('$');

});

test('should render Milestone/Status', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const manageDocumentTemplateBtn = getByText('Milestone/Status');
  expect(manageDocumentTemplateBtn).toBeInTheDocument();

  const backBtn = getByText('Application Submitted');
  expect(backBtn).toBeInTheDocument();

});

test('should render Property type', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const propTypeLabel = getByText('Property type');
  expect(propTypeLabel).toBeInTheDocument();

  const propTypeText = getByText('Single Family Detached');
  expect(propTypeText).toBeInTheDocument();

});

test('should render Rate', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const rateLabel = getByText('Rate');
  expect(rateLabel).toBeInTheDocument();

});


test('should render loan Program', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const loanProgramLabel = getByText('Loan Program');
  expect(loanProgramLabel).toBeInTheDocument();

});

test('should render Property Address', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const propAddressLabel = getByText('Property Address');
  expect(propAddressLabel).toBeInTheDocument();

  const propAddressText = getByText('New27Aug # 2708');
  const propAddress2Text = getByText('Houston, TX 77023')
  expect(propAddressText).toBeInTheDocument();
  expect(propAddress2Text).toBeInTheDocument();

});

test('should render Lock status', async () => {
  const {getByText, getByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const lockStatusLabel = getByText('Lock status');
  expect(lockStatusLabel).toBeInTheDocument();

  const lockStatusText = getByTestId('SVGopenLock');
  expect(lockStatusText).toBeInTheDocument();

});

test('should render Lock Date', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const lockDateLabel = getByText('Lock Date');
  expect(lockDateLabel).toBeInTheDocument();

  const lockDateText = getByText('Aug 18, 2020');
  expect(lockDateText).toBeInTheDocument();

});



test('should render Expiration date', async () => {
  const {getByText, getByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const expirationDateLabel = getByText('Expiration date');
  expect(expirationDateLabel).toBeInTheDocument();

});