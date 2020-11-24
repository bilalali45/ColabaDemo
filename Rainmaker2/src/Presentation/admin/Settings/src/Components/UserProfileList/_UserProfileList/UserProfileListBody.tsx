import React from 'react'


type Props = {
  backHandler?: any
}

export const UserProfileListBody = ({ backHandler }: Props) => {






  return (
    <div className={`settings__manage-users--subbody`}>
      <table className={`table table-striped`}>
        <thead>
          <tr>
            <th>Name</th>
            <th>Contact</th>
            <th>Country</th>
          </tr>
        </thead>
        <tbody>
          <tr onClick={(e) => backHandler()}>
            <td>Alfreds Futterkiste</td>
            <td>Maria Anders</td>
            <td>Germany</td>
          </tr>
          <tr onClick={(e) => backHandler()}>
            <td>Centro comercial Moctezuma</td>
            <td>Francisco Chang</td>
            <td>Mexico</td>
          </tr>
          <tr onClick={(e) => backHandler()}>
            <td>Ernst Handel</td>
            <td>Roland Mendel</td>
            <td>Austria</td>
          </tr>
          <tr onClick={(e) => backHandler()}>
            <td>Island Trading</td>
            <td>Helen Bennett</td>
            <td>UK</td>
          </tr>
          <tr onClick={(e) => backHandler()}>
            <td>Laughing Bacchus Winecellars</td>
            <td>Yoshi Tannamuri</td>
            <td>Canada</td>
          </tr>
          <tr onClick={(e) => backHandler()}>
            <td>Magazzini Alimentari Riuniti</td>
            <td>Giovanni Rovelli</td>
            <td>Italy</td>
          </tr>
        </tbody>
      </table>
    </div>
  )
}
