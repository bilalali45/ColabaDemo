import React from 'react';
import { render, fireEvent, waitFor } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../../../../Store/Store';
import { MockEnvConfig } from '../../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../../test_utilities/LocalStoreMock';
import { DocumentsTable } from './DocumentsTable';


jest.mock('pspdfkit');
jest.mock('../../../../../Store/actions/DocumentActions');
jest.mock('../../../../../Store/actions/TemplateActions');

const Url = '/DocManager/2515'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Doc Table Section ', () => {

    test('Should show Doc heading ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        
            await waitFor(() => {
                const docHeading = getByTestId("doc-heading");
                expect(docHeading).toBeInTheDocument();

                expect(docHeading).toHaveTextContent("Document")
            })
    });
    test('Should show Status heading ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        
            await waitFor(() => {
                const statusHeading = getByTestId("doc-status-heading");
                expect(statusHeading).toBeInTheDocument();

                expect(statusHeading).toHaveTextContent("Status")
            })
    });

    test('Should show add file btn', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        
            await waitFor(() => {
                const addFileBtn = getByTestId("add-file-btn");
                expect(addFileBtn).toBeInTheDocument();

                expect(addFileBtn).toHaveTextContent("Add Files +")
            })
    });

    test('Should show all docs names', async () => {
        const { getByTestId,getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let addFileBtn:any;
            await waitFor(() => {
                addFileBtn = getByTestId("add-file-btn");
                expect(addFileBtn).toBeInTheDocument();

                expect(addFileBtn).toHaveTextContent("Add Files +")
            })

            fireEvent.click(addFileBtn);
            await waitFor(() => {
                let addFileHeader = getByTestId("add-file-header")
                expect(addFileHeader).toHaveTextContent("Select Document Type")
            })

            await waitFor(()=>{
                let docsNames = getAllByTestId("doc-item-names")
                expect(docsNames[0]).toBeInTheDocument()
                fireEvent.mouseDown(docsNames[0])
            })
    });


   

    test('Should show Add Document Overlay', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );

        let addFileBtn:any;
            await waitFor(() => {
                addFileBtn = getByTestId("add-file-btn");
                expect(addFileBtn).toBeInTheDocument();

                expect(addFileBtn).toHaveTextContent("Add Files +")
            })

            fireEvent.click(addFileBtn);
            await waitFor(() => {
                let addFileHeader = getByTestId("add-file-header")
                expect(addFileHeader).toHaveTextContent("Select Document Type")
            })


        let addDocumentText:any;
        await waitFor(() => {
                addDocumentText  = getByTestId('doc-add-doc-btn');
                expect(addDocumentText).toBeInTheDocument();
                expect(addDocumentText).toHaveTextContent("Add Document")
               
            })
            
            

            // fireEvent.click(addDocumentText);
            // let docPopOver;
            // await waitFor(() => {
            //     docPopOver = getByTestId('popup-add-doc');
            //     expect(docPopOver).toBeInTheDocument();
            // });
    
            // let docCats = getAllByTestId('doc-cat');
    
            // fireEvent.click(docCats[2]);
    
            // const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');
    
            // expect(selectedCatDocsContainer).toHaveTextContent('Liabilities');
    
            // const itemsToClick = getAllByTestId('doc-item');
    
            // expect(itemsToClick[2]).toHaveTextContent('Rental Agreement');
    
            // fireEvent.click(itemsToClick[2]);
    
            // fireEvent.click(document.body);

    });

})
