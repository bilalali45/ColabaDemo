import React from 'react'
import { HeaderMenu } from '../../../Navigation/Menu/HeaderMenu';
import { MenuOptionType } from '../../../Types/MenuOptionsPropsType';

type HeaderPropsType = {
    logoSrc: string;
    displayName: string;
   // displayNameOnClick: Function;
    options: MenuOptionType[];

}

export const RainsoftRcHeader = ({ logoSrc, displayName,options }: HeaderPropsType) => {
    return (
        <header className="header-main">
            <div className="container-fluid">
                <div className="row">
                    <div className="col-12">
                        <nav className="navbar navbar-default">
                            <div className="navbar-header h-logo">
                                <a className="logo-link" href="/">

                                    <img alt="" src={logoSrc} className="d-none d-sm-block" />
                                </a>
                            </div>

                            <div className="s-account pull-right" data-private >

                                {/* <a className="d-name d-none d-sm-block" onClick={(e) => displayNameOnClick(e)} >
                                    Hello,
                                    {displayName}
                                </a> */}
                                <HeaderMenu
                                    options={options}
                                    name={displayName}
                                />
                            </div>
                        </nav>
                    </div>
                </div>
            </div>

        </header>
    )
}


