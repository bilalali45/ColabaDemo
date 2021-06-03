import React from 'react'
import { ManageUsersBody } from './_ManageUsers/ManageUsersBody'
import { ManageUsersHeader } from './_ManageUsers/ManageUsersHeader'

import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import App from '../../App';
import { RequestEmailTemplateActions } from '../../Store/actions/RequestEmailTemplateActions';
import { LocalDB } from '../../Utils/LocalDB';
import { UserActions } from '../../Store/actions/UserActions';

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
    test('check wrapper div', async ()=>{
        const { getByTestId } = render(
            <MemoryRouter initialEntries={['/Setting/Profile']} >
               <App/>
            </MemoryRouter>
        );
        
        // await waitFor(() => {
        //     const mainWrapper = getByTestId('manageuser');
        //     expect(mainWrapper).toHaveClass('settings__manage-users');
        // })
        //Test setting text and sideBar menu is rendered
        
        //expect(mainHead).toHaveTextContent('Settings');
        //expect(getByTestId('sideBar')).toBeInTheDocument();
    })
});