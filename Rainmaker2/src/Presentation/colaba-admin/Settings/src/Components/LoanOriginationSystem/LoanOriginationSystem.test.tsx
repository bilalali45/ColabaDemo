import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { UserActions } from '../../Store/actions/UserActions';
import { NotificationActions } from '../../Store/actions/NotificationActions';
import App from '../../App';
import { StoreProvider } from '../../Store/Store';
import { LoanOriginationSystemHeader } from './_LoanOriginationSystem/LoanOriginationSystemHeader';





// jest.mock('axios');
// jest.mock('../../Store/actions/UserActions');
// jest.mock('../../Store/actions/LoanOfficersActions');

// describe('Loan origination System',()=>{
//     test('should render Only loan origination system', async () => {
//         const { getByTestId } = render(
//            <MemoryRouter initialEntries={['/LoanOriginationSystem']}>
//               <LoanOriginationSystemHeader/>
//             </MemoryRouter>
//         );
  
//         const loanoriginationsystemHeader = getByTestId('header-title-text');
//         expect(loanoriginationsystemHeader).toHaveTextContent('BYTE SOFTWARE INTEGRATION SETTING');
//     });

// })