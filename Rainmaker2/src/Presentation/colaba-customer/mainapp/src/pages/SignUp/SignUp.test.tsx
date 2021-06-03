import React from 'react';
import { render } from '@testing-library/react';
import { MemoryRouter } from 'react-router-dom';
import { StoreProvider } from '../../Store/Store';
import { SignUp } from './';

jest.mock('../../lib/localStorage');
jest.mock('../../Store/actions/UserActions');


describe('<-------------SignUp Section-----------------> ', () => {
    test('Should show "Create Your Account" header text', async() => {
        const {getByTestId} = render(
         <StoreProvider> 
             <MemoryRouter initialEntries={["/"]}>
                     <SignUp></SignUp>
                 </MemoryRouter>
         </StoreProvider>
        );
        expect(getByTestId('signup-sreen')).toBeInTheDocument();
        
     });
    
});