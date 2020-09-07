export class Template {
    
    public id: string;
    public  type: string;
    public name: string;
    public docs: any[];
    // public showEditing: boolean = false;

    constructor(id: string, type: string, name: string,docs:any[]) {
        this.id = id;
        this.type = type;
        this.name = name;
        this.docs = docs;
    }

}