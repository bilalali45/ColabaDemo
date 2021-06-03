import { useEffect } from "react";
import { unMaskPhoneNumber } from "../../../../../../../Utilities/helpers/PhoneMasking";
import { SelfEmploymentIncomeFields } from "./SelfEmploymentIncomeForm";


export const useSelfEmploymentIncomeErrors = (register) => {
    const { Name, Phone, StartDate, Title } = SelfEmploymentIncomeFields;
    useEffect(() => {

        register(Name.Key, {
            validate: (value) => {
                if (!value) {
                    return `${Name.Label} is required.`
                }
                return true;
            },
        });
        register(Phone.Key, {
            validate: (value) => {
                if (!value) {
                    return `${Phone.Label} is required.`
                } else if (unMaskPhoneNumber(value).length < 10) {
                    return `${value} is not a valid phone number.`
                } else if (!/^[0-9]{1,10}$/g.test(unMaskPhoneNumber(value))) {
                    return `${value} is not a valid phone number.`
                }
                return true;
            },
        });
        register(StartDate.Key, {
            validate: (value) => {
                if (!value) {
                    return `${StartDate.Label} is required.`
                }
                return true;
            },
        });
        register(Title.Key, {
            validate: (value) => {
                if (!value) {
                    return `${Title.Label} is required.`
                }
                return true;
            },
        });

    }, [register]);

}