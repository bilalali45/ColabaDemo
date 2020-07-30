if (ev.TableAndFieldName == "FileData.AgencyCaseNo" || ev.TableAndFieldName == "FileData.OccupancyType") {
    var occuType = los.GetField("FileData.OccupancyType");
    var agencyCaseNo = los.GetField("FileData.AgencyCaseNo");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"occupancyType\"\":\"{{occuType}}\" ,\"\"agencyCaseNo\"\":\"\"{{agencyCaseNo}}\"\" , \"\"fileDataId\"\":\"{{fileDataId}}\" }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/filedata\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{occuType}}", occuType);
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

