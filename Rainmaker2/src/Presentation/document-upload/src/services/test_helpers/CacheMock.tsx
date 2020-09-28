import { InMemoryCache } from "apollo-cache-inmemory";

export const MockCache = () => {
  const initialState = {
    Rainmaker2Token: "",
    Rainmaker2RefreshToken: "",
  };
  const cache = new InMemoryCache();
  cache.writeData({
    data: {
      ...initialState,
      Rainmaker2Token:
        "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsIlVzZXJQcm9maWxlSWQiOiI2MzE3IiwiVXNlck5hbWUiOiJzaGVocm96QG1haWwuY29tIiwiaHR0cDovL3NjaGVtYXMueG1sc29hcC5vcmcvd3MvMjAwNS8wNS9pZGVudGl0eS9jbGFpbXMvbmFtZSI6InNoZWhyb3pAbWFpbC5jb20iLCJGaXJzdE5hbWUiOiJTaGVocm96IiwiTGFzdE5hbWUiOiJSaXlheiIsIlRlbmFudElkIjoiMSIsImV4cCI6MTU5OTIxMDE4MSwiaXNzIjoicmFpbnNvZnRmbiIsImF1ZCI6InJlYWRlcnMifQ.ZRI8mlLPIOPYFIbUPGDie7yrQmjTNRsWqGgpf2gTSYs",
      Rainmaker2RefreshToken: "arWCfD86glTvE+ABFGu1/2/RY0AQEyigKCebdCnIdTU=",
    },
  });
};
