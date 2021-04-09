import React, { useEffect, useState, useContext } from 'react';
import { CategoryDocument } from '../../Entities/Models/CategoryDocument';
import { Document } from '../../Entities/Models/Document';
import { TemplateDocument } from '../../Entities/Models/TemplateDocument';
import { TemplateActionsType } from '../../Store/reducers/TemplatesReducer';
import { Store, StoreProvider } from '../../Store/Store';
import { DocumentTypes } from './DocumentTypes';
import { SelectedType } from './SelectedType';

import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
import { debug } from 'console';

jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/LoanStatusUpdateActions');
jest.mock('../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
});

describe('DocumentTypes', ()=>{

    test('DocumentTypes : Render', ()=>{
        const props = {
            currentCategoryDocuments:'',
            changeCurrentDocType: jest.fn(),
            documentTypeList: []
        }
        const {getByTestId} = render(<StoreProvider>
            <MemoryRouter initialEntries={["/Setting/ManageDocumentTemplate"]}>
                <DocumentTypes/>
            </MemoryRouter>
        </StoreProvider>);
        expect(getByTestId('DocumentTypes')).toBeTruthy();
    });

});