import React, { useEffect, useState, useContext } from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import AddDocument from './AddDocuments';


jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/LoanStatusUpdateActions');
jest.mock('../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

describe('AddDocument', ()=>{

    test('AddDocument : Render', async()=>{
        const props = {
            addDocumentToList: jest.fn(),
            needList: [],
            styling:{}
        }
        const {getByTestId, debug} = render(<StoreProvider>
            <AddDocument {...props}/>
        </StoreProvider>);
        expect(getByTestId('ManageDocumentTemplateBody')).toBeTruthy();
        
        await waitFor(()=>{            
           // expect(getByTestId('popup-add-doc')).toBeTruthy();
            debug();
        })
        
    });

    // test('AddDocument : Render', ()=>{
    //     const props = {
    //         addDocumentToList: jest.fn(),
    //         needList: [],
    //         styling:{}
    //     }
    //     const {getByTestId} = render(<AddDocument {...props}/>);
    //     expect(getByTestId('popup-add-doc')).toBeTruthy();
    // });

});