import React from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { UserActions } from '../../Store/actions/UserActions';
import App from '../../App';
import { StoreProvider } from '../../Store/Store';
import { AssignedRoleActions } from '../../Store/actions/AssignedRoleActions';
import { LocalDB } from '../../Utils/LocalDB';
import { Profile } from './Profile';
import _ProfileHeader from './_Profile/ProfileHeader'


jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Utils/LocalDB');



beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/Notification');
});

describe('Profile Header', () => {
    test('Should render "Your Profile" text ', () => {
        const { getByTestId, getAllByTestId } = render(           
                <Profile/>
                
             );

             const profileHeader = getByTestId('notification-header');
             expect(profileHeader).toHaveTextContent('Your Profile');
    });

    test('Should render "Account Detail", "Assigne Role", "Notification" text on Header ', () => {
        const { getByTestId, getAllByTestId } = render(           
                <Profile/>
                
             );

             const profileHeaderMenu = getAllByTestId('profile-menu');
             
             expect(profileHeaderMenu[0]).toHaveTextContent('Account Detail');
             expect(profileHeaderMenu[1]).toHaveTextContent('Assigned Role');
             expect(profileHeaderMenu[2]).toHaveTextContent('Notification');
    })

    test('Should move to next tab on clicking on tab', async () => {
        const { getByTestId, getAllByTestId } = render(           
                <Profile/>
                
             );

             const profileHeaderMenu = getAllByTestId('profile-menu');
             
             expect(profileHeaderMenu[0]).toHaveClass('active');
             fireEvent.click(profileHeaderMenu[1].children[0]);
              
             await waitFor(() => {
                expect(profileHeaderMenu[1]).toHaveClass('active');
                expect(profileHeaderMenu[0]).not.toHaveClass('active');
             })
    });

});
