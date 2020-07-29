if (ev.TableAndFieldName == "CustomFields.Field01" || ev.TableAndFieldName == "CustomFields.Field02" || ev.TableAndFieldName == "CustomFields.Field03" || ev.TableAndFieldName == "CustomFields.Field04") {
    var custom01 = los.GetField("Custom.Field01");
    var custom02 = los.GetField("Custom.Field02");
    var custom03 = los.GetField("Custom.Field03");
    var custom04 = los.GetField("Custom.Field04");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"field01\"\":\"\"{{custom01}}\"\" ,\"\"field02\"\":\"\"{{custom02}}\"\",\"\"field03\"\":\"\"{{custom03}}\"\" ,\"\"field04\"\":\"\"{{custom04}}\"\" , \"\"fileDataId\"\":\"{{fileDataId}}\"  }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/custom\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{custom01}}", custom01);
    dataRaw = dataRaw.replace("{{custom02}}", custom02);
    dataRaw = dataRaw.replace("{{custom03}}", custom03);
    dataRaw = dataRaw.replace("{{custom04}}", custom04);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);
    
    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    los.Application.ShowMessageBox("arguments " + arguments);
    System.Diagnostics.Process.Start("curl.exe", arguments);
};






