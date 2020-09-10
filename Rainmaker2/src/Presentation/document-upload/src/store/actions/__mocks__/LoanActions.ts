
export class LaonActions {
    static async getLoanOfficer(loanApplicationId: string) {

    }

    static async getLoanApplication(loanApplicationId: string) {
        return Promise.resolve({

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
        })
    }

    static async getLOPhoto(lOPhotoId: string = "", loanApplicationId: string) {

    }

    static async getFooter(loanApplicationId: string) {

    }

    static async getLoanProgressStatus(loanApplicationId: string) {

    }
}

const attachStatus = (data: any) => {
    //   let current = 0;

    //   data.forEach((l: any, i: number) => {
    //     if (l.isCurrentStep) {
    //       current = i;
    //     }
    //   });

    //   return data.map((l: any, i: number) => {
    //     if (i < current) {
    //       l.status = statusText.COMPLETED;
    //     }

    //     if (i === current) {
    //       l.status = statusText.CURRENT;
    //     }

    //     if (i > current) {
    //       l.status = statusText.UPCOMMING;
    //     }
    //     return l;
    //   });
};
