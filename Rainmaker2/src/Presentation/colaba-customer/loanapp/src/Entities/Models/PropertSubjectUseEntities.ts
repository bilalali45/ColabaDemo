

export class PropertyUsageProto {
    propertyUsageId: number|null
    borrowers: PropertyUsageBorrowerProto[]
    constructor(
        propertyUsageId: number,
        borrowers: PropertyUsageBorrowerProto[]) {
        this.borrowers = borrowers
        this.propertyUsageId = propertyUsageId
    }
    public toString = () : string => {
        return JSON.stringify(this);
    }
}


export class  AddOrUpdatePropertyUsagePayload  extends PropertyUsageProto{
    loanApplicationId: number
    constructor(loanApplicationId: number, propertyUsageId: number|null, borrowers: PropertyUsageBorrowerProto[]) {
        super(propertyUsageId, borrowers );
        this.loanApplicationId = loanApplicationId
    }
    public toString = () : string => {
        return JSON.stringify(this);
    }
}

export class PropertyUsageBorrowerProto {
    id: number
    firstName?: string
    willLiveIn?: boolean
    constructor(id :number, firstName :string, willLiveIn :boolean ) {
        this.id = id;
        this.firstName = firstName;
        this.willLiveIn = willLiveIn;
    }
    public toString = () : string => {
        return JSON.stringify(this);
    }
}