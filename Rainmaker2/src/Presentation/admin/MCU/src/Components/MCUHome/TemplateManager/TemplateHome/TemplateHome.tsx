import React from 'react'
import { TemplateListContainer } from './TemplateListContainer/TemplateListContainer'
import { SelectedTemplate } from './SelectedTempate/SelectedTemplate'
import { NewTemplate } from '../NewTemplate/NewTemplate'

export const TemplateHome = () => {
    return (
        <section className="MT-CWrap">
            <div className="container-mcu">
                <div className="row">
                    <div className="col-sm-4">
                        <div className="MT-leftbar">
                            <TemplateListContainer />

                        </div>
                    </div>
                    <div className="col-sm-8">
                        <div className="MT-rightbar">
                            <SelectedTemplate />
                            {/* <NewTemplate/> */}
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}
