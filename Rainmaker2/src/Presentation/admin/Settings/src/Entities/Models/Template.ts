export class Template {
    
    public id: string;
    public  type: string;
    public name: string;
    public docs: null | any[];
    public confirmDelete: boolean;
    public edit: boolean;
    public open: boolean;
    public isNew: boolean;
    public deleteRequestSent: boolean;
    public renameRequestSent: boolean;
    public NewlyCreated: boolean;

    constructor(id: string = '', type: string = '', name: string = '', docs = null) {
        this.id = id;
        this.type = type;
        this.name = name;
        this.docs = docs;
        this.confirmDelete = false;
        this.edit = false;
        this.open = false
        this.isNew = false;
        this.deleteRequestSent = false;
        this.renameRequestSent = false;
        this.NewlyCreated = false;
    }

}