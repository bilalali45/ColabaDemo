import React from 'react';
import { render, fireEvent, waitFor, getByTestId } from '@testing-library/react';
import userEvent from '@testing-library/user-event'
import { DocumentsHeader } from './DocumentsHeader';
import {MockEnvConfig} from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../../../Store/Store';
import { MemoryRouter } from 'react-router-dom';

// test('renders learn react link', () => {
//     const { getByText } = render(<DocumentsHeader />);
//     waitFor(() => {
//         const title = getByText('DOC MANAGER');
//         expect(title).toBeInTheDocument();
//     })
// });

jest.mock('pspdfkit');
jest.mock('../../../../Store/actions/DocumentActions');
jest.mock('../../../../Store/actions/TemplateActions');

const Url = '/DocManager/2515'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Doc Manager Header', () => {

    test('Should show Doc Manager Heading', async () => {
        const { getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        
            waitFor(() => {
                const title = getByText('Doc Manager');
                expect(title).toBeInTheDocument();
            })
    });

    test('Should show Search Text', async () => {
        const { getByText, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        
            waitFor(() => {
                const searchText = getByText('Search');
                expect(searchText).toBeInTheDocument();

                const searchButtonIcon = getByTestId("searchIcon")
                expect(searchButtonIcon).toBeInTheDocument();
            })
    });

    test('Should show Search bar ', async () => {
        const { getByText, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        let searchText:any;
            waitFor(() => {
                searchText = getByText('Search');
                expect(searchText).toBeInTheDocument();
            })
                fireEvent.click(searchText)
                await waitFor(() => {
                    let searchBar = getByTestId("search-bar")
                expect(searchBar).toBeInTheDocument();
                fireEvent.change(searchBar, { target: { value: "abcd" } });
                // expect(getByText(" No Results Found for ")).toBeInTheDocument();
            })
    });

    test('Should clear Search text ', async () => {
        const { getByText, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        let searchText:any;
            waitFor(() => {
                searchText = getByText('Search');
                expect(searchText).toBeInTheDocument();
            })
                fireEvent.click(searchText)
                let searchBar
                await waitFor(() => {
                    searchBar = getByTestId("search-bar")
                fireEvent.change(searchBar, { target: { value: "abcd" } });
                })
                let clearSearchBtn = getByTestId("clear-search")
                fireEvent.click(clearSearchBtn)
                await waitFor(() => {
                expect(searchBar).not.toBeInTheDocument()
            })
    });

    
    

    test('Should show Trash Bin Text', async () => {
        const { getByText, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        
            waitFor(() => {
                const trashBinText = getByText('Trash Bin');
                expect(trashBinText).toBeInTheDocument();
                fireEvent.click(trashBinText)
                const searchButtonIcon = getByTestId("trashIcon")
                expect(searchButtonIcon).toBeInTheDocument();

            })
    });

    test('Should show Trash Bin Overlay', async () => {
        const { getByText, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        
        let trashBinText :any;
        waitFor(() => {
            trashBinText = getByText('Trash Bin');
            expect(trashBinText).toBeInTheDocument();
            fireEvent.click(trashBinText)
        })
        let TrashBinHeader:any;
        await waitFor(() => {
            TrashBinHeader  = getByTestId('trashHeader');
            expect(TrashBinHeader).toHaveTextContent("Document")
        })




    });

    test('Should show Documents in Trash Bin Overlay', async () => {
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        
        let trashBinText :any;
        waitFor(() => {
            trashBinText = getByText('Trash Bin');
            expect(trashBinText).toBeInTheDocument();
            fireEvent.click(trashBinText)
        })
        let trashBinDocs:any;
        await waitFor(() => {
            trashBinDocs  = getAllByTestId('trashDoc');
            expect(trashBinDocs).toHaveLength(3)
            expect(trashBinDocs[0]).toHaveTextContent("Portrait-family-1-600-xxxq87.png")
            expect(trashBinDocs[0]).toHaveTextContent("Uploaded By: Ali Momin on Jan 5, 2021 09:26 AM")
            
        })
        userEvent.hover(trashBinDocs[0])
        expect(getAllByTestId("putBackIcon")[0]).toBeInTheDocument()
    });

    
    test('Should click restore btn', async () => {
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        
        let trashBinText :any;
        waitFor(() => {
            trashBinText = getByText('Trash Bin');
            expect(trashBinText).toBeInTheDocument();
            fireEvent.click(trashBinText)
        })
        let trashBinDocs:any;
        await waitFor(() => {
            trashBinDocs  = getAllByTestId('trashDoc');
            
        })
        userEvent.hover(trashBinDocs[0])
        let restoreBtn = getAllByTestId("putBackIcon")[0]
        expect(restoreBtn).toBeInTheDocument()
        fireEvent.click(restoreBtn)
        await waitFor(() => {
           
            
        })
    });

    

    test('Should show Add Document Text', async () => {
        const { getByText, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        
            waitFor(() => {
                const addDocumentText = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

                const searchButtonIcon = getByTestId("addDocumentIcon")
                expect(searchButtonIcon).toBeInTheDocument();
            })
    });


    test('Should drag file from trash bin', async () => {
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        let trashBinText :any;
        waitFor(() => {
            trashBinText = getByText('Trash Bin');
            expect(trashBinText).toBeInTheDocument();
            fireEvent.click(trashBinText)
        })
        let trashBinDocs:any;
        await waitFor(() => {
            trashBinDocs  = getAllByTestId('trashDoc');
            const mockdt = { setData: jest.fn() };
            fireEvent.dragStart(trashBinDocs[0].children[1], { dataTransfer: mockdt});
            expect(mockdt.setData).toBeCalled();
        })
    });

    test('Should drop file to trash bin', async () => {
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        let trashBinDrop :any;
        waitFor(() => {
            trashBinDrop = getByTestId('drop-to-trashbin');
            expect(trashBinDrop).toBeInTheDocument();
            const mockdt = { getData: jest.fn() };
            fireEvent.drop(trashBinDrop);
            // expect(mockdt.getData).toBeCalled();
        })
    });
})
