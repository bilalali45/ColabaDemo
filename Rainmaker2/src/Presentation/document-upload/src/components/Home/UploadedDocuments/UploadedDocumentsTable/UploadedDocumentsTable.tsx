import React, { useState, useEffect } from 'react'
import { UploadedDocuments } from '../../../../entities/Models/UploadedDocuments'
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { Auth } from '../../../../services/auth/Auth';
import { Document } from '../../../../entities/Models/Document'
import moment from 'moment';
import * as momentDate from 'moment';
//import DocUploadIcon from '../../../assets/images/upload-doc-icon.svg';
import DocUploadIcon from '../../../../assets/images/upload-doc-icon.svg';

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
                return <a href="" className="block-element">{item.clientName}</a>
            })}
        </td>
    }

    const renderAddedColumn = (data) => {
        return <td>
            {data.map((item: Document) => {
                return (
                    <span className="block-element">{
                        moment(new Date(item.fileUploadedOn)).format('MM-DD-YYYY HH:mm')
                        }
                        
                        </span>
                )
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
             {!docList &&
            <div className="no-document">               

                 <div className="no-document--wrap">
                        <div className="no-document--img">
                            <img src={DocUploadIcon} alt="Your don't have any files!" />
                        </div>
                        <label htmlFor="inputno-document--text">
                            Your don't have any files.<br/>
                            Go to <a href="http://localhost:3000/DocumentManagement/uploadedDocuments">Loan Home</a>
                        </label>
                </div>  
                
            </div>
             }
        </div>
    )
}
