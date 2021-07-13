//
//  APIRouterUnitTest.swift
//  ColabaTests
//
//  Created by Muhammad Murtaza on 09/07/2021.
//

import XCTest
import SwiftyJSON
@testable import Colaba

class APIRouterUnitTest: XCTestCase {
    
    let api = APIRouter.sharedInstance
    
    func testIsLoginAPIGivesFailure(){
        
        api.executeAPI(type: .login, method: .post, params: ["Email":"Murtaza", "Password": "test123"]) { status, result, message in
            XCTAssertFalse(status == .success)
        }
        
    }
    
    func testIsLoginAPIGivesSuccess(){
        
        api.executeAPI(type: .login, method: .post, params: ["Email":"mobileuser1@mailinator.com", "Password": "test123"]) { status, result, message in
            XCTAssertTrue(status == .success)
        }
        
    }

    func testPipeLineResponse(){
        
        let extraData = "dateTime=\(Utility.getDate())&pageNumber=1&pageSize=20&loanFilter=0&orderBy=1&assignedToMe=false"
        
        api.executeDashboardAPIs(type: .getPipelineList, method: .get, params: nil, extraData: extraData) { status, result, message in
            XCTAssertTrue(status == .success)
        }
        
    }
    
    
}
