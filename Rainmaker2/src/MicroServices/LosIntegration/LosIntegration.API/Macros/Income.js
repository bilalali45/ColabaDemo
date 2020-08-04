if (ev.TableAndFieldName == "Income.Amount" || ev.TableAndFieldName == "Income.IncomeType" || ev.TableAndFieldName == "Income.DescriptionOV") {
    var amount = ev.Table.GetFieldValue("Amount");
    var incomeType = ev.Table.GetFieldValue("IncomeType");
    var descriptionOV = ev.Table.GetFieldValue("DescriptionOV");
    var borrowerId = ev.Table.GetFieldValue("BorrowerID");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"amount\"\":{{amount}} ,\"\"incomeType\"\":\"\"{{incomeType}}\"\" , \"\"descriptionOV\"\":\"\"{{descriptionOV}}\"\" , \"\"borrowerID\"\":{{borrowerId}} , \"\"fileDataId\"\":{{fileDataId}}  }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:5050/api/ByteWebConnector/BorrowerIncome/update\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{amount}}", (amount) ? amount : null);
    dataRaw = dataRaw.replace("{{incomeType}}", incomeType);
    dataRaw = dataRaw.replace("{{descriptionOV}}", descriptionOV);
    dataRaw = dataRaw.replace("{{borrowerId}}", (borrowerId) ? borrowerId : null);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);
    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
}