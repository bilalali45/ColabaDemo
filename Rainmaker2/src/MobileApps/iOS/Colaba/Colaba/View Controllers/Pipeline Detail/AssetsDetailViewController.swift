//
//  AssetsDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 06/09/2021.
//

import UIKit

class AssetsDetailViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewBankAccount: UITableView!
    @IBOutlet weak var tableViewBankAccountHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewRetirementAccount: UITableView!
    @IBOutlet weak var tableViewRetirementAccountHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewStockBonds: UITableView!
    @IBOutlet weak var tableViewStockBondsHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewTransaction: UITableView!
    @IBOutlet weak var tableViewTransactionHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewGiftFunds: UITableView!
    @IBOutlet weak var tableViewGiftHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewOther: UITableView!
    @IBOutlet weak var tableViewOtherHeightConstraint: NSLayoutConstraint!
    
    var selectedTableView: UITableView?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupTableViews(tableViews: [tableViewBankAccount, tableViewRetirementAccount, tableViewStockBonds, tableViewTransaction, tableViewGiftFunds, tableViewOther])
        
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
        
        let bankAccountTableViewHeight = selectedTableView == tableViewBankAccount ? 292 : 58
        let retirementAccountTableViewHeight = selectedTableView == tableViewRetirementAccount ? 209 : 58
        let stocksBondTableViewHeight = selectedTableView == tableViewStockBonds ? 209 : 58
        let transactionTableViewHeight = selectedTableView == tableViewTransaction ? 209 : 58
        let giftFundTableViewHeight = selectedTableView == tableViewGiftFunds ? 209 : 58
        let otherTableViewHeight = selectedTableView == tableViewOther ? 209 : 58
        
        let totalHeight = bankAccountTableViewHeight + retirementAccountTableViewHeight + stocksBondTableViewHeight + transactionTableViewHeight + giftFundTableViewHeight + otherTableViewHeight + 100
        
        tableViewBankAccountHeightConstraint.constant = CGFloat(bankAccountTableViewHeight)
        tableViewRetirementAccountHeightConstraint.constant = CGFloat(retirementAccountTableViewHeight)
        tableViewStockBondsHeightConstraint.constant = CGFloat(stocksBondTableViewHeight)
        tableViewTransactionHeightConstraint.constant = CGFloat(transactionTableViewHeight)
        tableViewGiftHeightConstraint.constant = CGFloat(giftFundTableViewHeight)
        tableViewOtherHeightConstraint.constant = CGFloat(otherTableViewHeight)
        
        self.mainViewHeightConstraint.constant = CGFloat(totalHeight)
        
        UIView.animate(withDuration: 1) {
            self.view.layoutSubviews()
        }
    }
  
}

extension AssetsDetailViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        if (tableView == tableViewBankAccount){
            if (tableView == selectedTableView){
                return 4
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewRetirementAccount){
            if (tableView == selectedTableView){
                return 3
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewStockBonds){
            if (tableView == selectedTableView){
                return 3
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewTransaction){
            if (tableView == selectedTableView){
                return 3
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewGiftFunds){
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
        if (tableView == tableViewBankAccount){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Bank Account"
                cell.lblAmount.text = "$26,000"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 3){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Bank Account"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = indexPath.row == 1 ? "Chase" : "Ally Bank"
                cell.lblStatus.text = indexPath.row == 1 ? "Checking" : "Savings"
                cell.lblAmount.text = indexPath.row == 1 ? "$20,000" : "$6,000"
                return cell
            }
        }
        else if (tableView == tableViewRetirementAccount){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Retirement Account"
                cell.lblAmount.text = "$10,000"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 2){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Retirement Account"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = "401K"
                cell.lblStatus.text = "Retirement Account"
                cell.lblAmount.text = "$10,000"
                return cell
            }
        }
        else if (tableView == tableViewStockBonds){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Stocks, Bonds, or Other Assets"
                cell.lblAmount.text = "$800"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 2){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Financial Assets"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = "AHC"
                cell.lblStatus.text = "Mutual Funds"
                cell.lblAmount.text = "$800"
                return cell
            }
        }
        else if (tableView == tableViewTransaction){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Proceeds From Transaction"
                cell.lblAmount.text = "$1,200"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 2){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Proceeds From Transaction"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = "Proceeds From Selling Non-Real Estate"
                cell.lblStatus.text = "Proceeds From Transaction"
                cell.lblAmount.text = "$1,200"
                return cell
            }
        }
        else if (tableView == tableViewGiftFunds){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Gift Funds"
                cell.lblAmount.text = "$20,000"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 2){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Gift Funds"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = "Relative"
                cell.lblStatus.text = "Cash Gift"
                cell.lblAmount.text = "$20,000"
                return cell
            }
        }
        else {
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Other"
                cell.lblAmount.text = "$600"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == 2){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Other Assets"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = "Individual Development Account"
                cell.lblStatus.text = "Other"
                cell.lblAmount.text = "$600"
                return cell
            }
            
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (tableView == tableViewBankAccount){
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
            
            DispatchQueue.main.async {
                self.tableViewBankAccount.reloadData()
                self.tableViewRetirementAccount.reloadData()
                self.tableViewStockBonds.reloadData()
                self.tableViewTransaction.reloadData()
                self.tableViewGiftFunds.reloadData()
                self.tableViewOther.reloadData()
                DispatchQueue.main.asyncAfter(deadline: .now() + 0.05) {
                    self.setScreenHeight()
                }
                
            }
            
        }
    }
    
}
