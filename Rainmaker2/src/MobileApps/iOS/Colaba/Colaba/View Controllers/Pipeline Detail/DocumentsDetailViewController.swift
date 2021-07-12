//
//  DocumentsDetailViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 12/07/2021.
//

import UIKit

class DocumentsDetailViewController: UIViewController {

    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblDocumentName: UILabel!
    @IBOutlet weak var tblViewDocuments: UITableView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        tblViewDocuments.register(UINib(nibName: "DocumentsDetailTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentsDetailTableViewCell")
    }
    
    //MARK:- Methods and Actions
    
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
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 96
    }
}
