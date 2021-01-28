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
jest.mock('pspdfkit');
jest.mock('../../../../Store/actions/DocumentActions');
jest.mock('../../../../Store/actions/TemplateActions');
const Url = '/DocManager/2515'
beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    
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

    // test('Should move file from workbench to trash', async () => {
    //     const { getByText, getByTestId, getAllByTestId } = render(
    //         <StoreProvider>
    //             <MemoryRouter initialEntries={[Url]}>
    //                 <Home />
    //             </MemoryRouter>
    //         </StoreProvider>
    //     );

    //     let workbenchItemsList = getAllByTestId('workbench-item');
    //     let firstWorkbenchItem = workbenchItemsList[0];
    //     let secondWorkbenchItem = workbenchItemsList[1];
    //     expect(firstWorkbenchItem).toContain('Uploaded By:');
    //     expect(secondWorkbenchItem).toContain('Modified By:');
    //     // let reassignIcon = getByTestId('reassign-icon-test-doc-1.jpeg');
    //     // fireEvent.click(reassignIcon);
    //     // await waitFor(() => {
    //     //     let reassignDropdown = getByTestId('reassign-dropdown');
    //     //     expect(reassignDropdown).toBeInTheDocument();
    //     // });
    // });
})