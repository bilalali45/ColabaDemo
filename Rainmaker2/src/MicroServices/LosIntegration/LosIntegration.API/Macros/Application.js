if (ev.TableAndFieldName == "Application.ApplicationMethod" || ev.TableAndFieldName == "Application.LifeInsCashValue") {
    var lifeInsCashValue = los.GetField("1003App1.LifeInsCashValue");
    var ApplicationMethod = los.GetField("1003App1.ApplicationMethod");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var applicationID = ev.Table.GetFieldValue("ApplicationID");
    var borrowerID = ev.Table.GetFieldValue("BorrowerID");
    var coBorrowerID = ev.Table.GetFieldValue("CoBorrowerID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"lifeInsCashValue\"\":{{lifeInsCashValue}} ,\"\"ApplicationMethod\"\":{{ApplicationMethod}}, \"\"fileDataId\"\":\"{{fileDataId}}\", \"\"applicationID\"\":\"{{applicationID}}\", \"\"borrowerID\"\":\"{{borrowerID}}\", \"\"coBorrowerID\"\":\"{{coBorrowerID}}\" }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:5050/api/ByteWebConnector/application/update\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{lifeInsCashValue}}", (lifeInsCashValue) ? lifeInsCashValue : null);
    dataRaw = dataRaw.replace("{{ApplicationMethod}}", ApplicationMethod);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);
    dataRaw = dataRaw.replace("{{applicationID}}", applicationID);
    dataRaw = dataRaw.replace("{{borrowerID}}", borrowerID);
    dataRaw = dataRaw.replace("{{coBorrowerID}}", coBorrowerID);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
};