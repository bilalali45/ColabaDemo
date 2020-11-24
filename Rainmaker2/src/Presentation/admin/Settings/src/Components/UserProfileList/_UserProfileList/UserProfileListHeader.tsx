import React from 'react'

export const UserProfileListHeader = () => {
    return (
        <div className={`settings__manage-users--subheader`}>
            <div className={`settings__manage-users--search-area`}>
                <label className={`settings__label`}>User Profile</label>
                <input className={`settings__control settings__control-search`} type="text" placeholder="Search.." ></input>
            </div>
        </div>
    )
}
