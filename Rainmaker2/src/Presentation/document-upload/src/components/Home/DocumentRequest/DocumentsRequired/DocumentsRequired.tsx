import React, {useEffect} from 'react'
import { DocumentActions } from '../../../../store/actions/DocumentActions';

export const DocumentsRequired = () => {

    useEffect(() => {
        fetchPendingDocs();
    }, [])

    const fetchPendingDocs = async () => {
        DocumentActions.getPendingDocuments('1', '1');
    }

    return (
        <div>
            <p>Documents Required</p>
        </div>
    )
}
