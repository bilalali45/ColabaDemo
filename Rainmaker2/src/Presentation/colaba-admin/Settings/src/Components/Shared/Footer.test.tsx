import React, { FunctionComponent, useState, useEffect, useRef } from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { debug } from 'console';
import Footer from './Footer';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

it('Footer', async () => {
    const { getByTestId, debug } = render(<Footer/>);
    expect(getByTestId('footer')).toBeTruthy();
});
