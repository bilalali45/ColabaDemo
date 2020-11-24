import "./envconfig.js";
import Authorization from "./src/authorization";

declare global {
  interface Window {
    envConfig: any;
    Authorization: any;
  }
}
console.log("in authorize js");
const authenticate = async () => {
  const isAuth = await Authorization.authorize();
};
if (!window.location.origin.includes("localhost")) {
  (async () => {
    await authenticate();
  })();
}

window.Authorization = Authorization;
export default Authorization;
window.Authorization = Authorization;
