//
//  LoanOfficerListViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 27/09/2021.
//

import UIKit
import SDWebImage

class LoanOfficerListViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var collectionView: UICollectionView!
    var isForPopup = false
    var selectedIndex: IndexPath?
    var selectedRole = LoanOfficersModel()
    var isForSearch = false
    var searchedMCUs = [Mcu]()
    
    override func viewDidLoad() {
        super.viewDidLoad()
        collectionView.register(UINib(nibName: "LoanOfficerCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "LoanOfficerCollectionViewCell")
        if (isForPopup){
            self.view.backgroundColor = .clear
        }
        NotificationCenter.default.addObserver(self, selector: #selector(showSearchResult(notification:)), name: NSNotification.Name(rawValue: kNotificationSearchOfficers), object: nil)
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        setupCollectionView()
    }

    //MARK:- Methods and Actions
    
    func setupCollectionView(){
        let collectionViewLayout = UICollectionViewFlowLayout()
        collectionViewLayout.scrollDirection = .vertical
        collectionViewLayout.sectionInset = UIEdgeInsets(top: 30, left: 0, bottom: 0, right: 0)
        collectionViewLayout.minimumLineSpacing = 30
        collectionViewLayout.minimumInteritemSpacing = 0
        let itemWidth = self.view.frame.width / 4
        collectionViewLayout.itemSize = CGSize(width: itemWidth, height: 108)
        self.collectionView.collectionViewLayout = collectionViewLayout
        self.collectionView.dataSource = self
        self.collectionView.delegate = self
        self.collectionView.reloadData()
        self.collectionView.isScrollEnabled = !isForPopup
    }
    
    @objc func showSearchResult(notification: Notification){
        if let searchUserInfo = notification.userInfo{
            if let searchText = searchUserInfo["searchText"] as? String{
                if (!isForPopup){
                    if (searchText == ""){
                        isForSearch = false
                        searchedMCUs.removeAll()
                        self.collectionView.reloadData()
                    }
                    else{
                        isForSearch = true
                        searchedMCUs = selectedRole.mcus.filter({$0.fullName.localizedCaseInsensitiveContains(searchText)})
                        self.collectionView.reloadData()
                    }
                    
                }
            }
        }
    }
    
}

extension LoanOfficerListViewController: UICollectionViewDataSource, UICollectionViewDelegate{
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        
        if (isForPopup){
            return selectedRole.mcus.count > 3 ? 4 : selectedRole.mcus.count
        }
        else{
            return isForSearch ? searchedMCUs.count : selectedRole.mcus.count
        }
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "LoanOfficerCollectionViewCell", for: indexPath) as! LoanOfficerCollectionViewCell
        cell.selectedView.isHidden = selectedIndex != indexPath
        if isForPopup && indexPath.row == 3{
            cell.lblUsername.text = "See more"
            cell.lblTenant.text = ""
            cell.userImageView.image = nil
            cell.userImageView.backgroundColor = Theme.getDashboardBackgroundColor()
            cell.seeMoreImage.isHidden = false
        }
        else{
            let mcu = isForSearch ? searchedMCUs[indexPath.row] : selectedRole.mcus[indexPath.row]
            cell.userImageView.sd_setImage(with: URL(string: mcu.profileimageurl), completed: nil)
            cell.lblUsername.text = mcu.fullName
            cell.lblTenant.text = mcu.branchName
            cell.seeMoreImage.isHidden = true
        }
        return cell
    }
    
    func collectionView(_ collectionView: UICollectionView, didSelectItemAt indexPath: IndexPath) {
        selectedIndex = indexPath
        self.collectionView.reloadData()
        if (isForPopup && indexPath.row == 3){
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationLoanOfficerSeeMoreTapped), object: nil)
        }
        else{
            let mcu = isForSearch ? searchedMCUs[indexPath.row] : selectedRole.mcus[indexPath.row]
            let selectedMCU = ["branchId": mcu.branchId,
                               "branchName": mcu.branchName,
                               "userId": mcu.userId,
                               "fullName": mcu.fullName,
                               "profileimageurl": mcu.profileimageurl] as [String: Any]
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationLoanOfficerSelected), object: nil, userInfo: selectedMCU)
            if (!isForPopup){
                self.dismissVC()
            }
            
        }
    }
}
