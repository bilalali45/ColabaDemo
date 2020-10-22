import React, { useEffect, useContext, useState } from "react";
import { LaonActions } from "../../../../store/actions/LoanActions";
import { Store } from "../../../../store/store";
import { LoanActionsType } from "../../../../store/reducers/loanReducer";
import { ContactUs as ContactUsModal } from "../../../../entities/Models/ContactU";
import { MaskPhone } from "rainsoft-js";
import { Auth } from "../../../../services/auth/Auth";
import { Loader } from "../../../../shared/Components/Assets/loader";

export const ContactUs = () => {
  const [loanOfficer, setLoanOfficer] = useState<ContactUsModal>();
  const { state, dispatch } = useContext(Store);

  const laon: any = state.loan;
  const LO = laon.loanOfficer;
  const LOImage = laon.loImage;
  const { isMobile } = laon;

  useEffect(() => {
    if (!LO) {
      fetchLoanOfficer();
    }

    if (LO) {
      setLoanOfficer(new ContactUsModal().fromJson(LO));
    }
  }, [LO]);

  const fetchLoanOfficer = async () => {
    let loanApplicationId = Auth.getLoanAppliationId();
    let currentLoanOfficer:
      | ContactUsModal
      | undefined = await LaonActions.getLoanOfficer(
        Auth.getLoanAppliationId()
      );
    if (currentLoanOfficer) {
      let src: any = await LaonActions.getLOPhoto(
        currentLoanOfficer.photo,
        loanApplicationId
      );
      dispatch({
        type: LoanActionsType.FetchLoanOfficer,
        payload: currentLoanOfficer,
      });
      dispatch({ type: LoanActionsType.FetchLOImage, payload: { src } });
      // setLOPhotoSrc(src);
    }
  };

  if (!loanOfficer) {
    return <Loader containerHeight={"153px"} />;
  }
  const ContactAvatar = () => (
    <img alt="contact us user image" src={`data:image/jpeg;base64,${LOImage?.src}`} />
  );



  const ContactUsMobile = () => {
    return (
      <div data-testid="contact-us" className="ContactUs ContactUs-mobile  box-wrap">
      <div className="box-wrap--header">
        <h2 className="heading-h2"> Contact Us </h2>
      </div>

      <div className="box-wrap--body">
        <div className="row">
          <div className="col-md-12 col-lg-6 ContactUs--left">
            <div className="ContactUs--user">
              <div className="ContactUs--user---img">
                <div className="ContactUs--user-image">
                  <ContactAvatar />
                </div>
              </div>

              <div className="ContactUs--user---detail">
                <h2>
                  <a
                    title={loanOfficer.webUrl}
                    target="_blank"
                    href={loanOfficer.webUrl}
                  >
                    {loanOfficer.completeName()}
                  </a>
                  {loanOfficer.nmls && (
                    <span className="ContactUs--user-id">
                      ID#{loanOfficer.nmls}
                    </span>
                  )}
                </h2>
              </div>
            </div>
          </div>
          </div>
          <div className="ContactUs--right">
            <ul className="ContactUs--list ContactUs--list-mobile">
              <li>
                <a title={loanOfficer.phone} href={`tel:${loanOfficer.phone}`}>
                  <span>
                    <i className="zmdi zmdi-phone"></i>
                    <span>Call</span>
                  </span>
                </a>
              </li>
              <li>
                <a
                  title={loanOfficer.email}
                  href={`mailto:${loanOfficer.email}`}
                >
                  <span>
                    <i className="zmdi zmdi-email"></i>
                    <span>Email</span>
                  </span>
                </a>
              </li>
              <li>
                <a
                  title={loanOfficer.webUrl?.split("/")[2]}
                  href={"http://" + loanOfficer?.webUrl?.split("/")[2]}
                  target="_blank"
                >
                  <span>
                    <i className="zmdi zmdi-globe-alt"></i>
                    <span>
                    Website
                    </span>

                  </span>
                </a>
              </li>
            </ul>
          </div>
        
      </div>
    </div>
    )
  }

  const ContactUsDesktop = () => {
    return (
      <div data-testid="contact-us" className="ContactUs box-wrap">
      <div className="box-wrap--header">
        <h2 className="heading-h2"> Contact Us </h2>
      </div>

      <div className="box-wrap--body">
        <div className="row">
          <div className="col-md-12 col-lg-6 ContactUs--left">
            <div className="ContactUs--user">
              <div className="ContactUs--user---img">
                <div className="ContactUs--user-image">
                  <ContactAvatar />
                </div>
              </div>

              <div className="ContactUs--user---detail">
                <h2>
                  <a
                    title={loanOfficer.webUrl}
                    target="_blank"
                    href={loanOfficer.webUrl}
                  >
                    {loanOfficer.completeName()}
                  </a>
                  {loanOfficer.nmls && (
                    <span className="ContactUs--user-id">
                      ID#{loanOfficer.nmls}
                    </span>
                  )}
                </h2>
              </div>
            </div>
          </div>

          <div className="col-md-12 col-lg-6  ContactUs--right">
            <ul className="ContactUs--list">
              <li>
                <a title={loanOfficer.phone} href={`tel:${loanOfficer.phone}`}>
                  <span>
                    <i className="zmdi zmdi-phone"></i>
                    <span>{MaskPhone(Number(loanOfficer.phone))}</span>
                  </span>
                </a>
              </li>
              <li>
                <a
                  title={loanOfficer.email}
                  href={`mailto:${loanOfficer.email}`}
                >
                  <span>
                    <i className="zmdi zmdi-email"></i>
                    <span>{loanOfficer.email}</span>
                  </span>
                </a>
              </li>
              <li>
                <a
                  title={loanOfficer.webUrl?.split("/")[2]}
                  href={"http://" + loanOfficer?.webUrl?.split("/")[2]}
                  target="_blank"
                >
                  <span>
                    <i className="zmdi zmdi-globe-alt"></i>
                    <span>
                      {loanOfficer.webUrl?.split("/")[2]?.toLocaleLowerCase()}
                    </span>
                  </span>
                </a>
              </li>
            </ul>
          </div>
        </div>
      </div>
    </div>
    )
  }

  
  if(isMobile?.value) {
    return (
      ContactUsMobile()
    )
}

  return (
    ContactUsDesktop()
  );
};
