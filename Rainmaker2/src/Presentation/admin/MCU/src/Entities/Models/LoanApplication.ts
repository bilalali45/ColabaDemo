import { toTitleCase, FormatAmountByCountry } from 'rainsoft-js'
import { DateFormat } from '../../Utils/helpers/DateFormat';

export class LoanApplication {
  public loanNumber?: number;
  public loanPurpose?: string;
  public propertyType?: string;
  public propertyAddress?: string;
  public loanAmount?: number;
  public countryName?: string;
  public countyName?: string;
  public cityName?: string;
  public stateName?: string;
  public streetAddress?: string;
  public zipCode?: string;
  public unitNumber?: any;
  public expectedClosingDate?: string;
  public popertyValue?: number;
  public status?: string;
  public rate?: number;
  public loanProgram?: string;
  public lockStatus?: string;
  public lockDate?: string;
  public expirationDate?: string;
  public borrowers?: string[]  = [];
  public borrowersName?: string;

  constructor(
    borrowers?: string[],
    loanNumber?: number,
    loanPurpose?: string,
    propertyType?: string,
    propertyAddress?: string,
    loanAmount?: number,
    country?: string,
    county?: string,
    city?: string,
    state?: string,
    street?: string,
    zipCode?: string,
    unitNumber?: any,
    expectedClosingDate?: string,
    propertyValue?: number,
    status?: string,
    rate?: number,
    loanProgram?: string,
    lockStatus?: string,
    lockDate?: string,
    expirationDate?: string,
    
  ) {
    this.loanPurpose = loanPurpose;
    this.propertyType = propertyType;
    this.propertyAddress = propertyAddress;
    this.loanAmount = loanAmount;
    this.countryName = country;
    this.countyName = county;
    this.cityName = city;
    this.stateName = state;
    this.streetAddress = street;
    this.zipCode = zipCode;
    this.unitNumber = unitNumber;
    this.loanNumber = loanNumber;
    this.expectedClosingDate = expectedClosingDate;
    this.popertyValue = propertyValue;
    this.status = status;
    this.rate = rate;
    this.loanProgram = loanProgram;
    this.lockStatus = lockStatus;
    this.lockDate = lockDate;
    this.expirationDate = expirationDate;
    this.borrowers = borrowers;
  }

  get getLoanAmount() {
    if (!this.loanAmount) {
      return undefined;
    }
    return FormatAmountByCountry(this.loanAmount)
  }

  get getPropertyValue(){
    if(!this.popertyValue){
      return undefined;
    }
    return FormatAmountByCountry(this.popertyValue);
  }

  public fromJson(json: LoanApplication) {
    this.loanPurpose = json.loanPurpose;
    this.propertyType = json.propertyType;
    this.propertyAddress = toTitleCase(json.propertyAddress);
    this.loanAmount = json.loanAmount;
    this.countryName = toTitleCase(json.countryName);
    this.countyName = toTitleCase(json.countyName);
    this.cityName = toTitleCase(json.cityName);
    this.stateName = json.stateName ? json.stateName.toUpperCase() : "";
    this.streetAddress = toTitleCase(json.streetAddress);
    this.zipCode = json.zipCode ? json.zipCode : "";
    this.unitNumber = json.unitNumber;
    this.expectedClosingDate = json.expectedClosingDate ? DateFormat(json.expectedClosingDate, true) : '';
    this.status = toTitleCase(json.status);
    this.rate = json.rate;
    this.loanProgram = json.loanProgram;
    this.loanNumber = json.loanNumber;
    this.lockStatus = json.lockStatus;
    this.lockDate = json.lockDate ? DateFormat(json.lockDate, true) : '';
    this.expirationDate = json.expirationDate ? DateFormat(json.expirationDate, true) : '';
    this.popertyValue = json.popertyValue;
    this.borrowersName = LoanApplication.GetBorrowerName(json.borrowers)
    return this;
  }

  static GetBorrowerName(nameArray?: string[]){
   let names = '';
   if(nameArray){
    for(let i = 0; i < nameArray.length; i++){
      names = names+toTitleCase(nameArray[i])
      if(i != nameArray.length -1) names += ', '
    }
   }
   
   return names;
  }

}
