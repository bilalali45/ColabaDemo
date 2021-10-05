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
    
    let titleArray = ["Earnest Money Deposit", "Bank Statements", "Profit and Loss Statement", "Form 1099 (Miscellaneous Income)", "Earnest Money Deposit", "Financial Statements"]
    let detailArray = ["", "Please provide 2 most recent month's bank statement with sufficient funds for cash to close.", "", "Please provide the extra income evidence document.", "", ""]
    
    override func viewDidLoad() {
        super.viewDidLoad()

        tableViewDocumentType.register(UINib(nibName: "DocumentTypeTableViewCell", bundle: nil), forCellReuseIdentifier: "DocumentTypeTableViewCell")
        btnNext.setButton(image: UIImage(named: "NextIcon")!)
    }
    
    //MARK:- Methods and Actions
    
    @IBAction func btnBackTapped(_ sender: UIButton){
        self.goBack()
    }
    
    @IBAction func btnNextTapped(_ sender: UIButton){
        
    }
    
}

extension DocumentsTypeViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return titleArray.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "DocumentTypeTableViewCell", for: indexPath) as! DocumentTypeTableViewCell
        cell.lblTitle.text = titleArray[indexPath.row]
        cell.lblDetail.text = detailArray[indexPath.row]
        cell.stackView.spacing = detailArray[indexPath.row] == "" ? 0 : 15
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        if (indexPath.row == 1){
            let vc = Utility.getBankStatementVC()
            self.presentVC(vc: vc)
        }
        else{
            self.goBack()
        }
        
    }
    
}
