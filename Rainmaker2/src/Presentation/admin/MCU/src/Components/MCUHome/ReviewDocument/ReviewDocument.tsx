import React from 'react';
import {ReviewDocumentHeader} from './ReviewDocumentHeader/ReviewDocumentHeader';

export const ReviewDocument = () => {
    return (
        <div id="ReviewDocument" data-component="ReviewDocument" className="review-document">
            <ReviewDocumentHeader />
            <div className="review-document-body">

                <div className="review-document-body--content">
                    
                    <h2>File Viewver</h2>

                </div>{/* review-document-body--content */}

                <aside className="review-document-body--aside">

                </aside>{/* review-document-body--aside */}

            </div>{/* review-document-body */}
        </div>
    )
}