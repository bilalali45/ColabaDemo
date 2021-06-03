import React from 'react'
import { BrowserRouter as Router, Switch } from 'react-router-dom'
import Home from './components/Home/Home'
import { Authorized } from './shared/Components/Authorized/Authorized'



export const RouterHandler = () => {
    return (
        <>
          {process.env.NODE_ENV === 'test' ? <Authorized
                path="/"
                component={Home}
            /> : <Authorized
                    path="/:navigation/:loanApplicationId"
                    component={Home}
                />
            }
            <Authorized path="/:loanApplicationId" component={Home} />
        </>
       
    )
}

export const RemoteRouter = () => {
    return <RouterHandler/>
}


export const LocalRouter = () => {
    return (
        <Router basename="/loanportal">
             <Switch>
                <RouterHandler/>
          </Switch>
        </Router>
    )
}
