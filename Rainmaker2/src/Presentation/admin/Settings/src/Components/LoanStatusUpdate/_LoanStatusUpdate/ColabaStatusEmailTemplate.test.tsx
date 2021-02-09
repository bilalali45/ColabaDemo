import React, { useState, useContext, useRef, useEffect } from 'react';
// import ReactDOM from 'react-dom';


import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { UserActions } from '../../../Store/actions/UserActions';
// import App from '../../../App';
import { StoreProvider } from '../../../Store/Store';
// import { AssignedRoleActions } from '../../../Store/actions/AssignedRoleActions';
import {ColabaStatus} from './ColabaStatus';
import LoanStatusUpdate from '../LoanStatusUpdate';


jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NotificationActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Store/actions/LoanStatusUpdateActions');
jest.mock('../../../Store/Store');
jest.mock('../../../Utils/LocalDB');



beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/LoanStatusUpdate');
});

describe('ColabaStatusEmailTemplate', () => {

    const selectedLoanStatus = {statusId : true};
    const statusId = 11;

    test('ColabaStatusEmailTemplate : Render', async () => {
        const { getAllByTestId, getByTestId, getByText, debug } = render(<StoreProvider><LoanStatusUpdate/></StoreProvider>);
           
        expect(getByTestId('loader')).toBeTruthy();
        
        await waitFor(()=>{
            expect(getByText(/Nothing/i)).toBeVisible();
        });

        expect(getByText("Select status")).toBeVisible();
        expect(getByTestId('colabaStatusList')).toBeVisible();
        
        await(()=>{
            debug();
            fireEvent.click(getAllByTestId('colabaStatusList')[1]);

            expect(getByText('From Pre Approval To Approved With Condition')).toBeVisible();
            expect(getByText('Disable/Enable ')).toBeVisible();
            expect(getByTestId('contentSubHeader')).toBeTruthy();

            // Form
            expect(getByText('From Address')).toBeVisible();
            expect(getByText('CC Address')).toBeVisible();
            expect(getByText('Subject Line')).toBeVisible();
            expect(getByText('Email Body')).toBeVisible();

            
            expect(getByTestId('ColabaStatusEmailTemplateContentFooter')).toBeTruthy();
        });

    });

})