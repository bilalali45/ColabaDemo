import React, {useState} from 'react';


export const Toggler = () => {
    const [toggle, setToggle] = useState(false);
  
  const callBack = () => {
    setToggle(!toggle)
  }
    return (
        <label className="switch" onClick={() => callBack()}>
            <input type="checkbox" id="toggle" defaultChecked={toggle} />
            <span className="slider round"></span>
        </label>
    )
}
