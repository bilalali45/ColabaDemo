import React from "react";
import { IconTick } from "./SVGs";

interface IconRadioSnipitProps {
  className?: string;
  title?: string;
  handlerClick?: (id: number | undefined, title?: string) => void;
  icon?: string | any;
  id?: number;
  dataTestId?: string;
}

const IconRadioSnipit2: React.FC<IconRadioSnipitProps> = ({
  className = "",
  title = "",
  icon = "",
  id,
  dataTestId = "list-item",
  handlerClick,
}) => {
  return (
    <div
      data-testid={dataTestId}
      id={`${id}`}
      className={`iconRadioSnipit2  ${className}`}
      onClick={() => handlerClick && handlerClick(id, title)}
    >
      <div className="c-wrap">
        <div className="i-wrap">
          {icon}
        </div>
        <div className="c-title">{title}</div>
        <div className="c-h-icon">
          <span className="i-icon">
            <IconTick />
          </span>
        </div>
      </div>
    </div>
  );
};

export default IconRadioSnipit2;
