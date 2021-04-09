import React from 'react';
import { render, fireEvent, waitFor } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { MockEnvConfig } from '../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../../Store/Store';
import { Home } from '../Home';
import DocumentActions from '../../../Store/actions/DocumentActions';
// import DocumentActions from '../../../Store/actions/__mocks__/DocumentActions';


jest.mock('pspdfkit');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/DocumentActions');
jest.mock('../../../Store/actions/TemplateActions');
jest.mock('../../../Utilities/AnnotationActions');

const Url = '/DocManager/2515'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('LOSSyncAlert', () => {

    test('Should show render sync alert popup', async () => {
        const { getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        
        let fileSyncIcon: any;
        await waitFor(() => {
            fileSyncIcon = getAllByTestId("file-sync-icon");

            expect(fileSyncIcon[0]).toBeInTheDocument();

            fireEvent.click(fileSyncIcon[0]);
        });

        await waitFor(() => {
            let losSyncAlert = getByTestId('los-sync-alert');
            expect(losSyncAlert).toBeInTheDocument();
            expect(losSyncAlert).toHaveTextContent('Are you ready to sync the selected documents?');
        });
    });

    test('Should show start sync process', async () => {
        const { getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        let fileSyncIcon: any;
        await waitFor(() => {
            fileSyncIcon = getAllByTestId("file-sync-icon");

            expect(fileSyncIcon[0]).toBeInTheDocument();

            fireEvent.click(fileSyncIcon[0]);
        });
        let syncBtn;
        await waitFor(() => {
            syncBtn = getByTestId('sync-button');
            expect(syncBtn).toBeInTheDocument();

            fireEvent.click(syncBtn);
        })

        await waitFor(() => {
            let losSyncAlert = getByTestId('los-sync-alert');
            expect(losSyncAlert).toBeInTheDocument();
            expect(losSyncAlert).toHaveTextContent('Synchronization completed');
        });
    });

    test('Should render sync failed alert', async () => {

        DocumentActions.syncFileToLos = jest.fn(() => Promise.reject(false));

        const { getAllByTestId, getByTestId   } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        let fileSyncIcon: any;
        await waitFor(() => {
            fileSyncIcon = getAllByTestId("file-sync-icon");

            expect(fileSyncIcon[0]).toBeInTheDocument();

            fireEvent.click(fileSyncIcon[0]);
        });
        let syncBtn;
        await waitFor(() => {
            syncBtn = getByTestId('sync-button');
            expect(syncBtn).toBeInTheDocument();

            fireEvent.click(syncBtn);
        })

        await waitFor(() => {
            let losSyncAlert = getByTestId('sync-failed-header');
            expect(losSyncAlert).toBeInTheDocument();
        });
    });
    test('Should retry sync and hide popup', async () => {

        DocumentActions.syncFileToLos = jest.fn(() => Promise.reject(false));

        const { getAllByTestId, getByTestId, getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        let fileSyncIcon: any;
        await waitFor(() => {
            fileSyncIcon = getAllByTestId("file-sync-icon");

            expect(fileSyncIcon[0]).toBeInTheDocument();

            fireEvent.click(fileSyncIcon[0]);
        });
        let syncBtn;
        await waitFor(() => {
            syncBtn = getByTestId('sync-button');
            expect(syncBtn).toBeInTheDocument();

            fireEvent.click(syncBtn);
        })
        let losSyncAlert;
        await waitFor(() => {
            losSyncAlert = getByTestId('sync-failed-header');
            expect(losSyncAlert).toBeInTheDocument();

        });

        await waitFor(() => {
            let retrySyncBtn = getByText('Sync again');
            fireEvent.click(retrySyncBtn);
            expect(losSyncAlert).not.toBeInTheDocument();
        })
    });

})
