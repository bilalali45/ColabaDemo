window.envConfig = {
  API_BASE_URL:'https://172.16.100.17:5001',
  // API_BASE_URL: "https://172.16.100.17:9088",
};

let scriptElement = document.getElementById("rs-alert-lib");
if (scriptElement) {
  console.log("scriptElement =====", scriptElement);
  let apiBaseURL = scriptElement.getAttribute("data-api-url");
  if (apiBaseURL) {
    console.log(
      "scriptElement =====",
      scriptElement,
      "apiBaseURL =====",
      apiBaseURL
    );
    window.envConfig = {
      API_BASE_URL: apiBaseURL,
    };
  }
}
