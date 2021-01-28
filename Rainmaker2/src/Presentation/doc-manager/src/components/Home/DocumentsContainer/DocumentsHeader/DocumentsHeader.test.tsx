import React from 'react';
import { render, fireEvent, waitFor, getByTestId, act } from '@testing-library/react';
import userEvent from '@testing-library/user-event'
import { DocumentsHeader } from './DocumentsHeader';
import {MockEnvConfig} from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../../../Store/Store';
import { MemoryRouter } from 'react-router-dom';
import { Home } from '../../Home';
import { DocumentsContainer } from '../DocumentsContainer';


jest.mock('pspdfkit');
jest.mock('../../../../Store/actions/DocumentActions');
jest.mock('../../../../Store/actions/TemplateActions');
jest.mock('../../../../Store/actions/UserActions');

const Url = '/DocManager/2515'





beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    JSON.parse = jest.fn((data:any)=> data)
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
        
            await waitFor(() => {
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
        
        await waitFor(() => {
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
        await waitFor(() => {
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
        await waitFor(() => {
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
        
        await waitFor(() => {
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
        await waitFor(() => {
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
        await waitFor(() => {
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
        await waitFor(() => {
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
        
        await waitFor(() => {
                const addDocumentText = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

                const searchButtonIcon = getByTestId("addDocumentIcon")
                expect(searchButtonIcon).toBeInTheDocument();
            })
    });

    test('Should show Add Document Overlay', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        let addDocumentText:any;
        await waitFor(() => {
                addDocumentText  = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

               
            })
            
            

            fireEvent.click(addDocumentText);
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

    });

    test('Should show add custom document header', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        let addDocumentText:any;
        await waitFor(() => {
                addDocumentText  = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

               
            })
             fireEvent.click(addDocumentText);
            let docPopOver;
            await waitFor(() => {
                docPopOver = getByTestId('popup-add-doc');
                expect(docPopOver).toBeInTheDocument();
            });
    
            let docCats = getAllByTestId('doc-cat');
    
            fireEvent.click(docCats[5]);
    
            const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');
    
            expect(selectedCatDocsContainer).toHaveTextContent('Other');
    
            const customeDocHeader = getByTestId('custom-doc-header');
    
            expect(customeDocHeader).toHaveTextContent('Add Custom Document');
    
    });


    test('Should add file with document ', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsContainer/>
                </MemoryRouter>
            </StoreProvider>
        );
        let addDocumentText:any;
        await waitFor(() => {
                addDocumentText  = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

               
            })
            
            

            fireEvent.click(addDocumentText);
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

            await waitFor(() => {
                const fileInput = getByTestId("file-input")
    
                const file = new File(['hello'], 'hello.png', {type: 'image/png'})
    
                userEvent.upload(fileInput, file)
                })
    });

    test('Should not add wrong extension file with document ', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home/>
                </MemoryRouter>
            </StoreProvider>
        );
        let addDocumentText:any;
        await waitFor(() => {
                addDocumentText  = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

               
            })
            
            

            fireEvent.click(addDocumentText);
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

            await waitFor(() => {
                const fileInput = getByTestId("file-input")
    
                const file = new File(['dummy'], 'test.cert', { type: 'cert' })
    
                fireEvent.change(fileInput, { target: { files: [file] } });
                })
    });

    test('Should add custom document via Enter key ', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        let addDocumentText:any;
        await waitFor(() => {
                addDocumentText  = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

               
            })
             fireEvent.click(addDocumentText);
            let docPopOver;
            await waitFor(() => {
                docPopOver = getByTestId('popup-add-doc');
                expect(docPopOver).toBeInTheDocument();
            });
    
            let docCats = getAllByTestId('doc-cat');
    
            fireEvent.click(docCats[5]);
    
            const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');
    
            expect(selectedCatDocsContainer).toHaveTextContent('Other');
    
            const customeDocHeader = getByTestId('custom-doc-name');
    
            fireEvent.change(customeDocHeader, { target: { value: "my template" } });

            fireEvent.keyDown(customeDocHeader, { key: 'Enter', code: 13 })

    });


    

    test('Should check if doc name length is greater than 50 ', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        let addDocumentText:any;
        await waitFor(() => {
                addDocumentText  = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

               
            })
             fireEvent.click(addDocumentText);
            let docPopOver;
            await waitFor(() => {
                docPopOver = getByTestId('popup-add-doc');
                expect(docPopOver).toBeInTheDocument();
            });
    
            let docCats = getAllByTestId('doc-cat');
    
            fireEvent.click(docCats[5]);
    
            const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');
    
            expect(selectedCatDocsContainer).toHaveTextContent('Other');
    
            const customeDocHeader = getByTestId('custom-doc-name');
    
            fireEvent.change(customeDocHeader, { target: { value: "qwertyuiopasdfghjklzxcvbnqwertyuiopasdfghjklzxcvbnm" } });

            fireEvent.keyDown(customeDocHeader, { key: 'Enter', code: 13 })

    });


    

    test('Should show empty doc name validation ', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );
        let addDocumentText:any;
        await waitFor(() => {
                addDocumentText  = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

               
            })
             fireEvent.click(addDocumentText);
            let docPopOver;
            await waitFor(() => {
                docPopOver = getByTestId('popup-add-doc');
                expect(docPopOver).toBeInTheDocument();
            });
    
            let docCats = getAllByTestId('doc-cat');
    
            fireEvent.click(docCats[5]);
    
            const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');
    
            expect(selectedCatDocsContainer).toHaveTextContent('Other');
    
            const customeDocHeader = getByTestId('custom-doc-name');
    
            fireEvent.change(customeDocHeader, { target: { value: "" } });

            const addCustomDocBtn = getByTestId("add-custom-doc")

            fireEvent.click(addCustomDocBtn)

            const nameError = getByTestId("doc-name-error")
            expect(nameError).toBeInTheDocument()
            expect(nameError).toHaveTextContent("Document name cannot be empty")
    });




    test('Should search a document from search textbox', async () => {
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsHeader></DocumentsHeader>
                </MemoryRouter>
            </StoreProvider>
        );

        let addDocumentText:any;
        await waitFor(() => {
                addDocumentText  = getByText('Add Document');
                expect(addDocumentText).toBeInTheDocument();

               
            })
            
            

            fireEvent.click(addDocumentText);


            let docPopOver;
            await waitFor(() => {
                docPopOver = getByTestId('popup-add-doc');
                expect(docPopOver).toBeInTheDocument();
            });

            expect(getByText("All")).toBeInTheDocument();

            const searchTextBox = getByTestId("search-doc-name")
            expect(searchTextBox).toBeInTheDocument();

            fireEvent.change(searchTextBox, { target: { value: "Rental Agreement" } });

            expect(getByText("Search Result")).toBeInTheDocument();

            const docs = getAllByTestId("doc-item");
            expect(docs[0]).toHaveTextContent("Rental Agreement");

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
        
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"000000000000000000000000","fromDocId":"000000000000000000000000","fromFileId":"60068fc832088251cb1c70f8","isFromTrash":true}
            trashBinDocs  = getAllByTestId('trashDoc');
            const mockdt = { setData: () =>  file };
            // fireEvent.dragStart(trashBinDocs[0].children[1], { dataTransfer: mockdt});
            act(() => {
                fireEvent.dragStart(trashBinDocs[0].children[1], {
                    dataTransfer: mockdt});
            });
            // expect(mockdt.setData).toBeCalled();
        
    });

    test('Should drop file to trash bin from workbench', async () => {     
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home></Home>
                </MemoryRouter>
            </StoreProvider>
        );
        let trashBinDrop :any;
        await waitFor(() => {
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"000000000000000000000000","fromDocId":"000000000000000000000000","fromFileId":"60091286122436829c4ad3cc","fileName":"images (2).jpg","isFromThumbnail":false,"isFromWorkbench":true,"isFromCategory":false}
            trashBinDrop = getByTestId('drop-to-trashbin');
            expect(trashBinDrop).toBeInTheDocument();
            const mockdt = { getData: () =>  file};
            act(() => {
                fireEvent.drop(trashBinDrop, {
                    dataTransfer: mockdt});
            });
            // expect(mockdt.getData).toBeCalled();
        })
    });

    test('Should drop file to trash bin from category', async () => {     
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home></Home>
                </MemoryRouter>
            </StoreProvider>
        );
        let trashBinDrop :any;
        await waitFor(() => {
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"000000000000000000000000","fromDocId":"000000000000000000000000","fromFileId":"60091286122436829c4ad3cc","fileName":"images (2).jpg","isFromThumbnail":false,"isFromWorkbench":false,"isFromCategory":true}
            trashBinDrop = getByTestId('drop-to-trashbin');
            expect(trashBinDrop).toBeInTheDocument();
            const mockdt = { getData: () =>  file};
            act(() => {
                fireEvent.drop(trashBinDrop, {
                    dataTransfer: mockdt});
            });
            // expect(mockdt.getData).toBeCalled();
        })
    });

    test('Should drop file to trash bin from thumbnail', async () => {     
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home></Home>
                </MemoryRouter>
            </StoreProvider>
        );
        let trashBinDrop :any;
        await waitFor(() => {
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"000000000000000000000000","fromDocId":"000000000000000000000000","fromFileId":"60091286122436829c4ad3cc","fileName":"images (2).jpg","isFromThumbnail":true,"isFromWorkbench":false,"isFromCategory":false}
            trashBinDrop = getByTestId('drop-to-trashbin');
            expect(trashBinDrop).toBeInTheDocument();
            const mockdt = { getData: () =>  file};
            act(() => {
                fireEvent.drop(trashBinDrop, {
                    dataTransfer: mockdt});
            });
            // expect(mockdt.getData).toBeCalled();
        })
    });

})
