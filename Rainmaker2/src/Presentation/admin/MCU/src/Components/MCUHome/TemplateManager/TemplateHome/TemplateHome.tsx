import React, { useState } from 'react'
import { TemplateListContainer } from './TemplateListContainer/TemplateListContainer'
import { SelectedTemplate } from './SelectedTempate/SelectedTemplate';

export const nameTest = /^[ A-Za-z0-9-\s]*$/i;

export const TemplateHome = () => {

    const [loaderVisible, setLoaderVisible] = useState<boolean>(false);

    return (
        <section className="MT-CWrap">
            <div className="container-mcu">
                <div className="row">
                    <div className="col-sm-4">
                        <div className="MT-leftbar">
                            <TemplateListContainer 
                                setLoaderVisible={setLoaderVisible}/>

                        </div>
                    </div>
                    <div className="col-sm-8">
                        <div className="MT-rightbar">
                            <SelectedTemplate 
                            setLoaderVisible={setLoaderVisible}
                            loaderVisible={loaderVisible}
                            />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}
