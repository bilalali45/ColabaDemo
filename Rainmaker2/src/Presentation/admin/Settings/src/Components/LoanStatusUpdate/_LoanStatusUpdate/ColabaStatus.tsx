import React, {useContext, useState, useEffect} from 'react'
import { LoanStatus } from '../../../Entities/Models/LoanStatusUpdate';
import { Store } from '../../../Store/Store';
import ContentHeader, { ContentSubHeader } from '../../Shared/ContentHeader';
import { ColabaStatusContent } from './ColabaStatusContent'


type ColabaStatusProps = {
    setDisableSaveBtn: Function;
    setShowEmailContentScreen: Function;
    error: string;
    setError : Function;
};

export const ColabaStatus = ({setDisableSaveBtn, setShowEmailContentScreen, error, setError}: ColabaStatusProps) => {
    const {state, dispatch} = useContext(Store);
    const loanStatusManager: any = state.loanStatusManager;
    const selectedLoanStatus: LoanStatus = loanStatusManager.selectedLoanStatus;

    const [activeColabaStatus, setActiveColabaStatus] = useState(0);
    const [fromStatus, setFromStatus] = useState<string>();
    const [toStatus, setToStatus] = useState<string>();
    

    useEffect(() => {
        if(selectedLoanStatus){
            setFromStatus(selectedLoanStatus.mcuName)
            setToStatus(selectedLoanStatus?.toStatus);
        }    
    }, [selectedLoanStatus])

    const statusSelectionHandler = (selection: number) =>{
        if(!selectedLoanStatus) return;
        setActiveColabaStatus(selection);
    }

    return (
        <>
            <ContentHeader title="Colaba Status" className="colaba-status-header"></ContentHeader>
            <div className="colaba-status-headers flex">
                
                <div data-testid="from-header" className={`col-md-6 no-padding csh--left`} onClick={()=>statusSelectionHandler(0)}>
                    <ContentSubHeader className={`colaba-status-subheader ${activeColabaStatus == 0 ?'active':''}`}>
                        <label>From</label>
                        <h5 data-testid="from-status-text" className="h5">{fromStatus}</h5>
                    </ContentSubHeader>
                </div>

                <div data-testid="to-header" className={`col-md-6 no-padding csh--right`} onClick={()=>statusSelectionHandler(1)} title={error?'Select any item from list':''}>
                    <ContentSubHeader className={`colaba-status-subheader ${!selectedLoanStatus ? 'disabled': ''} ${error ?  "error" : ''} ${activeColabaStatus == 1 ?'active':''}`}>
                        <label>To</label>
                        <h5 className="h5">{toStatus}</h5>
                    </ContentSubHeader>
                </div>

            </div>
            <ColabaStatusContent
            activeColabaStatus = {activeColabaStatus}
            setActiveColabaStatus = {setActiveColabaStatus}
            setDisableSaveBtn= {setDisableSaveBtn}
            setShowEmailContentScreen = {setShowEmailContentScreen}
            setError = {setError}
            />
        </>
    )
}
