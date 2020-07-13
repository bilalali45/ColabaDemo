import React from 'react'
export const TemplateListContainer = () => {
    const MyTemplates = () => {
        return (
          <>
<div className="m-template">
                <div className="MT-groupList">
                    <div className="title-wrap">
                        <h4>My Templates</h4>
                    </div>

                    <div className="list-wrap my-temp-list">
                        <ul>
                            <li>
                                Income Template
                        <span className="BTNclose"><i className="zmdi zmdi-close"></i></span>
                            </li>
                            <li  className="active">My standard checklist <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></li>
                            <li>Assets Template <span className="BTNclose"><i className="zmdi zmdi-close"></i></span></li>
                        </ul>
                    </div>
                </div>
                </div>
          </>
        );
      };


      const TemplatesByTenant = () => {
        return (
          <>
         <div className="template-by-tenant">
                    <div className="MT-groupList">
                        <div className="title-wrap">
                            <h4>Templates by Tenant</h4>
                        </div>

                        <div className="list-wrap my-temp-list">
                            <ul>
                                <li>FHA Full Doc Refinance - W2</li>
                                <li>VA Cash Out - W-2</li>
                                <li>FHA Full Doc Refinance</li>
                                <li>Conventional Refinance - SE</li>
                                <li>VA Purchase - W-2</li>
                                <li>Additional Questions</li>
                            </ul>
                        </div>
                    </div>
                </div>
          </>
        );
      };

    return (
        <div className="TL-container">

            <div className="head-TLC">

                <h4>Templates</h4>

                <div className="btn-add-new-Temp">
                    <button className="btn btn-primary addnewTemplate-btn">
                        <span className="btn-text">Add new template</span>
                        <span className="btn-icon">
                            <i className="zmdi zmdi-plus"></i>
                        </span>

                    </button>
                </div>
            </div>





            <div className="listWrap-templates">
{/* My Templates */}
{MyTemplates()}

       {TemplatesByTenant()}
           
           
            </div>
       
       
        </div>
    )
}
