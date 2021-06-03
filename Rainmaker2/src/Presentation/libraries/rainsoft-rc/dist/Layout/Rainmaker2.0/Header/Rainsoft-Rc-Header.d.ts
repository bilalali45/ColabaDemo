/// <reference types="react" />
import { MenuOptionType } from '../../../Types/MenuOptionsPropsType';
declare type HeaderPropsType = {
    logoSrc: string;
    displayName: string;
    options: MenuOptionType[];
};
export declare const RainsoftRcHeader: ({ logoSrc, displayName, options }: HeaderPropsType) => JSX.Element;
export {};
