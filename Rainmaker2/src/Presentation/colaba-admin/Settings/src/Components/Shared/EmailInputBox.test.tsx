import React, { FunctionComponent, useState, useEffect, useRef } from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { debug } from 'console';
import { EmailInputBox } from './EmailInputBox';
import { ReactMultiEmail } from 'react-multi-email';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

it('EmailInputBox', async () => {
    const props = {
        handlerEmail: () => { },
        tokens: [],
        handlerClick: () => { },
        exisitngEmailValues: [] ?? null,
        className: '',
        dataTestId: '',
        id: '',
        setInputError: () => { },
        triggerInputValidation: () => { },
        clearInputError: () => { },
    }
    const { getByTestId, debug } = render(<EmailInputBox {...props} />);
    expect(getByTestId('EmailInputBox')).toBeTruthy();
    
    //const {getByTestId} = render(<ReactMultiEmail/>);
    expect(getByTestId('EmailInputBox_Input')).toBeTruthy();
    fireEvent.keyDown(getByTestId('EmailInputBox_Input'));

    expect(getByTestId('EmailInputBox_Input')).not.toHaveTextContent('###RequestorUserEmail###');

    fireEvent.change(getByTestId('EmailInputBox_Input'));
    fireEvent.keyUp(getByTestId('EmailInputBox_Input'))

    // fireEvent.click(getByTestId('EmailInputBox_TagCross'));
    
    // debug()
});
