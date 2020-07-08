import React, { useState, useContext } from 'react'
import { Store } from '../../../store/store';


type AlertBoxType = {
    hideAlert: Function
}

export const AlertBox = ({ hideAlert }: AlertBoxType) => {

    const { state, dispatch } = useContext(Store);
    const { pendingDocs, currentDoc }: any = state.documents;

    // const yesHandler = () => {

    // }
    // const noHandler = () => {

    // }


    return (
        <div className="alert-box" id="AlertBox" data-component="AlertBox">
            {/* <div className="backdrop"></div> */}
            <div className="alert-box--modal">
                <button className="alert-box--modal-close" onClick={() => { hideAlert() }} ><em className="zmdi zmdi-close"></em></button>
                <header className="alert-box--modal-header">
                    <h2 className="text-center">Are you sure, you want to proceed!</h2>
                </header>
                <section className="alert-box--modal-body">
                    <p>Some files are still in progess, please complete them before navigating away!</p>
                    {/* <p>Some files are still in progess, please complete them before proceeding to the next Document</p> */}
                    {/* <p>If you select yes, all selected files will be lost!</p> */}
                </section>
                {/* <footer className="alert-box--modal-footer">
                    <p className="text-center">
                        <button className="btn btn-primary btn-small">Yes</button> <button className="btn btn-default btn-small">No</button>
                    </p>
                </footer> */}
            </div>
        </div>
    )
}
