//
//  DocumentsDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/07/2021.
//

import UIKit
import QuickLook
import Alamofire

class DocumentsDetailViewController: UIViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblDocumentName: UILabel!
    @IBOutlet weak var tblViewDocuments: UITableView!
    
    var selectedDocument = LoanDocumentModel()
    lazy var previewItem = NSURL()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        lblDocumentName.text = selectedDocument.docName
        tblViewDocuments.register(UINib(nibName: "DocumentsDetailTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentsDetailTableViewCell")
    }
    
    //MARK:- Methods and Actions
    func showDocumentFile(fileName: String, fileId: String){
        
        Utility.showOrHideLoader(shouldShow: true)
        
        let extraData = "id=\(selectedDocument.id)&requestId=\(selectedDocument.requestId)&docId=\(selectedDocument.docId)&fileId=\(fileId)"
        
        APIRouter.sharedInstance.downloadFileFromRequest(type: .viewLoanDocument, method: .get, params: nil, extraData: extraData) { status, fileData, message in
            
            DispatchQueue.main.async {
                Utility.showOrHideLoader(shouldShow: false)
                
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
                        previewController.navigationItem.title = ""
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
        return selectedDocument.files.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsDetailTableViewCell", for: indexPath) as! DocumentsDetailTableViewCell
        let file = selectedDocument.files[indexPath.row]
        
        cell.lblAttatchmentName.font = file.isRead ? Theme.getRubikRegularFont(size: 15) : Theme.getRubikMediumFont(size: 15)
        cell.lblAttatchmentName.text = file.clientName == "" ? file.mcuName : file.clientName
        cell.lblTime.text = Utility.getDocumentDate(file.fileUploadedOn)
        cell.mainView.layer.cornerRadius = 8
        cell.mainView.dropShadow()
        
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let file = selectedDocument.files[indexPath.row]
        file.isRead = true
        showDocumentFile(fileName: file.clientName == "" ? file.mcuName : file.clientName, fileId: file.fileId)
        self.tblViewDocuments.reloadData()
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 96
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
