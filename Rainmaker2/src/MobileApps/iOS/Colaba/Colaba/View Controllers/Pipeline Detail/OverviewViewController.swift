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
        tableViewOverView.register(UINib(nibName: "BorrowerAddressTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressTableViewCell")
        tableViewOverView.register(UINib(nibName: "BorrowerLoanInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerLoanInfoTableViewCell")
        tableViewOverView.register(UINib(nibName: "BorrowerApplicationStatusButtonTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerApplicationStatusButtonTableViewCell")
        tableViewOverView.register(UINib(nibName: "BorrowerAddressAndLoanInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressAndLoanInfoTableViewCell")
        tableViewOverView.coverableCellsIdentifiers = ["BorrowerOverviewTableViewCell", "BorrowerApplicationStatusButtonTableViewCell", "BorrowerAddressAndLoanInfoTableViewCell"]
        getLoanApplicationInfo()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowNavigationBar), object: nil, userInfo: nil)
    }
    
    //MARK:- APIs
    
    func getLoanApplicationInfo(){
        
        self.view.isUserInteractionEnabled = false
        loadingPlaceholderView.cover(tableViewOverView, animated: true)
        
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
            
            cell.lblCoBorrowerName.text = secondaryBorrowerNames.joined(separator: ", ")
            cell.lblCoBorrowerName.isHidden = secondaryBorrowers.count == 0
            cell.lblCoBorrowerTopConstraint.constant = secondaryBorrowers.count == 0 ? 0 : 10
            cell.lblCoBorrowerHeightConstraint.constant = secondaryBorrowers.count == 0 ? 0 : 16
            cell.lblLoanNo.text = "Loan No. \(loanInfoData.loanNumber)"
            cell.lblLoanNo.isHidden = loanInfoData.loanNumber == ""
            cell.lblLoanNoTopConstraint.constant = loanInfoData.loanNumber == "" ? 0 : 15
            cell.lblLoanNoHeightConstraint.constant = loanInfoData.loanNumber == "" ? 0 : 18
            cell.iconByte.isHidden = loanInfoData.postedOn == ""
            cell.lblByte.text = loanInfoData.postedOn
            cell.lblByteTopConstraint.constant = loanInfoData.postedOn == "" ? 0 : 15
            
            cell.updateConstraintsIfNeeded()
            cell.layoutSubviews()
            
            return cell
        }
        else if (indexPath.row == 1){
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerApplicationStatusButtonTableViewCell", for: indexPath) as! BorrowerApplicationStatusButtonTableViewCell
            cell.lblApplicationStatus.text = loanInfoData.milestone
            return cell
        }
        else{
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerAddressAndLoanInfoTableViewCell", for: indexPath) as! BorrowerAddressAndLoanInfoTableViewCell
            cell.mainView.layer.cornerRadius = 6
            cell.mainView.layer.borderWidth = 1
            cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            cell.mainView.dropShadowToCollectionViewCell()
            cell.lblLoanPurpose.text = loanInfoData.loanPurpose
            cell.lblLoanType.text = "- \(loanInfoData.loanGoal)"
            cell.lblAddress.text = "\(loanInfoData.street) \(loanInfoData.unit),\n\(loanInfoData.city), \(loanInfoData.stateName) \(loanInfoData.zipCode)"
            let propertyTypeText = "\(loanInfoData.propertyType)   ·   \(loanInfoData.propertyUsage)"
            let propertyTypeAttributedText = NSMutableAttributedString(string: propertyTypeText)
            let range1 = propertyTypeText.range(of: "·")
            propertyTypeAttributedText.addAttribute(NSAttributedString.Key.font, value: Theme.getRubikBoldFont(size: 15), range: propertyTypeText.nsRange(from: range1!))
            cell.lblPropertyType.attributedText = propertyTypeAttributedText
            cell.lblPropertyValue.text = loanInfoData.propertyValue.withCommas().replacingOccurrences(of: ".00", with: "")
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
            
            cell.mainViewHeightConstraint.constant = Utility.checkIsSmallDevice() ? 278 : 253
            cell.lblLoanPurposeTopConstraint.constant = Utility.checkIsSmallDevice() ? 15 : 30
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
        if (indexPath.row == 1){
            let vc = Utility.getApplicationStatusVC()
            self.pushToVC(vc: vc)
        }
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (indexPath.row == 1){
            return 106
        }
        else if (indexPath.row == 2){
            return  Utility.checkIsSmallDevice() ? 335 : 278
        }
        else{
            return UITableView.automaticDimension
        }
        
    }
}
