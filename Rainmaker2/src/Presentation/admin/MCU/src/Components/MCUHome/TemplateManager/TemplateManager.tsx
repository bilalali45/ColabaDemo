import React from 'react'
import { TemplateHeader } from './TemplateHeader/TemplateHeader'
import { TemplateHome } from './TemplateHome/TemplateHome'

export const TemplateManager = () => {
    return (
        <div>
            <h1>TemplateManager</h1>
            <TemplateHeader/>
            <TemplateHome/>
        </div>
    )
}
