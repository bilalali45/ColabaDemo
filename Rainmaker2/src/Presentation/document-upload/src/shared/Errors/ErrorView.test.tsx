import React from "react";
import {
  render,
  waitForDomChange,
  fireEvent,
  waitFor,
  waitForElement,
} from "@testing-library/react";
import { ErrorView } from "../Errors/ErrorView";
import { MemoryRouter } from "react-router-dom";

import { createMemoryHistory } from "history";

beforeEach(() => {});

describe("Error View", () => {
  test('should render with id "error-view" ', async () => {
    const { getByTestId } = render(<ErrorView />);
    const activityHeader = getByTestId("error-view");
    expect(activityHeader).toContainHTML("Please try");
  });
});
