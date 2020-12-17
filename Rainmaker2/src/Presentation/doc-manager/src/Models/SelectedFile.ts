export class SelectedFile {
    public name: string;
    public id: string;
    public fileId?:string;
    

    constructor(id: string, name: string, fileId?: string) {
        this.id = id;
        this.name = name;
        this.fileId = fileId;
    }
}