import React from 'react';
import {ReviewDocumentHeader} from './ReviewDocumentHeader/ReviewDocumentHeader';
import {ReviewDocumentStatement} from './ReviewDocumentStatement/ReviewDocumentStatement';
import {DocumentView} from './../../../Shared/DocumentView';

export const ReviewDocument = () => {
    return (
        <div id="ReviewDocument" data-component="ReviewDocument" className="review-document">
            <ReviewDocumentHeader />
            
            <div className="review-document-body row">

                <div className="review-document-body--content col-md-8">                    
                    <DocumentView />
                </div>{/* review-document-body--content */}

                <aside className="review-document-body--aside col-md-4">
                    <ReviewDocumentStatement />
                </aside>{/* review-document-body--aside */}

            </div>{/* review-document-body */}
        </div>
    )
}