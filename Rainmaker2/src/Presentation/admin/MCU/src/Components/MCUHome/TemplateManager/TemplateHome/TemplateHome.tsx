import React, { useState, useRef } from 'react'
import { TemplateListContainer } from './TemplateListContainer/TemplateListContainer'
import { SelectedTemplate } from './SelectedTempate/SelectedTemplate';

export const nameTest = /^[ A-Za-z0-9-\s]*$/i;

export const TemplateHome = () => {

    const [loaderVisible, setLoaderVisible] = useState<boolean>(false);

    const templateListContainerRef = useRef<HTMLDivElement>(null);


    // const setListContainerElement = (el: HTMLDivElement) => {
    //     templateListContainerRef = el;
    // }

    return (
        <section data-testid="template-home" className="MT-CWrap">
            <div className="container-mcu">
                <div className="row">
                    <div className="col-sm-4">
                        <div className="MT-leftbar">
                            <TemplateListContainer
                                listContainerElRef={templateListContainerRef}
                                setLoaderVisible={setLoaderVisible} />

                        </div>
                    </div>
                    <div className="col-sm-8">
                        <div className="MT-rightbar">
                            <SelectedTemplate
                                listContainerElRef={templateListContainerRef}
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
