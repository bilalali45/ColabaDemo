import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import TableTH from './TableTH';
import { debug } from 'console';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

describe('TableTH', ()=>{
    test('TableTH : Render',async()=>{
        render(<TableTH/>);
        expect(screen.getByTestId('TableTH')).toBeTruthy();
    });

    test('TableTH : Valign - Top && Align - Left',async()=>{
        const props = {
            valign:'top',
            align:'left',
            className:''
        }
        const {getByTestId} = render(<TableTH {...props}/>);
        expect(getByTestId('TableTH')).toBeTruthy();
    });

    test('TableTH : Valign - Top && Align - Center',async()=>{
        const props = {
            valign:'top',
            align:'center',
            className:''
        }
        const {getByTestId} = render(<TableTH {...props}/>);
        expect(getByTestId('TableTH')).toBeTruthy();
    });

    test('TableTH : Valign - Top && Align - Right',async()=>{
        const props = {
            valign:'top',
            align:'right',
            className:''
        }
        const {getByTestId} = render(<TableTH {...props}/>);
        expect(getByTestId('TableTH')).toBeTruthy();
    });

    test('TableTH : Valign - Middle && Align - Left',async()=>{
        const props = {
            valign:'middle',
            align:'left',
            className:''
        }
        const {getByTestId} = render(<TableTH {...props}/>);
        expect(getByTestId('TableTH')).toBeTruthy();
    });

    test('TableTH : Valign - Middle && Align - Center',async()=>{
        const props = {
            valign:'middle',
            align:'center',
            className:''
        }
        const {getByTestId} = render(<TableTH {...props}/>);
        expect(getByTestId('TableTH')).toBeTruthy();
    });

    test('TableTH : Valign - Middle && Align - Right',async()=>{
        const props = {
            valign:'middle',
            align:'right',
            className:''
        }
        const {getByTestId} = render(<TableTH {...props}/>);
        expect(getByTestId('TableTH')).toBeTruthy();
    });

    test('TableTH : Valign - Bottom && Align - Left',async()=>{
        const props = {
            valign:'bottom',
            align:'left',
            className:''
        }
        const {getByTestId} = render(<TableTH {...props}/>);
        expect(getByTestId('TableTH')).toBeTruthy();
    });

    test('TableTH : Valign - Bottom && Align - Center',async()=>{
        const props = {
            valign:'bottom',
            align:'center',
            className:''
        }
        const {getByTestId} = render(<TableTH {...props}/>);
        expect(getByTestId('TableTH')).toBeTruthy();
    });

    test('TableTH : Valign - Bottom && Align - Right',async()=>{
        const props = {
            valign:'bottom',
            align:'right',
            className:''
        }
        const {getByTestId} = render(<TableTH {...props}/>);
        expect(getByTestId('TableTH')).toBeTruthy();
    });
});
