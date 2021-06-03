import React from "react";

import { NavigationHandler } from "../../Utilities/Navigation/NavigationHandler";

interface PageHeadProps {
  className?: string;
  title?: string;
  handlerBack?: () => void;
  showBackBtn?: boolean;
}

const PageHead: React.FC<PageHeadProps> = ({
  className = "",
  title = "",
  showBackBtn = true,
}) => {
  return (
    <div data-testid="head" className={`comp-head ${className}`}>
      <div className="comp-head-wrap">
        <div data-testid="page-title" className="c-title">
          {title}
        </div>
        {showBackBtn && (
          <div className="link-back" onClick={() => NavigationHandler.moveBack()}>
            <span  className="icon-wrap">
              <i className="zmdi zmdi-arrow-left"></i>
            </span>
            <span data-testid="back-btn-txt" className="txt-wrap" >
              Back
            </span>
          </div>
        )}
      </div>
    </div>
  );
};

export default PageHead;
