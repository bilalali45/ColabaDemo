export class CurrentInView {
    public src: string;
    public name: string;
    public id: string;
    public isWorkBenchFile: boolean;
    public fileId?:string;
    

    constructor(id: string, src: string, name: string, isWorkBenchFile: boolean,fileId?: string) {
        this.id = id;
        this.src = src;
        this.name = name;
        this.fileId = fileId;
        this.isWorkBenchFile= isWorkBenchFile;

    }
}