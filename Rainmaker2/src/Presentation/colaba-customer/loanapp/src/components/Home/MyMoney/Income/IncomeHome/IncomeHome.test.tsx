import React, {createContext, useReducer, ReactFragment} from "react";
import {MemoryRouter} from 'react-router-dom';
// import { LoanAmountIcon, LoanBoxGoIcon, LoanPurposeIcon, LoanStatusIcon, PropertyTypeIcon } from '../../Shared/Components/SVGs'
// import { curruncyFormatter } from './_Dashboard'
import {createMemoryHistory} from 'history';
import {fireEvent, getAllByTestId, getByTestId, render, screen, waitFor} from '@testing-library/react';
import MaritalStatusActions from "../../../../../store/actions/MaritalStatusActions";
import {Store, StoreProvider} from "../../../../../store/store";
import {IncomeHome} from "./IncomeHome";
import {MockEnvConfig} from "../../../../../test_utilities/EnvConfigMock";
import {MockSessionStorage} from "../../../../../test_utilities/SessionStoreMock";
import IncomeActions from "../../../../../store/actions/IncomeActions";
import {IncomeHomeList} from "./IncomeHomeList";
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";


jest.mock('../../../../../store/actions/EmploymentActions');
jest.mock("../../../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../../../store/actions/MilitaryActions");
jest.mock("../../../../../store/actions/MilitaryIncomeActions");
jest.mock('../../../../../store/actions/IncomeActions');
jest.mock('../../../../../store/actions/BusinessActions');
jest.mock('../../../../../store/actions/SelfEmploymentActions');

jest.mock("../../../../../store/actions/RetirementIncomeActions");
jest.mock("../../../../../store/actions/IncomeReviewActions");
jest.mock("../../../../../store/actions/BorrowerActions");
jest.mock("../../../../../store/actions/EmploymentHistoryActions");
jest.mock("../../../../../store/actions/AssetsActions");
jest.mock("../../../../../store/actions/GiftAssetActions");
jest.mock("../../../../../store/actions/TransactionProceedsActions");
jest.mock("../../../../../store/actions/OtherAssetsActions");


jest.mock('../../../../../store/store');
jest.mock('../../../../../lib/LocalDB');
beforeEach(() => {
    MockEnvConfig();
    MockSessionStorage();
    //NavigationHandler.navigateToPath('/homepage');
    //createTestStore()
    NavigationHandler.getNavigationStateAsString =  jest.fn(()=> "")
  NavigationHandler.navigateToPath = jest.fn(()=>{})
  NavigationHandler.moveNext = jest.fn(()=>{})

});
const mockIncomeSourceHomeData = {
    "totalMonthlyQualifyingIncome": 185.17,
    "borrowers": [{
        "borrowerId": 1294,
        "borrowerName": "Qumber Kazmi",
        "ownTypeId": 1,
        "ownTypeName": "Primary Contact",
        "ownTypeDisplayName": "Primary Contact",
        "incomes": [{
            "incomeName": "asd",
            "incomeValue": 185.17,
            "incomeId": 2125,
            "incomeTypeId": 4,
            "incomeTypeDisplayName": "Cooperation",
            "employmentCategory": {"categoryId": 3, "categoryName": "Business", "categoryDisplayName": "Business"},
            "isCurrentIncome": true
        }],
        "monthlyIncome": 185.17
    }]
};

describe('Income Home Section', () => {
    test('Render check when data is available', async () => {
        IncomeActions.GetSourceOfIncomeList = jest.fn(() => Promise.resolve(mockIncomeSourceHomeData));
        const {getByTestId} = render(
            <StoreProvider>
                <MemoryRouter initialEntries={["/"]}>
                    <IncomeHome/>
                </MemoryRouter>
            </StoreProvider>
        );
        await waitFor(() => {
            expect(getByTestId('income-home-3-dots-2125')).toBeInTheDocument();
        });
    });
});


describe('Income Home Section', () => {
    test('Render check when data is available', async () => {
        IncomeActions.GetSourceOfIncomeList = jest.fn(() => Promise.resolve(mockIncomeSourceHomeData));
        const {getByTestId} = render(
            <StoreProvider>
                <MemoryRouter initialEntries={["/"]}>
                    <IncomeHomeList incomeHomeBorowerData={mockIncomeSourceHomeData} editIncome={null} deleteIncome={null} addIncome={null} shouldDisableButton={()=>{return false}} moveNext={null}  />
                </MemoryRouter>
            </StoreProvider>
        );
        await waitFor(() => {
            expect( getByTestId('income-box-1294')).toBeInTheDocument();
        });
    });
});


