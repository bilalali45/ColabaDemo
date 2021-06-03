export enum TenantConfigConditionEnum {
    Default = 0,
    Required = 1,
    Hidden = 2,
    Optional = 3
}

export enum TenantConfigType {
    Navigation = 1,
    Field = 2
}


export enum TenantConfigFieldNameEnum {
    AnyPartOfDownPaymentGift = 'AnyPartOfDownPaymentGift',
    CoBorowerVeteranStatus = 'CoBorower_VeteranStatus',
    CoBorrowerEmailAddress = 'CoBorrower_EmailAddress',
    CoBorrowerHomeNumber = 'CoBorrower_HomeNumber',
    CoBorrowerWorkNumber = 'CoBorrower_WorkNumber',
    CoBorrowerCellNumber = 'CoBorrower_CellNumber',
    CoBorrowerDOB = 'CoBorrower_DOB',
    CoBorrowerSSN = 'CoBorrower_SSN',
    PrimaryBorrowerCellNumber = 'PrimaryBorrower_CellNumber',
    PrimaryBorrowerDOB = 'PrimaryBorrower_DOB',
    PrimaryBorrowerHomeNumber = 'PrimaryBorrower_HomeNumber',
    PrimaryBorrowerSSN = 'PrimaryBorrower_SSN',
    PrimaryBorrowerWorkNumber = 'PrimaryBorrower_WorkNumber',
    PreviosEmployment = 'PreviosEmployment',
    IncomeSection = 'IncomeSection',
    PropertyTypeSubjectProperty = 'PropertyType_SubjectProperty',
    PropertyTypeMyProperties = 'PropertyType_MyProperties',    
    PrimaryBorowerVeteranStatus = 'PrimaryBorower_VeteranStatus',
    WhereAreYouInPurchaseProcess = 'WhereAreYouInPurchaseProcess',
    MyProperties = 'MyProperties',
    TaxIncludedInPayment = 'TaxIncludedInPayment',
    InsuranceIncludedInPayment = 'InsuranceIncludedInPayment',
    AdditionalPropertyMortgage = 'AdditionalPropertyMortgage',
    CurrentResidenceMortgage = 'CurrentResidenceMortgage'
}