import React from 'react';
import {
  render,
  waitForDomChange,
  fireEvent,
  waitFor,
  waitForElement
} from '@testing-library/react';
import App from '../../../../App';
import {MemoryRouter} from 'react-router-dom';
import {MockEnvConfig} from '../../../../test_utilities/EnvConfigMock';
import {MockLocalStorage} from '../../../../test_utilities/LocalStoreMock';
import {createMemoryHistory} from 'history';
import {NewNeedList} from '../../NeedList/NewNeedList/NewNeedList';

jest.mock('axios');
jest.mock('../../../../Store/actions/UserActions');
jest.mock('../../../../Store/actions/NeedListActions');
jest.mock('../../../../Store/actions/ReviewDocumentActions');
jest.mock('../../../../Store/actions/TemplateActions');
jest.mock('../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3';
let getByTestId:any, getByText:any, getAllByTestId: any;


const navigatingToReviewNeedListScreen = async () => {
    let {getByTestId, getByText, getAllByTestId} = render(
        <MemoryRouter initialEntries={[Url]}>
          <App />
        </MemoryRouter>
      );
  const addTemplatebtn = getByTestId('addTemplateBtn');

  fireEvent.click(addTemplatebtn);

  let templates;

  templates = getByTestId('templates');

  fireEvent.click(templates.children[1].children[0].children[0].children[0]);

  await waitFor(() => {
    const continueWithTempBtn = getByTestId('continue-with-temp-btn');
    expect(continueWithTempBtn).toBeInTheDocument();

    fireEvent.click(continueWithTempBtn.children[0]);
  });

  let addDocumentBtn: any;
  await waitFor(() => {
    addDocumentBtn = getByTestId('add-document');
    expect(addDocumentBtn).toBeInTheDocument();
  });
  fireEvent.click(addDocumentBtn);

  let docPopOver;
  await waitFor(() => {
    docPopOver = getByTestId('popup-add-doc');
    expect(docPopOver).toBeInTheDocument();
  });

  let docCats = getAllByTestId('doc-cat');

  fireEvent.click(docCats[2]);

  const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');

  expect(selectedCatDocsContainer).toHaveTextContent('Liabilities');

  const itemsToClick = getAllByTestId('doc-item');

  expect(itemsToClick[2]).toHaveTextContent('Rental Agreement');

  fireEvent.click(itemsToClick[2]);

  const selectedTemplate = getByTestId('veiw-selected-template');
  expect(selectedTemplate).toHaveTextContent('Rental Agreement');

  const reviewReqBtn = getByTestId('review-req-btn');
    expect(reviewReqBtn).toBeInTheDocument();
    fireEvent.click(reviewReqBtn);
};
beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();
  const history = createMemoryHistory();
  history.push('/');
});

describe('REVIEW Need list request', () => {
  test('Should show review need list.', async () => {
    let {getByTestId, getByText, getAllByTestId} = render(
        <MemoryRouter initialEntries={[Url]}>
          <App />
        </MemoryRouter>
      );
    await waitForDomChange();

    await navigatingToReviewNeedListScreen();

    
    await waitFor(() => {
      expect(getByText('Review Needs List')).toBeInTheDocument();
    });
  });

  test('Should show review need list docment Name.', async () => {
    
    let {getByTestId, getByTitle, getAllByTestId} = render(
        <MemoryRouter initialEntries={[Url]}>
          <App />
        </MemoryRouter>
      );await waitForDomChange();
    

    await navigatingToReviewNeedListScreen();

    await waitFor(() => {
    expect(getByTitle('Rental Agreement')).toBeInTheDocument();
    });
  });

  test('Should show email review heading with borrower name.', async () => {
    
    let {getByText, getByTitle, getAllByTestId} = render(
        <MemoryRouter initialEntries={[Url]}>
          <App />
        </MemoryRouter>
      );await waitForDomChange();
    

    await navigatingToReviewNeedListScreen();

    await waitFor(() => {
    expect(getByTitle('Review email to Taruf Ali')).toBeInTheDocument();
    expect(getByText("If you'd like, you can customize this email.")).toBeInTheDocument();
    });
  });


  test('Should show email content', async () => {
    
    let {getByText, getByTitle, getByTestId} = render(
        <MemoryRouter initialEntries={[Url]}>
          <App />
        </MemoryRouter>
      );await waitForDomChange();
    

    await navigatingToReviewNeedListScreen();

    await waitFor(() => {
        expect(getByTestId("email-content")).toBeInTheDocument();
    });
  });

  test('Should edit email content', async () => {
    
    let {getByText, getByTitle, getByTestId} = render(
        <MemoryRouter initialEntries={[Url]}>
          <App />
        </MemoryRouter>
      );await waitForDomChange();
    

    await navigatingToReviewNeedListScreen();

    await waitFor(() => {
        const emailContent = getByTestId("email-content")
        expect(emailContent).toBeInTheDocument();

        fireEvent.change(emailContent, { target: { value: "This is an email review content" } });
    });
  });

  test('Should save email content on blur', async () => {
    
    let {getByText, getByTitle, getByTestId} = render(
        <MemoryRouter initialEntries={[Url]}>
          <App />
        </MemoryRouter>
      );await waitForDomChange();
    

    await navigatingToReviewNeedListScreen();

    await waitFor(() => {
        const emailContent = getByTestId("email-content")
        expect(emailContent).toBeInTheDocument();

        fireEvent.change(emailContent, { target: { value: "This is an email review content" } });
        fireEvent.blur(emailContent, { target: { value: "This is an email review content" } })
    });
  });

  test('Should navigate to need list page on save request click', async () => {
    
    let {getByText, getByTitle, getByTestId} = render(
        <MemoryRouter initialEntries={[Url]}>
          <App />
        </MemoryRouter>
      );await waitForDomChange();
    

    await navigatingToReviewNeedListScreen();

    await waitFor(() => {
        const sendreqBtn = getByTestId("send-req-btn");

        expect(sendreqBtn).toBeInTheDocument();

        fireEvent.click(sendreqBtn);

    });

    await waitFor(()=>{
        expect(getByText("Need list has been sent.")).toBeInTheDocument();
        expect(getByTestId("need-view-test-header")).toBeInTheDocument();
    })
  });
});
