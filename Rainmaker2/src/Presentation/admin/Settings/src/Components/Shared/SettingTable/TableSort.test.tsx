import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen, getByTestId } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import TableSort from './TableSort';
import { debug } from 'console';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

describe('TableSort', ()=>{

    it('TableSort : Render', async()=>{
        const props = {order: null,
        className:"",
        callBackFunction: ()=>{}};
        render(<TableSort {...props}/>);
        expect(screen.getByTestId('TableSort')).toBeTruthy();
        fireEvent.click(screen.getByTestId('TableSort'));
    
    });

    it('TableSort : Up', async()=>{
        const props = {order: 1,
        className:"",
        callBackFunction: ()=>{}};
        render(<TableSort {...props}/>);
        expect(screen.getByTestId('TableSort')).toBeTruthy();
        fireEvent.click(screen.getByTestId('TableSort'));
        
        await waitFor(()=>{
            expect(screen.getByTestId('sortDes')).toBeTruthy();
        })
    });

    it('TableSort : Down', async()=>{
        const props = {order: 2,
        className:"",
        callBackFunction: ()=>{}};
        render(<TableSort {...props}/>);
        expect(screen.getByTestId('TableSort')).toBeTruthy();
        fireEvent.click(screen.getByTestId('TableSort'));
        await waitFor(()=>{
            expect(screen.getByTestId('sortAsc')).toBeTruthy();
        });        
    });


})
