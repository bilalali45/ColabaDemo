import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { createMemoryHistory } from 'history'
import { MockEnvConfig } from '../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../test_utilities/LocalStoreMock';
import testfile from '../../../test_utilities/images/test-input-file.png'
import App from '../../../App';
import { FileUpload } from '../../../utils/helpers/FileUpload';

jest.mock('axios');
jest.mock('../../../store/actions/UserActions');
jest.mock('../../../store/actions/LoanActions');
jest.mock('../../../store/actions/DocumentActions');
jest.mock('../../../services/auth/Auth');

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
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
    test('Should add a new file into list" ', async (done) => {
        await act(async () => {
            const { getByTestId, getAllByTestId } = render(
                <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                    <App />
                </MemoryRouter>
            );

            await waitForDomChange();

            FileUpload.isFileAllowed = jest.fn(() => Promise.resolve(true));
            FileUpload.isTypeAllowed = jest.fn(() => Promise.resolve(true));
            FileUpload.isSizeAllowed = jest.fn(() => true);

            const getStartedBtn = getByTestId('get-started');

            fireEvent.click(getStartedBtn);

            const input = getByTestId('file-input');
                
            const file = createMockFile('test.jpg', 30000, 'image/jpeg');
            fireEvent.change(input, { target: { files: [file] } });
            await waitForDomChange();

            const renameInput: any = getByTestId('file-item-rename-input');
            fireEvent.blur(renameInput);
            const files = getAllByTestId('file-item');
            expect(files).toHaveLength(2);
            // expect(renameInput).not.toBeInTheDocument();


            expect(files[1]).toHaveTextContent('test.jpeg');


            done();
        })


    });



})