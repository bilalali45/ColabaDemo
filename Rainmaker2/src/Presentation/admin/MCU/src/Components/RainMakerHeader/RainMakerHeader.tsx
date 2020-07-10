import React, {useState} from 'react'
import logo from '../../Assets/images/logo.svg';

export const RainMakerHeader = () => {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    return (
        <header className="rainmaker-header">
          <div className="container-fluid">
              <div className="row">

              <div className="col-md-6">
                <div className="logo-wrap">
                    <div className="burger-menu">
                        <i className="mcu-icon-burger"></i>
                    </div>
                    <div className="logo">
                        <a href="javascript:">
                            <img src={logo} alt="" />
                        </a>
                    </div>
                </div>
              </div>
              <div className="col-md-6">
                <div className="header-options">
                    <ul>
                        <li>
                            <a href="">
                                <span className="header-options--icon">
                                    <em className="zmdi zmdi-notifications"></em>
                                    <span className="header-options--counts">20</span>
                                </span>
                            </a>
                        </li>
                    </ul>
                </div>
              </div>         
              </div>
            </div>  
        </header>
    )
}
