import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { SideBar } from './SideBar';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

it('SideBar', async()=>{
    render(<SideBar/>);
    expect(screen.getByTestId('sideBar')).toBeTruthy();
})