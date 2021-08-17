//
//  Constant.swift
//  Colaba
//
//  Created by Murtaza on 19/05/2021.
//

import Foundation

let kDeviceNotRegistered = "Device is not registered with Biometric login. Please setup it from settings."
let kFaceID = "FaceID"
let kTouchID = "TouchID"
let kYes = "YES"
let kNo = "NO"
let kFaceIdResetPopup = "This will reset your face Id verification. Do you want to continue?"
let kFingerIdResetPopup = "This will reset your finger print verification. Do you want to continue?"
//let BASEURL = "https://devmobilegateway.rainsoftfn.com:5002/api/mcu/mobile/"
let BASEURL = "https://qamobilegateway.rainsoftfn.com/api/mcu/mobile/"
//let BASEURL = "https://mobilegateway.rainsoftfn.xyz/api/mcu/mobile/"
let kResendEnableTimeStamp = "kResendEnableTimeStamp"
var isBiometricAllow = false
var shouldShowSkipButton = false
var totalCodeLimit = 5
let kIsUserRegisteredWithBiometric = "UserRegisteredWithBiometric"
let kDontAsk2FAValue = "dontAsk2FAValue"
let kNotificationPopupViewCloseTapped = "NotificationPopupViewCloseTapped"
let kNotificationRefreshCounter = "NotificationRefreshCounter"
let kIsAssignToMe = "IsAssignToMe"
let kNotificationAssignToMeSwitchChanged = "NotificationAssignToMeSwitchChanged"
let kNotificationBtnFiltersTapped = "NotificationBtnFiltersTapped"
let kIsWalkthroughShowed = "isWalkthroughShowed"
let kNotificationHidesNavigationBar = "notificationHidesNavigationBar"
let kNotificationShowNavigationBar = "notificationShowNavigationBar"
let kNotificationShowMailingAddress = "notificationShowMailingAddress"
let kNotificationSaveAddressAndDismiss = "notificationSaveAddressAndDismiss"
let kNotificationDeleteMailingAddressAndDismiss = "notificationDeleteMailingAddressAndDismiss"
let kHousingStatusArray = ["Own", "Rent", "No Primary Housing Expense"]

var sortingFilter = 1

let kCountryListArray = ["United States", "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia and Herzegowina", "Botswana", "Bouvet Island", "Brazil", "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Colombia", "Comoros", "Congo", "Cook Islands", "Costa Rica", "Cote d'Ivoire", "Croatia (Hrvatska)", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands (Malvinas)", "Faroe Islands", "Fiji", "Finland", "France", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard and Mc Donald Islands", "Holy See (Vatican City State)", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran (Islamic Republic of)", "Iraq", "Ireland", "Israel", "Italy", "Jamaica", "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, Democratic People's Republic of", "Korea, Republic of", "Kuwait", "Kyrgyzstan", "Lao, People's Democratic Republic", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libyan Arab Jamahiriya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia, The Former Yugoslav Republic of", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia, Federated States of", "Moldova, Republic of", "Monaco", "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Northern Mariana Islands", "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russian Federation", "Rwanda", "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Seychelles", "Sierra Leone", "Singapore", "Slovakia (Slovak Republic)", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Georgia and the South Sandwich Islands", "Spain", "Sri Lanka", "St. Helena", "St. Pierre and Miquelon", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan", "Tajikistan", "Tanzania, United Republic of", "Thailand", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "Uruguay", "Uzbekistan", "Vanuatu", "Venezuela", "Vietnam", "Virgin Islands", "Virgin Islands", "Wallis and Futuna Islands", "Western Sahara", "Yemen", "Yugoslavia", "Zambia", "Zimbabwe"]

let kUSAStatesArray = ["Alaska", "Alabama", "Arkansas", "American Samoa", "Arizona", "California", "Colorado", "Connecticut", "District of Columbia", "Delaware", "Florida", "Georgia", "Guam", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Virgin Islands", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming"]

let kRelationshipTypeArray = ["Civil Unions", "Domestic Partners", "Registered Reciprocal", "Other"]
let kVisaStatusArray = ["I am a temporary worker (H-2A, etc.)", "I hold a valid work visa (H1, L1, etc.)", "Other"]
