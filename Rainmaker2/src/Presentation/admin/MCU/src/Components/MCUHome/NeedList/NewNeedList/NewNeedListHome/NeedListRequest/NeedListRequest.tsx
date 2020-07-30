import React, { useState, useContext, useEffect } from 'react'
import { AddDocument } from '../../../../TemplateManager/AddDocument/AddDocument';

import { clear } from 'console';
import { NeedListRequestItem } from './NeedListRequestItem/NeedListRequestItem';
import { Document } from '../../../../../../Entities/Models/Document';
import { DocumentRequest } from '../../../../../../Entities/Models/DocumentRequest';
import { TemplateDocument } from '../../../../../../Entities/Models/TemplateDocument';
import { Template } from '../../../../../../Entities/Models/Template';
import { NeedListSelect } from '../../../NeedListSelect/NeedListSelect';
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
    templateList: Template[];
    redirectToDocumentRequest: Function;
    isDraft: string;
    viewSaveDraft: Function;
}


export const NeedListRequest = ({
    loaderVisible,
    setLoaderVisible,
    documentList,
    changeDocument,
    currentDocument,
    addDocumentToList,
    templateList, 
    redirectToDocumentRequest, 
    isDraft, 
    viewSaveDraft }: AddNeedListContainerType) => {

    useEffect(() => {
        setLoaderVisible(false);
    }, []);

    const renderDocumentList = () => {
        return (
            <>
                <div className="m-template">
                    <div className="MT-groupList">
                        <div className="list-wrap my-temp-list">
                            <ul>
                                {
                                    documentList?.map((d: TemplateDocument) => {

                                        return <NeedListRequestItem
                                            isSelected={currentDocument?.docId === d.docId}
                                            changeDocument={changeDocument}
                                            document={d}
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




    // if(!templates) return  <Loader containerHeight={"100%"} />;

    return (
        <div className="TL-container">

            <div className="head-TLC">

                <h4>Request Needs List</h4>

                <div className="btn-add-new-Temp">

                    <AddDocument
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

            <div className="listWrap-templates">
                {renderDocumentList()}
            </div>

            <div className="left-footer">
                <div className="btn-wrap">
                    <NeedListSelect
                        showButton={false}
                        templateList={templateList}
                        redirectToDocumentRequest={redirectToDocumentRequest}
                        viewSaveDraft={viewSaveDraft}
                        isDraft={isDraft}
                    />
                    <a className="btn-link link-primary">Save as template</a>
                </div>
            </div>
        </div>
    )
}
