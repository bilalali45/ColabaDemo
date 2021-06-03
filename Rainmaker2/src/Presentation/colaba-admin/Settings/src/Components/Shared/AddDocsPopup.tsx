import React from 'react';

const AddDocsPopup = () => {
    return (
        <section data-testid="addDocsPopup" className="settings__add-docs-popup" data-component="AddDocsPopup">
            <div className="settings__add-docs-popup-wrap">

                <aside className="settings__add-docs-popup--sidebar">
                    <ul>
                        <li className="active" title="All"><a href="">All</a></li>
                        <li><a href="" title="All">Assets</a></li>
                        <li><a href="" title="All">Credit & risk</a></li>
                        <li><a href="" title="All">Income</a></li>
                        <li><a href="" title="All">Letter of explanation</a></li>
                        <li><a href="" title="All">Personal</a></li>
                        <li><a href="" title="All">Property</a></li>
                        <li><a href="" title="All">Other</a></li>
                    </ul>
                </aside>

                <section className="settings__add-docs-popup--content">
                    <div className="settings__add-docs-popup--search">
                        <input className="settings__add-docs-popup--search-input" type="search" name="" id="search" placeholder="Enter follow up name…"/>
                        <button className="settings__add-docs-popup--search-submit"><i className="zmdi zmdi-search"></i></button>
                    </div>
                    <div className="settings__add-docs-popup--search-details">
                        
                        <h4 className="h4">Commonly used</h4>

                        <div className="settings__add-docs-popup--search-data">
                            <ul>
                                <li><a href="" title="All">Credit Report</a></li>
                                <li><a href="" title="All">Earnest Money Deposit</a></li>
                                <li><a href="" title="All">Financial Statement</a></li>
                                <li><a href="" title="All">Form 1099</a></li>
                                <li><a href="" title="All">Government-issued ID</a></li>
                                <li><a href="" title="All">Letter of Explanation - General</a></li>
                                <li><a href="" title="All">Mortgage Statement</a></li>
                                <li><a href="" title="All">Paystubs</a></li>
                            </ul>
                        </div>

                    </div>
                </section>

            </div>
        </section>
    )
}

export default AddDocsPopup;
