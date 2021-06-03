import { Http } from "rainsoft-js";
import { APIResponse } from "../../Entities/Models/APIResponse";
import { CurrentPropertyVal, FirstMortgageValue, HasFirstMortgage, HasSecondMortgage, SecondMortgageValue, PrimaryPropertyType } from "../../Entities/Models/CurrentResidence";
import { SectionTypeEnum } from "../../Utilities/Enumerations/MyPropertyEnums";
import { Endpoints } from "../endpoints/Endpoint";


export default class MyPropertyActions {

    static async getAllpropertytypes(sectionType: SectionTypeEnum) {
        let url = Endpoints.MyProperty.GET.getAllpropertytypes(sectionType);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async getPropertyValue(loanApplicationId: number, borrowerPropertyId: number) {
        let url = Endpoints.MyProperty.GET.getPropertyValue(loanApplicationId, borrowerPropertyId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async getPrimaryBorrowerAddressDetail(loanApplicationId: number) {
        let url = Endpoints.MyProperty.GET.getPrimaryBorrowerAddressDetail(loanApplicationId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async getFinalScreenReview(borrowerId: number, loanApplicationId: number) {
        let url = Endpoints.MyProperty.GET.getFinalScreenReview(borrowerId, loanApplicationId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async getBorrowerPrimaryPropertyType(loanApplicationId: number, borrowerPropertyId: number) {
        let url = Endpoints.MyProperty.GET.getBorrowerPrimaryPropertyType(loanApplicationId, borrowerPropertyId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async doYouHaveFirstMortgage(loanApplicationId: number, borrowerPropertyId: number) {
        let url = Endpoints.MyProperty.GET.doYouHaveFirstMortgage(loanApplicationId, borrowerPropertyId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async getFirstMortgageValue(loanApplicationId: number, borrowerPropertyId: number) {
        let url = Endpoints.MyProperty.GET.getFirstMortgageValue(loanApplicationId, borrowerPropertyId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async doYouHaveSecondMortgage(loanApplicationId: number, borrowerPropertyId: number) {
        let url = Endpoints.MyProperty.GET.doYouHaveSecondMortgage(loanApplicationId, borrowerPropertyId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async getSecondMortgageValue(loanApplicationId: number, borrowerPropertyId: number) {
        let url = Endpoints.MyProperty.GET.getSecondMortgageValue(loanApplicationId, borrowerPropertyId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async addOrUpdatePrimaryPropertyType(dataValues: PrimaryPropertyType) {

        const { LoanApplicationId, id, BorrowerId, PropertyTypeId, RentalIncome, State }: PrimaryPropertyType = dataValues;
        let url = Endpoints.MyProperty.POST.addOrUpdatePrimaryPropertyType();
        try {
            let response = await Http.post(url, {
                LoanApplicationId: LoanApplicationId,
                State: State,
                Id: id,
                BorrowerId: BorrowerId,
                PropertyTypeId: PropertyTypeId,
                RentalIncome: RentalIncome
            })
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }
    }

    static async addOrUpdatePropertyValue(dataValues: CurrentPropertyVal) {
        const { LoanApplicationId, Id, IsSelling, OwnersDue, PropertyValue, State, BorrowerId }: CurrentPropertyVal = dataValues;
        let url = Endpoints.MyProperty.POST.addOrUpdatePropertyValue();
        try {
            let response = await Http.post(url, {
                LoanApplicationId: LoanApplicationId,
                State: State,
                Id: Id,
                IsSelling: IsSelling,
                OwnersDue: OwnersDue,
                PropertyValue: PropertyValue,
                BorrowerId: BorrowerId
            })
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }
    }

    static async addOrUpdateHasFirstMortgage(dataValues: HasFirstMortgage) {
        const { LoanApplicationId, Id, HasFirstMortgage, PropertyTax, HomeOwnerInsurance, FloodInsurance, State }: HasFirstMortgage = dataValues;
        let url = Endpoints.MyProperty.POST.addOrUpdateHasFirstMortgage();
        try {
            let response = await Http.post(url, {
                LoanApplicationId: LoanApplicationId,
                State: State,
                Id: Id,
                HasFirstMortgage: HasFirstMortgage,
                PropertyTax: PropertyTax,
                HomeOwnerInsurance: HomeOwnerInsurance,
                FloodInsurance: FloodInsurance
            })
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }

    }

    static async addOrUpdateFirstMortgageValue(dataValues: FirstMortgageValue) {
        const { LoanApplicationId, Id, PropertyTax, PropertyTaxesIncludeinPayment, HomeOwnerInsurance, HomeOwnerInsuranceIncludeinPayment, FirstMortgagePayment, UnpaidFirstMortgagePayment, IsHeloc, HelocCreditLimit, FloodInsurance, FloodInsuranceIncludeinPayment, PaidAtClosing, State }: FirstMortgageValue = dataValues;
        let url = Endpoints.MyProperty.POST.addOrUpdateFirstMortgageValue();
        try {
            let response = await Http.post(url, {
                LoanApplicationId: LoanApplicationId,
                State: State,
                Id: Id,
                PropertyTax: PropertyTax,
                PropertyTaxesIncludeinPayment: PropertyTaxesIncludeinPayment,
                UnpaidFirstMortgagePayment: UnpaidFirstMortgagePayment,
                HomeOwnerInsurance: HomeOwnerInsurance,
                IsHeloc: IsHeloc,
                HelocCreditLimit: HelocCreditLimit,
                HomeOwnerInsuranceIncludeinPayment: HomeOwnerInsuranceIncludeinPayment,
                FirstMortgagePayment: FirstMortgagePayment,
                FloodInsurance: FloodInsurance,
                PaidAtClosing: PaidAtClosing,
                FloodInsuranceIncludeinPayment: FloodInsuranceIncludeinPayment
            })
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }

    }

    static async addOrUpdateHasSecondMortgage(dataValues: HasSecondMortgage) {
        const { LoanApplicationId, Id, HasSecondMortgage, State }: HasSecondMortgage = dataValues;
        let url = Endpoints.MyProperty.POST.addOrUpdateHasSecondMortgage();
        try {
            let response = await Http.post(url, {
                LoanApplicationId: LoanApplicationId,
                State: State,
                Id: Id,
                HasSecondMortgage: HasSecondMortgage,
            })
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }

    }

    static async addOrUpdateSecondMortgageValue(dataValues: SecondMortgageValue) {
        const { LoanApplicationId, Id, IsHeloc, HelocCreditLimit, SecondMortgagePayment, UnpaidSecondMortgagePayment, PaidAtClosing, State }: SecondMortgageValue = dataValues;
        let url = Endpoints.MyProperty.POST.addOrUpdateSecondMortgageValue();
        try {
            let response = await Http.post(url, {
                LoanApplicationId: LoanApplicationId,
                State: State,
                Id: Id,
                UnpaidSecondMortgagePayment: UnpaidSecondMortgagePayment,
                IsHeloc: IsHeloc,
                HelocCreditLimit: HelocCreditLimit,
                SecondMortgagePayment: SecondMortgagePayment,
                PaidAtClosing: PaidAtClosing
            })
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }

    }

    static async getAllPropertyUsages(sectionId: number) {
        let url = Endpoints.MyProperty.GET.getAllPropertyUsages(sectionId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }
   
    static async getPropertyList(loanApplicationId: number, borrowerId: number) {
        let url = Endpoints.MyProperty.GET.getPropertyList(loanApplicationId, borrowerId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async doYouOwnAdditionalProperty(loanApplicationId: number, borrowerId: number) {
        let url = Endpoints.MyProperty.GET.doYouOwnAdditionalProperty(loanApplicationId, borrowerId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async getBorrowerAdditionalPropertyType(loanApplicationId: number, borrowerPropertyId: number) {
        let url = Endpoints.MyProperty.GET.getBorrowerAdditionalPropertyType(loanApplicationId, borrowerPropertyId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async getBorrowerAdditionalPropertyInfo(loanApplicationId: number, borrowerPropertyId: number) {
        let url = Endpoints.MyProperty.GET.getBorrowerAdditionalPropertyInfo(loanApplicationId, borrowerPropertyId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async getBorrowerAdditionalPropertyAddress(loanApplicationId: number, borrowerPropertyId: number) {
        let url = Endpoints.MyProperty.GET.getBorrowerAdditionalPropertyAddress(loanApplicationId, borrowerPropertyId);
        try {
            let response = await Http.get(url);
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return error;
        }
    }

    static async addOrUpdatBorrowerAdditionalPropertyAddress(dataValues) {
        let url = Endpoints.MyProperty.POST.addOrUpdatBorrowerAdditionalPropertyAddress();
        try {
            let response = await Http.post(url, dataValues)
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }

    }

    static async addOrUpdateAdditionalPropertyType(dataValues: PrimaryPropertyType) {

        const { LoanApplicationId, id, BorrowerId, PropertyTypeId, RentalIncome, State }: PrimaryPropertyType = dataValues;

        let url = Endpoints.MyProperty.POST.addOrUpdateAdditionalPropertyType();
        try {
            let response = await Http.post(url, {
                LoanApplicationId: LoanApplicationId,
                State: State,
                Id: id,
                BorrowerId: BorrowerId,
                PropertyTypeId: PropertyTypeId,
                RentalIncome: RentalIncome
            })
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }

    }

    static async addOrUpdateAdditionalPropertyInfo(dataValues) {
        let url = Endpoints.MyProperty.POST.addOrUpdateAdditionalPropertyInfo();
        try {
            let response = await Http.post(url, dataValues)
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }

    }
   
    static async deleteProperty(loanApplicationId: number, borrowerPropertyId: number) {
        let url = Endpoints.MyProperty.DELETE.deleteProperty(loanApplicationId, borrowerPropertyId);
        try {
            let response = await Http.delete(url)
            return new APIResponse(response.status, response.data);
        } catch (error) {
            console.log(error);
            return null;
        }
    }
}
