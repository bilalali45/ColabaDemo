import React, { createContext, useReducer, ReactFragment } from "react";
import { MemoryRouter } from 'react-router-dom';
// import { LoanAmountIcon, LoanBoxGoIcon, LoanPurposeIcon, LoanStatusIcon, PropertyTypeIcon } from '../../Shared/Components/SVGs'
// import { curruncyFormatter } from './_Dashboard'
import { createMemoryHistory } from 'history';
import { fireEvent, getAllByTestId, getByTestId, render, screen, waitFor } from '@testing-library/react';
import MaritalStatusActions from '../../../../store/actions/MaritalStatusActions';
import { StoreProvider } from '../../../../store/store';
import {CoApplicant} from './CoApplicant'
import {InitialStateType} from '../../../../store/store'
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import LeftMenuHandler from "../../../../store/actions/LeftMenuHandler";


jest.mock('../../../../store/actions/MaritalStatusActions');
jest.mock('../../../../store/store');
jest.mock("../../../../lib/LocalDB")
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
    LeftMenuHandler.addDecision = jest.fn(()=>{})
    NavigationHandler.navigateToPath= jest.fn(()=>{})
});

const mockAllMaritalStatusData = [{ "id": 1, "name": "Married" }, { "id": 2, "name": "Separated" }, { "id": 3, "name": "Single" }, { "id": 4, "name": "Divorced" }, { "id": 5, "name": "Widowed" }, { "id": 6, "name": "Civil Union" }, { "id": 7, "name": "Domestic Partnership" }, { "id": 8, "name": "Registered Reciprocal Beneficiary Relationship" }];
const mockCurrentBorrowerMaritalStatusData = { "maritalStatus": 7, "relationshipWithPrimary": null };

describe('CoApplicant Section', () => {
    test('select yes and submit', async () => {
        MaritalStatusActions.getAllMaritalStatuses = jest.fn(() => Promise.resolve(mockAllMaritalStatusData));
        MaritalStatusActions.getMaritalStatus = jest.fn(() => Promise.resolve(mockCurrentBorrowerMaritalStatusData));
        const { getByTestId } = render(
            <StoreProvider>
                    <CoApplicant currentStep = {""} setcurrentStep={""}/>
                 </StoreProvider>
        );
        await waitFor(() => {
            const coapplicant_yes:HTMLElement = getByTestId('coapplicant_yes');
            expect(coapplicant_yes).toBeInTheDocument();
            fireEvent.click(coapplicant_yes)
        });

        await waitFor(()=>{
            const submit = getByTestId('coapplicant-submit');
            expect(submit).toBeInTheDocument();
            fireEvent.click(submit)
        })
    });

    test('select no and submit', async () => {
        MaritalStatusActions.getAllMaritalStatuses = jest.fn(() => Promise.resolve(mockAllMaritalStatusData));
        MaritalStatusActions.getMaritalStatus = jest.fn(() => Promise.resolve(mockCurrentBorrowerMaritalStatusData));
        const { getByTestId } = render(
            <StoreProvider>
                    <CoApplicant currentStep = {""} setcurrentStep={""}/>
                 </StoreProvider>
        );
        await waitFor(() => {
            const coapplicant_yes:HTMLElement = getByTestId('coapplicant_no');
            expect(coapplicant_yes).toBeInTheDocument();
            fireEvent.click(coapplicant_yes)
        });

        await waitFor(()=>{
            const submit = getByTestId('coapplicant-submit');
            expect(submit).toBeInTheDocument();
            fireEvent.click(submit)
        })
    });
});


