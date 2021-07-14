//
//  DocumentsDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/07/2021.
//

import UIKit
import QuickLook

class DocumentsDetailViewController: UIViewController {

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblDocumentName: UILabel!
    @IBOutlet weak var tblViewDocuments: UITableView!
    
    lazy var previewItem = NSURL()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        tblViewDocuments.register(UINib(nibName: "DocumentsDetailTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentsDetailTableViewCell")
    }
    
    //MARK:- Methods and Actions
    
    func downloadfile(fileURL: String, completion: @escaping (_ success: Bool,_ fileLocation: URL?) -> Void){
        
        if let itemUrl = URL(string: fileURL){
            let fileNameWithExtension = itemUrl.lastPathComponent
            
            // then lets create your document folder url
            let documentsDirectoryURL =  FileManager.default.urls(for: .documentDirectory, in: .userDomainMask).first!
            
            // lets create your destination file url
            let destinationUrl = documentsDirectoryURL.appendingPathComponent(fileNameWithExtension)
            
            // to check if it exists before downloading it
            if FileManager.default.fileExists(atPath: destinationUrl.path) {
                debugPrint("The file already exists at path")
                completion(true, destinationUrl)
                
                // if the file doesn't exist
            } else {
                
                // you can use NSURLSession.sharedSession to download the data asynchronously
                URLSession.shared.downloadTask(with: itemUrl, completionHandler: { (location, response, error) -> Void in
                    guard let tempLocation = location, error == nil else { return }
                    do {
                        // after downloading your file you need to move it to your destination url
                        try FileManager.default.moveItem(at: tempLocation, to: destinationUrl)
                        print("File moved to documents folder")
                        completion(true, destinationUrl)
                    } catch let error as NSError {
                        print(error.localizedDescription)
                        completion(false, nil)
                    }
                }).resume()
            }
        }
        
    }
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
}

extension DocumentsDetailViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 4
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsDetailTableViewCell", for: indexPath) as! DocumentsDetailTableViewCell
        cell.mainView.layer.cornerRadius = 8
        cell.mainView.dropShadow()
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        downloadfile(fileURL: "https://images.apple.com/environment/pdf/Apple_Environmental_Responsibility_Report_2017.pdf") { success, fileLocationURL in
                    //https://file-examples-com.github.io/uploads/2017/10/file_example_JPG_100kB.jpg
            if success {
                // Set the preview item to display======
                DispatchQueue.main.async {
                    self.previewItem = fileLocationURL! as NSURL
                    // Display file
                    let previewController = QLPreviewController()
                    previewController.dataSource = self
                    //previewController.navigationItem.rightBarButtonItem = UIBarButtonItem()
                    self.presentVC(vc: previewController)
                }
            }else{
                debugPrint("File can't be downloaded")
            }
        }
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
