import React from 'react'
// import { LoanStatus } from './LoanStatus/LoanStatus'
import { LoanProgress } from './LoanProgress/LoanProgress'
import { DocumentStatus } from './DocumentsStatus/DocumentStatus'
import { ContactUs } from './ContactUs/ContactUs'
import Williams_Jack from '../../../assets/images/Williams_Jack.jpg';

export class Activity extends React.Component {

    state = {
        tasks: [
            {task:'Bank Statement'}, 
            {task:'W-2s 2017'}, 
            {task:'W-2s 2018'}, 
            {task:'Personal Tax Returns'}
        ]
    }

    // { this.state.tasks.map( item => {
    //     return <li> {item.task} </li>
    // })}

    passData(){
        return this.state.tasks.map( item => {
            return <li> {item.task} </li>
        })
    }

    render(){
        return (
            <div>
                <section className="page-content">
                    <div className="container">
                        <div className="row gutter15">
                            <div className="col-md-5">
                                {/* <LoanStatus /> */}
                                <LoanProgress />
                            </div>
                            <div className="col-md-7">
                                <DocumentStatus 
                                    heading="Document Request" 
                                    counts={8} 
                                    moreTask="/#" 
                                    getStarted="/#" 
                                    tasks={this.state.tasks}
                                />
                                
                                <ContactUs  
                                    userName="Williams Jack" 
                                    userId={254545} 
                                    userContact="(888) 971-1254" 
                                    userEmail="Williams.jack@texastrustloans.com"
                                    userWebsite="www.texatrustloans.com"
                                    userImg={Williams_Jack}
                                />
                            </div>
                        </div>
                    </div>
                </section>
                {/* <h1>Activity</h1>
                <LoanStatus/>
                <LoanProgress/>
                <DocumentStatus/>
                <ContactUs/> */}
            </div>
        )
    }
}
