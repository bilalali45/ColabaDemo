import React, { useState, useEffect, useRef } from "react";

export const ReviewDocumentActivityLog = () => {

    const [tab, setTab] = useState(1);
    const sectionRef = useRef<HTMLElement>(null);
    const allSections:any = sectionRef?.current?.children[0]?.children.length;
    let getWidthSection:any = sectionRef?.current?.offsetWidth;
    let totalWidth:any  = allSections * getWidthSection;

    const checkActiveTab = (step: any) => {
        if (step == tab) {
            return 'active'
        }
    }

    const getTab = (step: any) => {
        setTab(step);
    }

    const switchTab = () => {
        return (tab-1) * -getWidthSection;
    }

    const tabDataStyle:any = {
        transform: 'translateX('+ switchTab() +'px)',
        width: totalWidth
    }

    return (
        <section ref={sectionRef} className="vertical-tabs" id="verticalTab">
            <div className="vertical-tabs--data" style={ tabDataStyle }>

                {/* Activity Log */}
                <div className={"vertical-tabs--wrap " + checkActiveTab(1)} data-step="1" style={{ width: `${getWidthSection}px` }}>
                    <div className="vertical-tabs--aside">
                        <header className="vertical-tabs--header">
                            <h2 className="vertical-tabs--header-title">Activity Log</h2>
                        </header>
                        <section className="vertical-tabs--body">
                            <ul className="vertical-tabs--list">
                                <li className="active">
                                    <a href="">
                                        <h6>Requested By</h6>
                                        <h2>Zohaib Siddiqui</h2>
                                        <time className="vertical-tabs--list-time">Jan, 19 at 11:00 PM</time>
                                    </a>
                                </li>
                                <li>
                                    <a href="">
                                        <h6>Requested By</h6>
                                        <h2>Zohaib Siddiqui</h2>
                                        <time className="vertical-tabs--list-time">Jan, 19 at 11:00 PM</time>
                                    </a>
                                </li>
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
                                        <th>Events</th>
                                        <th>Date & Time</th>
                                    </tr>
                                </thead>
                            </table>
                            <div className="vertical-tabs--scrollable">
                                <table className="table">
                                    <tbody>
                                        <tr>
                                            <td>Status changed : Borrower to do</td>
                                            <td>Jan, 19 at 11:00 PM</td>
                                        </tr>
                                        <tr>
                                            <td>Status changed : Started</td>
                                            <td>Jan, 19 at 11:00 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File submitted : Bank-statement-Jan-to-Mar-2020-1.jpg</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File submitted : Bank-statement-Jan-to-Mar-2020-2.jpg</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File submitted : Bank-statement-Jan-to-Mar-2020-3.jpg</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File submitted : Bank-statement-Jan-to-Mar-2020-4.jpg</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File submitted : Bank-statement-Jan-to-Mar-2020-5.jpg</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File submitted : Bank-statement-Jan-to-Mar-2020-6.jpg</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File submitted : Bank-statement-Jan-to-Mar-2020-7.jpg</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File submitted : Bank-statement-Jan-to-Mar-2020-8.jpg</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File submitted : Bank-statement-Jan-to-Mar-2020-9.jpg</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File submitted : Bank-statement-Jan-to-Mar-2020-10.jpg</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>File changed : Pending View</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                        <tr>
                                            <td>Renamed by : Sheikh Al-Zuhaib Siddiqui</td>
                                            <td>Jan, 20 at 03:15 PM</td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>

                        </section>
                        <footer className="vertical-tabs--footer">
                            <h2><span>Message</span></h2>
                            <h2>Hi Richard Glenn Randall,</h2>
                            <h2>Please Submit 2 months of the most recent Bank Statements</h2>
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
                                <li className="active">
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
                                </li>
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
                            <h6>Hi Zohaib Siddiqui!</h6>
                            <p>Lorem ipsum, dolor sit amet consectetur adipisicing elit. Non nam dignissimos et, earum debitis ducimus tenetur dicta quo. Ad, nobis! Dolor voluptatum quas neque laborum tempora eveniet delectus magnam omnis?</p>

                            <ul>
                                <li>Lorem ipsum dolor, sit amet consectetur adipisicing elit.</li>
                                <li>Laboriosam illo magni, inventore perspiciatis distinctio.</li>
                                <li>Explicabo fugiat ratione culpa dolore tempora dolor cum.</li>
                                <li>Eum sed quam aliquid nihil sequi reprehenderit reiciendis?</li>
                            </ul>

                            <p>Lorem ipsum dolor sit amet, consectetur adipisicing elit. Praesentium soluta possimus, in ex labore rerum necessitatibus a maxime ea recusandae quo? Qui doloribus explicabo eum, optio quod sequi iure iste?</p>

                        </section>
                    </div>
                </div>

            </div>
        </section>
    )
}