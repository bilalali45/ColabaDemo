/// <reference types="react" />
import { MenuOptionType } from '../../Types/MenuOptionsPropsType';
declare type HeaderMenuPropsType = {
    displayName: string;
    options: MenuOptionType[];
};
export declare const HeaderMenu: ({ displayName, options }: HeaderMenuPropsType) => JSX.Element;
export {};
