import React from 'react';
import { render, waitForElementToBeRemoved, fireEvent, waitFor, getByTestId, screen, waitForDomChange, waitForElement } from '@testing-library/react';

import { MemoryRouter } from 'react-router-dom';
import { MockLocalStorage } from '../../../../../test_utilities/LocalStoreMock';
import { NewNeedList } from '../NewNeedList';
import { StoreProvider } from '../../../../../Store/Store';
import App from '../../../../../App';
import { TemplateActions as TemplateActionsOriginal } from '../../../../../Store/actions/TemplateActions';
import { mockIsDocumentDraft, TemplateActions } from '../../../../../Store/actions/__mocks__/TemplateActions';


jest.mock('../../../../../Store/actions/UserActions');
jest.mock('../../../../../Store/actions/NewNeedListActions');
jest.mock('../../../../../Store/actions/NeedListActions');
jest.mock('../../../../../Store/actions/TemplateActions');
jest.mock('../../../../../Utils/LocalDB');

beforeEach(() => {
    MockLocalStorage();
});

describe('NewNeedListHeader', () => {
    test('Should render NewNeedListHeader', async () => {
        const { getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            let closeBtn = getByText('Close');
            let saveNCloseBtn = getByText('Save & Close');

            expect(closeBtn).toBeInTheDocument();
            expect(saveNCloseBtn).toBeInTheDocument();
        });
    });

    test('Should render confirmation popup', async () => {
        const { getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );
        let closeBtn: any;
        await waitFor(() => {
            closeBtn = getByText('Close');
        });
        fireEvent.click(closeBtn);
        await waitFor(() => {
            expect(document.body).toHaveTextContent('Are you sure you want to close this');
            expect(document.body).toHaveTextContent('request without saving?');
            expect(document.body).toHaveTextContent('Close');
            expect(document.body).toHaveTextContent('Save & Close');
        })
    });



    test('Should enable Save & Close button when at least one document', async () => {

        const { getByText, getByTestId, getAllByTestId } = render(
            <MemoryRouter>
                <App />
            </MemoryRouter>
        );

        let selectTempBtn: any;
        await waitFor(() => {
            selectTempBtn = getByText('Add');
        });

        fireEvent.click(selectTempBtn);

        let startFromNew = getByText('Start from new list');

        fireEvent.click(startFromNew);

        let saveNCloseBtn: any;
        await waitFor(() => {
            saveNCloseBtn = getByText('Save & Close');
        });

        const addDocBtn = getByTestId('add-doc-btn');

        fireEvent.click(addDocBtn);
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

        fireEvent.click(document.body);

        await waitFor(() => {
            expect(saveNCloseBtn).toBeEnabled();
        });
        

    });
    
    test('Should redirect to NeedList and Show Review Saved Draft on Save & Close button click', async () => {

        const { getByText, getByTestId, getAllByTestId } = render(
            <MemoryRouter>
                <App />
            </MemoryRouter>
        );

        let selectTempBtn: any;
        await waitFor(() => {
            selectTempBtn = getByText('Add');
        });

        fireEvent.click(selectTempBtn);

        let startFromNew = getByText('Start from new list');

        fireEvent.click(startFromNew);

        let saveNCloseBtn: any;
        await waitFor(() => {
            saveNCloseBtn = getByText('Save & Close');
        });

        const addDocBtn = getByTestId('add-doc-btn');

        fireEvent.click(addDocBtn);
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

        fireEvent.click(document.body);

        await waitFor(() => {
            expect(saveNCloseBtn).toBeEnabled();
        });
        console.log('mock =======================================', mockIsDocumentDraft);
        fireEvent.click(saveNCloseBtn);
        let cachedIsDocDraft = TemplateActionsOriginal.isDocumentDraft;
        TemplateActionsOriginal.isDocumentDraft = async () => {
            console.log('in here reached');
            return Promise.resolve({ requestId: "5f84049c49fa941f146f93f0" })
        };

        await waitFor(() => {
            expect(location.pathname).toBe('/DocumentManagement/needList/3');
            console.log('mock =======================================', mockIsDocumentDraft);
            expect(getByTestId('need-list-view')).toBeInTheDocument();
        });

        await waitFor(() => {
            expect(getByText('View Saved Draft')).toBeInTheDocument();
        });

        TemplateActionsOriginal.isDocumentDraft = cachedIsDocDraft;

    });

    test('Should close confirmation popup and redirect to NeedListTable', async () => {
        const { getByText, getByTestId } = render(
            <MemoryRouter>
                <App />
            </MemoryRouter>
        );

        let selectTempBtn: any;
        await waitFor(() => {
            selectTempBtn = getByText('Add');
        });

        fireEvent.click(selectTempBtn);

        let startFromNew = getByText('Start from new list');

        fireEvent.click(startFromNew);

        let closeBtn: any;
        await waitFor(() => {
            closeBtn = getByText('Close');
        });


        fireEvent.click(closeBtn);
        let popupCloseBtn: any;
        await waitFor(() => {
            popupCloseBtn = getByTestId('needlist-close-popup-button');
        });
        fireEvent.click(popupCloseBtn);

        await waitFor(() => {
            expect(location.pathname).toBe('/DocumentManagement/needList/3');
        })

    });
})


