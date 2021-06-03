import React, {Children} from 'react';
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
import {getByText} from '@testing-library/react';
import {getByTestId} from '@testing-library/react';
import { getByAltText } from '@testing-library/react';

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
  const {getByText, getByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const tableHeadings = getByTestId('needlist-table-header');

  expect(tableHeadings.children[0]).toHaveTextContent('Document');

  expect(tableHeadings.children[1]).toHaveTextContent('Status');

  expect(tableHeadings.children[2]).toHaveTextContent('File Name');

  expect(tableHeadings.children[3]).toHaveTextContent('sync to LOS');
});

test('should sort on Document click', async () => {
//   const {getByTestId, getAllByTestId} = render(
//     <MemoryRouter initialEntries={[Url]}>
//       <App />
//     </MemoryRouter>
//   );

//   await waitForDomChange();

//   const Doc = getByTestId('needlist-table-header').children[0].children[0];

  
 
//  expect(Doc).toBeDefined()
//  fireEvent.click(Doc);
//  await waitFor(() => {
//  const needList = getAllByTestId('needList');
//   expect(needList[1]).toHaveTextContent('Government Issued Identification');
//  })

//  fireEvent.click(Doc);
//  await waitFor(() => {
//  const needList = getAllByTestId('needList');
//   expect(needList[0]).toHaveTextContent('Work Visa - Work Permit');
//  })
});

test('should sort on status click', async () => {
//   const {getByTestId, getAllByTestId} = render(
//     <MemoryRouter initialEntries={[Url]}>
//       <App />
//     </MemoryRouter>
//   );

//   await waitForDomChange();

//   const Status = getByTestId('status-title');

  
 
//  expect(Status).toBeDefined()
//  fireEvent.click(Status);
//  await waitFor(() => {
//  const needList = getAllByTestId('needList');
//   expect(needList[1]).toHaveTextContent('Pending Review');
//  })

//  fireEvent.click(Status);
//  await waitFor(() => {
//  const needList = getAllByTestId('needList');
//   expect(needList[1]).toHaveTextContent('Pending Review');
//  })
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
  expect(actionButton[2].children[0]).toHaveClass(
    'btn btn-secondry btn-sm nl-btn'
  );
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
  expect(actionButton[3].children[0]).toHaveClass(
    'btn btn-secondry btn-sm nl-btn'
  );
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


test('should render sync ', async () => {
  const {getByTestId, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const SyncStatusLabels = getAllByTestId('sync-status');
  expect(SyncStatusLabels[1].children[0]).toHaveTextContent('Synced')
  expect(SyncStatusLabels[0].children[0].children[0].children[0]).toHaveClass('synced')
  
  
});


test('should render Not sync ', async () => {
  const {getByTestId, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const SyncStatusLabels = getAllByTestId('sync-status');
  expect(SyncStatusLabels[2].children[0]).toHaveTextContent('Not Synced')
  expect(SyncStatusLabels[2].children[0].children[0].children[0]).toHaveClass('icon-refresh not_Synced')
  
  
});

test('should sync file on click ', async () => {
  const {getByTestId, getAllByTestId, getByAltText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const SyncStatusLabels = getAllByTestId('sync-status');
  expect(SyncStatusLabels[2].children[0]).toHaveTextContent('Not Synced')
  expect(SyncStatusLabels[2].children[0].children[0].children[0]).toHaveClass('icon-refresh not_Synced')
  
  fireEvent.click(SyncStatusLabels[2].children[0].children[0])
 
  

  await waitForDomChange();

  expect(SyncStatusLabels[2].children[0]).toHaveTextContent('Ready to Sync')
  expect(SyncStatusLabels[2].children[0].children[0].children[0]).toHaveClass("icon-refresh readyto_Sync")

  const syncLosAlert = getByTestId("sync-alert")
  expect(syncLosAlert.children[0].children[0]).toHaveAttribute('alt','syncLosIcon' )
  
  expect(syncLosAlert.children[1]).toHaveTextContent('Are you ready to sync the selected documents?')
  
  expect(syncLosAlert.children[2].children[0]).toHaveTextContent('Sync')
});


test('should should file not sync Alert', async () => {
  const {getByTestId, getAllByTestId, getByText} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();

  const SyncStatusLabels = getAllByTestId('sync-status');
  expect(SyncStatusLabels[2].children[0]).toHaveTextContent('Not Synced')
  expect(SyncStatusLabels[2].children[0].children[0].children[0]).toHaveClass('icon-refresh not_Synced')
  
  fireEvent.click(SyncStatusLabels[2].children[0].children[0])
 
  

  await waitForDomChange();

  expect(SyncStatusLabels[2].children[0]).toHaveTextContent('Ready to Sync')
  expect(SyncStatusLabels[2].children[0].children[0].children[0]).toHaveClass("icon-refresh readyto_Sync")

  const syncLosAlert = getByTestId("sync-alert")
  expect(syncLosAlert.children[0].children[0]).toHaveAttribute('alt','syncLosIcon' )
  
  expect(syncLosAlert.children[1]).toHaveTextContent('Are you ready to sync the selected documents?')
  
  expect(syncLosAlert.children[2].children[0]).toHaveTextContent('Sync')

  fireEvent.click(syncLosAlert.children[2].children[0])

  await waitForDomChange();

    const alertHeader = getByText('Document Synchronization Failed')
  expect(alertHeader).toBeDefined()
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

  const docNameLink = getAllByTestId('file-link');
  fireEvent.click(docNameLink[0]);

  await waitFor(() => {
    let reviewHeader = getByTestId('review-headerts');
    expect(reviewHeader).toHaveTextContent('Review Document');
  });
});

test('should redirect to documnet review page when click on Review button', async () => {
  const {getByText, getAllByTestId} = render(
    <MemoryRouter initialEntries={[Url]}>
      <App />
    </MemoryRouter>
  );

  await waitForDomChange();
  const detailBtns = getAllByTestId('needList-detailBtnts');
  fireEvent.click(detailBtns[0]);

  await waitFor(() => {
    expect(getByText('Document Details'));
  });
});
