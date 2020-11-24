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
  }
  