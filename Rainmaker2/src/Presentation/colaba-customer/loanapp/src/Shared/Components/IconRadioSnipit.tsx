import React from "react";
import { IconTick } from "./SVGs";

interface IconRadioSnipitProps {
  className?: string;
  title?: string;
  handlerClick?: (id: number, title?: string) => void;
  icon?: string | React.ReactNode;
  id?: number;
}

const IconRadioSnipit: React.FC<IconRadioSnipitProps> = ({
  className = "",
  title = "",
  icon = "",
  id,
  handlerClick,
}) => {
  return (
    <div
      data-testid="list-item"
      id={`${id}`}
      className={`iconRadioSnipit  ${className}`}
      onClick={() => handlerClick(id, title)}
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

export default IconRadioSnipit;
