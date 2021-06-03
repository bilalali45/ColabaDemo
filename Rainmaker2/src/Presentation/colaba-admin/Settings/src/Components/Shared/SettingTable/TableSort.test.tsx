import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
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

it('TableSort', async()=>{
    const props = {order: "",
    className:"",
    callBackFunction: ()=>{}};
    render(<TableSort {...props}/>);
    expect(screen.getByTestId('TableSort')).toBeTruthy();
});
