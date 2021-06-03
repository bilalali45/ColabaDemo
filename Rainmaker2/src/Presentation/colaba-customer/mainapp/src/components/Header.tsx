import React from "react";
import { useAuth } from "./AuthContext";
import Dropdown from 'react-bootstrap/Dropdown'
import { LOGOTexasTrust} from '../Shared/Components/SVGs'
export default function Header() {
  const auth: any = useAuth();


  const LogedInHeader = () => {
    return (
      <header className="main-header">
      <section>
          <div className="container-fluid">
              <div className="row align-items-center justify-content-between h-inner-wrap">
                  <div className="col-6">
                      <a href="" className="logo-wrap">
                          <LOGOTexasTrust />
                      </a>
                  </div>
                  <div className="col-6 text-right">
                      <div className="user-dropdown-wrap">
                          <Dropdown>
                              <Dropdown.Toggle as="div">
                                  Hello, Adeel <i className="zmdi zmdi-caret-down-circle"></i>
                              </Dropdown.Toggle>
                              <Dropdown.Menu>
                                  <Dropdown.Item href="#/action-1">Action</Dropdown.Item>
                                  <Dropdown.Item href="#/action-2">Another action</Dropdown.Item>
                                  <Dropdown.Item href="#/action-3">Something else</Dropdown.Item>
                              </Dropdown.Menu>
                          </Dropdown>
                      </div>
                  </div>
              </div>
          </div>
      </section>

  </header>
    )
  }

  return auth.user ? (
    LogedInHeader()
  ) : LogedInHeader();
}
