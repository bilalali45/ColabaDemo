export class LoanAmountDetails {
    public LoanApplicationId?: number;
    public PropertyValue?: number;
    public DownPayment?: number;
    public GiftPartOfDownPayment?: boolean;
    public GiftPartReceived?: boolean;
    public DateOfTransfer?: string;
    public GiftAmount?: number;
    public State?: string; 

    constructor(
        LoanApplicationId?: number, 
        PropertyValue?: number,
        DownPayment?: number,
        GiftPartOfDownPayment?: boolean,
        GiftPartReceived?: boolean,
        DateOfTransfer?: string,
        GiftAmount?: number,
        State?: string
        ){
            this.LoanApplicationId = LoanApplicationId;
            this.PropertyValue = PropertyValue;
            this.DownPayment = DownPayment;
            this.GiftPartOfDownPayment = GiftPartOfDownPayment;
            this.GiftPartReceived = GiftPartReceived;
            this.DateOfTransfer = DateOfTransfer;
            this.GiftAmount = GiftAmount;
            this.State = State
        }
}