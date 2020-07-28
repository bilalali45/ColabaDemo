import React, { useState, useEffect, useRef, useCallback } from "react";
import { Http } from "rainsoft-js";
import _ from 'lodash'

import { ActivityLogType, LogType } from "../../../../Entities/Types/Types";
import { DateTimeFormat } from "../../../../Utils/helpers/DateFormat";
import { NeedListEndpoints } from "../../../../Store/endpoints/NeedListEndpoints";

export const ReviewDocumentActivityLog = ({ id, typeId }: { id: string | null, typeId: string | null }) => {
    const [tab, setTab] = useState(1);
    const sectionRef = useRef<HTMLElement>(null);
    const allSections: any = sectionRef?.current?.children[0]?.children.length;
    const getWidthSection: any = sectionRef?.current?.offsetWidth;
    const totalWidth: any = allSections * getWidthSection;

    const [activityLogs, setActivityLogs] = useState<ActivityLogType[]>([])
    const [logIndex, setLogIndex] = useState(0)
    const [sortByEventName, setSortByEventName] = useState<boolean | null>(null)
    const [sortByEventDateTime, setSortByEventDateTime] = useState<boolean | null>(true)

    const checkActiveTab = (step: any) => {
        if (step == tab) {
            return 'active'
        }
    }

    const getTab = (step: any) => {
        setTab(step);
    }

    const switchTab = () => {
        return (tab - 1) * -getWidthSection;
    }

    const tabDataStyle: any = {
        transform: 'translateX(' + switchTab() + 'px)',
        width: totalWidth
    }

    const getActivityLogs = useCallback(async (id, typeId) => {
        try {
            const http = new Http()

            const { data } = await http.get<ActivityLogType[]>(NeedListEndpoints.GET.documents.activityLogs(id, typeId))

            setActivityLogs(data)
        } catch (error) {
            console.log(error)

            alert('Something went wrong while fetching logs. Please try again.')
        }
    }, [])

    const renderLogs = useCallback((logs: LogType[]) => {
        return logs.map(log => {
            return (
                <tr key={log._id}>
                    <td>{log.activity}</td>
                    <td>{DateTimeFormat(log.dateTime, true)}</td>
                </tr>
            )
        })
    }, [])

    const renderActivityLog = useCallback((activityLogs: ActivityLogType[]) => {
        return activityLogs.map((activityLog: ActivityLogType, index: number) => {
            return (
                <li className={`${index === logIndex && 'active'}`} key={activityLog.dateTime}>
                    <a href="#" onClick={() => {
                        setSortByEventDateTime(null)
                        setSortByEventName(null)
                        setLogIndex(index)
                    }}>
                        <div className="d-flex justify-content-between">
                            <h6>Requested By</h6>
                            <time className="vertical-tabs--list-time">{DateTimeFormat(activityLog.dateTime, true)}</time>
                        </div>
                        
                        <h2>{activityLog.userName}</h2>
                        
                    </a>
                </li>
            )
        })
    }, [logIndex])


    const sortEventNames = () => {
        sortByEventDateTime !== null && setSortByEventDateTime(null)

        const clonedActivityLogs = _.cloneDeep(activityLogs)

        const currentLog = clonedActivityLogs[logIndex]

        const sortedActivityLogs = _.orderBy(currentLog.log, ['activity'], [sortByEventName === false || sortByEventName === null ? 'asc' : 'desc'])

        clonedActivityLogs[logIndex].log = sortedActivityLogs

        setActivityLogs(() => clonedActivityLogs)
        setSortByEventName(() => sortByEventName === false || sortByEventName === null ? true : false)
    }

    const sortEventDates = () => {
        sortByEventName !== null && setSortByEventName(() => null)

        const clonedActivityLogs = _.cloneDeep(activityLogs)

        const currentLog = clonedActivityLogs[logIndex]

        const sortedActivityLogs = _.orderBy(currentLog.log, ['dateTime'], [
            sortByEventDateTime === false || sortByEventDateTime === null ? 'asc' : 'desc'
        ])

        clonedActivityLogs[logIndex].log = sortedActivityLogs

        setActivityLogs(() => clonedActivityLogs)
        setSortByEventDateTime(() => sortByEventDateTime === false || sortByEventDateTime === null ? true : false)
    }

    const getSortIconClassName = (sortState: boolean | null) => {
        if (sortState === null) {
            return ''
        } else if (sortState === false) {
            return 'zmdi zmdi-long-arrow-down table-th-arrow'
        } else if (sortState === true) {
            return 'zmdi zmdi-long-arrow-up table-th-arrow'
        }
    }

    useEffect(() => {
        if (id === null || typeId === null) return

        getActivityLogs(id, typeId)
    }, [getActivityLogs, id, typeId])


    return (
        <section ref={sectionRef} className="vertical-tabs" id="verticalTab">
            <div className="vertical-tabs--data" style={tabDataStyle}>

                {/* Activity Log */}
                <div className={"vertical-tabs--wrap " + checkActiveTab(1)} data-step="1" style={{ width: `${getWidthSection}px` }}>
                    <div className="vertical-tabs--aside">
                        <header className="vertical-tabs--header">
                            <h2 className="vertical-tabs--header-title">Activity Log</h2>
                        </header>
                        <section className="vertical-tabs--body">
                            <ul className="vertical-tabs--list">
                                {activityLogs.length > 0 && renderActivityLog(activityLogs)}
                            </ul>
                        </section>
                    </div>

                    <div className="vertical-tabs--content">
                        <header className="vertical-tabs--header flex">
                            <div className="vertical-tabs--header-left">
                                <h2 className="vertical-tabs--header-title">Log Details</h2>
                            </div>
                            <div className="vertical-tabs--header-right">
                                <button className="btn-go" onClick={() => { getTab(2) }}>View Email Log <em className="zmdi zmdi-arrow-right"></em></button>
                            </div>
                        </header>
                        <section className="vertical-tabs--body padding">
                            <table className="table table-noborder">
                                <thead>
                                    <tr>
                                        <th>
                                            <button onClick={sortEventNames}>Events <em className={getSortIconClassName(sortByEventName)}></em>
                                            </button>
                                        </th>
                                        <th><button onClick={sortEventDates}>Date & Time <em className={getSortIconClassName(sortByEventDateTime)}></em></button></th>
                                    </tr>
                                </thead>
                            </table>
                            <div className="vertical-tabs--scrollable">
                                <table className="table">
                                    <tbody>
                                        {activityLogs.length > 0 && renderLogs(activityLogs[logIndex].log)}
                                    </tbody>
                                </table>
                            </div>

                        </section>
                        <footer className="vertical-tabs--footer">
                            <h2><span>Message to borrower</span></h2>
                            {activityLogs.length > 0 && activityLogs[logIndex].message}
                        </footer>
                    </div>
                </div>

                {/* Email Log */}
                <div className={"vertical-tabs--wrap " + checkActiveTab(2)} data-step="2" style={{ width: `${sectionRef?.current?.offsetWidth}px` }}>
                    <div className="vertical-tabs--aside">
                        <header className="vertical-tabs--header">
                            <h2 className="vertical-tabs--header-title"><button className="btn-go" onClick={() => { getTab(1) }}><em className="zmdi zmdi-arrow-left"></em> Back</button></h2>
                        </header>
                        <section className="vertical-tabs--body">
                            <ul className="vertical-tabs--list">
                                {/* <li className="active">
                                    <a href="">
                                        <div className="row">
                                            <div className="col-md-5"><h2>Zohaib</h2></div>
                                            <div className="col-md-6 offset-md-1"><time className="vertical-tabs--list-time">Jan, 19 at 11:00 PM</time></div>
                                        </div>
                                    </a>
                                </li>
                                <li>
                                    <a href="">
                                        <div className="row">
                                            <div className="col-md-5"><h2>Kashan</h2></div>
                                            <div className="col-md-6 offset-md-1"><time className="vertical-tabs--list-time">Jan, 19 at 11:00 PM</time></div>
                                        </div>
                                    </a>
                                </li>
                                <li>
                                    <a href="">
                                        <div className="row">
                                            <div className="col-md-5"><h2>Atif</h2></div>
                                            <div className="col-md-6 offset-md-1"><time className="vertical-tabs--list-time">Jan, 19 at 11:00 PM</time></div>
                                        </div>
                                    </a>
                                </li> */}
                            </ul>
                        </section>
                    </div>

                    <div className="vertical-tabs--content">
                        <header className="vertical-tabs--header flex">
                            <div className="vertical-tabs--header-left">
                                <h2 className="vertical-tabs--header-title">Email Log</h2>
                            </div>
                            <div className="vertical-tabs--header-right">
                                &nbsp;
                        </div>
                        </header>
                        <section className="vertical-tabs--body padding">
                            {/* <h6>Hi Zohaib Siddiqui!</h6>
                            <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Non nam dignissimos et, earum debitis ducimus tenetur dicta quo. Ad, nobis! Dolor voluptatum quas neque laborum tempora eveniet delectus magnam omnis?</p>

                            <ul>
                                <li>Lorem ipsum dolor, sit amet consectetur adipisicing elit.</li>
                                <li>Laboriosam illo magni, inventore perspiciatis distinctio.</li>
                                <li>Explicabo fugiat ratione culpa dolore tempora dolor cum.</li>
                                <li>Eum sed quam aliquid nihil sequi reprehenderit reiciendis?</li>
                            </ul>

                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Praesentium soluta possimus, in ex labore rerum necessitatibus a maxime ea recusandae quo? Qui doloribus explicabo eum, optio quod sequi iure iste?</p> */}

                        </section>
                    </div>
                </div>

            </div>
        </section>
    )
}