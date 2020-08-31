export class Template {
    
    public id: string;
    public  type: string;
    public name: string;
    // public showEditing: boolean = false;

    constructor(id: string, type: string, name: string) {
        this.id = id;
        this.type = type;
        this.name = name;
    }

}