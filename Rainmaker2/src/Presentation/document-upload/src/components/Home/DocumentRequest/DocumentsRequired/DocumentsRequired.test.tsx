import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { createMemoryHistory } from 'history'
import testfile from '../../../test_utilities/images/test-input-file.png'
import { MockEnvConfig } from '../../../../services/test_helpers/EnvConfigMock';
import { MockLocalStorage } from '../../../../services/test_helpers/LocalStorageMock';
import App from '../../../../App';

jest.mock('axios');
jest.mock('../../../../store/actions/UserActions');
jest.mock('../../../../store/actions/LoanActions');
jest.mock('../../../../store/actions/DocumentActions');
jest.mock('../../../../services/auth/Auth');

beforeEach(() => {
    // MockEnvConfig();
    // MockLocalStorage();
});

describe('Document Request File Upload', () => {
    test('Should change the current document" ', async () => {
        const { getByTestId, getAllByTestId } = render(
            <MemoryRouter initialEntries={['/loanportal/activity/3']}>
                <App />
            </MemoryRouter>
        );

        await waitFor(() => {
            const getStartedBtn = getByTestId('get-started');

            fireEvent.click(getStartedBtn);
        });

        await waitFor(() => {

            fireEvent.click(getByTestId('pending-doc-1'));
            
            const selectedDocTitle = getByTestId('selected-doc-title');
            expect(selectedDocTitle).toHaveTextContent('Bank Statement');
        })
       
        await waitFor(() => {

            fireEvent.click(getByTestId('pending-doc-0'));
            
            const selectedDocTitle = getByTestId('selected-doc-title');
            expect(selectedDocTitle).toHaveTextContent('Alimony Income Verification');
        })
        
    });



})