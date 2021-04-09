import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen, getAllByTestId } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import {DropDown} from './DropDown';
import { debug } from 'console';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

describe('DropDown', ()=>{
    it('DropDown Editable By Input : Click', async()=>{
        const props = {
            listData: [
                { text: '02', value: '02' },
                { text: '04', value: '04' },
                { text: '06', value: '06' },
                { text: '08', value: '08' },
                { text: '10', value: '10' }
            ],
            editable: true,
            selectedValue: [{ text: '02', value: '02' }],
            disabled: false,
            handlerSelect: ()=>{},
            handlerInput: ()=>{},
            maxLength: 2,
            inputType:'text',
        }
        const {getByTestId,debug} = render(<DropDown {...props}/>);
        expect(getByTestId('dropDown')).toBeTruthy();

        // For Input Edit Value
        expect(getByTestId('settings__dropdown-text')).toBeTruthy();
        fireEvent.click(getByTestId('settings__dropdown-text'));        
    });

    it('DropDown Editable By Input : KeyUp', async()=>{
        const props = {
            listData: [
                { text: '02', value: '02' },
                { text: '04', value: '04' },
                { text: '06', value: '06' },
                { text: '08', value: '08' },
                { text: '10', value: '10' }
            ],
            editable: true,
            selectedValue: [{ text: '02', value: '02' }],
            disabled: false,
            handlerSelect: ()=>{},
            handlerInput: ()=>{},
            maxLength: 2,
            inputType:'text',
        }
        const {getByTestId,debug} = render(<DropDown {...props}/>);
        expect(getByTestId('dropDown')).toBeTruthy();

        // For Event
        fireEvent.keyUp(getByTestId('settings__dropdown-text'));
    });

    it('DropDown Editable By Input : Change', async()=>{
        const props = {
            listData: [
                { text: '02', value: '02' },
                { text: '04', value: '04' },
                { text: '06', value: '06' },
                { text: '08', value: '08' },
                { text: '10', value: '10' }
            ],
            editable: true,
            selectedValue: [{ text: '02', value: '02' }],
            disabled: false,
            handlerSelect: ()=>{},
            handlerInput: ()=>{},
            maxLength: 2,
            inputType:'text',
        }
        const {getByTestId,debug} = render(<DropDown {...props}/>);
        expect(getByTestId('dropDown')).toBeTruthy();

        // For Event
        expect(getByTestId('settings__dropdown-text')).toBeTruthy();
        fireEvent.change(getByTestId('settings__dropdown-text'));       
    });

    it('DropDown Editable -> Select By Drop Value : Render', async()=>{
        const props = {
            listData: [
                { text: '02', value: '02' },
                { text: '04', value: '04' },
                { text: '06', value: '06' },
                { text: '08', value: '08' },
                { text: '10', value: '10' }
            ],
            editable: true,
            selectedValue: [{ text: '02', value: '02' }],
            disabled: false,
            handlerSelect: ()=>{},
            handlerInput: ()=>{},
            maxLength: 2,
            inputType:'text',
        }
        const {getByTestId,getByText,debug} = render(<DropDown {...props}/>);
        expect(getByTestId('dropDown')).toBeTruthy();

        // For Select Value
        expect(getByTestId('settings__dropdown-arrow')).toBeTruthy();
        fireEvent.click(getByTestId('settings__dropdown-arrow'));
        
        await waitFor(()=>{
            expect(getByTestId('dropDownMenu')).toBeTruthy();
            fireEvent.click(getByText('04'));
        });

    });

    it('DropDown Editable By Input and Disabled', async()=>{
        const props = {
            listData: [
                { text: '02', value: '02' },
                { text: '04', value: '04' },
                { text: '06', value: '06' },
                { text: '08', value: '08' },
                { text: '10', value: '10' }
            ],
            editable: true,
            selectedValue: [{ text: '02', value: '02' }],
            disabled: true,
            handlerSelect: ()=>{},
            handlerInput: ()=>{},
            maxLength: 2,
            inputType:'text',
        }
        const {getByTestId,debug} = render(<DropDown {...props}/>);
        expect(getByTestId('dropDown')).toBeTruthy();
        expect(getByTestId('dropDown')).toHaveClass('disabled');
    });

    /////////////////////////////
    /////////////////////////////

    it('DropDown Not Editable : Render', async()=>{
        const props = {
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
            handlerInput: ()=>{},
            maxLength: 2,
            inputType:'text',
        }
        const {getByTestId,getByText,debug} = render(<DropDown {...props}/>);
        expect(getByTestId('dropDown')).toBeTruthy();
        expect(getByTestId('settings__dropdown-text')).toBeTruthy();
        expect(getByTestId('settings__dropdown-arrow')).toBeTruthy();
        fireEvent.click(getByTestId('settings__dropdown-arrow'));
        
        await waitFor(()=>{
            expect(getByTestId('dropDownMenu')).toBeTruthy();
            fireEvent.click(getByText('04'));
        });

    });

    it('DropDown Editable By Input and Disabled', async()=>{
        const props = {
            listData: [
                { text: '02', value: '02' },
                { text: '04', value: '04' },
                { text: '06', value: '06' },
                { text: '08', value: '08' },
                { text: '10', value: '10' }
            ],
            editable: false,
            selectedValue: [{ text: '02', value: '02' }],
            disabled: true,
            handlerSelect: ()=>{},
            handlerInput: ()=>{},
            maxLength: 2,
            inputType:'text',
        }
        const {getByTestId,debug} = render(<DropDown {...props}/>);
        expect(getByTestId('dropDown')).toBeTruthy();
        expect(getByTestId('dropDown')).toHaveClass('disabled');
    });


})
