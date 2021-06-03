import React from 'react'
import PageHead from '../../../../Shared/Components/PageHead';
import { BorrowerConsentTabs } from './BorrowerConsentTabs';

export const Consent = () => { 
    return (
        <div data-testid="consent-screen" className="compo-abt-yourSelf fadein">
            <PageHead title="Personal Information"/>           
            <div className="comp-form-panel ssn-panel colaba-form">
                <div className="row form-group_">
                    <div className="col-md-12">
                        <BorrowerConsentTabs />
                    </div>
                </div>
            </div>
        </div>              
    )
}
