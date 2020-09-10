import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor, waitForElement } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom'
import { createMemoryHistory } from 'history'
import { MockEnvConfig } from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import App from '../../../../App';
import { DocumentActions } from '../../../../store/actions/DocumentActions';


jest.mock('axios');
jest.mock('../../../../store/actions/UserActions');
jest.mock('../../../../store/actions/LoanActions');
jest.mock('../../../../store/actions/DocumentActions');
jest.mock('../../../../services/auth/Auth');

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    const history = createMemoryHistory()
    history.push('/');
});

describe('Documents Status', () => {
    test('should render DocumentsStatus" ', async () => {
        const { getByTestId } = render(<App />, { wrapper: MemoryRouter });

        await waitForDomChange();
        
        const pendingDocs = getByTestId('borrower-pending-docs');
        
        expect(pendingDocs).toHaveTextContent('You have 3 items to complete');
        
    });
    
    test('should show the list of pending documents" ', async () => {
        const { getByTestId, getAllByTestId } = render(<App />, { wrapper: MemoryRouter });

        await waitForDomChange();
        
        const pendingDocs = getAllByTestId('borrower-pending-doc');

        expect(pendingDocs[0]).toHaveTextContent('Alimony Income Verification');
        expect(pendingDocs[1]).toHaveTextContent('Bank Statement');
        expect(pendingDocs[2]).toHaveTextContent('Salary Slip');
        
    });
    
})