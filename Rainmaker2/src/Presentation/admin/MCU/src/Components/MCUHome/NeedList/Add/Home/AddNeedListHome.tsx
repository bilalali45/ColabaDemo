import React, { useState } from 'react'
import { AddNeedListContainer } from './AddNeedListContainer/AddNeedListContainer'
import { SelectedDocument } from './SelectedDocument/SelectedDocument';
export const nameTest = /^[ A-Za-z0-9-\s]*$/i;

export const AddNeedListHome = () => { 
    const [loaderVisible, setLoaderVisible] = useState<boolean>(false);
    return (
        <section className="MT-CWrap">
            <div className="container-mcu">
                <div className="row">
                    <div className="col-sm-4">
                        <div className="MT-leftbar">
                        <AddNeedListContainer 
                            setLoaderVisible={setLoaderVisible}
                            loaderVisible={loaderVisible}
                                />

                        </div>
                    </div>
                    <div className="col-sm-8">
                        <div className="MT-rightbar">
                        <SelectedDocument />
                        </div>
                    </div>
                </div>
            </div>
        </section>
    )
}
