export class UserEndpoints {
  static GET = {};

  static POST = {
    authorize: () => `/api/Identity/token/authorize`,
    customerauthorize: () => `/api/identity/customeraccount/signin`,
    backofficeauthorize: () => `/api/identity/customeraccount/signin`,
    refreshToken: () => `/api/Identity/token/refresh`,
  };

  static PUT = {};

  static DELETE = {};
}
