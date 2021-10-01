//
//  DocumentTemplatesViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/10/2021.
//

import UIKit

class DocumentTemplatesViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewMyTemplates: UITableView!
    @IBOutlet weak var tableViewMyTemplatesHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewSystemTemplate: UITableView!
    @IBOutlet weak var tableViewSystemTemplateHeightConstraint: NSLayoutConstraint!
    
    let myTemplatesArray = ["Income Template", "My Standard Checklist", "Assets template"]
    let systemTemplatesArray = ["FHA Full Doc Refinance - W2", "VA Cash Out - W-2", "FHA Full Doc Refinance", "Conventional Refinance - SE", "VA Purchase - W-2", "Additional Questions", "Auto Loan", "Construction Loan-Phase 1"]
    
    var selectedTableView: UITableView?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupTableView(tableViews: [tableViewMyTemplates, tableViewSystemTemplate])
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2){
            self.setScreenHeight()
        }
        
    }
    
    //MARK:- Methods
    
    func setupTableView(tableViews: [UITableView]){
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
    }
   
    func setScreenHeight(){
        
        let myTemplateTableViewHeight = selectedTableView == tableViewMyTemplates ? 174 : 56
        let systemTemplateTableViewHeight = selectedTableView == tableViewSystemTemplate ? 376 : 56
        
        let totalHeight = myTemplateTableViewHeight + systemTemplateTableViewHeight + 100
        
        tableViewMyTemplatesHeightConstraint.constant = CGFloat(myTemplateTableViewHeight)
        tableViewSystemTemplateHeightConstraint.constant = CGFloat(systemTemplateTableViewHeight)
        
        self.mainViewHeightConstraint.constant = CGFloat(totalHeight)
        
        UIView.animate(withDuration: 0.0) {
            self.view.layoutIfNeeded()
            self.tableViewMyTemplates.reloadData()
            self.tableViewSystemTemplate.reloadData()
        }
    }
}

extension DocumentTemplatesViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        if (tableView == tableViewMyTemplates){
            return selectedTableView == tableViewMyTemplates ? myTemplatesArray.count + 1 : 1
        }
        else{
            return selectedTableView == tableViewSystemTemplate ? systemTemplatesArray.count + 1 : 1
        }
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        if (tableView == tableViewMyTemplates){
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "My Templates"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                return cell 
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = myTemplatesArray[indexPath.row - 1]
                return cell
            }
        }
        else{
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "AssetsHeadingTableViewCell", for: indexPath) as! AssetsHeadingTableViewCell
                cell.lblTitle.text = "System Templates"
                cell.lblAmount.text = ""
                cell.imageArrow.image = UIImage(named: selectedTableView == tableView ? "AssetsUpArrow" : "AssetsDownArrow")
                cell.separatorView.isHidden = selectedTableView != tableView
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTemplatesTableViewCell", for: indexPath) as! DocumentsTemplatesTableViewCell
                cell.lblTemplateName.text = systemTemplatesArray[indexPath.row - 1]
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
        return indexPath.row == 0 ? 56 : 40
    }
}
