import React from 'react'

export const Header = () => {
    return (
        <header data-testid = "main-header"  className="settings__header">
            <h2  className="h2">Settings</h2>
            <button className="settings-btn pull-right settings-btn-goback"><i className="zmdi zmdi-close"></i></button>
        </header>
    )
}
