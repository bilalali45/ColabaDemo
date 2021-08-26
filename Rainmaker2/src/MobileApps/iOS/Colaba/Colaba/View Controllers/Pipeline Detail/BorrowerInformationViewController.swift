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
    @IBOutlet weak var lblBorrowerName: UILabel!
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
    @IBOutlet weak var lblAddAddress: UILabel!
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
//    @IBOutlet weak var txtfieldNoOfDependent: TextField!
    @IBOutlet weak var lblNoOfDependent: UILabel!
    @IBOutlet weak var dependentsCollectionView: UICollectionView!
    @IBOutlet weak var dependentsCollectionViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var stackViewAddDependent: UIStackView!
    @IBOutlet weak var militaryView: UIView!
    @IBOutlet weak var militaryViewHeightConstraint: NSLayoutConstraint!// 470 or 340 or 370 or 250
    @IBOutlet weak var activeDutyStackView: UIStackView!
    @IBOutlet weak var btnActiveDuty: UIButton!
    @IBOutlet weak var lblActiveDuty: UILabel!
    @IBOutlet weak var reserveNationalGuardStackView: UIStackView!
    @IBOutlet weak var btnReserveNationalGuard: UIButton!
    @IBOutlet weak var lblReserveNationalGuard: UILabel!
    @IBOutlet weak var reserveNationalGuardStackViewTopConstraint: NSLayoutConstraint!// 17 or 134
    @IBOutlet weak var veteranStackView: UIStackView!
    @IBOutlet weak var btnVeteran: UIButton!
    @IBOutlet weak var lblVeteran: UILabel!
    @IBOutlet weak var veteranStackViewTopConstraint: NSLayoutConstraint!// 154 or 17
    @IBOutlet weak var survivingSpouseStackView: UIStackView!
    @IBOutlet weak var btnSurvivingSpouse: UIButton!
    @IBOutlet weak var lblSurvingSpouse: UILabel!
    @IBOutlet weak var lastDateOfServiceMainView: UIView!
    @IBOutlet weak var lblLastDateQuestion: UILabel!
    @IBOutlet weak var lblLastDate: UILabel!
    @IBOutlet weak var lastDateOfServiceTopConstraint: NSLayoutConstraint!// 138 or 20
    @IBOutlet weak var reserveOrNationalGuardMainView: UIView!
    @IBOutlet weak var lblReserveNationalGuardQuestion: UILabel!
    @IBOutlet weak var lblReserveNationalGuardAns: UILabel!
    @IBOutlet weak var btnSaveChanges: UIButton!
    
    var totalAddresses = 2
    var maritalStatus = 1 //1 for unmarried, 2 for married and 3 for separated
    var citizenshipStatus = 1 // 1 for US Citizen, 2 for Permanent and 3 for Non Permanent
    var isShowSecurityNo = false
    var noOfDependents = 0
    var isActiveDutyPersonal = true
    var isReserveOrNationalCard = true
    var isVeteran = false
    var isSurvivingSpouse = false
    private let validation: Validation
    let dobDateFormatter = DateFormatter()
    
    init(validation: Validation) {
        self.validation = validation
        super.init(nibName: nil, bundle: nil)
    }
    
    required init?(coder: NSCoder) {
        self.validation = Validation()
        super.init(coder: coder)
    }
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setMaterialTextFieldsAndViews(textfields: [txtfieldLegalFirstName, txtfieldMiddleName, txtfieldLegalLastName, txtfieldSuffix, txtfieldEmail, txtfieldHomeNumber, txtfieldWorkNumber, txtfieldExtensionNumber, txtfieldCellNumber, txtfieldDOB, txtfieldSecurityNo /*txtfieldNoOfDependent*/])
        tblViewAddress.register(UINib(nibName: "BorrowerAddressInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressInfoTableViewCell")
        dependentsCollectionView.register(UINib(nibName: "DependentCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "DependentCollectionViewCell")
        
        unMarriedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(unmarriedTapped)))
        marriedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(marriedTapped)))
        separatedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(separatedTapped)))
        
        usCitizenStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(usCitizenTapped)))
        permanentResidentStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(permanentResidentTapped)))
        nonPermanentStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonPermanentResidentTapped)))
        
        stackViewAddDependent.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addDependentTapped)))
        
        activeDutyStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(activeDutyTapped)))
        reserveNationalGuardStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(reserveNationalGuardTapped)))
        veteranStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(veteranTapped)))
        survivingSpouseStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(survivingSpouseTapped)))
        
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        setDependentCollectionViewLayout()
        setScreenHeight()
    }
    
    //MARK:- Methods and Actions
    
    func setScreenHeight(){
        let addressTableViewHeight = tblViewAddress.contentSize.height
        let maritalStatusViewHeight = maritalStatusView.frame.height
        let citizenshipViewHeight = citizenshipView.frame.height
        let dependentsCollectionViewHeight = dependentsCollectionView.contentSize.height
        let militaryViewHeight = militaryView.frame.height
        let totalHeight = addressTableViewHeight + maritalStatusViewHeight + citizenshipViewHeight + dependentsCollectionViewHeight + militaryViewHeight + 1100
        tblViewHeightConstraint.constant = addressTableViewHeight
        dependentsCollectionViewHeightConstraint.constant = dependentsCollectionViewHeight
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
            textfield.detailVerticalOffset = 4
            textfield.placeholderVerticalOffset = 8
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
        
        dobDateFormatter.dateStyle = .medium
        dobDateFormatter.dateFormat = "dd/MM/yyyy"
        txtfieldDOB.addInputViewDatePicker(target: self, selector: #selector(dateChanged), maximumDate: Date())
        
        lastDateOfServiceMainView.layer.cornerRadius = 6
        lastDateOfServiceMainView.layer.borderWidth = 1
        lastDateOfServiceMainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        lastDateOfServiceMainView.dropShadowToCollectionViewCell()
        lastDateOfServiceMainView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(lastDateOfServiceMainViewTapped)))
        
        reserveOrNationalGuardMainView.layer.cornerRadius = 6
        reserveOrNationalGuardMainView.layer.borderWidth = 1
        reserveOrNationalGuardMainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        reserveOrNationalGuardMainView.dropShadowToCollectionViewCell()
        reserveOrNationalGuardMainView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(reserveOrNationalGuardMainViewTapped)))
        
        btnSaveChanges.layer.borderWidth = 1
        btnSaveChanges.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        btnSaveChanges.roundButtonWithShadow(shadowColor: UIColor.white.withAlphaComponent(0.20).cgColor)
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
    
    func setDependentCollectionViewLayout(){
        let layout = UICollectionViewFlowLayout()
        layout.scrollDirection = .vertical
        layout.sectionInset = UIEdgeInsets(top: 22, left: 20, bottom: 0, right: 20)
        layout.minimumLineSpacing = 0
        layout.minimumInteritemSpacing = 0
        let itemWidth = UIScreen.main.bounds.width
        layout.itemSize = CGSize(width: itemWidth, height: 69)
        self.dependentsCollectionView.collectionViewLayout = layout
    }
    
    @objc func addAddressViewTapped(){
        
        if (totalAddresses == 0){
            let vc = Utility.getAddResidenceVC()
            let navVC = UINavigationController(rootViewController: vc)
            navVC.modalPresentationStyle = .fullScreen
            navVC.navigationBar.isHidden = true
            self.presentVC(vc: navVC)
        }
        else{
            let vc = Utility.getAddPreviousResidenceVC()
            let navVC = UINavigationController(rootViewController: vc)
            navVC.modalPresentationStyle = .fullScreen
            navVC.navigationBar.isHidden = true
            self.presentVC(vc: navVC)
        }
        
    }
    
    @objc func unmarriedTapped(){
        maritalStatus = 1
        changeMaritalStatus()
        let vc = Utility.getUnmarriedFollowUpQuestionsVC()
        self.presentVC(vc: vc)
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
        }
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
   
    @objc func unmarriedMainViewTapped(){
        let vc = Utility.getUnmarriedFollowUpQuestionsVC()
        self.presentVC(vc: vc)
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
        let vc = Utility.getNonPermanentResidenceFollowUpQuestionsVC()
        self.presentVC(vc: vc)
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
        }
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
    
    @objc func nonPermanentResidentMainViewTapped(){
        let vc = Utility.getNonPermanentResidenceFollowUpQuestionsVC()
        self.presentVC(vc: vc)
    }
    
    @objc func dateChanged() {
        if let  datePicker = self.txtfieldDOB.inputView as? UIDatePicker {
            self.txtfieldDOB.text = dobDateFormatter.string(from: datePicker.date)
        }
    }
    
    @objc func addDependentTapped(){
        if (noOfDependents < 99){
            noOfDependents = noOfDependents + 1
            lblNoOfDependent.text = "\(noOfDependents)"
            self.dependentsCollectionView.reloadData()
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
                self.setScreenHeight()
            }
        }
    }
    
    @objc func activeDutyTapped(){
        isActiveDutyPersonal = !isActiveDutyPersonal
        btnActiveDuty.setImage(UIImage(named: isActiveDutyPersonal ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblActiveDuty.font = isActiveDutyPersonal ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        changeMilitaryStatus()
        if (isActiveDutyPersonal){
            let vc = Utility.getActiveDutyPersonnelFollowUpQuestionVC()
            self.presentVC(vc: vc)
        }
    }
    
    @objc func lastDateOfServiceMainViewTapped(){
        let vc = Utility.getActiveDutyPersonnelFollowUpQuestionVC()
        self.presentVC(vc: vc)
    }
    
    @objc func reserveNationalGuardTapped(){
        isReserveOrNationalCard = !isReserveOrNationalCard
        btnReserveNationalGuard.setImage(UIImage(named: isReserveOrNationalCard ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblReserveNationalGuard.font = isReserveOrNationalCard ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        changeMilitaryStatus()
        if (isReserveOrNationalCard){
            let vc = Utility.getReserveFollowUpQuestionsVC()
            self.presentVC(vc: vc)
        }
    }
    
    @objc func reserveOrNationalGuardMainViewTapped(){
        let vc = Utility.getReserveFollowUpQuestionsVC()
        self.presentVC(vc: vc)
    }
    
    @objc func veteranTapped(){
        isVeteran = !isVeteran
        btnVeteran.setImage(UIImage(named: isVeteran ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblVeteran.font = isVeteran ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        changeMilitaryStatus()
    }
    
    @objc func survivingSpouseTapped(){
        isSurvivingSpouse = !isSurvivingSpouse
        btnSurvivingSpouse.setImage(UIImage(named: isSurvivingSpouse ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblSurvingSpouse.font = isSurvivingSpouse ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        changeMilitaryStatus()
    }
    
    func changeMilitaryStatus(){
        if (isActiveDutyPersonal && isReserveOrNationalCard){ //Both are selected
            lastDateOfServiceMainView.isHidden = false
            reserveOrNationalGuardMainView.isHidden = false
            reserveNationalGuardStackViewTopConstraint.constant = 134
            veteranStackViewTopConstraint.constant = 154
            militaryViewHeightConstraint.constant = 470
        }
        else if (isActiveDutyPersonal && !isReserveOrNationalCard){ // Active duty is selected but reserve national is not selected
            lastDateOfServiceMainView.isHidden = false
            reserveOrNationalGuardMainView.isHidden = true
            reserveNationalGuardStackViewTopConstraint.constant = 134
            veteranStackViewTopConstraint.constant = 17
            militaryViewHeightConstraint.constant = 340
        }
        else if (isReserveOrNationalCard && !isActiveDutyPersonal){ // Reserve is selected but active duty is not selected
            lastDateOfServiceMainView.isHidden = true
            reserveOrNationalGuardMainView.isHidden = false
            reserveNationalGuardStackViewTopConstraint.constant = 17
            veteranStackViewTopConstraint.constant = 154
            militaryViewHeightConstraint.constant = 370
        }
        else{ // No Active duty and no reserver personal
            lastDateOfServiceMainView.isHidden = true
            reserveOrNationalGuardMainView.isHidden = true
            reserveNationalGuardStackViewTopConstraint.constant = 17
            veteranStackViewTopConstraint.constant = 17
            militaryViewHeightConstraint.constant = 250
        }
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
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
    
    @IBAction func btnActiveDutyTapped(_ sender: UIButton) {
        activeDutyTapped()
    }
    
    @IBAction func btnReserveNationalGuardTapped(_ sender: UIButton) {
        reserveNationalGuardTapped()
    }
    
    @IBAction func btnVeteranTapped(_ sender: UIButton) {
        veteranTapped()
    }
    
    @IBAction func btnSurvivingSpouseTapped(_ sender: UIButton) {
        survivingSpouseTapped()
    }
    
    @IBAction func btnSaveChangesTapped(_ sender: UIButton){
        
        var borrowerFirstName: String = "", borrowerLastName: String = "", borrowerEmail: String = "", borrowerHomeNumber: String = ""
        var dependentAges = [String:String]()
        
        do{
            let firstName = try validation.validateBorrowerFirstName(txtfieldLegalFirstName.text)
            borrowerFirstName = firstName
            DispatchQueue.main.async {
                self.txtfieldLegalFirstName.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldLegalFirstName.detail = ""
            }
            
        }
        catch{
            self.txtfieldLegalFirstName.dividerColor = .red
            self.txtfieldLegalFirstName.detail = error.localizedDescription
        }
        
        do{
            let lastName = try validation.validateBorrowerLastName(txtfieldLegalLastName.text)
            borrowerLastName = lastName
            DispatchQueue.main.async {
                self.txtfieldLegalLastName.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldLegalLastName.detail = ""
            }
            
        }
        catch{
            self.txtfieldLegalLastName.dividerColor = .red
            self.txtfieldLegalLastName.detail = error.localizedDescription
        }
        
        do{
            let email = try validation.validateBorrowerEmail(txtfieldEmail.text)
            borrowerEmail = email
            DispatchQueue.main.async {
                self.txtfieldEmail.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldEmail.detail = ""
            }
            
        }
        catch{
            self.txtfieldEmail.dividerColor = .red
            self.txtfieldEmail.detail = error.localizedDescription
        }
        
        do{
            let homeNumber = try validation.validateBorrowrHomePhoneNumber(txtfieldHomeNumber.text)
            borrowerHomeNumber = homeNumber
            DispatchQueue.main.async {
                self.txtfieldHomeNumber.dividerColor = Theme.getSeparatorNormalColor()
                self.txtfieldHomeNumber.detail = ""
            }
            
        }
        catch{
            self.txtfieldHomeNumber.dividerColor = .red
            self.txtfieldHomeNumber.detail = error.localizedDescription
        }
        
        for i in 0..<noOfDependents{
            let cell = dependentsCollectionView.cellForItem(at: IndexPath(row: i, section: 0)) as! DependentCollectionViewCell
            do{
                let age = try validation.validateDependentAge(cell.txtfieldAge.text)
                let dictKey = (i + 1).ordinalNumber()
                dependentAges[dictKey] = age
                DispatchQueue.main.async {
                    cell.txtfieldAge.dividerColor = Theme.getSeparatorNormalColor()
                    cell.txtfieldAge.detail = ""
                }
                
            }
            catch{
                cell.txtfieldAge.dividerColor = .red
                cell.txtfieldAge.detail = error.localizedDescription
            }
        }
        
        if (borrowerFirstName != "" && borrowerLastName != "" && borrowerEmail != "" && borrowerHomeNumber != "" && dependentAges.keys.count == noOfDependents){
            self.goBack()
        }
    }
}

extension BorrowerInformationViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return totalAddresses
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
        
        if (indexPath.row == 0){
            let vc = Utility.getAddResidenceVC()
            let navVC = UINavigationController(rootViewController: vc)
            navVC.modalPresentationStyle = .fullScreen
            navVC.navigationBar.isHidden = true
            self.presentVC(vc: navVC)
        }
        else{
            let vc = Utility.getAddPreviousResidenceVC()
            let navVC = UINavigationController(rootViewController: vc)
            navVC.modalPresentationStyle = .fullScreen
            navVC.navigationBar.isHidden = true
            self.presentVC(vc: navVC)
        }
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return UITableView.automaticDimension
    }
    
    func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        return true
    }
    
    func tableView(_ tableView: UITableView, trailingSwipeActionsConfigurationForRowAt indexPath: IndexPath) -> UISwipeActionsConfiguration? {
        
        let deleteView = UIView()
        deleteView.frame = CGRect(x: 0, y: 0, width: 60, height: 135)
        deleteView.backgroundColor = UIColor(patternImage: UIImage(named: "AddressDeleteIconBig")!)
        
        let deleteAction = UIContextualAction(style: .destructive, title: "") { action, actionView, bool in
            DispatchQueue.main.async {
                let vc = Utility.getDeleteAddressPopupVC()
                vc.popupTitle = "Are you sure you want to delete Richard's Current Residence?"
                vc.screenType = 1
                vc.indexPath = indexPath
                vc.delegate = self
                self.present(vc, animated: false, completion: nil)
            }
        }
        deleteAction.backgroundColor = Theme.getDashboardBackgroundColor()
        deleteAction.image = UIImage(named: "AddressDeleteIconBig")
        return UISwipeActionsConfiguration(actions: [deleteAction])
        
    }
}

extension BorrowerInformationViewController: DeleteAddressPopupViewControllerDelegate{
    func deleteAddress(indexPath: IndexPath) {
        totalAddresses = totalAddresses - 1
        self.lblAddAddress.text = totalAddresses == 0 ? "Add Current Residence" : "Add Previous Residence"
        self.tblViewAddress.deleteRows(at: [indexPath], with: .left)
        self.tblViewAddress.reloadData()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
}

extension BorrowerInformationViewController: UICollectionViewDataSource, UICollectionViewDelegate{
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return noOfDependents
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "DependentCollectionViewCell", for: indexPath) as! DependentCollectionViewCell
        let textFieldNo = indexPath.row + 1
        cell.txtfieldAge.placeholder = "\(textFieldNo.ordinalNumber()) Dependent Age (Years)"
        cell.indexPath = indexPath
        cell.delegate = self
        return cell
    }
    
}

extension BorrowerInformationViewController: DependentCollectionViewCellDelegate{
    func deleteDependent(indexPath: IndexPath) {
        self.dependentsCollectionView.deleteItems(at: [indexPath])
        self.noOfDependents = self.noOfDependents - 1
        self.lblNoOfDependent.text = "\(noOfDependents)"
        self.dependentsCollectionView.reloadData()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
}

extension BorrowerInformationViewController: UITextFieldDelegate{
    
    func textFieldDidBeginEditing(_ textField: UITextField) {
        if (textField == txtfieldDOB){
            dateChanged()
        }
    }
    
    func textFieldDidEndEditing(_ textField: UITextField) {
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldLegalFirstName, txtfieldMiddleName, txtfieldLegalLastName, txtfieldSuffix, txtfieldEmail, txtfieldHomeNumber, txtfieldWorkNumber, txtfieldExtensionNumber, txtfieldCellNumber, txtfieldDOB, txtfieldSecurityNo /*txtfieldNoOfDependent*/])
       
        if (textField == txtfieldLegalFirstName){
            do{
                let firstName = try validation.validateBorrowerFirstName(txtfieldLegalFirstName.text)
                DispatchQueue.main.async {
                    self.txtfieldLegalFirstName.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldLegalFirstName.detail = ""
                }
                
            }
            catch{
                self.txtfieldLegalFirstName.dividerColor = .red
                self.txtfieldLegalFirstName.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldLegalLastName){
            do{
                let lastName = try validation.validateBorrowerLastName(txtfieldLegalLastName.text)
                DispatchQueue.main.async {
                    self.txtfieldLegalLastName.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldLegalLastName.detail = ""
                }
                
            }
            catch{
                self.txtfieldLegalLastName.dividerColor = .red
                self.txtfieldLegalLastName.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldEmail){
            do{
                let email = try validation.validateBorrowerEmail(txtfieldEmail.text)
                DispatchQueue.main.async {
                    self.txtfieldEmail.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldEmail.detail = ""
                }
                
            }
            catch{
                self.txtfieldEmail.dividerColor = .red
                self.txtfieldEmail.detail = error.localizedDescription
            }
        }
        
        if (textField == txtfieldHomeNumber){
            do{
                let homeNumber = try validation.validateBorrowrHomePhoneNumber(txtfieldHomeNumber.text)
                DispatchQueue.main.async {
                    self.txtfieldHomeNumber.dividerColor = Theme.getSeparatorNormalColor()
                    self.txtfieldHomeNumber.detail = ""
                }
                
            }
            catch{
                self.txtfieldHomeNumber.dividerColor = .red
                self.txtfieldHomeNumber.detail = error.localizedDescription
            }
        }
        
//        for i in 0..<noOfDependents{
//            let cell = dependentsCollectionView.cellForItem(at: IndexPath(row: i, section: 0)) as! DependentCollectionViewCell
//            print("\((i + 1).ordinalNumber()) Dependent age is \(cell.txtfieldAge.text!)")
//        }
    }
    
    func textField(_ textField: UITextField, shouldChangeCharactersIn range: NSRange, replacementString string: String) -> Bool {
        
        if (textField == txtfieldHomeNumber || textField == txtfieldWorkNumber || textField == txtfieldCellNumber){
            guard let text = textField.text else { return false }
            let newString = (text as NSString).replacingCharacters(in: range, with: string)
            textField.text = self.formatPhoneNumber(with: "(XXX) XXX-XXXX", phone: newString)
            return false
        }
        else if (textField == txtfieldSecurityNo){
            guard let text = textField.text else { return false }
            let newString = (text as NSString).replacingCharacters(in: range, with: string)
            textField.text = self.formatPhoneNumber(with: "XXX-XX-XXXX", phone: newString)
            return false
        }
        else if (textField == txtfieldExtensionNumber){
            return string == "" ? true : (txtfieldExtensionNumber.text!.count < 8)
        }
        else{
            return true
        }
//        do{
//            let phoneNumber = try validation.validatePhoneNumber(txtFieldPhone.text)
//            self.lblPhoneError.isHidden = true
//            self.phoneSeparator.backgroundColor = Theme.getSeparatorNormalColor()
//        }
//        catch{
////            self.lblPhoneError.text = error.localizedDescription
////            self.lblPhoneError.isHidden = false
////            self.phoneSeparator.backgroundColor = Theme.getSeparatorErrorColor()
//        }
        
    }
}
