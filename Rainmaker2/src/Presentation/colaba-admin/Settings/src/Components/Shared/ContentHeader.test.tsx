import React, {FunctionComponent, useState, useEffect, useRef} from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElement, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import ContentHeader, { ContentSubHeader } from './ContentHeader';
import { debug } from 'console';
import { StoreProvider } from '../../Store/Store';


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

describe('ContentHeader and ContentSubHeader', ()=>{

    test('ContentHeader', async()=>{
        const props = {
            title:'',
            tooltipType:0,
            className:'',
            backLinkText: '',
            backLink: ()=>{}
        }
        const {getByTestId,debug} = render(<StoreProvider><ContentHeader {...props}/></StoreProvider>);
        expect(getByTestId('contentHeader')).toBeTruthy();



        // const resolvedEl = getByTestId('contentHeader').find('contentHeader-backBtn');
        // expect(resolvedEl).toBe('Back');

    });
    
    test('ContentSubHeader', async()=>{
        const props = {
            title:'',
            tooltipType:0,
            className:'',
            backLinkText: '',
            backLink: ()=>{}
        }
        const {getByTestId,debug} = render(<StoreProvider><ContentSubHeader {...props}/></StoreProvider>);
        expect(getByTestId('contentSubHeader')).toBeTruthy();
    });

})
