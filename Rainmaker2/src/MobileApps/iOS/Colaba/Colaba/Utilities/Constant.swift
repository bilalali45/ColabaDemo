//
//  Constant.swift
//  Colaba
//
//  Created by Murtaza on 19/05/2021.
//

import Foundation

let kGoogleAPIKey = "AIzaSyBzPEiQOTReBzy6W1UcIyHApPu7_5Die6w"
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
let kNotificationHidesHomeNavigationBar = "notificationHidesHomeNavigationBar"
let kNotificationShowHomeNavigationBar = "notificationShowHomeNavigationBar"
let kNotificationShowMailingAddress = "notificationShowMailingAddress"
let kNotificationSaveAddressAndDismiss = "notificationSaveAddressAndDismiss"
let kNotificationDeleteMailingAddressAndDismiss = "notificationDeleteMailingAddressAndDismiss"
let kNotificationAddCurrentEmployement = "notificationAddCurrentEmployement"
let kNotificationAddPreviousEmployement = "notificationAddPreviousEmployement"
let kNotificationLoanOfficerSeeMoreTapped = "notificationLoanOfficerSeeMoreTapped"
let kNotificationLoanOfficerSelected = "notificationLoanOfficerSelected"
let kNotificationDeleteDocument = "notificationDeleteDocument"
let kNotificationShowRequestDocumentFooterButton = "notificationShowRequestDocumentFooterButton"
let kNotificationHideRequestDocumentFooterButton = "notificationHideRequestDocumentFooterButton"
let kHousingStatusArray = ["Own", "Rent", "No Primary Housing Expense"]
let kBusinessTypeArray = ["Partnership (e.g. LLC, LP, or GP)", "Corporation (e.g. C-Corp, S-Corp, or LLC)"]
let kRetirementIncomeTypeArray = ["Social Security", "Pension", "IRA / 401K", "Other Retirement Source"]
let kOtherIncomeTypeArray = ["Alimony", "Child Support", "Separate Maintenance", "Foster Care", "Annuity", "Capital Gains", "Interest / Dividends", "Notes Receivable", "Trust", "Housing Or Parsonage", "Mortgage Credit Certificate", "Mortgage Differential Payments", "Public Assistance", "Unemployment Benefits", "VA Compensation", "Automobile Allowance", "Boarder Income", "Royalty Payments", "Disability", "Other Income Source"]
let kPropertyStatusArray = ["Retained", "Pending sale"]

var sortingFilter = 1
var isAppOpenFromBackground = false

let kCountryListArray = ["United States", "Afghanistan", "Albania", "Algeria", "American Samoa", "Andorra", "Angola", "Anguilla", "Antarctica", "Antigua and Barbuda", "Argentina", "Armenia", "Aruba", "Australia", "Austria", "Azerbaijan", "Bahamas", "Bahrain", "Bangladesh", "Barbados", "Belarus", "Belgium", "Belize", "Benin", "Bermuda", "Bhutan", "Bolivia", "Bosnia and Herzegowina", "Botswana", "Bouvet Island", "Brazil", "Brunei Darussalam", "Bulgaria", "Burkina Faso", "Burundi", "Cambodia", "Cameroon", "Canada", "Cape Verde", "Cayman Islands", "Central African Republic", "Chad", "Chile", "China", "Christmas Island", "Colombia", "Comoros", "Congo", "Cook Islands", "Costa Rica", "Cote d'Ivoire", "Croatia (Hrvatska)", "Cuba", "Cyprus", "Czech Republic", "Denmark", "Djibouti", "Dominica", "Dominican Republic", "East Timor", "Ecuador", "Egypt", "El Salvador", "Equatorial Guinea", "Eritrea", "Estonia", "Ethiopia", "Falkland Islands (Malvinas)", "Faroe Islands", "Fiji", "Finland", "France", "Gabon", "Gambia", "Georgia", "Germany", "Ghana", "Gibraltar", "Greece", "Greenland", "Grenada", "Guadeloupe", "Guam", "Guatemala", "Guinea", "Guinea-Bissau", "Guyana", "Haiti", "Heard and Mc Donald Islands", "Holy See (Vatican City State)", "Honduras", "Hong Kong", "Hungary", "Iceland", "India", "Indonesia", "Iran (Islamic Republic of)", "Iraq", "Ireland", "Israel", "Italy", "Jamaica", "Japan", "Jordan", "Kazakhstan", "Kenya", "Kiribati", "Korea, Democratic People's Republic of", "Korea, Republic of", "Kuwait", "Kyrgyzstan", "Lao, People's Democratic Republic", "Latvia", "Lebanon", "Lesotho", "Liberia", "Libyan Arab Jamahiriya", "Liechtenstein", "Lithuania", "Luxembourg", "Macau", "Macedonia, The Former Yugoslav Republic of", "Madagascar", "Malawi", "Malaysia", "Maldives", "Mali", "Malta", "Marshall Islands", "Martinique", "Mauritania", "Mauritius", "Mayotte", "Mexico", "Micronesia, Federated States of", "Moldova, Republic of", "Monaco", "Mongolia", "Montserrat", "Morocco", "Mozambique", "Myanmar", "Namibia", "Nauru", "Nepal", "Netherlands", "Netherlands Antilles", "New Caledonia", "New Zealand", "Nicaragua", "Niger", "Nigeria", "Niue", "Norfolk Island", "Northern Mariana Islands", "Norway", "Oman", "Pakistan", "Palau", "Panama", "Papua New Guinea", "Paraguay", "Peru", "Philippines", "Pitcairn", "Poland", "Portugal", "Puerto Rico", "Qatar", "Reunion", "Romania", "Russian Federation", "Rwanda", "Saint Kitts and Nevis", "Saint Lucia", "Saint Vincent and the Grenadines", "Samoa", "San Marino", "Sao Tome and Principe", "Saudi Arabia", "Senegal", "Seychelles", "Sierra Leone", "Singapore", "Slovakia (Slovak Republic)", "Slovenia", "Solomon Islands", "Somalia", "South Africa", "South Georgia and the South Sandwich Islands", "Spain", "Sri Lanka", "St. Helena", "St. Pierre and Miquelon", "Sudan", "Suriname", "Swaziland", "Sweden", "Switzerland", "Syrian Arab Republic", "Taiwan", "Tajikistan", "Tanzania, United Republic of", "Thailand", "Togo", "Tokelau", "Tonga", "Trinidad and Tobago", "Tunisia", "Turkey", "Turkmenistan", "Turks and Caicos Islands", "Tuvalu", "Uganda", "Ukraine", "United Arab Emirates", "United Kingdom", "Uruguay", "Uzbekistan", "Vanuatu", "Venezuela", "Vietnam", "Virgin Islands", "Virgin Islands", "Wallis and Futuna Islands", "Western Sahara", "Yemen", "Yugoslavia", "Zambia", "Zimbabwe"]

let kUSAStatesArray = ["Alaska", "Alabama", "Arkansas", "American Samoa", "Arizona", "California", "Colorado", "Connecticut", "District of Columbia", "Delaware", "Florida", "Georgia", "Guam", "Hawaii", "Iowa", "Idaho", "Illinois", "Indiana", "Kansas", "Kentucky", "Louisiana", "Massachusetts", "Maryland", "Maine", "Michigan", "Minnesota", "Missouri", "Mississippi", "Montana", "North Carolina", "North Dakota", "Nebraska", "New Hampshire", "New Jersey", "New Mexico", "Nevada", "New York", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Puerto Rico", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Virginia", "Virgin Islands", "Vermont", "Washington", "Wisconsin", "West Virginia", "Wyoming"]

let kRelationshipTypeArray = ["Civil Unions", "Domestic Partners", "Registered Reciprocal", "Other"]
let kVisaStatusArray = ["I am a temporary worker (H-2A, etc.)", "I hold a valid work visa (H1, L1, etc.)", "Other"]

let kOrdinalToSpellingDictionary = ["1st": "First", "2nd": "Second", "3rd": "Third", "4th": "Fourth", "5th": "Fifth", "6th": "Sixth", "7th": "Seventh", "8th": "Eighth", "9th": "Ninth", "10th": "Tenth", "11th": "Eleventh", "12th": "Twelfth", "13th": "Thirteenth", "14th": "Fourteenth", "15th": "Fifteenth", "16th": "Sixteenth", "17th": "Seventeenth", "18th": "Eighteenth", "19th": "Nineteenth", "20th": "Twentieth", "21st": "Twenty-First", "22nd": "Twenty-Second", "23rd": "Twenty-Third", "24th": "Twenty-Fourth", "25th": "Twenty-Fifth", "26th": "Twenty-Sixth", "27th": "Twenty-Seventh", "28th": "Twenty-Eighth", "29th": "Twenty-Ninth", "30th": "Thirtieth", "31st": "Thirty-First", "32nd": "Thirty-Second", "33rd": "Thirty-Third", "34th": "Thirty-Fourth", "35th": "Thirty-Fifth", "36th": "Thirty-Sixth", "37th": "Thirty-Seventh", "38th": "Thirty-Eighth", "39th": "Thirty-Ninth", "40th": "Fortieth", "41st": "Forty-First", "42nd": "Forty-Second", "43rd": "Forty-Third", "44th": "Forty-Fourth", "45th": "Forty-Fifth", "46th": "Forty-Sixth", "47th": "Forty-Seventh", "48th": "Forty-Eighth", "49th": "Forty-Ninth", "50th": "Fiftieth", "51st": "Fifty-First", "52nd": "Fifty-Second", "53rd": "Fifty-Third", "54th": "Fifty-Fourth", "55th": "Fifty-Fifth", "56th": "Fifty-Sixth", "57th": "Fifty-Seventh", "58th": "Fifty-Eighth", "59th": "Fifty-Ninth", "60th": "Sixtieth", "61st": "Sixty-First", "62nd": "Sixty-Second", "63rd": "Sixty-Third", "64th": "Sixty-Fourth", "65th": "Sixty-Fifth", "66th": "Sixty-Sixth", "67th": "Sixty-Seventh", "68th": "Sixty-Eighth", "69th": "Sixty-Ninth", "70th": "Seventieth", "71st": "Seventy-First", "72nd": "Seventy-Second", "73rd": "Seventy-Third", "74th": "Seventy-Fourth", "75th": "Seventy-Fifth", "76th": "Seventy-Sixth", "77th": "Seventy-Seventh", "78th": "Seventy-Eighth", "79th": "Seventy-Ninth", "80th": "Eightieth", "81st": "Eighty-First", "82nd": "Eighty-Second", "83rd": "Eighty-Third", "84th": "Eighty-Fourth", "85th": "Eighty-Fifth", "86th": "Eighty-Sixth", "87th": "Eighty-Seventh", "88th": "Eighty-Eighth", "89th": "Eighty-Ninth", "90th": "Ninetieth", "91st": "Ninety-First", "92nd": "Ninety-Second", "93rd": "Ninety-Third", "94th": "Ninety-Fourth", "95th": "Ninety-Fifth", "96th": "Ninety-Sixth", "97th": "Ninety-Seventh", "98th": "Ninety-Eighth", "99th": "Ninety-Ninth"]

let kLoanStageArray = ["Pre-Approval"]
let kPropertyTypeArray = ["Single Family Property", "Condominium", "Townhouse", "Cooperative", "Manufactured Home", "Duplex (2 Unit)", "Triplex (3 Unit)", "Quadplex (4 Unit)"]
let kOccupancyTypeArray = ["Primary Residence", "Second Home", "Investment Property"]
let kAccountTypeArray = ["Checking Account", "Saving Account"]
let kFinancialsAccountTypeArray = ["Mutual Funds", "Stocks", "Money Market", "Stock Options", "Certificate Of Deposit", "Bonds"]
let kTransactionTypeArray = ["Proceeds From A Loan", "Proceeds From Selling Non-Real Estate Assets", "Proceeds From Selling Real Estate Assets"]
let kAssetsTypeArray = ["House", "Automobile", "Financial Account", "Other"]
let kGiftSourceArray = ["Relative", "Unmarried Partner", "Federal Agency", "State Agency", "Local Agency", "Community Non Profit", "Employer", "Religious Non Profit", "Lender"]
let kOtherAssetsTypeArray = ["Trust Account", "Bridge Loan Proceeds", "Individual Development Account (IDA)", "Cash Value of Life Insurance", "Employer Assistance", "Relocation Funds", "Rent Credit", "Lot Equity", "Sweat Equity", "Trade Equity", "Other"]
let kHoldTitleArray = ["By Yourself", "Jointly with your spouse", "Jointly with another person"]
let kPaymentsRemainingArray = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "12+"]
let kRequestPaymentTemplates: [RequestEmailTemplate] = [RequestEmailTemplate(title: "Document Request with Intro", description: "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque"),RequestEmailTemplate(title: "Default Document Request", description: "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque"), RequestEmailTemplate(title: "Only Tenant Intro", description: "Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque")]
