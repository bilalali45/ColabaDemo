import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import DisabledWidget from './DisabledWidget';
import { debug } from 'console';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

it('DisabledWidget', async()=>{
    const props = {
        text:''
    }
    const {getByTestId,debug} = render(<DisabledWidget {...props}/>);
    expect(getByTestId('DisabledWidget')).toBeTruthy();
});
