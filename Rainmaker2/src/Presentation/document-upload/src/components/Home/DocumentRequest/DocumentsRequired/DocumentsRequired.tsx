import React, { useEffect } from 'react'
import { DocumentActions } from '../../../../store/actions/DocumentActions';

export const DocumentsRequired = () => {

    useEffect(() => {
        fetchPendingDocs();
    }, [])

    const fetchPendingDocs = async () => {
        DocumentActions.getPendingDocuments('1', '1');
    }

    return (
        <div className="dr-list-wrap">
            <nav>
                <ul>
                    <li>
                        <a className="active"><span> Bank Statement</span></a>
                    </li>
                    <li>
                        <a> <span> W-2s 2017</span></a>
                    </li>
                    <li>
                        <a>  <span>W-2s 2018</span></a>
                    </li>
                    <li>
                        <a> <span> Personal Tax Returns</span></a>
                    </li>
                    <li>
                        <a>
                            <span> Alimony Income Verification</span></a>
                    </li>
                    <li>
                        <a> <span> Home Insurance</span></a>
                    </li>
                    <li>
                        <a> <span> W-2s 2018</span></a>
                    </li>
                    <li>
                        <a>  <span>Personal Tax Returns</span></a>
                    </li>
                    <li>
                        <a> <span> W-2s 2017</span></a>
                    </li>
                    <li>
                        <a> <span>Alimony Income Verification</span></a>
                    </li>

                    <li>
                        <a> <span>Alimony Income Verification</span></a>
                    </li>

                    <li>
                        <a> <span>Alimony Income Verification</span></a>
                    </li>

                    <li>
                        <a> <span>Alimony Income Verification</span></a>
                    </li>

                    <li>
                        <a> <span>Alimony Income Verification</span></a>
                    </li>

                    <li>
                        <a> <span>Alimony Income Verification</span></a>
                    </li>

                    <li>
                        <a> <span>Alimony Income Verification</span></a>
                    </li>

                    <li>
                        <a> <span>Alimony Income Verification</span></a>
                    </li>

                    <li>
                        <a> <span>Alimony Income Verification</span></a>
                    </li>


                </ul>
            </nav>

        </div>
    )
}
