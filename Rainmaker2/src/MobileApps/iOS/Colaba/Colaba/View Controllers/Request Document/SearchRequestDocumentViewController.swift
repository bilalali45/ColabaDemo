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
    
    var assetsArray: [String] = []
    var incomeArray: [String] = []
    var liabilitiesArray: [String] = []
    var personalArray: [String] = []
    var propertyArray: [String] = []
    var disclosureArray: [String] = []
    var otherArray: [String] = []
    
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

        tableViewAssetTopConstraint.constant = assetsArray.count == 0 ? 0 : 10
        tableViewIncomeTopConstraint.constant = incomeArray.count == 0 ? 0 : 10
        tableViewLiabilityTopConstraint.constant = liabilitiesArray.count == 0 ? 0 : 10
        tableViewPersonalTopConstraint.constant = personalArray.count == 0 ? 0 : 10
        tableViewPropertyTopConstraint.constant = propertyArray.count == 0 ? 0 : 10
        tableViewDisclosureTopConstraint.constant = disclosureArray.count == 0 ? 0 : 10
        tableViewOtherTopConstraint.constant = otherArray.count == 0 ? 0 : 10
        
        tableViewAssets.isHidden = assetsArray.count == 0
        tableViewIncome.isHidden = incomeArray.count == 0
        tableViewLiabilities.isHidden = liabilitiesArray.count == 0
        tableViewPersonal.isHidden = personalArray.count == 0
        tableViewProperty.isHidden = propertyArray.count == 0
        tableViewDisclosure.isHidden = disclosureArray.count == 0
        tableViewOther.isHidden = otherArray.count == 0
        
        searchResultSeparator.isHidden = !(assetsArray.count == 0 && incomeArray.count == 0 && liabilitiesArray.count == 0 && personalArray.count == 0 && propertyArray.count == 0 && disclosureArray.count == 0 && otherArray.count == 0)
        lblNoResult.isHidden = !(assetsArray.count == 0 && incomeArray.count == 0 && liabilitiesArray.count == 0 && personalArray.count == 0 && propertyArray.count == 0 && disclosureArray.count == 0 && otherArray.count == 0)
        lblNoResult.text = "No Results Found for \"\(txtfieldSearch.text!)\" "
        
        let assetsTableViewHeight = (assetsArray.count == 0 ? 0 : tableViewAssets.contentSize.height)
        let incomeTableViewHeight = (incomeArray.count == 0 ? 0 : tableViewIncome.contentSize.height)
        let liabilityTableViewHeight = (liabilitiesArray.count == 0 ? 0 : tableViewLiabilities.contentSize.height)
        let personalTableViewHeight = (personalArray.count == 0 ? 0 : tableViewPersonal.contentSize.height)
        let propertyTableViewHeight = (propertyArray.count == 0 ? 0 : tableViewProperty.contentSize.height)
        let disclosureTableViewHeight = (disclosureArray.count == 0 ? 0 : tableViewDisclosure.contentSize.height)
        let otherTableViewHeight = (otherArray.count == 0 ? 0 : tableViewOther.contentSize.height)
        
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
        if (txtfieldSearch.text == ""){
            assetsArray = []
            incomeArray = []
            liabilitiesArray = []
            personalArray = []
            propertyArray = []
            disclosureArray = []
            otherArray = []
        }
        else{
            assetsArray = kAssetsArray.filter{$0.localizedCaseInsensitiveContains(txtfieldSearch.text!)}
            incomeArray = kIncomeArray.filter{$0.localizedCaseInsensitiveContains(txtfieldSearch.text!)}
            liabilitiesArray = kLiabilitiesArray.filter{$0.localizedCaseInsensitiveContains(txtfieldSearch.text!)}
            personalArray = kPersonalArray.filter{$0.localizedCaseInsensitiveContains(txtfieldSearch.text!)}
            propertyArray = kPropertyArray.filter{$0.localizedCaseInsensitiveContains(txtfieldSearch.text!)}
            disclosureArray = kDisclosureArray.filter{$0.localizedCaseInsensitiveContains(txtfieldSearch.text!)}
            otherArray = kOtherArray.filter{$0.localizedCaseInsensitiveContains(txtfieldSearch.text!)}
        }
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.05) {
            self.setScreenHeight()
        }
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
        if (tableView == tableViewAssets){
            return assetsArray.count + 1
        }
        else if (tableView == tableViewIncome){
            return incomeArray.count + 1
        }
        else if (tableView == tableViewLiabilities){
            return liabilitiesArray.count + 1
        }
        else if (tableView == tableViewPersonal){
            return personalArray.count + 1
        }
        else if (tableView == tableViewProperty){
            return propertyArray.count + 1
        }
        else if (tableView == tableViewDisclosure){
            return disclosureArray.count + 1
        }
        else{
            return otherArray.count + 1
        }
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        if (tableView == tableViewAssets){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "Assets"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false

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
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
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
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
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
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
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
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
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
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
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
                cell.imageArrow.image = UIImage(named: "AssetsUpArrow")
                cell.separatorView.isHidden = false
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = otherArray[indexPath.row - 1]
                cell.btnInfo.isHidden = true
                return cell
            }
        }
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return indexPath.row == 0 ? 56 : UITableView.automaticDimension
    }
    
}

extension SearchRequestDocumentViewController: UITextFieldDelegate{
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        searchDocument()
        return true
    }
    
}
