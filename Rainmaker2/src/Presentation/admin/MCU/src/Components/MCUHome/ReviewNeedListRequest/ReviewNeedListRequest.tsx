import React, { useContext, useEffect } from 'react'
import { ReviewNeedListRequestHeader } from './ReviewNeedListRequestHeader/ReviewNeedListRequestHeader'
import { ReviewNeedListRequestHome } from './ReviewNeedListRequestHome/ReviewNeedListRequestHome'

export const ReviewNeedListRequest = () => {
    
    const saveAsDraft = () => {
        console.log('Save as Draft');
    }

    console.log('Parent')
    return (
        <div className="mcu-panel">
            <ReviewNeedListRequestHeader
             saveAsDraft={saveAsDraft}
            />
            <ReviewNeedListRequestHome
            />
        </div>
    )
}
