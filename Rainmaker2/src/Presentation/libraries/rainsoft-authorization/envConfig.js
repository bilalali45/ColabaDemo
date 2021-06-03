if(!window.envConfig) { window.envConfig = {}}
if (process.env.NODE_ENV === 'development') {
  window.envConfig.API_BASE_URL = "https://172.16.100.11/";
}

let scriptElement = document.getElementById("rs-authorization-lib");
if (scriptElement) {
  console.log("scriptElement =====", scriptElement);
  let apiBaseURL = scriptElement.getAttribute("data-api-url");
  if (apiBaseURL) {
    window.envConfig.API_BASE_URL = apiBaseURL
  }
}
