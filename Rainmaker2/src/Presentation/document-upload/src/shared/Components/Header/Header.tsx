import React from 'react'
import ImageAssets from '../../../utils/image_assets/ImageAssets';
import Dropdown from 'react-bootstrap/Dropdown';


const Header = () => {
    return (
        <header className="header-main">


            <div className="container-fluid">
                <div className="row">
                    <div className="col-12">
                        <nav className="navbar navbar-default">
                            <div className="navbar-header h-logo">
                                <a className="logo-link" href="/">

                                    <img alt="Texas Trust Home Loans - Mortgage Lender in Texas " src={ImageAssets.header.logoheader} className="d-none d-sm-block" />
                                </a>
                            </div>

                            <div className="s-account pull-right">

                                <a className="d-name d-none d-sm-block" href="/Dashboard">
                                    Hello,
                                    Muhammad Usman
                    </a>
                                <Dropdown className="userdropdown">
                                    <Dropdown.Toggle id="dropdownMenuButton" className="hd-shorname" as="a">
                                        <span>MU</span>
                                    </Dropdown.Toggle>

                                    <Dropdown.Menu>
                                        <Dropdown.Item href="/Dashboard">Dashboard</Dropdown.Item>
                                        <Dropdown.Item href="/Account/ManagePassword">Change Password</Dropdown.Item>
                                        <Dropdown.Item href="/Account/LogOff">Sign Out</Dropdown.Item>
                                    </Dropdown.Menu>
                                </Dropdown>
                            </div>
                        </nav>
                    </div>
                </div>
            </div>

        </header>
    )
}

export default Header;