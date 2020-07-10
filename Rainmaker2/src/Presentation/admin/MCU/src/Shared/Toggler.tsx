import React, {useState} from 'react';


export const Toggler = () => {

    const [toggle, setToggle] = useState(true);

    return (
        <label className="switch" onClick={()=>{ setToggle(true) }}>
            <input type="checkbox" id="toggle" defaultChecked={toggle} />
            <span className="slider round"></span>
        </label>
    )
}
