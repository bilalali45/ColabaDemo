export type LoanInfoCalculated = {
    downPayment: string;
    purchasePrice: string;
    percentValue: string;
};

export class LoanCalculations {

    

    static calculatedownPaymentandPercent(purchasePrice: number, downPaymentPercent: number ) {
        let loaninfoCalculated: LoanInfoCalculated | null = null;
        if (purchasePrice <= 0) {
            return loaninfoCalculated;
        }
        if (downPaymentPercent <= 0) {
            downPaymentPercent = 20;
        }

        let downPaymentPrice = (purchasePrice * downPaymentPercent) / 100;


        let percentValue = (100 * downPaymentPrice) / purchasePrice;
        if (percentValue > 97) {
            percentValue = 97;
            downPaymentPrice = (purchasePrice * percentValue) / 100;
        }

        loaninfoCalculated = {
            downPayment: downPaymentPrice.toFixed(2),
            percentValue: percentValue.toFixed(2),
            purchasePrice: purchasePrice.toFixed(0)

        }
        return loaninfoCalculated;
    }

    static calculatedownPaymentPercent(purchasePrice: number, downPaymentPrice: number ) {
        let loaninfoCalculated: LoanInfoCalculated | null= null;
        if (purchasePrice <= 0 || downPaymentPrice <= 0)
            return loaninfoCalculated;

        let downPaymentPercent = (100 * downPaymentPrice) / purchasePrice;
        if (downPaymentPercent > 97) {
            downPaymentPercent = 97;
            downPaymentPrice = (purchasePrice * downPaymentPercent) / 100;
        }
        
        loaninfoCalculated = {
            downPayment: downPaymentPrice.toFixed(0),
            percentValue: downPaymentPercent.toFixed(2),
            purchasePrice: purchasePrice.toFixed(0)

        }
        return loaninfoCalculated;
    }

    static calculatedownPayment(purchasePrice: number, downPaymentPercent: number ) {
        let loaninfoCalculated: LoanInfoCalculated | null= null;
        if (purchasePrice <= 0 && downPaymentPercent <= 0) {
            return loaninfoCalculated;
        }

        let downPaymentPrice = (purchasePrice * downPaymentPercent) / 100;

        loaninfoCalculated = {
            downPayment: downPaymentPrice.toFixed(0),
            percentValue: downPaymentPercent.toFixed(2),
            purchasePrice: purchasePrice.toFixed(0)

        }
        return loaninfoCalculated;
    }

    static calculateLoanAmount(downPaymentPercent: number, purchasePrice: number ) {
        
        let downPaymentPrice = (purchasePrice * downPaymentPercent) / 100;
        const loanAmount = purchasePrice - downPaymentPrice;
        return loanAmount.toFixed(0);
    }

    
}