import React, { useState } from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { UserActions } from '../../../Store/actions/UserActions';
import App from '../../../App';
import { StoreProvider } from '../../../Store/Store';
import { AssignedRoleActions } from '../../../Store/actions/AssignedRoleActions';
import { LocalDB } from '../../../Utils/LocalDB';


import Nothing from '../../Shared/Nothing';
import { ColabaStatus } from './ColabaStatus'
import { ColabaStatusEmailTemplate } from './ColabaStatusEmailTemplate'

import ContentBody from '../../Shared/ContentBody';
import { LoanStatusUpdateBody } from './LoanStatusUpdateBody';
import DisabledWidget from '../../Shared/DisabledWidget';
import LoanStatusUpdate from '../LoanStatusUpdate';


jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NotificationActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Store/actions/LoanStatusUpdateActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/LoanStatusUpdate');
});

describe('Loan Status Update : Body', () => {

    test('Loan Status Update Body : Render', async () => {
        const { getByText, getByTestId, debug } = render(<StoreProvider><LoanStatusUpdate/></StoreProvider>);
        //debug();  
        expect(getByTestId('loader')).toBeTruthy();
        await waitFor(()=>{
            expect(getByTestId('LoanStatusUpdateBodyLeft')).toBeTruthy();
            expect(getByTestId('LoanStatusUpdateBodyRight')).toBeTruthy();
        });        
    });

})