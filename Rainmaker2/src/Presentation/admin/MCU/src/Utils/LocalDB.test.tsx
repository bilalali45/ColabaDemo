import React from "react";

import { LocalDB } from "./LocalDB";


describe("LocalDB", () => {
  beforeEach(() => {
    Object.defineProperty(window, "localStorage", {
      value: {
        getItem: jest.fn(() => null),
        setItem: jest.fn((name, data) => name),
        removeItem: jest.fn(() => null)
      },
      writable: true,
    });

    Object.defineProperty(window, "sessionStorage", {
      value: {
        getItem: jest.fn(() => null),
        setItem: jest.fn((name, data) => null),
        removeItem: jest.fn(() => null),
      },
      writable: true,
    });
  });

  test("should get devUserName and devPassword from local storage", async () => {
    LocalDB.getAuthToken();
    
    expect(localStorage.getItem).toHaveBeenCalledWith("Rainmaker2Token");
  });

  test("should get devUserName and devPassword from local storage", async () => {
    LocalDB.getLoginDevUserName();
    expect(localStorage.getItem).toHaveBeenCalledWith("devusername");

    LocalDB.getLoginDevPassword();
    expect(localStorage.getItem).toHaveBeenCalledWith("devuserpassword");
  });

  test("should get refreshToken from local storage", async () => {
    LocalDB.getRefreshToken();
    expect(localStorage.getItem).toHaveBeenCalledWith("Rainmaker2RefreshToken");
  });

  test("should set tokenPayload to local storage", async () => {
    const payload = "payload";
    LocalDB.storeTokenPayload(payload);
    expect(localStorage.setItem).toHaveBeenCalledWith(
      "TokenPayload",
      "InBheWxvYWQifFJhaW5tYWtlck1DVTIwMjB8"
    );
  });

  test("should set auth token to local storage", async () => {
    const payload = "payload";
    const refreshToken = "refreshToken"
    LocalDB.storeAuthTokens(payload, refreshToken);
    expect(localStorage.setItem).toHaveBeenCalledWith(
        "Rainmaker2Token",
        "cGF5bG9hZHxSYWlubWFrZXJNQ1UyMDIwfA=="
      );
    
  });

  test("should get tokenPayload from local storage", async () => {
    const payload = { payload: "payload" };
    LocalDB.storeTokenPayload(payload);
    LocalDB.getUserPayload();
    expect(localStorage.getItem).toHaveBeenCalledWith("TokenPayload");
  });

  test("should get tokenPayload from local storage", async () => {
    localStorage.setItem("Rainmaker2Token", "omnomnom");
    Object.defineProperty(window.document, "cookie", {
      writable: true,
      value:
        "ZXlKaGJHY2lPaUpJVXpJMU5pSXNJblI1Y0NJNklrcFhWQ0o5LmV5Sm9kSFJ3T2k4dmMyTm9aVzFoY3k1dGFXTnliM052Wm5RdVkyOXRMM2R6THpJd01EZ3ZNRFl2YVdSbGJuUnBkSGt2WTJ4aGFXMXpMM0p2YkdVaU9pSkRkWE4wYjIxbGNpSXNJbFZ6WlhKUWNtOW1hV3hsU1dRaU9pSTJOelExSWl3aVZYTmxjazVoYldVaU9pSjNZV2hoWWtCdGIyMXBiaTVqYjIwaUxDSm9kSFJ3T2k4dmMyTm9aVzFoY3k1NGJXeHpiMkZ3TG05eVp5OTNjeTh5TURBMUx6QTFMMmxrWlc1MGFYUjVMMk5zWVdsdGN5OXVZVzFsSWpvaWQyRm9ZV0pBYlc5dGFXNHVZMjl0SWl3aVJtbHljM1JPWVcxbElqb2lWMkZvWVdJaUxDSk1ZWE4wVG1GdFpTSTZJazF2YldsdUlpd2lWR1Z1WVc1MFNXUWlPaUl4SWl3aVpYaHdJam94TmpBek9UQTJNems0TENKcGMzTWlPaUp5WVdsdWMyOW1kR1p1SWl3aVlYVmtJanBiSW5KbFlXUmxjbk1pTENKeVpXRmtaWEp6SWwxOS5HTkNIT3Y0MVNyWVNuLW9RRWZHR0p2RmpoODBCOUUxZ1dIZXkwWFBGNGdjfFJhaW5tYWtlckF1dGhvcml6YXRpb24yMDIwfA==",
    });

    LocalDB.getAuthToken = jest.fn(
      () =>
        "ZXlKaGJHY2lPaUpJVXpJMU5pSXNJblI1Y0NJNklrcFhWQ0o5LmV5Sm9kSFJ3T2k4dmMyTm9aVzFoY3k1dGFXTnliM052Wm5RdVkyOXRMM2R6THpJd01EZ3ZNRFl2YVdSbGJuUnBkSGt2WTJ4aGFXMXpMM0p2YkdVaU9pSkRkWE4wYjIxbGNpSXNJbFZ6WlhKUWNtOW1hV3hsU1dRaU9pSTJOelExSWl3aVZYTmxjazVoYldVaU9pSjNZV2hoWWtCdGIyMXBiaTVqYjIwaUxDSm9kSFJ3T2k4dmMyTm9aVzFoY3k1NGJXeHpiMkZ3TG05eVp5OTNjeTh5TURBMUx6QTFMMmxrWlc1MGFYUjVMMk5zWVdsdGN5OXVZVzFsSWpvaWQyRm9ZV0pBYlc5dGFXNHVZMjl0SWl3aVJtbHljM1JPWVcxbElqb2lWMkZvWVdJaUxDSk1ZWE4wVG1GdFpTSTZJazF2YldsdUlpd2lWR1Z1WVc1MFNXUWlPaUl4SWl3aVpYaHdJam94TmpBek9UQTJNems0TENKcGMzTWlPaUp5WVdsdWMyOW1kR1p1SWl3aVlYVmtJanBiSW5KbFlXUmxjbk1pTENKeVpXRmtaWEp6SWwxOS5HTkNIT3Y0MVNyWVNuLW9RRWZHR0p2RmpoODBCOUUxZ1dIZXkwWFBGNGdjfFJhaW5tYWtlckF1dGhvcml6YXRpb24yMDIwfA=="
    );
     
    LocalDB.checkAuth();
    expect(localStorage.getItem).toHaveBeenCalledWith("TokenPayload");
  });

  test("should get loanApplicatin id from session storage", async () => {
    LocalDB.getLoanAppliationId();
    expect(sessionStorage.getItem).toHaveBeenCalledWith("loanApplicationId");
  });

  test("should set loanApplication is to session storage", async () => {
    const loanApplicationId = "1";
    LocalDB.setLoanAppliationId(loanApplicationId);
    expect(sessionStorage.setItem).toHaveBeenCalledWith(
      "loanApplicationId",
      "MXxSYWlubWFrZXJNQ1UyMDIwfA=="
    );
  });

  test("should set item is to local storage", async () => {
    const name = "test";
    const value = "test";
    LocalDB.storeItem(name, value);
    expect(localStorage.setItem).toHaveBeenCalledWith(
      "test",
      "dGVzdHxSYWlubWFrZXJNQ1UyMDIwfA=="
    );
  });

  test("should remove LocalDB from local storage", async () => {
    LocalDB.removeAuth();
    expect(localStorage.removeItem).toHaveBeenCalledTimes(3);
  });
});
