import React, { createContext, useReducer, ReactFragment } from "react";
import { MemoryRouter } from 'react-router-dom';
// import { LoanAmountIcon, LoanBoxGoIcon, LoanPurposeIcon, LoanStatusIcon, PropertyTypeIcon } from '../../Shared/Components/SVGs'
// import { curruncyFormatter } from './_Dashboard'
import { createMemoryHistory } from 'history';
import { fireEvent, getAllByTestId, getByTestId, render, screen, waitFor } from '@testing-library/react';
import MilitaryService from './MilitaryService';
import { NavigationHandler } from "../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../store/store";

jest.mock('../../../../store/actions/MilitaryActions');

const state = {
    leftMenu: {
      navigation:null,
      leftMenuItems: [],
      notAllowedItems: [],
    },
    error: {},
    loanManager: {
      loanInfo:{
        loanApplicationId: 41313,
        loanPurposeId: null,
        loanGoalId: null,
        borrowerId: 31494,
        ownTypeId: 1,
        borrowerName: "second",
      },
      // incomeInfo:{
      //   incomeId:2102,
      //   incomeTypeId:null
      // },
      primaryBorrowerInfo: {
        id: 31450, 
        name: "khalid"
      } 
      
    },
    commonManager: {},
    employment:{},
    business:{},
    employmentHistory:{},
    militaryIncomeManager: {},
    otherIncomeManager:{},
    assetsManager: {}
  };
  const dispatch = jest.fn();

beforeEach(() => {
    //EnvConfigMock();
    //LocalStorageMock();
    //NavigationHandler.navigateToPath('/homepage');
    //createTestStore()
    NavigationHandler.enableFeature = jest.fn(()=>{})
    NavigationHandler.disableFeature = jest.fn(()=>{})
    NavigationHandler.moveNext = jest.fn(()=>{})
    NavigationHandler.isFieldVisible = jest.fn(()=>true)
    NavigationHandler.getNavigationStateAsString = jest.fn(()=>"")
});

describe('Military Service Section', () => {
    test('Check Active Military to no', async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
              <MilitaryService />
            </Store.Provider>
          );
        await waitFor(() => {
            const stats:HTMLElement = getByTestId('performedMilitaryService_no');
            expect(stats).toBeInTheDocument();
            fireEvent.click(stats)
        });

        await waitFor(()=>{
            const submit = getByTestId('military-service-submit');
            expect(submit).toBeInTheDocument();
            fireEvent.click(submit)
        })
    });

    test('Check Active Military to yes', async () => {
        const { getByTestId } = render(
            <Store.Provider value={{ state, dispatch }}>
              <MilitaryService />
            </Store.Provider>
          );
        await waitFor(() => {
            const stats:HTMLElement = getByTestId('performedMilitaryService_yes');
            expect(stats).toBeInTheDocument();
            fireEvent.click(stats)
        });

        await waitFor(() => {
            const activeDutyPersonnel:HTMLElement = getByTestId('activeDutyPersonnel');
            expect(activeDutyPersonnel).toBeInTheDocument();
            fireEvent.click(activeDutyPersonnel)
        });
        await waitFor(() => {
            const lastDateOfTourOrService:HTMLElement = getByTestId('lastDateOfTourOrService');
            expect(lastDateOfTourOrService).toBeInTheDocument();
            fireEvent.change(lastDateOfTourOrService, {target:{value: "05/05/2020"}})
        });
        await waitFor(() => {
            const reserveOrNationalGuard:HTMLElement = getByTestId('reserveOrNationalGuard');
            expect(reserveOrNationalGuard).toBeInTheDocument();
            fireEvent.click(reserveOrNationalGuard)
        });
        await waitFor(() => {
            const reserveOrNationalGuardYes:HTMLElement = getByTestId('reserveOrNationalGuardYes');
            expect(reserveOrNationalGuardYes).toBeInTheDocument();
            fireEvent.click(reserveOrNationalGuardYes)
        });
        await waitFor(() => {
            const veteran:HTMLElement = getByTestId('veteran');
            expect(veteran).toBeInTheDocument();
            fireEvent.click(veteran)
        });
        await waitFor(() => {
            const survivingSpouse:HTMLElement = getByTestId('survivingSpouse');
            expect(survivingSpouse).toBeInTheDocument();
            fireEvent.click(survivingSpouse)
        });


        await waitFor(()=>{
            const submit = getByTestId('military-service-submit');
            expect(submit).toBeInTheDocument();
            fireEvent.click(submit)
        })
    });
});


