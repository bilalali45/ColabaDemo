import React, {useState} from 'react';
import { render, cleanup, waitForDomChange, fireEvent, waitFor, waitForElement, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../../test_utilities/LocalStorageMock';
import { UserActions } from '../../../Store/actions/UserActions';
import App from '../../../App';
import { StoreProvider } from '../../../Store/Store';
import { AssignedRoleActions } from '../../../Store/actions/AssignedRoleActions';
import { LocalDB } from '../../../Utils/LocalDB';
import {LoanStatusUpdateHeader} from './LoanStatusUpdateHeader';
import ContentHeader from '../../Shared/ContentHeader';
import { Toggler } from '../../Shared/Toggler';
import { Home } from '../../Home';


jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NotificationActions');
jest.mock('../../../Store/actions/AssignedRoleActions');
jest.mock('../../../Store/actions/LoanStatusUpdateActions');
jest.mock('../../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/LoanStatusUpdate');
});

describe('Loan Status Update : Header', ()=>{

    
    test('Loan Status Update Header : Heading', async () => {
        //const { getByText } = render(<StoreProvider><LoanStatusUpdateHeader/></StoreProvider>);
        const { getByText } = render(<StoreProvider><LoanStatusUpdateHeader/></StoreProvider>);
        // debug();  
        const linkElement = getByText(/LOAN STATUS UPDATE/i);
        expect(linkElement).toBeInTheDocument();
             
    });

    test('Loan Status Update Header : Info Component', async () => {
        const { getByTestId, debug } = render(<StoreProvider><LoanStatusUpdateHeader/></StoreProvider>);
        //debug(); 
        // 
        expect(getByTestId('toolTip')).toBeTruthy();
        expect(getByTestId('toolTip')).toHaveClass("info-display--icon");

        
    });

    test('Loan Status Update Header : Disable/Enable Toggler', async () => {
        const { getByTestId, debug } = render(<StoreProvider><LoanStatusUpdateHeader/></StoreProvider>);
        
        expect(getByTestId('disEna')).toBeTruthy();
        expect(getByTestId('disEna')).toHaveTextContent('Disable/Enable');
        fireEvent.click(getByTestId('togglerDefault').children[0]);
        debug(); 
        expect(getByTestId('togglerDefault').children[0]).toHaveProperty('checked')
        //.toHaveAttribute("checked", "false")


    });


})