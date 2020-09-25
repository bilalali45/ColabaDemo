import React from 'react';
import {render, waitForDomChange, fireEvent, waitFor, waitForElement, getByTestId} from '@testing-library/react';
import App from '../../../App';
import {MemoryRouter} from 'react-router-dom';
import {MockEnvConfig} from '../../../test_utilities/EnvConfigMock';
import {MockLocalStorage} from '../../../test_utilities/LocalStoreMock';
import {createMemoryHistory} from 'history';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NeedListActions');
jest.mock('../../../Store/actions/ReviewDocumentActions');
jest.mock('../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    const history = createMemoryHistory();
    history.push('/');
  });

  describe('REVIEW DOCUMENT', () => {
      test('Should render Document Details text and close button when clictk on Details button of list item.', async ()=> {
        const {getByTestId, getByText, getAllByTestId } = render(
            <MemoryRouter initialEntries={[Url]} >
               <App/>
            </MemoryRouter>
           );

           await waitForDomChange()

          const detailBtns: any = getAllByTestId('needList-detailBtnts');     
          fireEvent.click(detailBtns[0]);

          await waitFor(() => {
            let reviewHeader = getByTestId('review-headerts');
            let reviewHeaderCloseBtn = getByTestId('review-closeBtnTs');
            expect(reviewHeader).toHaveTextContent('Document Detail');
            expect(reviewHeaderCloseBtn).toBeInTheDocument();
          })
      });
      
      test('Should render file viewer with viewer header detail', async () => {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange();

         const detailBtns: any = getAllByTestId('needList-detailBtnts');     
         fireEvent.click(detailBtns[0]);

        await waitFor(() => {
          const fileViewer = getByText((content, element) => element.className === "document-view--header")
          expect(fileViewer).toBeDefined();
        })    
      })

      test('Should render file viewer with zoomIn, zoomOut and fitToScreen', async () => {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange();

         const detailBtns: any = getAllByTestId('needList-detailBtnts');     
         fireEvent.click(detailBtns[0]);

        await waitFor(() => {
          const fileViewer = getByText((content, element) => element.className === "document-view--floating-options")
          expect(fileViewer).toBeDefined();
        });    
      });

      test('Should render Activity log section', async () => {
        const {getByTestId, getByText, getAllByTestId } = render(
          <MemoryRouter initialEntries={[Url]} >
             <App/>
          </MemoryRouter>
         );

         await waitForDomChange();

         const detailBtns: any = getAllByTestId('needList-detailBtnts');     
         fireEvent.click(detailBtns[0]);

        await waitFor(() => {
          const fileViewer = getByText((content, element) => element.className === "document-statement--header")
          expect(fileViewer).toBeDefined();
        });    
      });
  });