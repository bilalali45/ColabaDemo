import React, { useState, useContext, useEffect } from 'react'
import { Items } from './Items/Items';
import { AddDocument } from '../../../../TemplateManager/AddDocument/AddDocument';

import { clear } from 'console';
export const MyTemplate = "MCU Template";
export const TenantTemplate = "Tenant Template";
export const SystemTemplate = "System Template";
type AddNeedListContainerType = {
    loaderVisible: boolean;
    setLoaderVisible: Function;
}


export const AddNeedListContainer = ({ loaderVisible, setLoaderVisible }: AddNeedListContainerType) => {
    const Itemslist = [0, 1, 3]


    useEffect(() => {
        setLoaderVisible(false);
    }, []);






    const MyTemplates = () => {
        return (
            <>
                <div className="m-template">
                    <div className="MT-groupList">
                        <div className="list-wrap my-temp-list">
                            <ul>
                                {
                                    Itemslist.map((index: any) => {

                                        return <Items
                                            key={index}

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
                {/* My Templates */}
                {MyTemplates()}

            </div>

            <div className="left-footer">
                <div className="btn-wrap">
                    <a className="btn-link link-primary">Add from template</a>
                    <a className="btn-link link-primary">Save as template</a>
                </div>
            </div>
        </div>
    )
}
