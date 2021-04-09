import React from 'react';
import { render, waitFor } from '@testing-library/react';

import { MemoryRouter } from 'react-router-dom';
import { MockEnvConfig } from '../../test_utilities/EnvConfigMock';
import { MockLocalStorage } from '../../test_utilities/LocalStoreMock';
import { StoreProvider } from '../../Store/Store';
import { Footer } from './Footer';


jest.mock('pspdfkit');

const Url = '/DocManager/2515'

beforeEach(() => {
    MockEnvConfig();
    MockLocalStorage();
});

describe('Doc Manager Header', () => {

    test('Should show Footer Text', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <Footer></Footer>
                </MemoryRouter>
            </StoreProvider>
        );
        
        await waitFor(() => {
                const footerText = getByTestId('footer-text');
                expect(footerText).toHaveTextContent("Powered by Colaba");
            })
    });
})