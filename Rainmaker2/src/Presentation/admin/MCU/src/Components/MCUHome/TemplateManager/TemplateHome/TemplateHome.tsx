import React from 'react'
import { TemplateListContainer } from './TemplateListContainer/TemplateListContainer'
import { TemplatesCheckList } from './TemplatesCheckList/TemplatesCheckList'
import { NewTemplate } from '../NewTemplate/NewTemplate'

export const TemplateHome = () => {
    return (
        <section>
            <TemplateListContainer/>
            <TemplatesCheckList/>
            <NewTemplate/>
        </section>
    )
}
