//
//  SearchRequestDocumentViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 04/10/2021.
//

import UIKit

class SearchRequestDocumentViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var searchView: UIView!
    @IBOutlet weak var searchIcon: UIImageView!
    @IBOutlet weak var txtfieldSearch: UITextField!
    @IBOutlet weak var btnClose: UIButton!
    @IBOutlet weak var searchResultSeparator: UIView!
    @IBOutlet weak var lblNoResult: UILabel!
    @IBOutlet weak var tableViewAssets: UITableView!
    @IBOutlet weak var tableViewAssetsHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewAssetTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewIncome: UITableView!
    @IBOutlet weak var tableViewIncomeHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewIncomeTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewLiabilities: UITableView!
    @IBOutlet weak var tableViewLiabilitiesHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewLiabilityTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewPersonal: UITableView!
    @IBOutlet weak var tableViewPersonalHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewPersonalTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewProperty: UITableView!
    @IBOutlet weak var tableViewPropertyHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewPropertyTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewDisclosure: UITableView!
    @IBOutlet weak var tableViewDisclosureHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewDisclosureTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewOther: UITableView!
    @IBOutlet weak var tableViewOtherHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewOtherTopConstraint: NSLayoutConstraint!
    @IBOutlet weak var btnNext: ColabaButton!
    
    var searchedDocumentName = ""
    
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
        txtfieldSearch.delegate = self
        txtfieldSearch.text = searchedDocumentName
        textfieldSearchEditingChanged()

    }
    
    //MARK:- Methods and Actions
    
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
        
        btnNext.setButton(image: UIImage(named: "NextIcon")!)
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
        
        let searchedAssetDocs = assets.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedIncomeDocs = income.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedLiabilitiesDocs = liabilities.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedPersonalDocs = personal.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedPropertyDocs = property.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedDisclosureDocs = disclosure.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedOtherDocs = other.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})

        tableViewAssetTopConstraint.constant = searchedAssetDocs.count == 0 ? 0 : 10
        tableViewIncomeTopConstraint.constant = searchedIncomeDocs.count == 0 ? 0 : 10
        tableViewLiabilityTopConstraint.constant = searchedLiabilitiesDocs.count == 0 ? 0 : 10
        tableViewPersonalTopConstraint.constant = searchedPersonalDocs.count == 0 ? 0 : 10
        tableViewPropertyTopConstraint.constant = searchedPropertyDocs.count == 0 ? 0 : 10
        tableViewDisclosureTopConstraint.constant = searchedDisclosureDocs.count == 0 ? 0 : 10
        tableViewOtherTopConstraint.constant = searchedOtherDocs.count == 0 ? 0 : 10
        
        tableViewAssets.isHidden = searchedAssetDocs.count == 0
        tableViewIncome.isHidden = searchedIncomeDocs.count == 0
        tableViewLiabilities.isHidden = searchedLiabilitiesDocs.count == 0
        tableViewPersonal.isHidden = searchedPersonalDocs.count == 0
        tableViewProperty.isHidden = searchedPropertyDocs.count == 0
        tableViewDisclosure.isHidden = searchedDisclosureDocs.count == 0
        tableViewOther.isHidden = searchedOtherDocs.count == 0
        
        searchResultSeparator.isHidden = !(searchedAssetDocs.count == 0 && searchedIncomeDocs.count == 0 && searchedLiabilitiesDocs.count == 0 && searchedPersonalDocs.count == 0 && searchedPropertyDocs.count == 0 && searchedDisclosureDocs.count == 0 && searchedOtherDocs.count == 0)
        lblNoResult.isHidden = !(searchedAssetDocs.count == 0 && searchedIncomeDocs.count == 0 && searchedLiabilitiesDocs.count == 0 && searchedPersonalDocs.count == 0 && searchedPropertyDocs.count == 0 && searchedDisclosureDocs.count == 0 && searchedOtherDocs.count == 0)
        lblNoResult.text = "No Results Found for \"\(txtfieldSearch.text!)\" "
        
        let assetsTableViewHeight = (searchedAssetDocs.count == 0 ? 0 : tableViewAssets.contentSize.height + 20)
        let incomeTableViewHeight = (searchedIncomeDocs.count == 0 ? 0 : tableViewIncome.contentSize.height + 20)
        let liabilityTableViewHeight = (searchedLiabilitiesDocs.count == 0 ? 0 : tableViewLiabilities.contentSize.height + 20)
        let personalTableViewHeight = (searchedPersonalDocs.count == 0 ? 0 : tableViewPersonal.contentSize.height + 20)
        let propertyTableViewHeight = (searchedPropertyDocs.count == 0 ? 0 : tableViewProperty.contentSize.height + 20)
        let disclosureTableViewHeight = (searchedDisclosureDocs.count == 0 ? 0 : tableViewDisclosure.contentSize.height + 20)
        let otherTableViewHeight = (searchedOtherDocs.count == 0 ? 0 : tableViewOther.contentSize.height)
        
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
        searchDocument()
    }
    
    func searchDocument(){
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.05) {
            self.setScreenHeight()
        }
    }
    
    func saveSelectedDocs(){
        selectedDocsFromList.removeAll(where: {$0.docTypeId != ""})
        selectedDocsFromList = selectedDocsFromList + assets.documents.filter({$0.isSelected}) + income.documents.filter({$0.isSelected}) + liabilities.documents.filter({$0.isSelected}) + personal.documents.filter({$0.isSelected}) + property.documents.filter({$0.isSelected}) + disclosure.documents.filter({$0.isSelected}) + other.documents.filter({$0.isSelected})
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        txtfieldSearch.text = ""
        btnClose.isHidden = true
        searchDocument()
    }

    @IBAction func btnNextTapped(_ sender: UIButton) {
        let vc = Utility.getDocumentsTypeVC()
        self.pushToVC(vc: vc)
    }
}

extension SearchRequestDocumentViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        let searchedAssetDocs = assets.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedIncomeDocs = income.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedLiabilitiesDocs = liabilities.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedPersonalDocs = personal.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedPropertyDocs = property.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedDisclosureDocs = disclosure.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedOtherDocs = other.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        
        if (tableView == tableViewAssets){
            return searchedAssetDocs.count + 1
        }
        else if (tableView == tableViewIncome){
            return searchedIncomeDocs.count + 1
        }
        else if (tableView == tableViewLiabilities){
            return searchedLiabilitiesDocs.count + 1
        }
        else if (tableView == tableViewPersonal){
            return searchedPersonalDocs.count + 1
        }
        else if (tableView == tableViewProperty){
            return searchedPropertyDocs.count + 1
        }
        else if (tableView == tableViewDisclosure){
            return searchedDisclosureDocs.count + 1
        }
        else{
            return searchedOtherDocs.count + 1
        }
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        if (tableView == tableViewAssets){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = assets.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
                let searchedDocs = assets.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.counterView.isHidden = searchedDocs.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(searchedDocs.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let searchedDocs = assets.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.lblTemplateName.text = searchedDocs[indexPath.row - 1].docType
                cell.btnCheckbox.setImage(UIImage(named: searchedDocs[indexPath.row - 1].isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                cell.tableView = tableViewAssets
                return cell
            }
        }
        else if (tableView == tableViewIncome){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = income.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
                let searchedDocs = income.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.counterView.isHidden = searchedDocs.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(searchedDocs.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let searchedDocs = income.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.lblTemplateName.text = searchedDocs[indexPath.row - 1].docType
                cell.btnCheckbox.setImage(UIImage(named: searchedDocs[indexPath.row - 1].isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                cell.tableView = tableViewIncome
                return cell
            }
        }
        else if (tableView == tableViewLiabilities){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = liabilities.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
                let searchedDocs = liabilities.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.counterView.isHidden = searchedDocs.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(searchedDocs.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let searchedDocs = liabilities.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.lblTemplateName.text = searchedDocs[indexPath.row - 1].docType
                cell.btnCheckbox.setImage(UIImage(named: searchedDocs[indexPath.row - 1].isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                cell.tableView = tableViewLiabilities
                return cell
            }
        }
        else if (tableView == tableViewPersonal){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = personal.catName
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
                let searchedDocs = personal.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.counterView.isHidden = searchedDocs.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(searchedDocs.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let searchedDocs = personal.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.lblTemplateName.text = searchedDocs[indexPath.row - 1].docType
                cell.btnCheckbox.setImage(UIImage(named: searchedDocs[indexPath.row - 1].isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                cell.tableView = tableViewPersonal
                return cell
            }
        }
        else if (tableView == tableViewProperty){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Property"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
                let searchedDocs = property.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.counterView.isHidden = searchedDocs.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(searchedDocs.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let searchedDocs = property.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.lblTemplateName.text = searchedDocs[indexPath.row - 1].docType
                cell.btnCheckbox.setImage(UIImage(named: searchedDocs[indexPath.row - 1].isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                cell.tableView = tableViewProperty
                return cell
            }
        }
        else if (tableView == tableViewDisclosure){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Disclosure"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
                let searchedDocs = disclosure.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.counterView.isHidden = searchedDocs.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(searchedDocs.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let searchedDocs = disclosure.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.lblTemplateName.text = searchedDocs[indexPath.row - 1].docType
                cell.btnCheckbox.setImage(UIImage(named: searchedDocs[indexPath.row - 1].isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                cell.tableView = tableViewDisclosure
                return cell
            }
        }
        else{
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Other"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
                let searchedDocs = other.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.counterView.isHidden = searchedDocs.filter({$0.isSelected}).count == 0
                cell.lblCounter.text = "\(searchedDocs.filter({$0.isSelected}).count)"
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                let searchedDocs = other.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
                cell.lblTemplateName.text = searchedDocs[indexPath.row - 1].docType
                cell.btnCheckbox.setImage(UIImage(named: searchedDocs[indexPath.row - 1].isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.btnInfo.isHidden = true
                cell.indexPath = indexPath
                cell.delegate = self
                cell.tableView = tableViewOther
                return cell
            }
        }
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return indexPath.row == 0 ? 56 : UITableView.automaticDimension
    }
    
}

extension SearchRequestDocumentViewController: DocumentsTemplatesTableViewCellDelegate{
    
    func infoTapped(indexPath: IndexPath) {
        
    }
    
    func templateSelect(indexPath: IndexPath, tableView: UITableView) {
        
        let searchedAssetDocs = assets.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedIncomeDocs = income.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedLiabilitiesDocs = liabilities.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedPersonalDocs = personal.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedPropertyDocs = property.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedDisclosureDocs = disclosure.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        let searchedOtherDocs = other.documents.filter({$0.docType.localizedCaseInsensitiveContains(txtfieldSearch.text!)})
        
        if (tableView == tableViewAssets){
            searchedAssetDocs[indexPath.row - 1].isSelected = !searchedAssetDocs[indexPath.row - 1].isSelected
            tableViewAssets.reloadData()
        }
        else if (tableView == tableViewIncome){
            searchedIncomeDocs[indexPath.row - 1].isSelected = !searchedIncomeDocs[indexPath.row - 1].isSelected
            tableViewIncome.reloadData()
        }
        else if (tableView == tableViewLiabilities){
            searchedLiabilitiesDocs[indexPath.row - 1].isSelected = !searchedLiabilitiesDocs[indexPath.row - 1].isSelected
            tableViewLiabilities.reloadData()
        }
        else if (tableView == tableViewPersonal){
            searchedPersonalDocs[indexPath.row - 1].isSelected = !searchedPersonalDocs[indexPath.row - 1].isSelected
            tableViewPersonal.reloadData()
        }
        else if (tableView == tableViewProperty){
            searchedPropertyDocs[indexPath.row - 1].isSelected = !searchedPropertyDocs[indexPath.row - 1].isSelected
            tableViewProperty.reloadData()
        }
        else if (tableView == tableViewDisclosure){
            searchedDisclosureDocs[indexPath.row - 1].isSelected = !searchedDisclosureDocs[indexPath.row - 1].isSelected
            tableViewDisclosure.reloadData()
        }
        else if (tableView == tableViewOther){
            searchedOtherDocs[indexPath.row - 1].isSelected = !searchedOtherDocs[indexPath.row - 1].isSelected
            tableViewOther.reloadData()
        }
        saveSelectedDocs()
    }
}

extension SearchRequestDocumentViewController: UITextFieldDelegate{
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        searchDocument()
        return true
    }
    
}
