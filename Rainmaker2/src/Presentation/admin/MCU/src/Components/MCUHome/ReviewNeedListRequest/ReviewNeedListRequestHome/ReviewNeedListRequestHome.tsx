import React from 'react'
import { SelectedNeedListReview } from './SelectedNeedListReview/SelectedNeedListReview'
import { EmailContentReview } from './EmailContentReview/EmailContentReview'

export const ReviewNeedListRequestHome = () => {
    return (
        <div>
            <SelectedNeedListReview></SelectedNeedListReview>
            <EmailContentReview></EmailContentReview>
        </div>
    )
}
