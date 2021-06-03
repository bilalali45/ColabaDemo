export const MockEnvConfig = () => {
    const envConfig = {
      API_BASE_URL: "https://alphamaingateway.rainsoftfn.com",
      IDLE_TIMER: "10", // Must be in minutes
    };
    Object.defineProperty(window, "envConfig", envConfig);
  };