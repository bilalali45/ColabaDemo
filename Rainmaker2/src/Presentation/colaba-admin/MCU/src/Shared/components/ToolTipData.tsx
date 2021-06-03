import React from 'react';
import { ToolTipEnum } from '../../Utils/helpers/Enums';

export const ToolTipData = (props: any) => {
    
    if (props.type === ToolTipEnum.NotificationHeader) {
        return (
            <>
                <h5>Notification settings</h5>
                <p>Configure your notification settings (bell on top right) for each borrower action. You will only receive notifications for loans you are assigned to.</p>

                <h5>Notification frequency</h5>
                <p><strong>Immediate</strong> – Receive a notification immediately after a borrower's action.</p>
                <p><strong>Delayed</strong> – Receive a notification of the borrower’s actions after the specified delay timer has elapsed.</p>
            </>
        )
    }
    else if (props.type === ToolTipEnum.ManageDocumentTemplateHeader) {
        return (
            <>
                <h5>Manage Document settings</h5>
                <p>Configure your notification settings (bell on top right) for each borrower action. You will only receive notifications for loans you are assigned to.</p>

                <h5>Manage Document frequency</h5>
                <p><strong>Immediate</strong> – Receive a notification immediately after a borrower's action.</p>
                <p><strong>Delayed</strong> – Receive a notification of the borrower’s actions after the specified delay timer has elapsed.</p>
            </>
        )
    } else if (props.type === ToolTipEnum.DocumentSubmit) {
        return (
            <>
                <h5>Document Submit</h5>
                <p>Notification is sent when the borrower uploads documents on the portal.</p>
            </>
        )
    } else if (props.type === ToolTipEnum.LoanApplicationSubmitted) {
        return (
            <>
                <h5>Loan Application Submitted</h5>
                <p>Notification is sent when the borrower uploads documents on the portal.</p>
            </>
        )
    } else if (props.type === ToolTipEnum.LoanFunded) {
        return (
            <>
                <h5>Loan Fund</h5>
                <p>Notification is sent when the borrower uploads documents on the portal.</p>
            </>
        )
    } else if (props.type === ToolTipEnum.SortOrder) {
        return (
            <>
                <h5>Sort Order</h5>
                <p>Configure the order of the dropdown list your users will use when they send Needs Lists to borrowers. The first item on this list will populate by default.</p>
            </>
        )
    } else if (props.type === ToolTipEnum.TokenList) {
        return (
            <>
                <h5>Token List</h5>
                <p>Configure the order of the dropdown list your users will use when they send Needs Lists to borrowers. The first item on this list will populate by default.</p>
            </>
        )
    }
    else
        return <> </>

}
