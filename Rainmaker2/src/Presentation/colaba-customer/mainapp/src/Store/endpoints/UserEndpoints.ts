export class UserEndpoints {
  static GET = {
    getTermConditionAndAgreement: (type: number) => `/api/tenantconfig/tenant/GetTermCondition?type=${type}`,
    getTenant2FaConfig: (getCustomerConfig: boolean) => `/api/Identity/TwoFa/GetTenant2FaConfig?getCustomerConfig=${getCustomerConfig}`,
    get2FaIntervalValue: () => '/api/Identity/TwoFa/Get2FaIntervalValue',
    doesCustomerAccountExist: (email: string) => `/api/identity/customeraccount/DoesCustomerAccountExist?email=${email}`,
    getTenantSettings: () => `/api/tenantconfig/tenant/GetSetting`,
    getSessionValidityForResetPassword: (userId:number, key:string)=> (`/api/identity/customeraccount/IsPasswordLinkExpired?userId=${userId}&key=${key}`)
  };

  static POST = {
    signIn: () => `/api/identity/customeraccount/signin`,
    send2FaRequest: () => `/api/Identity/TwoFa/Send2FaRequest`,
    verify2FaSignIn: () => `/api/Identity/TwoFa/Verify2FaSignInRequest`,
    verify2FaSignUp: () => '/api/Identity/TwoFa/Verify2FaRequest',
    skip2FaRequest: () => `/api/Identity/TwoFa/Skip2FaSignIn`,
    forgotPassword: () => `/api/identity/customeraccount/forgotpasswordrequest`,
    changePassword: () => `/api/identity/CustomerAccount/ChangePassword`,
    insertContactEmailLog: () => `/api/TenantConfig/Log/InsertContactEmailLog`,
    insertContactEmailPhoneLog: () => '/api/tenantconfig/log/InsertContactEmailPhoneLog',
    forgotPasswordResponse: () => `/api/identity/customeraccount/forgotpasswordresponse`,
    register: () => '/api/identity/customeraccount/register',
    dontAsk2FA: () => `/api/Identity/TwoFa/DontAsk2FA`
  };
  static PUT = {};

  static DELETE = {};
}