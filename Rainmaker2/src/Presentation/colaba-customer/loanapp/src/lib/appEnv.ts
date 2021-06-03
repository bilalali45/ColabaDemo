declare global {
  interface Window {
    grecaptcha: any;
    envConfig: any;
    Authorization: any;
  }
}
export class ApplicationEnv {
  static Encode_Key = 'RainmakerNotification2020|'; 
  //static ColabaWebUrl = 'https://apply.lendova.com:5003'; 
  static ColabaWebUrl = 'https://apply.lendova.com:5003/lendova/aliya/app/jk';
  static ApplicationBasePath = "/loanApplication"
  static GOOGLE_PLACES_API = 'AIzaSyBzPEiQOTReBzy6W1UcIyHApPu7_5Die6w'
}
