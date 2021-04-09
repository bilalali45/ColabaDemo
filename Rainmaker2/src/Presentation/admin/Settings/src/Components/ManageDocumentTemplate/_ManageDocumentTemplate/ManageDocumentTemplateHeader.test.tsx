import React, { useContext, useEffect, useState, useRef } from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen, getByTestId } from '@testing-library/react'
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { StoreProvider } from '../../../Store/Store';
import ManageDocumentTemplateHeader from './ManageDocumentTemplateHeader'

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NotificationActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Store/actions/TemplateActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/ManageDocumentTemplate');
});

describe('ManageDocumentTemplateHeader', ()=>{

    test('ManageDocumentTemplateHeader : Render & Add Personal Template', async()=>{
        const {getByTestId, debug} = render(<StoreProvider><ManageDocumentTemplateHeader/></StoreProvider>);
        expect(getByTestId('ManageDocumentTemplateHeader')).toBeTruthy();
        
        fireEvent.click(getByTestId('addNewTemplate-btn'));

        await waitFor(()=>{
            expect(getByTestId('addNewTemplate-dropdown')).toBeTruthy();
            expect(getByTestId('AddPersonalTemplateClick')).toBeTruthy();
            expect(getByTestId('AddSystemTemplateClick')).toBeTruthy();
        });

        fireEvent.click(getByTestId('AddPersonalTemplateClick'));
        debug()
    });

    test('ManageDocumentTemplateHeader : Click on Dropdown 1', async ()=>{
        const {getByTestId} = render(<StoreProvider><ManageDocumentTemplateHeader/></StoreProvider>);
        expect(getByTestId('ManageDocumentTemplateHeader')).toBeTruthy();
        fireEvent.click(getByTestId('addNewTemplate-btn'));
        
        await waitFor(()=>{
            expect(getByTestId('addNewTemplate-dropdown')).toBeTruthy();
            expect(getByTestId('AddPersonalTemplateClick')).toBeTruthy();
            expect(getByTestId('AddSystemTemplateClick')).toBeTruthy();
        });
        
        fireEvent.click(getByTestId('AddPersonalTemplateClick'))
    })

    test('ManageDocumentTemplateHeader : Click on Dropdown 2', async ()=>{
        const {getByTestId} = render(<StoreProvider><ManageDocumentTemplateHeader/></StoreProvider>);
        expect(getByTestId('ManageDocumentTemplateHeader')).toBeTruthy();
        fireEvent.click(getByTestId('addNewTemplate-btn'));
        
        await waitFor(()=>{
            expect(getByTestId('addNewTemplate-dropdown')).toBeTruthy();
            expect(getByTestId('AddPersonalTemplateClick')).toBeTruthy();
            expect(getByTestId('AddSystemTemplateClick')).toBeTruthy();
        })
        
        fireEvent.click(getByTestId('AddSystemTemplateClick'))
    })

    test('ManageDocumentTemplateHeader : Click on Body', async ()=>{
        const {getByTestId} = render(<StoreProvider><ManageDocumentTemplateHeader/></StoreProvider>);
        expect(getByTestId('ManageDocumentTemplateHeader')).toBeTruthy();
        fireEvent.click(getByTestId('addNewTemplate-btn'));
        
        await waitFor(()=>{
            expect(getByTestId('addNewTemplate-dropdown')).toBeTruthy();
        })
        
        fireEvent.click(getByTestId('contentHeader'))
    })

});
