import React from "react";
import { fireEvent, render } from "@testing-library/react";
import { PageNotFound } from "./PageNotFound";

beforeEach(() => {});

describe("PageNotFound View", () => {
  test('should render with text "Sorry the page you are looking for does not exist" ', async () => {
    const { getByTestId } = render(<PageNotFound  />);
    const container = getByTestId("pagenotfound");
    expect(container).toHaveTextContent("Sorry the page you are looking for does not exist");
  });


});
