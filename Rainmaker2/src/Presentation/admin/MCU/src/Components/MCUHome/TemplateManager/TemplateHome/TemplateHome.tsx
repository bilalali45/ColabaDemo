import React from 'react'
import { TemplateListContainer } from './TemplateListContainer/TemplateListContainer'
import { SelectedTemplate } from './SelectedTempate/SelectedTemplate'
import { NewTemplate } from '../NewTemplate/NewTemplate'

export const TemplateHome = () => {
    return (
        <section>
            <TemplateListContainer/>
            <SelectedTemplate/>
            <NewTemplate/>
        </section>
    )
}
