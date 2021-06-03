import React from 'react';
import { render, fireEvent, waitFor, getByTestId, getAllByTestId, act } from '@testing-library/react';
import userEvent from '@testing-library/user-event'
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../../../../../Store/Store';
import { MockEnvConfig } from '../../../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../../../test_utilities/LocalStoreMock';
import { DocumentsTable } from '../DocumentsTable';
import { Home } from '../../../../Home';


jest.mock('pspdfkit');
jest.mock('../../../../../../Store/actions/DocumentActions');
jest.mock('../../../../../../Store/actions/TemplateActions');

const Url = '/DocManager/2515'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    JSON.parse = jest.fn((data:any)=> data)
});

describe('Doc Item Section ', () => {

    test('Should show Docs Name', async () => {
        const { getAllByTestId, getByTestId, getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        
        await waitFor(() => {
                const docItems = getAllByTestId("document-name");
    
                expect(docItems[0]).toHaveTextContent("Bank Statements - Two Months")
            })
    });
    
    test('Should hide files', async () => {
        const { getAllByTestId, getByTestId, getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        
        await waitFor(() => {
                const docItems = getAllByTestId("document-name");
    
                expect(docItems[0]).toHaveTextContent("Bank Statements - Two Months")
                fireEvent.click(docItems[0])
            })
    });
    
    
    test('Should show Docs Status', async () => {
        const { getAllByTestId, getByTestId, getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        
        await waitFor(() => {
                const docItems = getAllByTestId("document-item");
    
                expect(docItems[0]).toHaveTextContent("Manually Added")
                expect(docItems[1]).toHaveTextContent("Borrower To Do")
                expect(docItems[2]).toHaveTextContent("Started")
                expect(docItems[3]).toHaveTextContent("Completed")
                expect(docItems[4]).toHaveTextContent("Pending Review")
               
            })
    });
    
    test('Should show delete doc btn when no file in doc', async () => {
        const { getAllByTestId, getByTestId, getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDelBtn ;
        await waitFor(() => {
            docDelBtn = getByTestId("btn-doc-delete");
    
                expect(docDelBtn).toBeInTheDocument()
            })
            fireEvent.click(docDelBtn)
            await waitFor(() => {
                let deleteAlert = getByTestId("delete-alert")
                expect(deleteAlert).toBeInTheDocument()
    
                expect(deleteAlert).toHaveTextContent("Remove this document type?")
            })
    });

    // test('Should hide delete doc alert', async () => {
    //     const { getAllByTestId, getByTestId, getByText } = render(
    //         <StoreProvider>
    //             <MemoryRouter initialEntries={[Url]}>
    //                 <DocumentsTable></DocumentsTable>
    //             </MemoryRouter>
    //         </StoreProvider>
    //     );
    //     let docDelBtn ;
    //     await waitFor(() => {
    //         docDelBtn = getByTestId("btn-doc-delete");
    
    //             expect(docDelBtn).toBeInTheDocument()
    //         })
    //         fireEvent.click(docDelBtn)
    //         let confirmDeleteBtn:any;
    //         await waitFor(() => {
    //             let deleteAlert = getByTestId("delete-alert")
    //             expect(deleteAlert).toBeInTheDocument()
    
    //             confirmDeleteBtn= getByTestId("hide-doc-alert")
    //             fireEvent.click(confirmDeleteBtn)

    //             expect(deleteAlert).not.toBeInTheDocument()
    //         })
    
    // });

    test('Should delete doc btn when confirmed', async () => {
        const { getAllByTestId, getByTestId, getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDelBtn ;
        await waitFor(() => {
            docDelBtn = getByTestId("btn-doc-delete");
    
                expect(docDelBtn).toBeInTheDocument()
            })
            fireEvent.click(docDelBtn)
            let confirmDeleteBtn:any;
            await waitFor(() => {
                let deleteAlert = getByTestId("delete-alert")
                expect(deleteAlert).toBeInTheDocument()
    
                confirmDeleteBtn= getByTestId("confirm-doc-delete")
                fireEvent.click(confirmDeleteBtn)
            })
    
    });

    

    


    test('Should drop file to document Item from other category', async () => {     
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDrop :any;
        await waitFor(() => {
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"60068fc132088251cb1c70f6","fromDocId":"60068fc132088251cb1c70f7","fromFileId":"60001b9932088251cb1c7061","fileName":"images 2.jpeg","isFromThumbnail":false,"isFromWorkbench":false,"isFromCategory":true}
            docDrop = getAllByTestId('doc-dnd');
            
            const mockdt = { getData: () =>  file};
            act(() => {
                fireEvent.drop(docDrop[0], {
                    dataTransfer: mockdt, preventDefault: ()=>jest.fn()});
            });
            // expect(mockdt.getData).toBeCalled();
        })
    });


    test('Should drop file to document Item from trash', async () => {     
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDrop :any;
        await waitFor(() => {
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"000000000000000000000000","fromDocId":"000000000000000000000000","fromFileId":"60001b9932088251cb1c7061","isFromTrash":true}
            docDrop = getAllByTestId('doc-dnd');
            
            const mockdt = { getData: () =>  file};
            act(() => {
                fireEvent.drop(docDrop[0], {
                    dataTransfer: mockdt, preventDefault: ()=>jest.fn()});
            });
            // expect(mockdt.getData).toBeCalled();
        })
    });

    test('Should drop file to document Item from workbench', async () => {     
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDrop :any;
        await waitFor(() => {
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"000000000000000000000000","fromDocId":"000000000000000000000000","fromFileId":"60001b9932088251cb1c7061","fileName":"images (1).jpg","isFromThumbnail":false,"isFromWorkbench":true,"isFromCategory":false}
            docDrop = getAllByTestId('doc-dnd');
            
            const mockdt = { getData: () =>  file};
            act(() => {
                fireEvent.drop(docDrop[0], {
                    dataTransfer: mockdt, preventDefault: ()=>jest.fn()});
            });
            // expect(mockdt.getData).toBeCalled();
        })
    });

    test('Should drop file to document Item from thumbnail', async () => {     
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDrop :any;
        await waitFor(() => {
            const file =  {"indexes":[0],"isFromThumbnail":true,"isFromWorkbench":false,"isFromCategory":false}
            docDrop = getAllByTestId('doc-dnd');
            
            const mockdt = { getData: () =>  file};
            // act(() => {
            //     fireEvent.drop(docDrop[0], {
            //         dataTransfer: mockdt, preventDefault: ()=>jest.fn()});
            // });
            // expect(mockdt.getData).toBeCalled();
        })

        
    });


    test('Should drop file to document Item from PC', async () => {     
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsTable></DocumentsTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDrop :any;
        await waitFor(() => {
            
            docDrop = getAllByTestId('doc-dnd');
            
            act(() => {
                fireEvent.drop(docDrop[0], {
        dataTransfer: {
            files: [new File(['(⌐□_□)'], 'chucknorris.png', { type: 'image/png' })],
          },
        })
            });
            // expect(mockdt.getData).toBeCalled();
        })

        
    });

    
})
