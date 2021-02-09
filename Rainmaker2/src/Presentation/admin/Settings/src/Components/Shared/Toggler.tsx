import React, { FunctionComponent, useState } from 'react';

interface TogglerProps{
  checked?:boolean,
  trueValue?:string,
  falseValue?:string,
  handlerClick:()=>void,
  isDisable?: boolean
}

export const Toggler:React.FC<TogglerProps> = ({checked,trueValue,falseValue,handlerClick, isDisable}) => {
    const [toggle, setToggle] = useState(checked);
    return (
      <>
        {trueValue && 
          <label data-testid="togglerTextBy" className={`settings__switch settings__switch-text ${isDisable?'disabled':''} ${ checked == true ? 'active' : ''}`} onClick={handlerClick}>
              <input type="checkbox" id="toggle" defaultChecked={toggle} disabled={isDisable} />
              <span className="settings__switch-text-true">{trueValue}</span>
              <span className="settings__switch-text-false">{falseValue}</span>
          </label>
        }
        {!trueValue &&
          <label data-testid="togglerDefault" className={`settings__switch ${isDisable?'disabled':''} ${ checked == true ? 'active' : ''}`} onClick={handlerClick}>
              <input type="checkbox" id="toggle" defaultChecked={toggle} disabled={isDisable}/>
              <span className="slider round"></span>
          </label>
        }
      </>
    )
}
