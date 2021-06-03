import React from 'react';
import { MemoryRouter } from 'react-router-dom';

import { render, screen, waitFor } from '@testing-library/react';
import { StoreProvider } from '../../Store/Store';
import { Dashboard } from './_Dashboard';
import DashboardActions from '../../Store/actions/DashboardActions';

jest.mock('../../lib/localStorage');
jest.mock('../../Store/actions/DashboardActions');

beforeEach(() => {
    //EnvConfigMock();
    //LocalStorageMock();
    //history.push('/homepage');
});

const userLoanApplication = [{ "id": 1, "loanPurpose": "Purchase a home", "stateName": "TX", "countyName": "Harris", "cityName": "Dallas", "streetAddress": "Khalid bin waleed road", "zipCode": "76009", "loanAmount": 12000, "countryName": "United States", "unitNumber": "A1234" }, { "id": 2, "loanPurpose": "Purchase a home", "stateName": "TX", "countyName": "Harris", "cityName": "Dallas", "streetAddress": "Khalid bin waleed road", "zipCode": "76009", "loanAmount": 12000, "countryName": "United States", "unitNumber": "A1234" }, { "id": 3, "loanPurpose": "Purchase a home", "stateName": "TX", "countyName": "Harris", "cityName": "Dallas", "streetAddress": "Khalid bin waleed road", "zipCode": "76009", "loanAmount": 12000, "countryName": "United States", "unitNumber": "A1234" }];
const userLoanApplicationEmpty = [];

describe('Dashboard Section', () => {
    test('Render check when data is available', async () => {
        DashboardActions.fetchLoggedInUserCurrentLoanApplications = jest.fn(() => Promise.resolve(userLoanApplication));
        const { getByTestId } = render(
            <StoreProvider> 
                <MemoryRouter initialEntries={["/"]}>
                    <Dashboard/>
                </MemoryRouter>
        </StoreProvider>
        );
        await waitFor(()=>{
            expect( getByTestId('loan-box-1')).toBeInTheDocument();
            expect( getByTestId('loan-box-2')).toBeInTheDocument();
            expect( getByTestId('loan-box-3')).toBeInTheDocument();
        });
    });

    test('Render check when data is unavailable', async () => {
        DashboardActions.fetchLoggedInUserCurrentLoanApplications = jest.fn(() => Promise.resolve(userLoanApplicationEmpty));
        const {  } = render(
            <StoreProvider> 
                <MemoryRouter initialEntries={["/"]}>
                    <Dashboard/>
                </MemoryRouter>
        </StoreProvider>
        );
        await waitFor(()=>{
            expect(screen.getByText('Redirect will be done from here')).not.toBeEmptyDOMElement()
        });     
    });

   /*  test('the data is peanut butter', async () => {
        await expect(fetchData()).resolves.toBe('peanut butter');
    }); */
});