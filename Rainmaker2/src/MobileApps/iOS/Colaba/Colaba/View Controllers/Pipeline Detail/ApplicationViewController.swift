//
//  ApplicationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/07/2021.
//

import UIKit
import LoadingPlaceholderView

class ApplicationViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var borrowerInfoView: UIView!
    @IBOutlet weak var borrowerCollectionView: UICollectionView!
    @IBOutlet weak var subjectPropertyView: UIView!
    @IBOutlet weak var addressView: UIView!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var lblPropertyType: UILabel!
    @IBOutlet weak var loanInfoView: UIView!
    @IBOutlet weak var LoanInfoMainView: UIView!
    @IBOutlet weak var lblLoanPayment: UILabel!
    @IBOutlet weak var lblDownPayment: UILabel!
    @IBOutlet weak var lblPercentage: UILabel!
    @IBOutlet weak var assetsAndIncomeView: UIView!
    @IBOutlet weak var assetsView: UIView!
    @IBOutlet weak var lblTotalAssets: UILabel!
    @IBOutlet weak var monthlyIncomeView: UIView!
    @IBOutlet weak var lblMonthlyIncome: UILabel!
    @IBOutlet weak var RealEstateView: UIView!
    @IBOutlet weak var realEstateCollectionView: UICollectionView!
    @IBOutlet weak var governmentQuestionsView: UIView!
    @IBOutlet weak var questionsCollectionView: UICollectionView!
    
    var loanApplicationId = 0
    var loanApplicationDetail = LoanApplicationModel()
    let loadingPlaceholderView = LoadingPlaceholderView()
    var lastContentOffset: CGFloat = 0
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
        getLoanApplicationDetail()
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowNavigationBar), object: nil, userInfo: nil)
    }
    
    //MARK:- Methods and Actions
    
    func setupViews(){
        borrowerCollectionView.register(UINib(nibName: "BorrowerInfoCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "BorrowerInfoCollectionViewCell")
        realEstateCollectionView.register(UINib(nibName: "RealEstateCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "RealEstateCollectionViewCell")
        questionsCollectionView.register(UINib(nibName: "GovernmentQuestionsCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "GovernmentQuestionsCollectionViewCell")
        questionsCollectionView.register(UINib(nibName: "DemographicInformationCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "DemographicInformationCollectionViewCell")
        addressView.layer.cornerRadius = 6
        addressView.layer.borderWidth = 1
        addressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addressView.dropShadowToCollectionViewCell()
        LoanInfoMainView.layer.cornerRadius = 6
        LoanInfoMainView.layer.borderWidth = 1
        LoanInfoMainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        LoanInfoMainView.dropShadowToCollectionViewCell()
        assetsView.layer.cornerRadius = 6
        assetsView.layer.borderWidth = 1
        assetsView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        assetsView.dropShadowToCollectionViewCell()
        monthlyIncomeView.layer.cornerRadius = 6
        monthlyIncomeView.layer.borderWidth = 1
        monthlyIncomeView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        monthlyIncomeView.dropShadowToCollectionViewCell()
        mainScrollView.delegate = self
    }
    
    func setApplicationData(){
        self.lblAddress.text = "\(loanApplicationDetail.street) \(loanApplicationDetail.unit)\n\(loanApplicationDetail.city) \(loanApplicationDetail.stateName) \(loanApplicationDetail.zipCode) \(loanApplicationDetail.countryName)"
        let propertyTypeText = "\(loanApplicationDetail.propertyTypeName)   ·   \(loanApplicationDetail.propertyUsageName)"
        let propertyTypeAttributedText = NSMutableAttributedString(string: propertyTypeText)
        let range1 = propertyTypeText.range(of: "·")
        propertyTypeAttributedText.addAttribute(NSAttributedString.Key.font, value: Theme.getRubikBoldFont(size: 15), range: propertyTypeText.nsRange(from: range1!))
        self.lblPropertyType.attributedText = propertyTypeAttributedText
        self.lblLoanPayment.text = loanApplicationDetail.loanAmount.withCommas().replacingOccurrences(of: ".00", with: "")
        self.lblDownPayment.text = loanApplicationDetail.downPayment.withCommas().replacingOccurrences(of: ".00", with: "")
        self.lblPercentage.text = String(format: "(%.0f%%)", loanApplicationDetail.downPaymentPercentage.rounded())
        let totalAssets = Int(loanApplicationDetail.totalAsset)
        let totalIncome = Int(loanApplicationDetail.totalMonthyIncome)
        self.lblTotalAssets.text = totalAssets.withCommas().replacingOccurrences(of: ".00", with: "")
        self.lblMonthlyIncome.text = totalIncome.withCommas().replacingOccurrences(of: ".00", with: "")
        self.borrowerCollectionView.reloadData()
        self.realEstateCollectionView.reloadData()
        self.questionsCollectionView.reloadData()
        
        let borrowerInfoCollectionViewLayout = UICollectionViewFlowLayout()
        borrowerInfoCollectionViewLayout.scrollDirection = .horizontal
        borrowerInfoCollectionViewLayout.sectionInset = UIEdgeInsets(top: 12, left: 20, bottom: 2, right: -80)
        borrowerInfoCollectionViewLayout.minimumLineSpacing = 10
        let itemWidth = UIScreen.main.bounds.width * 0.75
        borrowerInfoCollectionViewLayout.itemSize = CGSize(width: itemWidth, height: 80)
        self.borrowerCollectionView.collectionViewLayout = borrowerInfoCollectionViewLayout
        
        let realEstateCollectionViewLayout = UICollectionViewFlowLayout()
        realEstateCollectionViewLayout.scrollDirection = .horizontal
        realEstateCollectionViewLayout.sectionInset = UIEdgeInsets(top: 12, left: 20, bottom: 2, right: -80)
        realEstateCollectionViewLayout.minimumLineSpacing = 10
        realEstateCollectionViewLayout.itemSize = CGSize(width: itemWidth, height: 107)
        self.realEstateCollectionView.collectionViewLayout = realEstateCollectionViewLayout
        
        let questionsCollectionViewLayout = UICollectionViewFlowLayout()
        questionsCollectionViewLayout.scrollDirection = .horizontal
        questionsCollectionViewLayout.sectionInset = UIEdgeInsets(top: 0, left: 20, bottom: 0, right: 20)
        questionsCollectionViewLayout.minimumLineSpacing = 10
        questionsCollectionViewLayout.itemSize = CGSize(width: itemWidth, height: 212)
        self.questionsCollectionView.collectionViewLayout = questionsCollectionViewLayout
        
//        self.borrowerCollectionView.scrollToItem(at: IndexPath(row: 0, section: 0), at: .left, animated: true)
//        self.realEstateCollectionView.scrollToItem(at: IndexPath(row: 0, section: 0), at: .left, animated: true)
//        if (loanApplicationDetail.governmentQuestions.count > 0){
//            self.questionsCollectionView.scrollToItem(at: IndexPath(row: 0, section: 0), at: .left, animated: true)
//        }
        
    }
    
    //MARK:- API's
    
    func getLoanApplicationDetail(){
        
        loadingPlaceholderView.cover(self.view, animated: true)
        let extraData = "loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getLoanApplicationData, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                self.loadingPlaceholderView.uncover(animated: true)
                
                if (status == .success){
                    self.mainView.isHidden = false
                    let loanApplicationModel = LoanApplicationModel()
                    loanApplicationModel.updateModelWithJSON(json: result["data"])
                    self.loanApplicationDetail = loanApplicationModel
                    self.setApplicationData()
                }
                else{
                    self.mainView.isHidden = true
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
    
}

extension ApplicationViewController: UICollectionViewDataSource, UICollectionViewDelegate, UIScrollViewDelegate{
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        
        if (collectionView == borrowerCollectionView){
            return loanApplicationDetail.borrowersInformation.count + 1
        }
        else if (collectionView == realEstateCollectionView){
            return loanApplicationDetail.realEstatesOwned.count + 1
        }
        else{
            return loanApplicationDetail.governmentQuestions.count + 1
        }
        
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        
        if (collectionView == borrowerCollectionView){
            let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "BorrowerInfoCollectionViewCell", for: indexPath) as! BorrowerInfoCollectionViewCell
            
            if (indexPath.row != loanApplicationDetail.borrowersInformation.count){
                let borrower = loanApplicationDetail.borrowersInformation[indexPath.row]
                cell.lblBorrowerName.text = "\(borrower.firstName) \(borrower.lastName)"
                cell.lblBorrowerType.text = borrower.ownTypeId == 1 ? "Primary Borrower" : "Co-Borrower"
                cell.mainView.isHidden = false
                cell.addMoreView.isHidden = true
            }
            else{
                cell.mainView.isHidden = true
                cell.addMoreView.isHidden = false
            }
            cell.mainView.layer.cornerRadius = 6
            cell.mainView.layer.borderWidth = 1
            cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            cell.mainView.dropShadowToCollectionViewCell(shadowColor: UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.12).cgColor, shadowRadius: 2)
            cell.addMoreView.layer.cornerRadius = 6
            cell.addMoreView.layer.borderWidth = 1
            cell.addMoreView.layer.borderColor = Theme.getAppGreyColor().withAlphaComponent(0.3).cgColor
            cell.addMoreView.backgroundColor = .clear
            
            return cell
        }
        else if (collectionView == realEstateCollectionView){
            let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "RealEstateCollectionViewCell", for: indexPath) as! RealEstateCollectionViewCell
            
            if (indexPath.row != loanApplicationDetail.realEstatesOwned.count){
                let realEstate = loanApplicationDetail.realEstatesOwned[indexPath.row]
                cell.lblAddress.text = "\(realEstate.street) \(realEstate.unit)\n\(realEstate.city) \(realEstate.stateName) \(realEstate.zipCode) \(realEstate.countryName)"
                cell.lblPropertyType.text = realEstate.propertyTypeName
                cell.mainView.isHidden = false
                cell.addMoreView.isHidden = true
            }
            else{
                cell.mainView.isHidden = true
                cell.addMoreView.isHidden = false
            }
            
            
            cell.mainView.layer.cornerRadius = 6
            cell.mainView.layer.borderWidth = 1
            cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            cell.mainView.dropShadowToCollectionViewCell(shadowColor: UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.12).cgColor, shadowRadius: 2)
            cell.addMoreView.layer.cornerRadius = 6
            cell.addMoreView.layer.borderWidth = 1
            cell.addMoreView.layer.borderColor = Theme.getAppGreyColor().withAlphaComponent(0.3).cgColor
            cell.addMoreView.backgroundColor = .clear
            
            return cell
        }
        else{
            
            if (indexPath.row != loanApplicationDetail.governmentQuestions.count){
                let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "GovernmentQuestionsCollectionViewCell", for: indexPath) as! GovernmentQuestionsCollectionViewCell //Main View Height Constraint 90 for 0, 120 for 1, 147 for 2, 174 for 3, 202 for 4
                let question = loanApplicationDetail.governmentQuestions[indexPath.row]
                
                cell.mainView.layer.cornerRadius = 6
                cell.mainView.layer.borderWidth = 1
                cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
                cell.lblQuestionHeading.text = question.questionHeader
                cell.lblQuestion.text = question.questionText
                
                if (question.questionResponses.count == 0){
                    cell.mainViewHeightConstraint.constant = 90
                    
                    cell.iconAns1.isHidden = true
                    cell.lblAns1.isHidden = true
                    cell.lblUser1.isHidden = true
                    
                    cell.iconAns2.isHidden = true
                    cell.lblAns2.isHidden = true
                    cell.lblUser2.isHidden = true
                    
                    cell.iconAns3.isHidden = true
                    cell.lblAns3.isHidden = true
                    cell.lblUser3.isHidden = true
                    
                    cell.iconAns4.isHidden = true
                    cell.lblAns4.isHidden = true
                    cell.lblUser4.isHidden = true
                }
                else if (question.questionResponses.count == 1){
                    cell.mainViewHeightConstraint.constant = 120
                    
                    let questionResponse1 = question.questionResponses[0]
                    
                    cell.iconAns1.isHidden = false
                    cell.lblAns1.isHidden = false
                    cell.lblUser1.isHidden = false
                    
                    if questionResponse1.questionResponseText == "Yes"{
                        cell.iconAns1.image = UIImage(named: "Ans-Yes")
                        cell.lblAns1.text = "Yes"
                        cell.lblAns1.font = Theme.getRubikMediumFont(size: 15)
                    }
                    else if (questionResponse1.questionResponseText == "No"){
                        cell.iconAns1.image = UIImage(named: "Ans-No")
                        cell.lblAns1.text = "No"
                        cell.lblAns1.font = Theme.getRubikRegularFont(size: 15)
                    }
                    else{
                        cell.iconAns1.image = UIImage(named: "Ans-NA")
                        cell.lblAns1.text = "N/a"
                        cell.lblAns1.font = Theme.getRubikRegularFont(size: 15)
                    }
                    cell.lblUser1.text = " - \(questionResponse1.borrowerFirstName)"
                    
                    cell.iconAns2.isHidden = true
                    cell.lblAns2.isHidden = true
                    cell.lblUser2.isHidden = true
                    
                    cell.iconAns3.isHidden = true
                    cell.lblAns3.isHidden = true
                    cell.lblUser3.isHidden = true
                    
                    cell.iconAns4.isHidden = true
                    cell.lblAns4.isHidden = true
                    cell.lblUser4.isHidden = true
                }
                else if (question.questionResponses.count == 2){
                    cell.mainViewHeightConstraint.constant = 147
                    
                    let questionResponse1 = question.questionResponses[0]
                    let questionResponse2 = question.questionResponses[1]
                    
                    cell.iconAns1.isHidden = false
                    cell.lblAns1.isHidden = false
                    cell.lblUser1.isHidden = false
                    
                    if questionResponse1.questionResponseText == "Yes"{
                        cell.iconAns1.image = UIImage(named: "Ans-Yes")
                        cell.lblAns1.text = "Yes"
                        cell.lblAns1.font = Theme.getRubikMediumFont(size: 15)
                        
                    }
                    else if (questionResponse1.questionResponseText == "No"){
                        cell.iconAns1.image = UIImage(named: "Ans-No")
                        cell.lblAns1.text = "No"
                        cell.lblAns1.font = Theme.getRubikRegularFont(size: 15)
                    }
                    else{
                        cell.iconAns1.image = UIImage(named: "Ans-NA")
                        cell.lblAns1.text = "N/a"
                        cell.lblAns1.font = Theme.getRubikRegularFont(size: 15)
                    }
                    cell.lblUser1.text = " - \(questionResponse1.borrowerFirstName)"
                    
                    cell.iconAns2.isHidden = false
                    cell.lblAns2.isHidden = false
                    cell.lblUser2.isHidden = false
                    
                    if questionResponse2.questionResponseText == "Yes"{
                        cell.iconAns2.image = UIImage(named: "Ans-Yes")
                        cell.lblAns2.text = "Yes"
                        cell.lblAns2.font = Theme.getRubikMediumFont(size: 15)
                    }
                    else if (questionResponse2.questionResponseText == "No"){
                        cell.iconAns2.image = UIImage(named: "Ans-No")
                        cell.lblAns2.text = "No"
                        cell.lblAns2.font = Theme.getRubikRegularFont(size: 15)
                    }
                    else{
                        cell.iconAns2.image = UIImage(named: "Ans-NA")
                        cell.lblAns2.text = "N/a"
                        cell.lblAns2.font = Theme.getRubikRegularFont(size: 15)
                    }
                    cell.lblUser2.text = " - \(questionResponse2.borrowerFirstName)"
                    
                    cell.iconAns3.isHidden = true
                    cell.lblAns3.isHidden = true
                    cell.lblUser3.isHidden = true
                    
                    cell.iconAns4.isHidden = true
                    cell.lblAns4.isHidden = true
                    cell.lblUser4.isHidden = true
                    
                }
                else if (question.questionResponses.count == 3){
                    cell.mainViewHeightConstraint.constant = 174
                    
                    let questionResponse1 = question.questionResponses[0]
                    let questionResponse2 = question.questionResponses[1]
                    let questionResponse3 = question.questionResponses[2]
                    
                    cell.iconAns1.isHidden = false
                    cell.lblAns1.isHidden = false
                    cell.lblUser1.isHidden = false
                    
                    if questionResponse1.questionResponseText == "Yes"{
                        cell.iconAns1.image = UIImage(named: "Ans-Yes")
                        cell.lblAns1.text = "Yes"
                        cell.lblAns1.font = Theme.getRubikMediumFont(size: 15)
                    }
                    else if (questionResponse1.questionResponseText == "No"){
                        cell.iconAns1.image = UIImage(named: "Ans-No")
                        cell.lblAns1.text = "No"
                        cell.lblAns1.font = Theme.getRubikRegularFont(size: 15)
                    }
                    else{
                        cell.iconAns1.image = UIImage(named: "Ans-NA")
                        cell.lblAns1.text = "N/a"
                        cell.lblAns1.font = Theme.getRubikRegularFont(size: 15)
                    }
                    cell.lblUser1.text = " - \(questionResponse1.borrowerFirstName)"
                    
                    cell.iconAns2.isHidden = false
                    cell.lblAns2.isHidden = false
                    cell.lblUser2.isHidden = false
                    
                    if questionResponse2.questionResponseText == "Yes"{
                        cell.iconAns2.image = UIImage(named: "Ans-Yes")
                        cell.lblAns2.text = "Yes"
                        cell.lblAns2.font = Theme.getRubikMediumFont(size: 15)
                    }
                    else if (questionResponse2.questionResponseText == "No"){
                        cell.iconAns2.image = UIImage(named: "Ans-No")
                        cell.lblAns2.text = "No"
                        cell.lblAns2.font = Theme.getRubikRegularFont(size: 15)
                    }
                    else{
                        cell.iconAns2.image = UIImage(named: "Ans-NA")
                        cell.lblAns2.text = "N/a"
                        cell.lblAns2.font = Theme.getRubikRegularFont(size: 15)
                    }
                    cell.lblUser2.text = " - \(questionResponse2.borrowerFirstName)"
                    
                    cell.iconAns3.isHidden = false
                    cell.lblAns3.isHidden = false
                    cell.lblUser3.isHidden = false
                    
                    if questionResponse3.questionResponseText == "Yes"{
                        cell.iconAns3.image = UIImage(named: "Ans-Yes")
                        cell.lblAns3.text = "Yes"
                        cell.lblAns3.font = Theme.getRubikMediumFont(size: 15)
                    }
                    else if (questionResponse3.questionResponseText == "No"){
                        cell.iconAns3.image = UIImage(named: "Ans-No")
                        cell.lblAns3.text = "No"
                        cell.lblAns3.font = Theme.getRubikRegularFont(size: 15)
                    }
                    else{
                        cell.iconAns3.image = UIImage(named: "Ans-NA")
                        cell.lblAns3.text = "N/a"
                        cell.lblAns3.font = Theme.getRubikRegularFont(size: 15)
                    }
                    cell.lblUser3.text = " - \(questionResponse3.borrowerFirstName)"
                    
                    cell.iconAns4.isHidden = true
                    cell.lblAns4.isHidden = true
                    cell.lblUser4.isHidden = true
                }
                else{
                    cell.mainViewHeightConstraint.constant = 202
                    
                    let questionResponse1 = question.questionResponses[0]
                    let questionResponse2 = question.questionResponses[1]
                    let questionResponse3 = question.questionResponses[2]
                    let questionResponse4 = question.questionResponses[3]
                    
                    cell.iconAns1.isHidden = false
                    cell.lblAns1.isHidden = false
                    cell.lblUser1.isHidden = false
                    
                    if questionResponse1.questionResponseText == "Yes"{
                        cell.iconAns1.image = UIImage(named: "Ans-Yes")
                        cell.lblAns1.text = "Yes"
                        cell.lblAns1.font = Theme.getRubikMediumFont(size: 15)
                    }
                    else if (questionResponse1.questionResponseText == "No"){
                        cell.iconAns1.image = UIImage(named: "Ans-No")
                        cell.lblAns1.text = "No"
                        cell.lblAns1.font = Theme.getRubikRegularFont(size: 15)
                    }
                    else{
                        cell.iconAns1.image = UIImage(named: "Ans-NA")
                        cell.lblAns1.text = "N/a"
                        cell.lblAns1.font = Theme.getRubikRegularFont(size: 15)
                    }
                    cell.lblUser1.text = " - \(questionResponse1.borrowerFirstName)"
                    
                    cell.iconAns2.isHidden = false
                    cell.lblAns2.isHidden = false
                    cell.lblUser2.isHidden = false
                    
                    if questionResponse2.questionResponseText == "Yes"{
                        cell.iconAns2.image = UIImage(named: "Ans-Yes")
                        cell.lblAns2.text = "Yes"
                        cell.lblAns2.font = Theme.getRubikMediumFont(size: 15)
                    }
                    else if (questionResponse2.questionResponseText == "No"){
                        cell.iconAns2.image = UIImage(named: "Ans-No")
                        cell.lblAns2.text = "No"
                        cell.lblAns2.font = Theme.getRubikRegularFont(size: 15)
                    }
                    else{
                        cell.iconAns2.image = UIImage(named: "Ans-NA")
                        cell.lblAns2.text = "N/a"
                        cell.lblAns2.font = Theme.getRubikRegularFont(size: 15)
                    }
                    cell.lblUser2.text = " - \(questionResponse2.borrowerFirstName)"
                    
                    cell.iconAns3.isHidden = false
                    cell.lblAns3.isHidden = false
                    cell.lblUser3.isHidden = false
                    
                    if questionResponse3.questionResponseText == "Yes"{
                        cell.iconAns3.image = UIImage(named: "Ans-Yes")
                        cell.lblAns3.text = "Yes"
                        cell.lblAns3.font = Theme.getRubikMediumFont(size: 15)
                    }
                    else if (questionResponse3.questionResponseText == "No"){
                        cell.iconAns3.image = UIImage(named: "Ans-No")
                        cell.lblAns3.text = "No"
                        cell.lblAns3.font = Theme.getRubikRegularFont(size: 15)
                    }
                    else{
                        cell.iconAns3.image = UIImage(named: "Ans-NA")
                        cell.lblAns3.text = "N/a"
                        cell.lblAns3.font = Theme.getRubikRegularFont(size: 15)
                    }
                    cell.lblUser3.text = " - \(questionResponse3.borrowerFirstName)"
                    
                    cell.iconAns4.isHidden = false
                    cell.lblAns4.isHidden = false
                    cell.lblUser4.isHidden = false
                    
                    if questionResponse4.questionResponseText == "Yes"{
                        cell.iconAns4.image = UIImage(named: "Ans-Yes")
                        cell.lblAns4.text = "Yes"
                        cell.lblAns4.font = Theme.getRubikMediumFont(size: 15)
                    }
                    else if (questionResponse4.questionResponseText == "No"){
                        cell.iconAns4.image = UIImage(named: "Ans-No")
                        cell.lblAns4.text = "No"
                        cell.lblAns4.font = Theme.getRubikRegularFont(size: 15)
                    }
                    else{
                        cell.iconAns4.image = UIImage(named: "Ans-NA")
                        cell.lblAns4.text = "N/a"
                        cell.lblAns4.font = Theme.getRubikRegularFont(size: 15)
                    }
                    cell.lblUser4.text = " - \(questionResponse4.borrowerFirstName)"
                }
                cell.mainView.dropShadowToCollectionViewCell(shadowColor: UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.12).cgColor, shadowRadius: 2)
                cell.updateConstraintsIfNeeded()
                cell.layoutSubviews()
                return cell
            }
            else{
                let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "DemographicInformationCollectionViewCell", for: indexPath) as! DemographicInformationCollectionViewCell // Height of main view 90, 123, 147, 171, 195
                cell.mainView.layer.cornerRadius = 6
                cell.mainView.layer.borderWidth = 1
                cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
                
                if (loanApplicationDetail.borrowersInformation.count == 1){
                    cell.mainViewHeightConstraint.constant = 123
                    
                    cell.lblUser1.isHidden = false
                    cell.lblAns1.isHidden = false
                    let borrower1 = loanApplicationDetail.borrowersInformation[0]
                    cell.lblUser1.text = "\(borrower1.firstName) - "
                    var userDetail = ""
                    let racesNames = borrower1.races.map{$0.raceNameAndDetail}
                    let ethnicityNames = borrower1.ethnicities.map{$0.ethnicityNameAndDetail}
                    userDetail = "\(racesNames.joined(separator: "/")) \(racesNames.count == 0 ? "" : "-") \(ethnicityNames.joined(separator: "/")) \(ethnicityNames.count == 0 ? "" : "-") \(borrower1.genderName)"
                    cell.lblAns1.text = userDetail
                    
                    cell.lblUser2.isHidden = true
                    cell.lblAns2.isHidden = true
                    
                    cell.lblUser3.isHidden = true
                    cell.lblAns3.isHidden = true
                    
                    cell.lblUser4.isHidden = true
                    cell.lblAns4.isHidden = true
                    
                }
                else if (loanApplicationDetail.borrowersInformation.count == 2){
                    cell.mainViewHeightConstraint.constant = 147
                    
                    cell.lblUser1.isHidden = false
                    cell.lblAns1.isHidden = false
                    let borrower1 = loanApplicationDetail.borrowersInformation[0]
                    cell.lblUser1.text = "\(borrower1.firstName) - "
                    var userDetail = ""
                    let racesNames = borrower1.races.map{$0.raceNameAndDetail}
                    let ethnicityNames = borrower1.ethnicities.map{$0.ethnicityNameAndDetail}
                    userDetail = "\(racesNames.joined(separator: "/")) \(racesNames.count == 0 ? "" : "-") \(ethnicityNames.joined(separator: "/")) \(ethnicityNames.count == 0 ? "" : "-") \(borrower1.genderName)"
                    cell.lblAns1.text = userDetail
                    
                    cell.lblUser2.isHidden = false
                    cell.lblAns2.isHidden = false
                    let borrower2 = loanApplicationDetail.borrowersInformation[1]
                    cell.lblUser2.text = "\(borrower2.firstName) - "
                    var userDetail2 = ""
                    let racesNames2 = borrower2.races.map{$0.raceNameAndDetail}
                    let ethnicityNames2 = borrower2.ethnicities.map{$0.ethnicityNameAndDetail}
                    userDetail2 = "\(racesNames2.joined(separator: "/")) \(racesNames2.count == 0 ? "" : "-") \(ethnicityNames2.joined(separator: "/")) \(ethnicityNames2.count == 0 ? "" : "-") \(borrower2.genderName)"
                    cell.lblAns2.text = userDetail2
                    
                    cell.lblUser3.isHidden = true
                    cell.lblAns3.isHidden = true
                    
                    cell.lblUser4.isHidden = true
                    cell.lblAns4.isHidden = true
                    
                }
                else if (loanApplicationDetail.borrowersInformation.count == 3){
                    cell.mainViewHeightConstraint.constant = 171
                    
                    cell.lblUser1.isHidden = false
                    cell.lblAns1.isHidden = false
                    let borrower1 = loanApplicationDetail.borrowersInformation[0]
                    cell.lblUser1.text = "\(borrower1.firstName) - "
                    var userDetail = ""
                    let racesNames = borrower1.races.map{$0.raceNameAndDetail}
                    let ethnicityNames = borrower1.ethnicities.map{$0.ethnicityNameAndDetail}
                    userDetail = "\(racesNames.joined(separator: "/")) \(racesNames.count == 0 ? "" : "-") \(ethnicityNames.joined(separator: "/")) \(ethnicityNames.count == 0 ? "" : "-") \(borrower1.genderName)"
                    cell.lblAns1.text = userDetail
                    
                    cell.lblUser2.isHidden = false
                    cell.lblAns2.isHidden = false
                    let borrower2 = loanApplicationDetail.borrowersInformation[1]
                    cell.lblUser2.text = "\(borrower2.firstName) - "
                    var userDetail2 = ""
                    let racesNames2 = borrower2.races.map{$0.raceNameAndDetail}
                    let ethnicityNames2 = borrower2.ethnicities.map{$0.ethnicityNameAndDetail}
                    userDetail2 = "\(racesNames2.joined(separator: "/")) \(racesNames2.count == 0 ? "" : "-") \(ethnicityNames2.joined(separator: "/")) \(ethnicityNames2.count == 0 ? "" : "-") \(borrower2.genderName)"
                    cell.lblAns2.text = userDetail2
                    
                    cell.lblUser3.isHidden = false
                    cell.lblAns3.isHidden = false
                    let borrower3 = loanApplicationDetail.borrowersInformation[2]
                    cell.lblUser3.text = "\(borrower3.firstName) - "
                    var userDetail3 = ""
                    let racesNames3 = borrower3.races.map{$0.raceNameAndDetail}
                    let ethnicityNames3 = borrower3.ethnicities.map{$0.ethnicityNameAndDetail}
                    userDetail3 = "\(racesNames3.joined(separator: "/")) \(racesNames3.count == 0 ? "" : "-") \(ethnicityNames3.joined(separator: "/")) \(ethnicityNames3.count == 0 ? "" : "-") \(borrower3.genderName)"
                    cell.lblAns3.text = userDetail3
                    
                    cell.lblUser4.isHidden = true
                    cell.lblAns4.isHidden = true
                    
                }
                else if (loanApplicationDetail.borrowersInformation.count == 4){
                    cell.mainViewHeightConstraint.constant = 195
                    
                    cell.lblUser1.isHidden = false
                    cell.lblAns1.isHidden = false
                    let borrower1 = loanApplicationDetail.borrowersInformation[0]
                    cell.lblUser1.text = "\(borrower1.firstName) - "
                    var userDetail = ""
                    let racesNames = borrower1.races.map{$0.raceNameAndDetail}
                    let ethnicityNames = borrower1.ethnicities.map{$0.ethnicityNameAndDetail}
                    userDetail = "\(racesNames.joined(separator: "/")) \(racesNames.count == 0 ? "" : "-") \(ethnicityNames.joined(separator: "/")) \(ethnicityNames.count == 0 ? "" : "-") \(borrower1.genderName)"
                    cell.lblAns1.text = userDetail
                    
                    cell.lblUser2.isHidden = false
                    cell.lblAns2.isHidden = false
                    let borrower2 = loanApplicationDetail.borrowersInformation[1]
                    cell.lblUser2.text = "\(borrower2.firstName) - "
                    var userDetail2 = ""
                    let racesNames2 = borrower2.races.map{$0.raceNameAndDetail}
                    let ethnicityNames2 = borrower2.ethnicities.map{$0.ethnicityNameAndDetail}
                    userDetail2 = "\(racesNames2.joined(separator: "/")) \(racesNames2.count == 0 ? "" : "-") \(ethnicityNames2.joined(separator: "/")) \(ethnicityNames2.count == 0 ? "" : "-") \(borrower2.genderName)"
                    cell.lblAns2.text = userDetail2
                    
                    cell.lblUser3.isHidden = false
                    cell.lblAns3.isHidden = false
                    let borrower3 = loanApplicationDetail.borrowersInformation[2]
                    cell.lblUser3.text = "\(borrower3.firstName) - "
                    var userDetail3 = ""
                    let racesNames3 = borrower3.races.map{$0.raceNameAndDetail}
                    let ethnicityNames3 = borrower3.ethnicities.map{$0.ethnicityNameAndDetail}
                    userDetail3 = "\(racesNames3.joined(separator: "/")) \(racesNames3.count == 0 ? "" : "-") \(ethnicityNames3.joined(separator: "/")) \(ethnicityNames3.count == 0 ? "" : "-") \(borrower3.genderName)"
                    cell.lblAns3.text = userDetail3
                    
                    cell.lblUser4.isHidden = false
                    cell.lblAns4.isHidden = false
                    let borrower4 = loanApplicationDetail.borrowersInformation[3]
                    cell.lblUser4.text = "\(borrower4.firstName) - "
                    var userDetail4 = ""
                    let racesNames4 = borrower4.races.map{$0.raceNameAndDetail}
                    let ethnicityNames4 = borrower4.ethnicities.map{$0.ethnicityNameAndDetail}
                    userDetail4 = "\(racesNames4.joined(separator: "/")) \(racesNames4.count == 0 ? "" : "-") \(ethnicityNames4.joined(separator: "/")) \(ethnicityNames4.count == 0 ? "" : "-") \(borrower4.genderName)"
                    cell.lblAns4.text = userDetail4
                    
                }
                
                cell.mainView.dropShadowToCollectionViewCell(shadowColor: UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.12).cgColor, shadowRadius: 2)
                cell.updateConstraintsIfNeeded()
                cell.layoutSubviews()
                return cell
            }
        }
        
    }
    
    func collectionView(_ collectionView: UICollectionView, didSelectItemAt indexPath: IndexPath) {
        if (collectionView == borrowerCollectionView){
            let vc = Utility.getBorrowerInformationVC()
            self.pushToVC(vc: vc)
        }
    }
    
    func scrollViewWillBeginDragging(_ scrollView: UIScrollView) {
        self.lastContentOffset = scrollView.contentOffset.y
    }

    func scrollViewDidScroll(_ scrollView: UIScrollView) {
        if self.lastContentOffset < scrollView.contentOffset.y {
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationHidesNavigationBar), object: nil, userInfo: nil)
        } else if self.lastContentOffset > scrollView.contentOffset.y {
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowNavigationBar), object: nil, userInfo: nil)
        } else {
            // didn't move
        }
    }
}
