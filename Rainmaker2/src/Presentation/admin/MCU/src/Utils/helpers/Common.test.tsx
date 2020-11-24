import React, { FunctionComponent } from "react";
import { render } from "@testing-library/react";
import { enableBrowserPrompt,disableBrowserPrompt, } from "./Common";

beforeEach(() => {});






describe("Common helper functions", () => {

  test('test window have onbeforeunload event" ', async () => {
    enableBrowserPrompt()
    let res = typeof(window.onbeforeunload);
    expect(res).toBe("function");
    expect(res).not.toBe("object");
  });

  test('test window have not onbeforeunload event" ', async () => {
    enableBrowserPrompt();
    disableBrowserPrompt();
    let res = typeof(window.onbeforeunload);
    expect(res).toBe("object");
    expect(res).not.toBe("function");
  });


});
























