import React, {useState} from 'react';
import ContentHeader from '../../Shared/ContentHeader';
import { Toggler } from '../../Shared/Toggler';

interface LoanStatusUpdateHeaderProps{
    enable:boolean;
    handlerEnabled:()=>void;
}

export const LoanStatusUpdateHeader:React.FC<LoanStatusUpdateHeaderProps> = ({enable,handlerEnabled}) => {
    return (
        <>
            <ContentHeader title="Loan Status Update" tooltipType={10} className="loan-status-update-header">
            <span data-testid="disEna" className="disable-enabled">Disable/Enable <Toggler checked={enable} handlerClick={handlerEnabled} /></span>
            </ContentHeader>
        </>
    )
}
