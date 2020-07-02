import React from 'react'
import logo from '../../Assets/images/logo.svg';
export const RainMakerHeader = () => {
    return (
        <header className="rainmaker-header">
          <div className="container-fluid">
              <div className="logo-wrap">
                  <div className="burger-menu"></div>
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
