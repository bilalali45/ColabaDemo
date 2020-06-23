import React, { useState, useEffect } from 'react'
import { UploadedDocuments } from '../../../../entities/Models/UploadedDocuments'
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { Auth } from '../../../../services/auth/Auth';
export const UploadedDocumentsTable = () => {
    
const [docList, setDocList] = useState<UploadedDocuments[] | null>(null)

 useEffect(()=>{
     if(!docList?.length){
        fetchUploadedDocuments();
     }
   
 },[]);    
    
  const fetchUploadedDocuments = async () => {
   let uploadedDocs = await DocumentActions.getSubmittedDocuments(Auth.getLoanAppliationId(), Auth.getTenantId());
   console.log('uploadedDocs',uploadedDocs)
   if(uploadedDocs){
    setDocList(uploadedDocs);
   }
  }  
    
    
    
    const renderUploadedDocs = (data) => {
        return data.map((item: any) => {
            debugger
            console.log('item',item)
            return (
                <tbody>
                    <tr>
                        <td><em className="far fa-file"></em> Bank Statement</td>
                        <td>
                            <a href="" className="block-element">Bank-statement-jan-to-mar-2020-1.jpg</a>
                            <a href="" className="block-element">Bank-statement-jan-to-mar-2020-2.jpg</a>
                            <a href="" className="block-element">Bank-statement-jan-to-mar-2020-3.jpg</a>
                        </td>
                        <td>
                            <span className="block-element">12-04-2020 17:30</span>
                            <span className="block-element">12-04-2020 17:30</span>
                            <span className="block-element">12-04-2020 17:30</span>
                        </td>
                    </tr>

                </tbody>
            )
        })
    }
    
    return (
        <div className="UploadedDocumentsTable">
            <table className="table  table-striped">
                <thead>
                    <tr>
                        <th>Documents</th>
                        <th>File Name</th>
                        <th>Added</th>
                    </tr>
                </thead>
               { docList &&
               renderUploadedDocs(docList)
               } 
            </table>
        </div>
    )
}
