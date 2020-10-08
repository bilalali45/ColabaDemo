import React, {useState} from 'react'
import logo from '../../Assets/images/logo.svg';

export const RainMakerHeader = () => {
    const [show, setShow] = useState(false);

    const handleClose = () => setShow(false);
    const handleShow = () => setShow(true);
    return (
        <header className="rainmaker-header">
          <div className="container-fluid">
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
        </header>
    )
}
