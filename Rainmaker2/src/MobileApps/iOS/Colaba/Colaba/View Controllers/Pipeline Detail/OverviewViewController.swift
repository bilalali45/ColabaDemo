//
//  OverviewViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/07/2021.
//

import UIKit
import LoadingPlaceholderView

class OverviewViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var tableViewOverView: UITableView!
    
    var loanApplicationId = 0
    let loadingPlaceholderView = LoadingPlaceholderView()
    var loanInfoData = LoanInfoModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        tableViewOverView.register(UINib(nibName: "BorrowerOverviewTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerOverviewTableViewCell")
        tableViewOverView.register(UINib(nibName: "BorrowerAddressTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressTableViewCell")
        tableViewOverView.register(UINib(nibName: "BorrowerLoanInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerLoanInfoTableViewCell")
        tableViewOverView.register(UINib(nibName: "BorrowerApplicationStatusButtonTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerApplicationStatusButtonTableViewCell")
        tableViewOverView.coverableCellsIdentifiers = ["BorrowerOverviewTableViewCell", "BorrowerAddressTableViewCell", "BorrowerLoanInfoTableViewCell", "BorrowerApplicationStatusButtonTableViewCell"]
        getLoanApplicationInfo()
    }
    
    //MARK:- APIs
    
    func getLoanApplicationInfo(){
        
        loadingPlaceholderView.cover(tableViewOverView, animated: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getLoanInformation, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                self.loadingPlaceholderView.uncover(animated: true)
                
                if (status == .success){
                    let model = LoanInfoModel()
                    model.updateModelWithJSON(json: result)
                    self.loanInfoData = model
                    self.tableViewOverView.reloadData()
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
        return 4
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
            cell.lblLoanNo.text = "Loan#\(loanInfoData.loanNumber)"
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
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerAddressTableViewCell", for: indexPath) as! BorrowerAddressTableViewCell
            cell.lblAddress.text = "\(loanInfoData.street) \(loanInfoData.unit) \(loanInfoData.city) \(loanInfoData.stateName) \(loanInfoData.zipCode) \(loanInfoData.countryName)"
            cell.lblPropertyValue.text = loanInfoData.propertyValue.withCommas().replacingOccurrences(of: ".00", with: "")
            cell.lblFamilyType.text = loanInfoData.propertyType
            return cell
        }
        else if (indexPath.row == 2){
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerLoanInfoTableViewCell", for: indexPath) as! BorrowerLoanInfoTableViewCell
            cell.lblLoanPurpose.text = loanInfoData.loanPurpose
            cell.lblLoanPayment.text = loanInfoData.loanAmount.withCommas().replacingOccurrences(of: ".00", with: "")
            cell.lblDownPayment.text = loanInfoData.downPayment.withCommas().replacingOccurrences(of: ".00", with: "")
            return cell
        }
        else{
            let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerApplicationStatusButtonTableViewCell", for: indexPath) as! BorrowerApplicationStatusButtonTableViewCell
            cell.lblApplicationStatus.text = loanInfoData.milestone
            return cell
        }
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        if (indexPath.row == 3){
            let vc = Utility.getApplicationStatusVC()
            self.pushToVC(vc: vc)
        }
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (indexPath.row == 2){
            return 107
        }
        else if (indexPath.row == 3){
            return 130
        }
        else{
            return UITableView.automaticDimension
        }
        
    }
}
