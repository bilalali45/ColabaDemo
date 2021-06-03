import React, { createContext, useReducer, ReactFragment } from "react";
import { MemoryRouter } from "react-router-dom";

import { createMemoryHistory } from "history";
import {
  fireEvent,
  getAllByTestId,
  getByTestId,
  render,
  screen,
  waitFor,
} from "@testing-library/react";
import { Authorize } from "./Authorize";

beforeEach(() => {
  //NavigationHandler.navigateToPath('/homepage');
  //createTestStore()
});

describe("Income Sources Home Section", () => {
  test("Should render income section", async () => {
    const { getByTestId } = render(
      <MemoryRouter initialEntries={["/"]}>
        <Authorize>
          <div></div>
        </Authorize>
      </MemoryRouter>
    );

    await waitFor(() => {});
  });
});
