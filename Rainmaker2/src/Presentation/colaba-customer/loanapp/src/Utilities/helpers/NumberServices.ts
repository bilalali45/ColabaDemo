import { CommaFormatted, removeCommaFormatting } from "./CommaSeparteMasking";

export class NumberServices {

    static formatPhoneNumber(phoneNumberString: string | number): string {
        if (!phoneNumberString) {
            return "";
        }

        let cleaned = ('' + phoneNumberString).replace(/\D/g, '');
        let match = cleaned.match(/^(1|)?(\d{3})(\d{3})(\d{4})$/);
        if (match) {
            let intlCode = (match[1] ? '+1 ' : '');
            return [intlCode, '(', match[2], ') ', match[3], '-', match[4]].join('');
        }
        return "";
    }

    static curruncyFormatter(price: number, region = 'en-US') {
        if (!price) {
            return "";
        }

        return price.toLocaleString(region);
    };

    static curruncyFormatterIncomeHome(price: number) {
        if (!price) {
            return "";
        }

        return parseFloat(price.toString()).toLocaleString().replace(/(\d)(?=(\d{3})+(?!\d))/g, '$1,');
    };

    

    static addProceedingZeros (value) {
        let netAnlIncome = Number(removeCommaFormatting(value)).toFixed(2);
        console.log("NumberServices.formateNumber(String(netAnlIncome)) ",netAnlIncome)
        return netAnlIncome;
    }
    static formateNumber (value) {
        if(value){
            if(isNaN(value)  && !value.includes(",")  )
                return value;
            else
                return CommaFormatted(removeCommaFormatting(value));
        } else
        return "";
    }

    static removeCommas(amount:String | number): number {
        if (amount) {
            if (amount !== "0.00") {
                amount = amount.toString().replace(/\,/g, "");
                return Number(amount.toString().replace('.00', ""));
            }
            else {
                return 0
            }
    
        }
        return 0
    }
}

