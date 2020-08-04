if (ev.TableAndFieldName == "Status.ApplicationDate" || ev.TableAndFieldName == "Status.SchedClosingDate") {
    var appDate = los.GetFieldValue("Status.ApplicationDate");
    var schedClosingDate = los.GetFieldValue("Status.SchedClosingDate");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");

    var useProxy = true;


    var dataRaw = " --data-raw \"{ \"\"applicationDate\"\":\"\"{{appDate}}\"\" ,\"\"schedClosingDate\"\":\"\"{{schedClosingDate}}\"\", \"\"fileDataId\"\":{{fileDataId}} } ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:5050/api/ByteWebConnector/Status/update\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{appDate}}", (appDate) ? appDate : null);
    dataRaw = dataRaw.replace("{{schedClosingDate}}", (schedClosingDate) ? schedClosingDate : null);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);
    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
};