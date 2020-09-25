import React, { Children } from 'react';
import {
  render,
  waitForDomChange,
  fireEvent,
  waitForElement,
  waitFor
} from '@testing-library/react';
import App from '../../../../../App';
import {MockEnvConfig} from '../../../../../test_utilities/EnvConfigMock';
import {MockLocalStorage} from '../../../../../test_utilities/LocalStoreMock';
import {MemoryRouter} from 'react-router-dom';
import { getByText } from '@testing-library/react';

jest.mock('axios');
jest.mock('../../../../../Store/actions/UserActions');
jest.mock('../../../../../Store/actions/NeedListActions');
jest.mock('../../../../../Store/actions/ReviewDocumentActions');
jest.mock('../../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3';

beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();
});

test('should render table headings', async () => {
  const {getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const document = getByText('Document');
  expect(document).toBeInTheDocument();

  const status = getByText('Status');
  expect(status).toBeInTheDocument();

  const fileName = getByText('File Name');
  expect(fileName).toBeInTheDocument();
});

test('should render need list', async () => {
  const {getByTestId, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const needList = getAllByTestId('needList');
  expect(needList[0]).toHaveTextContent('Covid-19');
  expect(needList[0]).toHaveTextContent('Pending Review');

  const statusIcon = getAllByTestId('needListStatus');
  expect(statusIcon[0]).toHaveClass('status-bullet pending');

  expect(needList[0]).toHaveTextContent('Review');
});

test('should render pending Status ', async () => {
  const {getByTestId, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const needList = getAllByTestId('needList');
  expect(needList[1]).toHaveTextContent('Pending Review');

  const statusIcon = getAllByTestId('needListStatus');
  expect(statusIcon[1]).toHaveClass('status-bullet pending');

  const actionButton = getAllByTestId('actionButton');
  expect(actionButton[1]).toHaveTextContent('Review');
  // expect(actionButton[1]).toHaveClass("btn btn-primary btn-sm");
  expect(actionButton[1]).not.toContainHTML(
    '<button class="btn btn-delete btn-sm"><em class="zmdi zmdi-close"></em></button>'
  );
});

test('should render started status', async () => {
  const {getByTestId, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const needList = getAllByTestId('needList');
  expect(needList[2]).toHaveTextContent('Government Issued Identification');
  expect(needList[2]).toHaveTextContent('Started');

  const statusIcon = getAllByTestId('needListStatus');
  expect(statusIcon[2]).toHaveClass('status-bullet started');

  const actionButton = getAllByTestId('actionButton');
  expect(actionButton[2]).toHaveTextContent('Details');
  expect(actionButton[2].children[0]).toHaveClass("btn btn-secondry btn-sm nl-btn");
  expect(actionButton[2]).not.toContainHTML(
    '<button class="btn btn-delete btn-sm"><em class="zmdi zmdi-close"></em></button>'
  );
});

test('should render borrower to do status', async () => {
  const {getByTestId, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();
  const needList = getAllByTestId('needList');

  expect(needList[3]).toHaveTextContent('Work Visa - Work Permit');
  expect(needList[3]).toHaveTextContent('Borrower to do');

  const statusIcon = getAllByTestId('needListStatus');
  expect(statusIcon[3]).toHaveClass('status-bullet borrower');

  const needListFiles = getAllByTestId('need-list-files');
  expect(needListFiles[3]).toHaveTextContent('No file submitted yet');

  const actionButton = getAllByTestId('actionButton');
  expect(actionButton[3]).toHaveTextContent('Details');
  expect(actionButton[3].children[0]).toHaveClass("btn btn-secondry btn-sm nl-btn");
  expect(actionButton[3]).toContainHTML(
    '<button data-testid="btn-delete" class="btn btn-delete btn-sm"><em class="zmdi zmdi-close"></em></button>'
  );
});

test('should render completed status', async () => {
  const {getByTestId, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const needListSwitch = getByTestId('needListSwitch');
  fireEvent.click(needListSwitch);

  await waitForDomChange();

  const needList = getAllByTestId('needList');
  expect(needList[1]).toHaveTextContent('Mortgage Statement');
  expect(needList[1]).toHaveTextContent('Completed');

  const needListFiles = getAllByTestId('need-list-files');
  expect(needListFiles[1]).not.toHaveTextContent('No file submitted yet');

  const statusIcon = getAllByTestId('needListStatus');
  expect(statusIcon[1]).toHaveClass('status-bullet completed');

  const actionButton = getAllByTestId('actionButton');
  expect(actionButton[1]).toHaveTextContent('Details');
  // expect(actionButton[1]).toHaveClass("btn btn-secondry btn-sm");

  // expect(getByTestId('btn-delete')).not.toBeInTheDocument()
  expect(actionButton[1]).not.toContainHTML(
    '<button class="btn btn-delete btn-sm"><em class="zmdi zmdi-close"></em></button>'
  );
});

test('should show delete popup', async () => {
  const {getByText, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const btnDelete = getAllByTestId('btn-delete');
  expect(btnDelete[0]).toBeInTheDocument();
  fireEvent.click(btnDelete[0]);
  const deleteWarning = getByText('Remove this document from Needs List?');
  expect(deleteWarning).toBeInTheDocument();

  const BtnNo = getByText('No');
  expect(BtnNo).toBeInTheDocument();

  const BtnYes = getByText('Yes');
  expect(BtnYes).toBeInTheDocument();

  fireEvent.click(BtnNo);
  expect(deleteWarning).not.toBeInTheDocument();

  
});

test('should redirect to documnet review page when click on the document name', async () => {
  const {getByTestId, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const docNameLink = getAllByTestId("file-link");
  fireEvent.click(docNameLink[0]);

  await waitFor(() => {
    let reviewHeader = getByTestId('review-headerts');
    expect(reviewHeader).toHaveTextContent('Review Document');
  })

});

test('should redirect to documnet review page when click on Review button', async () => {
  const {getByText, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();
  const detailBtns=  getAllByTestId('needList-detailBtnts');      
          fireEvent.click(detailBtns[0]);

          await waitFor(() => {
            
           
            expect(getByText('Document Details'));
          })
});
