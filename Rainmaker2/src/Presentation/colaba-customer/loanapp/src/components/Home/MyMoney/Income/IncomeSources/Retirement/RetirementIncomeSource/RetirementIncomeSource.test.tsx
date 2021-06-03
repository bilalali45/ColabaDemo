import React, { createContext, useReducer, ReactFragment } from "react";
import { MemoryRouter } from 'react-router-dom';
import { createMemoryHistory } from 'history';
import { fireEvent, getAllByTestId, getByTestId, render, screen, waitFor } from '@testing-library/react';
import { StoreProvider } from '../../../../../../../store/store';
import { InitialStateType } from '../../../../../../../store/store'
import { RetirementIncomeSource } from "./RetirementIncomeSource";
import RetirementIncomeActions from '../../../../../../../store/actions/RetirementIncomeActions';
import { APIResponse } from '../../../../../../../Entities/Models/APIResponse';
import { CommaFormatted } from '../../../../../../../Utilities/helpers/CommaSeparteMasking';
import { incomeTypeIds, incomeTypesMock, incomeInfoMock } from '../../../../../../../store/actions/__mocks__/RetirementIncomeActions';

jest.mock("../../../../../../../store/actions/RetirementIncomeActions");
jest.mock('../../../../../../../store/store');


describe('RetirementIncomeSource Fields Rendering Test Cases', () => {
    test("Should render all retirement types", async () => {
        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(null));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            expect(screen.getByTestId("income-type-" + incomeTypeIds.socialSecurity)).toBeInTheDocument();
        });

        await waitFor(() => {
            expect(screen.getByTestId("income-type-" + incomeTypeIds.pension)).toBeInTheDocument();
        });

        await waitFor(() => {
            expect(screen.getByTestId("income-type-" + incomeTypeIds.ira)).toBeInTheDocument();
        });

        await waitFor(() => {
            expect(screen.getByTestId("income-type-" + incomeTypeIds.otherRetirement)).toBeInTheDocument();
        });
    });

    test("Should not render fields when IncomeType is not selected", async () => {
        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(null));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            expect(screen.queryByTestId('monthly-base-income')).toBeNull();
        });

        await waitFor(() => {
            expect(screen.queryByTestId('employer-name')).toBeNull();
        });

        await waitFor(() => {
            expect(screen.queryByTestId('description-value')).toBeNull();
        });
    });

    test("Should only render 'monthlyBaseIncome' field when IncomeType is 'Social Security'", async () => {
        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(null));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let incomeType: HTMLElement = getByTestId("income-type-" + incomeTypeIds.socialSecurity);
            fireEvent.click(incomeType)
        });

        await waitFor(() => {
            expect(screen.getByTestId('monthly-base-income')).toBeInTheDocument();
        });

        await waitFor(() => {
            expect(screen.queryByTestId('employer-name')).toBeNull();
        });

        await waitFor(() => {
            expect(screen.queryByTestId('description-value')).toBeNull();
        });
    });

    test("Should only render 'employerName' & 'monthlyBaseIncome' fields when IncomeType is 'Pension'", async () => {
        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(null));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let incomeType: HTMLElement = getByTestId("income-type-" + incomeTypeIds.pension);
            fireEvent.click(incomeType)
        });

        await waitFor(() => {
            expect(screen.getByTestId('monthly-base-income')).toBeInTheDocument();
        });

        await waitFor(() => {
            expect(screen.queryByTestId('employer-name')).toBeInTheDocument();
        });

        await waitFor(() => {
            expect(screen.queryByTestId('description-value')).toBeNull();
        });
    });

    test("Should only render 'monthlyBaseIncome' field when IncomeType is 'Ira / 401K'", async () => {
        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(null));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let incomeType: HTMLElement = getByTestId("income-type-" + incomeTypeIds.ira);
            fireEvent.click(incomeType)
        });

        await waitFor(() => {
            expect(screen.getByTestId('monthly-base-income')).toBeInTheDocument();
        });

        await waitFor(() => {
            expect(screen.queryByTestId('employer-name')).toBeNull();
        });

        await waitFor(() => {
            expect(screen.queryByTestId('description-value')).toBeNull();
        });
    });

    test("Should only render 'monthlyBaseIncome' & 'description' fields when IncomeType is 'Other Retirement'", async () => {
        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(null));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let incomeType: HTMLElement = getByTestId("income-type-" + incomeTypeIds.otherRetirement);
            fireEvent.click(incomeType)
        });

        await waitFor(() => {
            expect(screen.getByTestId('monthly-base-income')).toBeInTheDocument();
        });

        await waitFor(() => {
            expect(screen.queryByTestId('employer-name')).toBeNull();
        });

        await waitFor(() => {
            expect(screen.queryByTestId('description-value')).toBeInTheDocument();
        });
    });
});

describe('RetirementIncomeSource Fields Value Change Test Cases', () => {
    test("Should change monthlyBaseIncome", async () => {
        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(null));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let incomeType: HTMLElement = getByTestId("income-type-" + incomeTypeIds.socialSecurity);
            fireEvent.click(incomeType)
        });

        let monthlyBaseIncome: HTMLElement;
        const value = "1000";
        await waitFor(() => {
            monthlyBaseIncome = screen.getByTestId('monthly-base-income');
            expect(monthlyBaseIncome).toBeInTheDocument();
            fireEvent.change(monthlyBaseIncome, { target: { value } })
        });

        await waitFor(() => {
            expect(monthlyBaseIncome).toHaveDisplayValue(CommaFormatted(value));
        })
    });

    test("Should change Employer Name", async () => {
        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(null));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let incomeType: HTMLElement = getByTestId("income-type-" + incomeTypeIds.pension);
            fireEvent.click(incomeType)
        });

        let employerName: HTMLElement;
        await waitFor(() => {
            employerName = screen.getByTestId('employer-name');
            expect(employerName).toBeInTheDocument();
            fireEvent.change(employerName, { target: { value: "Employer 1" } })
        });

        await waitFor(() => {
            expect(employerName).toHaveDisplayValue("Employer 1");
        })
    });

    test("Should change Description", async () => {

        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(null));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let incomeType: HTMLElement = getByTestId("income-type-" + incomeTypeIds.otherRetirement);
            fireEvent.click(incomeType)
        });

        let description: HTMLElement;
        await waitFor(() => {
            description = screen.getByTestId('description-value');
            expect(description).toBeInTheDocument();
            fireEvent.change(description, { target: { value: "Some description" } })
        });

        await waitFor(() => {
            expect(description).toHaveDisplayValue("Some description");
        })
    });

});

describe('RetirementIncomeSource Actions Test Cases', () => {
    test("Should populate values in fields for incomeType 'Social Security'", async () => {
        incomeInfoMock.incomeTypeId = incomeTypeIds.socialSecurity;
        incomeInfoMock.monthlyBaseIncome = 1050;

        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(new APIResponse(200, incomeInfoMock)));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let monthlyBaseIncome: HTMLElement = screen.getByTestId('monthly-base-income');
            expect(monthlyBaseIncome).toHaveDisplayValue(CommaFormatted(incomeInfoMock.monthlyBaseIncome));
        });
    });

    test("Should populate values in fields for incomeType 'Pension'", async () => {
        incomeInfoMock.incomeTypeId = incomeTypeIds.pension;
        incomeInfoMock.monthlyBaseIncome = 1050;

        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(new APIResponse(200, incomeInfoMock)));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let monthlyBaseIncome: HTMLElement = screen.getByTestId('monthly-base-income');
            expect(monthlyBaseIncome).toHaveDisplayValue(CommaFormatted(incomeInfoMock.monthlyBaseIncome));

            let employerName: HTMLElement = screen.getByTestId('employer-name');
            expect(employerName).toHaveDisplayValue(CommaFormatted(incomeInfoMock.employerName));
        });
    });

    test("Should populate values in fields for incomeType 'IRA / 401K'", async () => {
        incomeInfoMock.incomeTypeId = incomeTypeIds.ira;
        incomeInfoMock.monthlyBaseIncome = 1050;

        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(new APIResponse(200, incomeInfoMock)));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let monthlyBaseIncome: HTMLElement = screen.getByTestId('monthly-base-income');
            expect(monthlyBaseIncome).toHaveDisplayValue(CommaFormatted(incomeInfoMock.monthlyBaseIncome));
        });
    });

    test("Should populate values in fields  for incomeType 'Other Retirement'", async () => {
        incomeInfoMock.incomeTypeId = incomeTypeIds.otherRetirement;
        incomeInfoMock.monthlyBaseIncome = 1050;
        incomeInfoMock.description = "some description";

        RetirementIncomeActions.GetRetirementIncomeInfo = jest.fn(() => Promise.resolve(new APIResponse(200, incomeInfoMock)));

        const { getByTestId } = render(
            <StoreProvider>
                <RetirementIncomeSource />
            </StoreProvider>
        );

        await waitFor(() => {
            let monthlyBaseIncome: HTMLElement = screen.getByTestId('monthly-base-income');
            expect(monthlyBaseIncome).toHaveDisplayValue(CommaFormatted(incomeInfoMock.monthlyBaseIncome));

            let description: HTMLElement = screen.getByTestId('description-value');
            expect(description).toHaveDisplayValue(CommaFormatted(incomeInfoMock.description));
        });
    });
});