import { GetReviewBorrowerInfoSectionProto, LoanInfoType } from '../../Entities/Models/types';
import { LocalDB } from '../../lib/LocalDB';
import BorrowerActions from '../../store/actions/BorrowerActions';
import { LoanApplicationActionsType } from '../../store/reducers/LoanApplicationReducer';
import { OwnTypeEnum } from '../../Utilities/Enum';

export class SetupLoan {

    static config : any = {};

    static async initLoanApplication(loanApplicationId, config) {
        this.config = config;
        let loanId = loanApplicationId;

        if (loanId === 'new') {
            this.startNewLoanApplication();
            return;
        }
        if (!isNaN(loanId)) {
            await this.editLoanApplication(loanId);
        }
    }

    static async editLoanApplication(loanId) {
        const primayBorrower = await this.getPrimaryBorrower();
        let loanInfo = await this.extractLoanInfo(loanId);
        if (!loanInfo.borrowerId) {
            if (primayBorrower.id) {
                loanInfo.borrowerId = primayBorrower.id;
                loanInfo.borrowerName = primayBorrower.name;
                loanInfo.ownTypeId = primayBorrower.ownTypeId;
                this.setLoanInfo(loanInfo);
                LocalDB.setBorrowerId(String(loanInfo.borrowerId));
            }
         }
        await this.setIncomeInfo();
        await this.setPropertyInfo();

    }

    static startNewLoanApplication = () => {
        // LocalDB.clearSessionStorage();
    }

    static async getBorrowerInfo(loanapplicationId, borrowerId) {
        let borrowerResponse = await BorrowerActions.getBorrowerInfo(
            Number(loanapplicationId),
            Number(borrowerId)
        );
        return borrowerResponse;
    }

    static getLoanInfo() {

        let loanInfo: LoanInfoType = {
            loanApplicationId: LocalDB.getLoanAppliationId(),
            loanPurposeId: LocalDB.getLoanPurposeId(),
            loanGoalId: LocalDB.getLoanGoalId(),
            borrowerId: LocalDB.getBorrowerId(),
            ownTypeId: null,
            borrowerName: null,
        };
        return loanInfo;
    }

    static async extractLoanInfo(loanId) {
        // let loanApplicationId = LocalDB.getLoanAppliationId();
        let borrowerId = LocalDB.getBorrowerId();
        let borrowerInfo : any = {};
         if(borrowerId) {
             borrowerInfo = await this.getBorrowerInfo(loanId, borrowerId);
        }
        // loanInfoObj.borrowerId = borrowerId;
        let loanInfo = this.getLoanInfo();
        if (borrowerInfo) {
            loanInfo.ownTypeId = borrowerInfo?.ownTypeId;
            loanInfo.borrowerName = borrowerInfo.firstName;
        }
        this.setLoanInfo(loanInfo);
        return loanInfo;
    }

    static async getPrimaryBorrower(borrowerInfo = { id: null , name: null, ownTypeId: null }) {
        let loanApplicationId = LocalDB.getLoanAppliationId();
        let response = await BorrowerActions.getBorrowersForFirstReview(
            +loanApplicationId
        );
        if (response.data) {
            let primaryBorrower: GetReviewBorrowerInfoSectionProto = response.data.borrowerReviews?.find(
                (item) => item.ownTypeId === OwnTypeEnum.PrimaryBorrower
            );
            if (primaryBorrower) {
                borrowerInfo.id = primaryBorrower.borrowerId;
                borrowerInfo.name = primaryBorrower.firstName;
                this.setPrimaryBorrowerInfo(borrowerInfo);
            }
        }
        return borrowerInfo;
    }

    static setPrimaryBorrowerInfo(info) {
        this.config.dispatch({
            type: LoanApplicationActionsType.SetPrimaryBorrowerInfo,
            payload: info,
        });
    }

    static setLoanInfo(loanInfo) {
        this.config.dispatch({
            type: LoanApplicationActionsType.SetLoanInfo,
            payload: loanInfo,
        });
    }

    static setIncomeInfo() {
        let incomeId = LocalDB.getIncomeId();
        this.config.dispatch({ type: LoanApplicationActionsType.SetIncomeInfo, payload: { incomeId, incomeTypeId: null } });
    }

    static setPropertyInfo() {
        const { myPropertyInfo }: any = this.config.state.loanManager;
        let myPropertyTypeId = LocalDB.getMyPropertyTypeId();
        this.config.dispatch({ type: LoanApplicationActionsType.SetMyPropertyInfo, payload: { ...myPropertyInfo, primaryPropertyTypeId:  myPropertyTypeId} });
    }

}
