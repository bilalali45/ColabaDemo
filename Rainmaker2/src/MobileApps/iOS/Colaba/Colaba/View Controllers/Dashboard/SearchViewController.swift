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
        tblViewSearchResult.loadControl = UILoadControl(target: self, action: #selector(loadMoreResult))
        tblViewSearchResult.loadControl?.heightLimit = 60
        
    }
    
    //MARK:- Actions and Methods
    
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.dismissVC()
    }
    
    @IBAction func btnCrossTapped(_ sender: UIButton) {
        txtFieldSearch.text = ""
        txtFieldSearch.becomeFirstResponder()
        self.searchArray.removeAll()
        self.tblViewSearchResult.reloadData()
        self.lblSearchResults.text = "0 result found"
    }
    
    @objc func loadMoreResult(){
        if (self.searchArray.count % 20 == 0){
            self.pageNumber = self.pageNumber + 1
            self.getSearchData()
        }
        else{
            tblViewSearchResult.loadControl?.endLoading()
        }
    }
    
    func scrollViewDidScroll(_ scrollView: UIScrollView) {
        scrollView.loadControl?.update()
    }
    
    //MARK:- API's
    
    func getSearchData(){
        
        if (self.pageNumber == 1){
            searchArray = [SearchModel(), SearchModel(), SearchModel(), SearchModel(), SearchModel(), SearchModel(), SearchModel()]
            self.tblViewSearchResult.reloadData()
            self.loadingPlaceholderView.cover(tblViewSearchResult, animated: true)
        }
        
        let extraData = "pageNumber=\(pageNumber)&pageSize=20&searchTerm=\(txtFieldSearch.text!)"
        
        APIRouter.sharedInstance.executeDashboardAPIs(type: .searchLoans, method: .get, params: nil, extraData: extraData) { status, result, message in
            
            DispatchQueue.main.async {
                self.loadingPlaceholderView.uncover(animated: true)
                self.tblViewSearchResult.loadControl?.endLoading()
                if (status == .success){
                    
                    if (self.pageNumber == 1){
                        self.searchArray.removeAll()
                        if (result.arrayValue.count > 0){
                            let loanArray = result.arrayValue
                            for loan in loanArray{
                                let model = SearchModel()
                                model.updateModelWithJSON(json: loan)
                                self.searchArray.append(model)
                            }
                        }
                        else{
                            self.showPopup(message: "No data found", popupState: .error, popupDuration: .custom(2)) { reason in
                                
                            }
                        }
                    }
                    else{
                        if (result.arrayValue.count > 0){
                            let loanArray = result.arrayValue
                            for loan in loanArray{
                                let model = SearchModel()
                                model.updateModelWithJSON(json: loan)
                                self.searchArray.append(model)
                            }
                        }
                    }
                    self.lblSearchResults.text = "\(self.searchArray.count) results found"
                    self.tblViewSearchResult.reloadData()
                    
                }
                else{
                    self.showPopup(message: "No data found", popupState: .error, popupDuration: .custom(2)) { reason in
                        
                    }
                }
            }
            
        }
        
    }
}

extension SearchViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return searchArray.count
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        
        let cell = tableView.dequeueReusableCell(withIdentifier: "SearchResultTableViewCell", for: indexPath) as! SearchResultTableViewCell
        let searchData = searchArray[indexPath.row]
        
        cell.lblUsername.text = "\(searchData.firstName) \(searchData.lastName)"
        cell.lblAddress.text = "\(searchData.streetAddress) \(searchData.unitNumber) \(searchData.cityName) \(searchData.stateName) \(searchData.zipCode) \(searchData.countryName)"
        cell.lblApplicationStatus.text = searchData.status
        cell.lblApplicationNumber.text = "\(searchData.loanNumber)"
        
        return cell
    }
}

extension SearchViewController: UITextFieldDelegate{
    
    func textFieldShouldReturn(_ textField: UITextField) -> Bool {
        txtFieldSearch.resignFirstResponder()
        self.pageNumber = 1
        self.searchArray.removeAll()
        self.getSearchData()
        return true
    }
    
}
