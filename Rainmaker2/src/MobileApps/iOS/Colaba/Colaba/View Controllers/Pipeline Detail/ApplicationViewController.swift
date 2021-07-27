//
//  ApplicationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 08/07/2021.
//

import UIKit

class ApplicationViewController: BaseViewController {

    //MARK:- Outlets and Properties
    
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var borrowerInfoView: UIView!
    @IBOutlet weak var borrowerCollectionView: UICollectionView!
    @IBOutlet weak var subjectPropertyView: UIView!
    @IBOutlet weak var addressView: UIView!
    @IBOutlet weak var lblAddress: UILabel!
    @IBOutlet weak var lblPropertyType: UILabel!
    @IBOutlet weak var loanInfoView: UIView!
    @IBOutlet weak var LoanInfoMainView: UIView!
    @IBOutlet weak var lblLoanPayment: UILabel!
    @IBOutlet weak var lblDownPayment: UILabel!
    @IBOutlet weak var lblPercentage: UILabel!
    @IBOutlet weak var assetsAndIncomeView: UIView!
    @IBOutlet weak var assetsView: UIView!
    @IBOutlet weak var lblTotalAssets: UILabel!
    @IBOutlet weak var monthlyIncomeView: UIView!
    @IBOutlet weak var lblMonthlyIncome: UILabel!
    @IBOutlet weak var RealEstateView: UIView!
    @IBOutlet weak var realEstateCollectionView: UICollectionView!
    @IBOutlet weak var governmentQuestionsView: UIView!
    @IBOutlet weak var questionsCollectionView: UICollectionView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        setupViews()
    }
    
    //MARK:- Methods and Actions
    
    func setupViews(){
        borrowerCollectionView.register(UINib(nibName: "BorrowerInfoCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "BorrowerInfoCollectionViewCell")
        realEstateCollectionView.register(UINib(nibName: "RealEstateCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "RealEstateCollectionViewCell")
        questionsCollectionView.register(UINib(nibName: "GovernmentQuestionsCollectionViewCell", bundle: nil), forCellWithReuseIdentifier: "GovernmentQuestionsCollectionViewCell")
        addressView.layer.cornerRadius = 6
        addressView.layer.borderWidth = 1
        addressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addressView.dropShadowToCollectionViewCell()
        LoanInfoMainView.layer.cornerRadius = 6
        LoanInfoMainView.layer.borderWidth = 1
        LoanInfoMainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        LoanInfoMainView.dropShadowToCollectionViewCell()
        assetsView.layer.cornerRadius = 6
        assetsView.layer.borderWidth = 1
        assetsView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        assetsView.dropShadowToCollectionViewCell()
        monthlyIncomeView.layer.cornerRadius = 6
        monthlyIncomeView.layer.borderWidth = 1
        monthlyIncomeView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        monthlyIncomeView.dropShadowToCollectionViewCell()
        
        let propertyTypeText = "Single Family Residency   ·   Investment Property"
        let propertyTypeAttributedText = NSMutableAttributedString(string: propertyTypeText)
        let range1 = propertyTypeText.range(of: "·")
        propertyTypeAttributedText.addAttribute(NSAttributedString.Key.font, value: Theme.getRubikBoldFont(size: 15), range: propertyTypeText.nsRange(from: range1!))
        lblPropertyType.attributedText = propertyTypeAttributedText
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 0.1) {
            let borrowerInfoCollectionViewLayout = UICollectionViewFlowLayout()
            borrowerInfoCollectionViewLayout.scrollDirection = .horizontal
            borrowerInfoCollectionViewLayout.sectionInset = UIEdgeInsets(top: 12, left: 20, bottom: 2, right: -80)
            borrowerInfoCollectionViewLayout.minimumLineSpacing = 10
            let itemWidth = UIScreen.main.bounds.width * 0.75
            borrowerInfoCollectionViewLayout.itemSize = CGSize(width: itemWidth, height: 80)
            self.borrowerCollectionView.collectionViewLayout = borrowerInfoCollectionViewLayout
            
            let realEstateCollectionViewLayout = UICollectionViewFlowLayout()
            realEstateCollectionViewLayout.scrollDirection = .horizontal
            realEstateCollectionViewLayout.sectionInset = UIEdgeInsets(top: 12, left: 20, bottom: 2, right: -80)
            realEstateCollectionViewLayout.minimumLineSpacing = 10
            realEstateCollectionViewLayout.itemSize = CGSize(width: itemWidth, height: 107)
            self.realEstateCollectionView.collectionViewLayout = realEstateCollectionViewLayout
            
            let questionsCollectionViewLayout = UICollectionViewFlowLayout()
            questionsCollectionViewLayout.scrollDirection = .horizontal
            questionsCollectionViewLayout.sectionInset = UIEdgeInsets(top: 12, left: 20, bottom: 2, right: 0)
            questionsCollectionViewLayout.minimumLineSpacing = 10
            questionsCollectionViewLayout.itemSize = CGSize(width: itemWidth, height: 157)
            self.questionsCollectionView.collectionViewLayout = questionsCollectionViewLayout
            
            self.questionsCollectionView.scrollToItem(at: IndexPath(row: 0, section: 0), at: .left, animated: true)
        }
    }
    
}

extension ApplicationViewController: UICollectionViewDataSource, UICollectionViewDelegate{
    
    func collectionView(_ collectionView: UICollectionView, numberOfItemsInSection section: Int) -> Int {
        return collectionView == questionsCollectionView ? 4 : 3
    }
    
    func collectionView(_ collectionView: UICollectionView, cellForItemAt indexPath: IndexPath) -> UICollectionViewCell {
        
        if (collectionView == borrowerCollectionView){
            let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "BorrowerInfoCollectionViewCell", for: indexPath) as! BorrowerInfoCollectionViewCell
            cell.mainView.layer.cornerRadius = 6
            cell.mainView.layer.borderWidth = 1
            cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            cell.mainView.dropShadowToCollectionViewCell()
            cell.addMoreView.layer.cornerRadius = 6
            cell.addMoreView.layer.borderWidth = 1
            cell.addMoreView.layer.borderColor = Theme.getAppGreyColor().withAlphaComponent(0.3).cgColor
            cell.addMoreView.dropShadowToCollectionViewCell()
            cell.lblBorrowerName.text = indexPath.row == 1 ? "Maria Randall" : "Richard Glenn Randall"
            cell.lblBorrowerType.text = indexPath.row == 1 ? "Co-Borrower" : "Primary Borrower"
            cell.mainView.isHidden = indexPath.row == 2
            cell.addMoreView.isHidden = indexPath.row != 2
            return cell
        }
        else if (collectionView == realEstateCollectionView){
            let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "RealEstateCollectionViewCell", for: indexPath) as! RealEstateCollectionViewCell
            cell.mainView.layer.cornerRadius = 6
            cell.mainView.layer.borderWidth = 1
            cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            cell.mainView.dropShadowToCollectionViewCell()
            cell.addMoreView.layer.cornerRadius = 6
            cell.addMoreView.layer.borderWidth = 1
            cell.addMoreView.layer.borderColor = Theme.getAppGreyColor().withAlphaComponent(0.3).cgColor
            cell.addMoreView.dropShadowToCollectionViewCell()
            cell.lblAddress.text = indexPath.row == 1 ? "727 Ashleigh Lane,\nSouth Lake TX, 76092" : "5919 Trussville Crossings\nParkways,"
            cell.lblPropertyType.text = indexPath.row == 1 ? "Single Family Property" : "Land"
            cell.mainView.isHidden = indexPath.row == 2
            cell.addMoreView.isHidden = indexPath.row != 2
            return cell
        }
        else{
            let cell = collectionView.dequeueReusableCell(withReuseIdentifier: "GovernmentQuestionsCollectionViewCell", for: indexPath) as! GovernmentQuestionsCollectionViewCell
            cell.mainView.layer.cornerRadius = 6
            cell.mainView.layer.borderWidth = 1
            cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
            cell.mainView.dropShadowToCollectionViewCell()
            cell.lblQuestionHeading.text = indexPath.row % 2 == 0 ? "Undisclosed Borrowed Funds" : "Ownership Interest in Property"
            cell.lblQuestion.text = indexPath.row % 2 == 0 ? "Are you borrowing any money for this real estate transaction (e.g., money for your ..." : "Have you had an ownership interest in another property in the last three years?"
            cell.iconAns1.image = UIImage(named: indexPath.row % 2 == 0 ? "Ans-Yes" : "Ans-NA")
            cell.lblAns1.text = indexPath.row % 2 == 0 ? "Yes" : "N/a"
            cell.lblAns1.font = indexPath.row % 2 == 0 ? Theme.getRubikMediumFont(size: 15) : Theme.getRubikRegularFont(size: 15)
            return cell
        }
        
    }
    
}

//extension ApplicationViewController: UITableViewDataSource, UITableViewDelegate{
//
//    func numberOfSections(in tableView: UITableView) -> Int {
//        return 6
//    }
//
//    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
//        return 0
//    }
//
//    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
//        return UITableViewCell()
//    }
//
//    func tableView(_ tableView: UITableView, heightForHeaderInSection section: Int) -> CGFloat {
//        return 100
//    }
//
//    func tableView(_ tableView: UITableView, viewForHeaderInSection section: Int) -> UIView? {
//        let nib = Bundle.main.loadNibNamed("ApplicationTabHeaderView", owner: self, options: nil)
//        let contentView = nib?.first as! ApplicationTabHeaderView
//        contentView.frame = tableView.bounds
//        contentView.autoresizingMask = [.flexibleWidth, .flexibleHeight]
//        contentView.lblTitle.text = "Hello World"
//        return contentView
//    }
//
//}
