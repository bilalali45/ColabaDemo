import axios, {AxiosInstance, AxiosRequestConfig} from 'axios';
import {getItem} from './localStorage';

const options: AxiosRequestConfig = {
  timeout: 30000, // it's 30 seconds, check with BE to know request timeout
  headers: {
    Authorization: `Bearer ${getItem('token')}`
  }
};

export const axiosCreate = (config?: AxiosRequestConfig): AxiosInstance => {
  const api = axios.create(config);
  const axiosBridge: any = {};

  const functionsToBridge = [
    'request',
    'get',
    'delete',
    'head',
    'options',
    'post',
    'put',
    'patch'
  ];

  functionsToBridge.forEach((functionName) => {
    axiosBridge[functionName] = async (...params: any[]) => {
      const promise: Promise<any> = ((api as any)[functionName] as any)(
        ...params
      ) as any;

      return promise;
    };
  });

  // not sure if needed, but this ensures that it doesn't get garbage-collected by accident.
  axiosBridge['axios'] = api;

  return axiosBridge;
};

export const apiV1BaseUrl = 'https://alphamaingateway.rainsoftfn.com/';

export const apiV1 = axiosCreate({
  ...options,
  baseURL: apiV1BaseUrl
});
