import React from 'react'
import { ManageUsersBody } from './_ManageUsers/ManageUsersBody'
import { ManageUsersHeader } from './_ManageUsers/ManageUsersHeader'

 const ManageUsers = ()=> {
    return (
        <div className={`settings__manage-users`} data-testid="manageuser">
            <ManageUsersHeader/>
            <ManageUsersBody/>
        </div>
    )
}

export default ManageUsers;