import React from 'react'
import { Link } from 'react-router-dom'

export const TemplateHeader = () => {
    return (
        <section className="MTheader">
            <h2>Manage Templates</h2>

            <Link to={'/needList'} className="close-ManageTemplate"><i className="zmdi zmdi-close"></i></Link>
        </section>
    )
}
