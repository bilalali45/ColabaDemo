declare global {
  interface Window {
    grecaptcha: any;
    envConfig: any;
    Authorization: any;
  }
}
export class ApplicationEnv {
  static Encode_Key = 'RainmakerNotification2020|'; // In Mbs
  static ColabaWebUrl = 'https://apply.lendova.com:5003'; // In Mbs
  static ApplicationBasePath = "/app"
  static LoanApplicationBasePath = "/loanapplication"
}
