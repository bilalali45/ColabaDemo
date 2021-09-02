//
//  DocumentsDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/07/2021.
//

import UIKit
import QuickLook
import Alamofire

class DocumentsDetailViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblDocumentName: UILabel!
    @IBOutlet weak var tblViewDocuments: UITableView!
    @IBOutlet weak var iconNoFiles: UIImageView!
    @IBOutlet weak var lblNoFiles: UILabel!
    
    var selectedDocument = LoanDocumentModel()
    lazy var previewItem = NSURL()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        lblDocumentName.text = selectedDocument.docName
        tblViewDocuments.register(UINib(nibName: "DocumentsDetailTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentsDetailTableViewCell")
        tblViewDocuments.register(UINib(nibName: "DocumentMessageTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentMessageTableViewCell")
        
    }
    
    //MARK:- Methods and Actions
    func showDocumentFile(fileName: String, fileId: String){
        
        let extraData = "id=\(selectedDocument.id)&requestId=\(selectedDocument.requestId)&docId=\(selectedDocument.docId)&fileId=\(fileId)"
        
        APIRouter.sharedInstance.downloadFileFromRequest(type: .viewLoanDocument, method: .get, params: nil, extraData: extraData) { status, fileData, message in
            
            DispatchQueue.main.async {
                
                if (status == .success){
                    if let downloadedFileData = fileData{
                        
                        let documentsURL = FileManager.default.urls(for: .documentDirectory, in: .userDomainMask).first!
                        let fileURL = documentsURL.appendingPathComponent(fileName)
                        do {
                            try downloadedFileData.write(to: fileURL)

                        } catch {
                            print("Something went wrong!")
                        }

                        let downloadedFile = fileURL as NSURL
                        self.previewItem = downloadedFile
                        // Display file
                        let previewController = QLPreviewController()
                        previewController.dataSource = self
                        self.presentVC(vc: previewController)
                    }
                }
                else{
                    self.showPopup(message: message, popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
}

extension DocumentsDetailViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        iconNoFiles.isHidden = selectedDocument.files.count > 0
        lblNoFiles.isHidden = selectedDocument.files.count > 0
        return selectedDocument.message == "" ? selectedDocument.files.count : selectedDocument.files.count + 1
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        if (selectedDocument.message == ""){
            let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsDetailTableViewCell", for: indexPath) as! DocumentsDetailTableViewCell
            let file = selectedDocument.files[indexPath.row]
            
            cell.iconDocument.image = Utility.getDocumentFileTypeIcon(fileName: file.clientName == "" ? file.mcuName : file.clientName)
            cell.lblAttatchmentName.font = file.isRead ? Theme.getRubikRegularFont(size: 15) : Theme.getRubikMediumFont(size: 15)
            cell.lblAttatchmentName.text = file.clientName == "" ? file.mcuName : file.clientName
            cell.lblTime.text = Utility.getDocumentDate(file.fileUploadedOn)
            cell.mainView.layer.cornerRadius = 8
            cell.mainView.dropShadow()
            
            return cell
        }
        else{
            if (indexPath.row == 0){
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentMessageTableViewCell", for: indexPath) as! DocumentMessageTableViewCell
                cell.mainView.layer.cornerRadius = 6
                cell.mainView.layer.borderWidth = 1
                cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
                cell.mainView.dropShadowToCollectionViewCell()
                cell.lblMessage.text = selectedDocument.message
                return cell
            }
            else{
                let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsDetailTableViewCell", for: indexPath) as! DocumentsDetailTableViewCell
                let file = selectedDocument.files[indexPath.row - 1]
                
                cell.iconDocument.image = Utility.getDocumentFileTypeIcon(fileName: file.clientName == "" ? file.mcuName : file.clientName)
                cell.lblAttatchmentName.font = file.isRead ? Theme.getRubikRegularFont(size: 15) : Theme.getRubikMediumFont(size: 15)
                cell.lblAttatchmentName.text = file.clientName == "" ? file.mcuName : file.clientName
                cell.lblTime.text = Utility.getDocumentDate(file.fileUploadedOn)
                cell.mainView.layer.cornerRadius = 8
                cell.mainView.dropShadow()
                
                return cell
            }
        }
        
        
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        if (selectedDocument.message == ""){
            let file = selectedDocument.files[indexPath.row]
            file.isRead = true
            showDocumentFile(fileName: file.clientName == "" ? file.mcuName : file.clientName, fileId: file.fileId)
            self.tblViewDocuments.reloadData()
        }
        else{
            if (indexPath.row != 0){
                let file = selectedDocument.files[indexPath.row - 1]
                file.isRead = true
                showDocumentFile(fileName: file.clientName == "" ? file.mcuName : file.clientName, fileId: file.fileId)
                self.tblViewDocuments.reloadData()
            }
        }
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        
        if (selectedDocument.message == ""){
            return 96
        }
        else{
            if (indexPath.row == 0){
                return UITableView.automaticDimension
            }
            else{
                return 96
            }
        }
        
        
    }
}

extension DocumentsDetailViewController: QLPreviewControllerDelegate, QLPreviewControllerDataSource{
    
    func numberOfPreviewItems(in controller: QLPreviewController) -> Int {
        return 1
    }
    
    func previewController(_ controller: QLPreviewController, previewItemAt index: Int) -> QLPreviewItem {
        return self.previewItem as QLPreviewItem
    }
}
