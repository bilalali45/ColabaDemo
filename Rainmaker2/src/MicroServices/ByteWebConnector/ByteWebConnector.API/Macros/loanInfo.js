if (ev.TableAndFieldName == "Loan.PurPrice" || ev.TableAndFieldName == "Loan.BaseLoan"
    || ev.TableAndFieldName == "Loan.SubFiBaseLoan" || ev.TableAndFieldName == "Loan.AmortizationType"
    || ev.TableAndFieldName == "Loan.MortgageType" || ev.TableAndFieldName == "Loan.RefinanceCashOutAmount"
    || ev.TableAndFieldName == "Loan.LoanPurpose") {
    var baseLoan = los.GetField("Loan.BaseLoan");
    var purPrice = los.GetField("Loan.PurPrice");
    var subFiBaseLoan = los.GetField("Loan.SubFiBaseLoan");
    var amortizationType = los.GetFieldValue("Loan.AmortizationType");
    var mortgageType = los.GetFieldValue("Loan.MortgageType");
    var RefinanceCashOutAmount = los.GetField("Loan.RefinanceCashOutAmount");
    var loanPurpose = los.GetFieldValue("Loan.LoanPurpose");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"baseLoan\"\":{{baseLoan}} ,\"\"purPrice\"\":{{purPrice}} ,\"\"subFiBaseLoan\"\":{{subFiBaseLoan}} ,\"\"amortizationType\"\":\"\"{{amortizationType}}\"\" ,\"\"mortgageType\"\":\"\"{{mortgageType}}\"\",\"\"RefinanceCashOutAmount\"\":{{RefinanceCashOutAmount}},\"\"loanPurpose\"\":\"\"{{loanPurpose}}\"\", \"\"fileDataId\"\":{{fileDataId}}   }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/loan\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{baseLoan}}", (baseLoan) ? baseLoan : null);
    dataRaw = dataRaw.replace("{{purPrice}}", (purPrice) ? purPrice : null);
    dataRaw = dataRaw.replace("{{subFiBaseLoan}}", (subFiBaseLoan) ? subFiBaseLoan : null);
    dataRaw = dataRaw.replace("{{amortizationType}}", amortizationType);
    dataRaw = dataRaw.replace("{{mortgageType}}", mortgageType);
    dataRaw = dataRaw.replace("{{RefinanceCashOutAmount}}", (RefinanceCashOutAmount) ? RefinanceCashOutAmount : null);
    dataRaw = dataRaw.replace("{{loanPurpose}}", loanPurpose);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
};



