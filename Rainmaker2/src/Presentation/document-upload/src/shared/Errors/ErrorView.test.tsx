import React from "react";
import { render } from "@testing-library/react";
import { ErrorView } from "../Errors/ErrorView";

beforeEach(() => {});

describe("Error View", () => {
  test('should render with id "error-view" ', async () => {
    const { getByTestId } = render(<ErrorView />);
    const errorViewDiv = getByTestId("error-view");
    expect(errorViewDiv).toContainHTML("Please try");
  });
});
