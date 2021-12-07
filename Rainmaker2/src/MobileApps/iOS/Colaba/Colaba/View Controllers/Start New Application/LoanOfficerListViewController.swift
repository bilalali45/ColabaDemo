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
    
    override func viewDidLoad() {
        super.viewDidLoad()
        collectionView.register(UINib(nibName: "LoanOfficerCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "LoanOfficerCollectionViewCell")
        if (isForPopup){
            self.view.backgroundColor = .clear
        }
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
    
}

extension LoanOfficerListViewController: UICollectionViewDataSource, UICollectionViewDelegate{
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        
        if (isForPopup){
            return selectedRole.mcus.count > 3 ? 4 : selectedRole.mcus.count
        }
        else{
            return selectedRole.mcus.count
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
            let mcu = selectedRole.mcus[indexPath.row]
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
            let selectedMCU = ["branchId": selectedRole.mcus[indexPath.row].branchId,
                               "branchName": selectedRole.mcus[indexPath.row].branchName,
                               "userId": selectedRole.mcus[indexPath.row].userId,
                               "fullName": selectedRole.mcus[indexPath.row].fullName,
                               "profileimageurl": selectedRole.mcus[indexPath.row].profileimageurl] as [String: Any]
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationLoanOfficerSelected), object: nil, userInfo: selectedMCU)
            if (!isForPopup){
                self.dismissVC()
            }
            
        }
    }
}
