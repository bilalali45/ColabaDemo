import React from 'react';
import { render } from '@testing-library/react';

import { MemoryRouter } from 'react-router-dom';
import { MockEnvConfig } from '../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../../Store/Store';
import { TemplateManager } from './TemplateManager';

jest.mock('axios');
jest.mock('../../../Store/actions/UserActions');
jest.mock('../../../Store/actions/NeedListActions');
jest.mock('../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Template Manager', () => {

    test('Should render Template Manager', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );
        expect(getByTestId('tempate-header')).toHaveTextContent('Manage Document Templates');
    });

})

