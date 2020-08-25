if (ev.TableAndFieldName == "REO.City" || ev.TableAndFieldName == "REO.State" || ev.TableAndFieldName == "REO.Street" || ev.TableAndFieldName == "REO.Zip"
    || ev.TableAndFieldName == "REO.CurrentUsageType" || ev.TableAndFieldName == "REO.REOType"
    || ev.TableAndFieldName == "REO.IsSubjectProperty" || ev.TableAndFieldName == "REO.MarketValue" || ev.TableAndFieldName == "REO.REOStatus" || ev.TableAndFieldName == "REO.GrossRentalIncome"
    || ev.TableAndFieldName == "REO.Taxes") {


    var city = ev.Table.GetFieldValue("City");
    var state = ev.Table.GetFieldValue("State");
    var street = ev.Table.GetFieldValue("Street");
    var zip = ev.Table.GetFieldValue("Zip");
    var currentUsageType = ev.Table.GetFieldValue("CurrentUsageType");
    var reoType = ev.Table.GetFieldValue("REOType");
    var isSubjectProperty = ev.Table.GetFieldValue("IsSubjectProperty");
    var marketValue = ev.Table.GetFieldValue("MarketValue");
    var reoStatus = ev.Table.GetFieldValue("REOStatus");
    var grossRentalIncome = ev.Table.GetFieldValue("GrossRentalIncome");
    var taxes = ev.Table.GetFieldValue("Taxes");
    var borrowerId = ev.Table.GetFieldValue("BorrowerID");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"city\"\":\"\"{{city}}\"\" ,\"\"state\"\":\"\"{{state}}\"\" , \"\"street\"\":\"\"{{street}}\"\" , \"\"zip\"\":\"\"{{zip}}\"\" , \"\"currentUsageType\"\":\"\"{{currentUsageType}}\"\" , \"\"reoType\"\":\"\"{{reoType}}\"\", \"\"isSubjectProperty\"\":{{isSubjectProperty}}, \"\"marketValue\"\":{{marketValue}}, \"\"reoStatus\"\":\"\"{{reoStatus}}\"\", \"\"grossRentalIncome\"\":{{grossRentalIncome}}, \"\"taxes\"\":{{taxes}},\"\"borrowerId\"\":{{borrowerId}},\"\"fileDataId\"\":{{fileDataId}}  }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:5050/api/ByteWebConnector/BorrowerReo/update\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{city}}", city);
    dataRaw = dataRaw.replace("{{state}}", state);
    dataRaw = dataRaw.replace("{{street}}", street);
    dataRaw = dataRaw.replace("{{zip}}", zip);
    dataRaw = dataRaw.replace("{{currentUsageType}}", currentUsageType);
    dataRaw = dataRaw.replace("{{isSubjectProperty}}", isSubjectProperty);
    dataRaw = dataRaw.replace("{{reoType}}", reoType);
    dataRaw = dataRaw.replace("{{marketValue}}", (marketValue) ? marketValue : null);
    dataRaw = dataRaw.replace("{{reoStatus}}", reoStatus);
    dataRaw = dataRaw.replace("{{grossRentalIncome}}", (grossRentalIncome) ? grossRentalIncome : null);
    dataRaw = dataRaw.replace("{{taxes}}", (taxes) ? taxes : null);
    dataRaw = dataRaw.replace("{{borrowerId}}", borrowerId);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);
    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
}