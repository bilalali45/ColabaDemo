//
//  Validation.swift
//  Colaba
//
//  Created by Murtaza on 19/05/2021.
//

import Foundation

struct Validation {
    
    func validateEmail(_ email: String?) throws -> String{
        guard let email = email else { throw ValidationError.noEmail }
        guard email.count > 0 else { throw ValidationError.noEmail }
        guard email.isValidEmail() else { throw ValidationError.invalidEmail }
        return email
    }
    
    func validateBorrowerFirstName(_ firstName: String?) throws -> String{
        guard let firstName = firstName else { throw ValidationError.requiredField }
        guard firstName.count > 0 else { throw ValidationError.requiredField }
        return firstName
    }
    
    func validateBorrowerLastName(_ lastName: String?) throws -> String{
        guard let lastName = lastName else { throw ValidationError.requiredField }
        guard lastName.count > 0 else { throw ValidationError.requiredField }
        return lastName
    }
    
    func validateBorrowerEmail(_ email: String?) throws -> String{
        guard let email = email else { throw ValidationError.requiredField }
        guard email.count > 0 else { throw ValidationError.requiredField }
        guard email.isValidEmail() else { throw ValidationError.invalidBorrowerEmail }
        return email
    }
    
    func validateBorrowrHomePhoneNumber(_ phoneNumber: String?) throws -> String {
        guard let phone = phoneNumber else { throw ValidationError.requiredField }
        guard phone.count > 0 else { throw ValidationError.requiredField }
        guard phone.count >= 14 else { throw ValidationError.invalidPhoneNumber }
        return phone
    }
    
//    func validateUsername(_ username: String?) throws -> String {
//        guard let username = username else { throw ValidationError.invalidValue }
//        guard username.count >= 3 else { throw ValidationError.usernameTooShort }
//        guard username.count <= 20 else { throw ValidationError.usernameTooLong }
//        return username
//    }
    
    func validatePassword(_ password: String?) throws -> String {
        guard let password = password else { throw ValidationError.invalidPassword }
        guard password.count > 0 else { throw ValidationError.invalidPassword }
        //guard password.count >= 6 else { throw ValidationError.passwordTooShort }
        //guard password.count <= 20 else { throw ValidationError.passwordTooLong }
        return password
    }
    
    func validatePhoneNumber(_ phoneNumber: String?) throws -> String {
        guard let phone = phoneNumber else { throw ValidationError.invalidPhoneNumber }
        guard phone.count >= 14 else { throw ValidationError.invalidPhoneNumber }
        return phone
    }
    
    func validateSearchHomeAddress(_ homeAddress: String?) throws -> String{
        guard let homeAddress = homeAddress else { throw ValidationError.requiredField }
        guard homeAddress.count > 0 else { throw ValidationError.requiredField }
        return homeAddress
    }
    
    func validateStreetAddressHomeAddress(_ streetAddress: String?) throws -> String{
        guard let streetAddress = streetAddress else { throw ValidationError.requiredField }
        guard streetAddress.count > 0 else { throw ValidationError.requiredField }
        return streetAddress
    }
    
    func validateCity(_ city: String?) throws -> String{
        guard let city = city else { throw ValidationError.requiredField }
        guard city.count > 0 else { throw ValidationError.requiredField }
        return city
    }
    
    func validateState(_ state: String?) throws -> String{
        guard let state = state else { throw ValidationError.requiredField }
        guard state.count > 0 else { throw ValidationError.requiredField }
        return state
    }
    
    func validateZipcode(_ zipCode: String?) throws -> String{
        guard let zipCode = zipCode else { throw ValidationError.requiredField }
        guard zipCode.count > 0 else { throw ValidationError.requiredField }
        return zipCode
    }
    
    func validateCountry(_ country: String?) throws -> String{
        guard let country = country else { throw ValidationError.requiredField }
        guard country.count > 0 else { throw ValidationError.requiredField }
        return country
    }
    
    func validateMoveInDate(_ moveInDate: String?) throws -> String{
        guard let moveInDate = moveInDate else { throw ValidationError.requiredField }
        guard moveInDate.count > 0 else { throw ValidationError.requiredField }
        return moveInDate
    }
    
    func validateHousingStatus(_ housingStatus: String?) throws -> String{
        guard let housingStatus = housingStatus else { throw ValidationError.requiredField }
        guard housingStatus.count > 0 else { throw ValidationError.requiredField }
        return housingStatus
    }
    
    func validateMonthlyRent(_ monthlyRent: String?) throws -> String{
        guard let monthlyRent = monthlyRent else { throw ValidationError.requiredField }
        guard monthlyRent.count > 0 else { throw ValidationError.requiredField }
        return monthlyRent
    }
    
    func validateTypeOfRelationship(_ typeOfRelationShip: String?) throws -> String{
        guard let typeOfRelationShip = typeOfRelationShip else { throw ValidationError.requiredField }
        guard typeOfRelationShip.count > 0 else { throw ValidationError.requiredField }
        return typeOfRelationShip
    }
    
    func validateRelationshipDetail(_ relationshipDetail: String?) throws -> String{
        guard let relationshipDetail = relationshipDetail else { throw ValidationError.requiredField }
        guard relationshipDetail.count > 0 else { throw ValidationError.requiredField }
        return relationshipDetail
    }
    
    func validateVisaStatus(_ visaStatus: String?) throws -> String{
        guard let visaStatus = visaStatus else { throw ValidationError.requiredField }
        guard visaStatus.count > 0 else { throw ValidationError.requiredField }
        return visaStatus
    }
    
    func validateVisaStatusDetail(_ visaStatusDetail: String?) throws -> String{
        guard let visaStatusDetail = visaStatusDetail else { throw ValidationError.requiredField }
        guard visaStatusDetail.count > 0 else { throw ValidationError.requiredField }
        return visaStatusDetail
    }
    
    func validateLastDateOfService(_ lastDateOfService: String?) throws -> String{
        guard let lastDateOfService = lastDateOfService else { throw ValidationError.requiredField }
        guard lastDateOfService.count > 0 else { throw ValidationError.requiredField }
        return lastDateOfService
    }
    
    func validateDependentAge(_ age: String?) throws -> String{
        guard let age = age else { throw ValidationError.requiredField }
        guard age.count > 0 else { throw ValidationError.requiredField }
        return age
    }
    
    func validateLoanStage(_ loanStage: String?) throws -> String{
        guard let loanStage = loanStage else { throw ValidationError.requiredField }
        guard loanStage.count > 0 else { throw ValidationError.requiredField }
        return loanStage
    }
    
    func validatePurchasePrice(_ purchasePrice: String?) throws -> String{
        guard let purchasePrice = purchasePrice else { throw ValidationError.requiredField }
        if let price = Int(purchasePrice.replacingOccurrences(of: ",", with: "")){
            if (price >= 50000 && price <= 100000000){
                return purchasePrice
            }
            throw ValidationError.invalidPurchasePrice
        }
        throw ValidationError.invalidPurchasePrice
    }
    
    func validateLoanAmount(_ loanAmount: String?) throws -> String{
        guard let loanAmount = loanAmount else { throw ValidationError.requiredField }
        guard loanAmount.count > 0 else { throw ValidationError.requiredField }
        return loanAmount
    }
    
    func validateDownPayment(_ downPayment: String?) throws -> String{
        guard let downPayment = downPayment else { throw ValidationError.requiredField }
        guard downPayment.count > 0 else { throw ValidationError.requiredField }
        return downPayment
    }
    
    func validateDownPaymentPercentage(_ downPaymentPercentage: String?) throws -> String{
        guard let downPaymentPercentage = downPaymentPercentage else { throw ValidationError.requiredField }
        guard downPaymentPercentage.count > 0 else { throw ValidationError.requiredField }
        return downPaymentPercentage
    }
    
    func validateClosingDate(_ closingDate: String?) throws -> String{
        guard let closingDate = closingDate else { throw ValidationError.requiredField }
        guard closingDate.count > 0 else { throw ValidationError.requiredField }
        return closingDate
    }
}

enum ValidationError: LocalizedError {
    case invalidPassword
    //case passwordTooLong
    //case passwordTooShort
    //case usernameTooLong
    //case usernameTooShort
    case noEmail
    case invalidEmail
    case invalidPhoneNumber
    case requiredField
    case invalidBorrowerEmail
    case invalidPurchasePrice
    
    var errorDescription: String? {
        switch self {
        case .invalidPassword:
            return "Please enter password."
//        case .passwordTooLong:
//            return "Your password is too long. Maximum limit of password is 20 characters."
//        case .passwordTooShort:
//            return "Your password is too short. Minimum limit of password is 6 characters."
//        case .usernameTooLong:
//            return "Your username is too long. Maximum limit of username is 20 characters."
//        case .usernameTooShort:
//            return "Your username is too short. Minimum limit of username is 3 characters."
        case .noEmail:
            return "Please insert email."
        case .invalidEmail:
            return "Your email is not valid. Please try again."
        case .invalidPhoneNumber:
            return "Your phone number is not valid. Please try again."
        case .requiredField:
            return "This field is required."
        case .invalidBorrowerEmail:
            return "Please enter a valid email address"
        case .invalidPurchasePrice:
            return "Purchase price should be between $50,000 and $100,000,000"
        }
        
        
    }
    
}
