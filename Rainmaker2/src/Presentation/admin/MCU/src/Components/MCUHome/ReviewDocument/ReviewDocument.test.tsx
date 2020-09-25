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
      test('Should render Document Details text when clictk on Details button', async ()=> {
        const {getByTestId, getByText, getAllByTestId } = render(
            <MemoryRouter initialEntries={[Url]} >
               <App/>
            </MemoryRouter>
           );

           await waitForDomChange()

          const detailBtns: any = getAllByTestId('needList-detailBtnts');
          console.log('------detailBtns-----------',detailBtns)        
          fireEvent.click(detailBtns[0]);

          await waitFor(() => {
            let reviewHeader = getByTestId('review-headerts');
            expect(reviewHeader).toHaveTextContent('Document Details');
          })

      });
  });