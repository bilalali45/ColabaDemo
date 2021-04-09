import React, { useContext, useEffect, useState } from 'react'
import Notificaiton from '../../../Entities/Models/Notification';
import { NotificationActions } from '../../../Store/actions/NotificationActions';
import { NotificationActionsType } from '../../../Store/reducers/NotificationReducer';
import { Store, StoreProvider } from '../../../Store/Store';
import ContentBody from '../../Shared/ContentBody';
import InfoDisplay from '../../Shared/InfoDisplay';
import Loader, {WidgetLoader} from '../../Shared/Loader';
import Table from '../../Shared/SettingTable/Table';
import TableROW from '../../Shared/SettingTable/TableROW';
import TableTD from '../../Shared/SettingTable/TableTD';
import TableTH from '../../Shared/SettingTable/TableTH';

import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { UserActions } from '../../../Store/actions/UserActions';
import { NotificationBody } from './NotificationBody';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NotificationActions');

beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    // const history = createMemoryHistory();
    // history.push('/Notification');
});

describe('NotificationBody', () => {
    
    test('NotificationBody : Render', async()=>{
        const { getByTestId, getAllByTestId, debug } = render(<StoreProvider><NotificationBody/></StoreProvider>);
        
        await waitFor(()=>{
            expect(getByTestId('NotificationBody')).toBeTruthy();  
            const select = getAllByTestId('input-select');
            expect(select[0]).toBeTruthy();
            fireEvent.click(select[0]);
        });

        const radio = getAllByTestId('input-check-3');

        expect(radio[0]).toBeTruthy();
        fireEvent.click(radio[0]);
        fireEvent.click(radio[1]);
        fireEvent.click(radio[2]);
        
        // 

        // await waitFor(()=>{
            
        // })
        debug();

    });

})