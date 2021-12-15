//
//  DocumentTemplatesViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 01/10/2021.
//

import UIKit
import LoadingPlaceholderView

class DocumentTemplatesViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewMyTemplates: UITableView!
    @IBOutlet weak var tableViewMyTemplatesHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var tableViewSystemTemplate: UITableView!
    @IBOutlet weak var tableViewSystemTemplateHeightConstraint: NSLayoutConstraint!
    
    var myTemplatesArray = [DocumentTemplateModel]()
    var systemTemplatesArray = [DocumentTemplateModel]()
    
    let loadingPlaceholderView = LoadingPlaceholderView()
    
    var selectedTableView: UITableView?
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setupTableView(tableViews: [tableViewMyTemplates, tableViewSystemTemplate])
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.2){
            self.setScreenHeight()
        }
        getDocumentTemplates()
        
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        
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
            tableView.coverableCellsIdentifiers = ["AssetsHeadingTableViewCell"]
        }
    }
   
    func setScreenHeight(){
        
        let myTemplateHeight = (self.myTemplatesArray.count * 40)
        let myTemplateTableViewHeight = selectedTableView == tableViewMyTemplates ? (myTemplateHeight + 56) : 56
        
        let systemTemplateHeight = (self.systemTemplatesArray.count * 40)
        let systemTemplateTableViewHeight = selectedTableView == tableViewSystemTemplate ? (systemTemplateHeight + 56) : 56
        
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
    
    //MARK:- API's
    
    func getDocumentTemplates(){
        
        if (myTemplatesArray.count == 0 && systemTemplatesArray.count == 0){
            loadingPlaceholderView.cover(self.view, animated: true)
        }
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getDocumentTemplates, method: .get, params: nil) { status, result, message in
            
            DispatchQueue.main.async {
                
                self.loadingPlaceholderView.uncover(animated: true)
                
                if (status == .success){
                    
                    self.myTemplatesArray.removeAll()
                    self.systemTemplatesArray.removeAll()
                    
                    let allTemplates = result.arrayValue
                    for template in allTemplates{
                        let model = DocumentTemplateModel()
                        model.updateModelWithJSON(json: template)
                        if (model.type.localizedCaseInsensitiveContains("Tenant Template")){
                            self.systemTemplatesArray.append(model)
                        }
                        else{
                            self.myTemplatesArray.append(model)
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
                let template = myTemplatesArray[indexPath.row - 1]
                cell.lblTemplateName.text = template.name
                cell.btnCheckbox.setImage(UIImage(named: template.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.indexPath = indexPath
                cell.delegate = self
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
                let template = systemTemplatesArray[indexPath.row - 1]
                cell.lblTemplateName.text = template.name
                cell.btnCheckbox.setImage(UIImage(named: template.isSelected ? "CheckBoxSelected" : "CheckBoxUnSelected"), for: .normal)
                cell.indexPath = indexPath
                cell.delegate = self
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

extension DocumentTemplatesViewController: DocumentsTemplatesTableViewCellDelegate{
    
    func infoTapped(indexPath: IndexPath) {
        let vc = Utility.getCheckListVC()
        vc.selectedTemplate = selectedTableView == tableViewMyTemplates ? myTemplatesArray[indexPath.row - 1] : systemTemplatesArray[indexPath.row - 1]
        self.present(vc, animated: false, completion: nil)
    }
    
    func templateSelect(indexPath: IndexPath) {
        if (selectedTableView == tableViewMyTemplates){
            myTemplatesArray[indexPath.row - 1].isSelected = !myTemplatesArray[indexPath.row - 1].isSelected
            tableViewMyTemplates.reloadData()
        }
        else if (selectedTableView == tableViewSystemTemplate){
            systemTemplatesArray[indexPath.row - 1].isSelected = !systemTemplatesArray[indexPath.row - 1].isSelected
            tableViewSystemTemplate.reloadData()
        }
    }
    
}
