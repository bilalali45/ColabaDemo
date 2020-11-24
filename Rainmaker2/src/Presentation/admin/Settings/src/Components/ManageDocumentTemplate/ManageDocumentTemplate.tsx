import React, { useContext, useEffect, useRef, useState } from 'react';
import { TemplateActionsType } from '../../Store/reducers/TemplatesReducer';
import { Store } from '../../Store/Store';
import Loader from '../Shared/Loader';
import ManageDocumentTemplateBody from './_ManageDocumentTemplate/ManageDocumentTemplateBody';
import ManageDocumentTemplateHeader from './_ManageDocumentTemplate/ManageDocumentTemplateHeader';


export const MyTemplate = "MCU Template";
export const TenantTemplate = "Tenant Template";
export const SystemTemplate = "System Template";

const ManageDocumentTemplate = () => {
    
    return (
        <>
            <ManageDocumentTemplateHeader/>
            <ManageDocumentTemplateBody/>         
        </>
    )
}

export default ManageDocumentTemplate;