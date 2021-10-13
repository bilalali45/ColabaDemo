//
//  IncomeDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 13/09/2021.
//

import UIKit

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
    
    var selectedTableView: UITableView?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupTableViews(tableViews: [tableViewEmployement, tableViewSelfEmployement, tableViewBusiness, tableViewMilitaryPay, tableViewRetitrement, tableViewOther])
        NotificationCenter.default.addObserver(self, selector: #selector(addCurrentEmployement), name: NSNotification.Name(rawValue: kNotificationAddCurrentEmployement), object: nil)
        NotificationCenter.default.addObserver(self, selector: #selector(addPreviousEmployement), name: NSNotification.Name(rawValue: kNotificationAddPreviousEmployement), object: nil)
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2){
            self.setScreenHeight()
        }
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
        }
    }
    
    func setScreenHeight(){
        
        let bankAccountTableViewHeight = selectedTableView == tableViewEmployement ? 292 : 58
        let retirementAccountTableViewHeight = selectedTableView == tableViewSelfEmployement ? 227 : 58
        let stocksBondTableViewHeight = selectedTableView == tableViewBusiness ? 209 : 58
        let transactionTableViewHeight = selectedTableView == tableViewMilitaryPay ? 209 : 58
        let giftFundTableViewHeight = selectedTableView == tableViewRetitrement ? 209 : 58
        let otherTableViewHeight = selectedTableView == tableViewOther ? 209 : 58
        
        let totalHeight = bankAccountTableViewHeight + retirementAccountTableViewHeight + stocksBondTableViewHeight + transactionTableViewHeight + giftFundTableViewHeight + otherTableViewHeight + 100
        
        tableViewEmployementHeightConstraint.constant = CGFloat(bankAccountTableViewHeight)
        tableViewSelfEmployementHeightConstraint.constant = CGFloat(retirementAccountTableViewHeight)
        tableViewBusinessHeightConstraint.constant = CGFloat(stocksBondTableViewHeight)
        tableViewMilitaryPayHeightConstraint.constant = CGFloat(transactionTableViewHeight)
        tableViewRetirementHeightConstraint.constant = CGFloat(giftFundTableViewHeight)
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
        let navVC = UINavigationController(rootViewController: vc)
        navVC.navigationBar.isHidden = true
        navVC.modalPresentationStyle = .fullScreen
        self.presentVC(vc: navVC)
    }
    
    @objc func addPreviousEmployement(){
        let vc = Utility.getAddPreviousEmploymentVC()
        let navVC = UINavigationController(rootViewController: vc)
        navVC.navigationBar.isHidden = true
        navVC.modalPresentationStyle = .fullScreen
        self.presentVC(vc: navVC)
    }
  
}

extension IncomeDetailViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        if (tableView == tableViewEmployement){
            if (tableView == selectedTableView){
                return 4
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewSelfEmployement){
            if (tableView == selectedTableView){
                return 3
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewBusiness){
            if (tableView == selectedTableView){
                return 3
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewMilitaryPay){
            if (tableView == selectedTableView){
                return 3
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewRetitrement){
            if (tableView == selectedTableView){
                return 3
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewOther){
            if (tableView == selectedTableView){
                return 3
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
                cell.lblAmount.text = "$5,000/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 3){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Employment"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = indexPath.row == 1 ? "Google LLC" : "Disrupt"
                cell.lblStatus.text = indexPath.row == 1 ? "Chief Executive Officer" : "Admin Manager"
                cell.lblAmount.text = indexPath.row == 1 ? "$5,000" : "$4,000"
                cell.lblDate.text = indexPath.row == 1 ? "From Jun 2020" : "Dec 2019 to May 2020"
                return cell
            }
        }
        else if (tableView == tableViewSelfEmployement){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Self Employment / Independent Contractor"
                cell.lblAmount.text = "$3,000/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 2){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Self Employment"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = "Freelance"
                cell.lblStatus.text = "Content Writer"
                cell.lblAmount.text = "$3,000"
                cell.lblDate.text = "From Dec 2020"
                return cell
            }
        }
        else if (tableView == tableViewBusiness){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Business"
                cell.lblAmount.text = "$6,000/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 2){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Business"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = "OPTP"
                cell.lblStatus.text = "Director"
                cell.lblAmount.text = "$6,000"
                cell.lblDate.text = "From Jun 2020"
                return cell
            }
        }
        else if (tableView == tableViewMilitaryPay){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Military Pay"
                cell.lblAmount.text = "$2,000/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 2){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Military Service"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = "US Army"
                cell.lblStatus.text = "Field 15 — Aviation"
                cell.lblAmount.text = "$2,000"
                cell.lblDate.text = "From Jun 2020"
                return cell
            }
        }
        else if (tableView == tableViewRetitrement){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Retirement"
                cell.lblAmount.text = "$1,200/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 2){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Retirement"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = "Google LLC"
                cell.lblStatus.text = "Pension"
                cell.lblAmount.text = "$1,200"
                cell.lblDate.text = ""
                return cell
            }
        }
        else {
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Other"
                cell.lblAmount.text = "$4,125/mo"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 2){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Other Income"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = "Alimony"
                cell.lblStatus.text = "Family"
                cell.lblAmount.text = "$4,125"
                cell.lblDate.text = ""
                return cell
            }
            
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (tableView == tableViewEmployement){
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == 3){
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
            else if (indexPath.row == 2){
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
            else if (indexPath.row == 2){
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
                if ((indexPath.row == 1)){
                    let vc = Utility.getAddCurrentEmployementVC()
                    let navVC = UINavigationController(rootViewController: vc)
                    navVC.navigationBar.isHidden = true
                    navVC.modalPresentationStyle = .fullScreen
                    self.presentVC(vc: navVC)
                }
                else if (indexPath.row == 2){
                    let vc = Utility.getAddPreviousEmploymentVC()
                    let navVC = UINavigationController(rootViewController: vc)
                    navVC.navigationBar.isHidden = true
                    navVC.modalPresentationStyle = .fullScreen
                    self.presentVC(vc: navVC)
                }
                else if (indexPath.row == 3){
                    let vc = Utility.getAddEmployementPopupVC()
                    self.present(vc, animated: false, completion: nil)
                }
                
            }
            else if (tableView == tableViewSelfEmployement && (indexPath.row == 1 || indexPath.row == 2)){
                let vc = Utility.getAddSelfEmploymentVC()
                let navVC = UINavigationController(rootViewController: vc)
                navVC.navigationBar.isHidden = true
                navVC.modalPresentationStyle = .fullScreen
                self.presentVC(vc: navVC)
            }
            else if (tableView == tableViewBusiness && (indexPath.row == 1 || indexPath.row == 2)){
                let vc = Utility.getAddBusinessVC()
                let navVC = UINavigationController(rootViewController: vc)
                navVC.navigationBar.isHidden = true
                navVC.modalPresentationStyle = .fullScreen
                self.presentVC(vc: navVC)
            }
            else if (tableView == tableViewMilitaryPay && (indexPath.row == 1 || indexPath.row == 2)){
                let vc = Utility.getAddMilitaryPayVC()
                let navVC = UINavigationController(rootViewController: vc)
                navVC.navigationBar.isHidden = true
                navVC.modalPresentationStyle = .fullScreen
                self.presentVC(vc: navVC)
            }
            else if (tableView == tableViewRetitrement && (indexPath.row == 1 || indexPath.row == 2)){
                let vc = Utility.getAddRetirementIncomeVC()
                let navVC = UINavigationController(rootViewController: vc)
                navVC.navigationBar.isHidden = true
                navVC.modalPresentationStyle = .fullScreen
                self.presentVC(vc: navVC)
            }
            else if (tableView == tableViewOther && (indexPath.row == 1 || indexPath.row == 2)){
                let vc = Utility.getAddOtherIncomeVC()
                let navVC = UINavigationController(rootViewController: vc)
                navVC.navigationBar.isHidden = true
                navVC.modalPresentationStyle = .fullScreen
                self.presentVC(vc: navVC)
            }
        }
    }
    
}
