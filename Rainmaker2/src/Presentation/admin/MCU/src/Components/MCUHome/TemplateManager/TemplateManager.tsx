import React, { useEffect, useContext } from 'react'
import { TemplateHeader } from './TemplateHeader/TemplateHeader'
import { TemplateHome } from './TemplateHome/TemplateHome'
// import { Http } from 'rainsoft-js'
import { TemplateActions } from '../../../Store/actions/TemplateActions'
import { Store } from '../../../Store/Store'
import { TemplateActionsType } from '../../../Store/reducers/TemplatesReducer'

// const http = new Http()

export const TemplateManager = () => {

    const { state, dispatch } = useContext(Store)

    useEffect(() => {
        fetchTemplates();
    }, [])

    const fetchTemplates = async () => {
        try {
            let res: any = await TemplateActions.fetchTemplates('1');
            dispatch({ type: TemplateActionsType.FetchTemplates, payload: res });
            //    if(res.data) {

            //    }
            console.log(res);
        } catch (error) {
            dispatch({ type: TemplateActionsType.FetchTemplates, payload: error });
        }
    }

    return (
        <main className="ManageTemplate-Wrap">
            <TemplateHeader/>
            <TemplateHome/>
        </main>
    )
}
