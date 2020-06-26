import React, { useState, useEffect, useContext } from 'react'
import { DocumentItem } from './DocumentItem/DocumentItem'
import { DocumentView } from '../../../../../shared/Components/DocumentView/DocumentView'
import { Http } from '../../../../../services/http/Http';
import { Store } from '../../../../../store/store';
import { Auth } from '../../../../../services/auth/Auth';
import { Document } from '../../../../../entities/Models/Document';

const httpClient = new Http();


type SelectedDocumentsType = {
    addMore: Function,
    files: Document[],
    url: string
}

export const SelectedDocuments = ({ files, url, addMore }: SelectedDocumentsType) => {

    const [showingDoc, setShowingDoc] = useState<boolean>(false);
    const [selectedFiles, setSelectedFiles] = useState<Document[]>(files);
    const [currentDoc, setCurrentDoc] = useState<File | null>(null);
    const [fileType, setFileType] = useState<string>('');
    const [uploadedPercent, setUploadPercent] = useState<number>();
    const [showProgressBar, setShowProgressBar] = useState<boolean>();

    const { state, dispatch } = useContext(Store);
    const documents: any = state.documents;
    const currentSelected: any = documents.currentDoc;

    useEffect(() => {
        console.log('in here!!', selectedFiles);
        setSelectedFiles(files);
        console.log('currentSelected', currentSelected);
    }, [files.length])

    console.log('selectedFiles', selectedFiles);

    const data = new FormData();

    const viewDocument = (file: File) => {
        setShowingDoc(true);
        setFileType(file.type);
        setCurrentDoc(file);
    }

    const closeDocumentView = () => {
        setShowingDoc(false);
    }

    const uploadFile = async () => {
        let fields = ['id', 'requestId', 'docId'];
        const fileOrder: any[] = [];
        for (const file of files) {
            if (file.file) {
                data.append('files', file.file, `${file.clientName}`);
            }

            for (const field of fields) {
                const value = currentSelected[field];
                data.append(field, value);
            }
            let counter = 1;
            for (const file of files) {
                fileOrder.push({
                    fileName: file?.file?.name,
                    order: counter
                });
                counter++;
            }
            data.append('order', JSON.stringify(fileOrder));
            data.append('tenantId', Auth.getTenantId());
    
            setShowProgressBar(true);
            try {
                let res = await httpClient.fetch({
                    method: httpClient.methods.POST,
                    url,
                    data,
                    onUploadProgress: e => {
                        let p = (e.loaded / e.total * 100);
                        setUploadPercent(p);
                    }
                }, {
                    Authorization: `Bearer ${Auth.getAuth()}`
                });
                setShowProgressBar(false);
            } catch (error) {
            }
        }

      
    }

    const changeName = (file: Document, newName: string) => {

        setSelectedFiles((prevFiles: Document[]) => {
            return prevFiles.map((f: Document) => {
                if (f.file && f.file.name === file?.file?.name) {
                    f.clientName = `${newName}`;
                    return f;
                }
                return f;
            })
        })
    }

    return (
        <section className="file-drop-box-wrap">
            <div className="file-drop-box havefooter">


                <div className="list-selected-doc">
                    <ul className="doc-list-ul">
                        {
                            selectedFiles.map((f, index) => {
                                return (
                                    <DocumentItem
                                        file={f}
                                        viewDocument={viewDocument}
                                        changeName={changeName}
                                        key={index}
                                    />
                                )
                            })
                        }
                    </ul>
                    <div className="addmore-wrap">
                        <a className="addmoreDoc" onClick={(e) => addMore(e)}>Add more files</a>
                    </div>
                </div>

                {showingDoc ? <DocumentView
                    file={currentDoc}
                    type={fileType}
                    url={`http://localhost:5000/pdf/${currentDoc?.name}`}
                    hide={closeDocumentView} />
                    : ''}
                {/* {showProgressBar && <progress value={uploadedPercent} max="100">{uploadedPercent + '%'}</progress>} */}


            </div>
            <div className="doc-upload-footer">
                <div className="doc-submit-wrap">
                    <button className="btn btn-primary" onClick={uploadFile}>Submit</button>
                </div>

                <div className="doc-confirm-wrap d-none">

                    <div className="row">
                        <div className="col-sm-8">
                            <div className="dc-text">
                                <p>Are you done with this Bank statement?</p>

                            </div>

                        </div>

                        <div className="col-sm-4">
                            <div className="dc-actions">
                                <button className="btn btn-small btn-secondary" onClick={uploadFile}>No</button>
                                <button className="btn btn-small btn-primary" onClick={uploadFile}>Yes</button>
                            </div>
                        </div>

                    </div>

                </div>

            </div>
        </section>
    )
}
