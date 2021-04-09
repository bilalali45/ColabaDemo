import React from 'react'
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { UserActions } from '../../Store/actions/UserActions';
import { StoreProvider } from '../../Store/Store';
import ManageUsers from './ManageUsers';

UserActions
jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/TemplateActions');
jest.mock('../../Store/actions/RequestEmailTemplateActions');
jest.mock('../../Utils/LocalDB');

beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Setting/Profile');
});

describe('Manage User Test', () => {
    test('Test manage User is render', async ()=>{
        const { getByTestId } = render(
           <StoreProvider>
               <ManageUsers/>
           </StoreProvider>
        );
        
        let manageUserDiv = getByTestId('manageuser');
        expect(manageUserDiv).toBeInTheDocument();
        let header = getByTestId('contentHeader');
        expect(header).toHaveTextContent('Manage Users')
    })
});