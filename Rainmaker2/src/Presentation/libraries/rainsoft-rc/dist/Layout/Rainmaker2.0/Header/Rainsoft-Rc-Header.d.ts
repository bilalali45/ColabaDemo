/// <reference types="react" />
import { MenuOptionType } from '../../../Types/MenuOptionsPropsType';
declare type HeaderPropsType = {
    logoSrc: string;
    displayName: string;
    displayNameOnClick: Function;
    options: MenuOptionType[];
};
export declare const RainsoftRcHeader: ({ logoSrc, displayName, displayNameOnClick, options }: HeaderPropsType) => JSX.Element;
export {};
