//
//  DocumentsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/07/2021.
//

import UIKit
import LoadingPlaceholderView

class DocumentsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var tblViewDocuments: UITableView!
    
    var loanApplicationId = 0
    var documentsArray = [LoanDocumentModel]()
    let loadingPlaceholderView = LoadingPlaceholderView()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        tblViewDocuments.register(UINib(nibName: "DocumentsTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentsTableViewCell")
        tblViewDocuments.coverableCellsIdentifiers = ["DocumentsTableViewCell", "DocumentsTableViewCell", "DocumentsTableViewCell", "DocumentsTableViewCell", "DocumentsTableViewCell", "DocumentsTableViewCell", "DocumentsTableViewCell"]
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        getDocuments()
    }
    
    //MARK:- API's
    
    func getDocuments(){
        
        self.loadingPlaceholderView.cover(tblViewDocuments, animated: true)
        
        let extraData = "loanApplicationId=\(loanApplicationId)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .getLoanDocuments, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                self.loadingPlaceholderView.uncover()
                
                if (status == .success){
                    
                    if (result.arrayValue.count == 0){
                        self.showPopup(message: "No documents found", popupState: .error, popupDuration: .custom(2)) { reason in
                            
                        }
                        return
                    }
                    
                    self.documentsArray.removeAll()
                    let documentsArray = result.arrayValue
                    for document in documentsArray{
                        let documentModel = LoanDocumentModel()
                        documentModel.updateModelWithJSON(json: document)
                        self.documentsArray.append(documentModel)
                    }
                    self.tblViewDocuments.reloadData()
                }
                else if (status == .internetError){
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
                else{
                    self.showPopup(message: "No documents found", popupState: .error, popupDuration: .custom(2)) { reason in
                        
                    }
                }
            }
            
        }
    }
    
}

extension DocumentsViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return documentsArray.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTableViewCell", for: indexPath) as! DocumentsTableViewCell
        let document = documentsArray[indexPath.row]
        
        cell.mainView.layer.cornerRadius = 8
        cell.mainView.dropShadow()
        
        let files = document.files.sorted { file1, file2 in
            return file1.fileUploadedTimeStamp > file2.fileUploadedTimeStamp
        }
        cell.lblDocumentName.text = document.docName
        if (files.count > 0){
            cell.lblTime.text = Utility.timeAgoSince(files.first!.fileUploadedOn)
        }
        else{
            cell.lblTime.text = ""
        }
        cell.viewAttatchment1.isHidden = document.files.count == 0
        cell.viewAttatchment2.isHidden = document.files.count < 2
        cell.viewOtherAttatchment.isHidden = document.files.count < 3
        cell.iconNoAttatchment.isHidden = document.files.count > 0
        cell.lblNoAttatchment.isHidden = document.files.count > 0
        cell.lblTime.isHidden = document.files.count == 0
        
        if let file1 = document.files.first{
            cell.lblAttatchment1.text = file1.clientName == "" ? file1.mcuName : file1.clientName
        }
        else{
            cell.lblAttatchment1.text = ""
        }
        
        if document.files.count > 1{
            cell.lblAttachment2.text = document.files[1].clientName == "" ? document.files[1].mcuName : document.files[1].clientName
        }
        else{
            cell.lblAttachment2.text = ""
        }
        
        let remainingCount = document.files.count - 2
        cell.lblOtherAttatchment.text = "+\(remainingCount)"
        
        cell.lblDocumentName.font = document.files.filter{$0.isRead == false}.count > 0 ? Theme.getRubikMediumFont(size: 17) : Theme.getRubikRegularFont(size: 17)
        
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        let document = documentsArray[indexPath.row]
        if (document.files.count > 0){
            let vc = Utility.getDocumentsDetailVC()
            vc.selectedDocument = document
            self.pushToVC(vc: vc)
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 116
    }
    
}
