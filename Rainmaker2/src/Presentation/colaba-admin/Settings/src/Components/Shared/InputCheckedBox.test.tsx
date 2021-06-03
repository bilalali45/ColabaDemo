import React, { FunctionComponent, useState, useEffect, useRef } from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { debug } from 'console';
import InputCheckedBox from './InputCheckedBox';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

it('InputCheckedBox', async () => {
    const props = {
        id: '',
        className: '',
        name: '',
        checked: true ?? false,
        value: '',
        testId: '',
        onchange: ()=>{}
    }
    const { getByTestId, debug } = render(<InputCheckedBox {...props} />);
    expect(getByTestId('InputCheckedBox')).toBeTruthy();
});
