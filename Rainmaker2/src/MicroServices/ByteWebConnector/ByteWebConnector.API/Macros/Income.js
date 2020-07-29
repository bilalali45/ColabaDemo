if (ev.TableAndFieldName == "Income.Amount" || ev.TableAndFieldName == "Income.IncomeType" || ev.TableAndFieldName == "Income.DescriptionOV") {
    var amount = ev.Table.GetFieldValue("Amount");
    var incomeType = ev.Table.GetField("IncomeType");
    var descriptionOV = ev.Table.GetFieldValue("DescriptionOV");
    var borId = ev.Table.GetFieldValue("BorrowerID");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"amount\"\":\"\"{{amount}}\"\" ,\"\"incomeType\"\":\"{{incomeType}}\" , \"\"descriptionOV\"\":\"\"{{descriptionOV}}\"\" , \"\"borrowerID\"\":\"{{borId}}\" , \"\"fileDataId\"\":\"{{fileDataId}}\"  }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/income\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{amount}}", amount);
    dataRaw = dataRaw.replace("{{incomeType}}", incomeType);
    dataRaw = dataRaw.replace("{{descriptionOV}}", descriptionOV);
    dataRaw = dataRaw.replace("{{borId}}", borId);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);
    los.Application.ShowMessageBox("arguments " + arguments);
    System.Diagnostics.Process.Start("curl.exe", arguments);
}