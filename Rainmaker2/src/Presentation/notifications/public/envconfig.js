window.envConfig = {
  API_BASE_URL: 'https://alphamaingateway.rainsoftfn.com'
};

let scriptElement = document.getElementById('rs-alert-lib');
if (scriptElement) {
  console.log('scriptElement =====', scriptElement);
  let apiBaseURL = scriptElement.getAttribute('data-api-url');
  if (apiBaseURL) {
    console.log(
      'scriptElement =====',
      scriptElement,
      'apiBaseURL =====',
      apiBaseURL
    );
    window.envConfig = {
      API_BASE_URL: apiBaseURL
    };
  }
}
