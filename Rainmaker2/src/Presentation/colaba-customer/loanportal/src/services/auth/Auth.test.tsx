import React from "react";

import { Auth } from "./Auth";


describe("Auth", () => {
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
    Auth.getAuth();
    expect(localStorage.getItem).toHaveBeenCalledWith("Rainmaker2Token");
  });

  test("should get devUserName and devPassword from local storage", async () => {
    Auth.getLoginUserName();
    expect(localStorage.getItem).toHaveBeenCalledWith("devusername");

    Auth.getLoginPassword();
    expect(localStorage.getItem).toHaveBeenCalledWith("devuserpassword");
  });

  test("should get refreshToken from local storage", async () => {
    Auth.getRefreshToken();
    expect(localStorage.getItem).toHaveBeenCalledWith("refreshToken");
  });

  test("should set auth to local storage", async () => {
    const token = "token";
    Auth.saveAuth(token);
    expect(localStorage.setItem).toHaveBeenCalledWith(
      "auth",
      "dG9rZW58UmFpbm1ha2VyMjAyMHw="
    );
  });

  test("should set refreshToken to local storage", async () => {
    const token = "token";
    Auth.saveRefreshToken(token);
    expect(localStorage.setItem).toHaveBeenCalledWith(
      "refreshToken",
      "dG9rZW58UmFpbm1ha2VyMjAyMHw="
    );
  });

  test("should remove refreshToken from local storage", async () => {
    const token = "token";
    Auth.removeRefreshToken();
    expect(localStorage.removeItem).toHaveBeenCalledWith("refreshToken");
  });

  test("should set tokenPayload to local storage", async () => {
    const payload = "payload";
    Auth.storeTokenPayload(payload);
    expect(localStorage.setItem).toHaveBeenCalledWith(
      "payload",
      "InBheWxvYWQifFJhaW5tYWtlcjIwMjB8"
    );
  });

  test("should set tokenPayload to local storage", async () => {
    const payload = "payload";
    Auth.storeTokenPayload(payload);
    expect(localStorage.setItem).toHaveBeenCalledWith(
      "payload",
      "InBheWxvYWQifFJhaW5tYWtlcjIwMjB8"
    );
  });

  test("should get tokenPayload from local storage", async () => {
    const payload = { payload: "payload" };
    Auth.storeTokenPayload(payload);
    Auth.getUserPayload();
    expect(localStorage.getItem).toHaveBeenCalledWith("payload");
  });

  test("should get tokenPayload from local storage", async () => {
    localStorage.setItem("Rainmaker2Token", "omnomnom");
    Object.defineProperty(window.document, "cookie", {
      writable: true,
      value:
        "ZXlKaGJHY2lPaUpJVXpJMU5pSXNJblI1Y0NJNklrcFhWQ0o5LmV5Sm9kSFJ3T2k4dmMyTm9aVzFoY3k1dGFXTnliM052Wm5RdVkyOXRMM2R6THpJd01EZ3ZNRFl2YVdSbGJuUnBkSGt2WTJ4aGFXMXpMM0p2YkdVaU9pSkRkWE4wYjIxbGNpSXNJbFZ6WlhKUWNtOW1hV3hsU1dRaU9pSTJOelExSWl3aVZYTmxjazVoYldVaU9pSjNZV2hoWWtCdGIyMXBiaTVqYjIwaUxDSm9kSFJ3T2k4dmMyTm9aVzFoY3k1NGJXeHpiMkZ3TG05eVp5OTNjeTh5TURBMUx6QTFMMmxrWlc1MGFYUjVMMk5zWVdsdGN5OXVZVzFsSWpvaWQyRm9ZV0pBYlc5dGFXNHVZMjl0SWl3aVJtbHljM1JPWVcxbElqb2lWMkZvWVdJaUxDSk1ZWE4wVG1GdFpTSTZJazF2YldsdUlpd2lWR1Z1WVc1MFNXUWlPaUl4SWl3aVpYaHdJam94TmpBek9UQTJNems0TENKcGMzTWlPaUp5WVdsdWMyOW1kR1p1SWl3aVlYVmtJanBiSW5KbFlXUmxjbk1pTENKeVpXRmtaWEp6SWwxOS5HTkNIT3Y0MVNyWVNuLW9RRWZHR0p2RmpoODBCOUUxZ1dIZXkwWFBGNGdjfFJhaW5tYWtlckF1dGhvcml6YXRpb24yMDIwfA==",
    });

    Auth.getAuth = jest.fn(
      () =>
        "ZXlKaGJHY2lPaUpJVXpJMU5pSXNJblI1Y0NJNklrcFhWQ0o5LmV5Sm9kSFJ3T2k4dmMyTm9aVzFoY3k1dGFXTnliM052Wm5RdVkyOXRMM2R6THpJd01EZ3ZNRFl2YVdSbGJuUnBkSGt2WTJ4aGFXMXpMM0p2YkdVaU9pSkRkWE4wYjIxbGNpSXNJbFZ6WlhKUWNtOW1hV3hsU1dRaU9pSTJOelExSWl3aVZYTmxjazVoYldVaU9pSjNZV2hoWWtCdGIyMXBiaTVqYjIwaUxDSm9kSFJ3T2k4dmMyTm9aVzFoY3k1NGJXeHpiMkZ3TG05eVp5OTNjeTh5TURBMUx6QTFMMmxrWlc1MGFYUjVMMk5zWVdsdGN5OXVZVzFsSWpvaWQyRm9ZV0pBYlc5dGFXNHVZMjl0SWl3aVJtbHljM1JPWVcxbElqb2lWMkZvWVdJaUxDSk1ZWE4wVG1GdFpTSTZJazF2YldsdUlpd2lWR1Z1WVc1MFNXUWlPaUl4SWl3aVpYaHdJam94TmpBek9UQTJNems0TENKcGMzTWlPaUp5WVdsdWMyOW1kR1p1SWl3aVlYVmtJanBiSW5KbFlXUmxjbk1pTENKeVpXRmtaWEp6SWwxOS5HTkNIT3Y0MVNyWVNuLW9RRWZHR0p2RmpoODBCOUUxZ1dIZXkwWFBGNGdjfFJhaW5tYWtlckF1dGhvcml6YXRpb24yMDIwfA=="
    );
     
    Auth.checkAuth();
    expect(localStorage.getItem).toHaveBeenCalledWith("payload");
  });

  test("should remove auth from local storage", async () => {
    Auth.removeAuthToken();
    expect(localStorage.removeItem).toHaveBeenCalledWith("auth");
  });

  test("should remove auth from local storage", async () => {
    Auth.removeAuthToken();
    expect(localStorage.removeItem).toHaveBeenCalledWith("auth");
  });

  test("should get loanApplicatin id from session storage", async () => {
    Auth.getLoanAppliationId();
    expect(sessionStorage.getItem).toHaveBeenCalledWith("loanApplicationId");
  });

  test("should set loanApplication is to session storage", async () => {
    const loanApplicationId = "1";
    Auth.setLoanAppliationId(loanApplicationId);
    expect(sessionStorage.setItem).toHaveBeenCalledWith(
      "loanApplicationId",
      "MXxSYWlubWFrZXIyMDIwfA=="
    );
  });

  test("should set item is to local storage", async () => {
    const name = "test";
    const value = "test";
    Auth.storeItem(name, value);
    expect(localStorage.setItem).toHaveBeenCalledWith(
      "test",
      "dGVzdHxSYWlubWFrZXIyMDIwfA=="
    );
  });

  test("should remove item from local storage", async () => {
    const name = "test";
    Auth.removeItem(name);
    expect(localStorage.removeItem).toHaveBeenCalledWith("test");
  });

  test("should remove auth from local storage", async () => {
    Auth.removeAuth();
    expect(localStorage.removeItem).toHaveBeenCalledTimes(3);
  });
});
