if (ev.TableAndFieldName == "Residence.City" || ev.TableAndFieldName == "Residence.State" || ev.TableAndFieldName == "Residence.Country"
    || ev.TableAndFieldName == "Residence.Street" || ev.TableAndFieldName == "Residence.Zip" || ev.TableAndFieldName == "Residence.Current" || ev.TableAndFieldName == "Residence.LivingStatus"
    || ev.TableAndFieldName == "Residence.NoMonths" || ev.TableAndFieldName == "Residence.NoYears" || ev.TableAndFieldName == "Residence.MonthlyRent") {

    var city = ev.Table.GetFieldValue("City");
    var country = ev.Table.GetFieldValue("Country");
    var state = ev.Table.GetFieldValue("State");
    var street = ev.Table.GetFieldValue("Street");
    var zip = ev.Table.GetFieldValue("Zip");
    var current = ev.Table.GetFieldValue("Current");
    var livingStatus = ev.Table.GetField("LivingStatus");
    var noMonths = ev.Table.GetFieldValue("NoMonths");
    var noYears = ev.Table.GetFieldValue("NoYears");
    var monthlyRent = ev.Table.GetFieldValue("MonthlyRent");
    var borId = ev.Table.GetFieldValue("BorrowerID");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"city\"\":\"\"{{city}}\"\" ,\"\"country\"\":\"\"{{country}}\"\" , \"\"state\"\":\"\"{{state}}\"\" , \"\"street\"\":\"\"{{street}}\"\" , \"\"zip\"\":\"\"{{zip}}\"\" , \"\"current\"\":\"{{current}}\", \"\"livingStatus\"\":\"{{livingStatus}}\", \"\"noMonths\"\":\"\"{{noMonths}}\"\", \"\"noYears\"\":\"\"{{noYears}}\"\", \"\"monthlyRent\"\":\"\"{{monthlyRent}}\"\", \"\"borrowerID\"\":\"{{borId}}\" , \"\"fileDataId\"\":\"{{fileDataId}}\"  }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/residence\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

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
    dataRaw = dataRaw.replace("{{noMonths}}", noMonths);
    dataRaw = dataRaw.replace("{{noYears}}", noYears);
    dataRaw = dataRaw.replace("{{monthlyRent}}", monthlyRent);
    dataRaw = dataRaw.replace("{{borId}}", borId);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);
    los.Application.ShowMessageBox("arguments " + arguments);
    System.Diagnostics.Process.Start("curl.exe", arguments);
};
