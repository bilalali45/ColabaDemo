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

const mockFaviCon = 'iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAMAAAAoLQ9TAAAABGdBTUEAALGPC/xhBQAAAAFzUkdCAK7OHOkAAAAgY0hSTQAAeiYAAICEAAD6AAAAgOgAAHUwAADqYAAAOpgAABdwnLpRPAAAAJNQTFRFAAAABB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8tBB8t////PlVkgAAAAC90Uk5TAAAEChAhNlBujKyWmKuLbTUgD8fX4efzxsrWkiInJiWJg3V2b/qVmftMWQtcSAJK/YAMAAAAAWJLR0Qwrtwt5AAAAAlwSFlzAAALEwAACxMBAJqcGAAAAG5JREFUGNNjYAACRkYmZmYmRkYQm4GFlY2dg5OLm4eXj59dQFCIgUdYRFRMXF9CUl9cTFRKWJpBRlZOXkYBJKDIKi8nKwjWxqgEEuCAmAEWUAYJqAyAgDCagKqaugaygKYWs7aOLpKAHiMQ6IEFAIvRDbKNsRMsAAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDE5LTA2LTI2VDA4OjM1OjIxKzAwOjAwD/md3gAAACV0RVh0ZGF0ZTptb2RpZnkAMjAxOS0wNi0yNlQwODozNToyMSswMDowMH6kJWIAAABGdEVYdHNvZnR3YXJlAEltYWdlTWFnaWNrIDYuNy44LTkgMjAxNC0wNS0xMiBRMTYgaHR0cDovL3d3dy5pbWFnZW1hZ2ljay5vcmfchu0AAAAAGHRFWHRUaHVtYjo6RG9jdW1lbnQ6OlBhZ2VzADGn/7svAAAAGHRFWHRUaHVtYjo6SW1hZ2U6OmhlaWdodAAxOTIPAHKFAAAAF3RFWHRUaHVtYjo6SW1hZ2U6OldpZHRoADE5MtOsIQgAAAAZdEVYdFRodW1iOjpNaW1ldHlwZQBpbWFnZS9wbmc/slZOAAAAF3RFWHRUaHVtYjo6TVRpbWUAMTU2MTUzODEyMc2wujoAAAAPdEVYdFRodW1iOjpTaXplADBCQpSiPuwAAABWdEVYdFRodW1iOjpVUkkAZmlsZTovLy9tbnRsb2cvZmF2aWNvbnMvMjAxOS0wNi0yNi9kMWRjYjg3OWVkMTc3MGIyMzE0YmUxZDI1NmZiZGRmNy5pY28ucG5nikfOCQAAAABJRU5ErkJggg==';
const mockBanner = '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';



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
        return Promise.resolve(seedLoanProgress);
    }
    
    static async getCompanyLogoSrc(loanApplicationId: string) {
        return Promise.resolve(mockBanner);
        
    }
    
    static async getCompanyFavIconSrc(loanApplicationId: string) {
        return Promise.resolve(mockFaviCon);
        
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
