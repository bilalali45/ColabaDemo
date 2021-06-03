import React from 'react';
import { render, cleanup, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { createMemoryHistory } from 'history'

import { MockEnvConfig } from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import App from '../../../../App';
import { FileUpload } from '../../../../utils/helpers/FileUpload';
// import { DocumentUploadActions } from '../../../../store/actions/__mocks__/DocumentUploadActions';
import { DocumentRequest } from '../../../../entities/Models/DocumentRequest';
import { Document } from '../../../../entities/Models/Document';
import { DocumentsActionType } from '../../../../store/reducers/documentReducer';
import { DocumentUploadActions } from '../../../../store/actions/DocumentUploadActions';
import { seedData, getPendingDocs } from '../../../../store/actions/__mocks__/DocumentActions';
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { StoreProvider } from '../../../../store/store';
import { AdaptiveWrapper } from '../../../../test_utilities/AdaptiveWrapper';


jest.mock('axios');
jest.mock('../../../../store/actions/UserActions');
jest.mock('../../../../store/actions/LoanActions');
jest.mock('../../../../store/actions/DocumentActions');
// jest.mock('../../../../store/actions/DocumentUploadActions');
jest.mock('../../../../services/auth/Auth');


let uploadPercent = 0;

let cachedIsTypeAllowed = FileUpload.isTypeAllowed;
let cachedIsSizeAllowed = FileUpload.isSizeAllowed;

beforeEach(() => {

    // @ts-ignore
    delete window.location;
    // @ts-ignore
    window.location = new URL('http://localhost/');


    MockEnvConfig();
    MockLocalStorage();

    FileUpload.isTypeAllowed = jest.fn(() => Promise.resolve(true));
    FileUpload.isSizeAllowed = jest.fn(() => true);

    const submitDocuments: any = (currentSelected: DocumentRequest, file: Document, dispatchProgress: Function, loanApplicationId: string) => {

        let p = Math.floor(uploadPercent * 100);
        let files: any = currentSelected.files;
        let updatedFiles = files.map((f: Document) => {
            if (f.clientName === file.clientName) {
                f.uploadProgress = p;
                if (p === 100) {
                    f.uploadStatus = "done";
                }
                return f;
            }
            return f;
        });
        dispatchProgress({
            type: DocumentsActionType.AddFileToDoc,
            payload: updatedFiles,
        });

    }

    DocumentUploadActions.submitDocuments = jest.fn((a, b, c, d) =>  Promise.resolve(submitDocuments(a, b, c, d)))
});

afterEach(() => {
    FileUpload.isTypeAllowed = cachedIsTypeAllowed;
    FileUpload.isSizeAllowed = cachedIsSizeAllowed;
})

export const createMockFile = (name, size, mimeType) => {
    let range = '';
    for (let i = 0; i < size; i++) {
        range += 'a';
    }
    let blob: any = new Blob([range], { type: mimeType });
    blob.lastModified = 1600081543628;
    blob.lastModifiedDate = 'Mon Sep 14 2020 16:05:43 GMT+0500 (Pakistan Standard Time)';
    blob.name = name;
    return blob;
}

describe('Document Request File Upload', () => {

    test('Should add a new file into list" ', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        FileUpload.isTypeAllowed = jest.fn(() => Promise.resolve(true));
        FileUpload.isSizeAllowed = jest.fn(() => true);
        let getStartedBtn: any;
        await waitFor(() => {
            getStartedBtn = getByTestId('get-started');
        })

        fireEvent.click(getStartedBtn);
        let input: any;
        await waitFor(() => {
            input = getByTestId('more-file-input');
        });

        const file = createMockFile('test.jpg', 30000, 'image/jpeg');

        fireEvent.change(input, { target: { files: [file] } });

        await waitFor(() => {
            const renameInput: any = getByTestId('file-item-rename-input');
            fireEvent.blur(renameInput);
            expect(renameInput).not.toBeInTheDocument();
        })


        const files = getAllByTestId('file-item');
        await waitFor(() => {

            expect(files).toHaveLength(2);
            expect(files[1]).toHaveTextContent('test.jpeg');

        })
    });

    test('Should save the file name on save button click" ', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');

            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('more-file-input');
        const file = createMockFile('sample.pdf', 30000, 'application/pdf');
        fireEvent.change(input, { target: { files: [file] } });


        await waitFor(() => {
            const saveBtn: any = getByTestId('name-save-btn');
            fireEvent.click(saveBtn);
            expect(saveBtn).not.toBeInTheDocument();
        })


        const files = getAllByTestId('file-item');
        await waitFor(() => {

            expect(files).toHaveLength(2);
            expect(files[1]).toHaveTextContent('sample-copy-1.pdf');

        })
    });

    test('Should enable the edit input on edit icon click" ', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');

            fireEvent.click(getStartedBtn);
        })


        await waitFor(() => {
            const input = getByTestId('more-file-input');
            const file = createMockFile('sample.pdf', 30000, 'application/pdf');
            fireEvent.change(input, { target: { files: [file] } });
        })

        await waitFor(() => {
            const editBtn: any = getByTestId('file-edit-btn-1');
            fireEvent.click(editBtn);            
        })

        const fileRenameInput = getByTestId('file-item-rename-input');
        expect(fileRenameInput).toBeInTheDocument();
        fireEvent.change(fileRenameInput, { target: { value: "newTestFile.jpeg" } });


        fireEvent.click(getByTestId('name-save-btn'));

    await waitFor(() => {

        let currentDocFiles = getAllByTestId('file-item');
        expect(currentDocFiles).toHaveLength(1);
        expect(currentDocFiles[0]).toHaveTextContent('newTestFile.jpeg');
    })  


    });

    test('Should remove the file on cross icon click" ', async () => {

        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');

            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('more-file-input');
        const file = createMockFile('sample.pdf', 30000, 'application/pdf');
        fireEvent.change(input, { target: { files: [file] } });


        await waitFor(() => {
            const saveBtn: any = getByTestId('name-save-btn');
            fireEvent.click(saveBtn);
            expect(saveBtn).not.toBeInTheDocument();
        })


        const files = getAllByTestId('file-item');
        await waitFor(() => {
            expect(files).toHaveLength(2);

        })

        await waitFor(() => {
            const delBtn: any = getByTestId('file-remove-btn-1');
            fireEvent.click(delBtn);
        })
        await waitFor(() => {
            const files = getAllByTestId('file-item');
            expect(files).toHaveLength(1);
        })
    });

    test('Should enable edit mode on double click" ', async () => {

        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');

            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('more-file-input');
        const file = createMockFile('sample.pdf', 30000, 'application/pdf');
        fireEvent.change(input, { target: { files: [file] } });


        await waitFor(() => {
            const saveBtn: any = getByTestId('name-save-btn');
            fireEvent.click(saveBtn);
            expect(saveBtn).not.toBeInTheDocument();
        })

        const fileContainer: any = getByTestId('file-container-1');
        fireEvent.doubleClick(fileContainer);
        expect(getByTestId('file-item-rename-input')).toBeInTheDocument();

    });

    test('Should show size not allowed item" ', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        FileUpload.isSizeAllowed = jest.fn(() => false);


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('more-file-input');
        const file = createMockFile('sample.pdf', 110000, 'application/pdf');
        fireEvent.change(input, { target: { files: [file] } });

        await waitFor(() => {
            const sizeNotAllowedItem = getByTestId('size-not-allowed-item');
            expect(sizeNotAllowedItem).toBeInTheDocument();
        })

    });

    test('Should show type not allowed item" ', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        FileUpload.isTypeAllowed = jest.fn(() => Promise.resolve(false));


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('more-file-input');
        const file = createMockFile('sample.pdf', 110000, 'application/pdf');
        fireEvent.change(input, { target: { files: [file] } });

        await waitFor(() => {
            const sizeNotAllowedItem = getByTestId('type-not-allowed-item');
            expect(sizeNotAllowedItem).toBeInTheDocument();
        })

    });

    test('Should show submit button enabled or disabled" ', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );
        let getStartedBtn: any;
        await waitFor(() => {
            getStartedBtn = getByTestId('get-started');
        })
        fireEvent.click(getStartedBtn);
        
        const input = getByTestId('more-file-input');
        const file = createMockFile('sample.pdf', 110000, 'application/pdf');
        fireEvent.change(input, { target: { files: [file] } });

        const taskListContainer = getByTestId('files-container');
        let submitBtn: any = null;
        let saveBtn: any = null;
        let fileContainer: any = null;
        let renameInput: any = null;

        await waitFor(() => {

            submitBtn = getByTestId('submit-button');
            saveBtn = getByTestId('name-save-btn');
            renameInput = getByTestId('file-item-rename-input');
            fileContainer = getByTestId('file-container-1');
            expect(taskListContainer).toContainElement(saveBtn);
            expect(taskListContainer).toContainElement(submitBtn);
            expect(submitBtn).toBeDisabled();

        });


        await waitFor(() => {
            fireEvent.click(saveBtn);
            expect(submitBtn).toBeEnabled();
            expect(saveBtn).not.toBeInTheDocument();
        })

        fireEvent.doubleClick(fileContainer);
        await waitFor(() => {
            renameInput = getByTestId('file-item-rename-input');
            expect(renameInput).toHaveFocus();
            expect(submitBtn).toBeDisabled();

        })

    });

    test('Should start uploading files and show progress for each file on submit click" ', async () => {

        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );

        uploadPercent = 0.8;
        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('more-file-input');
        const file = createMockFile('sample.pdf', 110000, 'application/pdf');
        fireEvent.change(input, { target: { files: [file] } });

        await waitFor(() => {
            const saveBtn: any = getByTestId('name-save-btn');
            fireEvent.click(saveBtn);
            expect(saveBtn).not.toBeInTheDocument();
        })

        const files = getAllByTestId('file-item');
        const submitBtn = getByTestId('submit-button');

        fireEvent.click(submitBtn);

        await waitFor(() => {
            const progressBar: any = getByTestId('upload-progress-bar');
            expect(progressBar).toHaveStyle("width: 80%");
            const delBtn: any = getByTestId('file-remove-btn-1');
            expect(delBtn).toBeInTheDocument();
        })

    });

    test('Should upload files on submit click" ', async () => {

        uploadPercent = 1;

        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('more-file-input');
        const file = createMockFile('sample.pdf', 110000, 'application/pdf');
        fireEvent.change(input, { target: { files: [file] } });

        await waitFor(() => {
            const saveBtn: any = getByTestId('name-save-btn');
            fireEvent.click(saveBtn);
            expect(saveBtn).not.toBeInTheDocument();
        })

        const files = getAllByTestId('file-item');

        const submitBtn = getByTestId('submit-button');
        fireEvent.click(submitBtn);

        expect(files).toHaveLength(2);

        let allDoneIcons = getAllByTestId('done-upload');
        await waitFor(() => {
            expect(allDoneIcons[1]).toContainHTML('<i class="zmdi zmdi-check"></i>')

        });

    });

    test('Should hide submit button after files are uploaded and instead show I\'m done button" ', async () => {

        uploadPercent = 1;

        const { getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('more-file-input');
        const file = createMockFile('sample.pdf', 110000, 'application/pdf');
        fireEvent.change(input, { target: { files: [file] } });

        await waitFor(() => {
            const saveBtn: any = getByTestId('name-save-btn');
            fireEvent.click(saveBtn);
            expect(saveBtn).not.toBeInTheDocument();
        })

        const files = getAllByTestId('file-item');

        const submitBtn = getByTestId('submit-button');
        fireEvent.click(submitBtn);

        expect(files).toHaveLength(2);

        let allDoneIcons = getAllByTestId('done-upload');
        await waitFor(() => {
            expect(allDoneIcons[1]).toContainHTML('<i class="zmdi zmdi-check"></i>')

        });

        expect(submitBtn).not.toBeInTheDocument();

        expect(getByText('I\'M Done')).toBeInTheDocument();

    });

    test('Should show I\'m done button if all files have been submitted" ', async () => {

        const { getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        expect(getByText('I\'M Done')).toBeInTheDocument();

    });

    test('Should remove the selected doc from pending documents on I\'m done button click" ', async () => {

        const { getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })
        const doneBtn = getByText('I\'M Done');

        expect(doneBtn).toBeInTheDocument();

        fireEvent.click(doneBtn);



        await waitFor(() => {
            const selectedDocTitle = getByTestId('selected-doc-title');
            expect(selectedDocTitle).not.toHaveTextContent('Alimony Income Verification');
            expect(selectedDocTitle).toHaveTextContent('Bank Statement');
        })
    });

    test('Should move to the next document on I\'ll Come Back button click" ', async () => {

        const { getByTestId, getAllByTestId, getByText } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <StoreProvider>
                    <App />
                </StoreProvider>
            </MemoryRouter>
        );


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })
        const illComBackBtn = getByText('I\'LL Come Back');

        expect(illComBackBtn).toBeInTheDocument();

        fireEvent.click(illComBackBtn);

        await waitFor(() => {
            expect(getByTestId('selected-doc-title')).toHaveTextContent('Bank Statement');
        })
    });

})

describe('Document Upload File Upload Adaptive', () => {
    test('should show the count of pending documents', async () => {
        const { getByTestId, getAllByTestId } = render(
            <AdaptiveWrapper>
                <StoreProvider>
                    <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                        <App />
                    </MemoryRouter>
                </StoreProvider>
            </AdaptiveWrapper>
        );
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
    })
    
    test('should render pending document items', async () => {
        const { getByTestId, getAllByTestId } = render(
            <AdaptiveWrapper>
                <StoreProvider>
                    <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                        <App />
                    </MemoryRouter>
                </StoreProvider>
            </AdaptiveWrapper>
        );
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
        
        expect(getByTestId('pending-doc-0')).toHaveTextContent('Alimony Income Verification');
        expect(getByTestId('pending-doc-1')).toHaveTextContent('Bank Statement');
        expect(getByTestId('pending-doc-2')).toHaveTextContent('Salary Slip');
    })
    
    test('should navigate to the file upload screen', async () => {
        const { getByTestId, getAllByTestId } = render(
            <AdaptiveWrapper>
                <StoreProvider>
                    <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                        <App />
                    </MemoryRouter>
                </StoreProvider>
            </AdaptiveWrapper>
        );
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
        
        const firstDoc = getByTestId('pending-doc-1');
        expect(firstDoc).toHaveTextContent('Bank Statement');

        fireEvent.click(firstDoc);

        await waitFor(() => {
            let filesContainer = getByTestId('files-container');
            expect(filesContainer).toBeInTheDocument();

            let currentDocFiles = getAllByTestId('file-item');

            let uploadDoneIcon = '<i class="zmdi zmdi-check"></i>';
          
            expect(currentDocFiles[0]).toHaveTextContent('nature.jpg');
            expect(currentDocFiles[0]).toContainHTML(uploadDoneIcon);
            expect(currentDocFiles[0]).toContainHTML('<i class="far fa-file-image"></i>');
            
            expect(currentDocFiles[2]).toHaveTextContent('sample.pdf');
            expect(currentDocFiles[2]).toContainHTML(uploadDoneIcon);
            expect(currentDocFiles[2]).toContainHTML('<i class="far fa-file-pdf"></i>');
            
        });
      
    })
    
    test('should show \"You don\'t have any files here.\" screen', async () => {
        const { getByTestId, getAllByTestId } = render(
            <AdaptiveWrapper>
                <StoreProvider>
                    <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                        <App />
                    </MemoryRouter>
                </StoreProvider>
            </AdaptiveWrapper>
        );
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
      
    })
    
    test('should add selected file', async () => {
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

        fireEvent.change(input, { target: { files: [file] } });

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
            // expect(currentDocFiles[0]).toContainHTML(uploadDoneIcon);
        })
    });
    
    
    test('should show type not allowed item', async () => {
        const { getByTestId, getAllByTestId, getByRole } = render(
            <AdaptiveWrapper>
                <StoreProvider>
                    <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                        <App />
                    </MemoryRouter>
                </StoreProvider>
            </AdaptiveWrapper>
        );

        FileUpload.isTypeAllowed = jest.fn(() => Promise.resolve(false));
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

        fireEvent.change(input, { target: { files: [file] } });

        await waitFor(() => {
            let filesContainer = getByTestId('files-container');
            expect(filesContainer).toBeInTheDocument();
            let currentDocFiles = getAllByTestId('type-not-allowed-item');
            expect(currentDocFiles).toHaveLength(1);
            expect(currentDocFiles[0]).toHaveTextContent('test.jpeg');
            expect(currentDocFiles[0]).toHaveTextContent('File type is not supported. Allowed types: PDF, JPEG, PNG');
        });

    })    
    
    test('should show size not allowed item', async () => {
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
        FileUpload.isSizeAllowed = jest.fn(() => false);

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

        fireEvent.change(input, { target: { files: [file] } });

        await waitFor(() => {
            let filesContainer = getByTestId('files-container');
            expect(filesContainer).toBeInTheDocument();
            let currentDocFiles = getAllByTestId('size-not-allowed-item');
            expect(currentDocFiles).toHaveLength(1);
            expect(currentDocFiles[0]).toHaveTextContent('test.jpeg');
            expect(currentDocFiles[0]).toHaveTextContent(`File size over ${FileUpload.allowedSize}mb limit`);
        });

    });
    
    test('should show add more files link', async () => {
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

        fireEvent.change(input, { target: { files: [file] } });

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
            // expect(currentDocFiles[0]).toContainHTML(uploadDoneIcon);
        })
            const addMorefilesbtn = getByTestId("more-file-input");
            expect(addMorefilesbtn).toBeInTheDocument();

        const moreFileInput = getByTestId("more-file-input");
            
        const file2 = createMockFile('test2.jpg', 30000, 'image/jpeg');

        fireEvent.change(moreFileInput, { target: { files: [file2] } });

        await waitFor(() => {

         expect(getByRole('dialog')).toBeInTheDocument();
        });
            const renameDialog = getByTestId("rename-popup");
            expect(renameDialog).toBeInTheDocument();

            expect(renameDialog).toHaveTextContent('Original Document Name');
            expect(renameDialog).toHaveTextContent('test2.jpeg');

        

        fireEvent.click(getByTestId('name-save-btn-adaptive'));

        await waitFor(() => {

            let currentDocFiles = getAllByTestId('file-item');
            expect(currentDocFiles).toHaveLength(2);
            expect(currentDocFiles[0]).toHaveTextContent('test.jpeg');
            expect(currentDocFiles[1]).toHaveTextContent('test2.jpeg');
            // expect(currentDocFiles[0]).toContainHTML(uploadDoneIcon);
        })
    });

    test('should remove file ', async () => {
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

        fireEvent.change(input, { target: { files: [file] } });

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
            expect(currentDocFiles).toHaveLength(1)
        })
            const addMorefilesbtn = getByTestId("add-more-btn");
            expect(addMorefilesbtn).toBeInTheDocument();

            const fileOptions = getByTestId("file-options-0");
            expect(fileOptions).toBeInTheDocument();
            fireEvent.click(fileOptions);
            let removeBtn;
            await waitFor(() => {

                removeBtn = getByTestId('file-remove-btn-0');
                expect(removeBtn).toBeInTheDocument();
                expect(removeBtn.children[0].children[0]).toHaveAttribute("class", "zmdi zmdi-close");
                expect(removeBtn).toHaveTextContent("Delete")
            })
            fireEvent.click(removeBtn);

            await waitFor(() => {

            let noFileScreen = getByTestId('no-files-screen');
            expect(noFileScreen).toBeInTheDocument();
            expect(noFileScreen).toHaveTextContent('You don’t have any files here.')
            
        })
    });
    
    test('should rename file ', async () => {
        const { getByTestId, getAllByTestId, getByRole, getByText } = render(
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

        fireEvent.change(input, { target: { files: [file] } });

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
            expect(currentDocFiles).toHaveLength(1)
        })
            const addMorefilesbtn = getByTestId("add-more-btn");
            expect(addMorefilesbtn).toBeInTheDocument();

            const fileOptions = getByTestId("file-options-0");
            expect(fileOptions).toBeInTheDocument();
            fireEvent.click(fileOptions);
            let renameBtn;
            await waitFor(() => {

                renameBtn = getByTestId('file-edit-btn-0');
                expect(renameBtn).toBeInTheDocument();
                expect(renameBtn.children[1]).toHaveTextContent("Rename");
            })

            const renameIcon = getByTestId("DocEditIcon");
            expect(renameIcon).toBeInTheDocument();

            fireEvent.click(renameBtn);
            await waitFor(() => {
            expect(getByRole('dialog')).toBeInTheDocument();
            });

            const renameDialog = getByTestId("rename-popup");
            expect(renameDialog).toBeInTheDocument();

            expect(renameDialog).toHaveTextContent('Original Document Name');
            expect(renameDialog).toHaveTextContent('test.jpeg');

            const renameInput = getByTestId("rename-input-adaptive");
            
            fireEvent.change(renameInput, { target: { value: "newTestFile.jpeg" } });


            fireEvent.click(getByTestId('name-save-btn-adaptive'));

        await waitFor(() => {

            let currentDocFiles = getAllByTestId('file-item');
            expect(currentDocFiles).toHaveLength(1);
            expect(currentDocFiles[0]).toHaveTextContent('newTestFile.jpeg');
        })   
    });

    test('should show error when renaming file with empty string ', async () => {
        const { getByTestId, getAllByTestId, getByRole, getByText } = render(
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

        fireEvent.change(input, { target: { files: [file] } });

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
            expect(currentDocFiles).toHaveLength(1)
        })
            const addMorefilesbtn = getByTestId("add-more-btn");
            expect(addMorefilesbtn).toBeInTheDocument();

            const fileOptions = getByTestId("file-options-0");
            expect(fileOptions).toBeInTheDocument();
            fireEvent.click(fileOptions);
            let renameBtn;
            await waitFor(() => {

                renameBtn = getByTestId('file-edit-btn-0');
                expect(renameBtn).toBeInTheDocument();
                expect(renameBtn.children[1]).toHaveTextContent("Rename");
            })

            const renameIcon = getByTestId("DocEditIcon");
            expect(renameIcon).toBeInTheDocument();

            fireEvent.click(renameBtn);
            await waitFor(() => {
            expect(getByRole('dialog')).toBeInTheDocument();
            });

            const renameDialog = getByTestId("rename-popup");
            expect(renameDialog).toBeInTheDocument();

            expect(renameDialog).toHaveTextContent('Original Document Name');
            expect(renameDialog).toHaveTextContent('test.jpeg');

            const renameInput = getByTestId("rename-input-adaptive");
            
            fireEvent.change(renameInput, { target: { value: "" } });

            const error = getByTestId("rename-input-adaptive");
            expect(error).toHaveAttribute("class", "haveError");

            const errorMessage = getByTestId("empty-file-name-error");
            expect(errorMessage).toHaveTextContent("File name cannot be empty.");
            expect(errorMessage).toHaveAttribute("class", "dl-errorrename");

            const saveBtn = getByTestId("name-save-btn-adaptive");
            fireEvent.click(saveBtn);

            expect(renameDialog).toHaveTextContent('Original Document Name');
    });



    test('should show error when renaming file with special character', async () => {
        const { getByTestId, getAllByTestId, getByRole, getByText } = render(
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

        fireEvent.change(input, { target: { files: [file] } });

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
            expect(currentDocFiles).toHaveLength(1)
        })
            const addMorefilesbtn = getByTestId("add-more-btn");
            expect(addMorefilesbtn).toBeInTheDocument();

            const fileOptions = getByTestId("file-options-0");
            expect(fileOptions).toBeInTheDocument();
            fireEvent.click(fileOptions);
            let renameBtn;
            await waitFor(() => {

                renameBtn = getByTestId('file-edit-btn-0');
                expect(renameBtn).toBeInTheDocument();
                expect(renameBtn.children[1]).toHaveTextContent("Rename");
            })

            const renameIcon = getByTestId("DocEditIcon");
            expect(renameIcon).toBeInTheDocument();

            fireEvent.click(renameBtn);
            await waitFor(() => {
            expect(getByRole('dialog')).toBeInTheDocument();
            });

            const renameDialog = getByTestId("rename-popup");
            expect(renameDialog).toBeInTheDocument();

            expect(renameDialog).toHaveTextContent('Original Document Name');
            expect(renameDialog).toHaveTextContent('test.jpeg');

            const renameInput = getByTestId("rename-input-adaptive");
            
            fireEvent.change(renameInput, { target: { value: "~" } });

            const error = getByTestId("rename-input-adaptive");
            expect(error).toHaveAttribute("class", "haveError");

            const errorMessage = getByTestId("special-character-error");
            expect(errorMessage).toHaveTextContent("File name cannot contain any special characters");
            expect(errorMessage).toHaveAttribute("class", "dl-errorrename");

            const saveBtn = getByTestId("name-save-btn-adaptive");
            fireEvent.click(saveBtn);

            expect(renameDialog).toHaveTextContent('Original Document Name');
    });

    test('should show unique file name error', async () => {
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

        fireEvent.change(input, { target: { files: [file] } });

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
            // expect(currentDocFiles[0]).toContainHTML(uploadDoneIcon);
        })
            const addMorefilesbtn = getByTestId("more-file-input");
            expect(addMorefilesbtn).toBeInTheDocument();

        const moreFileInput = getByTestId("more-file-input");
            
        const file2 = createMockFile('test2.jpg', 30000, 'image/jpeg');

        fireEvent.change(moreFileInput, { target: { files: [file2] } });

        await waitFor(() => {

         expect(getByRole('dialog')).toBeInTheDocument();
        });
            const renameDialog = getByTestId("rename-popup");
            expect(renameDialog).toBeInTheDocument();

            expect(renameDialog).toHaveTextContent('Original Document Name');
            let renameInput = getByTestId("rename-input-adaptive");
            
            expect(renameDialog).toHaveTextContent('test2.jpeg');
    
            expect(renameInput).toBeInTheDocument();
            fireEvent.change(renameInput, { target: { value: "test" } });
    
            await waitFor(() => {
                const error = getByTestId("rename-input-adaptive");
            });
                const errorMessage = getByTestId("unique-file-name-error");
                expect(errorMessage).toHaveTextContent("File name must be unique.");
                expect(errorMessage).toHaveAttribute("class", "dl-errorrename");
    
                const saveBtn = getByTestId("name-save-btn-adaptive");
                fireEvent.click(saveBtn);
    
                expect(renameDialog).toHaveTextContent('Original Document Name');
       
    });

})

