import React from "react";
import { fireEvent, render } from "@testing-library/react";
import { AlertBox } from "../AlertBox/AlertBox";

beforeEach(() => {});

describe("Alert View", () => {
  test('should render with class Name "alert-box" ', async () => {
    const { getByTestId } = render(<AlertBox hideAlert={() => {}} />);
    const alertDiv = getByTestId("alert-box");
    expect(alertDiv).toHaveClass("alert-box");
  });

  test("When click on Yes button", async () => {
    const { getByTestId } = render(<AlertBox hideAlert={() => {}} />);
    const btnYes = getByTestId("btnyes");
    fireEvent.click(btnYes);
    expect(btnYes).toHaveClass("btn btn-secondary");
  });

  test("When click on No button", async () => {
    const { getByTestId } = render(<AlertBox hideAlert={() => {}} />);
    const btnNo = getByTestId("btnno");
    fireEvent.click(btnNo);
    expect(btnNo).toHaveClass("btn btn-primary");
  });
});
