import { MockEnvConfig } from "./EnvConfigMock";
import { MockLocalStorage } from "./LocalStorageMock";

export const BeforeStartConfiguration = () => {
  debugger;
  MockLocalStorage();
  MockEnvConfig();
  MockLocalStorage();
};
