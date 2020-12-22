import React, { useContext, useEffect, useState, useRef } from 'react'
import { DocumentItem } from './DocumentItem/DocumentItem';
import DocumentActions from '../../../../../Store/actions/DocumentActions';
import { Store } from '../../../../../Store/Store';
import { DocumentActionsType } from '../../../../../Store/reducers/documentsReducer';
import { ViewerActionsType } from '../../../../../Store/reducers/ViewerReducer';
import { LocalDB } from '../../../../../Utilities/LocalDB';
import { async } from 'q';
import { CurrentInView } from '../../../../../Models/CurrentInView';
import { AnnotationActions } from '../../../../../Utilities/AnnotationActions';
import { SelectedFile } from '../../../../../Models/SelectedFile';


export const DocumentsTable = () => {
    const refReassignDropdown = useRef<HTMLDivElement>(null);
    const { state, dispatch } = useContext(Store);

    const documents: any = state.documents;
    const documentItems: any = documents?.documentItems;
    const catScrollFreeze: any = documents?.catScrollFreeze;
    let loanApplicationId = LocalDB.getLoanAppliationId();
    const {isLoading, selectedFileData}:any  = state.viewer;
    const [fileClicked, setFileClicked]= useState<boolean>(false);

    const [openedReassignDropdown,setOpenReassignDropdown] = useState<boolean>(false);

    useEffect(() => {
        if (!documentItems) {
            DocumentActions.getCurrentDocumentItems(dispatch, true);
            checkIsByteProAuto()
        }
    }, [!documentItems]);

    const checkIsByteProAuto = async () => {
        let res: any = await DocumentActions.checkIsByteProAuto();
        let isAuto = res?.syncToBytePro != 2 ? true : false;
        dispatch({type: DocumentActionsType.SetIsByteProAuto, payload: isAuto});
      };

    return (
        <div id="c-DocTable" className="dm-docTable c-DocTable" >

            <div className="dm-dt-thead">
                <div className="dm-dt-thead-left">Document</div>
                <div className="dm-dt-thead-right">Status</div>
            </div>

            <div className={`dm-dt-tbody ${catScrollFreeze?" freeze":""}`} ref={refReassignDropdown}>

                {
                    documentItems && documentItems.length ?( documentItems?.map((d: any, i: number) => {
                        return (
                            <DocumentItem key={i}
                                docInd={i}
                                document={d}
                                refReassignDropdown={refReassignDropdown}
                                setFileClicked={setFileClicked}
                                fileClicked = {fileClicked}
                                setOpenReassignDropdown={setOpenReassignDropdown}
                            />
                        )
                    })
                    ):null
                }

            </div>

        </div>
    )
}
