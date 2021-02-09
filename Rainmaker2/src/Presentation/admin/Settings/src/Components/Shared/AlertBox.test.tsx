import React, { FunctionComponent, useState, useEffect, useRef } from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { AlertBox } from './AlertBox';
import { debug } from 'console';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/');
});

describe('AlertBox', ()=>{
    
    test('AlertBox : Render & Yes', async () => {
        const props = {
            hideAlert: ()=>{},
            setshowAlert: ()=>{},
            navigateUrl: ''
        }
        const { getByTestId, debug } = render(<AlertBox {...props} />);
        expect(getByTestId('alert-box')).toBeTruthy();
    
        await waitFor(()=>{
            fireEvent.click(getByTestId('btnyes'));
        })
    });

    test('AlertBox : Render & No', async () => {
        const props = {
            hideAlert: ()=>{},
            setshowAlert: ()=>{},
            navigateUrl: ''
        }
        const { getByTestId, debug } = render(<AlertBox {...props} />);
        expect(getByTestId('alert-box')).toBeTruthy();
    
        await waitFor(()=>{
            fireEvent.click(getByTestId('btnno'));
        })
    });
})


