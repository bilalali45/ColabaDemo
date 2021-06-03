import React from "react";

interface IncomePopUpProps {
  className?: string;
  title?: string;
  handlerCancel?: any;
  id?: number;
  setShowPopup?:any
}

const IncomePopUp: React.FC<IncomePopUpProps> = ({
  className = "",
  title = "",
  setShowPopup,
  children
}) => {
  return (
    <>
    <div className={`income-popup  ${className}`}>
    <div className="income-popup-head">
        <span className="icon-back">
              <i className="zmdi zmdi-arrow-left"></i>
            </span>
        <h4>{title}</h4>

        <div className="icon-close" onClick={()=>{setShowPopup(false)}}>
            <i className="zmdi zmdi-close"></i>
        </div>

    </div>
    <div className="income-popup-body">
       {children}
    </div>
</div>
<div className="income-popup-wrap"></div>
</>
  );
};

export default IncomePopUp;
