import React from 'react'

export const UploadedDocumentsTable = () => {
    return (
        <div className="UploadedDocumentsTable">
            <table className="table  table-striped">
                <thead>
                    <tr>
                        <th>Documents</th>
                        <th>File Name</th>
                        <th>Added</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><em className="far fa-file"></em> Bank Statement</td>
                        <td>
                            <a href="" className="block-element">Bank-statement-jan-to-mar-2020-1.jpg</a>
                            <a href="" className="block-element">Bank-statement-jan-to-mar-2020-2.jpg</a>
                            <a href="" className="block-element">Bank-statement-jan-to-mar-2020-3.jpg</a>
                        </td>
                        <td>
                            <span className="block-element">12-04-2020 17:30</span>
                            <span className="block-element">12-04-2020 17:30</span>
                            <span className="block-element">12-04-2020 17:30</span>
                        </td>
                    </tr>
                    <tr>
                        <td><em className="far fa-file"></em> W-2s 2017</td>
                        <td>
                            <a href="" className="block-element">W-2s-2017.pdf</a>
                        </td>
                        <td>
                            <span className="block-element">12-04-2020 17:30</span>
                        </td>
                    </tr>
                    <tr>
                        <td><em className="far fa-file"></em> W-2s 2018</td>
                        <td>
                            <a href="" className="block-element">W-2s-2018.pdf</a>
                        </td>
                        <td>
                            <span className="block-element">12-04-2020 17:30</span>
                        </td>
                    </tr>
                    <tr>
                        <td><em className="far fa-file"></em> Personal Tax Returns</td>
                        <td>
                            <a href="" className="block-element">Personal-Tax-Returns.pdf</a>
                        </td>
                        <td>
                            <span className="block-element">12-04-2020 17:30</span>
                        </td>
                    </tr>
                    <tr>
                        <td><em className="far fa-file"></em> Tax Transcripts</td>
                        <td>
                            <a href="" className="block-element">Tax-Transcripts.pdf</a>
                        </td>
                        <td>
                            <span className="block-element">12-04-2020 17:30</span>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    )
}
