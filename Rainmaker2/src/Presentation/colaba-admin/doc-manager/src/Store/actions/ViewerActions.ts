import { PDFThumbnails } from "../../Utilities/PDFThumbnails";
import axios from 'axios';
import { ViewerActionsType } from "../reducers/ViewerReducer";

export class ViewerActions {
    
    static resetInstance = (dispatch:Function) => {
        dispatch({ type: ViewerActionsType.SetInstance, payload: null })
        dispatch({ type: ViewerActionsType.SetCurrentFile, payload: null });
        dispatch({ type: ViewerActionsType.SetSelectedFileData, payload: null });
    }
}