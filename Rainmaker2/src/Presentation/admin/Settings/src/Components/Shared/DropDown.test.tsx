import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
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

it('DropDown', async()=>{
    const props = {
        listData: [],
        editable: true??false,
        selectedValue: [],
        disabled: true??false,
        handlerSelect: ()=>{},
        handlerInput: ()=>{},
        maxLength: 2,
        inputType:'text',
    }
    const {getByTestId,debug} = render(<DropDown {...props}/>);
    expect(getByTestId('dropDown')).toBeTruthy();
});
