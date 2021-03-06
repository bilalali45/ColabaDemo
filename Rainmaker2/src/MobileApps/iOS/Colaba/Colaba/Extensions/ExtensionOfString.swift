//
//  ExtensionOfString.swift
//  Colaba
//
//  Created by Murtaza on 19/05/2021.
//

import Foundation
import UIKit
import MonthYearPicker

extension String {
    
    public func isValidEmail() -> Bool {
        let regex = try! NSRegularExpression(pattern: "^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$", options: .caseInsensitive)
        return regex.firstMatch(in: self, options: [], range: NSRange(location: 0, length: count)) != nil
    }
    
    func nsRange(from range: Range<String.Index>) -> NSRange {
        let from = range.lowerBound.samePosition(in: utf16)
        let to = range.upperBound.samePosition(in: utf16)
        return NSRange(location: utf16.distance(from: utf16.startIndex, to: from!),
                       length: utf16.distance(from: from!, to: to!))
    }
    
    var htmlToAttributedString: NSAttributedString? {
        guard let data = data(using: .utf8) else { return nil }
        do {
            return try NSAttributedString(data: data, options: [.documentType: NSAttributedString.DocumentType.html, .characterEncoding:String.Encoding.utf8.rawValue], documentAttributes: nil)
        } catch {
            return nil
        }
    }
    var htmlToString: String {
        return htmlToAttributedString?.string ?? ""
    }
    
}

extension Int{
    
    func withCommas() -> String {
        let numberFormatter = NumberFormatter()
        numberFormatter.numberStyle = .currency
        numberFormatter.locale = Locale(identifier: "en_US")
        numberFormatter.alwaysShowsDecimalSeparator = false
        return numberFormatter.string(from: NSNumber(value:self))!
    }
    
    func ordinalNumber() -> String{
        let numberFormatter = NumberFormatter()
        numberFormatter.numberStyle = .ordinal
        let ordinalNumber = numberFormatter.string(from: NSNumber(value: self))!
        return ordinalNumber
    }
}

extension UITextField {
    
    func addInputViewDatePicker(target: Any, selector: Selector, maximumDate: Date?=nil) {
        
        let screenWidth = UIScreen.main.bounds.width
        
        //Add DatePicker as inputView
        let datePicker = UIDatePicker(frame: CGRect(x: 0, y: 0, width: screenWidth, height: 216))
        datePicker.datePickerMode = .date
        if let maxDate = maximumDate{
            datePicker.maximumDate = maxDate
        }
        if #available(iOS 13.4, *) {
            datePicker.preferredDatePickerStyle = .wheels
        } else {
            // Fallback on earlier versions
        }
        datePicker.addTarget(target, action: selector, for: .valueChanged)
        self.inputView = datePicker
        
        //Add Tool Bar as input AccessoryView
        let toolBar = UIToolbar(frame: CGRect(x: 0, y: 0, width: screenWidth, height: 44))
        let flexibleSpace = UIBarButtonItem(barButtonSystemItem: .flexibleSpace, target: nil, action: nil)
        let cancelBarButton = UIBarButtonItem(title: "", style: .plain, target: self, action: #selector(cancelPressed))
        let doneBarButton = UIBarButtonItem(title: "Done", style: .plain, target: self, action: #selector(cancelPressed))
        toolBar.setItems([cancelBarButton, flexibleSpace, doneBarButton], animated: false)
        
        self.inputAccessoryView = toolBar
    }
    
    func addInputViewMonthYearDatePicker(target: Any, selector: Selector) {
        
        let screenWidth = UIScreen.main.bounds.width
        
        //Add DatePicker as inputView
        let datePicker = MonthYearPickerView(frame: CGRect(x: 0, y: 0, width: screenWidth, height: 216))
        datePicker.addTarget(target, action: selector, for: .valueChanged)
        self.inputView = datePicker
        
        //Add Tool Bar as input AccessoryView
        let toolBar = UIToolbar(frame: CGRect(x: 0, y: 0, width: screenWidth, height: 44))
        let flexibleSpace = UIBarButtonItem(barButtonSystemItem: .flexibleSpace, target: nil, action: nil)
        let cancelBarButton = UIBarButtonItem(title: "", style: .plain, target: self, action: #selector(cancelPressed))
        let doneBarButton = UIBarButtonItem(title: "Done", style: .plain, target: self, action: #selector(cancelPressed))
        toolBar.setItems([cancelBarButton, flexibleSpace, doneBarButton], animated: false)
        
        self.inputAccessoryView = toolBar
    }
    
    @objc func cancelPressed() {
        self.resignFirstResponder()
    }
}

extension UITextView {

  func addHyperLinksToText(originalText: String, hyperLinks: [String: String]) {
    let style = NSMutableParagraphStyle()
    style.alignment = .left
    let attributedOriginalText = NSMutableAttributedString(string: originalText)
    for (hyperLink, urlString) in hyperLinks {
        let linkRange = attributedOriginalText.mutableString.range(of: hyperLink)
        let fullRange = NSRange(location: 0, length: attributedOriginalText.length)
        attributedOriginalText.addAttribute(NSAttributedString.Key.link, value: urlString, range: linkRange)
        attributedOriginalText.addAttribute(NSAttributedString.Key.paragraphStyle, value: style, range: fullRange)
        attributedOriginalText.addAttribute(NSAttributedString.Key.font, value: Theme.getRubikRegularFont(size: 15), range: fullRange)
    }
    
    self.linkTextAttributes = [
        NSAttributedString.Key.foregroundColor: Theme.getButtonBlueColor()]
    self.attributedText = attributedOriginalText
  }
}
