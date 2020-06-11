import React, { useEffect, useContext, useState } from 'react'
// import { SVG } from './../../../../shared/Components/Assets/SVG';
import { userInfo, loadavg } from 'os';
import { LaonActions } from '../../../../store/actions/LoanActions';
import { Store } from '../../../../store/store';
import { LoanActionsType } from '../../../../store/reducers/loanReducer';
import { ContactUs as ContactUsModal } from '../../../../entities/Models/ContactU';
import { SVGtel, SVGmail, SVGinternet } from '../../../../shared/Components/Assets/SVG';


export const ContactUs: React.SFC = () => {

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

                        <div className="col-md-5 ContactUs--left">
                            <div className="row ContactUs--user">
                                <div className="col-4 ContactUs--user---img">
                                    <div className="ContactUs--user-image"><img src={loanOfficer.photo} alt="Williams Jack" /></div>
                                </div>
                                <div className="col-8 ContactUs--user---detail">
                                    <h2><a href="">{loanOfficer.completeName()}</a> <span className="ContactUs--user-id">ID#{loanOfficer.rmls}</span></h2>
                                </div>
                            </div>

                        </div>

                        <div className="col-md-7 col-md-offset-1 ContactUs--right">
                            <ul className="ContactUs--list">
                                <li><SVGtel /> <a href="tel:8889711254">{loanOfficer.photo}</a></li>
                                <li><SVGmail /> <a href="mailto:Williams.jack@texastrustloans.com">{loanOfficer.phone}</a></li>
                                <li><SVGinternet /> <a href="www.texatrustloans.com" target="_blank">{loanOfficer.webUrl}</a></li>
                            </ul>
                        </div>

                    </div>
                </div>
            </div>
        )
    }

    return <div>...loading...</div>

}
