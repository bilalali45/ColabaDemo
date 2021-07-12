//
//  DocumentsViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/07/2021.
//

import UIKit

class DocumentsViewController: BaseViewController {

    @IBOutlet weak var tblViewDocuments: UITableView!
    var documentsName = ["Bank Statements", "W-2s 2018", "Home Insurance", "Tax Transcripts"]
    var documentsTime = ["Yesterday, 8:32 PM", "22h ago", "", "Saturday, 4:12 PM"]
    
    override func viewDidLoad() {
        super.viewDidLoad()
        tblViewDocuments.register(UINib(nibName: "DocumentsTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentsTableViewCell")
    }
    
}

extension DocumentsViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 4
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentsTableViewCell", for: indexPath) as! DocumentsTableViewCell
        cell.mainView.layer.cornerRadius = 8
        cell.mainView.dropShadow()
        cell.lblDocumentName.text = documentsName[indexPath.row]
        cell.lblTime.text = documentsTime[indexPath.row]
        cell.viewAttatchment1.isHidden = indexPath.row == 2
        cell.viewAttatchment2.isHidden = indexPath.row == 1 || indexPath.row == 2
        cell.viewOtherAttatchment.isHidden = indexPath.row > 0
        cell.iconNoAttatchment.isHidden = indexPath.row != 2
        cell.lblNoAttatchment.isHidden = indexPath.row != 2
        return cell
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return 116
    }
    
}
