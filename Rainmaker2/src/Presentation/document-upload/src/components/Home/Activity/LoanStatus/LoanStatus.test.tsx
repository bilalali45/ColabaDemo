import React from "react";
import {
  render,
  fireEvent,
  waitFor,
  waitForElement,
  findByTestId,
  getByText,
} from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { MockEnvConfig } from "../../../../test_utilities/EnvConfigMock";
import { MockLocalStorage } from "../../../../test_utilities/LocalStoreMock";
import App from "../../../../App";
import { Console } from "console";
import { StoreProvider } from "../../../../store/store";
import { AdaptiveWrapper } from "../../../../test_utilities/AdaptiveWrapper";

jest.mock("axios");
jest.mock("../../../../store/actions/UserActions");
jest.mock("../../../../store/actions/LoanActions");
jest.mock("../../../../store/actions/DocumentActions");
jest.mock("../../../../services/auth/Auth");

const Url = "/loanportal/activity/3";

beforeEach(() => {
  MockEnvConfig();
  MockLocalStorage();
});

describe("Loan Status components rendering", () => {
  test('should render Property Address" ', async () => {
    const { getByTestId } = render(
      <MemoryRouter initialEntries={[Url]}>
        <StoreProvider>
          <App />
        </StoreProvider>
      </MemoryRouter>
    );
    await waitFor(() => {
      const propertyAddressDiv = getByTestId("prop-address");
      expect(propertyAddressDiv.children[0].children[0].children[0]).toHaveAttribute("src", "property-address-icon.svg")
      expect(propertyAddressDiv.children[0].children[1].children[0]).toHaveTextContent("Property Address")
      expect(propertyAddressDiv.children[0].children[1].children[1]).toHaveTextContent("Houston, TEXAS 77098")
    });
  });

  test('should render Property type" ', async () => {
    const { getByTestId } = render(
      <MemoryRouter initialEntries={[Url]}>
        <StoreProvider>
          <App />
        </StoreProvider>
      </MemoryRouter>
    );
    await waitFor(() => {
      const propertyTypeDiv = getByTestId("prop-type");
      
      expect(propertyTypeDiv.children[0].children[0].children[0]).toHaveAttribute("src", "property-type-icon.svg")
      expect(propertyTypeDiv.children[0].children[1].children[0]).toHaveTextContent("Property Type")
      expect(propertyTypeDiv.children[0].children[1].children[1]).toHaveTextContent("Single Family Detached")
    });
  });

  test('should render Loan Purpose" ', async () => {
    const { getByTestId } = render(
      <MemoryRouter initialEntries={[Url]}>
        <StoreProvider>
          <App />
        </StoreProvider>
      </MemoryRouter>
    );
    await waitFor(() => {
      const loanPurposeDiv = getByTestId("loan-purpose");
      
      expect(loanPurposeDiv.children[0].children[0].children[0]).toHaveAttribute("src", "loan-purpose-icon.svg")
      expect(loanPurposeDiv.children[0].children[1].children[0]).toHaveTextContent("Loan Purpose")
      expect(loanPurposeDiv.children[0].children[1].children[1]).toHaveTextContent("Purchase a home")
    });
  });


  test('should render Loan Amount" ', async () => {
    const { getByTestId } = render(
      <MemoryRouter initialEntries={[Url]}>
        <StoreProvider>
          <App />
        </StoreProvider>
      </MemoryRouter>
    );
    await waitFor(() => {
      const loanAmtDiv = getByTestId("loan-amt");
      expect(loanAmtDiv.children[0].children[0].children[0]).toHaveAttribute("src", "loan-amount-icon.svg")
      expect(loanAmtDiv.children[0].children[1].children[0]).toHaveTextContent("Loan Amount")
      expect(loanAmtDiv.children[0].children[1].children[1]).toHaveTextContent("$")
      expect(loanAmtDiv.children[0].children[1].children[1]).toHaveTextContent("288,000")
    });
  });
});

describe("Loan Status adaptive components rendering", () => {
  test('should not render Property type', async () => {
      const { getByTestId, getByText } = render(
        <AdaptiveWrapper>
          <MemoryRouter initialEntries={[Url]}>
            <StoreProvider>
              <App />
            </StoreProvider>
          </MemoryRouter>
        </AdaptiveWrapper>
      );
      let loanStatus: any;
      await waitFor(() => {
        loanStatus = getByTestId('loan-status-mobile');
        expect(loanStatus).toBeInTheDocument();
        expect(loanStatus.children[0]).not.toHaveTextContent("Property Type");
      });
    });
  });
