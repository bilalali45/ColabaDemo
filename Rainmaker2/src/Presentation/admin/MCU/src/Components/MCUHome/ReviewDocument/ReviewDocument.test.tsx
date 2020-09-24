import React from 'react';
import {render, waitForDomChange, fireEvent} from '@testing-library/react';
import App from '../../../App';
import {MemoryRouter} from 'react-router-dom';
import {MockEnvConfig} from '../../../test_utilities/EnvConfigMock';
import {MockLocalStorage} from '../../../test_utilities/LocalStoreMock';
import {createMemoryHistory} from 'history';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NeedListActions');
jest.mock('../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
  });

  describe('REVIEW DOCUMENT', () => {
      test('Should render Document Details text when clictk on Details button', async ()=> {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={[Url]} >
               <App/>
            </MemoryRouter>
           );
     
        await waitForDomChange();

        //  const allStatus = getAllByTestId('needList-statusts');
        //  const allFiles = getAllByTestId('needList-filets');
        //  expect(allStatus[0]).toHaveTextContent('Pending Review');
        //  expect(allStatus[0]).not.toHaveTextContent('No file submitted yet');

        const allDetailsBtn = getAllByTestId('needList-detailBtnts');
        fireEvent.click(allDetailsBtn[0]);
           
        
         const reviewHeader = getByTestId('testId');
         expect(reviewHeader).toHaveTextContent('No file submitted yet');

      });
  });