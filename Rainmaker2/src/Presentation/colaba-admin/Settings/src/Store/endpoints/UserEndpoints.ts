export class UserEndpoints {
    static GET = {};
  
    static POST = {
      authorize: () => `/api/Identity/token/authorize`,
      refreshToken: () => `/api/Identity/token/refresh`,
    };
  
    static PUT = {};
  
    static DELETE = {};
  }
  