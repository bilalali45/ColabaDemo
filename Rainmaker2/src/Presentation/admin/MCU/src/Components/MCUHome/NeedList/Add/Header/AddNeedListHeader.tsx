import React from 'react'
import { Link } from 'react-router-dom'

export const AddNeedListHeader = () => {
    return (
        <section className="MTheader">
            <h2>Add Need List</h2>

            <Link to={'/needList'} className="close-ManageTemplate"><i className="zmdi zmdi-close"></i></Link>
        </section>
    )
}
