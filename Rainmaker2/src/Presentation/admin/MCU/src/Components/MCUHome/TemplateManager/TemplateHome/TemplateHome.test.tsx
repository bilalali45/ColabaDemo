import React from 'react';
import { render } from '@testing-library/react';

import { MemoryRouter } from 'react-router-dom';
import { MockEnvConfig } from '../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../../../Store/Store';
import { TemplateManager } from '../TemplateManager';

jest.mock('axios');
jest.mock('../../../../Store/actions/UserActions');
jest.mock('../../../../Store/actions/NeedListActions');
jest.mock('../../../../Store/actions/TemplateActions');
jest.mock('../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Template Home', () => {

    test('Should render Template Home', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );
        expect(getByTestId('template-home')).toBeInTheDocument();
    });

})

