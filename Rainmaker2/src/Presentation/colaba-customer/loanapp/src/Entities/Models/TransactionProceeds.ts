import { removeCommaFormatting } from "../../Utilities/helpers/CommaSeparteMasking";

export class TransactionProceedsDTO {
    AssetTypeId: number
    LoanApplicationId: number
    BorrowerId: number
    AssetCategoryId: number
    constructor(AssetTypeId :number, LoanApplicationId: number,
    BorrowerId: number,
    AssetCategoryId: number){
        this.AssetTypeId = AssetTypeId;
        this.LoanApplicationId = LoanApplicationId;
        this.BorrowerId = BorrowerId;
        this.AssetCategoryId = AssetCategoryId;
    }
    public toString = () : string => {
        return JSON.stringify(this);
    }
}

export class TransactionProceedsFromRealAndNonRealEstateDTO  extends TransactionProceedsDTO {
    AssetValue: number
    Description: string
    borrowerAssetId ?:number|null
    constructor(AssetTypeId :number, LoanApplicationId: number, BorrowerId: number, AssetCategoryId: number,
        AssetValue :number, Description: string, borrowerAssetId ?:number|null ) {
        super(AssetTypeId, LoanApplicationId, BorrowerId, AssetCategoryId);

        this.AssetValue = +removeCommaFormatting(AssetValue);
        this.Description = Description;
        this.borrowerAssetId = borrowerAssetId;
    }
    public toString = () : string => {
        return JSON.stringify(this);
    }
}

export class TransactionProceedsFromLoanDTO extends TransactionProceedsDTO{
    AssetValue: number
    ColletralAssetTypeId: number
    SecuredByColletral: boolean
    CollateralAssetDescription: string
    borrowerAssetId ?:number|null
    collateralAssetName: string
    constructor(AssetTypeId :number, LoanApplicationId: number, BorrowerId: number, AssetCategoryId: number,
                AssetValue :number, ColletralAssetTypeId :number,  SecuredByColletral: boolean,CollateralAssetDescription?: string, borrowerAssetId ?:number|null,collateralAssetName ?:string|null) {

        super(AssetTypeId, LoanApplicationId, BorrowerId, AssetCategoryId);
        this.AssetValue = +removeCommaFormatting(AssetValue);
        this.ColletralAssetTypeId = ColletralAssetTypeId;
        this.SecuredByColletral = SecuredByColletral;
        this.CollateralAssetDescription =CollateralAssetDescription;
        this.borrowerAssetId = borrowerAssetId;
        this.collateralAssetName = collateralAssetName;
    }
    public toString = () : string => {
        return JSON.stringify(this);
    }
}


