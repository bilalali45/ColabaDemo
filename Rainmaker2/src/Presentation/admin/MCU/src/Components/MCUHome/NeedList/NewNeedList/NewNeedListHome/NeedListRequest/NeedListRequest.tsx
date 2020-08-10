import React, { useState, useContext, useEffect, ChangeEvent } from 'react'
import { AddDocument } from '../../../../TemplateManager/AddDocument/AddDocument';

import { clear } from 'console';
import { NeedListRequestItem } from './NeedListRequestItem/NeedListRequestItem';
import { Document } from '../../../../../../Entities/Models/Document';
import { DocumentRequest } from '../../../../../../Entities/Models/DocumentRequest';
import { TemplateDocument } from '../../../../../../Entities/Models/TemplateDocument';
import { Template } from '../../../../../../Entities/Models/Template';
import { NeedListSelect } from '../../../NeedListSelect/NeedListSelect';

import emptyIcon from '../../../../../../Assets/images/empty-icon.svg'
import { nameTest } from '../../../Add/Home/AddNeedListHome';
import Spinner from 'react-bootstrap/Spinner';
import { isDocumentDraftType } from '../../../../../../Store/reducers/TemplatesReducer';
import { CustomDocuments } from '../../../../TemplateManager/AddDocument/SelectedDocumentType/CustomDocuments/CustomDocuments';

export const MyTemplate = "MCU Template";
export const TenantTemplate = "Tenant Template";
export const SystemTemplate = "System Template";

type AddNeedListContainerType = {
    currentDocument: TemplateDocument | null,
    changeDocument: Function,
    documentList: TemplateDocument[],
    loaderVisible: boolean,
    setLoaderVisible: Function,
    addDocumentToList: Function,
    templateList: Template[],
    addTemplatesDocuments: Function,
    isDraft: isDocumentDraftType,
    viewSaveDraft: Function,
    saveAsTemplate: Function,
    templateName: string,
    changeTemplateName: Function,
    removeDocumentFromList: Function,
    requestSent: boolean,
    showSaveAsTemplateLink: boolean,
}


export const NeedListRequest = ({
    loaderVisible,
    setLoaderVisible,
    documentList,
    changeDocument,
    currentDocument,
    addDocumentToList,
    templateList,
    addTemplatesDocuments,
    isDraft,
    viewSaveDraft,
    saveAsTemplate,
    changeTemplateName,
    templateName,
    removeDocumentFromList,
    requestSent,
    showSaveAsTemplateLink }: AddNeedListContainerType) => {

    const [showSaveAsTemplate, setShowSaveAsTemplate] = useState<boolean>(false);
    const [templateNameError, setTemplateNameError] = useState<string>();
    const [requestHit, setRequestHit] = useState<boolean>(false);

    useEffect(() => {
        setLoaderVisible(false);
    }, []);

    const toggleSaveAsTemplate = () => setShowSaveAsTemplate(!showSaveAsTemplate);

    const validateTemplateName = (e: ChangeEvent<HTMLInputElement>) => {
        let { target: { value } } = e;
        changeTemplateName(e);
        setTemplateNameError('');

        if (value?.length > 255) {
            setTemplateNameError('Name must be less than 256 chars');
            return;
        }

        if (templateList.find((t: Template) => t.name.trim() === value.trim())) {
            setTemplateNameError(`Template name must be unique`);
            return;
        };

        if (!nameTest.test(value)) {
            setTemplateNameError('Template name cannot contain any special characters');
        }
    }

    console.log('needlee', showSaveAsTemplateLink)
    const renderNoDocumentSelect = () => {
        return (
            <div className="no-preview">
                <div>
                    <div className="icon-wrap">
                        <img src={emptyIcon} alt="" />
                    </div>
                    <h2>Nothing</h2>
                    <p>You have not added any document</p>
                </div>
            </div>
        )
    }



    const renderDocumentList = () => {
        if (!documentList?.length) {
            return renderNoDocumentSelect()
        }
        return (
            <>
                <div className="m-template">
                    <div className="MT-groupList">
                        <div className="list-wrap my-temp-list">
                            <ul>
                                {
                                    documentList?.map((d: TemplateDocument) => {

                                        return <NeedListRequestItem
                                            key={d?.localId}
                                            isSelected={currentDocument?.localId === d?.localId}
                                            // isSelected={currentDocument?.docName?.toLowerCase() === d?.docName?.toLowerCase()}
                                            changeDocument={changeDocument}
                                            document={d}
                                            removeDocumentFromList={removeDocumentFromList}
                                        />

                                    })
                                }
                            </ul>
                        </div>
                    </div>
                </div>
            </>
        );
    };


    const renderSaveAsTemplate = () => {
        return (
            <div className="save-template">
                <input
                    onKeyDown={(e: any) => {
                        let { keyCode, target: { value } } = e;
                        console.log('in here you know werhe', keyCode);
                        if (keyCode === 13) {
                            if (!value?.trim()?.length) {
                                setTemplateNameError('Template name cannot be empty');
                                return;
                            }
                        }
                    }}
                    maxLength={255}
                    style={{ border: templateNameError && '1px solid red' }}
                    value={templateName}
                    onChange={validateTemplateName}
                    className="form-control"
                    type="text"
                    placeholder="Template Name"
                    autoFocus
                />
                <div className="save-template-btns">
                    <button className="btn btn-sm btn-secondry" onClick={toggleSaveAsTemplate}>Close</button>
                    {" "}
                    <button className="btn btn-sm btn-primary" onClick={async () => {
                        setRequestHit(true);
                        await saveAsTemplate();
                        setRequestHit(false);
                        toggleSaveAsTemplate();
                    }}>Save</button>
                </div>
            </div>
        )
    }


    // if(!templates) return  <Loader containerHeight={"100%"} />;

    return (
        <div className="TL-container">

            <div className="head-TLC">

                <h4>Request Needs List</h4>

                <div className="btn-add-new-Temp">

                    <AddDocument
                        needList={documentList}
                        addDocumentToList={addDocumentToList}
                        setLoaderVisible={setLoaderVisible}
                        popoverplacement="right-end"
                    />

                    {/* <button className="btn btn-primary addnewTemplate-btn">
                        <span className="btn-text">Add document</span>
                        <span className="btn-icon">
                            <i className="zmdi zmdi-plus"></i>
                        </span>

                    </button> */}
                </div>
            </div>

            {requestSent || !isDraft
                ?
                <div className="flex-center">
                    <Spinner animation="border" role="status">
                        <span className="sr-only">Loading...</span>
                    </Spinner>
                </div>


                : <div className="listWrap-templates">
                    {renderDocumentList()}

                    {/* Remove Message */}


                </div>}

            {requestHit ?
                <div className="left-footer text-center alert alert-success">Template has been created.</div>
                :
                <div className="left-footer">


                    {showSaveAsTemplate ?
                        <>
                            {renderSaveAsTemplate()}
                            {templateNameError && <p style={{ color: 'red' }}>{templateNameError}</p>}
                        </>
                        :
                        <div className="btn-wrap">
                            <NeedListSelect
                                showButton={false}
                                templateList={templateList?.filter((t: Template) => t.name)}
                                addTemplatesDocuments={addTemplatesDocuments}
                                viewSaveDraft={viewSaveDraft}
                            />
                            {showSaveAsTemplateLink ? <a
                                onClick={toggleSaveAsTemplate}
                                className="btn-link link-primary">
                                Save as template
                        </a> : ''}
                        </div>}
                </div>}
        </div>
    )
}
