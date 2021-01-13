import React from 'react';
import ReactDOM from 'react-dom';
import Axios from 'axios';

import './index.css';
import App from './App';

// This will fetch paramters from within iFrame urls (window.location.href)
function useParametersQuery() {
  return (new URL(window.location.href)).searchParams
}
function isValidHttpUrl(string : any) {
  let url;
  
  try {
    url = new URL(string);
  } catch (_) {
    return false;  
  }

  return url.protocol === "http:" || url.protocol === "https:";
}
function getCookie(name:any){
  const value = `; ${document.cookie}`;
  const parts = value.split(`; ${name}=`);
  if (parts.length === 2) return decodeURIComponent(parts.pop()?.split(';').shift()!);
  return null;
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
let key : string | null | undefined = params.get('key'); 
const mvcURL = params.get('PortalReferralUrl')
if(!key)
{
  console.log('reading key from cookie');
  key = getCookie('Rainmaker2RefreshToken');
}
else
{
  console.log('reading key from param');
  key = decodeURIComponent(key!)
}
console.log(`key is ${key}`);
/**
 * We are now using cross origin domains, we are passing param to iFrame URL (PortalReferralUrl)
 * so that we can go back from within iframe
 * we can't access localStorage of cross origin domains.
 * widow.top.history.back() will not work since it tries to access cross origin domain's data.
 **/
const rainmakerUrl = window?.envConfig?.RAIN_MAKER_URL; 
if(mvcURL && isValidHttpUrl(mvcURL)){
  localStorage.setItem('PortalReferralUrl', mvcURL)
}
else
{
   if(!localStorage.getItem('PortalReferralUrl'))
  {
    localStorage.setItem('PortalReferralUrl', `${rainmakerUrl}/Admin/Dashboard`)
  }
}
console.log(`Portal referral URL is ${localStorage.getItem('PortalReferralUrl')}`);
const baseUrl: any = window?.envConfig?.API_BASE_URL;

  // Reset cookies to use GoColaba Domain.
  // We only initialize app if BackEnd API request succeeded without errors.
  Axios.get(`${baseUrl}/api/identity/token/singlesignon`, {
    params: {
      key
    }
  }).then((response) => {
    //createCookie('Rainmaker2RefreshToken', key!); // This is RereshToken
    //createCookie('Rainmaker2Token', response.data); // This is AccessToken
    
    ReactDOM.render(
      <React.StrictMode>
        <App authToken={response.data} refreshToken={key!} />
      </React.StrictMode>,
      document.getElementById('root')
    );
  }).catch(error => console.log('error', error))
}


