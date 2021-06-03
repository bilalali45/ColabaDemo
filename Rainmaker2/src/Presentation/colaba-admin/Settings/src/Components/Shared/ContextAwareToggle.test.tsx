import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import ContextAwareToggle from './ContextAwareToggle';

beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

it('ContextAwareToggle', async()=>{
    const {getByTestId,debug} = render(<ContextAwareToggle/>);
    expect(getByTestId('settings__accordion-signable-header')).toBeTruthy();
    expect(getByTestId('settings__accordion-signable-toggle-btn')).toBeTruthy();

    fireEvent.click(getByTestId('settings__accordion-signable-toggle-btn'));
});
