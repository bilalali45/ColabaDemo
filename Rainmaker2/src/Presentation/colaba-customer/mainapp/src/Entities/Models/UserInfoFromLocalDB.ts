export class UserInfoFromLocalDB {
    constructor(
        public userId: string,
        public firstName: string,
        public lastName: string,
        public tenantCode: string,
        public branchCode: string,
        public aud: string,
        public exp: string,
        public iss: string
    ) {
      
    }
}
