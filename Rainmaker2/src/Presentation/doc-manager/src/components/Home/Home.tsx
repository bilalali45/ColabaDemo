import React, { useState, useEffect, useContext } from 'react'
import { DocumentsContainer } from './DocumentsContainer/DocumentsContainer'
import { ViewerContainer } from './ViewerContainer/ViewerContainer'

import { DocumentActionsType } from '../../Store/reducers/documentsReducer';
import { Store } from '../../Store/Store';

import { useParams } from "react-router";
import { LOSSyncAlert } from './LOSSyncAlert/LOSSyncAlert';
import { ParamsService } from '../../Utilities/helpers/ParamService';
import { LocalDB } from '../../Utilities/LocalDB';
import DocumentActions from '../../Store/actions/DocumentActions';
import { UserActions } from '../../Store/actions/UserActions';
import { ViewerActionsType } from '../../Store/reducers/ViewerReducer';
import { Loader } from '../../Utilities/Loader';
import { AlertLoader } from '../../Utilities/AlertLoader';
import { ConfirmationAlert } from './ConfirmationAlert/ConfirmationAlert';


let timer: any;

export const Home = () => {
  const [synchingComplete, setSynchingComplete] = useState(false);

  const { state, dispatch } = useContext(Store);
  const { loanApplicationId }: any = useParams();
  ParamsService.storeParams(loanApplicationId);
  const documents: any = state?.documents;
  const filesToSync: any = documents?.filesToSync;
  const isSynching: any = documents?.isSynching;

  DocumentActions.loanApplicationId = LocalDB.getLoanAppliationId();

  const viewer: any = state?.viewer;
  const isNeedListLocked = viewer?.isNeedListLocked;


  const { currentFile, isLoading, isFileChanged, showingConfirmationAlert, isSaving }: any = state.viewer;

  useEffect(() => {

    retainLock();
    if (isNeedListLocked?.lockUserName === LocalDB.getUserPayload()?.UserName) {
      timer = setInterval(() => {
        retainLock();
      }, 1000 * window?.envConfig?.LOCK_RETAIN_DURATION);
    }
    return () => {
      clearInterval(timer);
    }
  }, [isNeedListLocked?.lockUserName]);

  const retainLock = async () => {
    let lockRetained = await UserActions.retainLock();
    dispatch({ type: ViewerActionsType.SetIsNeedListLocked, payload: lockRetained })
  }
  useEffect(() => {
    setStateProps();
  }, []);

  useEffect(() => {
    if (isFileChanged) {
      window.onbeforeunload = (e: any) => {
        e.preventDefault();
        e.returnValue = ''
      };
    } else {
      window.onbeforeunload = null;
    }
  }, [isFileChanged, !isFileChanged])

  const setStateProps = async () => {
    await dispatch({ type: DocumentActionsType.SetFailedDocs, payload: [] })
    await dispatch({ type: DocumentActionsType.SetDocumentItems, payload: [] })
    await dispatch({type: ViewerActionsType.SetAnnotationsFirstTime, payload: true});
  }
  return (
    <section className="c-Home loader-parent-Wrap"
      onDragEnd={(e: any) => {
        e.preventDefault();
        // e.dataTransfer.setData('file', JSON.stringify(file));
        dispatch({ type: DocumentActionsType.SetIsDragging, payload: false });
        dispatch({ type: DocumentActionsType.SetIsDraggingCurrentFile, payload: false });
      }}>
      {/* {isLoading && <Loader containerHeight={"153px"} />} */}
      {isLoading && <AlertLoader content="Loading File. Please wait..." />}
      {isSaving && <AlertLoader content="Saving File. Please wait.."/>}
      <div className="c-Home-wrap">
        <div className="c-Home-left">
          <DocumentsContainer />
          {filesToSync?.length ? <LOSSyncAlert /> : ''}
        </div>

        <div className="c-Home-right">
          <ViewerContainer />
        </div>
      </div>
      { isFileChanged && showingConfirmationAlert  ? <ConfirmationAlert /> : ''}
    </section>
  )
}
