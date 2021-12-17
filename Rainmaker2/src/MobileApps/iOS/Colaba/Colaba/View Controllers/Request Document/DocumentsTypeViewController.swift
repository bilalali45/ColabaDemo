//
//  DocumentsTypeViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 04/10/2021.
//

import UIKit

class DocumentsTypeViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var tableViewDocumentType: UITableView!
    @IBOutlet weak var btnNext: ColabaButton!
    
    var selectedDocs = [Doc]()
    
    override func viewDidLoad() {
        super.viewDidLoad()

        selectedDocs = selectedDocsFromTemplate + selectedDocsFromList
        tableViewDocumentType.register(UINib(nibName: "DocumentTypeTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentTypeTableViewCell")
        btnNext.setButton(image: UIImage(named: "NextIcon")!)
    }
    
    override func viewWillAppear(_ animated: Bool) {
        super.viewWillAppear(animated)
        self.tableViewDocumentType.reloadData()
    }
    
    //MARK:- Methods and Actions
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnNextTapped(_ sender: UIButton){
        let vc = Utility.getSendDocumentRequestVC()
        self.pushToVC(vc: vc)
    }
    
}

extension DocumentsTypeViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return selectedDocs.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentTypeTableViewCell", for: indexPath) as! DocumentTypeTableViewCell
        let doc = selectedDocs[indexPath.row]
        cell.lblTitle.text = doc.docType
        cell.lblDetail.text = doc.docMessage
        cell.stackView.spacing = doc.docMessage == "" ? 0 : 15
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let doc = selectedDocs[indexPath.row]
        let vc = Utility.getBankStatementVC()
        vc.selectedDoc = doc
        self.presentVC(vc: vc)
        
    }
    
}
