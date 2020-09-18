import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { createMemoryHistory } from 'history'

import { MockEnvConfig } from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import App from '../../../../App';
import { FileUpload } from '../../../../utils/helpers/FileUpload';

jest.mock('axios');
jest.mock('../../../../store/actions/UserActions');
jest.mock('../../../../store/actions/LoanActions');
jest.mock('../../../../store/actions/DocumentActions');
jest.mock('../../../../services/auth/Auth');

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

describe('Document Request File Upload a', () => {
    test('Should add a new file into list" ', async () => {
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
            console.log(files[1].innerHTML);
            expect(files[1]).toHaveTextContent('sample-copy-1.pdf');

        })
    });

    test('Should add a new file into list" ', async () => {
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
            console.log(files[1].innerHTML);
            expect(files[1]).toHaveTextContent('sample-copy-1.pdf');

        })
    });

    afterEach(() => {
        console.log('[[[[[[[[[[[[[[[[]]]]]]]]]]]]]]]] cleanup')
        cleanup();
    })

})
