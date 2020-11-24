import React, { FunctionComponent } from "react";
import { render } from "@testing-library/react";
import { truncate } from "./TruncateString";

beforeEach(() => {});
const TruncateComponent = () => {
    return (
      <div data-TestId="truncatedId"> {truncate("Hello World",5)} </div>
    )

}

describe("Truncate String", () => {
  test('should render with truncated text "Hello..." ', async () => {
    const { getByTestId } = render(<TruncateComponent  />);
    const block = getByTestId("truncatedId");
    expect(block).toHaveTextContent("Hello...");
  });
});
