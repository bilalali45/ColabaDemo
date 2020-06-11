import React from 'react'
import { HeaderMenu } from '../../../Navigation/Menu/HeaderMenu';
import { MenuOptionType } from '../../../Types/MenuOptionsPropsType';

type HeaderPropsType = {
    logoSrc: string;
    displayName: string;
    displayNameOnClick: Function;
    options: MenuOptionType[];

}

export const RainsoftRcHeader = ({ logoSrc, displayName, displayNameOnClick, options }: HeaderPropsType) => {
    return (
        <header className="header-main">
            <div className="container-fluid">
                <div className="row">
                    <div className="col-12">
                        <nav className="navbar navbar-default">
                            <div className="navbar-header h-logo">
                                <a className="logo-link" href="/">

                                    <img alt="Texas Trust Home Loans - Mortgage Lender in Texas " src={logoSrc} className="d-none d-sm-block" />
                                </a>
                            </div>

                            <div className="s-account pull-right">

                                <a className="d-name d-none d-sm-block" onClick={(e) => displayNameOnClick(e)} >
                                    Hello,
                                    {displayName}
                                </a>
                                <HeaderMenu
                                    options={options}
                                    displayName={displayName} />
                            </div>
                        </nav>
                    </div>
                </div>
            </div>

        </header>
    )
}


