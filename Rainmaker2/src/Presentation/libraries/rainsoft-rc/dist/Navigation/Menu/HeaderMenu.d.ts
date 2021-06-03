/// <reference types="react" />
import { MenuOptionType } from '../../Types/MenuOptionsPropsType';
declare type HeaderMenuPropsType = {
    options: MenuOptionType[];
    name: String;
};
export declare const HeaderMenu: ({ options, name }: HeaderMenuPropsType) => JSX.Element;
export {};
