import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor } from '@testing-library/react';

import { MemoryRouter } from 'react-router-dom';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../../../Store/Store';
import { NewNeedList } from './NewNeedList';

jest.mock('../../../../Store/actions/UserActions');
jest.mock('../../../../Store/actions/NewNeedListActions');
jest.mock('../../../../Store/actions/TemplateActions');
jest.mock('../../../../Utils/LocalDB');

beforeEach(() => {
    MockLocalStorage();
});


describe('New Need List', () => {
    test('Should render NewNeedList', async () => {
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            let newNeedList = getByText('Request Needs List');
            let closeBtn = getByText('Close');
            let saveNCloseBtn = getByText('Save & Close');
            let addDocumentBtn = getByTestId("add-document");
            expect(newNeedList).toBeInTheDocument();
            expect(closeBtn).toBeInTheDocument();
            expect(saveNCloseBtn).toBeInTheDocument();
            expect(addDocumentBtn).toBeInTheDocument();
        });
    });


    test('Should render no document Icon and text', async () => {

        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            
            let tempDocContainer = getByAltText("no-document-preview-icon");
            expect(tempDocContainer).toBeInTheDocument();

            expect(getByText("Nothing")).toBeInTheDocument();
            expect(getByText("You have not added any document")).toBeInTheDocument();
        });
    });

    test('Should add document to request need list', async () => {
        
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );
        let addDocumentBtn:any;
        await waitFor(() => {
            
            addDocumentBtn = getByTestId("add-document");
            expect(addDocumentBtn).toBeInTheDocument();
        });
            fireEvent.click(addDocumentBtn);


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

            const docTemp = getByTestId("doc-temp");

            expect(docTemp).toHaveTextContent("Rental Agreement")
       
    });
    test('Should add to Draft ', async () => {
        
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );
        let addDocumentBtn:any;
        await waitFor(() => {
            
            addDocumentBtn = getByTestId("add-document");
            expect(addDocumentBtn).toBeInTheDocument();
        });
            fireEvent.click(addDocumentBtn);
      
      
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
      
            const docTemp = getByTestId("doc-temp");
      
            expect(docTemp).toHaveTextContent("Rental Agreement")

            let closeBtn = getByText("Close");
            expect(closeBtn).toBeInTheDocument();

            fireEvent.click(closeBtn)

            let saveDraftBtn
            await waitFor(() => {
                saveDraftBtn = getByTestId("needlist-save-popup-button")
                expect(saveDraftBtn).toBeInTheDocument()
                fireEvent.click(saveDraftBtn);
            });

            await waitFor(() => {
                // expect(getByText("View Saved Draft"))
            })
       
      });


    test('Should render popup for document categories', async () => {
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        let addDocumentBtn:any;
        await waitFor(() => {
            
            addDocumentBtn = getByTestId("add-document");
            expect(addDocumentBtn).toBeInTheDocument();
        });
            fireEvent.click(addDocumentBtn);


            let docPopOver;
            await waitFor(() => {
                docPopOver = getByTestId('popup-add-doc');
                expect(docPopOver).toBeInTheDocument();
            });

            expect(getByText("All")).toBeInTheDocument();
            expect(getByTestId("search-doc-name")).toBeInTheDocument();
        });


        test('Should show selected document category in popup heading', async () => {
            const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
                <MemoryRouter>
                    <StoreProvider>
                        <NewNeedList />
                    </StoreProvider>
                </MemoryRouter>
            );

            let addDocumentBtn:any;
            await waitFor(() => {
                
                addDocumentBtn = getByTestId("add-document");
                expect(addDocumentBtn).toBeInTheDocument();
            });
                fireEvent.click(addDocumentBtn);
    
    
                let docPopOver;
                await waitFor(() => {
                    docPopOver = getByTestId('popup-add-doc');
                    expect(docPopOver).toBeInTheDocument();
                });
    
                let docCats = getAllByTestId('doc-cat');
                fireEvent.click(docCats[2]);

                const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');
    
                expect(selectedCatDocsContainer).toHaveTextContent('Liabilities');

            });

    test('Should remove document from request need list on clicking delete', async () => {
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        let addDocumentBtn:any;
        await waitFor(() => {
            
            addDocumentBtn = getByTestId("add-document");
            expect(addDocumentBtn).toBeInTheDocument();
        });
            fireEvent.click(addDocumentBtn);


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

            const docTemp = getByTestId("doc-temp");

            expect(docTemp).toHaveTextContent("Rental Agreement")
       

            let deleteDoc = docTemp.children[1];
            expect(deleteDoc).toHaveAttribute("class", "BTNclose");
            expect(deleteDoc.children[0]).toHaveAttribute("class", "zmdi zmdi-close");

            fireEvent.click(deleteDoc);

            await waitFor(() => {
                let tempDocContainer = getByAltText("no-document-preview-icon");
            expect(tempDocContainer).toBeInTheDocument();
            });

    });


    test('Should show document details', async () => {
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        let addDocumentBtn:any;
        await waitFor(() => {
            
            addDocumentBtn = getByTestId("add-document");
            expect(addDocumentBtn).toBeInTheDocument();
        });
            fireEvent.click(addDocumentBtn);


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

            const selectedTemplate  = getByTestId("veiw-selected-template");
            expect(selectedTemplate).toHaveTextContent("Rental Agreement");
            expect(selectedTemplate).toHaveTextContent("Add or edit details for this document");
            expect(getByTestId("doc-message")).toHaveTextContent("Lease or rental agreement");

    });

    test('Should edit document message', async () => {
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        let addDocumentBtn:any;
        await waitFor(() => {
            
            addDocumentBtn = getByTestId("add-document");
            expect(addDocumentBtn).toBeInTheDocument();
        });
            fireEvent.click(addDocumentBtn);


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

            const selectedTemplate  = getByTestId("veiw-selected-template");
            expect(selectedTemplate).toHaveTextContent("Rental Agreement");
            expect(selectedTemplate).toHaveTextContent("Add or edit details for this document");

         const message = getByTestId("email-content")
            expect(message).toHaveTextContent("Lease or rental agreement");

            fireEvent.change(message, { target: { value: "This is a text doc" } });
            const reviewReq = getByTestId("review-req-btn"); 

    fireEvent.click(reviewReq);
    });


    
    test('Should show save as template btn', async () => {
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        let addDocumentBtn:any;
        await waitFor(() => {
            
            addDocumentBtn = getByTestId("add-document");
            expect(addDocumentBtn).toBeInTheDocument();
        });
            fireEvent.click(addDocumentBtn);


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
                expect(getByTestId("save-as-template-btn")).toBeInTheDocument();
            });

    });

    test('Should save template on click', async () => {
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        let addDocumentBtn:any;
        await waitFor(() => {
            
            addDocumentBtn = getByTestId("add-document");
            expect(addDocumentBtn).toBeInTheDocument();
        });
            fireEvent.click(addDocumentBtn);


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

            let saveAsTempBtn:any;
            await waitFor(() => {
                saveAsTempBtn = getByTestId("save-as-template-btn");
                expect(saveAsTempBtn).toBeInTheDocument();
            });

            fireEvent.click(saveAsTempBtn);

            let tempName;
            await waitFor(() => {
                tempName = getByTestId("save-template-input");
                expect(tempName).toBeInTheDocument();
                fireEvent.change(tempName, { target: { value: "Temp1" } });
            })

            
            
            const saveBtn = getByTestId("save-temp-btn");
            expect(saveBtn).toBeInTheDocument();

            await waitFor(() => {
            fireEvent.click(saveBtn);
            })


            let addFromTemp;
            await waitFor(() => {
                
            addFromTemp = getByTestId("add-from-temp-btn");
            expect(addFromTemp).toBeInTheDocument()
            
            })
            

    });

    test('Should show empty name error', async () => {
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        let addDocumentBtn:any;
        await waitFor(() => {
            
            addDocumentBtn = getByTestId("add-document");
            expect(addDocumentBtn).toBeInTheDocument();
        });
            fireEvent.click(addDocumentBtn);


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

            let saveAsTempBtn:any;
            await waitFor(() => {
                saveAsTempBtn = getByTestId("save-as-template-btn");
                expect(saveAsTempBtn).toBeInTheDocument();
            });

            fireEvent.click(saveAsTempBtn);

            let tempName;
            await waitFor(() => {
                tempName = getByTestId("save-template-input");
                expect(tempName).toBeInTheDocument();
            })

            const saveBtn = getByTestId("save-temp-btn");
            expect(saveBtn).toBeInTheDocument();

            fireEvent.click(saveBtn);
                
            await waitFor(() => {
            const errorText = getByTestId("error-text");
            expect(errorText).toHaveTextContent("Template name cannot be empty")
            
            })
    });

    // test('Should show unique name error', async () => {
    //     const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
    //         <MemoryRouter>
    //             <StoreProvider>
    //                 <NewNeedList />
    //             </StoreProvider>
    //         </MemoryRouter>
    //     );

    //     let addDocumentBtn:any;
    //     await waitFor(() => {
            
    //         addDocumentBtn = getByTestId("add-document");
    //         expect(addDocumentBtn).toBeInTheDocument();
    //     });
    //         fireEvent.click(addDocumentBtn);


    //         let docPopOver;
    //         await waitFor(() => {
    //             docPopOver = getByTestId('popup-add-doc');
    //             expect(docPopOver).toBeInTheDocument();
    //         });
    
    //         let docCats = getAllByTestId('doc-cat');
    
    //         fireEvent.click(docCats[2]);
    
    //         const selectedCatDocsContainer = getByTestId('selected-cat-docs-container');
    
    //         expect(selectedCatDocsContainer).toHaveTextContent('Liabilities');
    
    //         const itemsToClick = getAllByTestId('doc-item');
    
    //         expect(itemsToClick[2]).toHaveTextContent('Rental Agreement'); 

    //         fireEvent.click(itemsToClick[2]);

    //         let saveAsTempBtn;
    //         await waitFor(() => {
    //             saveAsTempBtn = getByTestId("save-as-template-btn");
    //             expect(saveAsTempBtn).toBeInTheDocument();
    //         });

    //         fireEvent.click(saveAsTempBtn);

    //         let tempName;
    //         await waitFor(() => {
    //             tempName = getByTestId("save-template-input");
    //             expect(tempName).toBeInTheDocument();
    //         })

    //         fireEvent.change(tempName, { target: { value: "Temp1" } });

    //         const saveBtn = getByTestId("save-temp-btn");
    //         expect(saveBtn).toBeInTheDocument();

    //         fireEvent.click(saveBtn);

    //         let addFromTemp;
    //         await waitFor(() => {
                
    //         addFromTemp = getByTestId("add-from-temp-btn");

    //         })

    //         await waitFor(() => {
    //             saveAsTempBtn = getByTestId("save-as-template-btn");
    //             expect(saveAsTempBtn).toBeInTheDocument();
    //         });

    //         fireEvent.click(saveAsTempBtn);

    //         await waitFor(() => {
    //             tempName = getByTestId("save-template-input");
    //             expect(tempName).toBeInTheDocument();
    //         })

    //         fireEvent.change(tempName, { target: { value: "Temp1" } });

            
    //         expect(saveBtn).toBeInTheDocument();

    //         fireEvent.click(saveBtn);


    //         await waitFor(() => {
                
    //         const errorText = getByTestId("error-text");
    //         expect(errorText).toHaveTextContent("Template name must be unique")
            
    //         })
            

    // });

    test('Should search a document from search textbox', async () => {
        const { getByAltText, getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter>
                <StoreProvider>
                    <NewNeedList />
                </StoreProvider>
            </MemoryRouter>
        );

        let addDocumentBtn:any;
        await waitFor(() => {
            
            addDocumentBtn = getByTestId("add-document");
            expect(addDocumentBtn).toBeInTheDocument();
        });
            fireEvent.click(addDocumentBtn);


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
    
})


