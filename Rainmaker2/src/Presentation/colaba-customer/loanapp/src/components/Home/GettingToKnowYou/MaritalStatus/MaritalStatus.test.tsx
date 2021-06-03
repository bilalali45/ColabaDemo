import React, { createContext, useReducer, ReactFragment } from "react";
import { MemoryRouter } from 'react-router-dom';
// import { LoanAmountIcon, LoanBoxGoIcon, LoanPurposeIcon, LoanStatusIcon, PropertyTypeIcon } from '../../Shared/Components/SVGs'
// import { curruncyFormatter } from './_Dashboard'
import { createMemoryHistory } from 'history';
import { fireEvent, getAllByTestId, getByTestId, render, screen, waitFor } from '@testing-library/react';
import MaritalStatusActions from '../../../../store/actions/MaritalStatusActions';
import { StoreProvider } from '../../../../store/store';
import { MaritalStatus } from './MaritalStatus';
import {InitialStateType} from '../../../../store/store'
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
let Store;
jest.mock('../../../../store/actions/MaritalStatusActions');
jest.mock('../../../../store/store');
beforeEach(() => {
    //EnvConfigMock();
    //LocalStorageMock();
    //NavigationHandler.navigateToPath('/homepage');
    //createTestStore()
    NavigationHandler.enableFeature = jest.fn(()=>{})
    NavigationHandler.disableFeature = jest.fn(()=>{})
    NavigationHandler.moveNext = jest.fn(()=>{})
    NavigationHandler.isFieldVisible = jest.fn(()=>true)
    NavigationHandler.getNavigationStateAsString = jest.fn(()=>"")
});

const mockAllMaritalStatusData = [{ "id": 1, "name": "Married" }, { "id": 2, "name": "Separated" }, { "id": 3, "name": "Single" }, { "id": 4, "name": "Divorced" }, { "id": 5, "name": "Widowed" }, { "id": 6, "name": "Civil Union" }, { "id": 7, "name": "Domestic Partnership" }, { "id": 8, "name": "Registered Reciprocal Beneficiary Relationship" }];
const mockCurrentBorrowerMaritalStatusData = { "maritalStatus": 7, "relationshipWithPrimary": null };

describe('Maritalstatus Section', () => {
    test('Render check when data is available', async () => {
        MaritalStatusActions.getAllMaritalStatuses = jest.fn(() => Promise.resolve(mockAllMaritalStatusData));
        MaritalStatusActions.getMaritalStatus = jest.fn(() => Promise.resolve(mockCurrentBorrowerMaritalStatusData));
        const { getByTestId } = render(
            <StoreProvider>
                    <MaritalStatus currentStep = {""} setcurrentStep={""}/>
                 </StoreProvider>
        );
        await waitFor(() => {
            const stats:HTMLElement = getByTestId('marital-status-checkbox-1');
            expect(stats).toBeInTheDocument();
            fireEvent.click(stats)
        });

        await waitFor(()=>{
            const submit = getByTestId('marital-stats-submit');
            expect(submit).toBeInTheDocument();
            fireEvent.click(submit)
        })
    });
});


