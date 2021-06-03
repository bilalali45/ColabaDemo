export const EnvConfigMock = () => {
    const envConfig = {
      API_BASE_URL: "https://qamaingateway.rainsoftfn.com",
      IDLE_TIMER: "10", // Must be in minutes
    };
    Object.defineProperty(window, "envConfig", envConfig);
  };