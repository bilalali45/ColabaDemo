import { TransactionProceedsFromLoanDTO, TransactionProceedsFromRealAndNonRealEstateDTO } from "./TransactionProceeds";

export type State = {
  id?: number;
  countryId?: number;
  name?: string;
  shortCode?: string;
};

export type Country = {
  id?: number;
  name?: string;
  shortCode?: string;
};

export type HomeOwnershipTypes = {
  id: number;
  name: string;
};

export interface HomeAddressObject {
  street?: string,
  unit?: string,
  city?: string,
  zipCode?: string,
  state?: State,
  country?: Country,
}

export interface CurrentHomeAddressReqObj {

  loanApplicationId?: number,
  id?: number,
  street?: string,
  unit?: string,
  city?: string,
  stateId?: number,
  stateName?: string,
  zipCode?: string,
  countryId?: number,
  countryName?: string,
  housingStatusId?: number,
  rent?: number | null,
  state?: string,

}

export type MilitaryIncomeInfo = {
  loanApplicationId?: number;
  id?: number;
  borrowerId?: number;
  employerName?: string;
  jobTitle?: string;
  startDate?: string;
  yearsInProfession?: string;
  address?: ServiceLocationAddressObject;
  monthlyBaseSalary?: number;
  militaryEntitlements?: number;
  state?: string;
}

export type MilitaryIncomeEmployer = {
  EmployerName?: string;
  JobTitle?: string;
  startDate?: string;
  YearsInProfession?: number | null;
}

export type MilitaryPaymentMode = {
  baseSalary?: string;
  entitlementL?: string;
}

export type IncomeSourceType = {
  id: number;
  name: string;
  displayName: string;
}

export type AssetSourceType = {
  id: number;
  name: string;
  displayName: string;
  path?: string;
}

export type ServiceLocationAddressObject = {
  street?: string,
  unit?: string,
  city?: string,
  stateId?: number,
  zipCode?: string,
  countryId: number,
  countryName: string,
  stateName: string
}

export type OtherIncomeSubmitDataType = {
  monthlyBaseIncome?: number;
  annualBaseIncome?: number;
  description?: string;
}

export type SelectedOtherIncomeType = {
  id: number;
  name: string;
  monthlySalary: string | null;
  annualSalary: string | null;
  description: string | null;
  fieldsInfo: string;
}
export type OtherIncomeType = {
  LoanApplicationId: number,
  IncomeInfoId: number,
  BorrowerId: number,
  MonthlyBaseIncome?: number | null,
  AnnualBaseIncome?: number | null,
  Description?: string | null,
  IncomeTypeId: number | null,
  State: string
}

export type OtherIncomeListType = {
  incomeGroupId: number;
  incomeGroupName: string;
  imageUrl?: string;
  incomeGroupDescription?: string;
  incomeGroupDisplayOrder: number;
  incomeTypes: SelectedOtherIncomeType[]
}

export type FieldsType = {
  Enabled: boolean;
  caption: string;
  name: string;
  datatype: string;
  displayOrder: number;
  maxLendth?: number;
}


export interface SubjectAddressPropertyReqObj {

  loanApplicationId: number,
  street: string,
  unit: string,
  city: string,
  stateId: number,
  stateName?: string,
  zipCode: string,
  estimatedClosingDate: string,
  state: string,

}

export interface DobSSNReqObj {

  LoanApplicationId: number,
  BorrowerId: number,
  DobUtc: string | null,
  Ssn: string | null
}

export type SearchByZipCodeCityCountryStateRes = {
  label: string;
  ids: string;
  stateName: string;
};

export type GetZipCodeByStateCountyCityNameRes = {
  stateId: number;
  stateName: string;
  cityId: number;
  cityName: string;
  countyId: number;
  countyName: string;
  zipPostalCode: string;
  abbreviation: string;
};
export type LoanOfficerType = {
  name: string;
  image: string;
  phone: number;
  email: string;
  url: string;
  nmlsNo: string;
  isLoanOfficer: boolean;
};

export type LoanPurposeType = {
  id: number;
  name: string;
  image: string;
};

export type LoanGoalType = {
  id: number;
  name: string;
  image: string;
};

export type LoanInfoType = {
  loanApplicationId: number | null;
  loanPurposeId: number | null;
  loanGoalId: number | null;
  borrowerId?: number | null;
  ownTypeId: number | null;
  borrowerName: string | null;
};


export type Borrower = {
  id: number;
  firstName: string;
  lastName: string;
  ownTypeId: number;
};

export type GetReviewBorrowerInfoSectionProto = {
  borrowerAddress: BorrowerAddressProto | null;
  borrowerId: number;
  firstName: string;
  middleName: string;
  lastName: string;
  emailAddress: string;
  homePhone: number;
  workPhone: number;
  workPhoneExt: number;
  isVaEligible: boolean | null;
  maritalStatusId: number | null;
  cellPhone: number;
  ownTypeId: number;
};

export type GetReviewBorrowerInfoSectionPayload = {
  loanPurpose: number;
  applyingWithSomeone: boolean;
  borrowerReviews: GetReviewBorrowerInfoSectionProto[];
  loanGoal: number;
};

export type PrimaryBorrowerInfo = {
  id: number | null;
  name: string | null;
};
export type IncomeInfo = {
  incomeId: number;
  incomeTypeId: number;
};

export type AssetInfo = {
  borrowerName: string;
  borrowerAssetId: number;
  assetCategoryId: number;
  assetTypeId?: number;
  displayName: string;
};

export type AssetPayloadType = {
  LoanApplicationId: number;
  BorrowerId: number;
  BorrowerAssetId: number;
  AssetCategoryId: number;
  AssetTypeId: number;
  InstitutionName: string;
  AccountNumber: string;
  AssetValue: number;
  State: string;
}

export type RetirementPayloadType = {
  LoanApplicationId: number;
  BorrowerId: number;
  InstitutionName: string;
  AccountNumber: string;
  Value: number;
  Id: number;
}

export type FinancialSatementPayloadType = {
  LoanApplicationId: number;
  BorrowerId: number;
  InstitutionName: string;
  AccountNumber: string;
  Balance: number;
  Id: number;
  AssetTypeId: number;
  State: string;
}

export type FinancialAssets = {
  id: number;
  name: string;
}

export type MyPropertyInfo = {
  primaryPropertyTypeId: number,
};

export type AdditionalPropertyInfo = {
  id: number,
};

export type GetBorrowerVaDetailPayload = {
  isVaEligible: boolean;
  vaDetails: VaDetailsProto[];
};

export type VaDetailsProto = {
  expirationDateUtc?: string;
  militaryAffiliationId: number;
  reserveEverActivated?: boolean;
};

export type GetBorrowerVaDetailFormObjectProto = {
  performedMilitaryService: string;
  activeDutyPersonnel: string;
  lastDateOfTourOrService: string | Date;
  reserveOrNationalGuard: string;
  everActivatedDuringTour: string;
  veteran: string;
  survivingSpouse: string;
  serviceTypeErrors?: string;
};

export type tabValidationArrayType = {
  tabId: number;
  validated: boolean;
};

export type SSNTabValidation = {
  tabId: number;
  validated: boolean;
  hasSSNValue?: boolean;
  isSSNRequired?: boolean;
  hasDobValue?: boolean;
  isDobRequired?: boolean;
}
export type consentType = {
  id: number;
  name: string;
  description: string;
};

export type ReviewBorrowerInfo = {
  applyingWithSomeone: boolean;
  loanPurpose: number;
  borrowerReviews: GetReviewBorrowerInfoSectionProto[];
};

export type LoanpurposeProto = {
  id: number;
  name: number;
  image: string;
};


export type BorrowerAddressProto = {
  countryId?: number
  countryName?: string
  stateId?: number
  stateName?: string
  countyId?: number
  countyName?: string
  cityId?: number
  cityName?: string
  streetAddress?: string
  zipCode?: number
  unitNo?: number
};

export type SubjectPropertyUsagesProto = {
  id: number
  name: string
  image: string
}



export type MortgagePropertyReviewProto = {
  applyingWithSomeone: boolean;
  loanPurpose: number;
  borrowerReviews: GetReviewBorrowerInfoSectionProto[];
  loanGoal: number
  loanGoalName: string
  propertyInfo: MortgagePropertyInfoProto

};

export type MortgagePropertyInfoProto = {
  propertyUsageId: number
  propertyTypeId: number
  propertyTypeName: string
  propertyUsageName: string
  addressInfo: BorrowerAddressProto
}

export type IncomeProto = {
  incomeName: string
  incomeValue: number
  incomeId: number
  incomeTypeId: number
  incomeTypeDisplayName: string
  employmentCategory: EmploymentCategory
  isCurrentIncome: boolean
}

export type IncomeBorrowerProto = {
  borrowerName: string
  incomes: Array<IncomeProto>
  monthlyIncome: number
  borrowerId: number
  ownTypeId: number
  ownTypeName: string
  ownTypeDisplayName: string
}

export type IncomeHomeBorrowerProto = {
  totalMonthlyQualifyingIncome: number
  borrowers: Array<IncomeBorrowerProto>
}

export type RetirementIncomeInfo = {
  id?: number;
  loanApplicationId?: number;
  incomeInfoId?: number;
  borrowerId?: number;
  incomeTypeId?: number;
  monthlyBaseIncome?: number;
  employerName?: string;
  description?: string;
  state?: string;
}

export type BusinessType = {
  id: number
  name: string
  fieldsInfo: string | null
}
export type EmploymentCategory = {
  categoryId: number
  categoryName: string
  categoryDisplayName: string
}

export type GiftSource = {
  id: number;
  name: string;
  imageUrl: string;
}

export type GiftAsset = {
  id?: number;
  loanApplicationId: number;
  borrowerId: number;
  assetTypeId: number;
  giftSourceId: number;
  isDeposited?: boolean;
  value?: number;
  valueDate?: string;
  state: string;
}


export type AssetTypesByCategory = {
  id: number,
  assetCategoryId: number,
  name: string
  displayName: string
  tenantAlternateName: null
  imageUrl?: string | null
  fieldsInfo?: string | null;
}

export type AssetProto = {
  assetName: string
  assetCategoryName: string
  assetValue: number
  assetId: number
  assetTypeID: number
  assetCategoryId: number
  isCurrentAsset: boolean,
  categoryId: string
}

export type AssetBorrowerProto = {
  borrowerName: string
  borrowerAssets: Array<AssetProto>
  borrowerId: number
  ownTypeId: number
  ownTypeName: string
  ownTypeDisplayName: string
  assetsValue: number
}

export type AssetsHomeBorrowerProto = {
  totalAssetsValue: number
  borrowers: Array<AssetBorrowerProto>
}

export type AssetCategory = {
  assetCategoryId: number
  categoryName: string
  categoryDisplayName: string
}

export type TransactionProceeds = {
  transactionProceedsFromRealAndNonRealEstate: TransactionProceedsFromRealAndNonRealEstateDTO,
  transactionProceedsFromLoan: TransactionProceedsFromLoanDTO,
}


export type CollateralAssetTypesProto = {
  id: number,
  name: string,
  displayName: string
}
export type MyPrimaryPropertyType = {
  id: number,
  propertyTypeId: number,
  rentalIncome: number
}

export type AddOrUpdateOtherAssetsInfoProto = {
  AssetTypeId: number,
  Value: number,
  InstitutionName: string,
  AccountNumber: string,
  Description: string,
  AssetId: number,
  BorrowerId: number
}

export type MyPropertyReviewProto = {
  primaryApplicantName: string;
  propertyList: GetReviewMyPropertyInfoSectionProto[]
};

export type GetReviewMyPropertyInfoSectionProto = {
  id: number;
  propertyType: string;
  typeId: number;
  firstName: string;
  lastName: string;
  ownTypeId: number;
  street: string;
  unit: string;
  city: string;
  stateId: number;
  zipCode: string;
  countryId: number;
  countryName: string;
  stateName: string
};

export type NavState = {
  lastPath: string, 
  history: [], 
  disabledSteps: []
}

export type HomeAddress = {
  street: string, 
  city: string, 
  stateName: string, 
  zipCode: string
}
export type propertyItemObj = {
  typeId: number, 
  propertyType: string, 
  address: HomeAddress, 
  id: number
}