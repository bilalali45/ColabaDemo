import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock'
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import App from '../../App';
import { RequestEmailTemplateActions } from '../../Store/actions/RequestEmailTemplateActions';
import ReminderControl from './ReminderControl';
import { debug } from 'console';
import { DropDown } from './DropDown';
import { StoreProvider } from '../../Store/Store';



jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/TemplateActions');
jest.mock('../../Store/actions/RequestEmailTemplateActions');
jest.mock('../../Utils/LocalDB');

const props = {
    number: '10',
    days: '02',
    time: '00:00',
    timeType: 'PM',
    makeEnabled: true,
    addNewReminder: true,
    handlerAddNewReminder: ()=>{},
    handlerDays: ()=>{},
    handlerTime: ()=>{},
    handlerTimeType: ()=>{},
    handlerEnabled: ()=>{},
    handlerDelete: ()=>{},
    handlerInput: ()=>{}
}

const DropDownProps = {
    listData: [
        { text: '02', value: '02' },
        { text: '04', value: '04' },
        { text: '06', value: '06' },
        { text: '08', value: '08' },
        { text: '10', value: '10' }
    ],
    editable: false,
    selectedValue: [{ text: '02', value: '02' }],
    disabled: false,
    handlerSelect: ()=>{},
    handlerInput:()=>{},
    maxLength:2,
    inputType:'text',
}

describe('ReminderControl', ()=>{
    test('ReminderControl : Render & Show Mega Menu', async()=>{
        render(<StoreProvider><ReminderControl {...props} /></StoreProvider>);
        expect(screen.getByRole('button')).toBeInTheDocument();
        debug();

        fireEvent.click(screen.getByTestId('toggle-drpdwn-btn'));

        await waitFor(()=>{
            expect(screen.getByTestId('item-control')).toHaveTextContent('Send email');
            expect(screen.getByTestId('item-control')).toHaveTextContent('Days after request at');
            expect(screen.getByTestId('item-control')).toHaveTextContent('AM');
            expect(screen.getByTestId('item-control')).toHaveTextContent('PM');   
            
            expect(screen.getByTestId('toggle-drpdwn-btn')).toHaveTextContent('02');
        });

    });

    test('ReminderControl : Render & Option Menu', async()=>{
        render(<StoreProvider><ReminderControl {...props} /></StoreProvider>);
        expect(screen.getByRole('button')).toBeInTheDocument();
        debug();

        fireEvent.click(screen.getByTestId('reminderControlBtn'));

        await waitFor(()=>{
            expect(screen.getByTestId('reminderControlBtnDropDown')).toHaveTextContent('Disable/Enable');
            expect(screen.getByTestId('reminderControlBtnDropDown')).toHaveTextContent('Delete');
        })

    });

    test('ReminderControl : Text Check', async()=>{

        render(<StoreProvider><ReminderControl {...props} /></StoreProvider>);        
        expect(screen.getByTestId('reminderControl')).toHaveTextContent('Send email');
        expect(screen.getByTestId('reminderControl')).toHaveTextContent('Days after request at');
    })
})