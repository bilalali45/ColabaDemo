import React, { useState } from 'react';
import ContentBody from '../../Shared/ContentBody';
import { EmailInputBox } from '../../Shared/EmailInputBox';
import { TextEditor } from '../../Shared/TextEditor';
import { Store } from '../../../Store/Store';
import { RequestEmailTemplate } from '../../../Entities/Models/RequestEmailTemplate';
import { RequestEmailTemplateActionsType } from '../../../Store/reducers/RequestEmailTemplateReducer';
import { RequestEmailTemplateActions } from '../../../Store/actions/RequestEmailTemplateActions';
import { Tokens } from '../../../Entities/Models/Token';
import { disableBrowserPrompt,enableBrowserPrompt} from '../../../Utils/helpers/Common';
import { useForm } from 'react-hook-form';
import ContentFooter from '../../Shared/ContentFooter';
import LoanStatusUpdateModel, { LoanStatus } from '../../../Entities/Models/LoanStatusUpdate';
import { LoanStatusUpdateActionsType } from '../../../Store/reducers/LoanStatusUpdateReducer';
import { LoanStatusUpdateActions } from '../../../Store/actions/LoanStatusUpdateActions';

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
import LoanStatusUpdate from '../LoanStatusUpdate';

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

describe('ColabaStatusEmailTemplateContent', () => {

    test('ColabaStatusEmailTemplateContent : Render', async () => {
        const { getByTestId, debug } = render(<StoreProvider><App/></StoreProvider>);

        expect(getByTestId('loader')).toBeTruthy();
        

        await waitFor(()=>{
            debug();
            
            
            
            
            expect(getByTestId('contentHeader')).toBeTruthy();  




            expect(getByTestId('contentSubHeader')).toBeTruthy(); 










        });
        
           
    });

})