import React from 'react';
import {render, waitForDomChange, fireEvent} from '@testing-library/react';
import App from '../../../App';
import {MemoryRouter} from 'react-router-dom';
import {MockEnvConfig} from '../../../test_utilities/EnvConfigMock';
import {MockLocalStorage} from '../../../test_utilities/LocalStoreMock';
import {createMemoryHistory} from 'history';
import { DateFormatWithMoment } from '../../../utils/helpers/DateFormat';

jest.mock('axios');
jest.mock('../../../store/actions/UserActions');
jest.mock('../../../store/actions/LoanActions');
jest.mock('../../../store/actions/DocumentActions');
jest.mock('../../../services/auth/Auth');

beforeEach(() => {
    MockLocalStorage();
    MockEnvConfig();
    const history = createMemoryHistory();
    history.push('/');
});

describe('Uploaded Documents', () => {
    test('should render "Uploaded Documents" Text', async () => {
      const { getByTestId } = render(
       <MemoryRouter initialEntries={['/loanportal/activity/3']} >
          <App/>
       </MemoryRouter>
      );

    await waitForDomChange();

    const rightNav = getByTestId('right-nav');
    fireEvent.click(rightNav)
    const uploadedDocuments = getByTestId('uploaded-documents');
    expect(uploadedDocuments).toHaveTextContent('Uploaded Documents')

    });

    test('should render "Documents", "File Name", "Added" column head', async () => {
      const { getByTestId } = render(
          <MemoryRouter initialEntries={['/loanportal/activity/3']} >
              <App/>
          </MemoryRouter>
          );

          await waitForDomChange();

          const rightNav = getByTestId('right-nav');
          fireEvent.click(rightNav);

          const tableHead = getByTestId('uploaded-docs-head');
          expect(tableHead).toHaveTextContent('Documents');
          expect(tableHead).toHaveTextContent('File Name')
          expect(tableHead).toHaveTextContent('Added')  
    });

    test('should show the list of uploaded documents', async () => {
        const { getAllByTestId } = render(
             <MemoryRouter initialEntries={['/loanportal/uploadedDocuments/3']} >
              <App/>
             </MemoryRouter>
          );

          await waitForDomChange();

          const allDocTypes = getAllByTestId('doc-type');
          expect(allDocTypes[0]).toHaveTextContent('Bank Statements - Two Months');
                  
          const allDocFiles = getAllByTestId('doc-file');
          expect(allDocFiles[0]).toHaveTextContent('images-copy-2.jpeg');

          let utcDate = '2020-09-15T12:24:28.85Z';
          let convertedDate = DateFormatWithMoment(utcDate, true);
          expect(convertedDate).toBe('Sep 15, 2020 05:24 PM')
          
          const allDocAddedDate = getAllByTestId('added-date')
          expect(allDocAddedDate[0]).toHaveTextContent(convertedDate);
        })

    test('should render documents type alphabatically order', async () => {
      const { getAllByTestId } = render(
          <MemoryRouter >
            <App/>
          </MemoryRouter>
      );

      await waitForDomChange();

      const allDocTypes = getAllByTestId('doc-type');
      expect(allDocTypes[0]).toHaveTextContent('Bank Statements - Two Months');
      expect(allDocTypes[1]).toHaveTextContent('Rental Agreement');
      expect(allDocTypes[2]).toHaveTextContent('Tax Returns with Schedules (Business - Two Years)');
      expect(allDocTypes[3]).toHaveTextContent('W-2s - Last Two years');
    });

    test('should render most recents file added date on top', async ()=> {
      const { getAllByTestId } = render(
        <MemoryRouter >
          <App/>
        </MemoryRouter>
    );
    
    await waitForDomChange();

    let utcDateOnTop = '2020-09-15T12:24:28.85Z';
    let utcDateOnSecond = '2020-09-15T12:23:26.161Z';
    let utcDateOnThird = '2020-09-15T12:09:04.528Z';

    let convertedDateOnTop = DateFormatWithMoment(utcDateOnTop, true);
    let convertedDateOnSecond = DateFormatWithMoment(utcDateOnSecond, true);
    let convertedDateOnThird = DateFormatWithMoment(utcDateOnThird, true);

    expect(convertedDateOnTop).toBe('Sep 15, 2020 05:24 PM')
    expect(convertedDateOnSecond).toBe('Sep 15, 2020 05:23 PM')
    expect(convertedDateOnThird).toBe('Sep 15, 2020 05:09 PM')

    });

    test('should open file viewer onClick', async () => {
      const { getByText, getAllByTestId } = render(
        <MemoryRouter >
          <App/>
        </MemoryRouter>
    );
    await waitForDomChange();
    
    const allDocFiles = getAllByTestId('doc-file-link');
    fireEvent.click(allDocFiles[0]);
   
    const fileViewer = getByText((content, element) => element.className === "document-view--header")
    expect(fileViewer).toBeDefined();
    

    });
});
