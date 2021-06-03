import React, { useContext, useEffect } from "react";
import { Store } from "../Store/Store";
import { useHistory } from 'react-router-dom';

interface FooterProps {
  type?: number;
}

export enum FooterType {
  WithoutCaptcha = 1,
  WithCaptcha = 2,
  LoanApplication = 3,
  
}

const Footer: React.FC<FooterProps> = ({ type }) => {

  const { state } = useContext(Store);
  const user: any = state.user;
  const tenantInfo = user.tenantInfo;

  useEffect(() => {
    if (tenantInfo && tenantInfo.favicon) {
      const favicon: any = document.getElementById("favicon");

      if (favicon)
        favicon.href = tenantInfo.favicon;
    }
  }, [tenantInfo.favicon])


  const history = useHistory();
  console.log('Location  :: ', history);


  return (
    <>


      { // Without Google Policy
        ((type == undefined) || (type == FooterType.WithoutCaptcha)) &&
        <footer className="colaba-main-footer">
          <div className="container">
            <span className="disclaimer" data-testid="footer-text">{tenantInfo.footer}</span>
            {/* <span className="google-policy">This site is protected by reCAPTCHA and the Google <a href="https://policies.google.com/privacy" target="_blank">Privacy Policy</a> and <a href="https://policies.google.com/terms" target="_blank">Terms and Conditions</a> apply.</span> */}
            <span className="poweredby">Powered By <a href="http://www.colaba.io" title="www.colaba.io" target="_blank"><svg xmlns="http://www.w3.org/2000/svg" width="85.048" height="28" viewBox="0 0 85.048 28">
              <g id="Group_15966" data-name="Group 15966" transform="translate(-100 -236.808)">
                <g id="Group_15931" data-name="Group 15931" transform="translate(100 239.377)">
                  <path id="Path_11079" data-name="Path 11079" d="M2462.1,266.167l-5.827,5.827v-2.628h-2.4v5.313l-4.456,4.227,7.512,7.512,2.058-2.058-5.729-5.729,8.84-8.84Z" transform="translate(-2449.416 -266.167)" fill="#4484f4" />
                  <path id="Path_11080" data-name="Path 11080" d="M2462.25,294.958l2-2,2,2,5.542-5.541,2.063,1.887-7.606,7.606Z" transform="translate(-2453.453 -273.478)" fill="#4484f4" opacity="0.5" />
                  <rect id="Rectangle_318" data-name="Rectangle 318" width="2.057" height="2.057" transform="translate(9.997 9.693)" fill="#4484f4" />
                  <rect id="Rectangle_320" data-name="Rectangle 320" width="2.057" height="2.057" transform="translate(9.997 12.435)" fill="#4484f4" />
                  <rect id="Rectangle_319" data-name="Rectangle 319" width="2.057" height="2.057" transform="translate(12.739 9.693)" fill="#4484f4" opacity="0.5" />
                  <rect id="Rectangle_321" data-name="Rectangle 321" width="2.057" height="2.057" transform="translate(12.739 12.435)" fill="#4484f4" />
                </g>
                <text id="Colaba" transform="translate(152.048 254.808)" fill="#4484f4" font-size="18" font-family="CenturyGothic-Bold, Century Gothic" font-weight="700"><tspan x="-32.761" y="0">Colaba</tspan></text>
              </g>
            </svg></a>

            </span>
          </div>
        </footer>
      }

      { // With Google Policy
        type == FooterType.WithCaptcha &&
        <footer className="colaba-main-footer">
          <div className="container">
            <span className="disclaimer" data-testid="footer-text">{tenantInfo.footer}</span>
            <span className="google-policy">This site is protected by reCAPTCHA and the Google <a href="https://policies.google.com/privacy" target="_blank">Privacy Policy</a> and <a href="https://policies.google.com/terms" target="_blank">Terms and Conditions</a> apply.</span>
            <span className="poweredby">Powered By <a href="http://www.colaba.io" title="www.colaba.io" target="_blank"><svg xmlns="http://www.w3.org/2000/svg" width="85.048" height="28" viewBox="0 0 85.048 28">
              <g id="Group_15966" data-name="Group 15966" transform="translate(-100 -236.808)">
                <g id="Group_15931" data-name="Group 15931" transform="translate(100 239.377)">
                  <path id="Path_11079" data-name="Path 11079" d="M2462.1,266.167l-5.827,5.827v-2.628h-2.4v5.313l-4.456,4.227,7.512,7.512,2.058-2.058-5.729-5.729,8.84-8.84Z" transform="translate(-2449.416 -266.167)" fill="#4484f4" />
                  <path id="Path_11080" data-name="Path 11080" d="M2462.25,294.958l2-2,2,2,5.542-5.541,2.063,1.887-7.606,7.606Z" transform="translate(-2453.453 -273.478)" fill="#4484f4" opacity="0.5" />
                  <rect id="Rectangle_318" data-name="Rectangle 318" width="2.057" height="2.057" transform="translate(9.997 9.693)" fill="#4484f4" />
                  <rect id="Rectangle_320" data-name="Rectangle 320" width="2.057" height="2.057" transform="translate(9.997 12.435)" fill="#4484f4" />
                  <rect id="Rectangle_319" data-name="Rectangle 319" width="2.057" height="2.057" transform="translate(12.739 9.693)" fill="#4484f4" opacity="0.5" />
                  <rect id="Rectangle_321" data-name="Rectangle 321" width="2.057" height="2.057" transform="translate(12.739 12.435)" fill="#4484f4" />
                </g>
                <text id="Colaba" transform="translate(152.048 254.808)" fill="#4484f4" font-size="18" font-family="CenturyGothic-Bold, Century Gothic" font-weight="700"><tspan x="-32.761" y="0">Colaba</tspan></text>
              </g>
            </svg></a>

            </span>
          </div>
        </footer>
      }


      { // After Auth Footer Loan Application
        type == FooterType.LoanApplication &&
        <footer className="colaba-main-footer compact">
          <div className="container">
            <div className="cmf-row">
              <span className="serial-num">NMLS #2940</span>
              <span className="powered-by">Powered by Colaba</span>
            </div>
          </div>
        </footer>
      }
    </>
  )

}

export default Footer;