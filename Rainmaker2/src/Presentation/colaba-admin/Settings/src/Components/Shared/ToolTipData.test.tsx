import React from 'react';
import { render, cleanup, fireEvent, waitFor, findByTestId, act, waitForElementToBeRemoved, wait, getByText, screen } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { EnvConfigMock } from '../../test_utilities/EnvConfigMock'
import { LocalStorageMock } from '../../test_utilities/LocalStorageMock';
// import App from '../../App';
// import { RequestEmailTemplateActions } from '../../Store/actions/RequestEmailTemplateActions';
import { ToolTipData } from './ToolTipData';
import { debug } from 'console';


jest.mock('axios');
jest.mock('../../Store/actions/UserActions');
jest.mock('../../Store/actions/NotificationActions');
jest.mock('../../Store/actions/AssignedRoleActions');
jest.mock('../../Store/actions/TemplateActions');
jest.mock('../../Store/actions/RequestEmailTemplateActions');
jest.mock('../../Utils/LocalDB');


it('ToolTipData', async()=>{
    render(<ToolTipData/>);
    expect(screen.getByTestId('ToolTipData')).toBeTruthy();
    expect(screen.queryAllByRole('h5')).toBeTruthy();
    expect(screen.queryAllByRole('p')).toBeTruthy();
    debug()
})