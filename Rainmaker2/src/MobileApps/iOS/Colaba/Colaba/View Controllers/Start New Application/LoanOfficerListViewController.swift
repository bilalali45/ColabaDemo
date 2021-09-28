//
//  LoanOfficerListViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 27/09/2021.
//

import UIKit

class LoanOfficerListViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var collectionView: UICollectionView!
    var isForPopup = false
    var selectedIndex: IndexPath?
    
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
        return isForPopup ? 4 : 14
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "LoanOfficerCollectionViewCell", for: indexPath) as! LoanOfficerCollectionViewCell
        cell.userImageView.image = UIImage(named: indexPath.row % 2 == 0 ? "LoanOfficer1" : "LoanOfficer2")
        cell.lblUsername.text = indexPath.row % 2 == 0 ? "John Doe" : "Jenny Dan"
        cell.lblTenant.text = indexPath.row % 2 == 0 ? "Texas Trust Home" : "AHC Lending"
        cell.selectedView.isHidden = selectedIndex != indexPath
        if isForPopup && indexPath.row == 3{
            cell.lblUsername.text = "See more"
            cell.lblTenant.text = ""
            cell.userImageView.image = nil
            cell.userImageView.backgroundColor = Theme.getDashboardBackgroundColor()
            cell.seeMoreImage.isHidden = false
        }
        else{
            cell.lblUsername.text = indexPath.row % 2 == 0 ? "John Doe" : "Jenny Dan"
            cell.lblTenant.text = indexPath.row % 2 == 0 ? "Texas Trust Home" : "AHC Lending"
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
            NotificationCenter.default.post(name: NSNotification.Name(rawValue: kNotificationLoanOfficerSelected), object: nil)
            if (!isForPopup){
                self.dismissVC()
            }
            
        }
    }
}
