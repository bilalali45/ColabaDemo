import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElement, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import ContentHeader, { ContentSubHeader } from './ContentHeader';
import { debug } from 'console';
import { StoreProvider } from '../../Store/Store';

// jest.mock('./ContentHeader');

beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

describe('ContentHeader and ContentSubHeader', ()=>{

    test('ContentHeader without back button', async()=>{
        const {getByTestId,debug} = render(<ContentHeader/>);
        expect(getByTestId('contentHeader')).toBeTruthy();
    });

    test('ContentHeader with back button', async()=>{
        const {getByTestId,debug} = render(<ContentHeader title="Manage Users" backLinkText="Back" className="settings__manage-users--header"></ContentHeader>);
        expect(getByTestId('contentHeader')).toBeTruthy();

        await waitFor(()=>{
            expect(getByTestId('contentHeader-backBtn')).toBeTruthy();
            fireEvent.click(getByTestId('contentHeader-backBtn'));
        })
        // debug();
    });
    
    test('ContentSubHeader without back button', async()=>{
        const {getByTestId,debug} = render(<ContentSubHeader></ContentSubHeader>);
        expect(getByTestId('contentSubHeader')).toBeTruthy();
    });

    test('ContentSubHeader with back button', async()=>{
        const {getByTestId,debug} = render(<ContentSubHeader title="Manage Users" backLinkText="Back" className="settings__content-area--subheader"></ContentSubHeader>);
        expect(getByTestId('contentSubHeader')).toBeTruthy();

        await waitFor(()=>{
            expect(getByTestId('subHeader-backBtn')).toBeTruthy();
            fireEvent.click(getByTestId('subHeader-backBtn'));
        });
    });

})
