import React from 'react'
import { SVGstorage } from '../../../../shared/Components/Assets/SVG';
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
            <div className="box-wrap--header clearfix">
                <h2 className="heading-h2"> {props.heading} </h2>
                <p>You have <span className="DocumentStatus--count">{props.counts}</span> items to complete</p>
            </div>
            <div className="box-wrap--body clearfix">
                <ul className="list">
                    {props.tasks.map((item: any) => {
                        return <li> {item.task} </li>
                    })}
                </ul>
            </div>
            <div className="box-wrap--footer clearfix">
                <button onClick={getStarted} className="btn btn-primary float-right">Get Start <em className="zmdi zmdi-arrow-right"></em></button>
            </div>
        </div>
    )
}
