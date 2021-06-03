import React, { FunctionComponent } from "react";
import { render } from "@testing-library/react";
import { ApplicationEnv} from "./AppEnv";
beforeEach(() => {});

describe("AppEnv helper class", () => {

  test('test AppEnv class have "Encode_Key" property and value ', async () => {
    let obj = ApplicationEnv.Encode_Key;
    expect(obj).toBe("RainmakerMCU2020|");
  });
});
























