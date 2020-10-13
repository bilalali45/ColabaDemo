export class LoanProgress {
    public id: string;
    public name: string;
    public description: string;
    public icon: string;
    public milestoneType: number;
    public isCurrent: boolean;
    public order: number;

    constructor(id: string, name: string, description: string, icon:string, milestoneType: number, isCurrent: boolean, order: number) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.icon = icon;
        this.milestoneType = milestoneType;
        this.isCurrent = isCurrent;
        this.order = order;
    }
}