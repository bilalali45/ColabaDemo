export interface AuthorizeType {
  refreshToken: string | null;
  token: string | null;
}

export interface PayloadType {
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role": string;
  UserProfileId: string;
  UserName: string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": string;
  FirstName: string;
  LastName: string;
  TenantId: string;
  EmployeeId: string;
  exp: number;
  iss: string;
  aud: string | string[];
}

export interface LoginPropsType {
  email: string;
  password: string;
}

export interface PhoneProps {
  mobileNumber: string;
}

export interface OtpProps {
  otp: string;
}

export interface LoginResponseType {
    tenant2FaStatus: number;
    userPreference: boolean | null | undefined;
    isLoggedIn: boolean;
    phoneNoMissing: boolean;
    requiresTwoFa: boolean;
    phoneNo: string;
    verificationSid: string;
    token: string;
    userProfileId: number;
    userName: string;
    validFrom: string;
    validTo: string;
    otp_valid_till: string | null | undefined
}

export interface LoggedInResponseType {
  isLoggedIn: boolean;
  token: string;
  refreshToken: string;
  validFrom: string;
  validTo: string;
  cookiePath: string;
}

export type formData = {
  password: string;
  confirmPassword: string;
};

export type signIn = {
  email: string,
  password: string
}

export type insertContactEmailLog = {
  firstName: string, 
  lastName: string, 
  email: string
}

export type insertContactEmailPhoneLog = {
  firstName: string, 
  lastName: string, 
  email: string,
  phone: string
}

export type verify2Fa = {
  otpCode: string,
  verificationSid: string, 
  email: string, 
  phoneNumber: string, 
  dontAsk2Fa: boolean, 
  mapPhoneNumber: boolean
}

export type send2FaRequest = {
  phoneNumber: string,
  email: string, 
  verificationSid: string, 
}

export type forgotPasswordResponse = {
  userId: number, 
  key:string, 
  newPassword: string
}

export type getSessionValidityForResetPassword = {
  userId: number, 
  key:string
}

export type register = {
  firstName: string, 
  lastName: string, 
  email: string, 
  phone: string, 
  password:string, 
  DontAsk2Fa:boolean, 
  MapPhoneNumber:boolean, 
  RequestSid:string, 
  Skipped2Fa: boolean
}
