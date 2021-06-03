import { BorrowerEndPoints } from "./BorrowerEndPoints";
import { GettingStartedEndpoints } from "./GettingStartedEndpoints";
import { GettingToKnowYouEndpoints } from "./GettingToKnowYouEndpoints";
import { CommonEndpoints } from "./CommonEndpoints";
import { DobSSNEndpoints } from "./DobSSNEndPoints";
import { LoanApplicationEndpoints } from "./LoanAplicationEndpoints";
import { MyNewMortgageEndpoints } from "./MyNewMortgageEndpoints";
import { MyNewPropertyAddressEndpoints } from "./MyNewPropertyAddressEndpoints";
import { EmploymentEndpoints } from './EmploymentEndPoints'
import { BusinessEndPoints } from './BusinessEndPoints'
import { RetirementIncomeEndpoints } from './RetirementIncomeEndpoints'
import { IncomeEndpoints } from './IncomeEndpoints';
import { MilitaryIncomeEndpoints } from './MilitaryIncomeEndpoints'
import { EmploymentHistoryEndpoints } from "./EmploymentHistoryEndPoints";
import { GiftAssetEndpoints } from './GiftAssetEndpoints';
import { AssetsEndpoints } from "./AssetsEndpoints";
import { MyPropertyEndPoints } from "./MyPropertyEndPoints";
import {TransactionProceedsEndpoints} from "./TransactionProceedsEndpoints";
import { OtherAssetsEndpoints } from "./OtherAssetsEndpoints";

export class Endpoints {
  static GettingToKnowYou = GettingToKnowYouEndpoints;
  static GettingStarted = GettingStartedEndpoints;
  static Borrower = BorrowerEndPoints;
  static Common = CommonEndpoints;
  static DobSSN = DobSSNEndpoints;
  static LoanApplication = LoanApplicationEndpoints;
  static MyNewMortgage = MyNewMortgageEndpoints;
  static MyNewPropertyAddress = MyNewPropertyAddressEndpoints;
  static Employemnt = EmploymentEndpoints;
  static Business = BusinessEndPoints;
  static Income = IncomeEndpoints;
  static MilitaryIncome = MilitaryIncomeEndpoints;
  static RetirementIncome = RetirementIncomeEndpoints
  static EmploymentHistory = EmploymentHistoryEndpoints;
  static GiftAssetEndpoints = GiftAssetEndpoints;
  static AssetsEndpoints = AssetsEndpoints;
  static MyProperty = MyPropertyEndPoints;
  static TransactionProceeds = TransactionProceedsEndpoints;
  static OtherAssets = OtherAssetsEndpoints;
}