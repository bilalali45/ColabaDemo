import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, getByText, screen, getByTestId } from '@testing-library/react';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { StoreProvider } from '../../../Store/Store';
import NeedListReminder from '../NeedListReminder';
import {NeedListReminderHeader} from '../../NeedListReminder/_NeedListReminder/NeedListReminderHeader';
import { ReminderSettings } from './ReminderSettings';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/ReminderEmailsActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

let props = {
    setShowFooter: ()=>{},
    cancelClick: true,
    setCancelClick: ()=> {},
    enableDisableClick: ()=>{}
}

describe('Need List Reminder Emails Settings', () => {

    it('Check Need List Reminder Sub Header label', async()=> {
        const { getByTestId, getAllByTestId } = render(
       <StoreProvider>
            <ReminderSettings {...props} />
       </StoreProvider> 
        ); 
       
            const subHeader = getByTestId('contentSubHeader');         
            expect(subHeader).toHaveTextContent('Settings'); 
                          
    });
 
});