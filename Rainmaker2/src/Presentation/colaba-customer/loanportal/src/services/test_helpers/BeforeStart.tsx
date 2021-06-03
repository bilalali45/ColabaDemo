import { MockEnvConfig } from "./EnvConfigMock";
import { MockLocalStorage } from "./LocalStorageMock";

export const BeforeStartConfiguration = () => {
  MockLocalStorage();
  MockEnvConfig();
  MockLocalStorage();
};
