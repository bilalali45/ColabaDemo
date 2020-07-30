import React from 'react'
import { ReviewNeedListRequestHeader } from './ReviewNeedListRequestHeader/ReviewNeedListRequestHeader'
import { ReviewNeedListRequestHome } from './ReviewNeedListRequestHome/ReviewNeedListRequestHome'

export const ReviewNeedListRequest = () => {
    return (
        <div className="mcu-panel">
            <ReviewNeedListRequestHeader></ReviewNeedListRequestHeader>
            <ReviewNeedListRequestHome></ReviewNeedListRequestHome>
        </div>
    )
}
