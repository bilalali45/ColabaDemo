//
//  DocumentsListViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/10/2021.
//

import UIKit

class DocumentsListViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var searchView: UIView!
    @IBOutlet weak var searchIcon: UIImageView!
    @IBOutlet weak var txtfieldSearch: UITextField!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var tableViewAssets: UITableView!
    @IBOutlet weak var tableViewAssetsHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewIncome: UITableView!
    @IBOutlet weak var tableViewIncomeHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewLiabilities: UITableView!
    @IBOutlet weak var tableViewLiabilitiesHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewPersonal: UITableView!
    @IBOutlet weak var tableViewPersonalHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewProperty: UITableView!
    @IBOutlet weak var tableViewPropertyHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewDisclosure: UITableView!
    @IBOutlet weak var tableViewDisclosureHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewOther: UITableView!
    @IBOutlet weak var tableViewOtherHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var addCustomDocumentView: UIView!
    
    var selectedTableView: UITableView?
    
    let assetsArray = ["Credit Report", "Earnest Money Deposit", "Financial Statement", "Form 1099", "Government-issued ID", "Letter of Explanation - General", "Mortgage Statement", "Form 1099"]
    let incomeArray = ["Tax Returns with Schedules (Business - Two Years)", "Tax Returns with Schedules (Personal - Two Years)", "Paystubs - Most Recent", "W-2s - Last Two years", "Rental Agreement - Real Estate Owned", "Income - Miscellaneous"]
    let liabilitiesArray = ["HOA or Condo Association Fee Statements", "Liabilities - Miscellaneous", "Rental Agreement", "Bankruptcy Papers", "Property Tax Statement"]
    let personalArray = ["Government Issued Identification", "Permanent Resident Card", "Work Visa - Work Permit"]
    let propertyArray = ["Purchase Contract", "Condo HO6 Interior Insurance", "Flood Insurance Policy", "Survey Affidavit", "Property Survey", "Homeowner's Association Certificate"]
    let disclosureArray = ["Purchase Contract", "Condo HO6 Interior Insurance", "Flood Insurance Policy", "Survey Affidavit", "Property Survey", "Homeowner's Association Certificate"]
    let otherArray = ["Trust - Family Trust", "Divorce Decree", "Mortgage Statement", "Homebuyer Education", "Credit Explanation", "Letter of Explanation", "Power of Attorney (POA)", "Purpose of Cash Out", "Page 5 of 5 of bank statements"]
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTableViewsAndViews(tableViews: [tableViewAssets, tableViewIncome, tableViewLiabilities, tableViewPersonal, tableViewProperty, tableViewDisclosure, tableViewOther])
        txtfieldSearch.addTarget(self, action: #selector(textfieldSearchEditingChanged), for: .editingChanged)
    }
    
    //MARK:- Methods
    
    func setupTableViewsAndViews(tableViews: [UITableView]){
        for tableView in tableViews{
            tableView.register(UINib(nibName: "DocumentsTemplatesTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentsTemplatesTableViewCell")
            tableView.register(UINib(nibName: "AssetsHeadingTableViewCell", bundle: nil), forCellReuseIdentifier: "AssetsHeadingTableViewCell")
            tableView.layer.cornerRadius = 6
            tableView.layer.borderWidth = 1
            tableView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            tableView.clipsToBounds = false
            tableView.layer.masksToBounds = false
            tableView.dropShadowToCollectionViewCell(shadowColor: UIColor(red: 0/255, green: 0/255, blue: 0/255, alpha: 0.12).cgColor, shadowRadius: 1, shadowOpacity: 1)
        }
        
        searchView.backgroundColor = .white
        searchView.layer.cornerRadius = 6
        searchView.layer.borderWidth = 1
        searchView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        
        txtfieldSearch.delegate = self
        
        addCustomDocumentView.layer.cornerRadius = 6
        addCustomDocumentView.layer.borderWidth = 1
        addCustomDocumentView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.1).cgColor
        addCustomDocumentView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addCustomDocumentTapped)))
    }
   
    func setScreenHeight(){
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
            self.tableViewAssets.reloadData()
            self.tableViewIncome.reloadData()
            self.tableViewLiabilities.reloadData()
            self.tableViewPersonal.reloadData()
            self.tableViewProperty.reloadData()
            self.tableViewDisclosure.reloadData()
            self.tableViewOther.reloadData()
        }

        let assetsTableViewHeight = selectedTableView == tableViewAssets ? (tableViewAssets.contentSize.height) : 56
        let incomeTableViewHeight = selectedTableView == tableViewIncome ? (tableViewIncome.contentSize.height) : 56
        let liabilityTableViewHeight = selectedTableView == tableViewLiabilities ? (tableViewLiabilities.contentSize.height) : 56
        let personalTableViewHeight = selectedTableView == tableViewPersonal ? (tableViewPersonal.contentSize.height) : 56
        let propertyTableViewHeight = selectedTableView == tableViewProperty ? (tableViewProperty.contentSize.height) : 56
        let disclosureTableViewHeight = selectedTableView == tableViewDisclosure ? (tableViewDisclosure.contentSize.height) : 56
        let otherTableViewHeight = selectedTableView == tableViewOther ? (tableViewOther.contentSize.height) : 56
        
        let totalHeight = assetsTableViewHeight + incomeTableViewHeight + liabilityTableViewHeight +  personalTableViewHeight + propertyTableViewHeight + disclosureTableViewHeight + otherTableViewHeight + 300
        
        tableViewAssetsHeightConstraint.constant = CGFloat(assetsTableViewHeight)
        tableViewIncomeHeightConstraint.constant = CGFloat(incomeTableViewHeight)
        tableViewLiabilitiesHeightConstraint.constant = CGFloat(liabilityTableViewHeight)
        tableViewPersonalHeightConstraint.constant = CGFloat(personalTableViewHeight)
        tableViewPropertyHeightConstraint.constant = CGFloat(propertyTableViewHeight)
        tableViewDisclosureHeightConstraint.constant = CGFloat(disclosureTableViewHeight)
        tableViewOtherHeightConstraint.constant = CGFloat(otherTableViewHeight)
        
        self.mainViewHeightConstraint.constant = CGFloat(totalHeight)

    }
    
    @objc func textfieldSearchEditingChanged(){
        btnClose.isHidden = txtfieldSearch.text == ""
    }
    
    @objc func addCustomDocumentTapped(){
        let vc = Utility.getCustomDocumentVC()
        self.presentVC(vc: vc)
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        txtfieldSearch.text = ""
        btnClose.isHidden = true
    }
    
}

extension DocumentsListViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        if (tableView == tableViewAssets){
            return selectedTableView == tableViewAssets ? assetsArray.count + 1 : 1
        }
        else if (tableView == tableViewIncome){
            return selectedTableView == tableViewIncome ? incomeArray.count + 1 : 1
        }
        else if (tableView == tableViewLiabilities){
            return selectedTableView == tableViewLiabilities ? liabilitiesArray.count + 1 : 1
        }
        else if (tableView == tableViewPersonal){
            return selectedTableView == tableViewPersonal ? personalArray.count + 1 : 1
        }
        else if (tableView == tableViewProperty){
            return selectedTableView == tableViewProperty ? propertyArray.count + 1 : 1
        }
        else if (tableView == tableViewDisclosure){
            return selectedTableView == tableViewDisclosure ? disclosureArray.count + 1 : 1
        }
        else{
            return selectedTableView == tableViewOther ? otherArray.count + 1 : 1
        }
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        if (tableView == tableViewAssets){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Assets"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                cell.counterView.isHidden = selectedTableView != tableView
                cell.lblCounter.text = "2"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = assetsArray[indexPath.row - 1]
                cell.btnInfo.isHidden = true
                return cell
            }
        }
        else if (tableView == tableViewIncome){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Income"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                cell.counterView.isHidden = selectedTableView != tableView
                cell.lblCounter.text = "1"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = incomeArray[indexPath.row - 1]
                cell.btnInfo.isHidden = true
                return cell
            }
        }
        else if (tableView == tableViewLiabilities){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Liabilities"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = liabilitiesArray[indexPath.row - 1]
                cell.btnInfo.isHidden = true
                return cell
            }
        }
        else if (tableView == tableViewPersonal){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Personal"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = personalArray[indexPath.row - 1]
                cell.btnInfo.isHidden = true
                return cell
            }
        }
        else if (tableView == tableViewProperty){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Property"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                cell.counterView.isHidden = selectedTableView != tableView
                cell.lblCounter.text = "4"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = propertyArray[indexPath.row - 1]
                cell.btnInfo.isHidden = true
                return cell
            }
        }
        else if (tableView == tableViewDisclosure){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Disclosure"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = disclosureArray[indexPath.row - 1]
                cell.btnInfo.isHidden = true
                return cell
            }
        }
        else{
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Other"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = otherArray[indexPath.row - 1]
                cell.btnInfo.isHidden = indexPath.row != otherArray.count
                cell.btnInfo.setImage(UIImage(named: "editIcon"), for: .normal)
                return cell
            }
        }
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
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
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return indexPath.row == 0 ? 56 : UITableView.automaticDimension
    }
}

extension DocumentsListViewController: UITextFieldDelegate{
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        let vc = Utility.getSearchRequestDocumentVC()
        vc.searchedDocumentName = txtfieldSearch.text!
        let navVC = UINavigationController(rootViewController: vc)
        navVC.navigationBar.isHidden = true
        navVC.modalPresentationStyle = .fullScreen
        self.presentVC(vc: navVC)
        return true
    }
    
}
