import axios, { AxiosRequestConfig, AxiosResponse } from 'axios';
import { OutgoingHttpHeaders } from 'http';

type HTTPMethod = AxiosRequestConfig['method'];

type CommonHTTPMethods<T> = {
    GET: T,
    POST: T,
    PUT: T,
    DELETE: T
};

type ReqConfig<T> = {
    method: HTTPMethod,
    url: string,
    data?: T,
    headers: any
}



export class Http {
    private static instance: Http | null = null;
    public baseUrl: string = '';
    public auth: string  = '';

    public methods: CommonHTTPMethods<HTTPMethod> = {
        GET: 'GET',
        POST: 'POST',
        PUT: 'PUT',
        DELETE: 'DELETE'
    }

    constructor() { 
        if (!Http.instance) {
            Http.instance = this;
        } else {
            return Http.instance;
        }
    }

    async get<T>(url: string) {
        return this.createRequest<T>(this.methods.GET, url);
    }

    async post<T, R>(url: string, data: R) {
        return this.createRequest<T>(this.methods.POST, url, data);
    }

    async put<T, R>(url: string, data: R) {
        return this.createRequest<T>(this.methods.PUT, url, data);
    }

    async delete<T>(url: string) {
        return this.createRequest<T>(this.methods.DELETE, url);
    }

    async fetch(config: AxiosRequestConfig, headers?: OutgoingHttpHeaders) {
        return axios.request({
            ...config,
            headers: headers
        });
    }

    setBaseUrl(baseUrl: string) {
        this.baseUrl = baseUrl;
    }

    setAuth(auth: string){
        this.auth = auth;
    }
    createUrl(baseUrl: string, url: string) {
        return `${baseUrl}${url}`
    }

    private async createRequest<T, R = any>(reqType: HTTPMethod, url: string, data?: R): Promise<AxiosResponse<T>> {

        try {
            let res = await axios.request<T>(this.getFonfig<R>(reqType, url, data));
            return res;
        } catch (error) {
            if (
                error?.response?.data?.name === 'TokenExpiredError'
                || error?.response?.data?.name === 'JsonWebTokenError'
                || error?.response?.data === 'Could not login'
                || error?.response?.status === 401
            ) {

            }
            

            return new Promise((_, reject) => {
                reject(error);
            });
        }
    }

    private getFonfig<T>(method: HTTPMethod, url: string, data?: T): ReqConfig<T> {
        let completeUrl = this.createUrl(this.baseUrl, url);

        let headers: OutgoingHttpHeaders = {}
        //let auth = Auth.getAuth();
        if (this.auth && (!url.includes('login') || !url.includes('authorize'))) {
            headers['Authorization'] = `Bearer ${this.auth}`;
        }

        let config: ReqConfig<T> = {
            method,
            url: completeUrl,
            headers,
        }


        if (data) {
            config.data = data;
        }
        return config;
    }

}