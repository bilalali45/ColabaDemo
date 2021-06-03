import { BorrowerAddressProto } from "../../Entities/Models/types";
import { GetIncomeSectionReviewEmploymentAddressProto } from "../../components/Home/MyMoney/Income/IncomeReview/IncomeReview";

export class StringServices {
    static removeText(text: string, from: string, to: string = '') {
        return text ? text.replace(from, to).trim() : '';
    }

    static capitalizeFirstLetter(text: string) {
        return text ? text.charAt(0).toUpperCase() + text.slice(1) : '';
    }
    static addressGenerator(borrowerAddress: BorrowerAddressProto | GetIncomeSectionReviewEmploymentAddressProto, addCountry: boolean = false): string {
        let address: string = '';
        address += (borrowerAddress?.streetAddress) ? `${borrowerAddress?.streetAddress} ` : '';
        address += (borrowerAddress?.unitNo) ? `Apt. ${borrowerAddress?.unitNo}, ` : '';
        address += (borrowerAddress?.cityName) ? `${borrowerAddress?.cityName}, ` : '';
        address += (borrowerAddress?.stateName && borrowerAddress?.stateName !== "None") ? `${borrowerAddress?.stateName} ` : '';
        address += (borrowerAddress?.zipCode) ? `${borrowerAddress?.zipCode}` : '';
        address += (borrowerAddress?.countryName && addCountry) ? `, ${borrowerAddress?.countryName} ` : '';
        return address
    }

    static componseFullName(firstName: string, lastName: string) {
        let name: string = '';
        name += (firstName) ? `${firstName} ` : '';
        name += (lastName) ? `${lastName}` : '';
        return name;
    }


    static generateAddress(streetAddress: string, unitNo: string, cityName: string, stateName: string, zipCode: string, countryName: string): string {
        let address: string = '';
        address += (streetAddress) ? `${streetAddress} ` : '';
        address += (unitNo) ? `Apt. ${unitNo}, ` : '';
        address += (cityName) ? `${cityName}, ` : '';
        address += (stateName) ? `${stateName} ` : '';
        address += (zipCode) ? `${zipCode} ` : '';
        address += (countryName) ? `${countryName} ` : '';
        return address
    }

}
