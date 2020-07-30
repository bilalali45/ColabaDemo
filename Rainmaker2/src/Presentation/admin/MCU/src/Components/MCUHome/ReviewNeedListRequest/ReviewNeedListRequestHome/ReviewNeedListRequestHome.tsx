import React from 'react'
import { SelectedNeedListReview } from './SelectedNeedListReview/SelectedNeedListReview'
import { EmailContentReview } from './EmailContentReview/EmailContentReview'

export const ReviewNeedListRequestHome = () => {
    return (
        <div className="mcu-panel-body">
            <div className="row">
                <div className="col-md-4 no-padding mcu-panel-body--col">
                    <SelectedNeedListReview></SelectedNeedListReview>
                </div>
                <div className="col-md-8 no-padding mcu-panel-body--col">
                    <EmailContentReview></EmailContentReview>
                </div>
            </div>            
        </div>
    )
}
