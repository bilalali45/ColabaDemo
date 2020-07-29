if (ev.TableAndFieldName == "Loan.PurPrice" || ev.TableAndFieldName == "Loan.BaseLoan"
    || ev.TableAndFieldName == "Loan.SubFiBaseLoan" || ev.TableAndFieldName == "Loan.AmortizationType"
    || ev.TableAndFieldName == "Loan.MortgageType" || ev.TableAndFieldName == "Loan.RefinanceCashOutAmount"
    || ev.TableAndFieldName == "Loan.LoanPurpose") {
    var baseLoan = los.GetField("Loan.BaseLoan");
    var purPrice = los.GetField("Loan.PurPrice");
    var subFiBaseLoan = los.GetField("Loan.SubFiBaseLoan");
    var amortizationType = los.GetField("Loan.AmortizationType");
    var mortgageType = los.GetField("Loan.MortgageType");
    var RefinanceCashOutAmount = los.GetField("Loan.RefinanceCashOutAmount");
    var loanPurpose = los.GetField("Loan.LoanPurpose");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"baseLoan\"\":\"{{baseLoan}}\" ,\"\"purPrice\"\":\"{{purPrice}}\" ,\"\"subFiBaseLoan\"\":\"\"{{subFiBaseLoan}}\"\" ,\"\"amortizationType\"\":\"{{amortizationType}}\" ,\"\"mortgageType\"\":\"{{mortgageType}}\",\"\"RefinanceCashOutAmount\"\":\"\"{{RefinanceCashOutAmount}}\"\",\"\"loanPurpose\"\":\"{{loanPurpose}}\", \"\"fileDataId\"\":\"{{fileDataId}}\"   }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/loan\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{baseLoan}}", baseLoan);
    dataRaw = dataRaw.replace("{{purPrice}}", purPrice);
    dataRaw = dataRaw.replace("{{subFiBaseLoan}}", subFiBaseLoan);
    dataRaw = dataRaw.replace("{{amortizationType}}", amortizationType);
    dataRaw = dataRaw.replace("{{mortgageType}}", mortgageType);
    dataRaw = dataRaw.replace("{{RefinanceCashOutAmount}}", RefinanceCashOutAmount);
    dataRaw = dataRaw.replace("{{loanPurpose}}", loanPurpose);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);
    
    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    los.Application.ShowMessageBox("arguments " + arguments);
    System.Diagnostics.Process.Start("curl.exe", arguments);
};



