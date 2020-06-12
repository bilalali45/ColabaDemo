import React, { useEffect, useContext, useState } from 'react'
// import { SVG } from './../../../../shared/Components/Assets/SVG';
import { userInfo, loadavg } from 'os';
import { LaonActions } from '../../../../store/actions/LoanActions';
import { Store } from '../../../../store/store';
import { LoanActionsType } from '../../../../store/reducers/loanReducer';
import { ContactUs as ContactUsModal } from '../../../../entities/Models/ContactU';
import { SVGtel, SVGmail, SVGinternet } from '../../../../shared/Components/Assets/SVG';


export const ContactUs = ({ }) => {

    const [loanOfficer, setLoanOfficer] = useState<ContactUsModal>();

    useEffect(() => {
        if (!loanOfficer) {
            fetchLoanOfficer();
        }
    });

    const fetchLoanOfficer = async () => {
        let loanOfficer: ContactUsModal | undefined = await LaonActions.getLoanOfficer('1', '2');
        if (loanOfficer) {
            setLoanOfficer(loanOfficer)
        }
    }

    if (loanOfficer) {
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
                                    <div className="ContactUs--user-image"><img src={'userImg'} alt="Williams Jack" /></div>
                                </div>

                                <div className="ContactUs--user---detail">
                                    <h2><a href="">{'props.userName'}</a> <span className="ContactUs--user-id">ID#{'props.userId'}</span></h2>
                                </div>
                            </div>

                        </div>

                        <div className="col-md-12 col-lg-6  ContactUs--right">
                            <ul className="ContactUs--list">
                                <li>
                                    <a title={'props.userContact'} href={"tel:" + 'props.userContact'}>
                                        <span>
                                            <i className="zmdi zmdi-phone"></i>
                                            <span>{'props.userContact'}</span>
                                        </span>
                                    </a></li>
                                <li>
                                    <a title={'props.userEmail'} href={"mailto:" + 'props.userEmail'}>
                                        <span>
                                            <i className="zmdi zmdi-email"></i>
                                            <span>{'props.userEmail'}</span>
                                        </span>

                                    </a>
                                </li>
                                <li>
                                    <a title={'props.userWebsite'} href="{props.userWebsite}" target="_blank">
                                        <span>
                                            <i className="zmdi zmdi-globe-alt"></i>
                                            <span>{'props.userWebsite'}</span>
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
    return <div>...loading...</div>


}
