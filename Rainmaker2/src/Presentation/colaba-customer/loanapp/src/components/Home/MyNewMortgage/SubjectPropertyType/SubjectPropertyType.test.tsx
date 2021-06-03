import React from "react";
import { fireEvent, render, waitFor } from "@testing-library/react";
import { SubjectPropertyNewHome } from "./SubjectPropertyType";
import { act } from "react-dom/test-utils";



jest.mock('../../../../store/actions/MyNewMortgageActions');
jest.mock('../../../../lib/LocalDB');


describe("My New Mortgage Steps ", () => {
  test("Shold render Property types", async () => {
    const { getByTestId, getAllByTestId } = render(
      <SubjectPropertyNewHome />
    );

    await waitFor(() => {
        const header = getByTestId("head");
        expect(header).toHaveTextContent("Subject Property");

        let tooltip = getByTestId("tooltip")
        expect(tooltip).toHaveTextContent("Please let us know about your new home.");
        expect(getByTestId("subjectProperty-form")).toBeInTheDocument();
        let propertyList = getAllByTestId('property-list');
        expect(propertyList).toHaveLength(8);
        let radioBox = getAllByTestId('list-item');
        expect(radioBox).toHaveLength(8);

        expect(radioBox[0]).toHaveClass('active');
        expect(radioBox[1]).not.toHaveClass('active');
        expect(radioBox[2]).not.toHaveClass('active');
        expect(radioBox[3]).not.toHaveClass('active');
        expect(radioBox[4]).not.toHaveClass('active');
        expect(radioBox[5]).not.toHaveClass('active');
        expect(radioBox[6]).not.toHaveClass('active');
        expect(radioBox[7]).not.toHaveClass('active');

        expect(getByTestId("save-btn")).toBeInTheDocument();

        });

        let radioBox = getAllByTestId('list-item');
        act(() => {
          fireEvent.click(radioBox[1]);
        })
        

        await waitFor(() => {
          expect(radioBox[0]).not.toHaveClass('active');
          expect(radioBox[1]).toHaveClass('active');
        });
         act(() => {
          fireEvent.click(getByTestId("save-btn"));
        })
        

  });
});
