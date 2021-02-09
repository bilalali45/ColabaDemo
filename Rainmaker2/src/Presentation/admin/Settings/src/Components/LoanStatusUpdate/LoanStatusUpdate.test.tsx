import React, {useContext, useEffect, useRef, useState} from 'react';
import LoanStatusUpdateModel from '../../Entities/Models/LoanStatusUpdate';
import { LoanStatusUpdateActions } from '../../Store/actions/LoanStatusUpdateActions';
import { Store } from '../../Store/Store';
import ContentBody from '../Shared/ContentBody';
import DisabledWidget from '../Shared/DisabledWidget';
import LoanStatusUpdate from './LoanStatusUpdate';
import { LoanStatusUpdateBody } from './_LoanStatusUpdate/LoanStatusUpdateBody';
import { LoanStatusUpdateHeader } from './_LoanStatusUpdate/LoanStatusUpdateHeader';
import {LoanStatusUpdateActionsType} from '../../Store/reducers/LoanStatusUpdateReducer';
import { WidgetLoader } from '../Shared/Loader';


import { render, fireEvent, waitFor, screen } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock';
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
// import { UserActions } from '../../Store/actions/UserActions';
import App from '../../App';
import { StoreProvider } from '../../Store/Store';
import { AssignedRoleActions } from '../../Store/actions/AssignedRoleActions';
import { LocalDB } from '../../Utils/LocalDB';
import { Home } from '../Home';




jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
// jest.mock('../../Store/actions/NotificationActions');
// jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/LoanStatusUpdateActions');
jest.mock('../../Utils/LocalDB');


beforeEach(() => {
    EnvConfigMock();
    LocalStorageMock();
    const history = createMemoryHistory();
    history.push('/LoanStatusUpdate');
});


it('Loan Status Update : Render Main Component', async () => {
    const {queryAllByTestId, queryByTestId, getByTestId, debug } = render(<StoreProvider><LoanStatusUpdate/></StoreProvider>);    
    // debug();
    expect(queryByTestId("loader")).toBeTruthy();
    await waitFor(()=>{           
        expect(queryByTestId("loanStatusUpdate")).toBeTruthy();
        expect(queryByTestId("loanStatusUpdate")).toHaveClass('settings__loan-status-update')
    });    
});


