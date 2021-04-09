import React from 'react'
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { StoreProvider } from '../../../Store/Store';

import {UserProfileListBody} from './UserProfileListBody';

describe('UserProfileListBody', ()=>{

    test('UserProfileListBody : Render', async()=>{
        const props = {backHandler:jest.fn()}
        const {getByTestId, getAllByTestId} = render(<StoreProvider><UserProfileListBody {...props}/></StoreProvider>);
        expect(getByTestId('UserProfileListBody')).toBeTruthy();

        fireEvent.click(getAllByTestId('UserProfileListBody_TableRow')[0]);

        await waitFor(()=>{
            fireEvent.click(getAllByTestId('UserProfileListBody_TableRow')[1]);
        })

        
        fireEvent.click(getAllByTestId('UserProfileListBody_TableRow')[2]);
        fireEvent.click(getAllByTestId('UserProfileListBody_TableRow')[3]);
        
        await waitFor(()=>{
            fireEvent.click(getAllByTestId('UserProfileListBody_TableRow')[4]);
            fireEvent.click(getAllByTestId('UserProfileListBody_TableRow')[5]);
        })
    });

});