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
    
    override func viewDidLoad() {
        super.viewDidLoad()

        setupTableViewsAndViews(tableViews: [tableViewAssets, tableViewIncome, tableViewLiabilities, tableViewPersonal, tableViewProperty, tableViewDisclosure, tableViewOther])
        txtfieldSearch.addTarget(self, action: #selector(textfieldSearchEditingChanged), for: .editingChanged)
//        tableViewAssets.isHidden = true
//        tableViewOther.isHidden = true
//        searchResultSeparator.isHidden = false
//        lblNoResult.isHidden = false
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

        let assetsTableViewHeight = (tableViewAssets.contentSize.height)
        let incomeTableViewHeight = (tableViewIncome.contentSize.height)
        let liabilityTableViewHeight = (tableViewLiabilities.contentSize.height)
        let personalTableViewHeight = (tableViewPersonal.contentSize.height)
        let propertyTableViewHeight = (tableViewProperty.contentSize.height)
        let disclosureTableViewHeight = (tableViewDisclosure.contentSize.height)
        let otherTableViewHeight = (tableViewOther.contentSize.height)
        
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
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnCloseTapped(_ sender: UIButton) {
        txtfieldSearch.text = ""
        btnClose.isHidden = true
    }

    @IBAction func btnNextTapped(_ sender: UIButton) {
        let vc = Utility.getDocumentsTypeViewController()
        self.pushToVC(vc: vc)
    }
}

extension SearchRequestDocumentViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        if (tableView == tableViewAssets){
            return 2
        }
        else if (tableView == tableViewIncome){
            return 0
        }
        else if (tableView == tableViewLiabilities){
            return 0
        }
        else if (tableView == tableViewPersonal){
            return 0
        }
        else if (tableView == tableViewProperty){
            return 0
        }
        else if (tableView == tableViewDisclosure){
            return 0
        }
        else{
            return 2
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
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = "Credit Report"
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
                cell.lblTemplateName.text = "Credit Explanation"
                cell.btnInfo.isHidden = true
                return cell
            }
        }
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return indexPath.row == 0 ? 56 : 40
    }
    
}
