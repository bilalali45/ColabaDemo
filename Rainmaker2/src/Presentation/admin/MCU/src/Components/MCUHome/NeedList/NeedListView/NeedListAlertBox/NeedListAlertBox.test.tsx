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

test('should render file not sync Alert', async () => {
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

    const syncAlertHeader = getByTestId("sync-alert-Header")
    expect(syncAlertHeader.children[0].children[0]).toHaveAttribute('alt', 'ErrorIcon')
      expect(syncAlertHeader.children[1]).toHaveTextContent('Document Synchronization Failed')
  });

