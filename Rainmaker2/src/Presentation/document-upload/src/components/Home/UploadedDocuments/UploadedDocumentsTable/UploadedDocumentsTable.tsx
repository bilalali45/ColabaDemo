import React, { useState, useEffect } from 'react'
import { UploadedDocuments } from '../../../../entities/Models/UploadedDocuments'
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { Auth } from '../../../../services/auth/Auth';
import { Document } from '../../../../entities/Models/Document'
import { DateFormat } from '../../../../utils/helpers/DateFormat';

export const UploadedDocumentsTable = () => {

    const [docList, setDocList] = useState<UploadedDocuments[] | null>(null)

    useEffect(() => {
        if (!docList?.length) {
            fetchUploadedDocuments();
        }
    }, []);

    const fetchUploadedDocuments = async () => {
        let uploadedDocs = await DocumentActions.getSubmittedDocuments(Auth.getLoanAppliationId(), Auth.getTenantId());
        console.log('uploadedDocs', uploadedDocs)
        if (uploadedDocs) {
            setDocList(uploadedDocs);
        }
    }
    const renderFileNameColumn = (data) => {
        return <td>
            {data.map((item: Document) => {
                return <tr><a href="" className="block-element">{item.clientName}</a></tr>
            })}
        </td>
    }

    const renderAddedColumn = (data) => {
        return <td>
            {data.map((item: Document) => {
                return <tr><span className="block-element">{DateFormat(item.fileUploadedOn, true)}           
                    </span></tr>
            })}
        </td>
    }

    const renderUploadedDocs = (data) => {
        return data.map((item: UploadedDocuments) => {
            if (!item.files.length)
                return;
            return (
                <tbody>
                    <tr>
                        <td><em className="far fa-file"></em> {item.docName}</td>
                        {renderFileNameColumn(item.files)}
                        {renderAddedColumn(item.files)}

                    </tr>
                </tbody>
            )
        })
    }

    return (
        <div className="UploadedDocumentsTable">
          {docList &&
            <table className="table  table-striped">
                <thead>
                    <tr>
                        <th>Documents</th>
                        <th>File Name</th>
                        <th>Added</th>
                    </tr>
                </thead>
                
                    {renderUploadedDocs(docList)}
               
            </table>
             }
             {docList?.length === 0 &&
            <div>No document</div>
             }
        </div>
    )
}
