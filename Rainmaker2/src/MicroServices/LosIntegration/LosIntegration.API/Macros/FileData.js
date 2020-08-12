if (ev.TableAndFieldName == "FileData.AgencyCaseNo" || ev.TableAndFieldName == "FileData.OccupancyType") {
    var occupancyType = los.GetFieldValue("FileData.OccupancyType");
    var agencyCaseNo = los.GetFieldValue("FileData.AgencyCaseNo");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"occupancyType\"\":\"\"{{occupancyType}}\"\" ,\"\"agencyCaseNo\"\":\"\"{{agencyCaseNo}}\"\" , \"\"fileDataId\"\":{{fileDataId}} }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:5050/api/ByteWebConnector/FileData/update\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{occupancyType}}", occupancyType);
    dataRaw = dataRaw.replace("{{agencyCaseNo}}", agencyCaseNo);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);
    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
};

