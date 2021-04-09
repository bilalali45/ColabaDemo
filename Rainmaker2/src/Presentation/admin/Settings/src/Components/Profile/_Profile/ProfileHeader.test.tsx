import React, {useEffect, useState} from 'react';
import { Role } from '../../../Store/Navigation';
import { ProfileMenu } from '../../../Utils/helpers/Enums';
import { LocalDB } from '../../../Utils/LocalDB';
import ContentHeader, { ContentSubHeader } from '../../Shared/ContentHeader';

import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import AssignRole from '../../AssignRole/AssignRole';
import { AssignedRoleActions } from '../../../Store/actions/AssignedRoleActions';
import { StoreProvider } from '../../../Store/Store';
import { UserActions } from '../../../Store/actions/UserActions';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';

import ProfileHeader from './ProfileHeader';

beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Setting/Profile');
});

describe('Profile Header', () => {
    it('Profile Header : Render', async()=>{

        const props = {
            navigation : 1,
            changeNav : jest.fn(),
            backHandler : jest.fn()
        }

        const {getByTestId, getAllByTestId, debug} = render(<StoreProvider><ProfileHeader {...props} ><ContentHeader title={'Manage Users'} className="profile-header"></ContentHeader></ProfileHeader></StoreProvider>);
        expect(getByTestId('ProfileHeader')).toBeTruthy();
        expect(getAllByTestId('profile-menu')).toBeTruthy();

        // const link = getAllByTestId('profile-menu')[0];
        fireEvent.click(getByTestId('profile-menu-link1'));
        debug()
        await waitFor(()=>{
            fireEvent.click(getByTestId('profile-menu-link2'));  
        })
    });
})