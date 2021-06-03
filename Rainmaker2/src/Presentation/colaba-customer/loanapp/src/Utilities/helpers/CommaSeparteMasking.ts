export function CommaFormatted(amount:String | number | null | undefined):string {
    if (amount) {
        return amount.toString().replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1,");
    }
    if(amount === 0) return amount.toString();

    return "";
}

export function removeCommaFormatting(amount:String | number): string {
    if (amount) {
        if (amount !== "0.00") {
            amount = amount.toString().replace(/\,/g, "");
            return amount.toString().replace('.00', "");
        }
        else {
            return ""
        }

    }
    return ""
}