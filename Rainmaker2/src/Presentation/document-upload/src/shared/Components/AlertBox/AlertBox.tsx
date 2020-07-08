import React, { useState } from 'react'


type AlertBoxType = {
    hideAlert: Function
}

export const AlertBox = ({hideAlert} : AlertBoxType) => {

    return (
        <div className="alert-box" id="AlertBox" data-component="AlertBox">
            {/* <div className="backdrop"></div> */}
            <div className="alert-box--modal">
                <button className="alert-box--modal-close" onClick={()=>{hideAlert()}} ><em className="zmdi zmdi-close"></em></button>
                <header className="alert-box--modal-header">
                    <h1 className="text-center">Helo world</h1>
                </header>
                <section className="alert-box--modal-body">
                    <p>Warning! Some files are still in progess, please complete them before proceeding to the next Document</p>
                </section>
                <footer className="alert-box--modal-footer">
                    <p className="text-center"><button className="btn btn-primary">OK</button></p>
                </footer>
            </div>            
        </div>
    )
}
