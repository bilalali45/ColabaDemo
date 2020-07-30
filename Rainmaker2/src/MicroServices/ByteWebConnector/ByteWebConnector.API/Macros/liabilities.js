if (ev.TableAndFieldName == "Debt.Name" || ev.TableAndFieldName == "Debt.DebtType" || ev.TableAndFieldName == "Debt.UnpaidBal"
    || ev.TableAndFieldName == "Debt.PaymentsLeft" || ev.TableAndFieldName == "Debt.ToBePaidOff" || ev.TableAndFieldName == "Debt.MoPayment"
    || ev.TableAndFieldName == "Debt.AccountNo" || ev.TableAndFieldName == "Debt.Notes") {
    var name = ev.Table.GetFieldValue("Name");
    var debtIndex = ev.Table.GetField("DebtType");
    var unpaidBal = ev.Table.GetFieldValue("UnpaidBal");
    var paymentsLeft = ev.Table.GetFieldValue("PaymentsLeft");
    var tobepaidoff = ev.Table.GetFieldValue("ToBePaidOff");
    var moPayment = ev.Table.GetFieldValue("MoPayment");
    var accountNo = ev.Table.GetFieldValue("AccountNo");
    var notes = ev.Table.GetFieldValue("Notes");
    var borrowerID = ev.Table.GetFieldValue("BorrowerID");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");

    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"name\"\":\"\"{{name}}\"\" ,\"\"debtType\"\":\"{{debtIndex}}\" , \"\"unpaidBal\"\":\"\"{{unpaidBal}}\"\" , \"\"paymentsLeft\"\":\"\"{{paymentsLeft}}\"\" , \"\"tobepaidoff\"\":\"{{tobepaidoff}}\" , \"\"borrowerID\"\":\"\"{{borrowerID}}\"\" , \"\"fileDataId\"\":\"{{fileDataId}}\" , \"\"moPayment\"\":\"\"{{moPayment}}\"\" , \"\"accountNo\"\":\"\"{{accountNo}}\"\" , \"\"notes\"\":\"\"{{notes}}\"\"    }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/debt\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{name}}", name);
    dataRaw = dataRaw.replace("{{debtIndex}}", debtIndex);
    dataRaw = dataRaw.replace("{{unpaidBal}}", unpaidBal);
    dataRaw = dataRaw.replace("{{paymentsLeft}}", paymentsLeft);
    dataRaw = dataRaw.replace("{{moPayment}}", moPayment);
    dataRaw = dataRaw.replace("{{accountNo}}", accountNo);
    dataRaw = dataRaw.replace("{{notes}}", notes);
    dataRaw = dataRaw.replace("{{tobepaidoff}}", tobepaidoff);
    dataRaw = dataRaw.replace("{{borrowerID}}", (borrowerID)? borrowerID : null);    
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);
    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
}