import React from 'react';
import { fireEvent, render, waitFor } from '@testing-library/react';

import { MemoryRouter } from 'react-router-dom';
import { MockEnvConfig } from '../../../../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../../../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../../../../Store/Store';
import { TemplateManager } from '../../TemplateManager';

jest.mock('axios');
jest.mock('../../../../../Store/actions/UserActions');
jest.mock('../../../../../Store/actions/NeedListActions');
jest.mock('../../../../../Store/actions/TemplateActions');
jest.mock('../../../../../Utils/LocalDB');

const Url = '/DocumentManagement/needList/3'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Template List Container', () => {

    test('Should render Template List Container', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );
        await waitFor(() => {
            expect(getByTestId('template-list-container')).toHaveTextContent('Add new template');
        });
    })



    test('Should show a new template on "Add New Template" click', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );

        await waitFor(() => {
            let newTempBtn = getByTestId('template-list-container');
            fireEvent.click(newTempBtn);
        });

        await waitFor(() => {
            let tempNameInput = getByTestId('new-template-input');
            expect(getByTestId('new-template-container')).toHaveTextContent('Add documents after template is created');
            expect(tempNameInput).toBeInTheDocument();
            expect(tempNameInput).toHaveFocus();
        })
    })

    test('Should add a new template on "Add New Template" click', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <TemplateManager></TemplateManager>
                </MemoryRouter>
            </StoreProvider>
        );
        let newTempBtn: any = null;
        await waitFor(() => {
            newTempBtn = getByTestId('template-list-container');
        });

        fireEvent.click(newTempBtn);
        let tempNameInput: any = null;
        await waitFor(() => {
            tempNameInput = getByTestId('new-template-input');
        });
        fireEvent.blur(tempNameInput);

        await waitFor(() => {
            expect(getByTestId('new-template-container')).toHaveTextContent('Add documents after template is created');

        })

    });
})

