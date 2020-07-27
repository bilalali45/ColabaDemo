import React from 'react'
import { Link } from 'react-router-dom'

export const AddNeedListHeader = () => {
    return (
        <section className="MTheader">
            <div className="addneedlist-actions">
<button className="btn btn-sm btn-secondary">Close</button>
<button className="btn btn-sm btn-primary">Save as Close</button>
            </div>
        </section>
    )
}
