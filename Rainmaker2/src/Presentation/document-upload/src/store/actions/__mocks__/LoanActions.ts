export const statusText = {
    COMPLETED: "COMPLETED",
    CURRENT: "CURRENT STEP",
    UPCOMMING: "UPCOMING",
};

const seedLoanProgress = [

    {
        description: "Tell us about yourself and your financial situation so we can find loan options for you.",
        id: "5ee863e7305e33a11c51eb55",
        isCurrentStep: false,
        name: "Fill out application",
        order: 1,
        status: 'COMPLETED'
    },

    {
        description: "Double-check the information you have entered and make any edits before you submit your application.",
        id: "5ee86445305e33a11c51eb88",
        isCurrentStep: false,
        name: "Review and submit application",
        order: 2,
        status: 'COMPLETED'

    },
    {
        description: "Congratulations! You 're one step closer to completing your loan.",
        id: "5ee86488305e33a11c51eb9d",
        isCurrentStep: true,
        name: "Application received",
        order: 3,
        status: 'CURRENT STEP'
    }
]

const seedLoanStatus = {

    cityName: "Houston",
    countryName: null,
    countyName: null,
    loanAmount: 288000,
    loanPurpose: "Purchase a home",
    name: "Shehroz Riyaz",
    propertyType: "Single Family Detached",
    stateName: "Texas",
    streetAddress: "Street 2",
    unitNumber: "52",
    url: "http://qa.RainsoftFn.com",
    zipCode: "77098"
}

const seedLoanOfficer = {
    firstName: 'John',
    lastName: 'Doe',
    phone: '123456789',
    email: 'john@doe.com',
    webUrl: 'http://www.johndoe.com',
    nmls: '8290348',
    photo: 'nophoto'
}


export class LaonActions {
    static async getLoanOfficer(loanApplicationId: string) {
        return Promise.resolve(seedLoanOfficer)
    }

    static async getLoanApplication(loanApplicationId: string) {
        return Promise.resolve(seedLoanStatus)
    }

    static async getLOPhoto(lOPhotoId: string = "", loanApplicationId: string) {

    }

    static async getFooter(loanApplicationId: string) {
        return Promise.resolve('Copyright 2002 – 2020. All rights reserved. American Heritage Capital, LP. NMLS 277676')
    }

    static async getLoanProgressStatus(loanApplicationId: string) {
        return Promise.resolve(seedLoanProgress)
    }
}

const attachStatus = (data: any) => {
    let current = 0;

    data.forEach((l: any, i: number) => {
        if (l.isCurrentStep) {
            current = i;
        }
    });

    return data.map((l: any, i: number) => {
        if (i < current) {
            l.status = statusText.COMPLETED;
        }

        if (i === current) {
            l.status = statusText.CURRENT;
        }

        if (i > current) {
            l.status = statusText.UPCOMMING;
        }
        return l;
    });
};
