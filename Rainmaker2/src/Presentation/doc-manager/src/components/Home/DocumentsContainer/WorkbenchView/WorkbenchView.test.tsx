import React from 'react';
import { render, fireEvent, waitFor, getByTestId, waitForElementToBeRemoved } from '@testing-library/react';
import userEvent from '@testing-library/user-event'
import { MockEnvConfig } from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../../../Store/Store';
import { MemoryRouter } from 'react-router-dom';
import { WorkbenchView } from './WorkbenchView';
import { Home } from '../../Home';
import { act } from 'react-dom/test-utils';
import { WorkbenchTable } from './WorkbenchTable/WorkbenchTable';
import { DocumentsContainer } from '../DocumentsContainer';
jest.mock('pspdfkit');
jest.mock('../../../../Store/actions/DocumentActions');
jest.mock('../../../../Store/actions/TemplateActions');
const Url = '/DocManager/2515'
beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    JSON.parse = jest.fn((data:any)=> data)
    
});
describe('Doc Manager Header', () => {
    test('Should render workbenchview', async () => {
        const { getByText } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        const title = getByText('Uncategorized Doc');
        expect(title).toBeInTheDocument();
    });
    test('Should render workbenchview', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        let workbenchTabel = getByTestId('workbench-table');
        expect(workbenchTabel).toBeInTheDocument();
        const showWorkbench = getByTestId('show-uncategorized');
        expect(showWorkbench).toBeInTheDocument();

        fireEvent.click(showWorkbench)
        await waitFor(()=>{
            expect(workbenchTabel).not.toBeInTheDocument();
        })
        fireEvent.click(showWorkbench)
    });
    test('Should render workbench table', async () => {
        const { getByText, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        let workbenchTabel = getByTestId('workbench-table');
        expect(workbenchTabel).toBeInTheDocument();
        let workbenchItemsList = getByTestId('workbench-items-list');
        expect(workbenchItemsList).toBeInTheDocument();
    });
    test('Should render workbench table items', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        let workbenchTabel = getByTestId('workbench-table');
        expect(workbenchTabel).toBeInTheDocument();
        let workbenchItemsList = getAllByTestId('workbench-item');
        expect(workbenchItemsList[0]).toHaveTextContent('test-doc-1.jpeg');
        expect(workbenchItemsList[1]).toHaveTextContent('test-doc-2.pdf');
        expect(workbenchItemsList[2]).toHaveTextContent('test-doc-3.pdf');
    });

    test('Should view workbench file', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        let workbenchTabel = getByTestId('workbench-table');
        expect(workbenchTabel).toBeInTheDocument();
        let workbenchItemsList = getAllByTestId("workbench-item-div");
        expect(workbenchItemsList[0]).toHaveTextContent('test-doc-1.jpeg');
        fireEvent.click(workbenchItemsList[0])
    });

    test('Should remove workbench item if trash icon clicked', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        let workbenchTable = getByTestId('workbench-table');
        expect(workbenchTable).toBeInTheDocument();
        let workbenchItemsList = getAllByTestId('workbench-item');
        let firstWorkbenchItem = workbenchItemsList[0];
        expect(firstWorkbenchItem).toHaveTextContent('test-doc-1.jpeg');
        let trashCan = getAllByTestId('trash-icon');
        expect(trashCan[0]).toBeInTheDocument();
        fireEvent.click(trashCan[0]);
    });
    test('Should show reassign dropdown if reassign icon clicked', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        let workbenchTable = getByTestId('workbench-table');
        expect(workbenchTable).toBeInTheDocument();
        let workbenchItemsList = getAllByTestId('workbench-item');
        let firstWorkbenchItem = workbenchItemsList[0];
        expect(firstWorkbenchItem).toHaveTextContent('test-doc-1.jpeg');
        let reassignIcon = getByTestId('reassign-icon-test-doc-1.jpeg');
        fireEvent.click(reassignIcon);
        await waitFor(() => {
            let reassignDropdown = getByTestId('reassign-dropdown');
            expect(reassignDropdown).toBeInTheDocument();
        });
    });


    test('Should show reassign dropdown heading if reassign icon clicked', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsContainer />
                </MemoryRouter>
            </StoreProvider>
        );
        let workbenchTable = getByTestId('workbench-table');
        expect(workbenchTable).toBeInTheDocument();
        let workbenchItemsList = getAllByTestId('workbench-item');
        let firstWorkbenchItem = workbenchItemsList[0];
        expect(firstWorkbenchItem).toHaveTextContent('test-doc-1.jpeg');
        let reassignIcon = getByTestId('reassign-icon-test-doc-1.jpeg');
        fireEvent.click(reassignIcon);
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
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsContainer />
                </MemoryRouter>
            </StoreProvider>
        );
        let workbenchTable = getByTestId('workbench-table');
        expect(workbenchTable).toBeInTheDocument();
        let workbenchItemsList = getAllByTestId('workbench-item');
        let firstWorkbenchItem = workbenchItemsList[0];
        expect(firstWorkbenchItem).toHaveTextContent('test-doc-1.jpeg');
        let reassignIcon = getByTestId('reassign-icon-test-doc-1.jpeg');
        fireEvent.click(reassignIcon);
        let reassignDropdown :any;
        await waitFor(() => {
            reassignDropdown = getByTestId('reassign-dropdown');
            expect(reassignDropdown).toBeInTheDocument();
            

            // 
        });
        let docCategories = getAllByTestId("reassign-cat-doc")
        expect(docCategories[0]).toHaveTextContent("Bank Statements - Two Months")
        expect(docCategories[1]).toHaveTextContent("Bank Statements - Two Months")
        expect(docCategories[2]).toHaveTextContent("Tax Returns with Schedules (Personal - Two Years)")
    });

    test('Should show hide reassign pop up if clicked on any category', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <DocumentsContainer />
                </MemoryRouter>
            </StoreProvider>
        );
        let workbenchTable = getByTestId('workbench-table');
        expect(workbenchTable).toBeInTheDocument();
        let workbenchItemsList = getAllByTestId('workbench-item');
        let firstWorkbenchItem = workbenchItemsList[0];
        expect(firstWorkbenchItem).toHaveTextContent('test-doc-1.jpeg');
        let reassignIcon = getByTestId('reassign-icon-test-doc-1.jpeg');
        fireEvent.click(reassignIcon);
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


    


    test('Should show text Modified By: if doc has been modified by mcu else Uploaded By: ', async () => {
        const { getByText, getByTestId, getAllByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Home />
                </MemoryRouter>
            </StoreProvider>
        );
        let workbenchTable = getByTestId('workbench-table');
        expect(workbenchTable).toBeInTheDocument();
        let workbenchItemsList = getAllByTestId('file-modified-status');
        let firstWorkbenchItem = workbenchItemsList[0];
        let secondWorkbenchItem = workbenchItemsList[1];
        expect(firstWorkbenchItem).toHaveTextContent('Uploaded By:');
        expect(secondWorkbenchItem).toHaveTextContent('Modified By:');
        // let reassignIcon = getByTestId('reassign-icon-test-doc-1.jpeg');
        // fireEvent.click(reassignIcon);
        // await waitFor(() => {
        //     let reassignDropdown = getByTestId('reassign-dropdown');
        //     expect(reassignDropdown).toBeInTheDocument();
        // });
    });

    test('Should drag file from trash bin', async () => {
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <WorkbenchTable></WorkbenchTable>
                </MemoryRouter>
            </StoreProvider>
        );
        
        let workbenchFiles:any;       
        
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"000000000000000000000000","fromDocId":"000000000000000000000000","fromFileId":"60001b9932088251cb1c7061","fileName":"fdg-copy-1.pdf","isFromThumbnail":false,"isFromWorkbench":true,"isFromCategory":false}
            workbenchFiles  = getAllByTestId('workbench-item-div');
            const mockdt = { setData: () =>  file };
            // fireEvent.dragStart(trashBinDocs[0].children[1], { dataTransfer: mockdt});
            act(() => {
                fireEvent.dragStart(workbenchFiles[0].children[1], {
                    dataTransfer: mockdt});
            });
            // expect(mockdt.setData).toBeCalled();
        
    });

    test('Should drop file to document Item from trash', async () => {     
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <WorkbenchTable></WorkbenchTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDrop :any;
        await waitFor(() => {
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"000000000000000000000000","fromDocId":"000000000000000000000000","fromFileId":"60001b9932088251cb1c7061","isFromTrash":true}
            docDrop = getAllByTestId('workbench-table');
            
            const mockdt = { getData: () =>  file};
            act(() => {
                fireEvent.drop(docDrop[0], {
                    dataTransfer: mockdt, preventDefault: ()=>jest.fn()});
            });
            // expect(mockdt.getData).toBeCalled();
        })
    });

    test('Should drop file to document Item from category', async () => {     
        const { getByText, getAllByTestId, getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <WorkbenchTable></WorkbenchTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDrop :any;
        await waitFor(() => {
            const file =  {"id":"5fec45b9c20bc413c03d3b42","fromRequestId":"60090d9b122436829c4ad3c6","fromDocId":"60090d9b122436829c4ad3c7","fromFileId":"60001b9932088251cb1c7061","fileName":"fdg.pdf","isFromThumbnail":false,"isFromWorkbench":false,"isFromCategory":true}
            docDrop = getAllByTestId('workbench-table');
            
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
                    <WorkbenchTable></WorkbenchTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDrop :any;
        await waitFor(() => {
            const file =  {"indexes":[0],"isFromThumbnail":true,"isFromWorkbench":false,"isFromCategory":false}
            docDrop = getAllByTestId('workbench-table');
            
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
                    <WorkbenchTable></WorkbenchTable>
                </MemoryRouter>
            </StoreProvider>
        );
        let docDrop :any;
        await waitFor(() => {
            
            docDrop = getAllByTestId('workbench-table');
            
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