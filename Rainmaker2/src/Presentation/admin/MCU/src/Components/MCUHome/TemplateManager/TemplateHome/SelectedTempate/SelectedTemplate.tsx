import React from 'react'
import { TemplateItemsList } from './TemplateItemsList/TemplateItemsList'
import { AddDocument } from '../../AddDocument/AddDocument'

export const SelectedTemplate = () => {
    return (
        <div>
            <h1>TemplatesCheckList</h1>
            <TemplateItemsList/>
            <AddDocument/>
        </div>
    )
}
