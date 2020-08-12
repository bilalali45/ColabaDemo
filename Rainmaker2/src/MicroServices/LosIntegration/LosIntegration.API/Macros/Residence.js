if (ev.TableAndFieldName == "Residence.City" || ev.TableAndFieldName == "Residence.State" || ev.TableAndFieldName == "Residence.Country"
    || ev.TableAndFieldName == "Residence.Street" || ev.TableAndFieldName == "Residence.Zip" || ev.TableAndFieldName == "Residence.Current" || ev.TableAndFieldName == "Residence.LivingStatus"
    || ev.TableAndFieldName == "Residence.NoMonths" || ev.TableAndFieldName == "Residence.NoYears" || ev.TableAndFieldName == "Residence.MonthlyRent") {

    var city = ev.Table.GetFieldValue("City");
    var country = ev.Table.GetFieldValue("Country");
    var state = ev.Table.GetFieldValue("State");
    var street = ev.Table.GetFieldValue("Street");
    var zip = ev.Table.GetFieldValue("Zip");
    var current = ev.Table.GetFieldValue("Current");
    var livingStatus = ev.Table.GetFieldValue("LivingStatus");
    var noMonths = ev.Table.GetFieldValue("NoMonths");
    var noYears = ev.Table.GetFieldValue("NoYears");
    var monthlyRent = ev.Table.GetFieldValue("MonthlyRent");
    var borrowerId = ev.Table.GetFieldValue("BorrowerID");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"city\"\":\"\"{{city}}\"\" ,\"\"country\"\":\"\"{{country}}\"\" , \"\"state\"\":\"\"{{state}}\"\" , \"\"street\"\":\"\"{{street}}\"\" , \"\"zip\"\":\"\"{{zip}}\"\" , \"\"current\"\":{{current}}, \"\"livingStatus\"\":\"\"{{livingStatus}}\"\", \"\"noMonths\"\":{{noMonths}}, \"\"noYears\"\":{{noYears}}, \"\"monthlyRent\"\":{{monthlyRent}}, \"\"borrowerID\"\":{{borrowerId}} , \"\"fileDataId\"\":{{fileDataId}} }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:5050/api/ByteWebConnector/BorrowerResidence/update\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{city}}", city);
    dataRaw = dataRaw.replace("{{country}}", country);
    dataRaw = dataRaw.replace("{{state}}", state);
    dataRaw = dataRaw.replace("{{street}}", street);
    dataRaw = dataRaw.replace("{{zip}}", zip);
    dataRaw = dataRaw.replace("{{current}}", current);
    dataRaw = dataRaw.replace("{{livingStatus}}", livingStatus);
    dataRaw = dataRaw.replace("{{noMonths}}", (noMonths) ? noMonths : null);
    dataRaw = dataRaw.replace("{{noYears}}", (noYears) ? noYears : null);
    dataRaw = dataRaw.replace("{{monthlyRent}}", (monthlyRent) ? monthlyRent : null);
    dataRaw = dataRaw.replace("{{borrowerId}}", borrowerId);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);
    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
};
