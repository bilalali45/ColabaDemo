import React from "react";
import {
  act,
  fireEvent,
  getAllByTestId,
  getByText,
  render,
  waitFor,
} from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import Path from "path";
import { StoreProvider } from "../../../../store/store";
import { SSN } from "./SSN";
import BorrowerActions from "../../../../store/actions/BorrowerActions";

// jest.mock('../../lib/localStorage');
jest.mock("../../../../Store/Actions/GettingToKnowYouActions");
jest.mock("../../../../Store/Actions/BorrowerActions");

// jest.mock('../../../../../Store/actions/TemplateActions');

const Url = "/";
beforeEach(() => {
  // MockEnvConfig();
  // MockLocalStorage();
});

describe("Personal information Current Home Address ", () => {
  test("Should show Page heading ", async () => {
    const { getByTestId } = render(<SSN />);

    await waitFor(() => {
      const resetPassHeader:HTMLElement = getByTestId("page-title");
      // expect(resetPassHeader).toBeInTheDocument();

      expect(resetPassHeader).toHaveTextContent("Personal Information");
    });
  });

  test("Should show back Button ", async () => {
    const { getByTestId } = render(<SSN />);

    await waitFor(() => {
      const resetPassHeader:HTMLElement = getByTestId("back-btn-txt");
      expect(resetPassHeader).toBeInTheDocument();

      expect(resetPassHeader).toHaveTextContent("Back");
    });
  });

  test("Should show save and continue button ", async () => {
    const { getByTestId, getAllByTestId } = render(
      <StoreProvider>
        <SSN />
      </StoreProvider>
    );

    await waitFor(() => {
      const saveContinue:HTMLElement = getByTestId("submit7002");
      expect(saveContinue).toBeInTheDocument();

      fireEvent.click(saveContinue);
    });
  });

  test("Should set ssn input", async () => {
    const { getByTestId, getAllByTestId } = render(
      <StoreProvider>
        <SSN />
      </StoreProvider>
    );

    await waitFor(() => {
      const ssnInput:HTMLElement[] = getAllByTestId("ssn7002");
      // expect(ssnInput).toBeInTheDocument();
      if(ssnInput && ssnInput.length)
      fireEvent.change(ssnInput[1], { target: { value: "" } });
    });
  });

  test("Should set date input", async () => {
    const { getByTestId, getAllByTestId, getAllByPlaceholderText } = render(
      <StoreProvider>
        <SSN />
      </StoreProvider>
    );

    await waitFor(() => {
      const dateInput:HTMLElement[] = getAllByPlaceholderText("MM/DD/YYYY");
      // expect(ssnInput).toBeInTheDocument();

      fireEvent.change(dateInput[1], { target: { value: "04/08/2021" } });
    });
  });

  test("Should click submit button", async () => {
    const { getByTestId, getAllByTestId, getAllByPlaceholderText } = render(
      <StoreProvider>
        <SSN />
      </StoreProvider>
    );

    await waitFor(() => {
      const ssnInput:HTMLElement[] = getAllByTestId("ssn7002");
      // expect(ssnInput).toBeInTheDocument();
      act(() => {
        fireEvent.change(ssnInput[1], { target: { value: "879878979" } });
      });
    });

    await waitFor(() => {
      const dateInput:HTMLElement[] = getAllByPlaceholderText("MM/DD/YYYY");
      // expect(ssnInput).toBeInTheDocument();

      act(() => {
        fireEvent.change(dateInput[1], { target: { value: "04/08/2021" } });
      });
    });

    const submitBtn:HTMLElement = getByTestId("submit7002");
    act(() => {
      fireEvent.click(submitBtn);
    });
  });


  test("Should show validation onsubmitting with empty fields", async () => {
    const { getByTestId, getAllByTestId, getAllByPlaceholderText, getByText } = render(
      <StoreProvider>
        <SSN />
      </StoreProvider>
    );

    // await waitFor(() => {
    //     const ssnInput = getAllByTestId("ssn7002");
    //     // expect(ssnInput).toBeInTheDocument();
    //     act(() => {
    //       fireEvent.change(ssnInput[1], { target: { value: "" } });
    //     });
    //   });
  
    //   await waitFor(() => {
    //     const dateInput = getAllByPlaceholderText("MM/DD/YYYY");
    //     // expect(ssnInput).toBeInTheDocument();
  
    //     act(() => {
    //       fireEvent.change(dateInput[1], { target: { value: "" } });
    //     });
    //   });
  
    //   const submitBtn = getByTestId("submit7002");
    //   act(() => {
    //     fireEvent.click(submitBtn);
    //   });
  
      //Second Tab Data

    //   await waitFor(() => {
    //     const tabs = getByTestId("tab");
    //     // expect(ssnInput).toBeInTheDocument();
    //     // act(() => {
    //         fireEvent.click(tabs.children[1]);
    //     // })
    //   });

    //   await waitFor(() => {
    //       const ssnInput = getAllByTestId("ssn7003");
    //       // expect(ssnInput).toBeInTheDocument();
    //       act(() => {
    //         fireEvent.change(ssnInput[1], { target: { value: "879878979" } });
    //       });
    //     });
    
    //     await waitFor(() => {
    //       const dateInput = getAllByPlaceholderText("MM/DD/YYYY");
    //       // expect(ssnInput).toBeInTheDocument();
    
    //       act(() => {
    //         fireEvent.change(dateInput[1], { target: { value: "04/08/2021" } });
    //       });
    //     });
    
    //     const submitBtn2 = getByTestId("submit7003");
    //     act(() => {
    //       fireEvent.click(submitBtn2);
    //     });
  });




  test("Should add data to second tab", async () => {
    const { getByTestId, getAllByTestId, getAllByPlaceholderText } = render(
      <StoreProvider>
        <SSN />
      </StoreProvider>
    );

    //First Tab Data
    await waitFor(() => {
      const ssnInput:HTMLElement[] = getAllByTestId("ssn7002");
      // expect(ssnInput).toBeInTheDocument();
      act(() => {
        fireEvent.change(ssnInput[1], { target: { value: "879878979" } });
      });
    });

    await waitFor(() => {
      const dateInput:HTMLElement[] = getAllByPlaceholderText("MM/DD/YYYY");
      // expect(ssnInput).toBeInTheDocument();

      act(() => {
        fireEvent.change(dateInput[1], { target: { value: "04/08/2021" } });
      });
    });

    const submitBtn:HTMLElement = getByTestId("submit7002");
    act(() => {
      fireEvent.click(submitBtn);
    });

    //Second Tab Data
    await waitFor(() => {
        const ssnInput:HTMLElement[] = getAllByTestId("ssn7003");
        // expect(ssnInput).toBeInTheDocument();
        act(() => {
          fireEvent.change(ssnInput[1], { target: { value: "879878979" } });
        });
      });
  
      await waitFor(() => {
        const dateInput:HTMLElement[] = getAllByPlaceholderText("MM/DD/YYYY");
        // expect(ssnInput).toBeInTheDocument();
  
        act(() => {
          fireEvent.change(dateInput[1], { target: { value: "04/08/2021" } });
        });
      });
  
      const submitBtn2:HTMLElement = getByTestId("submit7003");
      act(() => {
        fireEvent.click(submitBtn2);
      });
    
  });
});
