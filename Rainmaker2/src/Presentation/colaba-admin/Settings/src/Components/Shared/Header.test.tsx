import React, { FunctionComponent, useState, useEffect, useRef } from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { debug } from 'console';
import {Header} from './Header';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

it('Header', async () => {
    const { getByTestId, debug } = render(<Header/>);
    expect(getByTestId('main-header')).toBeTruthy();
});
