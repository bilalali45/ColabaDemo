import React from 'react';
import { render, fireEvent, waitFor, getByTestId } from '@testing-library/react';
import userEvent from '@testing-library/user-event'

import { MemoryRouter } from 'react-router-dom';
import { MockEnvConfig } from '../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../Store/Store';
import { Header } from './Header';
jest.mock('pspdfkit');

const Url = '/DocManager/2515'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Doc Manager Header', () => {

    test('Should show Back Button', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Header></Header>
                </MemoryRouter>
            </StoreProvider>
        );
        
            await waitFor(() => {
                const backBtn = getByTestId('back-btn');
                expect(backBtn).toHaveTextContent("Back");
            })
    });
})