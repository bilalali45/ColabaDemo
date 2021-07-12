//
//  ValidationUnitTest.swift
//  ColabaTests
//
//  Created by Muhammad Murtaza on 09/07/2021.
//

import XCTest
@testable import Colaba

class ValidationUnitTest: XCTestCase {
    
    let validation = Validation()
    
    func testEmailIsEmpty(){
        
        do{
            let email = try validation.validateEmail("")
        }
        catch{
            XCTAssertEqual(error.localizedDescription, "Please insert email.")
        }
        
    }
    
    func testEmailIsInvalid(){
        do{
            let email = try validation.validateEmail("Murtaza")
        }
        catch{
            XCTAssertEqual(error.localizedDescription, "Your email is not valid. Please try again.")
        }
    }
    
    func testEmailIsValid(){
        do{
            
            XCTAssertNoThrow(try validation.validateEmail("Murtaza@gmail.com"))
        }
        catch{
            
        }
    }
    
    func testPasswordIsEmpty(){
        do{
            let password = try validation.validatePassword("")
            
        }
        catch{
            XCTAssertEqual(error.localizedDescription, "Please enter password.")
        }
    }

    func testPasswordIsValid(){
        do{
            let password = try validation.validatePassword("test123")
            XCTAssertEqual(password, password, "Password is valid")
        }
        catch{
            
        }
    }
    
    func testPhoneNumberIsNotValid(){
        
        do{
            let phoneNumber = try validation.validatePhoneNumber("458698")
        }
        catch{
            XCTAssertEqual(error.localizedDescription, "Your phone number is not valid. Please try again.")
        }
        
    }
    
    func testPhoneNumberIsValid(){
        
        do{
            let phoneNumber = try validation.validatePhoneNumber("(2944) 268-2855")
            XCTAssertEqual(phoneNumber, phoneNumber, "Phone number is valid")
        }
        catch{
            
        }
        
    }
    
}
