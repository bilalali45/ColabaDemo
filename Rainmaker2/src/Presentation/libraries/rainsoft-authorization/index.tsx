import "./envconfig.js";
import Authorization from "./src/authorization";

declare global {
  interface Window {
    envConfig: any;
    Authorization: any;
  }
}
console.log("in authorize js");

export default Authorization;
window.Authorization = Authorization;
