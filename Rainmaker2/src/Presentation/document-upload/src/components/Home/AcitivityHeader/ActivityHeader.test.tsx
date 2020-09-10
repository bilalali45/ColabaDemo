import React from 'react';
import { render, waitForDomChange, fireEvent, waitFor, waitForElement } from '@testing-library/react'
import App from '../../../App'
import { MemoryRouter } from 'react-router-dom'
import { MockEnvConfig } from '../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../test_utilities/LocalStoreMock';
import { createMemoryHistory } from 'history'

jest.mock('axios');
jest.mock('../../../store/actions/UserActions');
jest.mock('../../../store/actions/LoanActions');
jest.mock('../../../services/auth/Auth');

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
    const history = createMemoryHistory()
    history.push('/');
});

describe('Activity Header', () => {
    test('should render with class name "activityHeader" ', async () => {
        const { getByTestId } = render(<App />, { wrapper: MemoryRouter });

        await waitForDomChange();

        const activityHeader = getByTestId('activity-header');

        expect(activityHeader).toHaveClass('activityHeader');
    });

    test('should render navigation links', async () => {
        const { getByTestId } = render(<App />, { wrapper: MemoryRouter });

        await waitForDomChange();

        const activityHeader = getByTestId('activity-header');

        expect(activityHeader).toHaveTextContent('Dashboard');
        expect(activityHeader).toHaveTextContent('Documents');
    });

    test('should redirect to link clicked', async () => {
        const { getByTestId } = render(<App />, { wrapper: MemoryRouter });

        await waitForDomChange();

        const activityHeader = getByTestId('activity-header');
        const rightNav = getByTestId('right-nav');
        const leftNav = getByTestId('left-nav');

        fireEvent.click(rightNav);
        expect(activityHeader).toHaveTextContent('Loan Center');
      
    });
    
    
})

// console.log('=======================================', window?.location?.pathname);
// expect(activityHeader).toHaveTextContent('Document Request');
// console.log('=======================================', window?.location?.pathname);