import React from 'react'
import ContentBody from "../../Shared/ContentBody";
import TableSort from "../../Shared/SettingTable/TableSort";



type Props = {
  backHandler?: Function
}

export const LOSOrganization = ({ backHandler }: Props) => {

  return (
      <ContentBody className="loan-origination-system los-organization-list">
          <table className="table table-striped request-email-templates-records">
              <colgroup>
                  <col span={1} style={{ width: '2%' }} />
                  <col span={2} style={{ width: '30%' }} />
                  <col span={2} style={{ width: '15%' }} />
                  <col span={3} />
              </colgroup>
              <thead>
              <tr>
                  <th scope="col"></th>
                  <th data-testid="th-templateName" scope="col"><TableSort>Name</TableSort></th>
                  <th scope="col">Byte Organization Code</th>
                  <th scope="col"></th>
              </tr>
              </thead>
              <tbody>
              <tr>
                  <td className="dp"><figure><img src="https://via.placeholder.com/100"/></figure></td>
                  <td>Texas Trust Home Loans</td>
                  <td>302039</td>
                  <td></td>
              </tr>
              <tr>
                  <td className="dp"><figure><img src="https://via.placeholder.com/100"/></figure></td>
                  <td>AHC Lending</td>
                  <td>100390</td>
                  <td></td>
              </tr>
              </tbody>
          </table>
      </ContentBody>
  )
}
