export default class Organization {
    id: number;
    name: string;
    byteOrganizationCode: string;
    photo:string;
    constructor(id: number, name: string, byteOrganizationCode: string,photo:string) {
        this.id = id;
        this.name = name === null ? "":name;
        this.byteOrganizationCode = byteOrganizationCode === null ? "" : byteOrganizationCode;
        this.photo =photo;
    }
}
