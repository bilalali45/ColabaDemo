//
//  BorrowerInformationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/08/2021.
//

import UIKit

class BorrowerInformationViewController: BaseViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblBorrowerType: UILabel!
    @IBOutlet weak var lblBorrowerName: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var borrowerInfoView: UIView!
    @IBOutlet weak var txtfieldLegalFirstName: ColabaTextField!
    @IBOutlet weak var txtfieldMiddleName: ColabaTextField!
    @IBOutlet weak var txtfieldLegalLastName: ColabaTextField!
    @IBOutlet weak var txtfieldSuffix: ColabaTextField!
    @IBOutlet weak var txtfieldEmail: ColabaTextField!
    @IBOutlet weak var txtfieldHomeNumber: ColabaTextField!
    @IBOutlet weak var txtfieldWorkNumber: ColabaTextField!
    @IBOutlet weak var txtfieldExtensionNumber: ColabaTextField!
    @IBOutlet weak var txtfieldCellNumber: ColabaTextField!
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
    @IBOutlet weak var txtfieldDOB: ColabaTextField!
    @IBOutlet weak var txtfieldSecurityNo: ColabaTextField!
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
    @IBOutlet weak var reserveNationalGuardStackViewTopConstraint: NSLayoutConstraint!// 17 or 116
    @IBOutlet weak var veteranStackView: UIStackView!
    @IBOutlet weak var btnVeteran: UIButton!
    @IBOutlet weak var lblVeteran: UILabel!
    @IBOutlet weak var veteranStackViewTopConstraint: NSLayoutConstraint!// 136 or 17
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
    @IBOutlet weak var btnSaveChanges: ColabaButton!
    
    var totalAddresses = 0
    var maritalStatus = 0 //1 for unmarried, 2 for married and 3 for separated
    var citizenshipStatus = 0 // 1 for US Citizen, 2 for Permanent and 3 for Non Permanent
    var isShowSecurityNo = false
    var noOfDependents = 0
    var isActiveDutyPersonal = false
    var isReserveOrNationalCard = false
    var isVeteran = false
    var isSurvivingSpouse = false
    
    var maritalStatusArray = [DropDownModel]()
    var housingStatusArray = [DropDownModel]()
    var relationshipTypeArray = [DropDownModel]()
    var citizenshipArray = [DropDownModel]()
    var visaStatusArray = [DropDownModel]()
    var militaryAffiliationArray = [DropDownModel]()
    var borrowerInformationModel = BorrowerInformationModel()
    var noOfAges = [String]()
    
    var loanApplicationId = 0
    var borrowerId = 0
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setViews()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        setDependentCollectionViewLayout()
        setScreenHeight()
        setTextFields()
        getAllMaritalStatus()
        self.lblAddAddress.text = totalAddresses == 0 ? "Add Current Residence" : "Add Previous Residence"
    }

    //MARK:- Methods and Actions
    func setTextFields() {
        ///Legal First Name Text Field
        txtfieldLegalFirstName.setTextField(placeholder: "Legal First Name", controller: self, validationType: .noValidation)
        txtfieldLegalFirstName.setIsValidateOnEndEditing(validate: false)
        
        ///Legal Middle Name Text Field
        txtfieldMiddleName.setTextField(placeholder: "Middle Name", controller: self, validationType: .noValidation)
        txtfieldMiddleName.setIsValidateOnEndEditing(validate: false)
        
        ///Legal Last Name Text Field
        txtfieldLegalLastName.setTextField(placeholder: "Legal Last Name", controller: self, validationType: .noValidation)
        txtfieldLegalLastName.setIsValidateOnEndEditing(validate: false)
        
        ///Suffix Text Field
        txtfieldSuffix.setTextField(placeholder: "Suffix", controller: self, validationType: .noValidation)
        txtfieldSuffix.setIsValidateOnEndEditing(validate: false)
        
        ///Email Adress Text Field
        txtfieldEmail.setTextField(placeholder: "Email", controller: self, validationType: .email, keyboardType: .emailAddress)
        
        ///Home Number Text Field
        txtfieldHomeNumber.setTextField(placeholder: "Home Number", controller: self, validationType: .phoneNumber, keyboardType: .phonePad)
        
        ///Work Number Text Field
        txtfieldWorkNumber.setTextField(placeholder: "Work Number", controller: self, validationType: .phoneNumber, keyboardType: .phonePad)

        ///Extension Number Text Field
        txtfieldExtensionNumber.setTextField(placeholder: "EXT", controller: self, validationType: .noValidation, keyboardType: .phonePad)
        
        ///Cell Number Text Field
        txtfieldCellNumber.setTextField(placeholder: "Cell Number", controller: self, validationType: .phoneNumber, keyboardType: .phonePad)
        
        ///Date of Birth Text Field
        txtfieldDOB.setTextField(placeholder: "Date of Birth", controller: self, validationType: .noValidation)
        txtfieldDOB.type = .datePicker
        
        ///Work Number Text Field
        txtfieldSecurityNo.setTextField(placeholder: "Social Security Number", controller: self, validationType: .socialSecurityNumber, keyboardType: .numberPad)
        txtfieldSecurityNo.type = .password
    }
    
    func setBorrowerInformation(){
        lblBorrowerName.text = "\(borrowerInformationModel.borrowerBasicDetails.firstName.uppercased()) \(borrowerInformationModel.borrowerBasicDetails.lastName.uppercased())"
        txtfieldLegalFirstName.setTextField(text: borrowerInformationModel.borrowerBasicDetails.firstName)
        txtfieldMiddleName.setTextField(text: borrowerInformationModel.borrowerBasicDetails.middleName)
        txtfieldLegalLastName.setTextField(text: borrowerInformationModel.borrowerBasicDetails.lastName)
        txtfieldSuffix.setTextField(text: borrowerInformationModel.borrowerBasicDetails.suffix)
        txtfieldEmail.setTextField(text: borrowerInformationModel.borrowerBasicDetails.emailAddress)
        txtfieldHomeNumber.setTextField(text: borrowerInformationModel.borrowerBasicDetails.homePhone)
        txtfieldWorkNumber.setTextField(text: borrowerInformationModel.borrowerBasicDetails.workPhone)
        txtfieldExtensionNumber.setTextField(text: borrowerInformationModel.borrowerBasicDetails.workPhoneExt)
        txtfieldCellNumber.setTextField(text: borrowerInformationModel.borrowerBasicDetails.cellPhone)
       
        if let selectedMaritalStatus = maritalStatusArray.filter({$0.optionId == self.borrowerInformationModel.maritalStatus.maritalStatusId}).first{
            if (selectedMaritalStatus.optionName.localizedCaseInsensitiveContains("Unmarried")){
                btnUnMarried.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblUnMarried.font = Theme.getRubikMediumFont(size: 14)
            }
            else if (selectedMaritalStatus.optionName.localizedCaseInsensitiveContains("Married")){
                btnMarried.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblMarried.font = Theme.getRubikMediumFont(size: 14)
            }
            else if (selectedMaritalStatus.optionName.localizedCaseInsensitiveContains("Separated")){
                btnSeparated.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblSeparated.font = Theme.getRubikMediumFont(size: 14)
            }
        }
        
        if let selectedCitizenshipStatus = citizenshipArray.filter({$0.optionId == self.borrowerInformationModel.borrowerCitizenship.residencyTypeId}).first{
            if (selectedCitizenshipStatus.optionName.localizedCaseInsensitiveContains("US Citizen")){
                btnUSCitizen.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblUSCitizen.font = Theme.getRubikMediumFont(size: 14)
            }
            else if (selectedCitizenshipStatus.optionName.localizedCaseInsensitiveContains("Permanent Resident Alien (Green Card)")){
                btnPermanentResident.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblPermanentResident.font = Theme.getRubikMediumFont(size: 14)
            }
            else if (selectedCitizenshipStatus.optionName.localizedCaseInsensitiveContains("Non Permanent Resident Alien")){
                btnNonPermanent.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblNonPermanent.font = Theme.getRubikMediumFont(size: 14)
            }
        }
        nonPermanentResidentMainView.isHidden = borrowerInformationModel.borrowerCitizenship.residencyStatusId == 0
        citizenshipViewHeightConstraint.constant = borrowerInformationModel.borrowerCitizenship.residencyStatusId == 0 ? 140 : 242
        txtfieldDOB.setTextField(text: Utility.getDayMonthYear(borrowerInformationModel.borrowerCitizenship.dobUtc))
        txtfieldSecurityNo.setTextField(text: borrowerInformationModel.borrowerCitizenship.ssn)
        noOfDependents = borrowerInformationModel.borrowerCitizenship.dependentCount
        lblNoOfDependent.text = "\(borrowerInformationModel.borrowerCitizenship.dependentCount)"
        noOfAges = borrowerInformationModel.borrowerCitizenship.dependentAges.components(separatedBy: ",")
        self.dependentsCollectionView.reloadData()
        
        for militaryService in borrowerInformationModel.militaryServiceDetails.details{
            if let militaryAffiliation = militaryAffiliationArray.filter({$0.optionId == militaryService.militaryAffiliationId}).first{
                if (militaryAffiliation.optionName.localizedCaseInsensitiveContains("Active Duty Personnel")){
                    isActiveDutyPersonal = true
                    btnActiveDuty.setImage(UIImage(named: "CheckBoxSelected"), for: .normal)
                    lblActiveDuty.font = Theme.getRubikMediumFont(size: 14)
                    lblLastDate.text = Utility.getMonthYear(militaryService.expirationDateUtc)
                    changeMilitaryStatus()
                }
                else if (militaryAffiliation.optionName.localizedCaseInsensitiveContains("Reserve Or National Guard")){
                    isReserveOrNationalCard = true
                    btnReserveNationalGuard.setImage(UIImage(named: "CheckBoxSelected"), for: .normal)
                    lblReserveNationalGuard.font = Theme.getRubikMediumFont(size: 14)
                    lblReserveNationalGuardQuestion.text = "Was \(borrowerInformationModel.borrowerBasicDetails.firstName.capitalized) \(borrowerInformationModel.borrowerBasicDetails.lastName.capitalized) ever activated during their tour of duty?"
                    lblReserveNationalGuardAns.text = militaryService.reserveEverActivated ? "Yes" : "No"
                    changeMilitaryStatus()
                }
                else if (militaryAffiliation.optionName.localizedCaseInsensitiveContains("Surviving Spouse")){
                    isSurvivingSpouse = true
                    btnSurvivingSpouse.setImage(UIImage(named: "CheckBoxSelected"), for: .normal)
                    lblSurvingSpouse.font = Theme.getRubikMediumFont(size: 14)
                    changeMilitaryStatus()
                }
                else if (militaryAffiliation.optionName.localizedCaseInsensitiveContains("Veteran")){
                    isVeteran = true
                    btnVeteran.setImage(UIImage(named: "CheckBoxSelected"), for: .normal)
                    lblVeteran.font = Theme.getRubikMediumFont(size: 14)
                    changeMilitaryStatus()
                }
            }
        }
        
        totalAddresses = borrowerInformationModel.currentAddress.id > 0 ? 1 : 0
        totalAddresses = totalAddresses + borrowerInformationModel.previousAddresses.count
        self.tblViewAddress.reloadData()
        self.lblAddAddress.text = totalAddresses == 0 ? "Add Current Residence" : "Add Previous Residence"
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
            self.changeMilitaryStatus()
        }
    }
    
    func setViews() {
        unMarriedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(unmarriedTapped)))
        marriedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(marriedTapped)))
        separatedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(separatedTapped)))
        
        btnUnMarried.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblUnMarried.font = Theme.getRubikRegularFont(size: 14)
        unmarriedMainView.isHidden = true
        maritalStatusViewHeightConstraint.constant = 140
        
        usCitizenStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(usCitizenTapped)))
        permanentResidentStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(permanentResidentTapped)))
        nonPermanentStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonPermanentResidentTapped)))
        
        btnNonPermanent.setImage(UIImage(named: "RadioButtonUnselected"), for: .normal)
        lblNonPermanent.font = Theme.getRubikRegularFont(size: 14)
        nonPermanentResidentMainView.isHidden = true
        citizenshipViewHeightConstraint.constant = 140
        
        stackViewAddDependent.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addDependentTapped)))
        
        activeDutyStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(activeDutyTapped)))
        reserveNationalGuardStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(reserveNationalGuardTapped)))
        veteranStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(veteranTapped)))
        survivingSpouseStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(survivingSpouseTapped)))
        
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
        
        btnActiveDuty.setImage(UIImage(named: isActiveDutyPersonal ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblActiveDuty.font = isActiveDutyPersonal ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        btnReserveNationalGuard.setImage(UIImage(named: isActiveDutyPersonal ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblReserveNationalGuard.font = isActiveDutyPersonal ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        changeMilitaryStatus()
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
        
        tblViewAddress.register(UINib(nibName: "BorrowerAddressInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressInfoTableViewCell")
        dependentsCollectionView.register(UINib(nibName: "DependentCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "DependentCollectionViewCell")
    }
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
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
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
        vc.relationshipTypeArray = self.relationshipTypeArray
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
        vc.relationshipTypeArray = self.relationshipTypeArray
        vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
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
        if let citizenShip = self.citizenshipArray.filter({$0.optionName.localizedCaseInsensitiveContains("Non Permanent Resident Alien")}).first{
            self.getAllVisaStatus(residencyTypeId: citizenShip.optionId, isForAddUpdate: true)
        }
        
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
        vc.visaStatusArray = visaStatusArray
        vc.selectedCitizenShip = borrowerInformationModel.borrowerCitizenship
        vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
        self.presentVC(vc: vc)
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
            vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
            if let activeDuty = self.borrowerInformationModel.militaryServiceDetails.details.filter({$0.militaryAffiliationId == self.militaryAffiliationArray.filter({$0.optionName == "Active Duty Personnel"}).first!.optionId}).first{
                vc.selectedMilitary = activeDuty
            }
            self.presentVC(vc: vc)
        }
    }
    
    @objc func lastDateOfServiceMainViewTapped(){
        let vc = Utility.getActiveDutyPersonnelFollowUpQuestionVC()
        vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
        if let activeDuty = self.borrowerInformationModel.militaryServiceDetails.details.filter({$0.militaryAffiliationId == self.militaryAffiliationArray.filter({$0.optionName == "Active Duty Personnel"}).first!.optionId}).first{
            vc.selectedMilitary = activeDuty
        }
        self.presentVC(vc: vc)
    }
    
    @objc func reserveNationalGuardTapped(){
        isReserveOrNationalCard = !isReserveOrNationalCard
        btnReserveNationalGuard.setImage(UIImage(named: isReserveOrNationalCard ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblReserveNationalGuard.font = isReserveOrNationalCard ? Theme.getRubikMediumFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        changeMilitaryStatus()
        if (isReserveOrNationalCard){
            let vc = Utility.getReserveFollowUpQuestionsVC()
            vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
            if let reserveNationalGuard = self.borrowerInformationModel.militaryServiceDetails.details.filter({$0.militaryAffiliationId == self.militaryAffiliationArray.filter({$0.optionName == "Reserve Or National Guard"}).first!.optionId}).first{
                vc.selectedMilitary = reserveNationalGuard
            }
            self.presentVC(vc: vc)
        }
    }
    
    @objc func reserveOrNationalGuardMainViewTapped(){
        let vc = Utility.getReserveFollowUpQuestionsVC()
        vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
        if let reserveNationalGuard = self.borrowerInformationModel.militaryServiceDetails.details.filter({$0.militaryAffiliationId == self.militaryAffiliationArray.filter({$0.optionName == "Reserve Or National Guard"}).first!.optionId}).first{
            vc.selectedMilitary = reserveNationalGuard
        }
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
            reserveNationalGuardStackViewTopConstraint.constant = 116
            veteranStackViewTopConstraint.constant = 136
            militaryViewHeightConstraint.constant = 470
        }
        else if (isActiveDutyPersonal && !isReserveOrNationalCard){ // Active duty is selected but reserve national is not selected
            lastDateOfServiceMainView.isHidden = false
            reserveOrNationalGuardMainView.isHidden = true
            reserveNationalGuardStackViewTopConstraint.constant = 116
            veteranStackViewTopConstraint.constant = 0
            militaryViewHeightConstraint.constant = 340
        }
        else if (isReserveOrNationalCard && !isActiveDutyPersonal){ // Reserve is selected but active duty is not selected
            lastDateOfServiceMainView.isHidden = true
            reserveOrNationalGuardMainView.isHidden = false
            reserveNationalGuardStackViewTopConstraint.constant = 0
            veteranStackViewTopConstraint.constant = 136
            militaryViewHeightConstraint.constant = 370
        }
        else{ // No Active duty and no reserver personal
            lastDateOfServiceMainView.isHidden = true
            reserveOrNationalGuardMainView.isHidden = true
            reserveNationalGuardStackViewTopConstraint.constant = 0
            veteranStackViewTopConstraint.constant = 0
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
        var dependentAges = [String:String]()
        
//        for i in 0..<noOfDependents{
//            let cell = dependentsCollectionView.cellForItem(at: IndexPath(row: i, section: 0)) as! DependentCollectionViewCell
//            do{
//                let age = try cell.txtfieldAge.text?.validate(type: .required)
//                let dictKey = (i + 1).ordinalNumber()
//                dependentAges[dictKey] = cell.txtfieldAge.text
//                DispatchQueue.main.async {
//                    cell.txtfieldAge.dividerColor = Theme.getSeparatorNormalColor()
//                    cell.txtfieldAge.detail = ""
//                }
//
//            }
//            catch{
//                cell.txtfieldAge.dividerColor = .red
//                cell.txtfieldAge.detail = error.localizedDescription
//            }
//        }
        if validate() {
            if (txtfieldEmail.text != "" && txtfieldHomeNumber.text != ""){
                if (txtfieldEmail.text!.isValidEmail() && txtfieldHomeNumber.text?.count == 14){
                    self.goBack()
                }
            }
            else if (txtfieldEmail.text != "" && txtfieldEmail.text!.isValidEmail() && txtfieldHomeNumber.text == ""){
                self.goBack()
            }
            else if (txtfieldEmail.text == "" && txtfieldHomeNumber.text?.count == 14){
                self.goBack()
            }
            else if (txtfieldEmail.text == "" && txtfieldHomeNumber.text == ""){
                self.goBack()
            }
        }
    }
    
    func validate() -> Bool {
        
        var isValidate = true
        
        for i in 0..<noOfDependents{
            let cell = dependentsCollectionView.cellForItem(at: IndexPath(row: i, section: 0)) as! DependentCollectionViewCell
            isValidate = cell.txtfieldAge.validate() && isValidate
        }
        
        if txtfieldEmail.text != "" {
            isValidate = txtfieldEmail.validate() && isValidate
        }
        if txtfieldHomeNumber.text != "" {
            isValidate = txtfieldHomeNumber.validate() && isValidate
        }
        if txtfieldWorkNumber.text != "" {
            isValidate = txtfieldWorkNumber.validate() && isValidate
        }
        if txtfieldCellNumber.text != "" {
            isValidate = txtfieldCellNumber.validate() && isValidate
        }
        if txtfieldSecurityNo.text != "" {
            isValidate = txtfieldSecurityNo.validate() && isValidate
        }
        return isValidate
    }
    
    //MARK:- API's
    
    func getAllMaritalStatus(){
        
        self.maritalStatusArray.removeAll()
        self.housingStatusArray.removeAll()
        self.relationshipTypeArray.removeAll()
        self.citizenshipArray.removeAll()
        self.visaStatusArray.removeAll()
        self.militaryAffiliationArray.removeAll()
        
        Utility.showOrHideLoader(shouldShow: true)
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getMaritalStatusList, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option, isForBorrowerInfo: true)
                        self.maritalStatusArray.append(model)
                    }
                    self.getAllHousingStatus()
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
    
    func getAllHousingStatus(){
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getHousingStatus, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option, isForBorrowerInfo: true)
                        self.housingStatusArray.append(model)
                    }
                    self.getAllRelationshipTypes()
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
    
    func getAllRelationshipTypes(){
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getRelationshipType, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option)
                        self.relationshipTypeArray.append(model)
                    }
                    self.getAllCitizenShipsTypes()
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
    
    func getAllCitizenShipsTypes(){
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getCitizenShip, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option, isForBorrowerInfo: true)
                        self.citizenshipArray.append(model)
                    }
                    self.getAllMilitaryAffiliation()
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
    
    func getAllVisaStatus(residencyTypeId: Int, isForAddUpdate: Bool = false){
        let extraData = "residencyTypeId=\(residencyTypeId)"
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getVisaStatus, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option, isForBorrowerInfo: true)
                        self.visaStatusArray.append(model)
                    }
                    if (!isForAddUpdate){
                        if let selectedVisaStatus = self.visaStatusArray.filter({$0.optionId == self.borrowerInformationModel.borrowerCitizenship.residencyStatusId}).first{
                            self.lblNonPermanentAns.text = selectedVisaStatus.optionName
                        }
                    }
                    else{
                        let vc = Utility.getNonPermanentResidenceFollowUpQuestionsVC()
                        vc.visaStatusArray = self.visaStatusArray
                        vc.selectedCitizenShip = self.borrowerInformationModel.borrowerCitizenship
                        vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
                        self.presentVC(vc: vc)
                    }
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
    
    func getAllMilitaryAffiliation(){
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getMilitartyAffiliation, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                if (status == .success){
                    let optionsArray = result.arrayValue
                    for option in optionsArray{
                        let model = DropDownModel()
                        model.updateModelWithJSON(json: option, isForBorrowerInfo: true)
                        self.militaryAffiliationArray.append(model)
                    }
                    if (self.loanApplicationId > 0){
                        self.getBorrowerDetail()
                    }
                    Utility.showOrHideLoader(shouldShow: false)
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
    
    func getBorrowerDetail(){
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerId=\(borrowerId)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getBorrowerDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                Utility.showOrHideLoader(shouldShow: false)
                
                if (status == .success){
                    let model = BorrowerInformationModel()
                    model.updateModelWithJSON(json: result["data"])
                    self.borrowerInformationModel = model
                    self.getAllVisaStatus(residencyTypeId: self.borrowerInformationModel.borrowerCitizenship.residencyTypeId)
                    self.setBorrowerInformation()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.goBack()
                    }
                }
            }
        }
        
    }
    
    func deletePreviousAddress(addressId: Int){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)&id=\(addressId)"
        
        APIRouter.sharedInstance.executeAPI(type: .deleteBorrowerPreviousAddress, method: .post, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    self.getBorrowerDetail()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        self.dismissVC()
                    }
                }
            }
        }
    }
}

extension BorrowerInformationViewController: ColabaTextFieldDelegate {
    func textFieldEndEditing(_ textField: ColabaTextField) {
        _ = validate()
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
        
        if (indexPath.row == 0){
            cell.lblAddressTopConstraint.constant = 51
            let address = borrowerInformationModel.currentAddress.addressModel
            cell.lblAddress.text = "\(address.street) \(address.unit),\n\(address.city), \(address.stateName) \(address.zipCode)"
            cell.lblRent.isHidden = borrowerInformationModel.currentAddress.monthlyRent == 0
            cell.lblRent.text = "Rental \(borrowerInformationModel.currentAddress.monthlyRent.withCommas().replacingOccurrences(of: ".00", with: ""))"
            cell.lblDate.text = Utility.getIncomeDate(startDate: borrowerInformationModel.currentAddress.fromDate, endDate: borrowerInformationModel.currentAddress.toDate, isForAddress: true)
        }
        else{
            cell.lblAddressTopConstraint.constant = 15
            let address = borrowerInformationModel.previousAddresses[indexPath.row - 1].addressModel
            cell.lblAddress.text = "\(address.street) \(address.unit),\n\(address.city), \(address.stateName) \(address.zipCode)"
            cell.lblRent.isHidden = borrowerInformationModel.previousAddresses[indexPath.row - 1].monthlyRent == 0
            cell.lblRent.text = "Rental \(borrowerInformationModel.previousAddresses[indexPath.row - 1].monthlyRent.withCommas().replacingOccurrences(of: ".00", with: ""))"
            cell.lblDate.text = Utility.getIncomeDate(startDate: borrowerInformationModel.previousAddresses[indexPath.row - 1].fromDate, endDate: borrowerInformationModel.previousAddresses[indexPath.row - 1].toDate, isForAddress: true)
        }
        
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
            vc.selectedAddress = borrowerInformationModel.currentAddress
            vc.borrowerFirstName = borrowerInformationModel.borrowerBasicDetails.firstName
            vc.borrowerLastName = borrowerInformationModel.borrowerBasicDetails.lastName
            vc.housingStatusArray = self.housingStatusArray
            let navVC = UINavigationController(rootViewController: vc)
            navVC.modalPresentationStyle = .fullScreen
            navVC.navigationBar.isHidden = true
            self.presentVC(vc: navVC)
        }
        else{
            let vc = Utility.getAddPreviousResidenceVC()
            vc.selectedAddress = borrowerInformationModel.previousAddresses[indexPath.row - 1]
            vc.borrowerFirstName = borrowerInformationModel.borrowerBasicDetails.firstName
            vc.borrowerLastName = borrowerInformationModel.borrowerBasicDetails.lastName
            vc.loanApplicationId = self.loanApplicationId
            vc.housingStatusArray = self.housingStatusArray
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
        return indexPath.row > 0
    }
    
    func tableView(_ tableView: UITableView, trailingSwipeActionsConfigurationForRowAt indexPath: IndexPath) -> UISwipeActionsConfiguration? {
        
        let deleteView = UIView()
        deleteView.frame = CGRect(x: 0, y: 0, width: 60, height: 135)
        deleteView.backgroundColor = UIColor(patternImage: UIImage(named: "AddressDeleteIconBig")!)
        
        let deleteAction = UIContextualAction(style: .destructive, title: "") { action, actionView, bool in
            DispatchQueue.main.async {
                let vc = Utility.getDeleteAddressPopupVC()
                if (indexPath.row == 0){
                    vc.popupTitle = "Are you sure you want to delete \(self.borrowerInformationModel.borrowerBasicDetails.firstName.capitalized)'s Current Residence?"
                }
                else{
                    vc.popupTitle = "Are you sure you want to delete \(self.borrowerInformationModel.borrowerBasicDetails.firstName.capitalized)'s Previous Residence?"
                }
                
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
        deletePreviousAddress(addressId: borrowerInformationModel.previousAddresses[indexPath.row - 1].id)
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
        if (indexPath.row < noOfAges.count){
            cell.txtfieldAge.setTextField(text: noOfAges[indexPath.row])
        }
        else{
            cell.txtfieldAge.setTextField(text: "")
        }
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
