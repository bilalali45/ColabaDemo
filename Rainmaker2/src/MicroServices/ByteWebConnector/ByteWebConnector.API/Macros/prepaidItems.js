if (ev.TableAndFieldName == "PrepaidItem.Payment") {

    var hazPayment = los.GetField("Haz.Payment");
    var propTaxPayment = los.GetField("PropTax.Payment");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"HazPayment\"\":\"{{hazPayment}}\" ,\"\"propTaxPayment\"\":\"{{propTaxPayment}}\" , \"\"fileDataId\"\":\"{{fileDataId}}\"  }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/prepaid\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{hazPayment}}", hazPayment);
    dataRaw = dataRaw.replace("{{propTaxPayment}}", propTaxPayment);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);
    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
};





