//
//  DocumentsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/07/2021.
//

import UIKit
import LoadingPlaceholderView
import QuickLook

class DocumentsViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var allView: UIView!
    @IBOutlet weak var draftView: UIView!
    @IBOutlet weak var borrowerToDoView: UIView!
    @IBOutlet weak var pendingView: UIView!
    @IBOutlet weak var startedView: UIView!
    @IBOutlet weak var completedView: UIView!
    @IBOutlet weak var manuallyView: UIView!
    @IBOutlet weak var tblViewDocuments: UITableView!
    @IBOutlet weak var iconNoDocument: UIImageView!
    @IBOutlet weak var lblNoDocuments: UILabel!
    @IBOutlet weak var btnRequestDocuments: UIButton!
    
    var loanApplicationId = 0
    var documentsArray = [LoanDocumentModel]()
    var filterDocumentsArray = [LoanDocumentModel]()
    let loadingPlaceholderView = LoadingPlaceholderView()
    var isFiltersApplied = false
    var lastContentOffset: CGFloat = 0
    
    lazy var previewItem = NSURL()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        btnRequestDocuments.isHidden = true
        btnRequestDocuments.layer.borderWidth = 1
        btnRequestDocuments.layer.cornerRadius = 5
        btnRequestDocuments.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        tblViewDocuments.contentInset = UIEdgeInsets(top: -20, left: 0, bottom: 120, right: 0)
        tblViewDocuments.register(UINib(nibName: "DocumentsTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentsTableViewCell")
        tblViewDocuments.coverableCellsIdentifiers = ["DocumentsTableViewCell", "DocumentsTableViewCell", "DocumentsTableViewCell", "DocumentsTableViewCell", "DocumentsTableViewCell", "DocumentsTableViewCell", "DocumentsTableViewCell"]
        roundAllFilterViews(filterViews: [allView, draftView, borrowerToDoView, pendingView, startedView, completedView, manuallyView])
        filterViewTapped(selectedFilterView: allView, filterViews: [allView, draftView, borrowerToDoView, pendingView, startedView, completedView, manuallyView])
        allView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(allFitersTapped)))
        draftView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(draftFilterTapped)))
        borrowerToDoView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(borrowerFilterTapped)))
        pendingView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(pendingFilterTapped)))
        startedView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(startedFilterTapped)))
        completedView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(completedFilterTapped)))
        manuallyView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(manuallyAddedFilterTapped)))
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        getDocuments()
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowNavigationBar), object: nil, userInfo: nil)
        NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowRequestDocumentFooterButton), object: nil, userInfo: nil)
    }
    
    
    //MARK:- Methods and Actions
    
    func roundAllFilterViews(filterViews: [UIView]){
        for filterView in filterViews{
            filterView.layer.cornerRadius = 15
        }
    }
    
    func filterViewTapped(selectedFilterView: UIView, filterViews: [UIView]){
        for filterView in filterViews{
            if (filterView == selectedFilterView){
                filterView.backgroundColor = .white
                filterView.layer.borderWidth = 1
                filterView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            }
            else{
                filterView.backgroundColor = Theme.getButtonGreyColor().withAlphaComponent(0.5)
                filterView.layer.borderWidth = 0
            }
        }
        
        isFiltersApplied = selectedFilterView != allView
        
        if (selectedFilterView == allView){
            filterDocumentsArray.removeAll()
        }
        else if (selectedFilterView == draftView){
            filterDocumentsArray = documentsArray.filter{$0.status == "In draft"}
        }
        else if (selectedFilterView == borrowerToDoView){
            filterDocumentsArray = documentsArray.filter{$0.status == "Borrower to do"}
        }
        else if (selectedFilterView == pendingView){
            filterDocumentsArray = documentsArray.filter{$0.status == "Pending review"}
        }
        else if (selectedFilterView == startedView){
            filterDocumentsArray = documentsArray.filter{$0.status == "Started"}
        }
        else if (selectedFilterView == completedView){
            filterDocumentsArray = documentsArray.filter{$0.status == "Completed"}
        }
        else if (selectedFilterView == manuallyView){
            filterDocumentsArray = documentsArray.filter{$0.status == "Manually added"}
        }
        self.tblViewDocuments.reloadData()
    }
    
    @objc func allFitersTapped(){
        filterViewTapped(selectedFilterView: allView, filterViews: [allView, draftView, borrowerToDoView, pendingView, startedView, completedView, manuallyView])
    }
    
    @objc func draftFilterTapped(){
        filterViewTapped(selectedFilterView: draftView, filterViews: [allView, draftView, borrowerToDoView, pendingView, startedView, completedView, manuallyView])
    }
    
    @objc func borrowerFilterTapped(){
        filterViewTapped(selectedFilterView: borrowerToDoView, filterViews: [allView, draftView, borrowerToDoView, pendingView, startedView, completedView, manuallyView])
    }
    
    @objc func pendingFilterTapped(){
        filterViewTapped(selectedFilterView: pendingView, filterViews: [allView, draftView, borrowerToDoView, pendingView, startedView, completedView, manuallyView])
    }
    
    @objc func startedFilterTapped(){
        filterViewTapped(selectedFilterView: startedView, filterViews: [allView, draftView, borrowerToDoView, pendingView, startedView, completedView, manuallyView])
    }
    
    @objc func completedFilterTapped(){
        filterViewTapped(selectedFilterView: completedView, filterViews: [allView, draftView, borrowerToDoView, pendingView, startedView, completedView, manuallyView])
    }
    
    @objc func manuallyAddedFilterTapped(){
        filterViewTapped(selectedFilterView: manuallyView, filterViews: [allView, draftView, borrowerToDoView, pendingView, startedView, completedView, manuallyView])
    }
    
    @IBAction func btnRequestDocuments(_ sender: UIButton) {
        let vc = Utility.getRequestDocumentVC()
        let navVC = UINavigationController(rootViewController: vc)
        navVC.modalPresentationStyle = .fullScreen
        navVC.navigationBar.isHidden = true
        self.presentVC(vc: navVC)
    }
    
    //MARK:- API's
    
    func getDocuments(){
        
        if (documentsArray.count == 0){
            self.view.isUserInteractionEnabled = false
            self.loadingPlaceholderView.cover(tblViewDocuments, animated: true)
            
        }
        
        let extraData = "loanApplicationId=\(loanApplicationId)&pending=true"
        
        APIRouter.sharedInstance.executeAPI(type: .getLoanDocuments, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                self.loadingPlaceholderView.uncover()
                self.view.isUserInteractionEnabled = true
                //self.btnRequestDocuments.isHidden = result.arrayValue.count > 0
//                NotificationCenter.default.post(name: NSNotification.Name(rawValue: result.arrayValue.count > 0 ? kNotificationShowRequestDocumentFooterButton : kNotificationHideRequestDocumentFooterButton), object: nil, userInfo: nil)
                
                if (status == .success){
                    
                    if (result.arrayValue.count == 0){
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
                    self.tblViewDocuments.reloadData()
                    
                }
            }
            
        }
    }
    
    func showDocumentFile(documentId: String ,requestId: String, docId: String, fileName: String, fileId: String){
        
        let extraData = "id=\(documentId)&requestId=\(requestId)&docId=\(docId)&fileId=\(fileId)"
        
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
    
}

extension DocumentsViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        
        if (isFiltersApplied){
            iconNoDocument.isHidden = filterDocumentsArray.count > 0
            lblNoDocuments.isHidden = filterDocumentsArray.count > 0
            return filterDocumentsArray.count
        }
        else{
            iconNoDocument.isHidden = documentsArray.count > 0
            lblNoDocuments.isHidden = documentsArray.count > 0
            return documentsArray.count
        }
        
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTableViewCell", for: indexPath) as! DocumentsTableViewCell
        let document = isFiltersApplied ? filterDocumentsArray[indexPath.row] : documentsArray[indexPath.row]
        
        cell.mainView.layer.cornerRadius = 8
        cell.mainView.dropShadow()
        cell.indexPath = indexPath
        cell.delegate = self
        
        let files = document.files.sorted { file1, file2 in
            return file1.fileUploadedTimeStamp > file2.fileUploadedTimeStamp
        }
        cell.lblDocumentName.text = document.docName
        cell.lblStatus.text = document.status
        cell.iconStatus.image = Utility.getDocumentStatusIcon(documentStatus: document.status)
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
        
        if let file1 = document.files.first{
            cell.lblAttatchment1.text = file1.clientName == "" ? file1.mcuName : file1.clientName
            cell.iconAttatchment1.image = Utility.getDocumentFileTypeIcon(fileName: file1.clientName == "" ? file1.mcuName : file1.clientName)
        }
        else{
            cell.lblAttatchment1.text = ""
            cell.iconAttatchment1.image = nil
        }
        
        if document.files.count > 1{
            cell.lblAttachment2.text = document.files[1].clientName == "" ? document.files[1].mcuName : document.files[1].clientName
            cell.iconAttachment2.image = Utility.getDocumentFileTypeIcon(fileName: document.files[1].clientName == "" ? document.files[1].mcuName : document.files[1].clientName)
        }
        else{
            cell.lblAttachment2.text = ""
            cell.iconAttachment2.image = nil
        }
        
        let remainingCount = document.files.count - 2
        cell.lblOtherAttatchment.text = "+\(remainingCount)"
        
        cell.lblDocumentName.font = document.files.filter{$0.isRead == false}.count > 0 ? Theme.getRubikMediumFont(size: 17) : Theme.getRubikRegularFont(size: 17)
        
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        
        let document = isFiltersApplied ? filterDocumentsArray[indexPath.row] : documentsArray[indexPath.row]
        let vc = Utility.getDocumentsDetailVC()
        vc.selectedDocument = document
        self.pushToVC(vc: vc)
        
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return UITableView.automaticDimension
    }
    
    func scrollViewWillBeginDragging(_ scrollView: UIScrollView) {
        self.lastContentOffset = scrollView.contentOffset.y
    }

    func scrollViewDidScroll(_ scrollView: UIScrollView) {
        
        if (documentsArray.count > 3){
            if self.lastContentOffset < scrollView.contentOffset.y {
                NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationHidesNavigationBar), object: nil, userInfo: nil)
            } else if self.lastContentOffset > scrollView.contentOffset.y {
                NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationShowNavigationBar), object: nil, userInfo: nil)
            } else {
                // didn't move
            }
        }
        
    }
}

extension DocumentsViewController: DocumentsTableViewCellDelegate{
    func attatchmentTapped(indexPath: IndexPath, fileIndex: Int) {
        
        let document = isFiltersApplied ? filterDocumentsArray[indexPath.row] : documentsArray[indexPath.row]
        let files = document.files.sorted { file1, file2 in
            return file1.fileUploadedTimeStamp > file2.fileUploadedTimeStamp
        }
        showDocumentFile(documentId: document.id, requestId: document.requestId, docId: document.docId, fileName: files[fileIndex].clientName == "" ? files[fileIndex].mcuName : files[fileIndex].clientName, fileId: files[fileIndex].fileId)
    }
}

extension DocumentsViewController: QLPreviewControllerDelegate, QLPreviewControllerDataSource{
    
    func numberOfPreviewItems(in controller: QLPreviewController) -> Int {
        return 1
    }
    
    func previewController(_ controller: QLPreviewController, previewItemAt index: Int) -> QLPreviewItem {
        return self.previewItem as QLPreviewItem
    }
}
