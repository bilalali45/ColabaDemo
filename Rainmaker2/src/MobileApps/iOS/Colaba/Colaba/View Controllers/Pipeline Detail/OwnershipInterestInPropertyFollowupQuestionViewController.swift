//
//  OwnershipInterestInPropertyFollowupQuestionViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/09/2021.
//

import UIKit
import MaterialComponents

protocol OwnershipInterestInPropertyFollowupQuestionViewControllerDelegate: AnyObject {
    func saveQuestion(propertyTypeQuestion: GovernmentQuestionModel, holdPropertyTypeQuestion: GovernmentQuestionModel)
}

class OwnershipInterestInPropertyFollowupQuestionViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblUsername: UILabel!
    @IBOutlet weak var scrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var txtfieldPropertyType: ColabaTextField!
    @IBOutlet weak var txtfieldHoldTitle: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var questions: [GovernmentQuestionModel]?
    var borrowerName = ""
    var propertyTypeArray = [DropDownModel]()
    var propertyHoldTitleArray = [DropDownModel]()
    weak var delegate: OwnershipInterestInPropertyFollowupQuestionViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTextfields()
        getPropertyTypeDropDown()
        setQuestionData()
        saveQuestions()
    }
    
    //MARK:- Methods and Actions
    
    func setupTextfields(){
        
        txtfieldPropertyType.setTextField(placeholder: "What type of property did you own?", controller: self, validationType: .required)
        txtfieldPropertyType.type = .dropdown
        
        txtfieldHoldTitle.setTextField(placeholder: "How did you hold title to the property?", controller: self, validationType: .required)
        txtfieldHoldTitle.type = .dropdown
        
    }
    
    func setQuestionData(){
        lblUsername.text = borrowerName.uppercased()
        if let allQuestions = questions{
            txtfieldPropertyType.setTextField(text: allQuestions.filter({$0.question.localizedCaseInsensitiveContains("What type of property did you own?")}).first!.answer)
            txtfieldHoldTitle.setTextField(text: allQuestions.filter({$0.question.localizedCaseInsensitiveContains("How did you hold title to the property?")}).first!.answer)
        }
    }
    
    func validate() -> Bool {

        if (!txtfieldPropertyType.validate()) {
            return false
        }
        if (!txtfieldHoldTitle.validate()){
            return false
        }
        return true
    }
    
    func saveQuestions(){
        
        var propertyTypeId = 0
        var propertyHoldTitleId = 0
        
        if let property = propertyTypeArray.filter({$0.optionName.localizedCaseInsensitiveContains(txtfieldPropertyType.text!)}).first{
            propertyTypeId = property.optionId
        }
        
        if let holdTitle = propertyHoldTitleArray.filter({$0.optionName.localizedCaseInsensitiveContains(txtfieldHoldTitle.text!)}).first{
            propertyHoldTitleId = holdTitle.optionId
        }
        
        if let allQuestions = questions{
            
            var question1 = GovernmentQuestionModel()
            var question2 = GovernmentQuestionModel()
            
            if let question = allQuestions.filter({$0.question.localizedCaseInsensitiveContains("What type of property did you own?")}).first{
                
                question.selectionOptionId = propertyTypeId
                question.answer = txtfieldPropertyType.text!
                question1 = question

            }
            if let question = allQuestions.filter({$0.question.localizedCaseInsensitiveContains("How did you hold title to the property?")}).first{
                
                question.selectionOptionId = propertyHoldTitleId
                question.answer = txtfieldHoldTitle.text!
                question2 = question
            }
            
            self.delegate?.saveQuestion(propertyTypeQuestion: question1, holdPropertyTypeQuestion: question2)
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        txtfieldPropertyType.validate()
        txtfieldHoldTitle.validate()
        
        if validate(){
            saveQuestions()
            self.dismissVC()
        }
    }
    
    //MARK:- API's
    
    func getPropertyTypeDropDown(){
        propertyTypeArray.removeAll()
        propertyHoldTitleArray.removeAll()
        Utility.showOrHideLoader(shouldShow: true)
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllOccupancyTypeDropDown, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.propertyTypeArray.append(model)
                    }
                    self.txtfieldPropertyType.setDropDownDataSource(self.propertyTypeArray.map{$0.optionName})
                    self.getPropertyHoldTitleDropDown()
                    
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                }
            }
            
        }
    }
    
    func getPropertyHoldTitleDropDown(){
       
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getAllPropertyHoldTitle, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.propertyHoldTitleArray.append(model)
                    }
                    self.txtfieldHoldTitle.setDropDownDataSource(self.propertyHoldTitleArray.map{$0.optionName})
                    
                }
                else{
                    
                }
            }
            
        }
    }
}
