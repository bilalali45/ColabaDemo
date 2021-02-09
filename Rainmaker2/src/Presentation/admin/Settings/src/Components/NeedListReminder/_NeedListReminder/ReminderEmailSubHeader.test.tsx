import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, getByText, screen, getByTestId } from '@testing-library/react';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { StoreProvider } from '../../../Store/Store';
import { ReminderEmailSubHeader } from './ReminderEmailSubHeader';


jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/ReminderEmailsActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});



describe('Need List Reminder Emails Content sub header', () => {

    it('Test Email Content', async()=> {
        const { getByTestId, getAllByTestId } = render(
       <StoreProvider>
           <ReminderEmailSubHeader/>
            
       </StoreProvider> 
        );

        const subHeader = getByTestId('contentSubHeader');         
        expect(subHeader).toHaveTextContent('Reminder Email ');
        
    });

});