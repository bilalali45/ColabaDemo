import React from 'react'
import Dropdown from 'react-bootstrap/Dropdown';
import { MenuOptionType } from '../../Types/MenuOptionsPropsType';

type HeaderMenuPropsType = {
    displayName: string;
    options: MenuOptionType[]; 
} 

export const HeaderMenu = ({displayName, options}: HeaderMenuPropsType) => {
const getShortName = (name: string) => {
    let splitData = name.split(" ");
    let shortName = splitData[0].charAt(0).toUpperCase() + splitData[1].charAt(0).toUpperCase();
  return shortName;
}
    return (   
            <Dropdown className="userdropdown">
                <Dropdown.Toggle id="dropdownMenuButton" className="hd-shorname" as="a">
                    <span>{getShortName(displayName)}</span>
                </Dropdown.Toggle>

                <Dropdown.Menu>
                    {options.map((item) =>{
                     return <Dropdown.Item key={item.name} onClick={(e: any) =>item.callback(e)}>{item.name}</Dropdown.Item>
                        })
                    }                 
                </Dropdown.Menu>
            </Dropdown>     
    )
}
