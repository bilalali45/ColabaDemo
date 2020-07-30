if (ev.TableAndFieldName == "Party.ContactNMLSID" || ev.TableAndFieldName == "Party.FirstName" || ev.TableAndFieldName == "Party.EMail" || ev.TableAndFieldName == "Party.WorkPhone") {
    var firstName = los.GetFieldValue("LO.FirstName");
    var contactNMLSID = los.GetFieldValue("LO.ContactNMLSID");
    var workPhone = los.GetFieldValue("LO.WorkPhone");
    var email = los.GetFieldValue("LO.EMail");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"firstName\"\":\"\"{{firstName}}\"\" ,\"\"contactNMLSID\"\":\"\"{{contactNMLSID}}\"\", \"\"workPhone\"\":\"\"{{workPhone}}\"\" ,\"\"email\"\":\"\"{{email}}\"\", \"\"fileDataId\"\":{{fileDataId}}  }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:5050/api/ByteWebConnector/Parties/update\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{firstName}}", firstName);
    dataRaw = dataRaw.replace("{{contactNMLSID}}", contactNMLSID);
    dataRaw = dataRaw.replace("{{workPhone}}", workPhone);
    dataRaw = dataRaw.replace("{{email}}", email);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);
    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
};