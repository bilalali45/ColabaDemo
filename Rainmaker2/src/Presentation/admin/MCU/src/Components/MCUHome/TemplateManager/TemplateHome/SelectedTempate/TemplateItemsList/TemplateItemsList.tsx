import React, { useContext, useEffect } from 'react'
import { TemplateItem } from '../TemplateItem/TemplateItem'
import { Store } from '../../../../../../Store/Store'
import { TemplateActions } from '../../../../../../Store/actions/TemplateActions';
import { TemplateActionsType } from '../../../../../../Store/reducers/TemplatesReducer';

export const TemplateItemsList = () => {

    const {state, dispatch} = useContext(Store);

    const templateManager : any = state.templateManager;
    const templates = templateManager?.templates;
    

    useEffect(() => {   
        if(!templates) {
            fetchSelectedTemplete();
        }
    }, []);

    const fetchSelectedTemplete = async () => {
        let templateDocs = await TemplateActions.fetchTemplateDocuments('1');
        if(templateDocs) {
            dispatch({type: TemplateActionsType.SetTemplateDocuments, payload: templateDocs});
        }
    }


    return (
        <div>
            <h1>TemplateItemsList</h1>
            <TemplateItem/>
        </div>
    )
}
