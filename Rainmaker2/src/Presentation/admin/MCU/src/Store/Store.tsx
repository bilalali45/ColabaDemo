import React, { createContext, useReducer } from 'react'
import { mainReducer } from './reducers/reducers'
import { TemplateType } from './reducers/TemplatesReducer'

export type InitialStateType = {
    user: {
        userInfo: {}
    },
    templates: TemplateType | {},
}

export const InitialState = {
    user: {
        userInfo: {}
    },
    templates: {},
}

const Store = createContext<{
    state: InitialStateType,
    dispatch: React.Dispatch<any>
}>({
    state: InitialState,
    dispatch: () => null
})

const StoreProvider: React.FC = ({ children }) => {

    const [state, dispatch] = useReducer(mainReducer, InitialState)

    return (
        <Store.Provider value={{ state, dispatch }}>
            {children}
        </Store.Provider>
    )
}

export {
    Store, StoreProvider
}