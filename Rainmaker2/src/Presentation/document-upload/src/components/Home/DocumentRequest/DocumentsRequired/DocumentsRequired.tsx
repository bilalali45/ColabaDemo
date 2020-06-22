import React, { useEffect, useContext } from 'react'
import { DocumentActions } from '../../../../store/actions/DocumentActions';
import { Store } from '../../../../store/store';
import { isArray } from 'util';

export const DocumentsRequired = () => {

    const { state, dispatch } = useContext(Store);
    const { pendingDocs }: any = state.documents;

    useEffect(() => {
        fetchPendingDocs();
    }, [])

    const fetchPendingDocs = async () => {
        DocumentActions.getPendingDocuments('1', '1');
    }

    console.log(state);

    const renderRequiredDocs = () => {

        if (pendingDocs) {
            console.log('pendingDocs', isArray(pendingDocs));
            console.log('pendingDocs', pendingDocs);
            return (
                <ul>
                    {
                        pendingDocs.map((p: any) => {
                            return (
                                <li>
                                    <a className="active"><span> {p.docName}</span></a>
                                </li>
                            )
                        })
                    }
                </ul>
            )
        }
        return '';
    }


    return (
        <div className="dr-list-wrap">
            <nav>


                {
                    pendingDocs && renderRequiredDocs()
                }
                {/* <ul>
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


                </ul> */}
            </nav>

        </div>
    )
}
