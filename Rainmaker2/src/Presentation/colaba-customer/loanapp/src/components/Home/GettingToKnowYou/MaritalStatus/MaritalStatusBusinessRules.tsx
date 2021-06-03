import { LoanInfoType, PrimaryBorrowerInfo } from "../../../../Entities/Models/types";
import MaritalStatusActions from "../../../../store/actions/MaritalStatusActions";
import { LocalDB } from "../../../../lib/LocalDB";

export default class MaritalStatusBusinessRules {

    static getPrimaryBorrowerId() {
    }

    static async shouldAskIfCurrentBorrowerIsSpouse(loanManager, currentUserMaritialStatus: number) {

        const primaryBorrowerInfo: PrimaryBorrowerInfo = loanManager.primaryBorrowerInfo;
        const loanApplicationId = Number(LocalDB.getLoanAppliationId());
        const loanInfo: LoanInfoType = loanManager.loanInfo;

        if (loanInfo.borrowerId == undefined || currentUserMaritialStatus == undefined)
            return false;

        const isPrimaryBorroweMarried = await MaritalStatusActions.getMaritalStatus(loanApplicationId, primaryBorrowerInfo.id ? primaryBorrowerInfo.id : loanInfo.borrowerId)
        /*console.count("shouldAskIfCurrentBorrowerIsSpouse");
        console.info(primaryBorrowerInfo.id);
        console.info("isPrimaryBorroweMarried", isPrimaryBorroweMarried);
        console.info("loanInfo.ownTypeId ", loanInfo.ownTypeId)
        console.info("currentUserMaritialStatus ", currentUserMaritialStatus)
        /* const isPrimaryBorrowerRelationAlreadyMapped = await MaritalStatusActions.isRelationAlreadyMapped(loanApplicationId, primaryBorrowerInfo.id)
        console.log("isPrimaryBorrowerRelationAlreadyMapped ",isPrimaryBorrowerRelationAlreadyMapped); */
        /* 
        console.log(!isPrimaryBorrowerRelationAlreadyMapped && 
            loanInfo.ownTypeId === 2 && 
            (currentUserMaritialStatus && currentUserMaritialStatus === 1)); */
        if (!isPrimaryBorroweMarried)
            return false;

        if (isPrimaryBorroweMarried.maritalStatus === 1)
            if (loanInfo.ownTypeId === 2)
                if (currentUserMaritialStatus && currentUserMaritialStatus === 1) {
                    const isSecondaryBorrowerRelationAlreadyMappedWithSomeOneOnThisApplication = await MaritalStatusActions.isRelationAlreadyMapped(loanApplicationId, loanInfo.borrowerId)
                    console.info("isSecondaryBorrowerRelationAlreadyMappedWithSomeOneOnThisApplication >>> ", isSecondaryBorrowerRelationAlreadyMappedWithSomeOneOnThisApplication)
                    if (!isSecondaryBorrowerRelationAlreadyMappedWithSomeOneOnThisApplication)
                        return true;
                    
                    return false;
                } else return false;
            else {
                return false;
            }
        else
            return false;
    }
};
