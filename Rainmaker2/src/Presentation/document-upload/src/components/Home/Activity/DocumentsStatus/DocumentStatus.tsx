import React from 'react'
import {SVG} from './../../../../shared/Components/Assets/SVG';

type Props = {
    heading?: string,
    counts: number,
    moreTask?: any,
    getStarted?: any,
    tasks?: any
}

export const DocumentStatus: React.SFC<Props> = (props) => {
    return (
        <div className="DocumentStatus box-wrap">
            <div className="row">
                <div className="col-md-7 DocumentStatus--left-side">
                    <div className="box-wrap--header">
                        <h2 className="heading-h2"> {props.heading} </h2>
    <p>You have <span className="DocumentStatus--count">{ props.counts }</span> items to complete</p>
                    </div>
                    <div className="box-wrap--body">
                        <ul className="list">
                                { props.tasks.map( (item:any) => {
                                        return <li> {item.task} </li>
                                }) }
                        </ul>
                        <div>
                            <a href={ props.moreTask } className="DocumentStatus--get-link">Show 4 more Tasks <SVG  shape="arrowFarword"/></a>
                        </div>
                    </div>
                </div>
                <div className="col-md-5 DocumentStatus--right-side">
                    <SVG shape="storage"/>
                    <a href={ props.getStarted } className="btn btn-primary float-right">Get Started</a>
                </div>
            </div>
        </div>
    )
}
