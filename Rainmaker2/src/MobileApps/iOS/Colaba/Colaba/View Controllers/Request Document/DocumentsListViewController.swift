//
//  DocumentsListViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/10/2021.
//

import UIKit
import LoadingPlaceholderView

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
    let loadingPlaceholderView = LoadingPlaceholderView()
    
    var assets = DocumentCategoryModel()
    var income = DocumentCategoryModel()
    var liabilities = DocumentCategoryModel()
    var personal = DocumentCategoryModel()
    var property = DocumentCategoryModel()
    var disclosure = DocumentCategoryModel()
    var other = DocumentCategoryModel()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupTableViewsAndViews(tableViews: [tableViewAssets, tableViewIncome, tableViewLiabilities, tableViewPersonal, tableViewProperty, tableViewDisclosure, tableViewOther])
        txtfieldSearch.addTarget(self, action: #selector(textfieldSearchEditingChanged), for: .editingChanged)
        getDocumentsList()
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        setScreenHeight()
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
            tableView.coverableCellsIdentifiers = ["AssetsHeadingTableViewCell"]
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
        let disclosureTableViewHeight = selectedTableView == tableViewDisclosure ? (tableViewDisclosure.contentSize.height + 20) : 56
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
    
    //MARK:- API's
    
    func getDocumentsList(){
        
        if (assets.documents.count == 0 && income.documents.count == 0 && liabilities.documents.count == 0 && personal.documents.count == 0 && property.documents.count == 0 && disclosure.documents.count == 0 && other.documents.count == 0){
            loadingPlaceholderView.cover(self.view, animated: true)
        }
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getDocumentListByCategory, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                
                self.loadingPlaceholderView.uncover(animated: true)
                
                if (status == .success){
                    
                    let allDocumentList = result.arrayValue
                    for document in allDocumentList{
                        let model = DocumentCategoryModel()
                        model.updateModelWithJSON(json: document)
                        if (model.catName == "Assets"){
                            self.assets = model
                        }
                        else if (model.catName == "Income"){
                            self.income = model
                        }
                        else if (model.catName == "Liabilities"){
                            self.liabilities = model
                        }
                        else if (model.catName == "Personal"){
                            self.personal = model
                        }
                        else if (model.catName == "Property"){
                            self.property = model
                        }
                        else if (model.catName == "Disclosure"){
                            self.disclosure = model
                        }
                        else if (model.catName == "Other"){
                            self.other = model
                        }
                    }
                    self.setScreenHeight()
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { dismiss in
                        
                    }
                }
            }
            
        }
        
    }
    
}

extension DocumentsListViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        if (tableView == tableViewAssets){
            return selectedTableView == tableViewAssets ? assets.documents.count + 1 : 1
        }
        else if (tableView == tableViewIncome){
            return selectedTableView == tableViewIncome ? income.documents.count + 1 : 1
        }
        else if (tableView == tableViewLiabilities){
            return selectedTableView == tableViewLiabilities ? liabilities.documents.count + 1 : 1
        }
        else if (tableView == tableViewPersonal){
            return selectedTableView == tableViewPersonal ? personal.documents.count + 1 : 1
        }
        else if (tableView == tableViewProperty){
            return selectedTableView == tableViewProperty ? property.documents.count + 1 : 1
        }
        else if (tableView == tableViewDisclosure){
            return selectedTableView == tableViewDisclosure ? disclosure.documents.count + 1 : 1
        }
        else{
            return selectedTableView == tableViewOther ? other.documents.count + 1 : 1
        }
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        if (tableView == tableViewAssets){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = assets.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                cell.counterView.isHidden = assets.documents.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(assets.documents.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let document = assets.documents[indexPath.row - 1]
                cell.lblTemplateName.text = document.docType
                cell.btnCheckbox.setImage(UIImage(named: document.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                return cell
            }
        }
        else if (tableView == tableViewIncome){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = income.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                cell.counterView.isHidden = income.documents.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(income.documents.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let document = income.documents[indexPath.row - 1]
                cell.lblTemplateName.text = document.docType
                cell.btnCheckbox.setImage(UIImage(named: document.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                return cell
            }
        }
        else if (tableView == tableViewLiabilities){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = liabilities.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                cell.counterView.isHidden = liabilities.documents.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(liabilities.documents.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let document = liabilities.documents[indexPath.row - 1]
                cell.lblTemplateName.text = document.docType
                cell.btnCheckbox.setImage(UIImage(named: document.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                return cell
            }
        }
        else if (tableView == tableViewPersonal){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = personal.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                cell.counterView.isHidden = personal.documents.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(personal.documents.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let document = personal.documents[indexPath.row - 1]
                cell.lblTemplateName.text = document.docType
                cell.btnCheckbox.setImage(UIImage(named: document.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                return cell
            }
        }
        else if (tableView == tableViewProperty){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = property.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                cell.counterView.isHidden = property.documents.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(property.documents.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let document = property.documents[indexPath.row - 1]
                cell.lblTemplateName.text = document.docType
                cell.btnCheckbox.setImage(UIImage(named: document.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                return cell
            }
        }
        else if (tableView == tableViewDisclosure){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = disclosure.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                cell.counterView.isHidden = disclosure.documents.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(disclosure.documents.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let document = disclosure.documents[indexPath.row - 1]
                cell.lblTemplateName.text = document.docType
                cell.btnCheckbox.setImage(UIImage(named: document.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                return cell
            }
        }
        else{
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = other.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                cell.counterView.isHidden = other.documents.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(other.documents.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let document = other.documents[indexPath.row - 1]
                cell.lblTemplateName.text = document.docType
                cell.btnCheckbox.setImage(UIImage(named: document.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
//                cell.btnInfo.isHidden = indexPath.row != otherArray.count
//                cell.btnInfo.setImage(UIImage(named: "editIcon"), for: .normal)
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

extension DocumentsListViewController: DocumentsTemplatesTableViewCellDelegate{
    
    func infoTapped(indexPath: IndexPath) {
        
    }
    
    func templateSelect(indexPath: IndexPath, tableView: UITableView) {
        if (selectedTableView == tableViewAssets){
            assets.documents[indexPath.row - 1].isSelected = !assets.documents[indexPath.row - 1].isSelected
            tableViewAssets.reloadData()
        }
        else if (selectedTableView == tableViewIncome){
            income.documents[indexPath.row - 1].isSelected = !income.documents[indexPath.row - 1].isSelected
            tableViewIncome.reloadData()
        }
        else if (selectedTableView == tableViewLiabilities){
            liabilities.documents[indexPath.row - 1].isSelected = !liabilities.documents[indexPath.row - 1].isSelected
            tableViewLiabilities.reloadData()
        }
        else if (selectedTableView == tableViewPersonal){
            personal.documents[indexPath.row - 1].isSelected = !personal.documents[indexPath.row - 1].isSelected
            tableViewPersonal.reloadData()
        }
        else if (selectedTableView == tableViewProperty){
            property.documents[indexPath.row - 1].isSelected = !property.documents[indexPath.row - 1].isSelected
            tableViewProperty.reloadData()
        }
        else if (selectedTableView == tableViewDisclosure){
            disclosure.documents[indexPath.row - 1].isSelected = !disclosure.documents[indexPath.row - 1].isSelected
            tableViewDisclosure.reloadData()
        }
        else if (selectedTableView == tableViewOther){
            other.documents[indexPath.row - 1].isSelected = !other.documents[indexPath.row - 1].isSelected
            tableViewOther.reloadData()
        }
    }
}

extension DocumentsListViewController: UITextFieldDelegate{
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        let vc = Utility.getSearchRequestDocumentVC()
        vc.searchedDocumentName = txtfieldSearch.text!
        vc.assets = self.assets
        vc.income = self.income
        vc.liabilities = self.liabilities
        vc.personal = self.personal
        vc.property = self.property
        vc.disclosure = self.disclosure
        vc.other = self.other
        let navVC = UINavigationController(rootViewController: vc)
        navVC.navigationBar.isHidden = true
        navVC.modalPresentationStyle = .fullScreen
        self.presentVC(vc: navVC)
        return true
    }
    
}
