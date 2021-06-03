export class MyPropertyEndPoints {
    static GET = {
        getAllpropertytypes: (sectionId: number) => `/api/loanapplication/Property/GetAllPropertyTypes?sectionId=${sectionId}`,
        getPropertyValue: (loanApplicationId: number, borrowerPropertyId: number) => `/api/loanapplication/myproperty/GetPropertyValue?LoanApplicationId=${loanApplicationId}&BorrowerPropertyId=${borrowerPropertyId}`,
        getAllPropertyTypes: () => `/api/loanapplication/myproperty/GetPrimaryBorrowerAddressDetail`,
        getPrimaryBorrowerAddressDetail: (loanApplicationId: number) => `/api/loanapplication/myproperty/GetPrimaryBorrowerAddressDetail?LoanApplicationId=${loanApplicationId}`,
        getBorrowerPrimaryPropertyType: (loanApplicationId: number, borrowerPropertyId: number) => `/api/loanapplication/myproperty/getBorrowerPrimaryPropertyType?LoanApplicationId=${loanApplicationId}&BorrowerPropertyId=${borrowerPropertyId}`,
        doYouHaveFirstMortgage: (loanApplicationId: number, borrowerPropertyId: number) => `/api/loanapplication/MyProperty/DoYouHaveFirstMortgage?LoanApplicationId=${loanApplicationId}&BorrowerPropertyId=${borrowerPropertyId}`,
        doYouHaveSecondMortgage: (loanApplicationId: number, borrowerPropertyId: number) => `/api/loanapplication/MyProperty/DoYouHaveSecondMortgage?LoanApplicationId=${loanApplicationId}&BorrowerPropertyId=${borrowerPropertyId}`,
        getFirstMortgageValue: (loanApplicationId: number, borrowerPropertyId: number) => `/api/loanapplication/MyProperty/GetFirstMortgageValue?LoanApplicationId=${loanApplicationId}&BorrowerPropertyId=${borrowerPropertyId}`,
        getSecondMortgageValue: (loanApplicationId: number, borrowerPropertyId: number) => `/api/loanapplication/MyProperty/GetSecondMortgageValue?LoanApplicationId=${loanApplicationId}&BorrowerPropertyId=${borrowerPropertyId}`,
        getFinalScreenReview: (borrowerId: number, loanApplicationId: number) => `/api/loanapplication/MyProperty/GetFinalScreenReview?loanApplicationId=${loanApplicationId}&borrowerId=${borrowerId}`,

        getAllPropertyUsages: (sectionId: number) => `/api/loanapplication/Property/GetAllPropertyUsages?sectionId=${sectionId}`,
        getPropertyList: (loanApplicationId: number, borrowerId: number) => `/api/loanapplication/MyProperty/GetPropertyList?LoanApplicationId=${loanApplicationId}&BorrowerId=${borrowerId}`,
        doYouOwnAdditionalProperty: (loanApplicationId: number, borrowerId: number) => `/api/loanapplication/myproperty/DoYouOwnAdditionalProperty?LoanApplicationId=${loanApplicationId}&BorrowerId=${borrowerId}`,
        getBorrowerAdditionalPropertyType: (loanApplicationId: number, borrowerPropertyId: number) => `/api/loanapplication/MyProperty/GetBorrowerAdditionalPropertyType?borrowerPropertyId=${borrowerPropertyId}&loanApplicationId=${loanApplicationId}`,
        getBorrowerAdditionalPropertyInfo: (loanApplicationId: number, borrowerPropertyId: number) => `/api/loanapplication/MyProperty/GetBorrowerAdditionalPropertyInfo?borrowerPropertyId=${borrowerPropertyId}&loanApplicationId=${loanApplicationId}`,
        getBorrowerAdditionalPropertyAddress: (loanApplicationId: number, borrowerPropertyId: number) => `/api/loanapplication/MyProperty/GetBorrowerAdditionalPropertyAddress?borrowerPropertyId=${borrowerPropertyId}&loanApplicationId=${loanApplicationId}`,

    }

    static POST = {
        addOrUpdatePropertyValue: () => `/api/loanapplication/myproperty/AddOrUpdatePropertyValue`,
        addOrUpdatePrimaryPropertyType: () => `/api/loanapplication/myproperty/addOrUpdatePrimaryPropertyType`,
        addOrUpdateHasFirstMortgage: () => `/api/loanapplication/MyProperty/AddOrUpdateHasFirstMortgage`,
        addOrUpdateHasSecondMortgage: () => `/api/loanapplication/MyProperty/AddOrUpdateHasSecondMortgage`,
        addOrUpdateFirstMortgageValue: () => `/api/loanapplication/MyProperty/AddOrUpdateFirstMortgageValue`,
        addOrUpdateSecondMortgageValue: () => `/api/loanapplication/MyProperty/AddOrUpdateSecondMortgageValue`,

        addOrUpdatBorrowerAdditionalPropertyAddress: () => `/api/loanapplication/MyProperty/AddOrUpdatBorrowerAdditionalPropertyAddress`,
        addOrUpdateAdditionalPropertyType: () => `/api/loanapplication/MyProperty/AddOrUpdateAdditionalPropertyType`,
        addOrUpdateAdditionalPropertyInfo: () => `/api/loanapplication/MyProperty/AddOrUpdateAdditionalPropertyInfo`,
    }

    static PUT = {

    }

    static DELETE = {
        deleteProperty: (loanApplicationId: number, borrowerPropertyId: number) => `/api/loanapplication/myproperty/DeleteProperty?loanApplicationId=${loanApplicationId}&borrowerPropertyId=${borrowerPropertyId}`

    }
}