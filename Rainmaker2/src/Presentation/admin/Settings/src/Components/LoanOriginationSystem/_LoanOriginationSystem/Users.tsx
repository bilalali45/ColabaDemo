import React from 'react'
import ContentBody from "../../Shared/ContentBody";
import {RequestEmailTemplateActionsType} from "../../../Store/reducers/RequestEmailTemplateReducer";
import ContentFooter from "../../Shared/ContentFooter";
import TableSort from "../../Shared/SettingTable/TableSort";


type Props = {
  //backHandler?: Function
}

export const LOSUsers = ({  }: Props) => {

  return (
      <>
          <ContentBody className="loan-origination-system los-user-list">
              <table className="table table-striped request-email-templates-records">
                  <colgroup>
                      <col span={1} style={{ width: '1%' }} />
                      <col span={2} style={{ width: '10%' }} />
                      <col span={2} style={{ width: '15%' }} />
                      <col span={3} />
                  </colgroup>
                  <thead>
                  <tr>
                      <th scope="col"></th>
                      <th data-testid="th-templateName" scope="col"><TableSort>Name</TableSort></th>
                      <th scope="col">Byte User Name</th>
                      <th scope="col"></th>
                  </tr>
                  </thead>
                  <tbody>
                  <tr>
                      <td className="dp"><figure><img src="https://via.placeholder.com/100"/></figure></td>
                      <td>Nathaniel Poole</td>
                      <td>
                          <input className={`settings__control`} type="text" value="Nathaniel.Poole" />
                      </td>
                      <td></td>
                  </tr>
                  <tr>
                      <td className="dp"><figure><img src="https://via.placeholder.com/100"/></figure></td>
                      <td>Mario Speedwagon</td>
                      <td>
                          <input className={`settings__control`} type="text" value="Mario.speedwagon" />
                      </td>
                      <td></td>
                  </tr>
                  <tr>
                      <td className="dp"><figure><img src="https://via.placeholder.com/100"/></figure></td>
                      <td>Paul Molive</td>
                      <td>
                          <input className={`settings__control`} type="text" value="Paul.molive" />
                      </td>
                      <td></td>
                  </tr>
                  <tr>
                      <td className="dp"><figure><img src="https://via.placeholder.com/100"/></figure></td>
                      <td>Anna Sthesia</td>
                      <td>
                          <input className={`settings__control`} type="text" value="Anna.sthesia" />
                      </td>
                      <td></td>
                  </tr>
                  <tr>
                      <td className="dp"><figure><img src="https://via.placeholder.com/100"/></figure></td>
                      <td>Nick R. Bocker</td>
                      <td>
                          <input className={`settings__control`} type="text" value="nick.bocker" />
                      </td>
                      <td></td>
                  </tr>
                  </tbody>
              </table>
          </ContentBody>
          <ContentFooter>
              <button data-testid= "save-btn" type="submit" className="settings-btn settings-btn-primary">Save</button>
              <button data-testid= "cancel-btn" className="settings-btn settings-btn-secondry">Cancel</button>
          </ContentFooter>
      </>
  )
}
