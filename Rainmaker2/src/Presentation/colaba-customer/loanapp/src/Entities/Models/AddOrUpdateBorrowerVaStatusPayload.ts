
import { NavigationHandler } from "../../Utilities/Navigation/NavigationHandler"

export class AddOrUpdateBorrowerVaStatusPayload  {
    IsVaEligible: boolean
    MilitaryAffiliationIds: number[]
    ExpirationDateUtc: string
    ReserveEverActivated: boolean
    BorrowerId: number
    State: string 
    constructor(BorrowerId) {
        this.BorrowerId = BorrowerId;
        this.State = NavigationHandler.getNavigationStateAsString()
        this.MilitaryAffiliationIds = [];
    }
}
