import React from 'react';
import { render, fireEvent, waitFor,act } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../../../../../Store/Store';
import { MockEnvConfig } from '../../../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../../../test_utilities/LocalStoreMock';
import { DocumentsTable } from '../DocumentsTable';


jest.mock('pspdfkit');
jest.mock('../../../../../../Store/actions/DocumentActions');
jest.mock('../../../../../../Store/actions/TemplateActions');

const Url = '/DocManager/2515'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('File Items ', () => {

    test('Should show File Name', async () => {
        const { getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        
        await waitFor(() => {
                const fileItems = getAllByTestId("file-item-div");
                // expect(fileItems).toBeInTheDocument();

                expect(fileItems[0]).toHaveTextContent("images 1.jpeg")
            })
    });


    test('Should show trashbin icon', async () => {
        const { getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let fileTrashbin:any;
        await waitFor(() => {
            fileTrashbin = getAllByTestId("file-trash-icon");
                // expect(fileItems).toBeInTheDocument();

                expect(fileTrashbin[0]).toBeInTheDocument()
                
            })
            fireEvent.click(fileTrashbin[0])
    });

    test('Should show reassign icon', async () => {
        const { getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let fileReassignIcon:any;
        await waitFor(() => {
            fileReassignIcon = getAllByTestId("file-reassign-icon");
                // expect(fileItems).toBeInTheDocument();

                expect(fileReassignIcon[0]).toBeInTheDocument()
                
            })
            fireEvent.click(fileReassignIcon[0])
    });

    test('Should show reassign dropdown heading if reassign icon clicked', async () => {
        const { getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable />
                </MemoryRouter>
            </StoreProvider>
        );
        let fileReassignIcon:any;
        await waitFor(() => {
            fileReassignIcon = getAllByTestId("file-reassign-icon");
                // expect(fileItems).toBeInTheDocument();

                expect(fileReassignIcon[0]).toBeInTheDocument()
                
            })
            fireEvent.click(fileReassignIcon[0])
        let reassignDropdown :any;
        await waitFor(() => {
            reassignDropdown = getByTestId('reassign-dropdown');
            expect(reassignDropdown).toBeInTheDocument();
            expect(reassignDropdown).toHaveTextContent("Select Document Type")

            // 
        });
        let docCategories = getAllByTestId("reassign-cat-doc")
        expect(docCategories[0]).toBeInTheDocument()
    });

    test('Should show reassign dropdown categories if reassign icon clicked', async () => {
        const { getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable />
                </MemoryRouter>
            </StoreProvider>
        );
        let fileReassignIcon:any;
        await waitFor(() => {
            fileReassignIcon = getAllByTestId("file-reassign-icon");
                // expect(fileItems).toBeInTheDocument();

                expect(fileReassignIcon[0]).toBeInTheDocument()
                
            })
            fireEvent.click(fileReassignIcon[0])
        let reassignDropdown :any;
        await waitFor(() => {
            reassignDropdown = getByTestId('reassign-dropdown');
            expect(reassignDropdown).toBeInTheDocument();
            

            // 
        });
        let docCategories = getAllByTestId("reassign-cat-doc")
        expect(docCategories[0]).toHaveTextContent("Bank Statements - Two Months")
        expect(docCategories[2]).toHaveTextContent("W-2s - Last Two years")
    });

    test('Should show hide reassign pop up if clicked on any category', async () => {
        const { getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable />
                </MemoryRouter>
            </StoreProvider>
        );
        let fileReassignIcon:any;
        await waitFor(() => {
            fileReassignIcon = getAllByTestId("file-reassign-icon");
                // expect(fileItems).toBeInTheDocument();

                expect(fileReassignIcon[0]).toBeInTheDocument()
                
            })
            fireEvent.click(fileReassignIcon[0])
        let reassignDropdown :any;
        await waitFor(() => {
            reassignDropdown = getByTestId('reassign-dropdown');
            expect(reassignDropdown).toBeInTheDocument();
            

            // 
        });
        let docCategories = getAllByTestId("reassign-cat-doc")
        expect(docCategories[0]).toHaveTextContent("Bank Statements - Two Months")
        fireEvent.click(docCategories[0])

        await waitFor(() => {
            expect(reassignDropdown).not.toBeInTheDocument();
        })
    });


    test('Should drag file from trash bin', async () => {
        const { getByText, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let trashBinText :any;
         waitFor(() => {
            trashBinText = getByText('Trash Bin');
            expect(trashBinText).toBeInTheDocument();
            fireEvent.click(trashBinText)
        })
        let fileItem:any;       
        
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"60090d9b122436829c4ad3c6","fromDocId":"60090d9b122436829c4ad3c7","fromFileId":"60001b9932088251cb1c7061","fileName":"images 2.jpeg","isFromThumbnail":false,"isFromWorkbench":false,"isFromCategory":true}
            fileItem = getAllByTestId('file-item');
            const mockdt = { setData: () =>  file };
            // fireEvent.dragStart(trashBinDocs[0].children[1], { dataTransfer: mockdt});
            act(() => {
                fireEvent.dragStart(fileItem[0].children[1], {
                    dataTransfer: mockdt});
            });
            // expect(mockdt.setData).toBeCalled();
        
    });

    test('Should show file ', async () => {
        const { getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        await waitFor(() => {
            const fileItems = getAllByTestId("file-item-div");
            // expect(fileItems).toBeInTheDocument()
            fireEvent.click(fileItems[0])
        })
    });

})
