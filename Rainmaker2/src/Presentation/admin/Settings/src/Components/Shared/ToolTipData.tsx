import React from 'react';
import { ToolTipEnum } from '../../Utils/helpers/Enums';

export const ToolTipData = (props: any) => {
    
    if (props.type === ToolTipEnum.NotificationHeader) {
        return (
            <div data-testid="ToolTipData">
                <h5>Notification settings</h5>
                <p>Configure your notification settings (bell on top right) for each borrower action. You will only receive notifications for loans you are assigned to.</p>

                <h5>Notification frequency</h5>
                <p><strong>Immediate</strong> – Receive a notification immediately after a borrower's action.</p>
                <p><strong>Delayed</strong> – Receive a notification of the borrower’s actions after the specified delay timer has elapsed.</p>
            </div>
        )
    }
    else if (props.type === ToolTipEnum.ManageDocumentTemplateHeader) {
        return (
            <div data-testid="ToolTipData">
                <h5>Document Templates:</h5>
                <p>Document Templates help you quickly request documents from borrowers with common loan scenarios. A Document Template is a set of documents grouped together. These templates are available whenever you request documents.</p>
            </div>
        )
    }
    else if (props.type === ToolTipEnum.RequestEmailTemplates) {
        return (
            <div data-testid="ToolTipData">
                <h5>Request Email Templates:</h5>
                <p>Request email templates help employees quickly draft emails that will be sent to borrowers when requesting documents. As an administrator, you can configure the templates your employees will see and specify certain default values. </p>
                <p><strong>Please note that you are only setting default values below and that users will be able to edit those default values when requesting items.</strong></p>
            </div>
        )
    } else if (props.type === ToolTipEnum.DocumentSubmit) {
        return (
            <div data-testid="ToolTipData">
                <h5>Document Submit</h5>
                <p>Notification is sent when the borrower uploads documents on the portal.</p>
            </div>
        )
    } else if (props.type === ToolTipEnum.LoanApplicationSubmitted) {
        return (
            <div data-testid="ToolTipData">
                <h5>Loan Application Submitted</h5>
                <p>Notification is sent when the borrower uploads documents on the portal.</p>
            </div>
        )
    } else if (props.type === ToolTipEnum.LoanFunded) {
        return (
            <div data-testid="ToolTipData">
                <h5>Loan Fund</h5>
                <p>Notification is sent when the borrower uploads documents on the portal.</p>
            </div>
        )
    } else if (props.type === ToolTipEnum.SortOrder) {
        return (
            <div data-testid="ToolTipData">
                <h5>Sort Order</h5>
                <p>Configure the order of the dropdown list your users will use when they send Needs Lists to borrowers. The first item on this list will populate by default.</p>
            </div>
        )
    } else if (props.type === ToolTipEnum.TokenList) {
        return (
            <div data-testid="ToolTipData">
                <h5>Tokens</h5>
                <p>You can add dynamic tokens to the fields below. These will populate with borrower specific data when an employee selects a template. </p>
                <p>For example, specifying the ###BorrowerFirstName### token will populate with the Borrower’s first name into the field. </p>
            </div>
        )
    } else if (props.type === ToolTipEnum.ByteSoftInteg) {
        return (
            <div data-testid="ToolTipData">
                <h5>Byte Software Integration settings</h5>
                <p>Configure your Byte software integration here. For additional configurations, please contact Colaba support.</p>
            </div>
        )
    } else if(props.type === ToolTipEnum.LoanStatusUpdate){
        return (
            <div data-testid="ToolTipData">
                <h5>Loan Status Update</h5>
                <p>Colaba allows you to email the borrower when their loan application status changes. You can configure how the borrower is notified below.</p>
            </div>
        )
    } else if(props.type === ToolTipEnum.SendEmailAfter){
        return (
            <div data-testid="ToolTipData">
                <h5>Send Email After</h5>
                <p>Specify how long you want to wait, after the status is updated, to notify the borrower.</p>
            </div>
        )
    }
    else
        return <div data-testid="ToolTipData"> </div>

}
