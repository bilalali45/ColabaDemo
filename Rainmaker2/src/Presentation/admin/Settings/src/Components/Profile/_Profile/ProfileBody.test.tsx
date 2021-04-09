import React from 'react'
import AccountDetail from '../../AccountDetail/AccountDetail';
import  AssignRole  from '../../AssignRole/AssignRole';
import {Notification} from '../../Notification/Notification';
import ContentBody from '../../Shared/ContentBody';

import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { AssignedRoleActions } from '../../../Store/actions/AssignedRoleActions';
import { StoreProvider } from '../../../Store/Store';
import { UserActions } from '../../../Store/actions/UserActions';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { ProfileBody } from './ProfileBody';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Setting/Profile');
});

describe('Profile Body', () => {
    it('Profile Body : Render', async()=>{
        const {getByTestId} = render(<StoreProvider><ProfileBody/></StoreProvider>);
        expect(getByTestId('ProfileBody')).toBeTruthy();
    });
})

