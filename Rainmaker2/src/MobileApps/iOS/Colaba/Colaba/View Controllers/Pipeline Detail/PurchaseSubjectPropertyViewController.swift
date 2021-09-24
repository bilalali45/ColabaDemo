//
//  PurchaseSubjectPropertyViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/09/2021.
//

import UIKit

class PurchaseSubjectPropertyViewController: BaseViewController {
    
    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblNavTitle: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
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
    
    var isTBDProperty = true
    var isMixedUseProperty: Bool?
    var isOccupying: Bool?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setTextFields()
        setViews()
    }
    
    //MARK:- Methods and Actions
    func setTextFields() {
        ///Property Type Text Field
        txtfieldPropertyType.setTextField(placeholder: "Property Type")
        txtfieldPropertyType.setDelegates(controller: self)
        txtfieldPropertyType.setValidation(validationType: .noValidation)
        txtfieldPropertyType.type = .dropdown
        txtfieldPropertyType.setDropDownDataSource(kPropertyTypeArray)
        
        ///Occupancy Type Text Field
        txtfieldOccupancyType.setTextField(placeholder: "Occupancy Type")
        txtfieldOccupancyType.setDelegates(controller: self)
        txtfieldOccupancyType.setValidation(validationType: .noValidation)
        txtfieldOccupancyType.type = .dropdown
        txtfieldOccupancyType.setDropDownDataSource(kOccupancyTypeArray)
        
        ///Appraised Property Value Text Field
        txtfieldAppraisedPropertyValue.setTextField(placeholder: "Appraised Property Value")
        txtfieldAppraisedPropertyValue.setDelegates(controller: self)
        txtfieldAppraisedPropertyValue.setTextField(keyboardType: .numberPad)
        txtfieldAppraisedPropertyValue.setValidation(validationType: .noValidation)
        txtfieldAppraisedPropertyValue.type = .amount
        
        ///Annual Property Taxes Text Field
        txtfieldTax.setTextField(placeholder: "Annual Property Taxes")
        txtfieldTax.setDelegates(controller: self)
        txtfieldTax.setTextField(keyboardType: .numberPad)
        txtfieldTax.setValidation(validationType: .noValidation)
        txtfieldTax.type = .amount
        
        ///Annual Homeowner's Insurance Text Field
        txtfieldHomeOwnerInsurance.setTextField(placeholder: "Annual Homeowner's Insurance")
        txtfieldHomeOwnerInsurance.setDelegates(controller: self)
        txtfieldHomeOwnerInsurance.setTextField(keyboardType: .numberPad)
        txtfieldHomeOwnerInsurance.setValidation(validationType: .noValidation)
        txtfieldHomeOwnerInsurance.type = .amount
        
        ///Annual Flood Insurance Text Field
        txtfieldFloodInsurance.setTextField(placeholder: "Annual Flood Insurance")
        txtfieldFloodInsurance.setDelegates(controller: self)
        txtfieldFloodInsurance.setTextField(keyboardType: .numberPad)
        txtfieldFloodInsurance.setValidation(validationType: .noValidation)
        txtfieldFloodInsurance.type = .amount
    }
    
    func setViews(){
        
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
        
        btnYes.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblYes.font = Theme.getRubikRegularFont(size: 14)
        propertyDetailView.isHidden = true
        propertyViewHeightConstraint.constant = 203
        setScreenHeight()
        
        btnOccupying.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblOccupying.font = Theme.getRubikRegularFont(size: 14)
    }
    
    func setScreenHeight(){
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
        setScreenHeight()
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
            setScreenHeight()
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
            lblOccupying.font = occupying ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 14)
            btnNonOccupying.setImage(UIImage(named: occupying ? "RadioButtonUnselected" : "RadioButtonSelected"), for: .normal)
            lblNonOccupying.font = occupying ?  Theme.getRubikRegularFont(size: 15) : Theme.getRubikMediumFont(size: 14)
        }
        
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        self.goBack()
    }
}
