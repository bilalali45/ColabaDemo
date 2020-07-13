import React from 'react'
import { TemplateListContainer } from './TemplateListContainer/TemplateListContainer'
import { TemplatesCheckList } from './TemplatesCheckList/TemplatesCheckList'
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
                    <TemplatesCheckList/>
                    </div>
                </div>
            </div>
            </div>



            


            {/* <TemplatesCheckList/>
            <NewTemplate/> */}
        </section>
    )
}
