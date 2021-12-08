//
//  OverviewViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/07/2021.
//

import UIKit
import LoadingPlaceholderView

protocol OverviewViewControllerDelegate: AnyObject {
    func getLoanDetailForMainPage(loanPurpose: String, email: String, phoneNumber: String)
}

class OverviewViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var tableViewOverView: UITableView!
    
    var loanApplicationId = 0
    let loadingPlaceholderView = LoadingPlaceholderView()
    var loanInfoData = LoanInfoModel()
    weak var delegate: OverviewViewControllerDelegate?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        tableViewOverView.register(UINib(nibName: "BorrowerOverviewTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerOverviewTableViewCell")
        tableViewOverView.register(UINib(nibName: "BorrowerOverviewInvitationTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerOverviewInvitationTableViewCell")
        tableViewOverView.register(UINib(nibName: "BorrowerAddressAndLoanInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressAndLoanInfoTableViewCell")
        
        tableViewOverView.coverableCellsIdentifiers = ["BorrowerOverviewTableViewCell", "BorrowerApplicationStatusButtonTableViewCell", "BorrowerAddressAndLoanInfoTableViewCell"]
//        tableViewOverView.register(UINib(nibName: "BorrowerAddressTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressTableViewCell")
//        tableViewOverView.register(UINib(nibName: "BorrowerLoanInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerLoanInfoTableViewCell")
//        tableViewOverView.register(UINib(nibName: "BorrowerApplicationStatusButtonTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerApplicationStatusButtonTableViewCell")
        
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowNavigationBar), object: nil, userInfo: nil)
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationHideRequestDocumentFooterButton), object: nil, userInfo: nil)
        getLoanApplicationInfo()
    }
    
    //MARK:- APIs
    
    func getLoanApplicationInfo(){
        
        self.view.isUserInteractionEnabled = false
        if (loanInfoData.borrowers.count == 0){
            loadingPlaceholderView.cover(tableViewOverView, animated: true)
        }
        
        let extraData = "loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getLoanInformation, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                self.loadingPlaceholderView.uncover(animated: true)
                self.view.isUserInteractionEnabled = true
                
                if (status == .success){
                    let model = LoanInfoModel()
                    model.updateModelWithJSON(json: result)
                    self.loanInfoData = model
                    self.tableViewOverView.reloadData()
                    self.delegate?.getLoanDetailForMainPage(loanPurpose: self.loanInfoData.loanPurpose, email: self.loanInfoData.email, phoneNumber: self.loanInfoData.cellPhone)
                }
                else if (status == .internetError){
                    self.tableViewOverView.reloadData()
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
                else{
                    self.tableViewOverView.reloadData()
                    self.showPopup(message: "No details found", popupState: .error, popupDuration: .custom(2)) { reason in
                        
                    }
                }
                
            }
            
        }
        
    }
    
}

extension OverviewViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 3
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        if (indexPath.row == 0){
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerOverviewTableViewCell", for: indexPath) as! BorrowerOverviewTableViewCell
            
            let primaryBorrowers = loanInfoData.borrowers.filter{$0.ownTypeId == 1}
            let secondaryBorrowers = loanInfoData.borrowers.filter{$0.ownTypeId != 1}
            
            if let primaryBorrower = primaryBorrowers.first{
                cell.lblBorrowerName.text = "\(primaryBorrower.firstName) \(primaryBorrower.lastName)"
            }
            else{
                cell.lblBorrowerName.text = ""
            }
            
            var secondaryBorrowerNames = [String]()
            secondaryBorrowerNames = secondaryBorrowers.map{"\($0.firstName) \($0.lastName)"}
            
            cell.delegate = self
            cell.lblCoBorrowerName.text = secondaryBorrowerNames.joined(separator: ", ")
            cell.lblCoBorrowerName.isHidden = secondaryBorrowers.count == 0
            cell.lblCoBorrowerTopConstraint.constant = secondaryBorrowers.count == 0 ? 0 : 10
            cell.lblCoBorrowerHeightConstraint.constant = secondaryBorrowers.count == 0 ? 0 : 16
            cell.lblLoanNo.text = "Loan No. \(loanInfoData.loanNumber)"
            cell.lblLoanNo.isHidden = loanInfoData.loanNumber == ""
            cell.iconByte.isHidden = loanInfoData.postedOn == ""
            cell.lblByte.text = loanInfoData.postedOn
            cell.lblApplicationStatus.text = loanInfoData.milestone
            if (loanInfoData.loanNumber == "" && loanInfoData.postedOn == ""){
                cell.applicationStatusViewHeightConstraint.constant = 61
                cell.applicationStatusSeparatorView.isHidden = true
            }
            else{
                cell.applicationStatusViewHeightConstraint.constant = 116
                cell.applicationStatusSeparatorView.isHidden = false
            }
            
            cell.updateConstraintsIfNeeded()
            cell.layoutSubviews()
            
            return cell
        }
        else if (indexPath.row == 1){
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerOverviewInvitationTableViewCell", for: indexPath) as! BorrowerOverviewInvitationTableViewCell
            cell.collectionView.register(UINib(nibName: "InvitationCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "InvitationCollectionViewCell")
            let layout = UICollectionViewFlowLayout()
            layout.scrollDirection = .horizontal
            layout.sectionInset = UIEdgeInsets(top: 0, left: 15, bottom: 0, right: 15)
            layout.minimumLineSpacing = 10
            let itemWidth = UIScreen.main.bounds.width * 0.7
            layout.itemSize = CGSize(width: itemWidth, height: 80)
            cell.collectionView.collectionViewLayout = layout
            cell.collectionView.dataSource = self
            cell.collectionView.delegate = self
            return cell
        }
        else{
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerAddressAndLoanInfoTableViewCell", for: indexPath) as! BorrowerAddressAndLoanInfoTableViewCell
            cell.mainView.layer.cornerRadius = 6
            cell.mainView.layer.borderWidth = 1
            cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.2).cgColor
            cell.mainView.backgroundColor = UIColor.white.withAlphaComponent(0.3)
            //cell.mainView.dropShadowToCollectionViewCell()
            
            cell.emptyStateLoanPurposeView.layer.cornerRadius = 6
            cell.emptyStateLoanPurposeView.layer.borderWidth = 1
            cell.emptyStateLoanPurposeView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            cell.emptyStateLoanPurposeView.backgroundColor = .white
            cell.emptyStateLoanPurposeView.dropShadowToCollectionViewCell()
            
            cell.mainView.isHidden = loanInfoData.street == ""
            cell.emptyStateLoanPurposeView.isHidden = loanInfoData.street != ""
            
            cell.lblLoanPurpose.text = loanInfoData.loanPurpose
            cell.lblEmptyStateLoanPurpose.text = loanInfoData.loanPurpose
            cell.lblLoanType.text = "- \(loanInfoData.loanGoal)"
            cell.lblAddress.text = "\(loanInfoData.street) \(loanInfoData.unit),\n\(loanInfoData.city), \(loanInfoData.stateName) \(loanInfoData.zipCode)"
            let propertyTypeText = "\(loanInfoData.propertyType)   ·   \(loanInfoData.propertyUsage)"
            let propertyTypeAttributedText = NSMutableAttributedString(string: propertyTypeText)
            let range1 = propertyTypeText.range(of: "·")
            propertyTypeAttributedText.addAttribute(NSAttributedString.Key.font, value: Theme.getRubikBoldFont(size: 15), range: propertyTypeText.nsRange(from: range1!))
            cell.lblPropertyType.attributedText = propertyTypeAttributedText
            cell.lblPropertyValue.text = loanInfoData.loanPurpose.capitalized
            cell.lblLoanAmount.text = loanInfoData.loanAmount.withCommas().replacingOccurrences(of: ".00", with: "")
            cell.lblDownPayment.text = loanInfoData.downPayment.withCommas().replacingOccurrences(of: ".00", with: "")
            cell.lblBottomDownPayment.text = loanInfoData.downPayment.withCommas().replacingOccurrences(of: ".00", with: "")
            
            if (loanInfoData.downPayment > 0 && loanInfoData.propertyValue > 0){
                var downPaymentPercentage:Double = Double(loanInfoData.downPayment) / Double(loanInfoData.propertyValue)
                downPaymentPercentage = downPaymentPercentage * 100
                cell.lblDownPaymentPercentage.text = String(format: "(%.0f%%)", downPaymentPercentage.rounded())
                cell.lblBottomDownPaymentPercentage.text = String(format: "(%.0f%%)", downPaymentPercentage.rounded())
            }
            else{
                cell.lblDownPaymentPercentage.text = ""
            }
            
            cell.mainViewHeightConstraint.constant = Utility.checkIsSmallDevice() ? 241 : 216
            //cell.lblLoanPurposeTopConstraint.constant = Utility.checkIsSmallDevice() ? 15 : 30
            cell.lblLoanPurposeTopConstraint.constant = 0
            cell.mapIconTopConstraint.constant = Utility.checkIsSmallDevice() ? 16 : 26
            cell.lblAddressTopConstraint.constant = Utility.checkIsSmallDevice() ? 15 : 25
            cell.loanDetailStackView.spacing = Utility.checkIsSmallDevice() ? 35 : 12
            cell.downPaymentView.isHidden = Utility.checkIsSmallDevice()
            cell.bottomDownPaymentView.isHidden = !Utility.checkIsSmallDevice()
            cell.updateConstraintsIfNeeded()
            cell.layoutSubviews()
            
            return cell
        }
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
//        if (indexPath.row == 1){
//            let vc = Utility.getApplicationStatusVC()
//            vc.loanApplicationId = self.loanApplicationId
//            self.pushToVC(vc: vc)
//        }
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (indexPath.row == 0){
            if (loanInfoData.borrowers.count > 1){
                if (loanInfoData.loanNumber == "" && loanInfoData.postedOn == ""){
                    return 170
                }
                else{
                    return 220
                }
            }
            else{
                if (loanInfoData.loanNumber == "" && loanInfoData.postedOn == ""){
                    return 142
                }
                else{
                    return 197
                }
            }
        }
        else if (indexPath.row == 1){
            return 106
        }
        else{
            return  Utility.checkIsSmallDevice() ? 355 : 320
        }
    }
}

extension OverviewViewController: UICollectionViewDataSource, UICollectionViewDelegate{
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return 2
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "InvitationCollectionViewCell", for: indexPath) as! InvitationCollectionViewCell
        cell.mainView.layer.cornerRadius = 6
        cell.mainView.layer.borderWidth = 1
        cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        cell.mainView.dropShadowToCollectionViewCell()
        cell.lblTopHeading.text = indexPath.row == 0 ? "PRIMARY BORROWER" : "DOCUMENTS"
        cell.lblType.text = indexPath.row == 0 ? "Send Invitation" : "Review Documents"
        cell.icon.image = UIImage(named: indexPath.row == 0 ? "sendInvitation" : "reviewDocument")
        return cell
    }
    
    func collectionView(_ collectionView: UICollectionView, didSelectItemAt indexPath: IndexPath) {
        if (indexPath.row == 0){
            let vc = Utility.getInvitePrimaryBorrowerVC()
            self.presentVC(vc: vc)
        }
        else if (indexPath.row == 1){
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowDocumentsTab), object: nil)
        }
    }
    
}

extension OverviewViewController: BorrowerOverviewTableViewCellDelegate{
    func applicationStatusViewTapped() {
        let vc = Utility.getApplicationStatusVC()
        vc.loanApplicationId = self.loanApplicationId
        self.pushToVC(vc: vc)
    }
}
