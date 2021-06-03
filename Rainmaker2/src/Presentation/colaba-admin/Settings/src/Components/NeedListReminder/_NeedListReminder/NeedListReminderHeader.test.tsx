import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, getByText, screen, getByTestId } from '@testing-library/react';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { StoreProvider } from '../../../Store/Store';
import NeedListReminder from '../NeedListReminder';
import {NeedListReminderHeader} from '../../NeedListReminder/_NeedListReminder/NeedListReminderHeader';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/ReminderEmailsActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});



describe('Need List Reminder Emails', () => {

    it('Check Need List Reminder Header label', async()=> {
        const { getByTestId, getAllByTestId } = render(
       <StoreProvider>
            <NeedListReminderHeader />
       </StoreProvider> 
        ); 
       
            const needListHeaderLabel = getByTestId('header-title-text');
            const togglerLabel = getByTestId('toggler-label');
            const toggler = getByTestId('togglerDefault');
            expect(needListHeaderLabel).toHaveTextContent('NEEDS LIST REMINDER EMAILS'); 
            expect(togglerLabel).toHaveTextContent('Disable/Enable'); 
            expect(toggler).toBeInTheDocument();                 
    });

    test('Check Need List Reminder Header Enable Disable toggle', async()=> {
        const { getByTestId, getAllByTestId } = render(
       <StoreProvider>
            <NeedListReminder />
            <NeedListReminderHeader />
       </StoreProvider> 
        ); 

      await waitFor(() => {
            const needListHeaderLabel = getAllByTestId('header-title-text');
            const togglerLabel = getAllByTestId('toggler-label');
            const toggler = getAllByTestId('togglerDefault');
            expect(needListHeaderLabel[0]).toHaveTextContent('NEEDS LIST REMINDER EMAILS'); 
            expect(togglerLabel[0]).toHaveTextContent('Disable/Enable'); 
            expect(toggler[0]).toBeInTheDocument();
            expect(toggler[0]).toHaveClass('settings__switch  active');  
        });
                                                             
    });

});