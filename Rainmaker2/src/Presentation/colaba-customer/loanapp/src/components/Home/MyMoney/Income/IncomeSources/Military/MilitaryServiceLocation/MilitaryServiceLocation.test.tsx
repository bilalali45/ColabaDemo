import React from "react";
import {
  fireEvent,
  getAllByTestId,
  getByPlaceholderText,
  render,
  waitFor,
} from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import Path from "path";
import { MockEnvConfig } from "../../../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../../../test_utilities/SessionStoreMock";
import { Store } from "../../../../../../../store/store";
import { MilitaryServiceLocation } from "./MilitaryServiceLocation";
import { MilitaryIncomeEmployer } from "../../../../../../../Entities/Models/types";

jest.mock("../../../../../../../store/actions/GettingToKnowYouActions");


    
const state = {
  leftMenu: {
    navigation: null,
    leftMenuItems: [],
    notAllowedItems: [],
  },
  error: {},
  loanManager: {
    loanInfo: {
      loanApplicationId: 41313,
      loanPurposeId: null,
      loanGoalId: null,
      borrowerId: 31494,
      ownTypeId: 2,
      borrowerName: "second",
    },
    primaryBorrowerInfo: {
      id: 31450,
      name: "khalid",
    },
    states: [
      { id: 1, countryId: 1, name: "Alaska", shortCode: "AK" },
      { id: 2, countryId: 1, name: "Alabama", shortCode: "AL" },
      { id: 3, countryId: 1, name: "Arkansas", shortCode: "AR" },
      { id: 4, countryId: 1, name: "Arizona", shortCode: "AZ" },
      { id: 5, countryId: 1, name: "California", shortCode: "CA" },
      { id: 6, countryId: 1, name: "Colorado", shortCode: "CO" },
      { id: 7, countryId: 1, name: "Connecticut", shortCode: "CT" },
      { id: 8, countryId: 1, name: "District Of Columbia", shortCode: "DC" },
      { id: 9, countryId: 1, name: "Delaware", shortCode: "DE" },
      { id: 10, countryId: 1, name: "Florida", shortCode: "FL" },
      { id: 11, countryId: 1, name: "Georgia", shortCode: "GA" },
      { id: 12, countryId: 1, name: "Hawaii", shortCode: "HI" },
      { id: 13, countryId: 1, name: "Iowa", shortCode: "IA" },
      { id: 14, countryId: 1, name: "Idaho", shortCode: "ID" },
      { id: 15, countryId: 1, name: "Illinois", shortCode: "IL" },
      { id: 16, countryId: 1, name: "Indiana", shortCode: "IN" },
      { id: 17, countryId: 1, name: "Kansas", shortCode: "KS" },
      { id: 18, countryId: 1, name: "Kentucky", shortCode: "KY" },
      { id: 19, countryId: 1, name: "Louisiana", shortCode: "LA" },
      { id: 20, countryId: 1, name: "Massachusetts", shortCode: "MA" },
      { id: 21, countryId: 1, name: "Maryland", shortCode: "MD" },
      { id: 22, countryId: 1, name: "Maine", shortCode: "ME" },
      { id: 23, countryId: 1, name: "Michigan", shortCode: "MI" },
      { id: 24, countryId: 1, name: "Minnesota", shortCode: "MN" },
      { id: 25, countryId: 1, name: "Missouri", shortCode: "MO" },
      { id: 26, countryId: 1, name: "Mississippi", shortCode: "MS" },
      { id: 27, countryId: 1, name: "Montana", shortCode: "MT" },
      { id: 28, countryId: 1, name: "North Carolina", shortCode: "NC" },
      { id: 29, countryId: 1, name: "North Dakota", shortCode: "ND" },
      { id: 30, countryId: 1, name: "Nebraska", shortCode: "NE" },
      { id: 31, countryId: 1, name: "New Hampshire", shortCode: "NH" },
      { id: 32, countryId: 1, name: "New Jersey", shortCode: "NJ" },
      { id: 33, countryId: 1, name: "New Mexico", shortCode: "NM" },
      { id: 34, countryId: 1, name: "Nevada", shortCode: "NV" },
      { id: 35, countryId: 1, name: "New York", shortCode: "NY" },
      { id: 36, countryId: 1, name: "Ohio", shortCode: "OH" },
      { id: 37, countryId: 1, name: "Oklahoma", shortCode: "OK" },
      { id: 38, countryId: 1, name: "Oregon", shortCode: "OR" },
      { id: 39, countryId: 1, name: "Pennsylvania", shortCode: "PA" },
      { id: 40, countryId: 1, name: "Puerto Rico", shortCode: "PR" },
      { id: 41, countryId: 1, name: "Rhode Island", shortCode: "RI" },
      { id: 42, countryId: 1, name: "South Carolina", shortCode: "SC" },
      { id: 43, countryId: 1, name: "South Dakota", shortCode: "SD" },
      { id: 44, countryId: 1, name: "Tennessee", shortCode: "TN" },
      { id: 45, countryId: 1, name: "Texas", shortCode: "TX" },
      { id: 46, countryId: 1, name: "Utah", shortCode: "UT" },
      { id: 47, countryId: 1, name: "Virginia", shortCode: "VA" },
      { id: 48, countryId: 1, name: "Vermont", shortCode: "VT" },
      { id: 49, countryId: 1, name: "Washington", shortCode: "WA" },
      { id: 50, countryId: 1, name: "Wisconsin", shortCode: "WI" },
      { id: 51, countryId: 1, name: "West Virginia", shortCode: "WV" },
      { id: 52, countryId: 1, name: "Wyoming", shortCode: "WY" },
      { id: 53, countryId: 1, name: "Virgin Islands", shortCode: "VI" },
    ],
    countries: [
      { id: 1, name: "United States", shortCode: "US" },
      { id: 2, name: "Andorra", shortCode: "AD" },
      { id: 3, name: "United Arab Emirates", shortCode: "AE" },
      { id: 4, name: "Afghanistan", shortCode: "AF" },
      { id: 5, name: "Antigua and Barbuda", shortCode: "AG" },
      { id: 6, name: "Anguilla", shortCode: "AI" },
      { id: 7, name: "Albania", shortCode: "AL" },
      { id: 8, name: "Armenia", shortCode: "AM" },
      { id: 9, name: "Angola", shortCode: "AO" },
      { id: 10, name: "Antarctica", shortCode: "AQ" },
      { id: 11, name: "Argentina", shortCode: "AR" },
      { id: 12, name: "American Samoa", shortCode: "AS" },
      { id: 13, name: "Austria", shortCode: "AT" },
      { id: 14, name: "Australia", shortCode: "AU" },
      { id: 15, name: "Aruba", shortCode: "AW" },
      { id: 16, name: "Aland Islands", shortCode: "AX" },
      { id: 17, name: "Azerbaijan", shortCode: "AZ" },
      { id: 18, name: "Bosnia and Herzegovina", shortCode: "BA" },
      { id: 19, name: "Barbados", shortCode: "BB" },
      { id: 20, name: "Bangladesh", shortCode: "BD" },
      { id: 21, name: "Belgium", shortCode: "BE" },
      { id: 22, name: "Burkina Faso", shortCode: "BF" },
      { id: 23, name: "Bulgaria", shortCode: "BG" },
      { id: 24, name: "Bahrain", shortCode: "BH" },
      { id: 25, name: "Burundi", shortCode: "BI" },
      { id: 26, name: "Benin", shortCode: "BJ" },
      { id: 27, name: "Saint Barthelemy", shortCode: "BL" },
      { id: 28, name: "Bermuda", shortCode: "BM" },
      { id: 29, name: "Brunei", shortCode: "BN" },
      { id: 30, name: "Bolivia", shortCode: "BO" },
      { id: 31, name: "Bonaire, Saint Eustatius and Saba ", shortCode: "BQ" },
      { id: 32, name: "Brazil", shortCode: "BR" },
      { id: 33, name: "Bahamas", shortCode: "BS" },
      { id: 34, name: "Bhutan", shortCode: "BT" },
      { id: 35, name: "Bouvet Island", shortCode: "BV" },
      { id: 36, name: "Botswana", shortCode: "BW" },
      { id: 37, name: "Belarus", shortCode: "BY" },
      { id: 38, name: "Belize", shortCode: "BZ" },
      { id: 39, name: "Canada", shortCode: "CA" },
      { id: 40, name: "Cocos Islands", shortCode: "CC" },
      { id: 41, name: "Democratic Republic of the Congo", shortCode: "CD" },
      { id: 42, name: "Central African Republic", shortCode: "CF" },
      { id: 43, name: "Republic of the Congo", shortCode: "CG" },
      { id: 44, name: "Switzerland", shortCode: "CH" },
      { id: 45, name: "Ivory Coast", shortCode: "CI" },
      { id: 46, name: "Cook Islands", shortCode: "CK" },
      { id: 47, name: "Chile", shortCode: "CL" },
      { id: 48, name: "Cameroon", shortCode: "CM" },
      { id: 49, name: "China", shortCode: "CN" },
      { id: 50, name: "Colombia", shortCode: "CO" },
      { id: 51, name: "Costa Rica", shortCode: "CR" },
      { id: 52, name: "Cuba", shortCode: "CU" },
      { id: 53, name: "Cabo Verde", shortCode: "CV" },
      { id: 54, name: "Curacao", shortCode: "CW" },
      { id: 55, name: "Christmas Island", shortCode: "CX" },
      { id: 56, name: "Cyprus", shortCode: "CY" },
      { id: 57, name: "Czechia", shortCode: "CZ" },
      { id: 58, name: "Germany", shortCode: "DE" },
      { id: 59, name: "Djibouti", shortCode: "DJ" },
      { id: 60, name: "Denmark", shortCode: "DK" },
      { id: 61, name: "Dominica", shortCode: "DM" },
      { id: 62, name: "Dominican Republic", shortCode: "DO" },
      { id: 63, name: "Algeria", shortCode: "DZ" },
      { id: 64, name: "Ecuador", shortCode: "EC" },
      { id: 65, name: "Estonia", shortCode: "EE" },
      { id: 66, name: "Egypt", shortCode: "EG" },
      { id: 67, name: "Western Sahara", shortCode: "EH" },
      { id: 68, name: "Eritrea", shortCode: "ER" },
      { id: 69, name: "Spain", shortCode: "ES" },
      { id: 70, name: "Ethiopia", shortCode: "ET" },
      { id: 71, name: "Finland", shortCode: "FI" },
      { id: 72, name: "Fiji", shortCode: "FJ" },
      { id: 73, name: "Falkland Islands", shortCode: "FK" },
      { id: 74, name: "Micronesia", shortCode: "FM" },
      { id: 75, name: "Faroe Islands", shortCode: "FO" },
      { id: 76, name: "France", shortCode: "FR" },
      { id: 77, name: "Gabon", shortCode: "GA" },
      { id: 78, name: "United Kingdom", shortCode: "GB" },
      { id: 79, name: "Grenada", shortCode: "GD" },
      { id: 80, name: "Georgia", shortCode: "GE" },
      { id: 81, name: "French Guiana", shortCode: "GF" },
      { id: 82, name: "Guernsey", shortCode: "GG" },
      { id: 83, name: "Ghana", shortCode: "GH" },
      { id: 84, name: "Gibraltar", shortCode: "GI" },
      { id: 85, name: "Greenland", shortCode: "GL" },
      { id: 86, name: "Gambia", shortCode: "GM" },
      { id: 87, name: "Guinea", shortCode: "GN" },
      { id: 88, name: "Guadeloupe", shortCode: "GP" },
      { id: 89, name: "Equatorial Guinea", shortCode: "GQ" },
      { id: 90, name: "Greece", shortCode: "GR" },
      {
        id: 91,
        name: "South Georgia and the South Sandwich Islands",
        shortCode: "GS",
      },
      { id: 92, name: "Guatemala", shortCode: "GT" },
      { id: 93, name: "Guam", shortCode: "GU" },
      { id: 94, name: "Guinea-Bissau", shortCode: "GW" },
      { id: 95, name: "Guyana", shortCode: "GY" },
      { id: 96, name: "Hong Kong", shortCode: "HK" },
      { id: 97, name: "Heard Island and McDonald Islands", shortCode: "HM" },
      { id: 98, name: "Honduras", shortCode: "HN" },
      { id: 99, name: "Croatia", shortCode: "HR" },
      { id: 100, name: "Haiti", shortCode: "HT" },
      { id: 101, name: "Hungary", shortCode: "HU" },
      { id: 102, name: "Indonesia", shortCode: "ID" },
      { id: 103, name: "Ireland", shortCode: "IE" },
      { id: 104, name: "Israel", shortCode: "IL" },
      { id: 105, name: "Isle of Man", shortCode: "IM" },
      { id: 106, name: "India", shortCode: "IN" },
      { id: 107, name: "British Indian Ocean Territory", shortCode: "IO" },
      { id: 108, name: "Iraq", shortCode: "IQ" },
      { id: 109, name: "Iran", shortCode: "IR" },
      { id: 110, name: "Iceland", shortCode: "IS" },
      { id: 111, name: "Italy", shortCode: "IT" },
      { id: 112, name: "Jersey", shortCode: "JE" },
      { id: 113, name: "Jamaica", shortCode: "JM" },
      { id: 114, name: "Jordan", shortCode: "JO" },
      { id: 115, name: "Japan", shortCode: "JP" },
      { id: 116, name: "Kenya", shortCode: "KE" },
      { id: 117, name: "Kyrgyzstan", shortCode: "KG" },
      { id: 118, name: "Cambodia", shortCode: "KH" },
      { id: 119, name: "Kiribati", shortCode: "KI" },
      { id: 120, name: "Comoros", shortCode: "KM" },
      { id: 121, name: "Saint Kitts and Nevis", shortCode: "KN" },
      { id: 122, name: "North Korea", shortCode: "KP" },
      { id: 123, name: "South Korea", shortCode: "KR" },
      { id: 124, name: "Kosovo", shortCode: "XK" },
      { id: 125, name: "Kuwait", shortCode: "KW" },
      { id: 126, name: "Cayman Islands", shortCode: "KY" },
      { id: 127, name: "Kazakhstan", shortCode: "KZ" },
      { id: 128, name: "Laos", shortCode: "LA" },
      { id: 129, name: "Lebanon", shortCode: "LB" },
      { id: 130, name: "Saint Lucia", shortCode: "LC" },
      { id: 131, name: "Liechtenstein", shortCode: "LI" },
      { id: 132, name: "Sri Lanka", shortCode: "LK" },
      { id: 133, name: "Liberia", shortCode: "LR" },
      { id: 134, name: "Lesotho", shortCode: "LS" },
      { id: 135, name: "Lithuania", shortCode: "LT" },
      { id: 136, name: "Luxembourg", shortCode: "LU" },
      { id: 137, name: "Latvia", shortCode: "LV" },
      { id: 138, name: "Libya", shortCode: "LY" },
      { id: 139, name: "Morocco", shortCode: "MA" },
      { id: 140, name: "Monaco", shortCode: "MC" },
      { id: 141, name: "Moldova", shortCode: "MD" },
      { id: 142, name: "Montenegro", shortCode: "ME" },
      { id: 143, name: "Saint Martin", shortCode: "MF" },
      { id: 144, name: "Madagascar", shortCode: "MG" },
      { id: 145, name: "Marshall Islands", shortCode: "MH" },
      { id: 146, name: "North Macedonia", shortCode: "MK" },
      { id: 147, name: "Mali", shortCode: "ML" },
      { id: 148, name: "Myanmar", shortCode: "MM" },
      { id: 149, name: "Mongolia", shortCode: "MN" },
      { id: 150, name: "Macao", shortCode: "MO" },
      { id: 151, name: "Northern Mariana Islands", shortCode: "MP" },
      { id: 152, name: "Martinique", shortCode: "MQ" },
      { id: 153, name: "Mauritania", shortCode: "MR" },
      { id: 154, name: "Montserrat", shortCode: "MS" },
      { id: 155, name: "Malta", shortCode: "MT" },
      { id: 156, name: "Mauritius", shortCode: "MU" },
      { id: 157, name: "Maldives", shortCode: "MV" },
      { id: 158, name: "Malawi", shortCode: "MW" },
      { id: 159, name: "Mexico", shortCode: "MX" },
      { id: 160, name: "Malaysia", shortCode: "MY" },
      { id: 161, name: "Mozambique", shortCode: "MZ" },
      { id: 162, name: "Namibia", shortCode: "NA" },
      { id: 163, name: "New Caledonia", shortCode: "NC" },
      { id: 164, name: "Niger", shortCode: "NE" },
      { id: 165, name: "Norfolk Island", shortCode: "NF" },
      { id: 166, name: "Nigeria", shortCode: "NG" },
      { id: 167, name: "Nicaragua", shortCode: "NI" },
      { id: 168, name: "Netherlands", shortCode: "NL" },
      { id: 169, name: "Norway", shortCode: "NO" },
      { id: 170, name: "Nepal", shortCode: "NP" },
      { id: 171, name: "Nauru", shortCode: "NR" },
      { id: 172, name: "Niue", shortCode: "NU" },
      { id: 173, name: "New Zealand", shortCode: "NZ" },
      { id: 174, name: "Oman", shortCode: "OM" },
      { id: 175, name: "Panama", shortCode: "PA" },
      { id: 176, name: "Peru", shortCode: "PE" },
      { id: 177, name: "French Polynesia", shortCode: "PF" },
      { id: 178, name: "Papua New Guinea", shortCode: "PG" },
      { id: 179, name: "Philippines", shortCode: "PH" },
      { id: 180, name: "Pakistan", shortCode: "PK" },
      { id: 181, name: "Poland", shortCode: "PL" },
      { id: 182, name: "Saint Pierre and Miquelon", shortCode: "PM" },
      { id: 183, name: "Pitcairn", shortCode: "PN" },
      { id: 184, name: "Puerto Rico", shortCode: "PR" },
      { id: 185, name: "Palestinian Territory", shortCode: "PS" },
      { id: 186, name: "Portugal", shortCode: "PT" },
      { id: 187, name: "Palau", shortCode: "PW" },
      { id: 188, name: "Paraguay", shortCode: "PY" },
      { id: 189, name: "Qatar", shortCode: "QA" },
      { id: 190, name: "Reunion", shortCode: "RE" },
      { id: 191, name: "Romania", shortCode: "RO" },
      { id: 192, name: "Serbia", shortCode: "RS" },
      { id: 193, name: "Russia", shortCode: "RU" },
      { id: 194, name: "Rwanda", shortCode: "RW" },
      { id: 195, name: "Saudi Arabia", shortCode: "SA" },
      { id: 196, name: "Solomon Islands", shortCode: "SB" },
      { id: 197, name: "Seychelles", shortCode: "SC" },
      { id: 198, name: "Sudan", shortCode: "SD" },
      { id: 199, name: "South Sudan", shortCode: "SS" },
      { id: 200, name: "Sweden", shortCode: "SE" },
      { id: 201, name: "Singapore", shortCode: "SG" },
      { id: 202, name: "Saint Helena", shortCode: "SH" },
      { id: 203, name: "Slovenia", shortCode: "SI" },
      { id: 204, name: "Svalbard and Jan Mayen", shortCode: "SJ" },
      { id: 205, name: "Slovakia", shortCode: "SK" },
      { id: 206, name: "Sierra Leone", shortCode: "SL" },
      { id: 207, name: "San Marino", shortCode: "SM" },
      { id: 208, name: "Senegal", shortCode: "SN" },
      { id: 209, name: "Somalia", shortCode: "SO" },
      { id: 210, name: "Suriname", shortCode: "SR" },
      { id: 211, name: "Sao Tome and Principe", shortCode: "ST" },
      { id: 212, name: "El Salvador", shortCode: "SV" },
      { id: 213, name: "Sint Maarten", shortCode: "SX" },
      { id: 214, name: "Syria", shortCode: "SY" },
      { id: 215, name: "Eswatini", shortCode: "SZ" },
      { id: 216, name: "Turks and Caicos Islands", shortCode: "TC" },
      { id: 217, name: "Chad", shortCode: "TD" },
      { id: 218, name: "French Southern Territories", shortCode: "TF" },
      { id: 219, name: "Togo", shortCode: "TG" },
      { id: 220, name: "Thailand", shortCode: "TH" },
      { id: 221, name: "Tajikistan", shortCode: "TJ" },
      { id: 222, name: "Tokelau", shortCode: "TK" },
      { id: 223, name: "Timor Leste", shortCode: "TL" },
      { id: 224, name: "Turkmenistan", shortCode: "TM" },
      { id: 225, name: "Tunisia", shortCode: "TN" },
      { id: 226, name: "Tonga", shortCode: "TO" },
      { id: 227, name: "Turkey", shortCode: "TR" },
      { id: 228, name: "Trinidad and Tobago", shortCode: "TT" },
      { id: 229, name: "Tuvalu", shortCode: "TV" },
      { id: 230, name: "Taiwan", shortCode: "TW" },
      { id: 231, name: "Tanzania", shortCode: "TZ" },
      { id: 232, name: "Ukraine", shortCode: "UA" },
      { id: 233, name: "Uganda", shortCode: "UG" },
      {
        id: 234,
        name: "United States Minor Outlying Islands",
        shortCode: "UM",
      },
      { id: 236, name: "Uruguay", shortCode: "UY" },
      { id: 237, name: "Uzbekistan", shortCode: "UZ" },
      { id: 238, name: "Vatican", shortCode: "VA" },
      { id: 239, name: "Saint Vincent and the Grenadines", shortCode: "VC" },
      { id: 240, name: "Venezuela", shortCode: "VE" },
      { id: 241, name: "British Virgin Islands", shortCode: "VG" },
      { id: 242, name: "U.S. Virgin Islands", shortCode: "VI" },
      { id: 243, name: "Vietnam", shortCode: "VN" },
      { id: 244, name: "Vanuatu", shortCode: "VU" },
      { id: 245, name: "Wallis and Futuna", shortCode: "WF" },
      { id: 246, name: "Samoa", shortCode: "WS" },
      { id: 247, name: "Yemen", shortCode: "YE" },
      { id: 248, name: "Mayotte", shortCode: "YT" },
      { id: 249, name: "South Africa", shortCode: "ZA" },
      { id: 250, name: "Zambia", shortCode: "ZM" },
      { id: 251, name: "Zimbabwe", shortCode: "ZW" },
      { id: 252, name: "Serbia and Montenegro", shortCode: "CS" },
      { id: 253, name: "Netherlands Antilles", shortCode: "AN" },
    ],
  },
  commonManager: {},
  employment: {},
  business: {},
  employmentHistory: {},
  militaryIncomeManager: {
    militaryEmployer :{
        EmployerName:"US Army",
        JobTitle:"Major",
        startDate: "11/11/2019",
        YearsInProfession: 5,
    }
  },
  otherIncomeManager:{},
  assetsManager: {}
};
const dispatch = jest.fn();

const setupGoogleMock = () => {
  /*** Mock Google Maps JavaScript API ***/
  const google = {
    maps: {
      places: {
        AutocompleteService: () => {},
        PlacesServiceStatus: {
          INVALID_REQUEST: "INVALID_REQUEST",
          NOT_FOUND: "NOT_FOUND",
          OK: "OK",
          OVER_QUERY_LIMIT: "OVER_QUERY_LIMIT",
          REQUEST_DENIED: "REQUEST_DENIED",
          UNKNOWN_ERROR: "UNKNOWN_ERROR",
          ZERO_RESULTS: "ZERO_RESULTS",
        },
      },
      Geocoder: () => {},
      GeocoderStatus: {
        ERROR: "ERROR",
        INVALID_REQUEST: "INVALID_REQUEST",
        OK: "OK",
        OVER_QUERY_LIMIT: "OVER_QUERY_LIMIT",
        REQUEST_DENIED: "REQUEST_DENIED",
        UNKNOWN_ERROR: "UNKNOWN_ERROR",
        ZERO_RESULTS: "ZERO_RESULTS",
      },
    },
  };
  // window.google = google;
};

beforeEach(() => {
  setupGoogleMock();
  MockEnvConfig();
  MockSessionStorage();
});

describe("Military Service Location Address ", () => {
 
    test("Should change street Address", async () => {
    const { getAllByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <MilitaryServiceLocation />
      </Store.Provider>
    );

    let streetAddress;
    await waitFor(() => {
      streetAddress = getAllByTestId("street_address");
     
      fireEvent.change(streetAddress[1], { target: { value: "Street 1" } });
    });
    await waitFor(() => {
      expect(streetAddress[1]).toHaveDisplayValue("Street 1");
    });
  });

  test("Should change unit field", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <MilitaryServiceLocation />
      </Store.Provider>
    );

    let unit;
    await waitFor(() => {
      unit = getByTestId("Unit");
     
      fireEvent.change(unit, { target: { value: "unit 123" } });
    });
    await waitFor(() => {
      expect(unit).toHaveDisplayValue("unit 123");
    });
  });

  test("Should change city field", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <MilitaryServiceLocation />
      </Store.Provider>
    );

    let city;
    await waitFor(() => {
      city = getByTestId("City");
     
      fireEvent.change(city, { target: { value: "Austin" } });
    });
    await waitFor(() => {
      expect(city).toHaveDisplayValue("Austin");
    });
  });

  test("Should change state field", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <MilitaryServiceLocation />
      </Store.Provider>
    );

    let stateName;
    let stateTxt;
    await waitFor(() => {
      stateName = getByTestId("state");
      stateTxt = stateName.children[0].children[0];
      fireEvent.change(stateTxt, { target: { value: "Texas" } });
    });
    await waitFor(() => {
      expect(stateTxt).toHaveDisplayValue("Texas");
    });
  });

  test("Should change zip code field", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <MilitaryServiceLocation />
      </Store.Provider>
    );

    let zipCode;
    await waitFor(() => {
      zipCode = getByTestId("Zip-Code");
    
      fireEvent.change(zipCode, { target: { value: "78717" } });
    });
    await waitFor(() => {
      expect(zipCode).toHaveDisplayValue("78717");
    });
  });

  test("Should change country field", async () => {
    const { getByTestId } = render(
      <Store.Provider value={{ state, dispatch }}>
        <MilitaryServiceLocation />
      </Store.Provider>
    );

    let country;
    let countryTxt;
    await waitFor(() => {
      country = getByTestId("country");
    
      countryTxt = country.children[0].children[0];
      fireEvent.change(countryTxt, { target: { value: "United States" } });
    });
    await waitFor(() => {
      expect(countryTxt).toHaveDisplayValue("United States");
    });
  });

  test("Should show errors on empty form", async () => {
    const { getByTestId } = render(<MilitaryServiceLocation />);

    let SubmitBtn;
    await waitFor(() => {
      SubmitBtn = getByTestId("employer-address-next");
      fireEvent.click(SubmitBtn);
    });
    await waitFor(() => {
      expect(getByTestId("street_address-error")).toBeInTheDocument();
      expect(getByTestId("city-error")).toBeInTheDocument();
      expect(getByTestId("state-error")).toBeInTheDocument();
      expect(getByTestId("zip_code-error")).toBeInTheDocument();
    });
  });

  test("Should submit form if all fields are filled", async () => {
    const { getByTestId, getAllByTestId } = render(<MilitaryServiceLocation />);
    await waitFor(() => {
      fireEvent.change(getAllByTestId("street_address")[1], {
        target: { value: "Street 1" },
      });

      fireEvent.change(getByTestId("City"), { target: { value: "Austin" } });
      let stateName = getByTestId("state");
      let stateTxt = stateName.children[0].children[0];
      fireEvent.change(stateTxt, { target: { value: "Texas" } });

      fireEvent.change(getByTestId("Zip-Code"), { target: { value: "78717" } });

      let country = getByTestId("country");
      let countryTxt:Element = country?.children[0]?.children[0];
      fireEvent.change(countryTxt, { target: { value: "United States" } });
    });
    let SubmitBtn;
    await waitFor(() => {
      SubmitBtn = getByTestId("employer-address-next");
      fireEvent.click(SubmitBtn);
    });
  });

});
