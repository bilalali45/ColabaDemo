import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText } from '@testing-library/react'
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


jest.mock('axios');
jest.mock('../../../../store/actions/UserActions');
jest.mock('../../../../store/actions/LoanActions');
jest.mock('../../../../store/actions/DocumentActions');
// jest.mock('../../../../store/actions/DocumentUploadActions');
jest.mock('../../../../services/auth/Auth');


let uploadPercent = 0;

beforeEach(() => {

    // @ts-ignore
    delete window.location;
    // @ts-ignore
    window.location = new URL('http://localhost/');


    MockEnvConfig();
    MockLocalStorage();

    FileUpload.isTypeAllowed = jest.fn(() => Promise.resolve(true));
    FileUpload.isSizeAllowed = jest.fn(() => true);

    const submitDocuments = (currentSelected: DocumentRequest, file: Document, dispatchProgress: Function, loanApplicationId: string) => {

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

    DocumentUploadActions.submitDocuments = jest.fn((a, b, c, d) => Promise.resolve(submitDocuments(a, b, c, d)))
});

const createMockFile = (name, size, mimeType) => {
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
                <App />
            </MemoryRouter>
        );

        await waitForDomChange();

        FileUpload.isTypeAllowed = jest.fn(() => Promise.resolve(true));
        FileUpload.isSizeAllowed = jest.fn(() => true);

        const getStartedBtn = getByTestId('get-started');

        fireEvent.click(getStartedBtn);

        const input = getByTestId('file-input');
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
                <App />
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');

            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('file-input');
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
                <App />
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');

            fireEvent.click(getStartedBtn);
        })


        await waitFor(() => {
            const input = getByTestId('file-input');
            const file = createMockFile('sample.pdf', 30000, 'application/pdf');
            fireEvent.change(input, { target: { files: [file] } });
        })

        await waitFor(() => {
            const editBtn: any = getByTestId('file-edit-btn-1');
            fireEvent.click(editBtn);
            expect(getByTestId('file-item-rename-input')).toBeInTheDocument();
        })

    });

    test('Should remove the file on cross icon click" ', async () => {

        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <App />
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');

            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('file-input');
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
                <App />
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');

            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('file-input');
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
                <App />
            </MemoryRouter>
        );

        FileUpload.isSizeAllowed = jest.fn(() => false);


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('file-input');
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
                <App />
            </MemoryRouter>
        );

        FileUpload.isTypeAllowed = jest.fn(() => Promise.resolve(false));


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('file-input');
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
                <App />
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('file-input');
        const file = createMockFile('sample.pdf', 110000, 'application/pdf');
        fireEvent.change(input, { target: { files: [file] } });

        const taskListContainer = getByTestId('task-list');
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
                <App />
            </MemoryRouter>
        );

        uploadPercent = 0.8;
        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('file-input');
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
                <App />
            </MemoryRouter>
        );


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('file-input');
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
                <App />
            </MemoryRouter>
        );


        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');
            fireEvent.click(getStartedBtn);
        })

        const input = getByTestId('file-input');
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
                <App />
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
                <App />
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
                <App />
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

