import React from 'react'
import Dropdown from 'react-bootstrap/Dropdown';
import { MenuOptionType } from '../../Types/MenuOptionsPropsType';

type HeaderMenuPropsType = {
    options: MenuOptionType[];
    name: String;
}

export const HeaderMenu = ({ options, name }: HeaderMenuPropsType) => {
    // const getShortName = (name: string) => {
    //     let splitData = name.split(" ");
    //     let shortName = splitData[0].charAt(0).toUpperCase() + splitData[1].charAt(0).toUpperCase();
    //   return shortName;
    // }
    return (
        <Dropdown className="userdropdown">
            <Dropdown.Toggle id="dname" className="d-name d-none d-sm-block" as="a">
                {name}
            </Dropdown.Toggle>
            <Dropdown.Toggle id="dropdownMenuButton" className="hd-shorname" as="a">
            </Dropdown.Toggle>

            <Dropdown.Menu>
                {options.map((item) => {
                    return <Dropdown.Item key={item.name} onClick={(e: any) => item.callback(e)}>{item.name}</Dropdown.Item>
                })
                }
            </Dropdown.Menu>
        </Dropdown>
    )
}
