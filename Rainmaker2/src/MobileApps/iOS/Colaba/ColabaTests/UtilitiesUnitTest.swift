//
//  UtilitiesUnitTest.swift
//  ColabaTests
//
//  Created by Muhammad Murtaza on 09/07/2021.
//

import XCTest
@testable import Colaba

class UtilitiesUnitTest: XCTestCase {

    func testGreetingMessage(){
        XCTAssertEqual(Utility.getGreetingMessage(), Utility.getGreetingMessage())
    }
    
    func testTokenExpiryReturnsFalse(){
        XCTAssertFalse(Utility.getIsTokenExpire(tokenValidityDate: "2021-07-15T22:49:40Z"))
    }
    
    func testTokenExpiryReturnsTrue(){
        XCTAssertTrue(Utility.getIsTokenExpire(tokenValidityDate: "2021-07-08T22:49:40Z"))
    }
    
    func testTokenExpiryWithEmptyDateReturnsTrue(){
        XCTAssertTrue(Utility.getIsTokenExpire(tokenValidityDate: ""))
    }
}
