import React from "react";
import { render } from "@testing-library/react";
import App from "./App";
import { BeforeStartConfiguration } from "./services/test_helpers/BeforeStart";
debugger;
test("renders learn react link", () => {
  debugger;
  beforeEach(() => {
    console.log("=======================window", window);
    BeforeStartConfiguration();
  });
  console.log("=======================window", window);
  const { getByText } = render(<App />);
  const linkElement = getByText(/learn react/i);
  expect(linkElement).toBeInTheDocument();
});
