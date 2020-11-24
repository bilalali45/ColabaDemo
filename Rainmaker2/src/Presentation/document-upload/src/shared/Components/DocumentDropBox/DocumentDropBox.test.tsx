import React from 'react';
import { render, fireEvent, waitFor } from '@testing-library/react';
import App from '../../../App';
import { MemoryRouter } from 'react-router-dom';
import { MockEnvConfig } from '../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../test_utilities/LocalStoreMock';
import { createMemoryHistory } from 'history';
import { DateFormatWithMoment } from '../../../utils/helpers/DateFormat';
import { StoreProvider } from '../../../store/store';
import {createMockFile} from '../../../components/Home/DocumentRequest/DocumentUpload/DocumentUpload.test'
import { AdaptiveWrapper } from '../../../test_utilities/AdaptiveWrapper';
import { FileUpload } from '../../../utils/helpers/FileUpload';


jest.mock('axios');
jest.mock('../../../store/actions/UserActions');
jest.mock('../../../store/actions/LoanActions');
jest.mock('../../../store/actions/DocumentActions');
jest.mock('../../../services/auth/Auth');

beforeEach(() => {
  MockLocalStorage();
  MockEnvConfig();
  const history = createMemoryHistory();
  history.push('/');
});

describe('Drag and Drop Documents', () => {
    test('should drag and drop selected file', async () => {
        const { getByTestId, getAllByTestId, getByRole } = render(
            <AdaptiveWrapper>
                <StoreProvider>
                    <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                        <App />
                    </MemoryRouter>
                </StoreProvider>
            </AdaptiveWrapper>
        );

        FileUpload.isTypeAllowed = jest.fn(() => Promise.resolve(true));
        FileUpload.isSizeAllowed = jest.fn(() => true);

        let taskList: any;
        await waitFor(() => {
            taskList = getByTestId('/documentsRequest');
        })
        fireEvent.click(taskList);

        let taskListHeader: any;
        await waitFor(() => {
            taskListHeader = getByTestId('task-list-header');
        })
        expect(taskListHeader).toBeInTheDocument();  
        expect(taskListHeader).toHaveTextContent('You have 3 items to complete');

        const requiredDocsContainer = getByTestId('requiredDocsList');
        expect(requiredDocsContainer).toBeInTheDocument();
        
        const thirdDoc = getByTestId('pending-doc-2');
        expect(thirdDoc).toHaveTextContent('Salary Slip');

        fireEvent.click(thirdDoc);

        await waitFor(() => {
            let noFileScreen = getByTestId('no-files-screen');
            expect(noFileScreen).toBeInTheDocument();
            expect(noFileScreen).toHaveTextContent('You don’t have any files here.')
        });

        const input = getByTestId('file-input');
        const file = createMockFile('test.jpg', 30000, 'image/jpeg');
        fireEvent.dragEnter(input, { dataTransfer: { files: [file] }})

        const fileDropper = getByTestId("document-dropper");
        fireEvent.dragOver(input, { dataTransfer: { files: [file] }})
        expect(fileDropper).toHaveAttribute("class", "Doc-upload drag-enter dragableArea");
        
        fireEvent.dragLeave(input, { dataTransfer: { files: [file] }})
        expect(fileDropper).not.toHaveAttribute("class", "Doc-upload drag-enter dragableArea");

        expect(fileDropper).toBeInTheDocument();
        fireEvent.drop(input, { dataTransfer: { files: [file] } });

        await waitFor(() => {
            let filesContainer = getByTestId('files-container');
            expect(filesContainer).toBeInTheDocument();
            expect(getByRole('dialog')).toBeInTheDocument();

            let doc = getByTestId('rename-popup-doc-container');

            expect(doc).toHaveTextContent('Original Document Name');
            expect(doc).toHaveTextContent('test.jpeg');
            
            let renameInput = getByTestId('rename-input-adaptive');
            
            expect(renameInput).toBeInTheDocument();
            
        });

        fireEvent.click(getByTestId('name-save-btn-adaptive'));

        await waitFor(() => {

            let currentDocFiles = getAllByTestId('file-item');
            expect(currentDocFiles).toHaveLength(1);
            expect(currentDocFiles[0]).toHaveTextContent('test.jpeg');
        })
    });
})
