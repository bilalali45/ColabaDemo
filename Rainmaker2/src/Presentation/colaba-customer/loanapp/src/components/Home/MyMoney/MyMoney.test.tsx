import React, { createContext, useReducer, ReactFragment } from "react";
import { MemoryRouter } from "react-router-dom";

import { createMemoryHistory } from "history";
import {
  fireEvent,
  getAllByTestId,
  getByTestId,
  render,
  screen,
  waitFor,
} from "@testing-library/react";
import { MyMoney } from "./MyMoney";
import { NavigationHandler } from "../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../store/store";
Store
jest.mock("../../../store/actions/MaritalStatusActions");
jest.mock("../../../store/actions/GettingStartedActions");
jest.mock("../../../store/actions/BorrowerActions");
jest.mock("../../../store/actions/EmploymentActions");
jest.mock("../../../store/actions/GettingToKnowYouActions");
jest.mock("../../../store/actions/MilitaryActions");
jest.mock("../../../store/actions/MilitaryIncomeActions");
jest.mock("../../../store/actions/IncomeActions");
jest.mock("../../../store/actions/BusinessActions");
jest.mock("../../../store/actions/SelfEmploymentActions");

jest.mock("../../../store/actions/RetirementIncomeActions");
jest.mock("../../../store/actions/IncomeReviewActions");
jest.mock("../../../store/actions/BorrowerActions");
jest.mock("../../../store/actions/EmploymentHistoryActions");
jest.mock("../../../store/actions/AssetsActions");
jest.mock("../../../store/actions/CommonActions");
jest.mock("../../../store/actions/SubjectPropertyUseActions");
jest.mock("../../../store/actions/MyNewMortgageActions");
jest.mock("../../../store/actions/MyNewPropertyAddressActions");
jest.mock("../../../store/actions/MyPropertyActions");
jest.mock("../../../store/actions/GiftAssetActions");

jest.mock("../../../store/actions/LoanAplicationActions");
jest.mock("../../../store/actions/OtherAssetsActions");
jest.mock("../../../store/actions/SubjectPropertyUseActions");
jest.mock("../../../store/actions/TransactionProceedsActions");

jest.mock("../../../store/store");
jest.mock("../../../lib/LocalDB");

beforeEach(() => {
  //NavigationHandler.navigateToPath('/homepage');
  //createTestStore()
  NavigationHandler.enableFeature = jest.fn(() => {});
  NavigationHandler.disableFeature = jest.fn(() => {});
  NavigationHandler.moveNext = jest.fn(() => {});
  NavigationHandler.isFieldVisible = jest.fn(() => true);
  NavigationHandler.getNavigationStateAsString = jest.fn(() => "");
  NavigationHandler.navigateToPath = jest.fn(() => {});
});

describe("Income Section", () => {
  test("Should render income section", async () => {
    const { getByTestId } = render(
      <MemoryRouter initialEntries={["/"]}>
        <MyMoney />
      </MemoryRouter>
    );

    await waitFor(() => {});
  });
});
