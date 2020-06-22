import React, { useState, useEffect } from 'react'
import {  useLocation, Link } from 'react-router-dom';
import { LoanStatus } from '../Activity/LoanStatus/LoanStatus'
const ActivityHeader = (props) => {
console.log('props',props)
    const [leftNav, setLeftNav] = useState('');
    const [rightNav, setRightNav] = useState('');
    const [leftNavUrl, setLeftNavUrl] = useState('');
    const [rightNavUrl, setRightNavUrl] = useState('');
   
    const location = useLocation();

    const setNavigations = (pathname) => {
        if (pathname.includes('activity')) {
            setLeftNav('Dashboard');
            setRightNav('Uploaded Document');
            setLeftNavUrl('/DashBoard');
            setRightNavUrl('/uploadedDocuments');
        }

        if (pathname.includes('documentsRequest')) {
            setLeftNav('Home');
            setRightNav('Uploaded Document');
            setLeftNavUrl('/activity');
            setRightNavUrl('/uploadedDocuments');
        }

        if (pathname.includes('uploadedDocuments')) {    
            if(props.location.state.from == '/activity'){
                setLeftNav('Home');
                setLeftNavUrl('/activity');
                setRightNav('Document Request');          
                setRightNavUrl('/documentsRequest');
            }else{
                setLeftNav('Document Request');
                setLeftNavUrl('/documentsRequest');
                setRightNav('');          
                setRightNavUrl('');
            }
        }
    }

    useEffect(() => {
       // let query = location.search.split('=')[1];
        setNavigations(location.pathname);
    }, [location.pathname]);

    // const handleNav = (id: string) => {
    //     let splitData = location.pathname.split('/');
    //     if(id === 'right') {  
    //         history.push(rightNavUrl+`?from=${splitData[1]}`)
    //     }else {
    //         history.push(leftNavUrl)
    //     }
    // }

    return (
        <div className="activityHeader">
            <section className="compo-loan-status">
                <LoanStatus />
            </section>
            <section className="row-subheader">
                <div className="row">
                    <div className="container">
                        <div className="sub-header-wrap">
                            <div className="row">
                                <div className="col-6">
                                    <ul className="breadcrmubs">
                                        <li>
                                        {/* <Link to={{
                                                pathname: leftNavUrl,
                                                state: { from: location.pathname }
                                            }}><i className="zmdi zmdi-arrow-left"></i>{leftNav}</Link> */}
                                       
                                            <a  ><i className="zmdi zmdi-arrow-left"></i> {leftNav}</a>
                                        </li>
                                    </ul>
                                </div>
                                <div className="col-6 text-right">

                                    <div className="action-doc-upload">
                                        
                                        <a >
                                            {rightNav}
                                        </a>
                                         {/* <Link to={{
                                                pathname: rightNavUrl,
                                                state: { from: location.pathname }
                                            }}>{rightNav}</Link> */}

                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>

        </div>
    )
}

export default ActivityHeader;