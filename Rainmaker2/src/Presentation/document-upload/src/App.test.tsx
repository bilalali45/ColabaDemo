import React from "react";
import { render } from "@testing-library/react";
import App from "./App";
import { BeforeStartConfiguration } from "./services/test_helpers/BeforeStart";
import { FormatAmountByCountry } from "rainsoft-js";
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

test('Should convert a number into US currency seperated by comma', async () => {

  const amount = 32094802;

  const formatted = FormatAmountByCountry(amount);

  expect(formatted).toEqual('32,094,802');

});