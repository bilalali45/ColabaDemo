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
            <div className="box-wrap--header">
                <h2 className="heading-h2"> Contact Us </h2>
            </div>
            <div className="box-wrap--body">
                <div className="row">
                    
                    <div className="col-md-5 ContactUs--left">
                        <div className="row ContactUs--user">
                            <div className="col-4 ContactUs--user---img">
                                <div className="ContactUs--user-image"><img src={ props.userImg } alt="Williams Jack" /></div>
                            </div>
                            <div className="col-8 ContactUs--user---detail">
                                <h2><a href="">{props.userName}</a> <span className="ContactUs--user-id">ID#{props.userId}</span></h2>
                            </div>
                        </div>
                       
                    </div>
                    
                    <div className="col-md-7 col-md-offset-1 ContactUs--right">                        
                        <ul className="ContactUs--list">
                            <li><SVG shape="tel" /> <a href="tel:8889711254">{props.userContact}</a></li>
                            <li><SVG shape="mail" /> <a href="mailto:Williams.jack@texastrustloans.com">{props.userEmail}</a></li>
                            <li><SVG shape="internet" /> <a href="www.texatrustloans.com" target="_blank">{props.userWebsite}</a></li>
                        </ul>
                    </div>

                </div>  
            </div>          
        </div>
    )
}
