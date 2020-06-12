import React from 'react'
// import { LoanStatus } from './LoanStatus/LoanStatus'
import { LoanProgress } from './LoanProgress/LoanProgress'
import { DocumentStatus } from './DocumentStatus/DocumentStatus'
import { ContactUs } from './ContactUs/ContactUs'
import contactAvatar from '../../../assets/images/contact-avatar-icon.svg';

export class Activity extends React.Component {

    state = {
        tasks: [
            {task:'Bank Statement'}, 
            {task:'W-2s 2017'}, 
            {task:'W-2s 2018'}, 
            {task:'Personal Tax Returns'},
            {task: 'Tax Transcripts'},
            {task: 'Home Insurance'},
            {task: 'Bank Deposit Slip'},
            {task: 'Alimony Income Verification'},
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
                            <div className="col-md-6">
                                {/* <LoanStatus /> */}
                                <DocumentStatus 
                                    heading="Document Request" 
                                    counts={8} 
                                    moreTask="/#" 
                                    getStarted="/#" 
                                    tasks={this.state.tasks}
                                />
                                
                            </div>
                            <div className="col-md-6">
                                <LoanProgress />                                
                                <ContactUs  
                                    userName="Jony Leo" 
                                    userId={290290} 
                                    userContact="(888) 971-1425" 
                                    userEmail="Jony.leo@texastrustloans.com"
                                    userWebsite="www.texatrustloans.com"
                                    userImg={contactAvatar}
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
