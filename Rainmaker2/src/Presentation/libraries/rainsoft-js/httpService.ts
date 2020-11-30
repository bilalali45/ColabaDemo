import axios, { AxiosRequestConfig, AxiosResponse } from "axios";
import { OutgoingHttpHeaders } from "http";

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
};

export class Http {
  private static instance: Http | null = null;
  public static baseUrl: string = "";
  public static authKey: string = "";

  static methods: CommonHTTPMethods<HTTPMethod> = {
    GET: "GET",
    POST: "POST",
    PUT: "PUT",
    DELETE: "DELETE",
  };

  constructor(baseUrl: string = "", authKey: string = "") {
    if (!Http.instance) {
      Http.baseUrl = baseUrl;
      Http.authKey = authKey;
      Http.instance = this;
    } else {
      return Http.instance;
    }
  }

  static async get<T>(url: string, customHeader?: OutgoingHttpHeaders) {
    return this.createRequest<T>(this.methods.GET, url, customHeader);
  }

  static async post<T, R>(
    url: string,
    data: R,
    customHeader?: OutgoingHttpHeaders
  ) {
    return this.createRequest<T>(this.methods.POST, url, data, customHeader);
  }

  static async put<T, R>(
    url: string,
    data: R,
    customHeader?: OutgoingHttpHeaders
  ) {
    return this.createRequest<T>(this.methods.PUT, url, data, customHeader);
  }

  static async delete<T, R>(
    url: string,
    data?: R,
    customHeader?: OutgoingHttpHeaders
  ) {
    return this.createRequest<T>(this.methods.DELETE, url, data, customHeader);
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
    return `${baseUrl}${url}`;
  }

  static async createRequest<T, R = any>(
    reqType: HTTPMethod,
    url: string,
    data?: R,
    customHeader?: OutgoingHttpHeaders
  ): Promise<AxiosResponse<T>> {
    try {
      let res = await axios.request<T>(
        this.getConfig<R>(reqType, url, data, customHeader)
      );
      return res;
    } catch (error) {
      if (
        error?.response?.data?.name === "TokenExpiredError" ||
        error?.response?.data?.name === "JsonWebTokenError" ||
        error?.response?.data === "Could not login" ||
        error?.response?.status === 401
      ) {
      }
      console.log("API request error", error, "request url", url)
      return new Promise((_, reject) => {
        reject(error);
      });
    }
  }

  private static getConfig<T>(
    method: HTTPMethod,
    url: string,
    data?: T,
    customHeader: OutgoingHttpHeaders = {}
  ): ReqConfig<T> {
    let completeUrl = this.createUrl(Http.baseUrl, url);

    let headers: OutgoingHttpHeaders = customHeader;
    //let auth = Auth.getAuth();
    if (!url.includes("login") || !url.includes("authorize")) {
      headers["Authorization"] = `Bearer ${this.decodeString(
        localStorage.getItem(this.authKey)
      )}`;
    }

    let config: ReqConfig<T> = {
      method,
      url: completeUrl,
      headers,
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
