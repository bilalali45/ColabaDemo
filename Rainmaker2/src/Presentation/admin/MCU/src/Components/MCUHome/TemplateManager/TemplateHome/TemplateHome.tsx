import React from 'react'
import { TemplateListContainer } from './TemplateListContainer/TemplateListContainer'
import { TemplatesCheckList } from './TemplatesCheckList/TemplatesCheckList'
import { NewTemplate } from '../NewTemplate/NewTemplate'

export const TemplateHome = () => {
    return (
        <div>
            <h1>TemplateHome</h1>
            <TemplateListContainer/>
            <TemplatesCheckList/>
            <NewTemplate/>
        </div>
    )
}
