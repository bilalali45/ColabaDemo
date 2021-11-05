//
//  IncomeDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/09/2021.
//

import UIKit
import LoadingPlaceholderView

protocol IncomeDetailViewControllerDelegate: Any {
    func getBorrowerTotalIncome(totalIncome: String)
}

class IncomeDetailViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewEmployement: UITableView!
    @IBOutlet weak var tableViewEmployementTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewEmployementHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewSelfEmployement: UITableView!
    @IBOutlet weak var tableViewSelfEmploymentTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewSelfEmployementHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewBusiness: UITableView!
    @IBOutlet weak var tableViewBusinessTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewBusinessHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewMilitaryPay: UITableView!
    @IBOutlet weak var tableViewMilitaryTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewMilitaryPayHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewRetitrement: UITableView!
    @IBOutlet weak var tableViewRetirementTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewRetirementHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewOther: UITableView!
    @IBOutlet weak var tableViewOtherTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewOtherHeightConstraint: NSLayoutConstraint!
    
    let loadingPlaceholderView = LoadingPlaceholderView()
    
    var delegate: IncomeDetailViewControllerDelegate?
    var loanApplicationId = 0
    var borrowerId = 0
    var borrowerName = ""
    var borrowerIncomeData = BorrowerIncomeModel()
    var selectedTableView: UITableView?
    
    var employmentIncome = BorrowerIncome()
    var selfEmployementIncome = BorrowerIncome()
    var businessIncome = BorrowerIncome()
    var militaryIncome = BorrowerIncome()
    var retiermentIncome = BorrowerIncome()
    var otherIncome = BorrowerIncome()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupTableViews(tableViews: [tableViewEmployement, tableViewSelfEmployement, tableViewBusiness, tableViewMilitaryPay, tableViewRetitrement, tableViewOther])
        NotificationCenter.default.addObserver(self, selector: #selector(addCurrentEmployement), name: NSNotification.Name(rawValue: kNotificationAddCurrentEmployement), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(addPreviousEmployement), name: NSNotification.Name(rawValue: kNotificationAddPreviousEmployement), object: nil)
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2){
            self.setScreenHeight()
        }
        getIncomeDetail()
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        self.delegate?.getBorrowerTotalIncome(totalIncome: "\(Int(borrowerIncomeData.monthlyIncome).withCommas().replacingOccurrences(of: ".00", with: ""))")
    }
    
    //MARK:- Methods and Actions
    
    func setupTableViews(tableViews: [UITableView]){
        for tableView in tableViews{
            tableView.separatorStyle = .none
            tableView.backgroundColor = .white
            tableView.layer.cornerRadius = 6
            tableView.layer.borderWidth = 1
            tableView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            tableView.clipsToBounds = false
            tableView.layer.masksToBounds = false
            tableView.dropShadowToCollectionViewCell(shadowColor: UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.12).cgColor, shadowRadius: 1, shadowOpacity: 1)
            tableView.register(UINib(nibName: "AssetsHeadingTableViewCell", bundle: nil), forCellReuseIdentifier: "AssetsHeadingTableViewCell")
            tableView.register(UINib(nibName: "AssetsDetailTableViewCell", bundle: nil), forCellReuseIdentifier: "AssetsDetailTableViewCell")
            tableView.register(UINib(nibName: "AssetsAddNewTableViewCell", bundle: nil), forCellReuseIdentifier: "AssetsAddNewTableViewCell")
            tableView.coverableCellsIdentifiers = ["AssetsHeadingTableViewCell"]
        }
    }
    
    func setIncomeDetail(){
        
        self.delegate?.getBorrowerTotalIncome(totalIncome: "\(Int(borrowerIncomeData.monthlyIncome).withCommas().replacingOccurrences(of: ".00", with: ""))")
        
        if let employement = self.borrowerIncomeData.borrowerIncomes.filter({$0.incomeCategory == "Employment"
        }).first{
            self.employmentIncome = employement
        }
        if let selfEmployment = self.borrowerIncomeData.borrowerIncomes.filter({$0.incomeCategory == "Self Employment / Independent Contractor"
        }).first{
            self.selfEmployementIncome = selfEmployment
        }
        if let business = self.borrowerIncomeData.borrowerIncomes.filter({$0.incomeCategory == "Business"
        }).first{
            self.businessIncome = business
        }
        if let militaryPay = self.borrowerIncomeData.borrowerIncomes.filter({$0.incomeCategory == "Military Pay"
        }).first{
            self.militaryIncome = militaryPay
        }
        if let retirement = self.borrowerIncomeData.borrowerIncomes.filter({$0.incomeCategory == "Retirement"
        }).first{
            self.retiermentIncome = retirement
        }
        if let other = self.borrowerIncomeData.borrowerIncomes.filter({$0.incomeCategory == "Other"
        }).first{
            self.otherIncome = other
        }
        
        self.setScreenHeight()
    }
    
    func setScreenHeight(){
        
        let employmentHeight = (self.employmentIncome.incomes.count * 83)
        let employmentTableViewHeight = selectedTableView == tableViewEmployement ? (employmentHeight + 128) : 58
        
        let selfEmploymentHeight = (self.selfEmployementIncome.incomes.count * 83)
        let selfEmploymentTableViewHeight = selectedTableView == tableViewSelfEmployement ? (selfEmploymentHeight + 148) : 58
        
        let businessHeight = (self.businessIncome.incomes.count * 83)
        let businessTableViewHeight = selectedTableView == tableViewBusiness ? (businessHeight + 128) : 58
        
        let militaryHeight = (self.militaryIncome.incomes.count * 83)
        let militaryPayTableViewHeight = selectedTableView == tableViewMilitaryPay ? (militaryHeight + 128) : 58
        
        let retirementHeight = (self.retiermentIncome.incomes.count * 83)
        let retirementTableViewHeight = selectedTableView == tableViewRetitrement ? (retirementHeight + 128) : 58
        
        let otherHeight = (self.otherIncome.incomes.count * 83)
        let otherTableViewHeight = selectedTableView == tableViewOther ? (otherHeight + 128) : 58
        
        let totalHeight = employmentTableViewHeight + selfEmploymentTableViewHeight + businessTableViewHeight + militaryPayTableViewHeight + retirementTableViewHeight + otherTableViewHeight + 100
        
        tableViewEmployementHeightConstraint.constant = CGFloat(employmentTableViewHeight)
        tableViewSelfEmployementHeightConstraint.constant = CGFloat(selfEmploymentTableViewHeight)
        tableViewBusinessHeightConstraint.constant = CGFloat(businessTableViewHeight)
        tableViewMilitaryPayHeightConstraint.constant = CGFloat(militaryPayTableViewHeight)
        tableViewRetirementHeightConstraint.constant = CGFloat(retirementTableViewHeight)
        tableViewOtherHeightConstraint.constant = CGFloat(otherTableViewHeight)
        
        self.mainViewHeightConstraint.constant = CGFloat(totalHeight)
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
            self.tableViewEmployement.reloadData()
            self.tableViewSelfEmployement.reloadData()
            self.tableViewBusiness.reloadData()
            self.tableViewMilitaryPay.reloadData()
            self.tableViewRetitrement.reloadData()
            self.tableViewOther.reloadData()
        }
    }
    
    @objc func addCurrentEmployement(){
        let vc = Utility.getAddCurrentEmployementVC()
        vc.isForAdd = true
        vc.loanApplicationId = self.loanApplicationId
        vc.borrowerId = self.borrowerId
        vc.borrowerName = self.borrowerName
        let navVC = UINavigationController(rootViewController: vc)
        navVC.navigationBar.isHidden = true
        navVC.modalPresentationStyle = .fullScreen
        self.presentVC(vc: navVC)
    }
    
    @objc func addPreviousEmployement(){
        let vc = Utility.getAddPreviousEmploymentVC()
        vc.isForAdd = true
        vc.loanApplicationId = self.loanApplicationId
        vc.borrowerId = self.borrowerId
        vc.borrowerName = self.borrowerName
        let navVC = UINavigationController(rootViewController: vc)
        navVC.navigationBar.isHidden = true
        navVC.modalPresentationStyle = .fullScreen
        self.presentVC(vc: navVC)
    }
  
    //MARK:- API's
    
    func getIncomeDetail(){
        
        loadingPlaceholderView.cover(self.view, animated: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerId=\(borrowerId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getIncomeDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                self.loadingPlaceholderView.uncover(animated: true)
                
                if (status == .success){
                    
                    let borrowerIncomeModel = BorrowerIncomeModel()
                    borrowerIncomeModel.updateModelWithJSON(json: result["data"]["borrower"])
                    self.borrowerIncomeData = borrowerIncomeModel
                    self.setIncomeDetail()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
            
        }
    }
}

extension IncomeDetailViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        if (tableView == tableViewEmployement){
            if (tableView == selectedTableView){
                return employmentIncome.incomes.count + 2
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewSelfEmployement){
            if (tableView == selectedTableView){
                return selfEmployementIncome.incomes.count + 2
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewBusiness){
            if (tableView == selectedTableView){
                return businessIncome.incomes.count + 2
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewMilitaryPay){
            if (tableView == selectedTableView){
                return militaryIncome.incomes.count + 2
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewRetitrement){
            if (tableView == selectedTableView){
                return retiermentIncome.incomes.count + 2
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewOther){
            if (tableView == selectedTableView){
                return otherIncome.incomes.count + 2
            }
            else{
                return 1
            }
        }
        return 0
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        if (tableView == tableViewEmployement){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Employment"
                cell.lblAmount.text = "\(Int(employmentIncome.incomeCategoryTotal).withCommas().replacingOccurrences(of: ".00", with: ""))/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == employmentIncome.incomes.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Employment"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = employmentIncome.incomes[indexPath.row - 1].incomeName
                cell.lblStatus.text = employmentIncome.incomes[indexPath.row - 1].jobTitle
                cell.lblAmount.text = "\(Int(employmentIncome.incomes[indexPath.row - 1].incomeValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.lblDate.text = Utility.getIncomeDate(startDate: employmentIncome.incomes[indexPath.row - 1].startDate, endDate: employmentIncome.incomes[indexPath.row - 1].endDate)
                return cell
            }
        }
        else if (tableView == tableViewSelfEmployement){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Self Employment / Independent Contractor"
                cell.lblAmount.text = "\(Int(selfEmployementIncome.incomeCategoryTotal).withCommas().replacingOccurrences(of: ".00", with: ""))/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == selfEmployementIncome.incomes.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Self Employment"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = selfEmployementIncome.incomes[indexPath.row - 1].incomeName
                cell.lblStatus.text = selfEmployementIncome.incomes[indexPath.row - 1].jobTitle
                cell.lblAmount.text = "\(Int(selfEmployementIncome.incomes[indexPath.row - 1].incomeValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.lblDate.text = Utility.getIncomeDate(startDate: selfEmployementIncome.incomes[indexPath.row - 1].startDate, endDate: selfEmployementIncome.incomes[indexPath.row - 1].endDate)
                return cell
            }
        }
        else if (tableView == tableViewBusiness){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Business"
                cell.lblAmount.text = "\(Int(businessIncome.incomeCategoryTotal).withCommas().replacingOccurrences(of: ".00", with: ""))/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == businessIncome.incomes.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Business"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = businessIncome.incomes[indexPath.row - 1].incomeName
                cell.lblStatus.text = businessIncome.incomes[indexPath.row - 1].jobTitle
                cell.lblAmount.text = "\(Int(businessIncome.incomes[indexPath.row - 1].incomeValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.lblDate.text = Utility.getIncomeDate(startDate: businessIncome.incomes[indexPath.row - 1].startDate, endDate: businessIncome.incomes[indexPath.row - 1].endDate)
                return cell
            }
        }
        else if (tableView == tableViewMilitaryPay){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Military Pay"
                cell.lblAmount.text = "\(Int(militaryIncome.incomeCategoryTotal).withCommas().replacingOccurrences(of: ".00", with: ""))/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == militaryIncome.incomes.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Military Service"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = militaryIncome.incomes[indexPath.row - 1].incomeName
                cell.lblStatus.text = militaryIncome.incomes[indexPath.row - 1].jobTitle
                cell.lblAmount.text = "\(Int(militaryIncome.incomes[indexPath.row - 1].incomeValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.lblDate.text = Utility.getIncomeDate(startDate: militaryIncome.incomes[indexPath.row - 1].startDate, endDate: militaryIncome.incomes[indexPath.row - 1].endDate)
                return cell
            }
        }
        else if (tableView == tableViewRetitrement){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Retirement"
                cell.lblAmount.text = "\(Int(retiermentIncome.incomeCategoryTotal).withCommas().replacingOccurrences(of: ".00", with: ""))/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == retiermentIncome.incomes.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Retirement"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = retiermentIncome.incomes[indexPath.row - 1].incomeName
                cell.lblStatus.text = retiermentIncome.incomes[indexPath.row - 1].jobTitle
                cell.lblAmount.text = "\(Int(retiermentIncome.incomes[indexPath.row - 1].incomeValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.lblDate.text = Utility.getIncomeDate(startDate: retiermentIncome.incomes[indexPath.row - 1].startDate, endDate: retiermentIncome.incomes[indexPath.row - 1].endDate)
                return cell
            }
        }
        else {
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Other"
                cell.lblAmount.text = "\(Int(otherIncome.incomeCategoryTotal).withCommas().replacingOccurrences(of: ".00", with: ""))/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == otherIncome.incomes.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Other Income"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = otherIncome.incomes[indexPath.row - 1].incomeName
                cell.lblStatus.text = otherIncome.incomes[indexPath.row - 1].jobTitle
                cell.lblAmount.text = "\(Int(otherIncome.incomes[indexPath.row - 1].incomeValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.lblDate.text = Utility.getIncomeDate(startDate: otherIncome.incomes[indexPath.row - 1].startDate, endDate: otherIncome.incomes[indexPath.row - 1].endDate)
                return cell
            }
            
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (tableView == tableViewEmployement){
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == employmentIncome.incomes.count + 1){
                return 70
            }
            else{
                return 83
            }
        }
        else if (tableView == tableViewSelfEmployement){
            if (indexPath.row == 0){
                return selectedTableView == tableViewSelfEmployement ? 78 : 58
            }
            else if (indexPath.row == selfEmployementIncome.incomes.count + 1){
                return 70
            }
            else{
                return 83
            }
        }
        else if (tableView == tableViewBusiness){
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == businessIncome.incomes.count + 1){
                return 70
            }
            else{
                return 83
            }
        }
        else if (tableView == tableViewMilitaryPay){
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == militaryIncome.incomes.count + 1){
                return 70
            }
            else{
                return 83
            }
        }
        else if (tableView == tableViewRetitrement){
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == retiermentIncome.incomes.count + 1){
                return 70
            }
            else{
                return 83
            }
        }
        else{
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == otherIncome.incomes.count + 1){
                return 70
            }
            else{
                return 83
            }
        }
        
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        if (indexPath.row == 0){
            if (selectedTableView == tableView){
                selectedTableView = nil
            }
            else{
                selectedTableView = tableView
            }
            DispatchQueue.main.asyncAfter(deadline: .now() + 0.05) {
                self.setScreenHeight()
            }
        }
        else{
            if (tableView == tableViewEmployement){
                
                if (indexPath.row == employmentIncome.incomes.count + 1){
                    let vc = Utility.getAddEmployementPopupVC()
                    self.present(vc, animated: false, completion: nil)
                }
                else{
                    if (employmentIncome.incomes[indexPath.row - 1].endDate == ""){
                        let vc = Utility.getAddCurrentEmployementVC()
                        vc.borrowerName = self.borrowerName
                        vc.loanApplicationId = self.loanApplicationId
                        vc.borrowerId = self.borrowerId
                        vc.incomeInfoId = employmentIncome.incomes[indexPath.row - 1].incomeId
                        let navVC = UINavigationController(rootViewController: vc)
                        navVC.navigationBar.isHidden = true
                        navVC.modalPresentationStyle = .fullScreen
                        self.presentVC(vc: navVC)
                    }
                    else{
                        let vc = Utility.getAddPreviousEmploymentVC()
                        vc.borrowerName = self.borrowerName
                        vc.loanApplicationId = self.loanApplicationId
                        vc.borrowerId = self.borrowerId
                        vc.incomeInfoId = employmentIncome.incomes[indexPath.row - 1].incomeId
                        let navVC = UINavigationController(rootViewController: vc)
                        navVC.navigationBar.isHidden = true
                        navVC.modalPresentationStyle = .fullScreen
                        self.presentVC(vc: navVC)
                    }
                    
                }
                
            }
            else if (tableView == tableViewSelfEmployement && (indexPath.row != 0))/*(indexPath.row == 1 || indexPath.row == 2))*/{
                let vc = Utility.getAddSelfEmploymentVC()
                let navVC = UINavigationController(rootViewController: vc)
                navVC.navigationBar.isHidden = true
                navVC.modalPresentationStyle = .fullScreen
                vc.borrowerName = self.borrowerName
                vc.loanApplicationId = self.loanApplicationId
                vc.borrowerId = self.borrowerId
                if (indexPath.row == selfEmployementIncome.incomes.count + 1){
                    vc.isForAdd = true
                }
                else{
                    vc.isForAdd = false
                    vc.incomeInfoId = selfEmployementIncome.incomes[indexPath.row - 1].incomeId
                }
                self.presentVC(vc: navVC)
            }
            else if (tableView == tableViewBusiness && (indexPath.row != 0))/*(indexPath.row == 1 || indexPath.row == 2))*/{
                let vc = Utility.getAddBusinessVC()
                let navVC = UINavigationController(rootViewController: vc)
                navVC.navigationBar.isHidden = true
                navVC.modalPresentationStyle = .fullScreen
                vc.borrowerName = self.borrowerName
                vc.loanApplicationId = self.loanApplicationId
                vc.borrowerId = self.borrowerId
                if (indexPath.row == businessIncome.incomes.count + 1){
                    vc.isForAdd = true
                }
                else{
                    vc.isForAdd = false
                    vc.incomeInfoId = businessIncome.incomes[indexPath.row - 1].incomeId
                }
                self.presentVC(vc: navVC)
            }
            else if (tableView == tableViewMilitaryPay && (indexPath.row != 0))/*(indexPath.row == 1 || indexPath.row == 2))*/{
                let vc = Utility.getAddMilitaryPayVC()
                let navVC = UINavigationController(rootViewController: vc)
                navVC.navigationBar.isHidden = true
                navVC.modalPresentationStyle = .fullScreen
                self.presentVC(vc: navVC)
            }
            else if (tableView == tableViewRetitrement && (indexPath.row != 0))/*(indexPath.row == 1 || indexPath.row == 2))*/{
                let vc = Utility.getAddRetirementIncomeVC()
                let navVC = UINavigationController(rootViewController: vc)
                navVC.navigationBar.isHidden = true
                navVC.modalPresentationStyle = .fullScreen
                self.presentVC(vc: navVC)
            }
            else if (tableView == tableViewOther && (indexPath.row != 0))/*(indexPath.row == 1 || indexPath.row == 2))*/{
                let vc = Utility.getAddOtherIncomeVC()
                let navVC = UINavigationController(rootViewController: vc)
                navVC.navigationBar.isHidden = true
                navVC.modalPresentationStyle = .fullScreen
                self.presentVC(vc: navVC)
            }
        }
    }
    
}
