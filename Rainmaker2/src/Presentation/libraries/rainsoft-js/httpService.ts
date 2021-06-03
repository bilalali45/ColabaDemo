import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
import { OutgoingHttpHeaders } from "http";
import Cookies from "universal-cookie";
 
const cookies = new Cookies();



type HTTPMethod = AxiosRequestConfig["method"];

type CommonHTTPMethods<T> = {
  GET: T;
  POST: T;
  PUT: T;
  DELETE: T;
};

type ReqConfig<T> = {
  method: HTTPMethod;
  url: string;
  data?: T;
  headers: any;
  withCredentials?: boolean;
};

export class Http {
  private static instance: Http | null = null;
  public static baseUrl: string = "";
  public static authKey: string = "";
  public static colabaWebUrl: string = "";
  
  public static isBearer: boolean = false;

  static methods: CommonHTTPMethods<HTTPMethod> = {
    GET: "GET",
    POST: "POST",
    PUT: "PUT",
    DELETE: "DELETE",
  };

  constructor(baseUrl: string = "", authKey: string = "", colabaWebUrl: string= "") {
    if (!Http.instance) {
      Http.baseUrl = baseUrl;
      Http.authKey = authKey;
      Http.colabaWebUrl = colabaWebUrl;
      Http.instance = this;
    } else {
      return Http.instance;
    }
  }

  static async get<T>(url: string, customHeader?: OutgoingHttpHeaders, bearerNotRequired?: boolean, defaultCookie?: boolean) {
    return this.createRequest<T>(this.methods.GET, url, '',customHeader, bearerNotRequired, defaultCookie);
  }

  static async post<T, R>(
    url: string,
    data: R,
    customHeader?: OutgoingHttpHeaders,
    bearerNotRequired?: boolean,
    defaultCookie?: boolean
  ) {
    return this.createRequest<T>(this.methods.POST, url, data, customHeader, bearerNotRequired, defaultCookie);
  }

  static async put<T, R>(
    url: string,
    data: R,
    customHeader?: OutgoingHttpHeaders,
    bearerNotRequired?: boolean,
    defaultCookie?: boolean
  ) {
    return this.createRequest<T>(this.methods.PUT, url, data, customHeader, bearerNotRequired, defaultCookie);
  }

  static async delete<T, R>(
    url: string,
    data?: R,
    customHeader?: OutgoingHttpHeaders,
    bearerNotRequired?: boolean,
    defaultCookie?: boolean
  ) {
    return this.createRequest<T>(this.methods.DELETE, url, data, customHeader, bearerNotRequired, defaultCookie);
  }

  static async fetch(
    config: AxiosRequestConfig,
    headers?: OutgoingHttpHeaders
  ) {
    return axios.request({
      ...config,
      headers: headers,
    });
  }

  static createUrl(baseUrl: string, url: string) {
    let timeStamp = Math.floor(Date.now() / 1000);
    let newUrl = ''
    if (url.includes('?')) {
      newUrl = `${baseUrl}${url}${'&timeStamp='}${timeStamp}`;
    } else {
      newUrl = `${baseUrl}${url}${'?timeStamp='}${timeStamp}`;
    }
    return newUrl;
  }

  static async createRequest<T, R = any>(
    reqType: HTTPMethod,
    url: string,
    data?: R,
    customHeader?: OutgoingHttpHeaders,
    bearerNotRequired?: boolean,
    defaultCookie?: boolean
  ): Promise<AxiosResponse<T>> {
    try {
      let res = await axios.request<T>(
        this.getConfig<R>(reqType, url, data, customHeader, bearerNotRequired, defaultCookie)
      );
      return res;
    } catch (error) {
      if (
        error?.response?.data?.name === "TokenExpiredError" ||
        error?.response?.data?.name === "JsonWebTokenError" ||
        error?.response?.data === "Could not login" ||
        error?.response?.status === 401
      ) {
        console.log("Request intercept token issue.");
        window.location.href = this.decodeString(window.sessionStorage.getItem("CookiePath")) || "/" + "app/signin";
      }
      console.log("API request error", error, "request url", url);
      return new Promise((_, reject) => {
        reject(error);
      });
    }
  }

  private static getConfig<T>(
    method: HTTPMethod,
    url: string,
    data?: T,
    customHeader: OutgoingHttpHeaders = {},
    bearerNotRequired: boolean = false,
    defaultCookie: boolean = false
  ): ReqConfig<T> {
    let completeUrl = this.createUrl(Http.baseUrl, url);

    let headers: OutgoingHttpHeaders = customHeader;

    if(!bearerNotRequired){
      //if (!url.includes("login") || !url.includes("authorize")) {
        headers["Authorization"] = `Bearer ${this.decodeString(
          cookies.get(this.authKey)
        )}`;
      //}
    }

    if(this.colabaWebUrl){
      headers["ColabaWebUrl"] = this.colabaWebUrl;
    }
    
    let config: ReqConfig<T> = {
      method,
      url: completeUrl,
      headers,
      withCredentials: defaultCookie ? true : false
    };

    if (data) {
      config.data = data;
    }
    return config;
  }

  private static decodeString(value?: string | null) {
    // Decode the String
    if (!value) {
      return "";
    }
    try {
      const decodedString = atob(value);
      return decodedString.split("|")[0];
    } catch {
      return null;
    }
  }
}
