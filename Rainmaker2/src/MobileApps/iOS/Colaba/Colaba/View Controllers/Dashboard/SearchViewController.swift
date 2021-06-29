//
//  SearchViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 16/06/2021.
//

import UIKit
import LoadingPlaceholderView

class SearchViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var searchView: UIView!
    @IBOutlet weak var txtFieldSearch: UITextField!
    @IBOutlet weak var btnCross: UIButton!
    @IBOutlet weak var lblSearchResults: UILabel!
    @IBOutlet weak var tblViewSearchResult: UITableView!
    
    let loadingPlaceholderView = LoadingPlaceholderView()
    var pageNumber = 1
    var searchArray = [SearchModel]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        searchView.layer.cornerRadius = 5
        searchView.layer.borderWidth = 1
        searchView.layer.borderColor = Theme.getSearchBarBorderColor().cgColor
        txtFieldSearch.delegate = self
        tblViewSearchResult.register(UINib(nibName: "SearchResultTableViewCell", bundle: nil), forCellReuseIdentifier: "SearchResultTableViewCell")
        tblViewSearchResult.rowHeight = 100
        tblViewSearchResult.coverableCellsIdentifiers = ["SearchResultTableViewCell", "SearchResultTableViewCell", "SearchResultTableViewCell", "SearchResultTableViewCell", "SearchResultTableViewCell", "SearchResultTableViewCell", "SearchResultTableViewCell"]
        
    }
    
    //MARK:- Methods and Actions
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnCrossTapped(_ sender: UIButton) {
        txtFieldSearch.text = ""
        txtFieldSearch.becomeFirstResponder()
    }
    
    //MARK:- API's
    
    func getSearchData(){
        
        self.loadingPlaceholderView.cover(tblViewSearchResult, animated: true)
        
        let extraData = "pageNumber=\(pageNumber)&pageSize=20&searchTerm=\(txtFieldSearch.text!)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .searchLoans, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                self.loadingPlaceholderView.uncover(animated: true)
                if (status == .success){
                    
                    self.searchArray.removeAll()
                    
                    if (result.arrayValue.count > 0){
                        let loanArray = result.arrayValue
                        for loan in loanArray{
                            let model = SearchModel()
                            model.updateModelWithJSON(json: loan)
                            self.searchArray.append(model)
                        }
                    }
                    self.lblSearchResults.text = "\(result.arrayValue.count) results found"
                    self.tblViewSearchResult.reloadData()
                    
                }
                else{
                    self.showPopup(message: "No data found", popupState: .error, popupDuration: .custom(5)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
}

extension SearchViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 10
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "SearchResultTableViewCell", for: indexPath) as! SearchResultTableViewCell
        return cell
    }
}

extension SearchViewController: UITextFieldDelegate{
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        txtFieldSearch.resignFirstResponder()
        return true
    }
    
}
