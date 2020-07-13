import React, { useEffect, useContext } from 'react'
import { TemplateHeader } from './TemplateHeader/TemplateHeader'
import { TemplateHome } from './TemplateHome/TemplateHome'
// import { Http } from 'rainsoft-js'
import { TemplateActions } from '../../../Store/actions/TemplateActions'
import { Store } from '../../../Store/Store'
import { TemplateActionsType } from '../../../Store/reducers/TemplatesReducer'

// const http = new Http()

export const TemplateManager = () => {

    return (
        <main className="ManageTemplate-Wrap">
            <TemplateHeader/>
            <TemplateHome/>
        </main>
    )
}
