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
        guard email.count > 1 else { throw ValidationError.noEmail }
        guard email.isValidEmail() else { throw ValidationError.invalidEmail }
        return email
    }
    
    func validateUsername(_ username: String?) throws -> String {
        guard let username = username else { throw ValidationError.invalidValue }
        guard username.count >= 3 else { throw ValidationError.usernameTooShort }
        guard username.count <= 20 else { throw ValidationError.usernameTooLong }
        return username
    }
    
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
}

enum ValidationError: LocalizedError {
    case invalidValue
    case invalidPassword
    case passwordTooLong
    case passwordTooShort
    case usernameTooLong
    case usernameTooShort
    case noEmail
    case invalidEmail
    case invalidPhoneNumber
    
    var errorDescription: String? {
        switch self {
        case .invalidValue:
            return "You have entered an invalid value."
        case .invalidPassword:
            return "Please enter password."
        case .passwordTooLong:
            return "Your password is too long. Maximum limit of password is 20 characters."
        case .passwordTooShort:
            return "Your password is too short. Minimum limit of password is 6 characters."
        case .usernameTooLong:
            return "Your username is too long. Maximum limit of username is 20 characters."
        case .usernameTooShort:
            return "Your username is too short. Minimum limit of username is 3 characters."
        case .noEmail:
            return "Please insert email."
        case .invalidEmail:
            return "Your email is not valid. Please try again."
        case .invalidPhoneNumber:
            return "Your phone number is not valid. Please try again."
        }
        
    }
    
}
