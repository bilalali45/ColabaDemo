import React, { useEffect, useContext, useState, useRef } from 'react'
// import { SVG } from './../../../../shared/Components/Assets/SVG';
import { userInfo, loadavg } from 'os';
import { LaonActions } from '../../../../store/actions/LoanActions';
import { Store } from '../../../../store/store';
import { LoanActionsType } from '../../../../store/reducers/loanReducer';
import { ContactUs as ContactUsModal } from '../../../../entities/Models/ContactU';
import { SVGtel, SVGmail, SVGinternet } from '../../../../shared/Components/Assets/SVG';
import { MaskPhone } from 'rainsoft-js';
import { Auth } from '../../../../services/auth/Auth';

export const ContactUs = ({ }) => {

    const [loanOfficer, setLoanOfficer] = useState<ContactUsModal>();
    const [lOPhotoSrc, setLOPhotoSrc] = useState<string>();

    const LOphotoRef: any = useRef();

    useEffect(() => {
        if (!loanOfficer) {
            fetchLoanOfficer();
        }
    });

    const fetchLoanOfficer = async () => {
        let loanOfficer: ContactUsModal | undefined = await LaonActions.getLoanOfficer(Auth.getLoanAppliationId(), Auth.getBusinessUnitId());
        if (loanOfficer) {
            let src: any = await LaonActions.getLOPhoto(loanOfficer.photo, Auth.getBusinessUnitId());
            // src = `data:image/jpeg;base64,${src}}`;
            setLOPhotoSrc(src);
            if (LOphotoRef.current) {
                LOphotoRef.current.src = src
            }
            setLOPhotoSrc(src);
            setLoanOfficer(loanOfficer);
        }
    }

    const ContactAvatar = () => <img src={`data:image/jpeg;base64,${lOPhotoSrc}`} />

    const getFormattedPhone = () => {
        if (loanOfficer && loanOfficer.phone) {
            let phone: any = Number(loanOfficer.phone) | 0;
            return MaskPhone(+phone)
        }
    }

    if (!loanOfficer) {
        return <div>...loading...</div>
    }
    return (
        <div className="ContactUs box-wrap">
            <div className="box-wrap--header">
                <h2 className="heading-h2"> Contact Us </h2>
            </div>

            <div className="box-wrap--body">
                <div className="row">

                    <div className="col-md-12 col-lg-6 ContactUs--left">
                        {/* <div className="row ContactUs--user">
                                <div className="col-4 ContactUs--user---img">
                                    <div className="ContactUs--user-image"><img src={ props.userImg } alt="Williams Jack" /></div>
                                </div>
    
                                <div className="col-8 ContactUs--user---detail">
                                    <h2><a href="">{props.userName}</a> <span className="ContactUs--user-id">ID#{props.userId}</span></h2>
                                </div>
                            </div> */}
                        <div className="ContactUs--user">
                            <div className="ContactUs--user---img">
                                <div className="ContactUs--user-image"><ContactAvatar/></div>
                            </div>

                            <div className="ContactUs--user---detail">
                                <h2><a href="">{loanOfficer.completeName()}</a> <span className="ContactUs--user-id">ID#{loanOfficer.nmls}</span></h2>
                            </div>
                        </div>

                    </div>

                    <div className="col-md-12 col-lg-6  ContactUs--right">
                        <ul className="ContactUs--list">
                            <li>
                                <a title={loanOfficer.phone} href={`tel:${loanOfficer.phone}`}>
                                    <span>
                                        <i className="zmdi zmdi-phone"></i>
                                        <span>{getFormattedPhone()}</span>
                                    </span>
                                </a></li>
                            <li>
                                <a title={loanOfficer.email} href={`mailto:${loanOfficer.email}`}>
                                    <span>
                                        <i className="zmdi zmdi-email"></i>
                                        <span>{loanOfficer.email}</span>
                                    </span>

                                </a>
                            </li>
                            <li>
                                <a title={loanOfficer.webUrl} href={loanOfficer.webUrl} target="_blank">
                                    <span>
                                        <i className="zmdi zmdi-globe-alt"></i>
                                        <span>{loanOfficer.webUrl}</span>
                                    </span>

                                </a>
                            </li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>
    );

}
