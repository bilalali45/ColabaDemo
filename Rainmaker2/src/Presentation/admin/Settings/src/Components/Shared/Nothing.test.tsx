import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import App from '../../App';
import Nothing from './Nothing';
import { SVGInfo } from './SVG';

beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

it('Nothing', async()=>{
    const props = {heading:'',text:''}
    render(<Nothing {...props} />);
    expect(screen.getByTestId('new-template-container')).toBeTruthy();
})