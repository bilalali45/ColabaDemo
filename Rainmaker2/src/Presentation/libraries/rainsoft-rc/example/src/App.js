import React from 'react'

import { RainsoftRcHeader, RainsoftRcFooter } from 'rainsoft-rc'

import 'rainsoft-rc/dist/index.css'
const callBackFunction = () => {
  console.log('Call back function')
  }
  const listArr = [
    {name:'Item1',callback:callBackFunction },
    {name:'Item2',callback:callBackFunction },
    {name:'Item3',callback:callBackFunction }
  ]
const logo = '';
const name = 'Jehangir Babul';

const headerProps = {
  logoSrc:'',
  displayName:"Jehangir Babul",
  displayNameOnClick: callBackFunction,
  options: listArr
}

const App = () => {
  return (
    <>
      <RainsoftRcHeader headerProps= {headerProps}/>
      
      <RainsoftRcFooter
      title='Dummy'
      streetName='abc 123'
      address='Dallas America'
      phoneOne='(888) 76 5546'
      phoneTwo = '(888) 76 5546'
      contentOne ='This is called “contextual typing”, a form of type inference. This helps cut down on the amount of effort to keep your program typed'
      contentTwo = 'This is called “contextual typing”, a form of type inference. This helps cut down on the amount of effort to keep your program typed'
      nmlLogoSrc = ''
      nmlUrl='http://www.nmlsconsumeraccess.org/Home.aspx/SubSearch?searchText=277676'
      
      />
    </>
  )
}

export default App
