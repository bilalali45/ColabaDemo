export class LoanProgress {
    public id: string;
    public name: string;
    public description: string;
    public order: number;
    public isCurrentStep: boolean;
    public status: string;

    constructor(id: string, name: string, description: string, order: number, isCurrentStep: boolean, status: string) {
        this.id = id;
        this.name = name;
        this.description = description;
        this.order = order;
        this.isCurrentStep = isCurrentStep;
        this.status = status;
    }
}