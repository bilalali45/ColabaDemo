//
//  AssetsDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 06/09/2021.
//

import UIKit
import LoadingPlaceholderView

protocol AssetsDetailViewControllerDelegate: Any {
    func getBorrowerTotalAssets(totalAssets: String)
}

class AssetsDetailViewController: BaseViewController {

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
    
    let loadingPlaceholderView = LoadingPlaceholderView()
    
    var delegate: AssetsDetailViewControllerDelegate?
    var loanApplicationId = 0
    var loanPurposeId = 0
    var borrowerId = 0
    var borrowerName = ""
    var borrowerAssetData = BorrowerAssetsModel()
    var selectedTableView: UITableView?
    
    var bankAccountAsset = BorrowerAsset()
    var retirementAccountAsset = BorrowerAsset()
    var stockBondsAsset = BorrowerAsset()
    var transactionAsset = BorrowerAsset()
    var giftFundsAsset = BorrowerAsset()
    var otherAsset = BorrowerAsset()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupTableViews(tableViews: [tableViewBankAccount, tableViewRetirementAccount, tableViewStockBonds, tableViewTransaction, tableViewGiftFunds, tableViewOther])
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2){
            self.setScreenHeight()
        }
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        self.delegate?.getBorrowerTotalAssets(totalAssets: "\(Int(borrowerAssetData.assetsTotal).withCommas().replacingOccurrences(of: ".00", with: ""))")
        getAssetsDetail()
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
    
    func setAssetsDetail(){
        
        self.delegate?.getBorrowerTotalAssets(totalAssets: "\(Int(borrowerAssetData.assetsTotal).withCommas().replacingOccurrences(of: ".00", with: ""))")
        
        if let bankAccount = self.borrowerAssetData.borrowerAssets.filter({$0.assetsCategory == "Bank Account"
        }).first{
            self.bankAccountAsset = bankAccount
        }
        if let retirementAccount = self.borrowerAssetData.borrowerAssets.filter({$0.assetsCategory == "Retirement Account"
        }).first{
            self.retirementAccountAsset = retirementAccount
        }
        if let stockAccount = self.borrowerAssetData.borrowerAssets.filter({$0.assetsCategory == "Stocks, Bonds, Or Other Financial Assets"
        }).first{
            self.stockBondsAsset = stockAccount
        }
        if let transactionAccount = self.borrowerAssetData.borrowerAssets.filter({$0.assetsCategory == "Proceeds from Transactions"
        }).first{
            self.transactionAsset = transactionAccount
        }
        if let giftAccount = self.borrowerAssetData.borrowerAssets.filter({$0.assetsCategory == "Gift Funds"
        }).first{
            self.giftFundsAsset = giftAccount
        }
        if let otherAccount = self.borrowerAssetData.borrowerAssets.filter({$0.assetsCategory == "Other"
        }).first{
            self.otherAsset = otherAccount
        }
        self.setScreenHeight()
    }
    
    func setScreenHeight(){
        
        let bankAccountHeight = (self.bankAccountAsset.assets.count * 83)
        let bankAccountTableViewHeight = selectedTableView == tableViewBankAccount ? (bankAccountHeight + 128) : 58
        
        let retirementHeight = (self.retirementAccountAsset.assets.count * 83)
        let retirementAccountTableViewHeight = selectedTableView == tableViewRetirementAccount ? (retirementHeight + 128) : 58
        
        let stockHeight = (self.stockBondsAsset.assets.count * 83)
        let stocksBondTableViewHeight = selectedTableView == tableViewStockBonds ? (stockHeight + 128) : 58
        
        let transactionHeight = (self.transactionAsset.assets.count * 83)
        let transactionTableViewHeight = selectedTableView == tableViewTransaction ? (transactionHeight + 128) : 58
        
        let giftHeight = (self.giftFundsAsset.assets.count * 83)
        let giftFundTableViewHeight = selectedTableView == tableViewGiftFunds ? (giftHeight + 128) : 58
        
        let otherHeight = (self.otherAsset.assets.count * 83)
        let otherTableViewHeight = selectedTableView == tableViewOther ? (otherHeight + 128) : 58
        
        let totalHeight = bankAccountTableViewHeight + retirementAccountTableViewHeight + stocksBondTableViewHeight + transactionTableViewHeight + giftFundTableViewHeight + otherTableViewHeight + 100
        
        tableViewBankAccountHeightConstraint.constant = CGFloat(bankAccountTableViewHeight)
        tableViewRetirementAccountHeightConstraint.constant = CGFloat(retirementAccountTableViewHeight)
        tableViewStockBondsHeightConstraint.constant = CGFloat(stocksBondTableViewHeight)
        tableViewTransactionHeightConstraint.constant = CGFloat(transactionTableViewHeight)
        tableViewGiftHeightConstraint.constant = CGFloat(giftFundTableViewHeight)
        tableViewOtherHeightConstraint.constant = CGFloat(otherTableViewHeight)
        
        self.mainViewHeightConstraint.constant = CGFloat(totalHeight)
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
            self.tableViewBankAccount.reloadData()
            self.tableViewRetirementAccount.reloadData()
            self.tableViewStockBonds.reloadData()
            self.tableViewTransaction.reloadData()
            self.tableViewGiftFunds.reloadData()
            self.tableViewOther.reloadData()
        }
    }
    
    //MARK:- API's
    
    func getAssetsDetail(){
        
        if (bankAccountAsset.assets.count == 0 && retirementAccountAsset.assets.count == 0 && stockBondsAsset.assets.count == 0 && transactionAsset.assets.count == 0 && giftFundsAsset.assets.count == 0 && otherAsset.assets.count == 0){
            loadingPlaceholderView.cover(self.view, animated: true)
        }
        
        let extraData = "loanApplicationId=\(loanApplicationId)&borrowerId=\(borrowerId)"
        
        APIRouter.sharedInstance.executeAPI(type: .getAssetsDetail, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                
                self.loadingPlaceholderView.uncover(animated: true)
                
                if (status == .success){
                    
                    self.bankAccountAsset = BorrowerAsset()
                    self.retirementAccountAsset = BorrowerAsset()
                    self.stockBondsAsset = BorrowerAsset()
                    self.transactionAsset = BorrowerAsset()
                    self.giftFundsAsset = BorrowerAsset()
                    self.otherAsset = BorrowerAsset()
                    
                    let borrowerAssetsModel = BorrowerAssetsModel()
                    borrowerAssetsModel.updateModelWithJSON(json: result["data"]["borrower"])
                    self.borrowerAssetData = borrowerAssetsModel
                    self.setAssetsDetail()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
            
        }
    }
  
}

extension AssetsDetailViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        if (tableView == tableViewBankAccount){
            if (tableView == selectedTableView){
                return bankAccountAsset.assets.count + 2
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewRetirementAccount){
            if (tableView == selectedTableView){
                return retirementAccountAsset.assets.count + 2
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewStockBonds){
            if (tableView == selectedTableView){
                return stockBondsAsset.assets.count + 2
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewTransaction){
            if (tableView == selectedTableView){
                return transactionAsset.assets.count + 2
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewGiftFunds){
            if (tableView == selectedTableView){
                return giftFundsAsset.assets.count + 2
            }
            else{
                return 1
            }
        }
        else if (tableView == tableViewOther){
            if (tableView == selectedTableView){
                return otherAsset.assets.count + 2
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
                cell.lblAmount.text = "\(Int(bankAccountAsset.assetsTotal).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == bankAccountAsset.assets.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Bank Account"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = bankAccountAsset.assets[indexPath.row - 1].assetName
                cell.lblStatus.text = bankAccountAsset.assets[indexPath.row - 1].assetTypeName
                cell.lblAmount.text = "\(Int(bankAccountAsset.assets[indexPath.row - 1].assetValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                return cell
            }
        }
        else if (tableView == tableViewRetirementAccount){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Retirement Account"
                cell.lblAmount.text = "\(Int(retirementAccountAsset.assetsTotal).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == retirementAccountAsset.assets.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Retirement Account"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = retirementAccountAsset.assets[indexPath.row - 1].assetName
                cell.lblStatus.text = retirementAccountAsset.assets[indexPath.row - 1].assetTypeName
                cell.lblAmount.text = "\(Int(retirementAccountAsset.assets[indexPath.row - 1].assetValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                return cell
            }
        }
        else if (tableView == tableViewStockBonds){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Stocks, Bonds, or Other Assets"
                cell.lblAmount.text = "\(Int(stockBondsAsset.assetsTotal).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == stockBondsAsset.assets.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Financial Assets"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = stockBondsAsset.assets[indexPath.row - 1].assetName
                cell.lblStatus.text = stockBondsAsset.assets[indexPath.row - 1].assetTypeName
                cell.lblAmount.text = "\(Int(stockBondsAsset.assets[indexPath.row - 1].assetValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                return cell
            }
        }
        else if (tableView == tableViewTransaction){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Proceeds From Transaction"
                cell.lblAmount.text = "\(Int(transactionAsset.assetsTotal).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == transactionAsset.assets.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Proceeds From Transaction"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = transactionAsset.assets[indexPath.row - 1].assetName
                cell.lblStatus.text = transactionAsset.assets[indexPath.row - 1].assetTypeName
                cell.lblAmount.text = "\(Int(transactionAsset.assets[indexPath.row - 1].assetValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                return cell
            }
        }
        else if (tableView == tableViewGiftFunds){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Gift Funds"
                cell.lblAmount.text = "\(Int(giftFundsAsset.assetsTotal).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == giftFundsAsset.assets.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Gift Funds"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = giftFundsAsset.assets[indexPath.row - 1].assetName
                cell.lblStatus.text = giftFundsAsset.assets[indexPath.row - 1].assetTypeName
                cell.lblAmount.text = "\(Int(giftFundsAsset.assets[indexPath.row - 1].assetValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                return cell
            }
        }
        else {
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Other"
                cell.lblAmount.text = "\(Int(otherAsset.assetsTotal).withCommas().replacingOccurrences(of: ".00", with: ""))"
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                return cell
            }
            else if (indexPath.row == otherAsset.assets.count + 1){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsAddNewTableViewCell", for: indexPath) as! AssetsAddNewTableViewCell
                cell.lblTitle.text = "Add Other Assets"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsDetailTableViewCell", for: indexPath) as! AssetsDetailTableViewCell
                cell.lblTitle.text = otherAsset.assets[indexPath.row - 1].assetName
                cell.lblStatus.text = otherAsset.assets[indexPath.row - 1].assetTypeName
                cell.lblAmount.text = "\(Int(otherAsset.assets[indexPath.row - 1].assetValue).withCommas().replacingOccurrences(of: ".00", with: ""))"
                return cell
            }
            
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (tableView == tableViewBankAccount){
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == bankAccountAsset.assets.count + 1){
                return 70
            }
            else{
                return 83
            }
        }
        else if (tableView == tableViewRetirementAccount){
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == retirementAccountAsset.assets.count + 1){
                return 70
            }
            else{
                return 83
            }
        }
        else if (tableView == tableViewStockBonds){
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == stockBondsAsset.assets.count + 1){
                return 70
            }
            else{
                return 83
            }
        }
        else if (tableView == tableViewTransaction){
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == transactionAsset.assets.count + 1){
                return 70
            }
            else{
                return 83
            }
        }
        else if (tableView == tableViewGiftFunds){
            if (indexPath.row == 0){
                return 58
            }
            else if (indexPath.row == giftFundsAsset.assets.count + 1){
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
            else if (indexPath.row == otherAsset.assets.count + 1){
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
            if (tableView == tableViewBankAccount && (indexPath.row != 0)){
                let vc = Utility.getAddBankAccountVC()
                vc.loanApplicationId = self.loanApplicationId
                vc.borrowerId = self.borrowerId
                vc.borrowerName = self.borrowerName
                if (indexPath.row == bankAccountAsset.assets.count + 1){
                    vc.isForAdd = true
                }
                else{
                    vc.isForAdd = false
                    vc.borrowerAssetId = bankAccountAsset.assets[indexPath.row - 1].assetId
                }
                self.presentVC(vc: vc)
            }
            else if (tableView == tableViewRetirementAccount && (indexPath.row != 0)){
                let vc = Utility.getAddRetirementAccountVC()
                vc.loanApplicationId = self.loanApplicationId
                vc.borrowerId = self.borrowerId
                vc.borrowerName = self.borrowerName
                if (indexPath.row == retirementAccountAsset.assets.count + 1){
                    vc.isForAdd = true
                }
                else{
                    vc.isForAdd = false
                    vc.borrowerAssetId = retirementAccountAsset.assets[indexPath.row - 1].assetId
                }
                self.presentVC(vc: vc)
            }
            else if (tableView == tableViewStockBonds && (indexPath.row != 0)){
                let vc = Utility.getAddStockBondVC()
                vc.loanApplicationId = self.loanApplicationId
                vc.borrowerId = self.borrowerId
                vc.borrowerName = self.borrowerName
                if (indexPath.row == stockBondsAsset.assets.count + 1){
                    vc.isForAdd = true
                }
                else{
                    vc.isForAdd = false
                    vc.borrowerAssetId = stockBondsAsset.assets[indexPath.row - 1].assetId
                }
                self.presentVC(vc: vc)
            }
            else if (tableView == tableViewTransaction && (indexPath.row != 0)){
                let vc = Utility.getAddProceedsFromTransactionVC()
                vc.loanApplicationId = self.loanApplicationId
                vc.borrowerId = self.borrowerId
                vc.borrowerName = self.borrowerName
                vc.loanPurposeId = self.loanPurposeId
                if (indexPath.row == transactionAsset.assets.count + 1){
                    vc.isForAdd = true
                    vc.assetCategoryId = 6
                }
                else{
                    vc.isForAdd = false
                    vc.assetCategoryId = transactionAsset.assets[indexPath.row - 1].assetCategoryId
                    vc.borrowerAssetId = transactionAsset.assets[indexPath.row - 1].assetId
                    vc.assetTypeId = transactionAsset.assets[indexPath.row - 1].assetTypeID
                }
                self.presentVC(vc: vc)
            }
            else if (tableView == tableViewGiftFunds && (indexPath.row != 0)){
                let vc = Utility.getAddGiftFundsVC()
                vc.loanApplicationId = self.loanApplicationId
                vc.borrowerId = self.borrowerId
                vc.borrowerName = self.borrowerName
                vc.loanPurposeId = self.loanPurposeId
                if (indexPath.row == giftFundsAsset.assets.count + 1){
                    vc.isForAdd = true
                    vc.assetCategoryId = 4
                }
                else{
                    vc.isForAdd = false
                    vc.assetCategoryId = giftFundsAsset.assets[indexPath.row - 1].assetCategoryId
                    vc.borrowerAssetId = giftFundsAsset.assets[indexPath.row - 1].assetId
                }
                self.presentVC(vc: vc)
            }
            else if (tableView == tableViewOther && (indexPath.row != 0)){
                let vc = Utility.getAddOtherAssetsVC()
                vc.loanApplicationId = self.loanApplicationId
                vc.borrowerId = self.borrowerId
                vc.borrowerName = self.borrowerName
                vc.loanPurposeId = self.loanPurposeId
                if (indexPath.row == otherAsset.assets.count + 1){
                    vc.isForAdd = true
                    vc.assetCategoryId = 7
                }
                else{
                    vc.isForAdd = false
                    vc.assetCategoryId = otherAsset.assets[indexPath.row - 1].assetCategoryId
                    vc.borrowerAssetId = otherAsset.assets[indexPath.row - 1].assetId
                }
                self.presentVC(vc: vc)
            }
        }
    }
    
}
