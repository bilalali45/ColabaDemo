import { NavStepConfigType } from "../LoanNavigator"

export enum MyMoneySubNav {
    Income = 'Income',
    Assets = 'Assets'
}

export enum MyIncomeSteps {
    IncomeHome = "Income Home",
    EmploymentHistory = "Employment History",
    IncomeReview = "IncomeReview"
}

export enum IncomeHomeSteps {
    IncomeSourcesHome = "Income Sources Home",
    EmploymentAlert = "Employment Alert",
}

export enum IncomeSourcesHomeSteps {
    IncomeSources = "Income Sources",
    Employment = "Employment",
    SelfIncome = "Self Income",
    Business = "Business",
    Military = "Military",
    Retirement = "Retirement",
    Rental = "Rental",
    Other = "Other",
}

export enum IncomeEmploymentSteps {
    EmploymentIncome = "Employment Income",
    EmployerAddress = "Employer Address",
    ModeOfEmploymentIncomePayment = "Mode Of Employment Income Payment",
    AdditionalIncome = "Additional Income"
}

export enum IncomeSelfEmploymentSteps {
    SelfEmploymentIncome = "Self Employment Income",
    SelfEmploymentAddress = "Self Employment Address",
    NetSelfEmploymentIncome = "Net Self Employment Income"
}

export enum BusinessIncomeSteps {
    BusinessIncomeType = "Business Income Type",
    BusinessAddress = "Business Address",
    BusinessRevenue = "Business Revenue",
}

export enum IncomeMIlitarySteps {
    MilitaryIncome = "Military Income",
    MilitaryServiceLocation = "Military Service Location",
    ModeOfMilitaryServicePayment = "Mode Of Military Service Payment",
}

export enum IncomeRetirementSteps {
    RetirementIncomeSource = "Retirement Income Source"
}

export enum OtherIncomeSteps {
    OtherIncome = "Other Income",
    OtherIncomeDetails = "Other Income Details"
}

export enum EmploymentHistorySteps {
    EmploymentHistoryDetails = "Employment History Details"
}

export enum EmploymentHistoryDetailsSteps {
    EmploymentHistoryDetails = "Employment History Details",
    PreviousEmployment = "Previous Employment",
    PreviousEmploymentAddress = "Previous Employment Address",
    PreviousEmploymentAmount = "Previous Employment Amount",
}

export enum MyAssetsSteps {
    EarnestMoneyDeposit = 'Earnest Money Deposit',
    AssetsHome = "Assets Home"
}

export enum AssetsHomeSteps {
    AssetSourcesHome = "Asset Sources Home"
}

export enum AssetSourcesHomeSteps {
    AssetSources = "Asset Sources",
    BankAccount = "Bank Account",
    RetirementAccount = "Retirement Account",
    GiftFunds = "Gift Funds",
    OtherFinancialAssets = "Other Financial Assets",
    ProceedsFromTransaction = "Proceeds From Transaction",
    OtherAssets = "Other Assets",
}

export enum BankAccountSteps {
    DetailsOfBankAccount = "Details Of Bank Account",
}
export enum RetirementAccounctSteps {
    RetirementAccountDetails = "Retirement Account Details"
}
export enum GiftFundsSteps {
    GiftFundsSource = "Gift Funds Source",
    GiftFundsDetails = "Gift Funds Details"
}
export enum OtherFinancialAssetsSteps {
    TypeOfFinancialAssets = "Type Of Financial Assets",
    FinancialAssetsDetails = "Financial Assets Detail"

}
export enum ProceedsFromTransactionSteps {
    TypeOfProceedsFromTransaction = "Type Of Proceeds From Transaction",
    DetailsOfProceedsFromTransaction = "Details Of Proceeds From Transaction",
}
export enum OtherAssetsSteps {
    OtherAssetsDetails = "Other Assets Details"
}



export class MyMoney {

    static incomeEmploymentSteps: NavStepConfigType[] = [
        {
            name: IncomeEmploymentSteps.EmploymentIncome,
            subSteps: []
        },
        {
            name: IncomeEmploymentSteps.EmployerAddress,
            subSteps: []
        },
        {
            name: IncomeEmploymentSteps.ModeOfEmploymentIncomePayment,
            subSteps: []
        },
        {
            name: IncomeEmploymentSteps.AdditionalIncome,
            subSteps: []
        }
    ]

    static incomeSelfEmploymentSteps: NavStepConfigType[] = [
        {
            name: IncomeSelfEmploymentSteps.SelfEmploymentIncome,
            subSteps: []
        },
        {
            name: IncomeSelfEmploymentSteps.SelfEmploymentAddress,
            subSteps: []
        },
        {
            name: IncomeSelfEmploymentSteps.NetSelfEmploymentIncome,
            subSteps: []
        },
    ]

    static businessIncomeSteps: NavStepConfigType[] = [
        {
            name: BusinessIncomeSteps.BusinessIncomeType,
            subSteps: []
        },
        {
            name: BusinessIncomeSteps.BusinessAddress,
            subSteps: []
        },
        {
            name: BusinessIncomeSteps.BusinessRevenue,
            subSteps: []
        },
    ]

    static incomeMilitarySteps: NavStepConfigType[] = [
        {
            name: IncomeMIlitarySteps.MilitaryIncome,
            subSteps: []
        },
        {
            name: IncomeMIlitarySteps.MilitaryServiceLocation,
            subSteps: []
        },
        {
            name: IncomeMIlitarySteps.ModeOfMilitaryServicePayment,
            subSteps: []
        },
    ]

    static incomeRetirementSteps: NavStepConfigType[] = [
        {
            name: IncomeRetirementSteps.RetirementIncomeSource,
            subSteps: []
        },
    ]

    static otherIncomeSteps: NavStepConfigType[] = [
        {
            name: OtherIncomeSteps.OtherIncome,
            subSteps: []
        },
        {
            name: OtherIncomeSteps.OtherIncomeDetails,
            subSteps: []
        },
    ]

    static incomeHomeSteps: NavStepConfigType[] = [
        {
            name: IncomeHomeSteps.IncomeSourcesHome,
            subSteps: [
                {
                    name: IncomeSourcesHomeSteps.IncomeSources,
                    subSteps: MyMoney.incomeEmploymentSteps,
                    partOfAWizard: true,
                    wizardName: MyIncomeSteps.IncomeHome

                },
                {
                    name: IncomeSourcesHomeSteps.Employment,
                    subSteps: MyMoney.incomeEmploymentSteps,
                    partOfAWizard: true,
                    wizardName: MyIncomeSteps.IncomeHome

                },
                {
                    name: IncomeSourcesHomeSteps.SelfIncome,
                    subSteps: MyMoney.incomeSelfEmploymentSteps,
                    partOfAWizard: true,
                    wizardName: MyIncomeSteps.IncomeHome
                },
                {
                    name: IncomeSourcesHomeSteps.Business,
                    subSteps: MyMoney.businessIncomeSteps,
                    partOfAWizard: true,
                    wizardName: MyIncomeSteps.IncomeHome
                },
                {
                    name: IncomeSourcesHomeSteps.Military,
                    subSteps: MyMoney.incomeMilitarySteps,
                    partOfAWizard: true,
                    wizardName: MyIncomeSteps.IncomeHome
                },
                {
                    name: IncomeSourcesHomeSteps.Retirement,
                    subSteps: MyMoney.incomeRetirementSteps,
                    partOfAWizard: true,
                    wizardName: MyIncomeSteps.IncomeHome
                },
                {
                    name: IncomeSourcesHomeSteps.Rental,
                    subSteps: [],
                    partOfAWizard: true,
                    wizardName: MyIncomeSteps.IncomeHome
                },
                {
                    name: IncomeSourcesHomeSteps.Other,
                    subSteps: MyMoney.otherIncomeSteps,
                    partOfAWizard: true,
                    wizardName: MyIncomeSteps.IncomeHome
                },
            ]
        },
        {
            name: IncomeHomeSteps.EmploymentAlert,
            subSteps: []
        },
    ]

    static myIncomeSteps: NavStepConfigType[] = [
        {
            name: MyIncomeSteps.IncomeHome,
            subSteps: MyMoney.incomeHomeSteps,
        },
        {
            name: MyIncomeSteps.EmploymentHistory,
            subSteps: [
                {
                    name: EmploymentHistoryDetailsSteps.EmploymentHistoryDetails,
                    partOfAWizard: true,
                    wizardName: MyIncomeSteps.EmploymentHistory,
                    subSteps: [
                        {
                            name: EmploymentHistoryDetailsSteps.PreviousEmployment,
                            subSteps: []
                        },
                        {
                            name: EmploymentHistoryDetailsSteps.PreviousEmploymentAddress,
                            subSteps: []
                        },
                        {
                            name: EmploymentHistoryDetailsSteps.PreviousEmploymentAmount,
                            subSteps: []
                        },
                    ]
                }
            ]
        },
        {
            name: MyIncomeSteps.IncomeReview,
            subSteps: []
        },
    ]

    static bankAccountSteps: NavStepConfigType[] = [
        {
            name: BankAccountSteps.DetailsOfBankAccount,
            subSteps: []
        }
    ];

    static retirementAccountSteps: NavStepConfigType[] = [
        {
            name: RetirementAccounctSteps.RetirementAccountDetails,
            subSteps: []
        }
    ]

    static giftFundsSteps: NavStepConfigType[] = [
        {
            name: GiftFundsSteps.GiftFundsSource,
            subSteps: []
        },
        {
            name: GiftFundsSteps.GiftFundsDetails,
            subSteps: []
        },
    ]
    static otherFinancialAssetsSteps: NavStepConfigType[] = [
        {
            name: OtherFinancialAssetsSteps.TypeOfFinancialAssets,
            subSteps: []
        },
        {
            name: OtherFinancialAssetsSteps.FinancialAssetsDetails,
            subSteps: []
        },
    ];

    static proceedsFromTransactionSteps: NavStepConfigType[] = [
        {
            name: ProceedsFromTransactionSteps.TypeOfProceedsFromTransaction,
            subSteps: []
        },
        {
            name: ProceedsFromTransactionSteps.DetailsOfProceedsFromTransaction,
            subSteps: []
        },
    ];

    static otherAssetsSteps: NavStepConfigType[] = [
        {
            name: OtherAssetsSteps.OtherAssetsDetails,
            subSteps: []
        }
    ]

    static assetSourcesHomeSteps: NavStepConfigType[] = [
        {
            name: AssetSourcesHomeSteps.AssetSources,
            subSteps: [],
            partOfAWizard: true,
            wizardName: AssetSourcesHomeSteps.AssetSources

        },
        {
            name: AssetSourcesHomeSteps.BankAccount,
            subSteps: MyMoney.bankAccountSteps,
            partOfAWizard: true,
            wizardName: AssetSourcesHomeSteps.AssetSources
        },
        {
            name: AssetSourcesHomeSteps.RetirementAccount,
            subSteps: MyMoney.retirementAccountSteps,
            partOfAWizard: true,
            wizardName: AssetSourcesHomeSteps.AssetSources
        },
        {
            name: AssetSourcesHomeSteps.GiftFunds,
            subSteps: MyMoney.giftFundsSteps,
            partOfAWizard: true,
            wizardName: AssetSourcesHomeSteps.AssetSources
        },
        {
            name: AssetSourcesHomeSteps.OtherFinancialAssets,
            subSteps: MyMoney.otherFinancialAssetsSteps,
            partOfAWizard: true,
            wizardName: AssetSourcesHomeSteps.AssetSources
        },
        {
            name: AssetSourcesHomeSteps.ProceedsFromTransaction,
            subSteps: MyMoney.proceedsFromTransactionSteps,
            partOfAWizard: true,
            wizardName: AssetSourcesHomeSteps.AssetSources
        },
        {
            name: AssetSourcesHomeSteps.OtherAssets,
            subSteps: MyMoney.otherAssetsSteps,
            partOfAWizard: true,
            wizardName: AssetSourcesHomeSteps.AssetSources
        },
    ];

    static myAssetsSteps = [
        {
            name: MyAssetsSteps.EarnestMoneyDeposit,
            subSteps: []
        },
        {
            name: MyAssetsSteps.AssetsHome,
            subSteps: [
                {
                    name: AssetsHomeSteps.AssetSourcesHome,
                    subSteps: MyMoney.assetSourcesHomeSteps
                }
            ]
        },
    ]
}

