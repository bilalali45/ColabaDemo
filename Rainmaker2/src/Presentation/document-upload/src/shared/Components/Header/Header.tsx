import React from 'react'
import ImageAssets from '../../../utils/image_assets/ImageAssets';

const Header = () => {
    return (
        <header className="header-main">
            
 
            <div className="container-fluid">
                <div className="row">
                    <div className="col-12">
                        <nav className="navbar navbar-default">
                            <div className="navbar-header">
                                <a className="navbar-brand" href="/">

<img alt="Texas Trust Home Loans - Mortgage Lender in Texas " src={ImageAssets.header.logoheader} className="d-none d-sm-block" />
                        </a> 
                    </div>
                                    <div className="s-account pull-right">

                                        <a className="d-name d-none d-sm-block" href="/Dashboard">
                                            Hello,
                                            Muhammad Usman
                        </a>
                                        <div className="dropdown">
                                            <a className="dropdown-toggle" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">

                                                <span>
                                                    MU
                            </span>
                                            </a>
                                            <div className="dropdown-menu" aria-labelledby="dropdownMenuButton">
                                                <a className="dropdown-item" href="/Dashboard">Dashboard</a>
                                                <a className="dropdown-item" href="/Account/ManagePassword">Change Password</a>
                                                <a className="dropdown-item" href="/Account/LogOff">Sign Out</a>
                                            </div>
                                        </div>
                                    </div>
            </nav>
                            </div>
    </div>
                    </div>

        </header>
    )
}

export default Header;
