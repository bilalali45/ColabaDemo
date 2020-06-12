import React from 'react'
// import {StorageSVG} from './../../../../shared/Components/Assets/SVG';
import { userInfo } from 'os';
import { SVGtel, SVGmail, SVGinternet } from '../../../../shared/Components/Assets/SVG';

type Props = {
    userName: string,
    userId: Number,
    userContact: any,
    userEmail: string,
    userWebsite: string,
    userImg: any
}

export const ContactUs: React.FC<Props> = (props) => {
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
                                <div className="ContactUs--user-image"><img src={ props.userImg } alt="Williams Jack" /></div>
                            </div>
                            
                            <div className="ContactUs--user---detail">
                                <h2><a href="">{props.userName}</a> <span className="ContactUs--user-id">ID#{props.userId}</span></h2>
                            </div>
                        </div>
                       
                    </div>
                    
                    <div className="col-md-12 col-lg-6  ContactUs--right">                        
                        <ul className="ContactUs--list">
                            <li>
                            <a title={props.userContact} href={"tel:"+props.userContact}> 
                            <span>
                            <i className="zmdi zmdi-phone"></i> 
                            <span>{props.userContact}</span>
                            </span>
                            </a></li>
                            <li>
                                <a title={props.userEmail} href={"mailto:"+props.userEmail}>
                                    <span>
                                    <i className="zmdi zmdi-email"></i>  
                                     <span>{props.userEmail}</span>
                                    </span>
                                
                                </a>
                                </li>
                            <li>
                             <a title={props.userWebsite} href="{props.userWebsite}" target="_blank">
                             <span>
                                    <i className="zmdi zmdi-globe-alt"></i>  
                                    <span>{props.userWebsite}</span>
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
