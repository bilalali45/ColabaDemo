import React from "react";
import { render } from "@testing-library/react";
import { ErrorView } from "../Errors/ErrorView";

beforeEach(() => {});

describe("Error View", () => {
  test('should render with text "Something went wrong..." ', async () => {
    const { getByTestId } = render(<ErrorView />);
    const errorViewDiv = getByTestId("error-view");
    expect(errorViewDiv).toHaveTextContent("Something went wrong...");
  });
});
