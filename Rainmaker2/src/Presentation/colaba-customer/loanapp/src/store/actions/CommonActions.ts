import { Http } from "rainsoft-js";
import { GetReviewBorrowerInfoSectionProto, LoanInfoType } from "../../Entities/Models/types";
import { LocalDB } from "../../lib/LocalDB";
import { OwnTypeEnum } from "../../Utilities/Enum";
import { Endpoints } from "../endpoints/Endpoint";
import { LoanApplicationActionsType } from "../reducers/LoanApplicationReducer";
import BorrowerActions from "./BorrowerActions";


export class CommonActions {
    static async getAllMaritialStatuses() {
        let url = Endpoints.Common.GET.getallmaritalstatus()
        try {
            let response: any = await Http.get(url, {}, false);
            return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }

    static async getPendingLoanApplication(loanAppId: string | null | undefined) {
        
        if (!loanAppId) {
            return;
        }

        let url = `/api/loanapplication/loan/getpendingloanapplication${!isNaN(Number(loanAppId)) ? `?loanApplicationId=${loanAppId}` : ''}`;

        try {
            let res: any = await Http.get(url, {}, false);
            console.log('===============================res', res);
            return res?.data;
        } catch (error) {
            console.log('error', error);
            return undefined
        }
    }

    static async getAllloanpurpose() {
        let url = Endpoints.Common.GET.getAllloanpurpose()
        try {
            let response: any = await Http.get(url, {}, false);
            return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }

    static async saveNavigation(navigationState) {
        let url = Endpoints.Common.POST.saveNavigation()
        try {
            let response: any = await Http.post(url, navigationState);
            return response.data;
        } catch (error) {
            console.log(error);
            return undefined;
        }
    }

    static async resettoPrimaryBorrower(loanInfo: LoanInfoType, dispatch:Function) {
        const loanapplicationId = LocalDB.getLoanAppliationId();
        if (loanapplicationId) {
          let response = await BorrowerActions.getBorrowersForFirstReview(
            Number(loanapplicationId)
          );
          if (response.data) {
            let primaryBorrower: GetReviewBorrowerInfoSectionProto = response.data.borrowerReviews?.find(
              (item: GetReviewBorrowerInfoSectionProto) => item.ownTypeId === OwnTypeEnum.PrimaryBorrower
            );
            if (primaryBorrower) {
              dispatch({
                type: LoanApplicationActionsType.SetLoanInfo,
                payload: {
                  ...loanInfo,
                  borrowerId: primaryBorrower.borrowerId,
                  ownTypeId: primaryBorrower?.ownTypeId,
                  borrowerName: primaryBorrower.firstName+' '+primaryBorrower.lastName,
                },
              });
              LocalDB.setBorrowerId(String(primaryBorrower.borrowerId));
            }
          }
        }
      }

};

