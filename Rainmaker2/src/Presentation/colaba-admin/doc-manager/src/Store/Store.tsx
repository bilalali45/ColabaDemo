import { Http } from "rainsoft-js";
import React, { createContext, useReducer } from "react";
import { mainReducer } from "./reducers/reducers";
import { TemplateType } from "./reducers/TemplatesReducer";
import { ToolbarType } from "./reducers/ToolbarReducer";
import { ViewerType } from "./reducers/ViewerReducer";

const baseUrl: any = window?.envConfig?.API_BASE_URL;
const httpClient = new Http(baseUrl, 'Rainmaker2Token');

export type InitialStateType = {
    toolbar: ToolbarType | {};
    documents: DocumentType | {};
    viewer: ViewerType | {};
    templateManager: TemplateType | {};
    // documentsSelection: {}
}

export const initialState: InitialStateType = {
    toolbar: {},
    documents: {},
    viewer: {},
    templateManager: {}
}

const Store = createContext<{
    state: InitialStateType;
    dispatch: React.Dispatch<any>;
}>({
    state: initialState,
    dispatch: () => null
});

const StoreProvider: React.FC = ({ children }) => {
    const [state, dispatch] = useReducer(mainReducer, initialState);

    return (
        <Store.Provider value={{ state, dispatch }}>
            {children}
        </Store.Provider>
    )
}

export { Store, StoreProvider }



