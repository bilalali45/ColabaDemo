import React, { useState } from "react";

import {UseFormMethods} from "react-hook-form";

interface IconRadioBoxProps extends Partial<Pick<UseFormMethods, "register" | "errors">> {
  className?: string;
  title?: string;
  handlerClick?: (id: any) => void;
  Icon?: any;
  id?: number;
  name?: string;
  value?: string;
  checked?: boolean;
  onChange?: any;
}

const IconRadioBoxWithState: React.FC<IconRadioBoxProps> = ({
  className = "",
  title = "",
  Icon,
  id,
  name,
  checked,
  value,
  handlerClick,
  register,
}) => {
    const [chk, setChk] = useState<any>(checked);

  return (
    <label
      data-testid="list-item"
      id={`${id}`}
      className={`IconRadioBox  ${className}`}
      onClick={() => {setChk(true);  handlerClick && handlerClick(value)}}
    >
      <div className="c-wrap">
        <div className="c-h-icon">
          {/* <span className="i-icon">
            <IconTick />
          </span> */}

          <input
            type="radio"
            id={"inputradio-" + id}
            name={name}
            checked={chk}
            value={value}
            ref={register}
          />
          <span className={`Radiobox-type`}></span>
        </div>
        <div className="i-wrap">
          <img src={Icon} />
          {/* {Icon} */}
        </div>
        <div className="c-title">{title}</div>
      </div>
    </label>
  );
};

export default IconRadioBoxWithState;
