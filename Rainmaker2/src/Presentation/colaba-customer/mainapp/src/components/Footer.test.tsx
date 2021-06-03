import React from 'react';
import { render, waitFor } from '@testing-library/react'
import { MemoryRouter } from 'react-router-dom';
import App from '../app/App';
import { StoreProvider } from '../Store/Store';

jest.mock('../../lib/localStorage');
jest.mock('../../Store/actions/UserActions');
// jest.mock('../../../../../Store/actions/TemplateActions');

const Url = '/'
beforeEach(() => {
    // MockEnvConfig();
    // MockLocalStorage();
});
let  Props={
    "favicon":"https://apply.lendova.com:5003/colabacdn/lendova/lendova/favico.png",
"footer":"Copyright 2002 – 2019. All rights reserved. American Heritage Capital, LP. NMLS 277676. NMLS Consumer Access Site. Equal Housing Lender. Portions licensed under U.S. Patent Numbers 7,366,694 and 7,680,728.",
"color":"#ff9800",
"logo":"https://apply.lendova.com:5003/colabacdn/lendova/lendova/logo.png",
"cookiePath":"/lendova/"
}
describe('Reset Password Section ', () => {

    test('Should show Reset Password heading ', async () => {
        const { getByTestId } = render(
            <StoreProvider>
                <MemoryRouter initialEntries={[Url]}>
                    <App tenantSettings={Props}/>
                </MemoryRouter>
            </StoreProvider>
        );
        
        await waitFor(() => {
        const resetPassHeader = getByTestId("footer-text");
        expect(resetPassHeader).toBeInTheDocument();

        expect(resetPassHeader).toHaveTextContent("Copyright 2002 – 2019. All rights reserved. American Heritage Capital, LP. NMLS 277676. NMLS Consumer Access Site. Equal Housing Lender. Portions licensed under U.S. Patent Numbers 7,366,694 and 7,680,728.")
        })
    });

    

    
})