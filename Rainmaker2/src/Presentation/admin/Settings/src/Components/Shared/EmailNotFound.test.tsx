import React, { FunctionComponent, useState, useEffect, useRef } from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { debug } from 'console';
import EmailNotFound from './EmailNotFound';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

it('EmailNotFound', async () => {
    const props = {
        heading: '',
        text: ''
    }
    const { getByTestId, debug } = render(<EmailNotFound {...props} />);
    expect(getByTestId('new-template-container')).toBeTruthy();
});
