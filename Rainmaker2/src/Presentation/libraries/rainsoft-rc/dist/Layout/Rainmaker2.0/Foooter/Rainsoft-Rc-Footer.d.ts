/// <reference types="react" />
declare type FooterPropsType = {
    title: string;
    streetName: string;
    address: string;
    phoneOne: string;
    phoneTwo: string;
    contentOne?: string;
    contentTwo?: string;
    nmlLogoSrc?: string;
    nmlUrl?: string;
};
export declare const RainsoftRcFooter: ({ title, streetName, address, phoneOne, phoneTwo, contentOne, contentTwo, nmlLogoSrc, nmlUrl }: FooterPropsType) => JSX.Element;
export {};
