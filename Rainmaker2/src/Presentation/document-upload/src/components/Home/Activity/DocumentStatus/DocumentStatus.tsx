import React from 'react'
import { SVG } from '../../../../shared/Components/Assets/SVG';
import { Link, useHistory } from 'react-router-dom';





type Props = {
    heading?: string,
    counts: number,
    moreTask?: any,
    getStarted?: any,
    tasks?: any
}

export const DocumentStatus: React.SFC<Props> = (props) => {

    const history = useHistory();

    const getStarted = () => {
        history.push('/home/DocumentStatus');
    }

    return (
        <div className="DocumentStatus box-wrap">
            <div className="row">
                <div className="col-md-7 DocumentStatus--left-side">
                    <div className="box-wrap--header">
                        <h2 className="heading-h2"> {props.heading} </h2>
                        <p>You have <span className="DocumentStatus--count">{props.counts}</span> items to complete</p>
                    </div>
                    <div className="box-wrap--body">
                        <ul className="list">
                            {props.tasks.map((item: any) => {
                                return <li> {item.task} </li>
                            })}
                        </ul>
                    </div>
                </div>
                <div className="col-md-5 DocumentStatus--right-side">
                    <SVG shape="storage" />
                    <button onClick={getStarted} className="btn btn-primary float-right">Get Started <em className="zmdi zmdi-arrow-right"></em></button>
                </div>
            </div>
        </div>
    )
}
