import React, { useState, useEffect } from 'react'
import { Route, Redirect, useHistory } from 'react-router-dom'
import { Auth } from '../../../services/auth/Auth'
import { UserActions } from '../../../store/actions/UserActions';

export const Authorized = ({ component: Component, ...props }) => {

    return <Route {...props} render={(props) => {
        if (Auth.checkAuth()) {
            return <Component {...props} />
        }
        return <Redirect to="/accounts/login" />
    }} />
}
