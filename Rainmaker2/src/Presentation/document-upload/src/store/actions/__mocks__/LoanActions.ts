
export class LaonActions {
    static async getLoanOfficer(loanApplicationId: string) {

    }

    static async getLoanApplication(loanApplicationId: string) {
        return Promise.resolve({

            loanPurpose: 'No Loan Purpose',
            propertyType: 'No Property Type',
            propertyAddress: 'No Property Address',
            loanAmount: 45645,
            country: '',
            county: 'No County',
            city: 'Dallas',
            state: 'TX',
            street: 'Abc Street 124',
            zipCode: 45682,
            unitNumber: 654654654658
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
