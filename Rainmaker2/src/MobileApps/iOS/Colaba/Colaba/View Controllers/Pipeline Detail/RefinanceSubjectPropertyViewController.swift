//
//  RefinanceSubjectPropertyViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/09/2021.
//

import UIKit

class RefinanceSubjectPropertyViewController: BaseViewController {
    
    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    @IBOutlet weak var subjectPropertyTBDView: UIView!
    @IBOutlet weak var lblSubjectPropertyTBD: UILabel!
    @IBOutlet weak var btnSubjectPropertyTBD: UIButton!
    @IBOutlet weak var subjectPropertyAddressView: UIView!
    @IBOutlet weak var subjectPropertyAddressViewHeightConstraint: NSLayoutConstraint! //50 or 110
    @IBOutlet weak var lblSubjectPropertyAddress: UILabel!
    @IBOutlet weak var btnSubjectPropertyAddress: UIButton!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var txtfieldPropertyType: ColabaTextField!
    @IBOutlet weak var txtfieldOccupancyType: ColabaTextField!
    @IBOutlet weak var txtfieldRentalIncome: ColabaTextField!
    @IBOutlet weak var txtfieldRentalIncomeTopConstraint: NSLayoutConstraint! //30 or 0
    @IBOutlet weak var txtfieldRentalIncomeHeightConstraint: NSLayoutConstraint! // 39 or 0
    @IBOutlet weak var propertyView: UIView!
    @IBOutlet weak var propertyViewHeightConstraint: NSLayoutConstraint! //203 or 347
    @IBOutlet weak var lblUsePropertyQuestion: UILabel!
    @IBOutlet weak var yesStackView: UIStackView!
    @IBOutlet weak var btnYes: UIButton!
    @IBOutlet weak var lblYes: UILabel!
    @IBOutlet weak var noStackView: UIStackView!
    @IBOutlet weak var btnNo: UIButton!
    @IBOutlet weak var lblNo: UILabel!
    @IBOutlet weak var propertyDetailView: UIView!
    @IBOutlet weak var lblPropertyUseDetail: UILabel!
    @IBOutlet weak var txtfieldAppraisedPropertyValue: ColabaTextField!
    @IBOutlet weak var txtfieldHomePurchaseDate: ColabaTextField!
    @IBOutlet weak var txtfieldHomeOwnerAssociationDues: ColabaTextField!
    @IBOutlet weak var txtfieldTax: ColabaTextField!
    @IBOutlet weak var txtfieldHomeOwnerInsurance: ColabaTextField!
    @IBOutlet weak var txtfieldFloodInsurance: ColabaTextField!
    @IBOutlet weak var occupancyStatusView: UIView!
    @IBOutlet weak var lblCoBorrowerName: UILabel!
    @IBOutlet weak var occupyingStackView: UIStackView!
    @IBOutlet weak var btnOccupying: UIButton!
    @IBOutlet weak var lblOccupying: UILabel!
    @IBOutlet weak var nonOccupyingStackView: UIStackView!
    @IBOutlet weak var btnNonOccupying: UIButton!
    @IBOutlet weak var lblNonOccupying: UILabel!
    @IBOutlet weak var firstMortgageMainView: UIView!
    @IBOutlet weak var firstMortgageMainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var firstMortgageYesStackView: UIStackView!
    @IBOutlet weak var btnFirstMortgageYes: UIButton!
    @IBOutlet weak var lblFirstMortgageYes: UILabel!
    @IBOutlet weak var firstMortgageNoStackView: UIStackView!
    @IBOutlet weak var btnFirstMortgageNo: UIButton!
    @IBOutlet weak var lblFirstMortgageNo: UILabel!
    @IBOutlet weak var firstMortgageView: UIView!
    @IBOutlet weak var lblFirstMortgagePayment: UILabel!
    @IBOutlet weak var lblFirstMortgageBalance: UILabel!
    @IBOutlet weak var secondMortgageMainView: UIView!
    @IBOutlet weak var secondMortgageMainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var secondMortgageYesStackView: UIStackView!
    @IBOutlet weak var btnSecondMortgageYes: UIButton!
    @IBOutlet weak var lblSecondMortgageYes: UILabel!
    @IBOutlet weak var secondMortgageNoStackView: UIStackView!
    @IBOutlet weak var btnSecondMortgageNo: UIButton!
    @IBOutlet weak var lblSecondMortgageNo: UILabel!
    @IBOutlet weak var secondMortgageView: UIView!
    @IBOutlet weak var lblSecondMortgagePayment: UILabel!
    @IBOutlet weak var lblSecondMortgageBalance: UILabel!
    
    var isTBDProperty = true
    var isMixedUseProperty: Bool?
    var isOccupying: Bool?
    var isFirstMortgage = false
    var isSecondMortgage = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setViews()
        setTextFields()
    }
    
    //MARK:- Methods and Actions
    func setViews() {
        
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        propertyDetailView.isHidden = true
        propertyViewHeightConstraint.constant = 203
        btnOccupying.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblOccupying.font = Theme.getRubikRegularFont(size: 14)
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
        
        subjectPropertyTBDView.layer.cornerRadius = 8
        subjectPropertyTBDView.layer.borderWidth = 1
        subjectPropertyTBDView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        subjectPropertyTBDView.dropShadowToCollectionViewCell()
        subjectPropertyTBDView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(tbdViewTapped)))
        
        subjectPropertyAddressView.layer.cornerRadius = 8
        subjectPropertyAddressView.layer.borderWidth = 1
        subjectPropertyAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        subjectPropertyAddressView.dropShadowToCollectionViewCell()
        subjectPropertyAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addressViewTapped)))
        
        propertyDetailView.layer.cornerRadius = 6
        propertyDetailView.layer.borderWidth = 1
        propertyDetailView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        propertyDetailView.dropShadowToCollectionViewCell()
        propertyDetailView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(propertyDetailViewTapped)))
        yesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(yesStackViewTapped)))
        noStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(noStackViewTapped)))
        
        occupyingStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(occupyingStackViewTapped)))
        nonOccupyingStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonOccupyingStackViewTapped)))
        
        firstMortgageYesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(firstMortgageYesStackViewTapped)))
        firstMortgageNoStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(firstMortgageNoStackViewTapped)))
        firstMortgageView.layer.cornerRadius = 6
        firstMortgageView.layer.borderWidth = 1
        firstMortgageView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        firstMortgageView.dropShadowToCollectionViewCell()
        firstMortgageView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(firstMortgageViewTapped)))
        
        secondMortgageYesStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(secondMortgageYesStackViewTapped)))
        secondMortgageNoStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(secondMortgageNoStackViewTapped)))
        secondMortgageView.layer.cornerRadius = 6
        secondMortgageView.layer.borderWidth = 1
        secondMortgageView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        secondMortgageView.dropShadowToCollectionViewCell()
        secondMortgageView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(secondMortgageViewTapped)))
        
    }
    
    func setTextFields(){

        txtfieldPropertyType.setTextField(placeholder: "Property Type" , controller: self, validationType: .noValidation)
        txtfieldPropertyType.type = .dropdown
        txtfieldPropertyType.setDropDownDataSource(kPropertyTypeArray)
        
        ///Occupancy Type Text Field
        txtfieldOccupancyType.setTextField(placeholder: "Occupancy Type" , controller: self, validationType: .noValidation)
        txtfieldOccupancyType.type = .dropdown
        txtfieldOccupancyType.setDropDownDataSource(kOccupancyTypeArray)
        
        ///Rental Income Text Field
        txtfieldRentalIncome.setTextField(placeholder: "Rental Income" , controller: self, validationType: .noValidation)
        txtfieldRentalIncome.type = .amount
        
        ///Property Value Text Field
        txtfieldAppraisedPropertyValue.setTextField(placeholder: "Property Value" , controller: self, validationType: .noValidation)
        txtfieldAppraisedPropertyValue.type = .amount
        
        ///Property Value Text Field
        txtfieldHomePurchaseDate.setTextField(placeholder: "Date of Home Purchase (MM/YYYY)", controller: self, validationType: .noValidation)
        txtfieldHomePurchaseDate.type = .monthlyDatePicker
        
        ///Annual Homeowner's Associations Due Text Field
        txtfieldHomeOwnerAssociationDues.setTextField(placeholder: "Annual Homeowner's Associations Due", controller: self, validationType: .noValidation)
        txtfieldHomeOwnerAssociationDues.type = .amount
        
        ///Annual Property Taxes Text Field
        txtfieldTax.setTextField(placeholder: "Annual Property Taxes", controller: self, validationType: .noValidation)
        txtfieldTax.type = .amount
        
        ///Annual Homeowner's Insurance Text Field
        txtfieldHomeOwnerInsurance.setTextField(placeholder: "Annual Homeowner's Insurance", controller: self, validationType: .noValidation)
        txtfieldHomeOwnerInsurance.type = .amount
        
        ///Annual Flood Insurance Text Field
        txtfieldFloodInsurance.setTextField(placeholder: "Annual Flood Insurance", controller: self, validationType: .noValidation)
        txtfieldFloodInsurance.type = .amount
        
    }
    
    func setScreenHeight(){
        let firstMortgageViewHeight = self.firstMortgageMainView.frame.height
        let secondMortgageViewHeight = self.secondMortgageMainView.frame.height
        
        self.mainViewHeightConstraint.constant = firstMortgageViewHeight + secondMortgageViewHeight + 1600
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
        }
    }
    
    @objc func tbdViewTapped(){
        isTBDProperty = true
        changedSubjectPropertyType()
    }
    
    @objc func addressViewTapped(){
        let vc = Utility.getSubjectPropertyAddressVC()
        self.presentVC(vc: vc)
        isTBDProperty = false
        changedSubjectPropertyType()
    }
    
    @objc func changedSubjectPropertyType(){
        btnSubjectPropertyTBD.setImage(UIImage(named: isTBDProperty ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
        lblSubjectPropertyTBD.font = isTBDProperty ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
        btnSubjectPropertyAddress.setImage(UIImage(named: isTBDProperty ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
        lblSubjectPropertyAddress.font = isTBDProperty ?  Theme.getRubikRegularFont(size: 15) : Theme.getRubikMediumFont(size: 15)
        subjectPropertyAddressViewHeightConstraint.constant = isTBDProperty ? 50 : 110
        lblAddress.isHidden = isTBDProperty
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @objc func showHideRentalIncome(){
        if (txtfieldOccupancyType.text == "Investment Property"){
            txtfieldRentalIncomeTopConstraint.constant = 30
            txtfieldRentalIncomeHeightConstraint.constant = 39
            txtfieldRentalIncome.isHidden = false
            txtfieldRentalIncome.resignFirstResponder()
        }
        else if ( (txtfieldOccupancyType.text == "Primary Residence") && (txtfieldPropertyType.text == "Duplex (2 Unit)" || txtfieldPropertyType.text == "Triplex (3 Unit)" || txtfieldPropertyType.text == "Quadplex (4 Unit)") ){
            txtfieldRentalIncomeTopConstraint.constant = 30
            txtfieldRentalIncomeHeightConstraint.constant = 39
            txtfieldRentalIncome.isHidden = false
            txtfieldRentalIncome.resignFirstResponder()
        }
        else{
            txtfieldRentalIncomeTopConstraint.constant = 0
            txtfieldRentalIncomeHeightConstraint.constant = 0
            txtfieldRentalIncome.isHidden = true
            txtfieldRentalIncome.resignFirstResponder()
            txtfieldRentalIncome.text = ""
            txtfieldRentalIncome.textInsetsPreset = .none
            txtfieldRentalIncome.placeholderHorizontalOffset = 0
        }
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @objc func yesStackViewTapped(){
        let vc = Utility.getMixPropertyDetailFollowUpVC()
        self.presentVC(vc: vc)
        isMixedUseProperty = true
        changeMixedUseProperty()
    }
    
    @objc func noStackViewTapped(){
        isMixedUseProperty = false
        changeMixedUseProperty()
    }
    
    @objc func changeMixedUseProperty(){
        if let mixUseProperty = isMixedUseProperty{
            btnYes.setImage(UIImage(named: mixUseProperty ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblYes.font = mixUseProperty ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnNo.setImage(UIImage(named: mixUseProperty ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
            lblNo.font = mixUseProperty ?  Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
            propertyDetailView.isHidden = !mixUseProperty
            propertyViewHeightConstraint.constant = mixUseProperty ? 347 : 203
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
        }
        
    }
    
    @objc func propertyDetailViewTapped(){
        let vc = Utility.getMixPropertyDetailFollowUpVC()
        self.presentVC(vc: vc)
    }
    
    @objc func occupyingStackViewTapped(){
        isOccupying = true
        changeOccupyingStatus()
    }
    
    @objc func nonOccupyingStackViewTapped(){
        isOccupying = false
        changeOccupyingStatus()
    }
    
    @objc func changeOccupyingStatus(){
        if let occupying = isOccupying{
            btnOccupying.setImage(UIImage(named: occupying ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblOccupying.font = occupying ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
            btnNonOccupying.setImage(UIImage(named: occupying ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
            lblNonOccupying.font = occupying ?  Theme.getRubikRegularFont(size: 14) : Theme.getRubikMediumFont(size: 14)
        }
    }
    
    @objc func firstMortgageYesStackViewTapped(){
        let vc = Utility.getFirstMortgageFollowupQuestionsVC()
        self.presentVC(vc: vc)
        isFirstMortgage = true
        changeMortgageStatus()
    }
    
    @objc func firstMortgageNoStackViewTapped(){
        isFirstMortgage = false
        isSecondMortgage = false
        changeMortgageStatus()
    }
    
    @objc func firstMortgageViewTapped(){
        let vc = Utility.getFirstMortgageFollowupQuestionsVC()
        self.presentVC(vc: vc)
    }
    
    @objc func secondMortgageYesStackViewTapped(){
        let vc = Utility.getSecondMortgageFollowupQuestionsVC()
        self.presentVC(vc: vc)
        isSecondMortgage = true
        changeMortgageStatus()
    }
    
    @objc func secondMortgageNoStackViewTapped(){
        isSecondMortgage = false
        changeMortgageStatus()
    }
    
    @objc func secondMortgageViewTapped(){
        let vc = Utility.getSecondMortgageFollowupQuestionsVC()
        self.presentVC(vc: vc)
    }
    
    func changeMortgageStatus(){
        if (!isFirstMortgage && !isSecondMortgage){
            
            btnFirstMortgageYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblFirstMortgageYes.font = Theme.getRubikRegularFont(size: 15)
            btnFirstMortgageNo.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblFirstMortgageNo.font = Theme.getRubikMediumFont(size: 15)
            firstMortgageMainViewHeightConstraint.constant = 145
            firstMortgageView.isHidden = true
            secondMortgageMainViewHeightConstraint.constant = 0
            secondMortgageMainView.isHidden = true
            secondMortgageView.isHidden = true
        }
        else if (isFirstMortgage){
            
            btnFirstMortgageYes.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblFirstMortgageYes.font = Theme.getRubikMediumFont(size: 15)
            btnFirstMortgageNo.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblFirstMortgageNo.font = Theme.getRubikRegularFont(size: 15)
            firstMortgageMainViewHeightConstraint.constant = 350
            firstMortgageView.isHidden = false
            secondMortgageMainView.isHidden = false
            secondMortgageView.isHidden = true
            btnSecondMortgageYes.setImage(UIImage(named: isSecondMortgage ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblSecondMortgageYes.font = isSecondMortgage ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
            btnSecondMortgageNo.setImage(UIImage(named: !isSecondMortgage ? "RadioButtonSelected" : "RadioButtonUnselected"), for: .normal)
            lblSecondMortgageNo.font = !isSecondMortgage ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
            secondMortgageMainViewHeightConstraint.constant = isSecondMortgage ? 350 : 145
            secondMortgageView.isHidden = !isSecondMortgage
        }

        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        self.goBack()
    }
}

extension RefinanceSubjectPropertyViewController: ColabaTextFieldDelegate {
    func selectedOption(option: String, atIndex: Int, textField: ColabaTextField) {
        if textField == txtfieldPropertyType || textField == txtfieldOccupancyType {
            showHideRentalIncome()
        }
    }
}
