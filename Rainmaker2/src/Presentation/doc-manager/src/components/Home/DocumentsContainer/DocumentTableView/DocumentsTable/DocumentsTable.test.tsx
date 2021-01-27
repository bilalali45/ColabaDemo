import React from 'react';
import { render, fireEvent, waitFor, getByTestId, getAllByTestId } from '@testing-library/react';
import userEvent from '@testing-library/user-event'
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../../../../Store/Store';
import { DocumentsHeader } from '../../DocumentsHeader/DocumentsHeader';
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
                let noItemText = getByTestId("no-item-list")
                expect(noItemText).toBeInTheDocument()
            })
            // await waitFor(()=>{
            //     let addFileDocList = getAllByTestId("add-file-doc-list")
            //     expect(addFileDocList[0]).toHaveTextContent("")
            // })
    });


    test('Should show Docs Name', async () => {
        const { getAllByTestId, getByTestId, getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        
        await waitFor(() => {
                // const docItems = getByTestId("document-item");
                // expect(docItems).toBeInTheDocument();

                // expect(docItems[0]).toHaveTextContent("Bank Statements - Two Months")
            })
    });


    test('Should show Add Document Overlay', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
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
                addDocumentText  = getByTestId('add-file-add-doc-btn');
                expect(addDocumentText).toBeInTheDocument();

               
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
