import React from 'react';
import ReactDOM from 'react-dom';
import Axios from 'axios';

import './index.css';
import App from './App';

// This will fetch paramters from within iFrame urls (window.location.href)
function useParametersQuery() {
  return (new URL(window.location.href)).searchParams
}

// Set cookie since we can't edit/modify so just create a new one 
// New cookie with same name, will override previous one
function createCookie(name:string,value:string) {
  document.cookie = name + "=" + value + "; path=/";
}

if(process.env.NODE_ENV==='development'){
 ReactDOM.render(
    <React.StrictMode>
      <App />
    </React.StrictMode>,
    document.getElementById('root')
  );
}else{

const params = useParametersQuery();
const key = params.get('key'); 
const mvcURL = params.get('PortalReferralUrl')

/**
 * We are now using cross origin domains, we are passing param to iFrame URL (PortalReferralUrl)
 * so that we can go back from within iframe
 * we can't access localStorage of cross origin domains.
 * widow.top.history.back() will not work since it tries to access cross origin domain's data.
 **/ 
if(mvcURL){
  localStorage.setItem('PortalReferralUrl', mvcURL)
}

const baseUrl: any = window?.envConfig?.API_BASE_URL;

  // Reset cookies to use GoColaba Domain.
  // We only initialize app if BackEnd API request succeeded without errors.
  Axios.get(`${baseUrl}/api/identity/token/singlesignon`, {
    params: {
      key
    }
  }).then((response) => {
    createCookie('Rainmaker2RefreshToken', key!); // This is RereshToken
    createCookie('Rainmaker2Token', response.data); // This is AccessToken
   
    ReactDOM.render(
      <React.StrictMode>
        <App />
      </React.StrictMode>,
      document.getElementById('root')
    );
  }).catch(error => console.log('error', error))
}


