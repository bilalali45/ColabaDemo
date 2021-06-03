declare namespace jest {
    interface Matchers<R> {
      toEndWith(value: string): CustomMatcherResult;
    }
  }

  declare module "*.png" {
    const value: any;
    export default value;
  }

  declare module "*.svg" {
    const value: any;
    export default value;
  }

  declare type PromiseConstructorLike = new <T>(executor: (resolve: (value?: T | PromiseLike<T>) => void, reject: (reason?: any) => void) => void) => PromiseLike<T>;