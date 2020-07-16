import React, { useState } from 'react'
import { TemplateListContainer } from './TemplateListContainer/TemplateListContainer'
import { SelectedTemplate } from './SelectedTempate/SelectedTemplate'

export const TemplateHome = () => {

    const [addingNew, setAddingNew] = useState<boolean>(false);

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
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}
