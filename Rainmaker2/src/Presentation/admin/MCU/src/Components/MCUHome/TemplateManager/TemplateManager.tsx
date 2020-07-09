import React from 'react'
import { TemplateHeader } from './TemplateHeader/TemplateHeader'
import { TemplateHome } from './TemplateHome/TemplateHome'

export const TemplateManager = () => {
    return (
        <main>
            <TemplateHeader/>
            <TemplateHome/>
        </main>
    )
}
