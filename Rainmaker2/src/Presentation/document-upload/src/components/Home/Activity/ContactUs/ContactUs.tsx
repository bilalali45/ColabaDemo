import React from 'react'
import {SVG} from './../../../../shared/Components/Assets/SVG';
import { userInfo } from 'os';

type Props = {
    userName: string,
    userId: Number,
    userContact: any,
    userEmail: string,
    userWebsite: string,
    userImg: any
}

export const ContactUs: React.SFC<Props> = (props) => {
    return (
        <div className="ContactUs box-wrap">
            <div className="row">
                
                <div className="col-md-7">
                    <div className="ContactUs--header">
                        <h2 className="heading-h2"> Contact Us </h2>
                    </div>
                    <div className="ContactUs--body">
                        <h2 className="ContactUs--user"><a href="">{props.userName}</a> <span className="ContactUs--user-id">ID#{props.userId}</span></h2>
                        <ul className="ContactUs--list">
                            <li><SVG shape="tel" /> <a href="tel:8889711254">{props.userContact}</a></li>
                            <li><SVG shape="mail" /> <a href="mailto:Williams.jack@texastrustloans.com">{props.userEmail}</a></li>
                            <li><SVG shape="internet" /> <a href="www.texatrustloans.com" target="_blank">{props.userWebsite}</a></li>
                        </ul>
                    </div>
                </div>

                <div className="col-md-5">
                    <div className="ContactUs--user-image"><img src={ props.userImg } alt="Williams Jack" /></div>
                </div>

            </div>            
        </div>
    )
}
