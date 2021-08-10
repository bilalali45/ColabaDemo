//
//  BorrowerInformationViewController.swift
//  Colaba
//
//  Created by Muhammad Murtaza on 05/08/2021.
//

import UIKit
import Material

class BorrowerInformationViewController: UIViewController {

    //MARK:- Outlets and Properties
    @IBOutlet weak var btnBack: UIButton!
    @IBOutlet weak var lblBorrowerType: UILabel!
    @IBOutlet weak var seperatorView: UIView!
    @IBOutlet weak var mainScrollView: UIScrollView!
    @IBOutlet weak var mainView: UIView!
    @IBOutlet weak var mainViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var borrowerInfoView: UIView!
    @IBOutlet weak var txtfieldLegalFirstName: TextField!
    @IBOutlet weak var txtfieldMiddleName: TextField!
    @IBOutlet weak var txtfieldLegalLastName: TextField!
    @IBOutlet weak var txtfieldSuffix: TextField!
    @IBOutlet weak var txtfieldEmail: TextField!
    @IBOutlet weak var txtfieldHomeNumber: TextField!
    @IBOutlet weak var txtfieldWorkNumber: TextField!
    @IBOutlet weak var txtfieldExtensionNumber: TextField!
    @IBOutlet weak var txtfieldCellNumber: TextField!
    @IBOutlet weak var tblViewAddress: UITableView!
    @IBOutlet weak var tblViewHeightConstraint: NSLayoutConstraint!
    @IBOutlet weak var addAddressView: UIView!
    
    override func viewDidLoad() {
        super.viewDidLoad()
        
        setMaterialTextFieldsAndViews(textfields: [txtfieldLegalFirstName, txtfieldMiddleName, txtfieldLegalLastName, txtfieldSuffix, txtfieldEmail, txtfieldHomeNumber, txtfieldWorkNumber, txtfieldExtensionNumber, txtfieldCellNumber])
        tblViewAddress.register(UINib(nibName: "BorrowerAddressInfoTableViewCell", bundle: nil), forCellReuseIdentifier: "BorrowerAddressInfoTableViewCell")
    }
    
    override func viewDidAppear(_ animated: Bool) {
        super.viewDidAppear(animated)
        
        DispatchQueue.main.asyncAfter(deadline: .now() + 1) {
            self.setScreenHeight()
        }
        
    }
    
    //MARK:- Methods and Actions
    
    func setScreenHeight(){
        let addressTableViewHeight = tblViewAddress.contentSize.height
        let totalHeight = addressTableViewHeight + 750
        tblViewHeightConstraint.constant = addressTableViewHeight
        self.mainViewHeightConstraint.constant = totalHeight
        self.mainView.updateConstraintsIfNeeded()
        self.mainView.layoutSubviews()
    }
    
    func setMaterialTextFieldsAndViews(textfields: [TextField]){
        for textfield in textfields{
            textfield.dividerActiveColor = Theme.getButtonBlueColor()
            textfield.dividerColor = Theme.getSeparatorNormalColor()
            textfield.placeholderActiveColor = Theme.getAppGreyColor()
            textfield.delegate = self
            textfield.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
            textfield.detailLabel.font = Theme.getRubikRegularFont(size: 12)
            textfield.detailColor = .red
        }
        
        addAddressView.layer.cornerRadius = 6
        addAddressView.layer.borderWidth = 1
        addAddressView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        addAddressView.addGestureRecognizer(UITapGestureRecognizer(target: self, action: #selector(addAddressViewTapped)))
    }
    
    func setPlaceholderLabelColorAfterTextFilled(selectedTextField: UITextField, allTextFields: [TextField]){
        for allTextField in allTextFields{
            if (allTextField == selectedTextField){
                if (allTextField.text == ""){
                    allTextField.placeholderLabel.textColor = Theme.getButtonGreyTextColor()
                }
                else{
                    allTextField.placeholderLabel.textColor = Theme.getAppGreyColor()
                }
            }
        }
    }
   
    @IBAction func btnBackTapped(_ sender: UIButton) {
        self.goBack()
    }
    
    @objc func addAddressViewTapped(){
        
    }
}

extension BorrowerInformationViewController: UITableViewDataSource, UITableViewDelegate{
    
    func tableView(_ tableView: UITableView, numberOfRowsInSection section: Int) -> Int {
        return 2
    }
    
    func tableView(_ tableView: UITableView, cellForRowAt indexPath: IndexPath) -> UITableViewCell {
        let cell = tableView.dequeueReusableCell(withIdentifier: "BorrowerAddressInfoTableViewCell", for: indexPath) as! BorrowerAddressInfoTableViewCell
        cell.addressIcon.isHidden = indexPath.row != 0
        cell.lblHeading.isHidden = indexPath.row != 0
        cell.lblRent.isHidden = indexPath.row != 0
        cell.lblAddressTopConstraint.constant = indexPath.row != 0 ? 15 : 51
        cell.lblAddress.text = indexPath.row != 0 ? "5919 Trussville Crossings Pkwy,\nBirmingham AL 35235" : "5919 Trussville Crossings Parkways, ZV Street,\nBirmingham AL 35235"
        cell.lblDate.text = indexPath.row != 0 ? "From Aug 2019 to Nov 2020" : "From Dec 2020"
        cell.mainView.layer.cornerRadius = 6
        cell.mainView.layer.borderWidth = 1
        cell.mainView.layer.borderColor = Theme.getButtonBlueColor().withAlphaComponent(0.3).cgColor
        cell.mainView.dropShadowToCollectionViewCell()
        cell.mainView.updateConstraintsIfNeeded()
        cell.mainView.layoutSubviews()
        return cell
    }
    
    func tableView(_ tableView: UITableView, didSelectRowAt indexPath: IndexPath) {
        let vc = Utility.getAddResidenceVC()
        self.presentVC(vc: vc)
    }
    
    func tableView(_ tableView: UITableView, heightForRowAt indexPath: IndexPath) -> CGFloat {
        return UITableView.automaticDimension
    }
    
    func tableView(_ tableView: UITableView, canEditRowAt indexPath: IndexPath) -> Bool {
        return true
    }
    
    func tableView(_ tableView: UITableView, trailingSwipeActionsConfigurationForRowAt indexPath: IndexPath) -> UISwipeActionsConfiguration? {
        
        let deleteAction = UIContextualAction(style: .normal, title: "") { action, actionView, bool in
            
        }
        deleteAction.backgroundColor = Theme.getDashboardBackgroundColor()
        deleteAction.image = UIImage(named: indexPath.row == 0 ? "AddressDeleteIconBig" : "AddressDeleteIconSmall")
        return UISwipeActionsConfiguration(actions: [deleteAction])
        
    }
}

extension BorrowerInformationViewController: UITextFieldDelegate{
    func textFieldDidEndEditing(_ textField: UITextField) {
        setPlaceholderLabelColorAfterTextFilled(selectedTextField: textField, allTextFields: [txtfieldLegalFirstName, txtfieldMiddleName, txtfieldLegalLastName, txtfieldSuffix, txtfieldEmail, txtfieldHomeNumber, txtfieldWorkNumber, txtfieldExtensionNumber, txtfieldCellNumber])
    }
}
