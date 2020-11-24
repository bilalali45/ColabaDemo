import React from 'react'
import { UserProfileListBody } from './_UserProfileList/UserProfileListBody'
import { UserProfileListHeader } from './_UserProfileList/UserProfileListHeader'


type Props = {
    backHandler?: Function
}

export const UserProfileList = ({backHandler}: Props) => {
    return (
        <>
            <UserProfileListHeader/>
            <UserProfileListBody backHandler = {backHandler} />
        </>
    )
}
