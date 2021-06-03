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

describe("ContactUs components rendering", () => {
  test('should render ContactUs" ', async () => {
    const { getByTestId } = render(
      <MemoryRouter initialEntries={[Url]}>
        <StoreProvider>
          <App />
        </StoreProvider>
      </MemoryRouter>
    );
    await waitFor(() => {
      const conatctUsdiv = getByTestId("contact-us");
      expect(conatctUsdiv).toHaveTextContent("Contact Us");
    });
  });

  test('should render image avatar" ', async () => {
    const { getByAltText } = render(
      <MemoryRouter initialEntries={[Url]}>
        <StoreProvider>
          <App />
        </StoreProvider>
      </MemoryRouter>
    );
    await waitFor(() => {
      const imageAvatar = getByAltText("contact us user image");
      expect(imageAvatar).toBeInTheDocument();
    });
  });

  test('should render company Name" ', async () => {
    const { getByTitle } = render(
      <MemoryRouter initialEntries={[Url]}>
        <StoreProvider>
          <App />
        </StoreProvider>
      </MemoryRouter>
    );
    await waitFor(() => {
      const companyName = getByTitle("http://www.johndoe.com");
      expect(companyName).toBeInTheDocument();
      expect(companyName).toHaveAttribute("href", "http://www.johndoe.com");
      expect(companyName).toHaveTextContent("John Doe");
    });
  });

  test('should render contact number" ', async () => {
    const { getByText, getByTitle } = render(
      <MemoryRouter initialEntries={[Url]}>
        <StoreProvider>
          <App />
        </StoreProvider>
      </MemoryRouter>
    );

    await waitFor(() => {
      const phoneIcon = getByText(
        (content, element) => element.className === "zmdi zmdi-phone"
      );
      expect(phoneIcon).toBeInTheDocument();
      const phoneNumber = getByTitle("123456789");
      expect(phoneNumber).toBeInTheDocument();
      expect(phoneNumber).toHaveAttribute("href", "tel:123456789");
      expect(phoneNumber).toHaveTextContent("(123) 456-789");
    });
  });

  test('should render contact email" ', async () => {
    const { getByText, getByTitle } = render(
      <MemoryRouter initialEntries={[Url]}>
        <StoreProvider>
          <App />
        </StoreProvider>
      </MemoryRouter>
    );

    await waitFor(() => {
      const emailIcon = getByText(
        (content, element) => element.className === "zmdi zmdi-email"
      );
      expect(emailIcon).toBeInTheDocument();
      const email = getByTitle("john@doe.com");
      expect(email).toBeInTheDocument();
      expect(email).toHaveAttribute("href", "mailto:john@doe.com");
      expect(email).toHaveTextContent("john@doe.com");
    });
  });

  test('should render website link" ', async () => {
    const { getByText, getByTitle } = render(
      <MemoryRouter initialEntries={[Url]}>
        <StoreProvider>
          <App />
        </StoreProvider>
      </MemoryRouter>
    );

    await waitFor(() => {
      const websiteIcon = getByText(
        (content, element) => element.className === "zmdi zmdi-globe-alt"
      );
      expect(websiteIcon).toBeInTheDocument();
      const website = getByTitle("www.johndoe.com");
      expect(website).toBeInTheDocument();
      expect(website).toHaveAttribute("href", "http://www.johndoe.com");
      expect(website).toHaveTextContent("www.johndoe.com");
      expect(website).not.toHaveTextContent("http://");
    });
  });

  
});

describe("ContactUs components rendering", () => {
    test('should render call div', async () => {
        const { getByTestId, getByText } = render(
          <AdaptiveWrapper>
            <MemoryRouter initialEntries={[Url]}>
              <StoreProvider>
                <App />
              </StoreProvider>
            </MemoryRouter>
          </AdaptiveWrapper>
        );
        let callDiv: any;
        await waitFor(() => {
            callDiv = getByTestId('div-call');
          expect(callDiv).toBeInTheDocument();
          expect(callDiv.children[0]).toHaveAttribute("href", "tel:123456789");
          expect(callDiv.children[0].children[0]).not.toHaveTextContent("(123) 456-789");
          expect(callDiv.children[0].children[0].children[0]).toHaveAttribute("class","zmdi zmdi-phone")
          expect(callDiv.children[0].children[0].children[1]).toHaveTextContent("Call");
        });
      });

      test('should render email div', async () => {
        const { getByTestId, getByText } = render(
          <AdaptiveWrapper>
            <MemoryRouter initialEntries={[Url]}>
              <StoreProvider>
                <App />
              </StoreProvider>
            </MemoryRouter>
          </AdaptiveWrapper>
        );
        let emailDiv: any;
        await waitFor(() => {
            emailDiv = getByTestId('div-email');
          expect(emailDiv).toBeInTheDocument();
          expect(emailDiv.children[0]).toHaveAttribute("href", "mailto:john@doe.com");
          expect(emailDiv.children[0].children[0]).not.toHaveTextContent("www.johndoe.com");
          expect(emailDiv.children[0].children[0].children[0]).toHaveAttribute("class","zmdi zmdi-email")
          expect(emailDiv.children[0].children[0].children[1]).toHaveTextContent("Email");
        });
      });


      test('should render website div', async () => {
        const { getByTestId, getByText } = render(
          <AdaptiveWrapper>
            <MemoryRouter initialEntries={[Url]}>
              <StoreProvider>
                <App />
              </StoreProvider>
            </MemoryRouter>
          </AdaptiveWrapper>
        );
        let websiteDiv: any;
        await waitFor(() => {
            websiteDiv = getByTestId('div-website');
          expect(websiteDiv).toBeInTheDocument();
          expect(websiteDiv.children[0]).toHaveAttribute("href", "http://www.johndoe.com");
          expect(websiteDiv.children[0].children[0]).not.toHaveTextContent("www.johndoe.com");
          expect(websiteDiv.children[0].children[0].children[0]).toHaveAttribute("class","zmdi zmdi-globe-alt")
          expect(websiteDiv.children[0].children[0].children[1]).toHaveTextContent("Website");
        });
      });
})
