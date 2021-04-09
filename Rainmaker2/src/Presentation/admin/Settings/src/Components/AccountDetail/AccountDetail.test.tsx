import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { debug } from 'console';
import AccountDetail from './AccountDetail';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

describe('AccountDetail', ()=>{

    test('AccountDetail : Render', ()=>{
        const {getByTestId} = render(<AccountDetail/>);
        expect(getByTestId('AccountDetail')).toBeTruthy();
    });



})