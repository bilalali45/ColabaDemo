//
//  BorrowerInformationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/08/2021.
//

import UIKit
import Material

class BorrowerInformationViewController: UIViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblBorrowerType: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var borrowerInfoView: UIView!
    @IBOutlet weak var txtfieldLegalFirstName: TextField!
    @IBOutlet weak var txtfieldMiddleName: TextField!
    @IBOutlet weak var txtfieldLegalLastName: TextField!
    @IBOutlet weak var txtfieldSuffix: TextField!
    @IBOutlet weak var txtfieldEmail: TextField!
    @IBOutlet weak var txtfieldHomeNumber: TextField!
    @IBOutlet weak var txtfieldWorkNumber: TextField!
    @IBOutlet weak var txtfieldExtensionNumber: TextField!
    @IBOutlet weak var txtfieldCellNumber: TextField!
    @IBOutlet weak var tblViewAddress: UITableView!
    @IBOutlet weak var tblViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var addAddressView: UIView!
    @IBOutlet weak var maritalStatusView: UIView!
    @IBOutlet weak var maritalStatusViewHeightConstraint: NSLayoutConstraint! //290 or 140
    @IBOutlet weak var unMarriedStackView: UIStackView!
    @IBOutlet weak var btnUnMarried: UIButton!
    @IBOutlet weak var lblUnMarried: UILabel!
    @IBOutlet weak var marriedStackView: UIStackView!
    @IBOutlet weak var btnMarried: UIButton!
    @IBOutlet weak var lblMarried: UILabel!
    @IBOutlet weak var separatedStackView: UIStackView!
    @IBOutlet weak var btnSeparated: UIButton!
    @IBOutlet weak var lblSeparated: UILabel!
    @IBOutlet weak var unmarriedMainView: UIView!
    @IBOutlet weak var lblUnmarriedQuestion: UILabel!
    @IBOutlet weak var lblUnmarriedAns: UILabel!
    @IBOutlet weak var citizenshipView: UIView!
    @IBOutlet weak var citizenshipViewHeightConstraint: NSLayoutConstraint!//242 or 140
    @IBOutlet weak var usCitizenStackView: UIStackView!
    @IBOutlet weak var btnUSCitizen: UIButton!
    @IBOutlet weak var lblUSCitizen: UILabel!
    @IBOutlet weak var permanentResidentStackView: UIStackView!
    @IBOutlet weak var btnPermanentResident: UIButton!
    @IBOutlet weak var lblPermanentResident: UILabel!
    @IBOutlet weak var nonPermanentStackView: UIStackView!
    @IBOutlet weak var btnNonPermanent: UIButton!
    @IBOutlet weak var lblNonPermanent: UILabel!
    @IBOutlet weak var nonPermanentResidentMainView: UIView!
    @IBOutlet weak var lblNonPermanentQuestion: UILabel!
    @IBOutlet weak var lblNonPermanentAns: UILabel!
    @IBOutlet weak var txtfieldDOB: TextField!
    @IBOutlet weak var btnCalendar: UIButton!
    @IBOutlet weak var txtfieldSecurityNo: TextField!
    @IBOutlet weak var btnEye: UIButton!
    @IBOutlet weak var txtfieldNoOfDependent: TextField!
    
    
    var maritalStatus = 1 //1 for unmarried, 2 for married and 3 for separated
    var citizenshipStatus = 1 // 1 for US Citizen, 2 for Permanent and 3 for Non Permanent
    var isShowSecurityNo = false
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setMaterialTextFieldsAndViews(textfields: [txtfieldLegalFirstName, txtfieldMiddleName, txtfieldLegalLastName, txtfieldSuffix, txtfieldEmail, txtfieldHomeNumber, txtfieldWorkNumber, txtfieldExtensionNumber, txtfieldCellNumber, txtfieldDOB, txtfieldSecurityNo, txtfieldNoOfDependent])
        tblViewAddress.register(UINib(nibName: "BorrowerAddressInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressInfoTableViewCell")
        
        unMarriedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(unmarriedTapped)))
        marriedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(marriedTapped)))
        separatedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(separatedTapped)))
        
        usCitizenStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(usCitizenTapped)))
        permanentResidentStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(permanentResidentTapped)))
        nonPermanentStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonPermanentResidentTapped)))
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        setScreenHeight()
    }
    
    //MARK:- Methods and Actions
    
    func setScreenHeight(){
        let addressTableViewHeight = tblViewAddress.contentSize.height
        let maritalStatusViewHeight = maritalStatusView.frame.height
        let citizenshipViewHeight = citizenshipView.frame.height
        let totalHeight = addressTableViewHeight + maritalStatusViewHeight + citizenshipViewHeight + 1100
        tblViewHeightConstraint.constant = addressTableViewHeight
        self.mainViewHeightConstraint.constant = totalHeight
        
        UIView.animate(withDuration: 0.5) {
            self.view.layoutIfNeeded()
        }
    }
    
    func setMaterialTextFieldsAndViews(textfields: [TextField]){
        for textfield in textfields{
            textfield.dividerActiveColor = Theme.getButtonBlueColor()
            textfield.dividerColor = Theme.getSeparatorNormalColor()
            textfield.placeholderActiveColor = Theme.getAppGreyColor()
            textfield.delegate = self
            textfield.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
            textfield.detailLabel.font = Theme.getRubikRegularFont(size: 12)
            textfield.detailColor = .red
        }
        
        addAddressView.layer.cornerRadius = 6
        addAddressView.layer.borderWidth = 1
        addAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addAddressViewTapped)))
        
        unmarriedMainView.layer.cornerRadius = 6
        unmarriedMainView.layer.borderWidth = 1
        unmarriedMainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        unmarriedMainView.dropShadowToCollectionViewCell()
        unmarriedMainView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(unmarriedMainViewTapped)))
        
        nonPermanentResidentMainView.layer.cornerRadius = 6
        nonPermanentResidentMainView.layer.borderWidth = 1
        nonPermanentResidentMainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        nonPermanentResidentMainView.dropShadowToCollectionViewCell()
        nonPermanentResidentMainView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonPermanentResidentMainViewTapped)))
    }
    
    func setPlaceholderLabelColorAfterTextFilled(selectedTextField: UITextField, allTextFields: [TextField]){
        for allTextField in allTextFields{
            if (allTextField == selectedTextField){
                if (allTextField.text == ""){
                    allTextField.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                }
                else{
                    allTextField.placeholderLabel.textColor = Theme.getAppGreyColor()
                }
            }
        }
    }
    
    @objc func addAddressViewTapped(){
        let vc = Utility.getAddResidenceVC()
        self.presentVC(vc: vc)
    }
    
    @objc func unmarriedTapped(){
        maritalStatus = 1
        changeMaritalStatus()
    }
    
    @objc func marriedTapped(){
        maritalStatus = 2
        changeMaritalStatus()
    }
    
    @objc func separatedTapped(){
        maritalStatus = 3
        changeMaritalStatus()
    }
    
    func changeMaritalStatus(){
        if (maritalStatus == 1){
            btnUnMarried.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblUnMarried.font = Theme.getRubikMediumFont(size: 14)
            btnMarried.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblMarried.font = Theme.getRubikRegularFont(size: 14)
            btnSeparated.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblSeparated.font = Theme.getRubikRegularFont(size: 14)
            unmarriedMainView.isHidden = false
            maritalStatusViewHeightConstraint.constant = 290
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
        }
        else if (maritalStatus == 2){
            btnMarried.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblMarried.font = Theme.getRubikMediumFont(size: 14)
            btnUnMarried.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblUnMarried.font = Theme.getRubikRegularFont(size: 14)
            btnSeparated.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblSeparated.font = Theme.getRubikRegularFont(size: 14)
            unmarriedMainView.isHidden = true
            maritalStatusViewHeightConstraint.constant = 140
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
        }
        else if (maritalStatus == 3){
            btnSeparated.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblSeparated.font = Theme.getRubikMediumFont(size: 14)
            btnUnMarried.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblUnMarried.font = Theme.getRubikRegularFont(size: 14)
            btnMarried.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblMarried.font = Theme.getRubikRegularFont(size: 14)
            unmarriedMainView.isHidden = true
            maritalStatusViewHeightConstraint.constant = 140
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
        }
    }
   
    @objc func unmarriedMainViewTapped(){
        
    }
    
    @objc func usCitizenTapped(){
        citizenshipStatus = 1
        changeCitizenshipStatus()
    }
    
    @objc func permanentResidentTapped(){
        citizenshipStatus = 2
        changeCitizenshipStatus()
    }
    
    @objc func nonPermanentResidentTapped(){
        citizenshipStatus = 3
        changeCitizenshipStatus()
    }
    
    func changeCitizenshipStatus(){
        if (citizenshipStatus == 1){
            btnUSCitizen.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblUSCitizen.font = Theme.getRubikMediumFont(size: 14)
            btnPermanentResident.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblPermanentResident.font = Theme.getRubikRegularFont(size: 14)
            btnNonPermanent.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblNonPermanent.font = Theme.getRubikRegularFont(size: 14)
            nonPermanentResidentMainView.isHidden = true
            citizenshipViewHeightConstraint.constant = 140
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
        }
        else if (citizenshipStatus == 2){
            btnPermanentResident.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblPermanentResident.font = Theme.getRubikMediumFont(size: 14)
            btnUSCitizen.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblUSCitizen.font = Theme.getRubikRegularFont(size: 14)
            btnNonPermanent.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblNonPermanent.font = Theme.getRubikRegularFont(size: 14)
            nonPermanentResidentMainView.isHidden = true
            citizenshipViewHeightConstraint.constant = 140
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
        }
        else if (citizenshipStatus == 3){
            btnNonPermanent.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblNonPermanent.font = Theme.getRubikMediumFont(size: 14)
            btnUSCitizen.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblUSCitizen.font = Theme.getRubikRegularFont(size: 14)
            btnPermanentResident.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
            lblPermanentResident.font = Theme.getRubikRegularFont(size: 14)
            nonPermanentResidentMainView.isHidden = false
            citizenshipViewHeightConstraint.constant = 242
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
        }
    }
    
    @objc func nonPermanentResidentMainViewTapped(){
        
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @IBAction func btnUnMarriedTapped(_ sender: UIButton) {
        unmarriedTapped()
    }
    
    @IBAction func btnMarriedTapped(_ sender: UIButton) {
        marriedTapped()
    }
    
    @IBAction func btnSeparatedTapped(_ sender: UIButton) {
        separatedTapped()
    }
    
    @IBAction func btnUSCitizenTapped(_ sender: UIButton) {
        usCitizenTapped()
    }
    
    @IBAction func btnPermanentResidentTapped(_ sender: UIButton) {
        permanentResidentTapped()
    }
    
    @IBAction func btnNonPermanentResidentTapped(_ sender: UIButton) {
        nonPermanentResidentTapped()
    }
    
    @IBAction func btnCalendarTapped(_ sender: UIButton){
        
    }
    
    @IBAction func btnEyeTapped(_ sender: UIButton){
        isShowSecurityNo = !isShowSecurityNo
        txtfieldSecurityNo.isSecureTextEntry = !isShowSecurityNo
        btnEye.setImage(UIImage(named: isShowSecurityNo ? "hide" : "eyeIcon"), for: .normal)
    }
}

extension BorrowerInformationViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 2
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerAddressInfoTableViewCell", for: indexPath) as! BorrowerAddressInfoTableViewCell
        cell.addressIcon.isHidden = indexPath.row != 0
        cell.lblHeading.isHidden = indexPath.row != 0
        cell.lblRent.isHidden = indexPath.row != 0
        cell.lblAddressTopConstraint.constant = indexPath.row != 0 ? 15 : 51
        cell.lblAddress.text = indexPath.row != 0 ? "5919 Trussville Crossings Pkwy,\nBirmingham AL 35235" : "5919 Trussville Crossings Parkways, ZV Street, Birmingham AL 35235"
        cell.lblDate.text = indexPath.row != 0 ? "From Aug 2019 to Nov 2020" : "From Dec 2020"
        cell.mainView.layer.cornerRadius = 6
        cell.mainView.layer.borderWidth = 1
        cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        cell.mainView.dropShadowToCollectionViewCell()
        cell.mainView.updateConstraintsIfNeeded()
        cell.mainView.layoutSubviews()
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let vc = Utility.getAddResidenceVC()
        self.presentVC(vc: vc)
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return UITableView.automaticDimension
    }
    
    func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        return true
    }
    
    func tableView(_ tableView: UITableView, trailingSwipeActionsConfigurationForRowAt indexPath: IndexPath) -> UISwipeActionsConfiguration? {
        
        let deleteAction = UIContextualAction(style: .normal, title: "") { action, actionView, bool in
            
        }
        deleteAction.backgroundColor = Theme.getDashboardBackgroundColor()
        deleteAction.image = UIImage(named: indexPath.row == 0 ? "AddressDeleteIconBig" : "AddressDeleteIconSmall")
        return UISwipeActionsConfiguration(actions: [deleteAction])
        
    }
}

extension BorrowerInformationViewController: UITextFieldDelegate{
    func textFieldDidEndEditing(_ textField: UITextField) {
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldLegalFirstName, txtfieldMiddleName, txtfieldLegalLastName, txtfieldSuffix, txtfieldEmail, txtfieldHomeNumber, txtfieldWorkNumber, txtfieldExtensionNumber, txtfieldCellNumber, txtfieldDOB, txtfieldSecurityNo, txtfieldNoOfDependent])
    }
}
