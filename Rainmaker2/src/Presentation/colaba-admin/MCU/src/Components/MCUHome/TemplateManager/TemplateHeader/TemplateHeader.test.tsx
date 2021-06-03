import React from 'react';
import { render, waitFor } from '@testing-library/react';

import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../../../Store/Store';
import { TemplateManager } from '../TemplateManager';
import { MockLocalStorage } from '../../../../test_utilities/LocalStoreMock';
import { Authorized } from '../../../Authorized/Authorized';


jest.mock('axios');
jest.mock('../../../../Store/actions/UserActions');
jest.mock('../../../../Store/actions/NeedListActions');
jest.mock('../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'


beforeEach(() => {
    MockLocalStorage();
});

describe('Template Manager Header', () => {

    test('should render Authorized Component', async () => {
        const { getByTestId } = render(
            <MemoryRouter>
                <Authorized component={TemplateManager} path="/"/>
            </MemoryRouter>
        );

        await waitFor(() => {
            const tempHeader = getByTestId('tempate-header')
            expect(tempHeader).toHaveTextContent('Manage Document Templates');
        });
    })


    test('Should render Template Manager Header', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );
        expect(getByTestId('tempate-header')).toHaveTextContent('Manage Document Templates');
    });

    test('Should add OnKeyDown listener', async () => {
        const { getByTestId, unmount } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );

        await waitFor(() => {
            const tempHeader = getByTestId('tempate-header')
            expect(tempHeader).toHaveTextContent('Manage Document Templates');
        });
        expect(document.body.onkeydown).toBe(Function)

    });

})

