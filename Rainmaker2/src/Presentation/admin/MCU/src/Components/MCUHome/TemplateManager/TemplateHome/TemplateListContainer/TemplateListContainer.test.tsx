import React from 'react';
import { findByTestId, fireEvent, render, waitFor } from '@testing-library/react';

import { MemoryRouter } from 'react-router-dom';
import { MockEnvConfig } from '../../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../../../../Store/Store';
import { TemplateManager } from '../../TemplateManager';

jest.mock('axios');
jest.mock('../../../../../Store/actions/UserActions');
jest.mock('../../../../../Store/actions/NeedListActions');
jest.mock('../../../../../Store/actions/TemplateActions');
jest.mock('../../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Template List Container', () => {

    test('Should render Template List Container', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );
        await waitFor(() => {
            expect(getByTestId('template-list-container')).toHaveTextContent('Add new template');
        });
    })



    test('Should show a new template on "Add New Template" click', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );

        await waitFor(() => {
            let newTempBtn = getByTestId('template-list-container');
            fireEvent.click(newTempBtn);
        });

        await waitFor(() => {
            let tempNameInput = getByTestId('new-template-input');
            expect(getByTestId('new-template-container')).toHaveTextContent('Add documents after template is created');
            expect(tempNameInput).toBeInTheDocument();
            expect(tempNameInput).toHaveFocus();
        })
    })

    test('Should add a new template on "Add New Template" click', async () => {
        const { getByTestId, findByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );
        let newTempBtn: any = null;
        await waitFor(() => {
            newTempBtn = getByTestId('add-new-template-btn');
        });

        fireEvent.click(newTempBtn);
        let tempNameInput: any = null;
        let newTempContainer = getByTestId('new-template-container');
        await waitFor(() => {
            tempNameInput = getByTestId('new-template-input');
        });
        await waitFor(() => {
            fireEvent.blur(tempNameInput);
        })
        let tempCreated;
        await waitFor(() => {
            tempCreated = getByTestId('new-template-container')
        })
        expect(tempCreated).toHaveTextContent('Your template is empty');

    });

    test('Should insert the clicked document into the current template', async () => {
        const { getByTestId, getAllByTestId, findByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );
        let newTempBtn: any = null;
        await waitFor(() => {
            newTempBtn = getByTestId('add-new-template-btn');
        });

        fireEvent.click(newTempBtn);
        let tempNameInput: any = null;
        let newTempContainer = getByTestId('new-template-container');
        await waitFor(() => {
            tempNameInput = getByTestId('new-template-input');
        });
        await waitFor(() => {
            fireEvent.blur(tempNameInput);
        })
        let tempCreated;
        await waitFor(() => {
            tempCreated = getByTestId('new-template-container')
        })
        expect(tempCreated).toHaveTextContent('Your template is empty');

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

        let tempDocs : any = [];
        await waitFor(() => {
            tempDocs = getAllByTestId('temp-doc');
            expect(tempDocs[0]).toHaveTextContent('Rental Agreement');
        });

    });
})

