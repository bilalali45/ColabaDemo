//
//  RefinanceLoanInfoViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 30/08/2021.
//

import UIKit

class RefinanceLoanInfoViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var txtfieldLoanStage: ColabaTextField!
    @IBOutlet weak var txtfieldAdditionalCashoutAmount: ColabaTextField!
    @IBOutlet weak var txtfieldLoanAmount: ColabaTextField!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var loanApplicationId = 0
    var loanStageArray = [LoanGoalModel]()
    var loanInfo = LoanInfoDetailModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
        getLoanDetail()
    }
    
    func setTextFields() {
        ///Loan Stage Text Field
        txtfieldLoanStage.setTextField(placeholder: "Loan Stage", controller: self, validationType: .required)
        txtfieldLoanStage.type = .dropdown
        
        ///Additional Cash Out Text Field
        txtfieldAdditionalCashoutAmount.setTextField(placeholder: "Additional Cash Out Amount", controller: self, validationType: .noValidation)
        txtfieldAdditionalCashoutAmount.type = .amount
        
        ///Loan Amount Text Field
        txtfieldLoanAmount.setTextField(placeholder: "Loan Amount", controller: self, validationType: .required)
        txtfieldLoanAmount.type = .amount
        
    }
    
    func setLoanInfo(){
        if let loanGoalModel = self.loanStageArray.filter({$0.id == self.loanInfo.loanGoalId}).first{
            txtfieldLoanStage.setTextField(text: loanGoalModel.loanGoal)
        }
        txtfieldAdditionalCashoutAmount.setTextField(text: String(format: "%.0f", self.loanInfo.cashOutAmount.rounded()))
        txtfieldLoanAmount.setTextField(text: String(format: "%.0f", self.loanInfo.loanPayment.rounded()))
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton) {
        if validate() {
            if (txtfieldLoanStage.text != "" && txtfieldLoanAmount.text != ""){
                self.goBack()
            }
        }
    }
    
    func validate() -> Bool {
        var isValidate = txtfieldLoanStage.validate()
        isValidate = txtfieldLoanAmount.validate() && isValidate
        return isValidate
    }
    
    //MARK:- API's
    
    func getLoanDetail(){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getLoanInfoDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                if (status == .success){
                    let loanInfoDetailModel = LoanInfoDetailModel()
                    loanInfoDetailModel.updateModelWithJSON(json: result["data"])
                    self.loanInfo = loanInfoDetailModel
                    self.getLoanGoals()
                }
                else{
                    Utility.showOrHideLoader(shouldShow: false)
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
    }
    
    func getLoanGoals(){
        
        let extraData = "loanpurposeid=\(self.loanInfo.loanPurposeId)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getLoanGoals, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    let loanGoalsArray = result.arrayValue
                    for loanGoal in loanGoalsArray{
                        let model = LoanGoalModel()
                        model.updateModelWithJSON(json: loanGoal)
                        self.loanStageArray.append(model)
                    }
                    self.txtfieldLoanStage.setDropDownDataSource(self.loanStageArray.map{$0.loanGoal})
                    self.setLoanInfo()
                }
                else{
                    
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
            
        }
    }
}
