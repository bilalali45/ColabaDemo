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
    @IBOutlet weak var maritalStatusViewHeightConstraint: NSLayoutConstraint! //200 or 342
    @IBOutlet weak var unMarriedStackView: UIView!
    @IBOutlet weak var unMarriedStackViewHeightConstraint: NSLayoutConstraint! //180 or 48
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
    @IBOutlet weak var citizenshipViewHeightConstraint: NSLayoutConstraint!//296 or 210
    @IBOutlet weak var usCitizenStackView: UIStackView!
    @IBOutlet weak var btnUSCitizen: UIButton!
    @IBOutlet weak var lblUSCitizen: UILabel!
    @IBOutlet weak var permanentResidentStackView: UIView!
    @IBOutlet weak var btnPermanentResident: UIButton!
    @IBOutlet weak var lblPermanentResident: UILabel!
    @IBOutlet weak var nonPermanentStackView: UIView!
    @IBOutlet weak var nonPermanentStackViewHeightConstraint: NSLayoutConstraint! //134 or 48
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
    @IBOutlet weak var militaryViewHeightConstraint: NSLayoutConstraint!// 650 or 432 or 458 or 260
    @IBOutlet weak var activeDutyStackView: UIView!
    @IBOutlet weak var activeDutyStackViewHeightConstraint: NSLayoutConstraint! //136 or 48
    @IBOutlet weak var btnActiveDuty: UIButton!
    @IBOutlet weak var lblActiveDuty: UILabel!
    @IBOutlet weak var reserveNationalGuardStackView: UIView!
    @IBOutlet weak var reserveNationalGuardStackViewHeightConstraint: NSLayoutConstraint! // 140 or 48
    @IBOutlet weak var btnReserveNationalGuard: UIButton!
    @IBOutlet weak var lblReserveNationalGuard: UILabel!
    @IBOutlet weak var veteranStackView: UIStackView!
    @IBOutlet weak var btnVeteran: UIButton!
    @IBOutlet weak var lblVeteran: UILabel!
    @IBOutlet weak var survivingSpouseStackView: UIStackView!
    @IBOutlet weak var btnSurvivingSpouse: UIButton!
    @IBOutlet weak var lblSurvingSpouse: UILabel!
    @IBOutlet weak var lastDateOfServiceMainView: UIView!
    @IBOutlet weak var lblLastDateQuestion: UILabel!
    @IBOutlet weak var lblLastDate: UILabel!
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
    var selectedResidencyStatusId: Any = NSNull()
    var selectedResidencyStatusExplanation: Any = NSNull()
    var totalBorrowers = [BorrowerInfoModel]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setViews()
        getAllMaritalStatus()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        setDependentCollectionViewLayout()
        setScreenHeight()
        setTextFields()
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
        txtfieldExtensionNumber.delegate = self
        
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
        
        lblBorrowerType.text = borrowerInformationModel.borrowerBasicDetails.ownTypeId == 1 ? "Primary Borrower" : "Co-Borrower"
        lblBorrowerName.text = "\(borrowerInformationModel.borrowerBasicDetails.firstName.uppercased()) \(borrowerInformationModel.borrowerBasicDetails.lastName.uppercased())"
        if (txtfieldLegalFirstName.text == ""){
            txtfieldLegalFirstName.setTextField(text: borrowerInformationModel.borrowerBasicDetails.firstName)
        }
        if (txtfieldMiddleName.text == ""){
            txtfieldMiddleName.setTextField(text: borrowerInformationModel.borrowerBasicDetails.middleName)
        }
        if (txtfieldLegalLastName.text == ""){
            txtfieldLegalLastName.setTextField(text: borrowerInformationModel.borrowerBasicDetails.lastName)
        }
        if (txtfieldSuffix.text == ""){
            txtfieldSuffix.setTextField(text: borrowerInformationModel.borrowerBasicDetails.suffix)
        }
        if (txtfieldEmail.text == ""){
            txtfieldEmail.setTextField(text: borrowerInformationModel.borrowerBasicDetails.emailAddress)
        }
        if (txtfieldHomeNumber.text == ""){
            let homePhoneNumber = formatNumber(with: "(XXX) XXX-XXXX", number: borrowerInformationModel.borrowerBasicDetails.homePhone)
            txtfieldHomeNumber.setTextField(text: homePhoneNumber)
        }
        if (txtfieldWorkNumber.text == ""){
            let workPhoneNumber = formatNumber(with: "(XXX) XXX-XXXX", number: borrowerInformationModel.borrowerBasicDetails.workPhone)
            txtfieldWorkNumber.setTextField(text: workPhoneNumber)
        }
        if (txtfieldExtensionNumber.text == ""){
            txtfieldExtensionNumber.setTextField(text: borrowerInformationModel.borrowerBasicDetails.workPhoneExt)
        }
        if (txtfieldCellNumber.text == ""){
            let cellPhoneNumber = formatNumber(with: "(XXX) XXX-XXXX", number: borrowerInformationModel.borrowerBasicDetails.cellPhone)
            txtfieldCellNumber.setTextField(text: cellPhoneNumber)
        }
        if (borrowerInformationModel.borrowerBasicDetails.firstName != ""){
            lblReserveNationalGuardQuestion.text =  "Was \(borrowerInformationModel.borrowerBasicDetails.firstName.capitalized) \(borrowerInformationModel.borrowerBasicDetails.lastName.capitalized) ever activated during their tour of duty?"
        }
        
        if let selectedMaritalStatus = maritalStatusArray.filter({$0.optionId == self.borrowerInformationModel.maritalStatus.maritalStatusId}).first{
            if (selectedMaritalStatus.optionName.localizedCaseInsensitiveContains("Unmarried")){
                maritalStatus = 1
                btnUnMarried.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblUnMarried.font = Theme.getRubikSemiBoldFont(size: 14)
                lblUnMarried.textColor = Theme.getAppBlackColor()
                unmarriedMainView.isHidden = false
                unMarriedStackViewHeightConstraint.constant = 180
                maritalStatusViewHeightConstraint.constant = 342
                lblUnmarriedAns.text = borrowerInformationModel.maritalStatus.isInRelationship == true ? "Yes" : "No"
                unMarriedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
                unMarriedStackView.dropShadowToCollectionViewCell()
                marriedStackView.removeShadow()
                separatedStackView.removeShadow()
            }
            else if (selectedMaritalStatus.optionName.localizedCaseInsensitiveContains("Married")){
                maritalStatus = 2
                btnMarried.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblMarried.font = Theme.getRubikSemiBoldFont(size: 14)
                lblMarried.textColor = Theme.getAppBlackColor()
                marriedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
                marriedStackView.dropShadowToCollectionViewCell()
                unMarriedStackView.removeShadow()
                separatedStackView.removeShadow()
            }
            else if (selectedMaritalStatus.optionName.localizedCaseInsensitiveContains("Separated")){
                maritalStatus = 3
                btnSeparated.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblSeparated.font = Theme.getRubikSemiBoldFont(size: 14)
                lblSeparated.textColor = Theme.getAppBlackColor()
                separatedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
                separatedStackView.dropShadowToCollectionViewCell()
                unMarriedStackView.removeShadow()
                marriedStackView.removeShadow()
            }
        }
        
        if let selectedCitizenshipStatus = citizenshipArray.filter({$0.optionId == self.borrowerInformationModel.borrowerCitizenship.residencyTypeId}).first{
            if (selectedCitizenshipStatus.optionName.localizedCaseInsensitiveContains("US Citizen")){
                citizenshipStatus = 1
                btnUSCitizen.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblUSCitizen.font = Theme.getRubikSemiBoldFont(size: 14)
                lblUSCitizen.textColor = Theme.getAppBlackColor()
                usCitizenStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
                usCitizenStackView.dropShadowToCollectionViewCell()
                permanentResidentStackView.removeShadow()
                nonPermanentStackView.removeShadow()
            }
            else if (selectedCitizenshipStatus.optionName.localizedCaseInsensitiveContains("Permanent Resident Alien (Green Card)")){
                citizenshipStatus = 2
                btnPermanentResident.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblPermanentResident.font = Theme.getRubikSemiBoldFont(size: 14)
                lblPermanentResident.textColor = Theme.getAppBlackColor()
                permanentResidentStackView.dropShadowToCollectionViewCell()
                usCitizenStackView.removeShadow()
                nonPermanentStackView.removeShadow()
            }
            else if (selectedCitizenshipStatus.optionName.localizedCaseInsensitiveContains("Non Permanent Resident Alien")){
                citizenshipStatus = 3
                btnNonPermanent.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
                lblNonPermanent.font = Theme.getRubikSemiBoldFont(size: 14)
                lblNonPermanent.textColor = Theme.getAppBlackColor()
                nonPermanentStackView.dropShadowToCollectionViewCell()
                permanentResidentStackView.removeShadow()
                usCitizenStackView.removeShadow()
                if let selectedVisaStatus = visaStatusArray.filter({$0.optionId == borrowerInformationModel.borrowerCitizenship.residencyStatusId}).first{
                    lblNonPermanentAns.text = selectedVisaStatus.optionName
                }
            }
        }
        selectedResidencyStatusId = borrowerInformationModel.borrowerCitizenship.residencyStatusId
        selectedResidencyStatusExplanation = borrowerInformationModel.borrowerCitizenship.residencyStatusExplanation
        nonPermanentResidentMainView.isHidden = borrowerInformationModel.borrowerCitizenship.residencyStatusId == 0
        citizenshipViewHeightConstraint.constant = borrowerInformationModel.borrowerCitizenship.residencyStatusId == 0 ? 210 : 296
        nonPermanentStackViewHeightConstraint.constant = borrowerInformationModel.borrowerCitizenship.residencyStatusId == 0 ? 48 : 134
        txtfieldDOB.setTextField(text: Utility.getDayMonthYear(borrowerInformationModel.borrowerCitizenship.dobUtc))
        txtfieldSecurityNo.setTextField(text: borrowerInformationModel.borrowerCitizenship.ssn)
        noOfDependents = borrowerInformationModel.borrowerCitizenship.dependentCount
        lblNoOfDependent.text = "\(borrowerInformationModel.borrowerCitizenship.dependentCount)"
        noOfAges = borrowerInformationModel.borrowerCitizenship.dependentAges.components(separatedBy: ",")
        changeMaritalStatus()
        changeCitizenshipStatus()
        self.dependentsCollectionView.reloadData()
        
        for militaryService in borrowerInformationModel.militaryServiceDetails.details{
            if let militaryAffiliation = militaryAffiliationArray.filter({$0.optionId == militaryService.militaryAffiliationId}).first{
                if (militaryAffiliation.optionName.localizedCaseInsensitiveContains("Active Duty Personnel")){
                    isActiveDutyPersonal = true
                    btnActiveDuty.setImage(UIImage(named: "CheckBoxSelected"), for: .normal)
                    lblActiveDuty.font = Theme.getRubikSemiBoldFont(size: 14)
                    lblActiveDuty.textColor = Theme.getAppBlackColor()
                    lblLastDate.text = Utility.getMonthYear(militaryService.expirationDateUtc)
                    changeMilitaryStatus()
                }
                else if (militaryAffiliation.optionName.localizedCaseInsensitiveContains("Reserve Or National Guard")){
                    isReserveOrNationalCard = true
                    btnReserveNationalGuard.setImage(UIImage(named: "CheckBoxSelected"), for: .normal)
                    lblReserveNationalGuard.font = Theme.getRubikSemiBoldFont(size: 14)
                    lblReserveNationalGuard.textColor = Theme.getAppBlackColor()
                    lblReserveNationalGuardQuestion.text = "Was \(borrowerInformationModel.borrowerBasicDetails.firstName.capitalized) \(borrowerInformationModel.borrowerBasicDetails.lastName.capitalized) ever activated during their tour of duty?"
                    lblReserveNationalGuardAns.text = militaryService.reserveEverActivated ? "Yes" : "No"
                    changeMilitaryStatus()
                }
                else if (militaryAffiliation.optionName.localizedCaseInsensitiveContains("Surviving Spouse")){
                    isSurvivingSpouse = true
                    btnSurvivingSpouse.setImage(UIImage(named: "CheckBoxSelected"), for: .normal)
                    lblSurvingSpouse.font = Theme.getRubikSemiBoldFont(size: 14)
                    lblSurvingSpouse.textColor = Theme.getAppBlackColor()
                    changeMilitaryStatus()
                }
                else if (militaryAffiliation.optionName.localizedCaseInsensitiveContains("Veteran")){
                    isVeteran = true
                    btnVeteran.setImage(UIImage(named: "CheckBoxSelected"), for: .normal)
                    lblVeteran.font = Theme.getRubikSemiBoldFont(size: 14)
                    lblVeteran.textColor = Theme.getAppBlackColor()
                    changeMilitaryStatus()
                }
            }
        }
        
        totalAddresses = borrowerInformationModel.currentAddress.addressModel.street != "" ? 1 : 0
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
        unMarriedStackView.layer.cornerRadius = 8
        unMarriedStackView.layer.borderWidth = 1
        unMarriedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        marriedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(marriedTapped)))
        marriedStackView.layer.cornerRadius = 8
        marriedStackView.layer.borderWidth = 1
        marriedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        separatedStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(separatedTapped)))
        separatedStackView.layer.cornerRadius = 8
        separatedStackView.layer.borderWidth = 1
        separatedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        btnUnMarried.setImage(UIImage(named: "radioUnslected"), for: .normal)
        lblUnMarried.font = Theme.getRubikRegularFont(size: 14)
        lblUnMarried.textColor = Theme.getAppGreyColor()
        unmarriedMainView.isHidden = true
        unMarriedStackViewHeightConstraint.constant = 48
        maritalStatusViewHeightConstraint.constant = 200
        
        usCitizenStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(usCitizenTapped)))
        usCitizenStackView.layer.cornerRadius = 8
        usCitizenStackView.layer.borderWidth = 1
        usCitizenStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        permanentResidentStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(permanentResidentTapped)))
        permanentResidentStackView.layer.cornerRadius = 8
        permanentResidentStackView.layer.borderWidth = 1
        permanentResidentStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        nonPermanentStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonPermanentResidentTapped)))
        nonPermanentStackView.layer.cornerRadius = 8
        nonPermanentStackView.layer.borderWidth = 1
        nonPermanentStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        btnNonPermanent.setImage(UIImage(named: "radioUnslected"), for: .normal)
        lblNonPermanent.font = Theme.getRubikRegularFont(size: 14)
        nonPermanentResidentMainView.isHidden = true
        citizenshipViewHeightConstraint.constant = 210
        nonPermanentStackViewHeightConstraint.constant = 48
        
        stackViewAddDependent.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addDependentTapped)))
        
        activeDutyStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(activeDutyTapped)))
        activeDutyStackView.layer.cornerRadius = 8
        activeDutyStackView.layer.borderWidth = 1
        activeDutyStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        reserveNationalGuardStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(reserveNationalGuardTapped)))
        reserveNationalGuardStackView.layer.cornerRadius = 8
        reserveNationalGuardStackView.layer.borderWidth = 1
        reserveNationalGuardStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        veteranStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(veteranTapped)))
        veteranStackView.layer.cornerRadius = 8
        veteranStackView.layer.borderWidth = 1
        veteranStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        survivingSpouseStackView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(survivingSpouseTapped)))
        survivingSpouseStackView.layer.cornerRadius = 8
        survivingSpouseStackView.layer.borderWidth = 1
        survivingSpouseStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        
        addAddressView.layer.cornerRadius = 6
        addAddressView.layer.borderWidth = 1
        addAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addAddressViewTapped)))
        
        unmarriedMainView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(unmarriedMainViewTapped)))
        
        nonPermanentResidentMainView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(nonPermanentResidentMainViewTapped)))
        
        lastDateOfServiceMainView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(lastDateOfServiceMainViewTapped)))
        
        reserveOrNationalGuardMainView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(reserveOrNationalGuardMainViewTapped)))
        
        btnActiveDuty.setImage(UIImage(named: isActiveDutyPersonal ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblActiveDuty.font = isActiveDutyPersonal ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblActiveDuty.textColor = isActiveDutyPersonal ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        btnReserveNationalGuard.setImage(UIImage(named: isReserveOrNationalCard ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblReserveNationalGuard.font = isReserveOrNationalCard ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblReserveNationalGuard.textColor = isReserveOrNationalCard ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
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
            vc.borrowerFirstName = borrowerInformationModel.borrowerBasicDetails.firstName
            vc.borrowerLastName = borrowerInformationModel.borrowerBasicDetails.lastName
            vc.housingStatusArray = self.housingStatusArray
            vc.delegate = self
            let navVC = UINavigationController(rootViewController: vc)
            navVC.modalPresentationStyle = .fullScreen
            navVC.navigationBar.isHidden = true
            self.presentVC(vc: navVC)
        }
        else{
            let vc = Utility.getAddPreviousResidenceVC()
            vc.borrowerFirstName = borrowerInformationModel.borrowerBasicDetails.firstName
            vc.borrowerLastName = borrowerInformationModel.borrowerBasicDetails.lastName
            vc.loanApplicationId = self.loanApplicationId
            vc.housingStatusArray = self.housingStatusArray
            vc.delegate = self
            let navVC = UINavigationController(rootViewController: vc)
            navVC.modalPresentationStyle = .fullScreen
            navVC.navigationBar.isHidden = true
            self.presentVC(vc: navVC)
        }
        
    }
    
    @objc func unmarriedTapped(){
//        maritalStatus = 1
//        changeMaritalStatus()
        let vc = Utility.getUnmarriedFollowUpQuestionsVC()
        vc.relationshipTypeArray = self.relationshipTypeArray
        vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
        vc.selectedMaritalStatus = self.borrowerInformationModel.maritalStatus
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func marriedTapped(){
        maritalStatus = 2
        borrowerInformationModel.maritalStatus.maritalStatusId = 1
        changeMaritalStatus()
        
        if (borrowerInformationModel.borrowerBasicDetails.ownTypeId == 1){
            if (totalBorrowers.count == 1){
                let vc = Utility.getSpouseBasicDetailVC()
                vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
                vc.selectedMaritalStatus = borrowerInformationModel.maritalStatus
                vc.delegate = self
                self.presentVC(vc: vc)
            }
            else{
                let vc = Utility.getSpouseLinkWithBorrowerVC()
                vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
                vc.selectedMaritalStatus = borrowerInformationModel.maritalStatus
                vc.totalBorrowers = self.totalBorrowers
                vc.delegate = self
                self.presentVC(vc: vc)
            }
        }
        else{
            if (borrowerInformationModel.maritalStatus.spouseBorrowerId == 0){
                let vc = Utility.getSpouseBasicDetailVC()
                vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
                vc.selectedMaritalStatus = borrowerInformationModel.maritalStatus
                vc.delegate = self
                self.presentVC(vc: vc)
            }
            else{
                let vc = Utility.getSpouseLinkWithBorrowerVC()
                vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
                vc.selectedMaritalStatus = borrowerInformationModel.maritalStatus
                vc.totalBorrowers = self.totalBorrowers
                vc.isForSecondaryBorrower = true
                vc.delegate = self
                self.presentVC(vc: vc)
            }
        }

    }
    
    @objc func separatedTapped(){
        maritalStatus = 3
        borrowerInformationModel.maritalStatus.maritalStatusId = 2
        changeMaritalStatus()
        
        if (borrowerInformationModel.borrowerBasicDetails.ownTypeId == 1){
            if (totalBorrowers.count == 1){
                let vc = Utility.getSpouseBasicDetailVC()
                vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
                vc.selectedMaritalStatus = borrowerInformationModel.maritalStatus
                vc.isForSeparated =  true
                vc.delegate = self
                self.presentVC(vc: vc)
            }
            else{
                let vc = Utility.getSpouseLinkWithBorrowerVC()
                vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
                vc.selectedMaritalStatus = borrowerInformationModel.maritalStatus
                vc.isForSeparated = true
                vc.totalBorrowers = self.totalBorrowers
                vc.delegate = self
                self.presentVC(vc: vc)
            }
        }
        else{
            if (borrowerInformationModel.maritalStatus.spouseBorrowerId == 0){
                let vc = Utility.getSpouseBasicDetailVC()
                vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
                vc.selectedMaritalStatus = borrowerInformationModel.maritalStatus
                vc.isForSeparated = true
                vc.delegate = self
                self.presentVC(vc: vc)
            }
            else{
                let vc = Utility.getSpouseLinkWithBorrowerVC()
                vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
                vc.selectedMaritalStatus = borrowerInformationModel.maritalStatus
                vc.isForSeparated = true
                vc.totalBorrowers = self.totalBorrowers
                vc.isForSecondaryBorrower = true
                vc.delegate = self
                self.presentVC(vc: vc)
            }
        }
        
    }
    
    func changeMaritalStatus(){
       
        if (maritalStatus == 1){
            btnUnMarried.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblUnMarried.font = Theme.getRubikSemiBoldFont(size: 14)
            lblUnMarried.textColor = Theme.getAppBlackColor()
            btnMarried.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblMarried.font = Theme.getRubikRegularFont(size: 14)
            lblMarried.textColor = Theme.getAppGreyColor()
            btnSeparated.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblSeparated.font = Theme.getRubikRegularFont(size: 14)
            lblSeparated.textColor = Theme.getAppGreyColor()
            unMarriedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            marriedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            separatedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            unMarriedStackView.dropShadowToCollectionViewCell()
            marriedStackView.removeShadow()
            separatedStackView.removeShadow()
        }
        else if (maritalStatus == 2){
            btnMarried.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblMarried.font = Theme.getRubikSemiBoldFont(size: 14)
            lblMarried.textColor = Theme.getAppBlackColor()
            btnUnMarried.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblUnMarried.font = Theme.getRubikRegularFont(size: 14)
            lblUnMarried.textColor = Theme.getAppGreyColor()
            btnSeparated.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblSeparated.font = Theme.getRubikRegularFont(size: 14)
            lblSeparated.textColor = Theme.getAppGreyColor()
            unmarriedMainView.isHidden = true
            unMarriedStackViewHeightConstraint.constant = 48
            maritalStatusViewHeightConstraint.constant = 200
            unMarriedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            marriedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            separatedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            marriedStackView.dropShadowToCollectionViewCell()
            unMarriedStackView.removeShadow()
            separatedStackView.removeShadow()
        }
        else if (maritalStatus == 3){
            btnSeparated.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblSeparated.font = Theme.getRubikSemiBoldFont(size: 14)
            lblSeparated.textColor = Theme.getAppBlackColor()
            btnUnMarried.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblUnMarried.font = Theme.getRubikRegularFont(size: 14)
            lblUnMarried.textColor = Theme.getAppGreyColor()
            btnMarried.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblMarried.font = Theme.getRubikRegularFont(size: 14)
            lblMarried.textColor = Theme.getAppGreyColor()
            unmarriedMainView.isHidden = true
            unMarriedStackViewHeightConstraint.constant = 48
            maritalStatusViewHeightConstraint.constant = 200
            unMarriedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            marriedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            separatedStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            separatedStackView.dropShadowToCollectionViewCell()
            marriedStackView.removeShadow()
            unMarriedStackView.removeShadow()
        }
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2) {
            self.setScreenHeight()
        }
    }
   
    @objc func unmarriedMainViewTapped(){
        let vc = Utility.getUnmarriedFollowUpQuestionsVC()
        vc.relationshipTypeArray = self.relationshipTypeArray
        vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
        vc.selectedMaritalStatus = self.borrowerInformationModel.maritalStatus
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func usCitizenTapped(){
        citizenshipStatus = 1
        borrowerInformationModel.borrowerCitizenship.residencyTypeId = 1
        changeCitizenshipStatus()
    }
    
    @objc func permanentResidentTapped(){
        citizenshipStatus = 2
        borrowerInformationModel.borrowerCitizenship.residencyTypeId = 1
        changeCitizenshipStatus()
    }
    
    @objc func nonPermanentResidentTapped(){
//        citizenshipStatus = 3
//        changeCitizenshipStatus()
        if let citizenShip = self.citizenshipArray.filter({$0.optionName.localizedCaseInsensitiveContains("Non Permanent Resident Alien")}).first{
            self.getAllVisaStatus(residencyTypeId: citizenShip.optionId, isForAddUpdate: true)
        }
        
    }
    
    func changeCitizenshipStatus(){
        if (citizenshipStatus == 1){
            btnUSCitizen.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblUSCitizen.font = Theme.getRubikSemiBoldFont(size: 14)
            lblUSCitizen.textColor = Theme.getAppBlackColor()
            btnPermanentResident.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblPermanentResident.font = Theme.getRubikRegularFont(size: 14)
            lblPermanentResident.textColor = Theme.getAppGreyColor()
            btnNonPermanent.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblNonPermanent.font = Theme.getRubikRegularFont(size: 14)
            lblNonPermanent.textColor = Theme.getAppGreyColor()
            usCitizenStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            permanentResidentStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            nonPermanentStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            usCitizenStackView.dropShadowToCollectionViewCell()
            permanentResidentStackView.removeShadow()
            nonPermanentStackView.removeShadow()
            nonPermanentResidentMainView.isHidden = true
            citizenshipViewHeightConstraint.constant = 210
            nonPermanentStackViewHeightConstraint.constant = 48
        }
        else if (citizenshipStatus == 2){
            btnPermanentResident.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblPermanentResident.font = Theme.getRubikSemiBoldFont(size: 14)
            lblPermanentResident.textColor = Theme.getAppBlackColor()
            btnUSCitizen.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblUSCitizen.font = Theme.getRubikRegularFont(size: 14)
            lblUSCitizen.textColor = Theme.getAppGreyColor()
            btnNonPermanent.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblNonPermanent.font = Theme.getRubikRegularFont(size: 14)
            lblNonPermanent.textColor = Theme.getAppGreyColor()
            permanentResidentStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            usCitizenStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            nonPermanentStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            permanentResidentStackView.dropShadowToCollectionViewCell()
            usCitizenStackView.removeShadow()
            nonPermanentStackView.removeShadow()
            nonPermanentResidentMainView.isHidden = true
            citizenshipViewHeightConstraint.constant = 210
            nonPermanentStackViewHeightConstraint.constant = 48
        }
        else if (citizenshipStatus == 3){
            btnNonPermanent.setImage(UIImage(named: "RadioButtonSelected"), for: .normal)
            lblNonPermanent.font = Theme.getRubikSemiBoldFont(size: 14)
            lblNonPermanent.textColor = Theme.getAppBlackColor()
            btnUSCitizen.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblUSCitizen.font = Theme.getRubikRegularFont(size: 14)
            lblUSCitizen.textColor = Theme.getAppGreyColor()
            btnPermanentResident.setImage(UIImage(named: "radioUnslected"), for: .normal)
            lblPermanentResident.font = Theme.getRubikRegularFont(size: 14)
            lblPermanentResident.textColor = Theme.getAppGreyColor()
            nonPermanentStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            permanentResidentStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            usCitizenStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
            nonPermanentStackView.dropShadowToCollectionViewCell()
            permanentResidentStackView.removeShadow()
            usCitizenStackView.removeShadow()
            nonPermanentResidentMainView.isHidden = false
            citizenshipViewHeightConstraint.constant = 296
            nonPermanentStackViewHeightConstraint.constant = 134
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
        vc.delegate = self
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
        lblActiveDuty.font = isActiveDutyPersonal ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblActiveDuty.textColor = isActiveDutyPersonal ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        changeMilitaryStatus()
        if (isActiveDutyPersonal){
            let vc = Utility.getActiveDutyPersonnelFollowUpQuestionVC()
            vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
            if let activeDuty = self.borrowerInformationModel.militaryServiceDetails.details.filter({$0.militaryAffiliationId == self.militaryAffiliationArray.filter({$0.optionName == "Active Duty Personnel"}).first!.optionId}).first{
                vc.selectedMilitary = activeDuty
            }
            vc.delegate = self
            self.presentVC(vc: vc)
            activeDutyStackView.dropShadowToCollectionViewCell()
        }
        else{
            activeDutyStackView.removeShadow()
        }
    }
    
    @objc func lastDateOfServiceMainViewTapped(){
        let vc = Utility.getActiveDutyPersonnelFollowUpQuestionVC()
        vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
        if let activeDuty = self.borrowerInformationModel.militaryServiceDetails.details.filter({$0.militaryAffiliationId == self.militaryAffiliationArray.filter({$0.optionName == "Active Duty Personnel"}).first!.optionId}).first{
            vc.selectedMilitary = activeDuty
        }
        vc.delegate = self
        self.presentVC(vc: vc)
    }
    
    @objc func reserveNationalGuardTapped(){
        isReserveOrNationalCard = !isReserveOrNationalCard
        btnReserveNationalGuard.setImage(UIImage(named: isReserveOrNationalCard ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblReserveNationalGuard.font = isReserveOrNationalCard ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblReserveNationalGuard.textColor = isReserveOrNationalCard ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        changeMilitaryStatus()
        if (isReserveOrNationalCard){
            let vc = Utility.getReserveFollowUpQuestionsVC()
            vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
            vc.delegate = self
            if let reserveNationalGuard = self.borrowerInformationModel.militaryServiceDetails.details.filter({$0.militaryAffiliationId == self.militaryAffiliationArray.filter({$0.optionName == "Reserve Or National Guard"}).first!.optionId}).first{
                vc.selectedMilitary = reserveNationalGuard
            }
            self.presentVC(vc: vc)
            reserveNationalGuardStackView.dropShadowToCollectionViewCell()
        }
        else{
            reserveNationalGuardStackView.removeShadow()
        }
    }
    
    @objc func reserveOrNationalGuardMainViewTapped(){
        let vc = Utility.getReserveFollowUpQuestionsVC()
        vc.borrowerName = "\(self.borrowerInformationModel.borrowerBasicDetails.firstName) \(self.borrowerInformationModel.borrowerBasicDetails.lastName)"
        vc.delegate = self
        if let reserveNationalGuard = self.borrowerInformationModel.militaryServiceDetails.details.filter({$0.militaryAffiliationId == self.militaryAffiliationArray.filter({$0.optionName == "Reserve Or National Guard"}).first!.optionId}).first{
            vc.selectedMilitary = reserveNationalGuard
        }
        self.presentVC(vc: vc)
    }
    
    @objc func veteranTapped(){
        isVeteran = !isVeteran
        btnVeteran.setImage(UIImage(named: isVeteran ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblVeteran.font = isVeteran ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblVeteran.textColor = isVeteran ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        if (isVeteran){
            veteranStackView.dropShadowToCollectionViewCell()
        }
        else{
            veteranStackView.removeShadow()
        }
        changeMilitaryStatus()
    }
    
    @objc func survivingSpouseTapped(){
        isSurvivingSpouse = !isSurvivingSpouse
        btnSurvivingSpouse.setImage(UIImage(named: isSurvivingSpouse ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
        lblSurvingSpouse.font = isSurvivingSpouse ? Theme.getRubikSemiBoldFont(size: 14) : Theme.getRubikRegularFont(size: 14)
        lblSurvingSpouse.textColor = isSurvivingSpouse ? Theme.getAppBlackColor() : Theme.getAppGreyColor()
        if (isSurvivingSpouse){
            survivingSpouseStackView.dropShadowToCollectionViewCell()
        }
        else{
            survivingSpouseStackView.removeShadow()
        }
        changeMilitaryStatus()
    }
    
    func changeMilitaryStatus(){
        if (isActiveDutyPersonal && isReserveOrNationalCard){ //Both are selected
            lastDateOfServiceMainView.isHidden = false
            reserveOrNationalGuardMainView.isHidden = false
            militaryViewHeightConstraint.constant = 500
            activeDutyStackViewHeightConstraint.constant = 136
            reserveNationalGuardStackViewHeightConstraint.constant = 150
        }
        else if (isActiveDutyPersonal && !isReserveOrNationalCard){ // Active duty is selected but reserve national is not selected
            lastDateOfServiceMainView.isHidden = false
            reserveOrNationalGuardMainView.isHidden = true
            militaryViewHeightConstraint.constant = 370
            activeDutyStackViewHeightConstraint.constant = 136
            reserveNationalGuardStackViewHeightConstraint.constant = 48
        }
        else if (isReserveOrNationalCard && !isActiveDutyPersonal){ // Reserve is selected but active duty is not selected
            lastDateOfServiceMainView.isHidden = true
            reserveOrNationalGuardMainView.isHidden = false
            militaryViewHeightConstraint.constant = 400
            activeDutyStackViewHeightConstraint.constant = 48
            reserveNationalGuardStackViewHeightConstraint.constant = 150
        }
        else{ // No Active duty and no reserver personal
            lastDateOfServiceMainView.isHidden = true
            reserveOrNationalGuardMainView.isHidden = true
            activeDutyStackViewHeightConstraint.constant = 48
            reserveNationalGuardStackViewHeightConstraint.constant = 48
            militaryViewHeightConstraint.constant = 260
        }
        
        
        if (isActiveDutyPersonal){
            activeDutyStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            activeDutyStackView.dropShadowToCollectionViewCell()
        }
        else{
            activeDutyStackView.removeShadow()
            activeDutyStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        }
        if (isReserveOrNationalCard){
            reserveNationalGuardStackView.dropShadowToCollectionViewCell()
            reserveNationalGuardStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        }
        else{
            reserveNationalGuardStackView.removeShadow()
            reserveNationalGuardStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        }
        if (isVeteran){
            veteranStackView.dropShadowToCollectionViewCell()
            veteranStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        }
        else{
            veteranStackView.removeShadow()
            veteranStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        }
        if (isSurvivingSpouse){
            survivingSpouseStackView.dropShadowToCollectionViewCell()
            survivingSpouseStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        }
        else{
            survivingSpouseStackView.removeShadow()
            survivingSpouseStackView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
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
            addUpdateBorrowerDetail()
//            if (txtfieldEmail.text != "" && txtfieldHomeNumber.text != ""){
//                if (txtfieldEmail.text!.isValidEmail() && txtfieldHomeNumber.text?.count == 14){
//                    self.goBack()
//                }
//            }
//            else if (txtfieldEmail.text != "" && txtfieldEmail.text!.isValidEmail() && txtfieldHomeNumber.text == ""){
//                self.goBack()
//            }
//            else if (txtfieldEmail.text == "" && txtfieldHomeNumber.text?.count == 14){
//                self.goBack()
//            }
//            else if (txtfieldEmail.text == "" && txtfieldHomeNumber.text == ""){
//                self.goBack()
//            }
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
                    self.visaStatusArray.removeAll()
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
                        vc.delegate = self
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
                    if (self.borrowerId > 0){
                        self.getBorrowerDetail()
                    }
                    else{
                        self.lblBorrowerType.text = self.borrowerInformationModel.borrowerBasicDetails.ownTypeId == 1 ? "Primary Borrower" : "Co-Borrower"
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
                    if (self.borrowerInformationModel.borrowerBasicDetails.ownTypeId != 1) && (self.borrowerInformationModel.maritalStatus.relationWithPrimaryId > 0) && (self.borrowerInformationModel.maritalStatus.spouseMaritalStatusId > 0){
                        self.borrowerInformationModel.maritalStatus.maritalStatusId = self.borrowerInformationModel.maritalStatus.spouseMaritalStatusId
                    }
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
    
    func addUpdateBorrowerDetail(){
        Utility.showOrHideLoader(shouldShow: true)
        
        let basicDetails = ["borrowerId": borrowerId == 0 ? NSNull() : borrowerId,
                            "loanApplicationId": loanApplicationId,
                            "firstName": txtfieldLegalFirstName.text!,
                            "middleName": txtfieldMiddleName.text!,
                            "lastName": txtfieldLegalLastName.text!,
                            "suffix": txtfieldSuffix.text!,
                            "emailAddress": txtfieldEmail.text!,
                            "homePhone": cleanString(string: txtfieldHomeNumber.text!, replaceCharacters: ["(", ")", " ", "-"], replaceWith: ""),
                            "workPhone": cleanString(string: txtfieldWorkNumber.text!, replaceCharacters: ["(", ")", " ", "-"], replaceWith: ""),
                            "workPhoneExt": txtfieldExtensionNumber.text!,
                            "cellPhone": cleanString(string: txtfieldCellNumber.text!, replaceCharacters: ["(", ")", " ", "-"], replaceWith: ""),
                            "ownTypeId": borrowerId == 0 ? 2 : borrowerInformationModel.borrowerBasicDetails.ownTypeId] as [String: Any]
        
        var maritalStatusId: Any = NSNull()
        var residencyTypeId: Any = NSNull()
        var dob = ""
        
        if (maritalStatus == 1){ //Unmarried
            if let selectedMaritalStatus = maritalStatusArray.filter({$0.optionName == "Unmarried"}).first{
                maritalStatusId = selectedMaritalStatus.optionId
            }
        }
        else if (maritalStatus == 2){ //married
            if let selectedMaritalStatus = maritalStatusArray.filter({$0.optionName == "Married"}).first{
                maritalStatusId = selectedMaritalStatus.optionId
            }
        }
        else if (maritalStatus == 3){ //separated
            if let selectedMaritalStatus = maritalStatusArray.filter({$0.optionName == "Separated"}).first{
                maritalStatusId = selectedMaritalStatus.optionId
            }
        }
        
        if (citizenshipStatus == 1){ //us citizen
            if let selectedCitizenship = citizenshipArray.filter({$0.optionName.localizedCaseInsensitiveContains("US Citizen")}).first{
                residencyTypeId = selectedCitizenship.optionId
            }
        }
        else if (citizenshipStatus == 2){ // permanent
            if let selectedCitizenship = citizenshipArray.filter({$0.optionName.localizedCaseInsensitiveContains("Permanent Resident Alien (Green Card)")}).first{
                residencyTypeId = selectedCitizenship.optionId
            }
        }
        else if (citizenshipStatus == 3){ // non permanent
            if let selectedCitizenship = citizenshipArray.filter({$0.optionName.localizedCaseInsensitiveContains("Non Permanent Resident Alien")}).first{
                residencyTypeId = selectedCitizenship.optionId
            }
        }
        
        let dobComponent = txtfieldDOB.text!.components(separatedBy: "/")
        if (dobComponent.count == 3){
            dob = "\(dobComponent[2])-\(dobComponent[0])-\(dobComponent[1])T00:00:00"
        }
        
        var agesArray = [Int]()
        for i in 0..<noOfDependents{
            let cell = dependentsCollectionView.cellForItem(at: IndexPath(row: i, section: 0)) as! DependentCollectionViewCell
            if let age = Int(cell.txtfieldAge.text!){
                agesArray.append(age)
            }
        }
        
        var militartyDetailsArray: [Any] = []
        
        if (isActiveDutyPersonal){
            if let selectedMilitary = militaryAffiliationArray.filter({$0.optionName.localizedCaseInsensitiveContains("Active Duty Personnel")}).first{
                var expirationDate = ""
                let expirationDateComponent = lblLastDate.text!.components(separatedBy: "/")
                if (expirationDateComponent.count == 2){
                    expirationDate = "\(expirationDateComponent[1])-\(expirationDateComponent[0])-01T00:00:00"
                }
                let detail = ["militaryAffiliationId": selectedMilitary.optionId,
                              "expirationDateUtc": expirationDate] as [String : Any]
                militartyDetailsArray.append(detail)
            }
        }
        if (isReserveOrNationalCard){
            if let selectedMilitary = militaryAffiliationArray.filter({$0.optionName.localizedCaseInsensitiveContains("Reserve Or National Guard")}).first{
                let detail = ["militaryAffiliationId": selectedMilitary.optionId,
                              "reserveEverActivated": lblReserveNationalGuardAns.text == "Yes" ? true : false] as [String : Any]
                militartyDetailsArray.append(detail)
            }
        }
        if (isVeteran){
            if let selectedMilitary = militaryAffiliationArray.filter({$0.optionName.localizedCaseInsensitiveContains("Veteran")}).first{
                let detail = ["militaryAffiliationId": selectedMilitary.optionId]
                militartyDetailsArray.append(detail)
            }
        }
        if (isSurvivingSpouse){
            if let selectedMilitary = militaryAffiliationArray.filter({$0.optionName.localizedCaseInsensitiveContains("Surviving Spouse")}).first{
                let detail = ["militaryAffiliationId": selectedMilitary.optionId]
                militartyDetailsArray.append(detail)
            }
        }
        
        var maritalStatusDetail: [String: Any] = [:]
        
        if (maritalStatus == 1){
            maritalStatusDetail = ["loanApplicationId": loanApplicationId,
                                   "borrowerId": borrowerId == 0 ? NSNull() : borrowerId,
                                   "maritalStatusId": maritalStatusId,
                                   "firstName": "",
                                   "middleName": "",
                                   "lastName": "",
                                   /*"relationWithPrimaryId": NSNull(),*/
                                   "spouseBorrowerId": NSNull(),
                                   /*"spouseMaritalStatusId": NSNull(),*/
                                   "isInRelationship": borrowerInformationModel.maritalStatus.isInRelationship,
                                   "relationFormedStateId": borrowerInformationModel.maritalStatus.isInRelationship == true ? borrowerInformationModel.maritalStatus.relationFormedStateId : NSNull(),
                                   "relationshipTypeId": borrowerInformationModel.maritalStatus.isInRelationship == true ? borrowerInformationModel.maritalStatus.relationshipTypeId : NSNull(),
                                   "otherRelationshipExplanation": borrowerInformationModel.maritalStatus.isInRelationship == true ? borrowerInformationModel.maritalStatus.otherRelationshipExplanation : NSNull(),
                                   "spouseLoanContactId": NSNull()]
        }
        else{
            maritalStatusDetail = ["loanApplicationId": loanApplicationId,
                                   "borrowerId": borrowerId == 0 ? NSNull() : borrowerId,
                                   "maritalStatusId": maritalStatusId,
                                   "firstName": borrowerInformationModel.maritalStatus.firstName,
                                   "middleName": borrowerInformationModel.maritalStatus.middleName,
                                   "lastName": borrowerInformationModel.maritalStatus.lastName,
                                   /*"relationWithPrimaryId": borrowerInformationModel.maritalStatus.relationWithPrimaryId == 0 ? NSNull() : borrowerInformationModel.maritalStatus.relationWithPrimaryId,*/
                                   "spouseBorrowerId": borrowerInformationModel.maritalStatus.spouseBorrowerId == 0 ? NSNull() : borrowerInformationModel.maritalStatus.spouseBorrowerId,
                                   /*"spouseMaritalStatusId": borrowerInformationModel.maritalStatus.spouseMaritalStatusId == 0 ? NSNull() : borrowerInformationModel.maritalStatus.spouseMaritalStatusId,*/
                                   "isInRelationship": NSNull(),
                                   "relationFormedStateId": NSNull(),
                                   "relationshipTypeId": NSNull(),
                                   "otherRelationshipExplanation": NSNull(),
                                   "spouseLoanContactId": borrowerInformationModel.maritalStatus.spouseLoanContactId == 0 ? NSNull() : borrowerInformationModel.maritalStatus.spouseLoanContactId]
        }
        
        let citizenshipDetail = ["borrowerId": borrowerId == 0 ? NSNull() : borrowerId,
                                 "loanApplicationId": loanApplicationId,
                                 "residencyTypeId": residencyTypeId,
                                 "residencyStatusId": citizenshipStatus == 3 ? selectedResidencyStatusId : NSNull(),
                                 "residencyStatusExplanation": citizenshipStatus == 3 ? selectedResidencyStatusExplanation : NSNull(),
                                 "dependentCount": noOfDependents,
                                 "dependentAges": agesArray.map{String($0)}.joined(separator: ","),
                                 "dobUtc": dob,
                                 "ssn": txtfieldSecurityNo.text!] as [String: Any]
        
        let militaryDetail = ["isVaEligible": true,
                              "details": militartyDetailsArray] as [String: Any]
        
        let addressModel = ["street": borrowerInformationModel.currentAddress.addressModel.street,
                            "unit": borrowerInformationModel.currentAddress.addressModel.unit,
                              "city": borrowerInformationModel.currentAddress.addressModel.city,
                            "stateId": borrowerInformationModel.currentAddress.addressModel.stateId == 0 ? NSNull() : borrowerInformationModel.currentAddress.addressModel.stateId,
                            "zipCode": borrowerInformationModel.currentAddress.addressModel.zipCode,
                            "countryId": borrowerInformationModel.currentAddress.addressModel.countryId == 0 ? NSNull() : borrowerInformationModel.currentAddress.addressModel.countryId,
                            "countryName": borrowerInformationModel.currentAddress.addressModel.countryName,
                            "stateName": borrowerInformationModel.currentAddress.addressModel.stateName,
                            "countyId": borrowerInformationModel.currentAddress.addressModel.countyId == 0 ? NSNull() : borrowerInformationModel.currentAddress.addressModel.countyId,
                            "countyName": borrowerInformationModel.currentAddress.addressModel.countyName] as [String: Any]
        
        let differentMailingAddressModel = ["street": borrowerInformationModel.currentAddress.mailingAddressModel.street,
                                            "unit": borrowerInformationModel.currentAddress.mailingAddressModel.unit,
                                              "city": borrowerInformationModel.currentAddress.mailingAddressModel.city,
                                            "stateId": borrowerInformationModel.currentAddress.mailingAddressModel.stateId == 0 ? NSNull() : borrowerInformationModel.currentAddress.mailingAddressModel.stateId,
                                            "zipCode": borrowerInformationModel.currentAddress.mailingAddressModel.zipCode,
                                            "countryId": borrowerInformationModel.currentAddress.mailingAddressModel.countryId == 0 ? NSNull() : borrowerInformationModel.currentAddress.mailingAddressModel.countryId,
                                            "countryName": borrowerInformationModel.currentAddress.mailingAddressModel.countryName,
                                            "stateName": borrowerInformationModel.currentAddress.mailingAddressModel.stateName,
                                            "countyId": borrowerInformationModel.currentAddress.mailingAddressModel.countyId == 0 ? NSNull() : borrowerInformationModel.currentAddress.mailingAddressModel.countyId,
                                            "countyName": borrowerInformationModel.currentAddress.mailingAddressModel.countyName] as [String: Any]
        
        let currentAddress = ["loanApplicationId": loanApplicationId,
                              "borrowerId": borrowerId == 0 ? NSNull() : borrowerId,
                              "id": borrowerInformationModel.currentAddress.id == 0 ? NSNull() : borrowerInformationModel.currentAddress.id,
                              "housingStatusId": borrowerInformationModel.currentAddress.housingStatusId,
                              "monthlyRent": borrowerInformationModel.currentAddress.monthlyRent,
                              "fromDate": borrowerInformationModel.currentAddress.fromDate,
                              "addressModel": addressModel,
                              "isMailingAddressDifferent": borrowerInformationModel.currentAddress.isMailingAddressDifferent,
                              "mailingAddressModel": borrowerInformationModel.currentAddress.isMailingAddressDifferent ? differentMailingAddressModel : NSNull()] as [String: Any]
        
        
        var previousAddresses: [Any] = []
        for address in borrowerInformationModel.previousAddresses{
            let addressDic = ["street": address.addressModel.street,
                              "unit": address.addressModel.unit,
                              "city": address.addressModel.city,
                              "stateId": address.addressModel.stateId == 0 ? NSNull() : address.addressModel.stateId,
                              "zipCode": address.addressModel.zipCode,
                              "countryId": address.addressModel.countryId  == 0 ? NSNull() : address.addressModel.countryId,
                              "countryName": address.addressModel.countryName,
                              "stateName": address.addressModel.stateName,
                              "countyId": address.addressModel.countyId == 0 ? NSNull() : address.addressModel.countyId,
                              "countyName": address.addressModel.countyName] as [String: Any]
            
            let previousAddress = ["id": address.id == 0 ? NSNull() : address.id,
                                   "housingStatusId": address.housingStatusId,
                                   "monthlyRent": address.monthlyRent,
                                   "fromDate": address.fromDate,
                                   "toDate": address.toDate,
                                   "addressModel": addressDic] as [String: Any]
            previousAddresses.append(previousAddress)
        }
        
        let params = ["loanApplicationId": loanApplicationId,
                      "borrowerId": borrowerId == 0 ? NSNull() : borrowerId,
                      "borrowerBasicDetails": basicDetails,
                      "maritalStatus": maritalStatusDetail,
                      "borrowerCitizenship": citizenshipDetail,
                      "militaryServiceDetails": militaryDetail,
                      "currentAddress": borrowerInformationModel.currentAddress.addressModel.street == "" ? NSNull() : currentAddress,
                      "previousAddresses": previousAddresses.count == 0 ? NSNull() : previousAddresses] as [String:Any]
        
        APIRouter.sharedInstance.executeAPI(type: .addUpdateBorrowerDetail, method: .post, params: params) { status, result, message in
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                if (status == .success){
                    self.goBack()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
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

extension BorrowerInformationViewController: ColabaTextFieldDelegate, UITextFieldDelegate {
    func textFieldEndEditing(_ textField: ColabaTextField) {
        _ = validate()
    }
    
    func textField(_ textField: UITextField, shouldChangeCharactersIn range: NSRange, replacementString string: String) -> Bool {
        if (textField == txtfieldExtensionNumber){
            let maxLength = 10
            let currentString: NSString = (textField.text ?? "") as NSString
            let newString: NSString =
                currentString.replacingCharacters(in: range, with: string) as NSString
            return newString.length <= maxLength
        }
        return true
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
            vc.delegate = self
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
            vc.delegate = self
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

extension BorrowerInformationViewController: UnmarriedFollowUpQuestionsViewControllerDelegate{
    func saveUnmarriedStatus(status: MaritalStatus) {
        maritalStatus = 1
        borrowerInformationModel.maritalStatus = status
        unmarriedMainView.isHidden = false
        unMarriedStackViewHeightConstraint.constant = 180
        maritalStatusViewHeightConstraint.constant = 342
        lblUnmarriedAns.text = status.isInRelationship == true ? "Yes" : "No"
        setBorrowerInformation()
    }
}

extension BorrowerInformationViewController: SpouseLinkWithBorrowerlViewControllerDelegate{
    func savePrimaryBorrowerMartialStatus(status: MaritalStatus) {
        borrowerInformationModel.maritalStatus = status
    }
}

extension BorrowerInformationViewController: SpouseBasicDetailViewControllerDelegate{
    func saveMaritalStatusMarriedOrSeparated(status: MaritalStatus) {
        borrowerInformationModel.maritalStatus = status
    }
}

extension BorrowerInformationViewController: NonPermanentResidenceFollowUpQuestionsViewControllerDelegate{
    func setResidencyStatus(citizenship: BorrowerCitizenship) {
        citizenshipStatus = 3
        selectedResidencyStatusId = citizenship.residencyStatusId
        selectedResidencyStatusExplanation = citizenship.residencyStatusExplanation
        borrowerInformationModel.borrowerCitizenship = citizenship
        setBorrowerInformation()
    }
}

extension BorrowerInformationViewController: ActiveDutyPersonnelFollowUpQuestionViewControllerDelegate{
    func saveLastDateOfService(date: String) {
        lblLastDate.text = date
    }
}

extension BorrowerInformationViewController: ReserveFollowUpQuestionsViewControllerDelegate{
    func saveReserveNationalGuard(status: String) {
        lblReserveNationalGuardAns.text = status
        if let selectedMilitary = borrowerInformationModel.militaryServiceDetails.details.filter({$0.militaryAffiliationId == 3}).first{
            selectedMilitary.reserveEverActivated = status == "Yes" ? true : false
        }

    }
}

extension BorrowerInformationViewController: AddResidenceViewControllerDelegate{
    func saveCurrentAddress(address: BorrowerAddress) {
        borrowerInformationModel.currentAddress = address
        setBorrowerInformation()
    }
}

extension BorrowerInformationViewController: AddPreviousResidenceViewControllerDelegate{
    func savePreviousAddress(address: BorrowerAddress) {
        if (borrowerInformationModel.previousAddresses.filter({$0.id == address.id})).count > 0{
            if var selectedAddress = borrowerInformationModel.previousAddresses.filter({$0.id == address.id}).first{
                selectedAddress = address
            }
        }
        else{
            borrowerInformationModel.previousAddresses.append(address)
        }
        setBorrowerInformation()
    }
    
    func deletePreviousAddress() {
        self.getBorrowerDetail()
    }
}
