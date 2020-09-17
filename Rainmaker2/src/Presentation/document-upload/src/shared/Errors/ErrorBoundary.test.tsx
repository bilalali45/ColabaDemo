import React from "react";
import { fireEvent, render } from "@testing-library/react";
import ErrorBoundary from "./ErrorBoundary";
beforeEach(() => {});

const ComponentWithoutError = () => {
  return (
    <div data-TestId="noerror"> There is no error </div>
  )
}

const ParentComponentWithoutError = () => {
  return (
    <div>
    <ErrorBoundary>
      <ComponentWithoutError />
    </ErrorBoundary>
  </div>
  )
}

const Childcomponent = () => {
  throw new Error('Something bad happened')
}
const ParentCmponent = () => {
  return (
    <div>
      <ErrorBoundary>
        <Childcomponent />
      </ErrorBoundary>
    </div>
  )
}
describe("ErrorBoundary View", () => {
  test('should render with text "Something went wrong." ', async () => {
    const { getByTestId } = render(
      <ParentCmponent />
    );
    const container = getByTestId("errorBoundry");
    expect(container).toHaveTextContent("Something went wrong.");
  });

  test('should render with no error case ', async () => {
    const { getByTestId } = render(
      <ParentComponentWithoutError />
    );
    const container = getByTestId("noerror");
    expect(container).toHaveTextContent("There is no error");
  });


});
