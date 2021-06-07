//
//  ColabaTests.swift
//  ColabaTests
//
//  Created by Murtaza on 18/05/2021.
//

import XCTest
@testable import Colaba

class ColabaTests: XCTestCase {

    var sut: Validation!
    
    override func setUpWithError() throws {
        try super.setUpWithError()
        sut = Validation()
        // Put setup code here. This method is called before the invocation of each test method in the class.
    }

    override func tearDownWithError() throws {
        // Put teardown code here. This method is called after the invocation of each test method in the class.
        sut = nil
        try super.tearDownWithError()
    }

    func testExample() throws {
        // This is an example of a functional test case.
        // Use XCTAssert and related functions to verify your tests produce the correct results.
    }

    func testPerformanceExample() throws {
        // This is an example of a performance test case.
        self.measure {
            // Put the code you want to measure the time of here.
        }
    }
    
    func testEmail(){
        
        do{
            let email = try sut.validateEmail("a")
        }
        catch{
            print(error.localizedDescription)
        }
    }

}
