import { LoanNagivator } from "../LoanNavigator";
import { Borrower } from "../../../Entities/Models/types";
import { OwnTypeEnum } from "../../Enum";
import { TenantConfigConditionEnum, TenantConfigFieldNameEnum, TenantConfigType } from "../../Enumerations/TenantConfigEnums";

export const featureSettings = [
    // type 1 is for Navigation and 2 is for field
    {
        name: TenantConfigFieldNameEnum.AnyPartOfDownPaymentGift,
        dbname: "AnyPartOfDownPaymentGift",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.CoBorowerVeteranStatus, // Conditional Handle on martial post click 
        dbname: "CoBorower_VeteranStatus",
        type: TenantConfigType.Navigation,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.CoBorrowerCellNumber,
        dbname: "CoBorrower_CellNumber",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.CoBorrowerEmailAddress,
        dbname: "CoBorrower_EmailAddress",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.CoBorrowerHomeNumber,
        dbname: "CoBorrower_HomeNumber",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.CoBorrowerWorkNumber,
        dbname: "CoBorrower_WorkNumber",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.CoBorrowerDOB,
        dbname: "CoBorrower_DOB",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.CoBorrowerSSN,
        dbname: "CoBorrower_SSN",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },

    {
        name: TenantConfigFieldNameEnum.PrimaryBorrowerCellNumber,
        dbname: "PrimaryBorrower_CellNumber",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.PrimaryBorrowerHomeNumber,
        dbname: "PrimaryBorrower_HomeNumber",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.PrimaryBorrowerWorkNumber,
        dbname: "PrimaryBorrower_WorkNumber",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.PrimaryBorrowerDOB,
        dbname: "PrimaryBorrower_DOB",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.PrimaryBorrowerSSN,
        dbname: "PrimaryBorrower_SSN",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.PreviosEmployment,
        dbname: "EmploymentHistorySection",
        type: TenantConfigType.Navigation,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.IncomeSection,
        dbname: "IncomeSection",
        type: TenantConfigType.Navigation,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.PrimaryBorowerVeteranStatus,
        dbname: "PrimaryBorower_VeteranStatus",
        type: TenantConfigType.Navigation,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.WhereAreYouInPurchaseProcess,
        dbname: "WhereAreYouInPurchaseProcess",
        type: TenantConfigType.Navigation,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.PropertyTypeSubjectProperty,
        dbname: "PropertyType_SubjectProperty",
        type: TenantConfigType.Navigation,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.MyProperties,
        dbname: "MyProperties",
        type: TenantConfigType.Navigation,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.TaxIncludedInPayment,
        dbname: "TaxIncludedInPayment",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.InsuranceIncludedInPayment,
        dbname: "InsuranceIncludedInPayment",
        type: TenantConfigType.Field,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.AdditionalPropertyMortgage,
        dbname: "AdditionalPropertyMortgage",
        type: TenantConfigType.Navigation,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.CurrentResidenceMortgage,
        dbname: "CurrentResidenceMortgage",
        type: TenantConfigType.Navigation,
        condition: TenantConfigConditionEnum.Default
    },
    {
        name: TenantConfigFieldNameEnum.PropertyTypeMyProperties,
        dbname: "PropertyType_MyProperties",
        type: TenantConfigType.Navigation,
        condition: TenantConfigConditionEnum.Default
    }    
]

export class NavigationSettings {
    static settings: any[] = [];
    static navigation: LoanNagivator = null

    static applySettings() {
        let featuresToDisable: any = featureSettings.filter(ms => ms.condition == TenantConfigConditionEnum.Hidden);

        this.disableFeatures(featuresToDisable?.map(fet => fet?.name));
    }

    static disableFeatures(featuresToDisable: string[]) {
        for (const feature of featuresToDisable) {
            this.toggleFeature(this.navigation.navItems, feature, true);
        }
    }
    static enableFeatures(featuresToDisable: string[]) {
        for (const feature of featuresToDisable) {
            this.toggleFeature(this.navigation.navItems, feature, false);
        }
    }

    static disableFeature(name) {
        this.toggleFeature(this.navigation.navItems, name, true);
        this.toggleNestedFeatures(this.navigation.navItems, true);
    }

    static enableFeature(name) {
        this.toggleFeature(this.navigation.navItems, name, false);
        this.toggleNestedFeatures(this.navigation.navItems, false);
    }

    private static toggleFeature(features, featureToDisable, disabled) {
        for (const feature of features) {
            if (featureToDisable === feature.name) {
                feature.isDisabled = disabled;
                if (disabled) {
                    var setting = this.settings.filter(x => x.name == featureToDisable)[0];
                    if (setting) {
                        setting = feature;
                    }
                    else {
                        this.settings.push(feature);
                    }
                } else {
                    this.settings = this.settings.filter(feature => feature.name !== featureToDisable);
                }
            } else {
                if (feature['navItems']?.length) {
                    this.toggleFeature(feature['navItems'], featureToDisable, disabled);
                } else {
                    this.toggleFeature(feature.steps, featureToDisable, disabled);
                }
            }
        }
    }


    private static toggleNestedFeatures(features, disabled) {
        for (const feature of features) {

            if (feature.isDisabled) {
                if (feature['navItems']?.length) {
                    for (const subNav of feature['navItems']) {
                        subNav.isDisabled = disabled;
                    }
                } else {
                    for (const step of feature.steps) {
                        step.isDisabled = disabled;
                    }
                }
            }

            if (feature['navItems']?.length) {
                this.toggleNestedFeatures(feature['navItems'], disabled);
            } else {
                this.toggleNestedFeatures(feature.steps, disabled);
            }

        }
    }




    static mapDisabledFeatures(features: string[]) {
        for (const feature of features) {
            this.toggleFeature(this.navigation.navItems, feature, true);
        }
    }

    static getDisabledPaths() {
        return this.settings.map(s => s.path);
    }

    static isStepDisabled(path) {
        return this.getDisabledPaths().includes(path);
    }   

    // static getFieldConfig(nameofField: TenantConfigFieldNameEnum) {
    //     let res = Boolean(featureSettings.find((rec) => rec.name == nameofField)?.show);
    //     console.log(nameofField + ": " + res)
    //     return res;
    // }

    // static filterBorrowerByFieldConfiguration(borrower: Borrower) {
    //     let res = true;
    //     if (borrower.ownTypeId == OwnTypeEnum.PrimaryBorrower) {
    //         res = this.getFieldConfig(TenantConfigFieldNameEnum.PrimaryBorrowerSSN) || this.getFieldConfig(TenantConfigFieldNameEnum.PrimaryBorrowerDOB);
    //     }
    //     else if (borrower.ownTypeId == OwnTypeEnum.SecondaryBorrower) {
    //         res = this.getFieldConfig(TenantConfigFieldNameEnum.CoBorrowerSSN) || this.getFieldConfig(TenantConfigFieldNameEnum.CoBorrowerDOB);
    //     }
    //     return res;
    // }

    static getFieldConfig(nameofField: TenantConfigFieldNameEnum): TenantConfigConditionEnum {
        var res = featureSettings.find((rec) => rec.name == nameofField)?.condition;
        return res;
    }

    static isFieldVisible(tenantConfigFieldName: TenantConfigFieldNameEnum) {
        return this.getFieldConfig(tenantConfigFieldName) != TenantConfigConditionEnum.Hidden;
    }

    static isFieldRequired(tenantConfigFieldName: TenantConfigFieldNameEnum, defaultValue: boolean) {
        if (this.getFieldConfig(tenantConfigFieldName) == TenantConfigConditionEnum.Required) {
            return true;
        }
        else if (this.getFieldConfig(tenantConfigFieldName) == TenantConfigConditionEnum.Optional) {
            return false;
        }
        return defaultValue;
    }

    static filterBorrowerByFieldConfiguration(borrower: Borrower) {
        let res = true;
        if (borrower.ownTypeId == OwnTypeEnum.PrimaryBorrower) {
            res = this.getFieldConfig(TenantConfigFieldNameEnum.PrimaryBorrowerSSN) != TenantConfigConditionEnum.Hidden || this.getFieldConfig(TenantConfigFieldNameEnum.PrimaryBorrowerDOB) != TenantConfigConditionEnum.Hidden;
        }
        else if (borrower.ownTypeId == OwnTypeEnum.SecondaryBorrower) {
            res = this.getFieldConfig(TenantConfigFieldNameEnum.CoBorrowerSSN) != TenantConfigConditionEnum.Hidden || this.getFieldConfig(TenantConfigFieldNameEnum.CoBorrowerDOB) != TenantConfigConditionEnum.Hidden;
        }
        return res;
    }

    static updateFeatureSettting = (setting: []) => {
        setting?.forEach(item => {
            const findIndex = featureSettings.findIndex(x => x.dbname == item['name']);
            if (findIndex != -1) {
                featureSettings[findIndex].condition = item['value']
            }
        });
    }

}