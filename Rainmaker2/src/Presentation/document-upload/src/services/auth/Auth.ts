import jwt_decode from "jwt-decode";
import Cookies from "universal-cookie";
const cookies = new Cookies();

export class Auth {
  public static saveAuth(token: string) {
    localStorage.setItem("auth", token);
  }

  public static saveRefreshToken(refToken: string) {
    localStorage.setItem("refreshToken", refToken);
  }

  public static getRefreshToken() {
    return localStorage.getItem("refreshToken");
  }

  public static removeRefreshToken() {
    localStorage.removeItem("refreshToken");
  }

  public static getAuth() {
    return localStorage.getItem("auth");
  }

  public static checkAuth(): boolean | string {
    const rainmaker2Token =
      "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJDdXN0b21lciIsIlVzZXJQcm9maWxlSWQiOiI1NzE2IiwiVXNlck5hbWUiOiJwa2R1bm5qckBkdW5uaGVhdC5jb20iLCJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoicGtkdW5uanJAZHVubmhlYXQuY29tIiwiRmlyc3ROYW1lIjoiUGV0ZXIiLCJMYXN0TmFtZSI6IkR1bm4iLCJleHAiOjE1OTQyMjYxMzQsImlzcyI6InJhaW5zb2Z0Zm4iLCJhdWQiOiJyZWFkZXJzIn0.55hLZyjlHroHMuDamgpOqgru2WaOEXg6VPt4mgzLT6k Rainmaker2RefreshToken TKmrSs+rdORazpOzkAXksJATEtdWTntpWmbsvVtkj8k="; //cookies.get("Rainmaker2Token");
    const auth = localStorage.getItem("auth");
    if (!auth) {
      return false;
    }
    debugger;
    if (rainmaker2Token) {
      const decodeCacheToken: any = jwt_decode(rainmaker2Token);
      const decodeAuth: any = jwt_decode(auth);
      if (decodeAuth?.UserName != decodeCacheToken?.UserName) {
        return false;
      }
      if (decodeCacheToken?.exp) {
        console.log("Cache token check expiry");
        const isValidToken = Auth.checkExpiry(decodeCacheToken?.exp);
        if (isValidToken == "token expired") {
        } else {
          console.log("Cache token is not expire");
          return false;
        }
      }
    }

    let payload = this.getUserPayload();
    if (payload) {
      return Auth.checkExpiry(payload.exp);
    }
    return true;
  }

  static checkExpiry = (interval) => {
    if (interval) {
      let expiry = new Date(interval * 1000);
      let currentDate = new Date(Date.now());
      if (currentDate < expiry) {
        return true;
      } else {
        return "token expired";
        // return false;
        // Auth.removeAuth();
      }
    }
    return false;
  };

  static storeTokenPayload(payload) {
    if (!payload) return;
    localStorage.setItem("payload", JSON.stringify(payload));
  }

  static getUserPayload() {
    let payload = localStorage.getItem("payload");
    if (payload) {
      return JSON.parse(payload);
    }
  }

  static removeAuthToken() {
    localStorage.removeItem("auth");
  }

  public static removeAuth() {
    let items = [
      "auth",
      "loanApplicationId",
      "tenantId",
      "businessUnitId",
      "payload",
      "refreshToken",
    ];
    for (const item of items) {
      localStorage.removeItem(item);
    }
  }

  public static getLoanAppliationId() {
    return localStorage.getItem("loanApplicationId") || "";
  }

  public static getTenantId() {
    return localStorage.getItem("tenantId") || "";
  }

  public static getBusinessUnitId() {
    return localStorage.getItem("businessUnitId") || "";
  }

  public static setLoanAppliationId(loanApplicationId: string) {
    localStorage.setItem("loanApplicationId", loanApplicationId);
  }

  public static setTenantId(tenantId: string) {
    localStorage.setItem("tenantId", tenantId);
  }

  public static setBusinessUnitId(businessUnitId: string) {
    localStorage.setItem("businessUnitId", businessUnitId);
  }

  public static storeItem(name: string, data: string) {
    localStorage.setItem(name, data);
  }

  public static removeItem(name: string) {
    localStorage.removeItem(name);
  }
}
