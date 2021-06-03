export class UserEndpoints {
  static GET = {};

  static POST = {
    authorize: () => `/api/Identity/token/authorize`,
    refreshToken: () => `/api/Identity/token/refresh`,
    retainLock: () => `/api/DocManager/Lock/RetainLock`,
    aquireLock: () => `/api/DocManager/Lock/AcquireLock`
  };

  static PUT = {};

  static DELETE = {};
}
