export class UserActions {
  static authorize() {
    return Promise.resolve(true);
  }

  static getUserInfo() {
    return {
      FirstName: 'John',
      LastName: 'Doe'
    };
  }

  static getUserName() {
    let info: any = UserActions.getUserInfo();
    return ` ${info?.FirstName} ${info?.LastName} `;
  }

  static addExpiryListener() {
    return Promise.resolve();
  }

  static keepAliveParentApp() {
    return true;
  }

  static async retainLock() {
    return {"id":"5fe9be0ff678496ad83da8ab","loanApplicationId":1002,"lockUserId":1,"lockUserName":"System Administrator","lockDateTime":"2021-01-27T05:56:37.6868188Z"}
  }

  static async aquireLock() {
    return true;
  }
}
