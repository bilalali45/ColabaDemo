import React, { useState, useEffect } from 'react'
import { useHistory, useLocation } from 'react-router-dom';
import { LoanStatus } from '../Activity/LoanStatus/LoanStatus'
import { ParamsService } from '../../../utils/ParamsService';
const ActivityHeader = () => {

    const [leftNav, setLeftNav] = useState('');
    const [rightNav, setRightNav] = useState('');
    const [leftNavUrl, setLeftNavUrl] = useState('');
    const [rightNavUrl, setRightNavUrl] = useState('');

    const history = useHistory();
    const location = useLocation();

    const setNavigations = (pathname, queryString) => {
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
            if(queryString == 'activity'){
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
        let query = location.search.split('=')[1];
        setNavigations(location.pathname, query);
    }, [location.pathname]);

    const handleNav = (id: string) => {
        let splitData = location.pathname.split('/');
        if(id === 'right') {  
            history.push(rightNavUrl+`?from=${splitData[1]}`)
        }else {
            history.push(leftNavUrl)
        }
    }

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
                                            
                                            <a tabIndex={-1} onClick={() => handleNav('left')} ><i className="zmdi zmdi-arrow-left"></i> {leftNav}</a>
                                        </li>
                                    </ul>
                                </div>
                                <div className="col-6 text-right">

                                    <div className="action-doc-upload">
                                        
                                        <a onClick={() => handleNav('right')} >
                                            {rightNav}
                                        </a>

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