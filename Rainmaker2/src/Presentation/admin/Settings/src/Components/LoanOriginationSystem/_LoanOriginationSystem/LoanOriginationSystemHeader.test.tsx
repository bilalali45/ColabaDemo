import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { UserActions } from '../../../Store/actions/UserActions';
import App from '../../../App';
import { StoreProvider } from '../../../Store/Store';
import { LoanOriginationSystemHeader } from './LoanOriginationSystemHeader';


jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/LoanOfficersActions');

describe('Loan origination system header',()=>{
    test('should render only users header', async () => {
        const { getByTestId } = render(
           <MemoryRouter initialEntries={['/LoanOriginationSystem']}>
              <LoanOriginationSystemHeader/>
            </MemoryRouter>
        );
  
        const loanoriginationsystemHeader = getByTestId('header-title-text');
        expect(loanoriginationsystemHeader).toHaveTextContent('Byte Software Integration Setting');
    });

})