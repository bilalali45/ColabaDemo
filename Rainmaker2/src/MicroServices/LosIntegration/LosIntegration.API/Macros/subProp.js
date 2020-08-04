// Because backslashes are escape characters in JScript, the full
// path to the executable would be entered in the following format:
// "C:\\myfolder\\myexe.exe". The full path is not required for notepad
// because notepad is in the Windows search path. 
if (ev.TableAndFieldName == "SubProp.City" || ev.TableAndFieldName == "SubProp.State" 
       || ev.TableAndFieldName == "SubProp.County" || ev.TableAndFieldName == "SubProp.Street"|| ev.TableAndFieldName == "SubProp.Zip"
       || ev.TableAndFieldName == "SubProp.NoUnits" || ev.TableAndFieldName == "SubProp.CPresValLot" || ev.TableAndFieldName == "SubProp.CAmtExLiens" 
|| ev.TableAndFieldName == "SubProp.RAmtExLiens" || ev.TableAndFieldName == "SubProp.ROrigCost"|| ev.TableAndFieldName == "SubProp.RYearLotAcq") 
{
    var city = los.GetField("SubProp.City");
    var state = los.GetField("SubProp.State");
    var county = los.GetField("SubProp.County");
    var street = los.GetField("SubProp.Street");
    var zip = los.GetField("SubProp.Zip");
    var noUnit = los.GetField("SubProp.NoUnits");
    var cpressValLot = los.GetField("SubProp.CPresValLot");
    var camExtLiens = los.GetField("SubProp.CAmtExLiens");
    var ramExtLiens = los.GetField("SubProp.RAmtExLiens");
    var rorigCost = los.GetField("SubProp.ROrigCost");
    var rYearLotAcq = los.GetField("SubProp.RYearLotAcq");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"city\"\":\"\"{{city}}\"\" ,\"\"state\"\":\"\"{{state}}\"\" ,\"\"county\"\":\"\"{{county}}\"\" ,\"\"street\"\":\"\"{{street}}\"\" ,\"\"zip\"\":\"\"{{zip}}\"\" ,\"\"noUnit\"\":{{noUnit}} ,\"\"cpressValLot\"\":{{cpressValLot}} ,\"\"camExtLiens\"\":{{camExtLiens}} ,\"\"ramExtLiens\"\":{{ramExtLiens}} ,\"\"rorigCost\"\":{{rorigCost}} ,\"\"rYearLotAcq\"\":{{rYearLotAcq}} , \"\"fileDataId\"\":{{fileDataId}} } ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:5050/api/ByteWebConnector/SubProperty/update\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{city}}", city);
    dataRaw = dataRaw.replace("{{state}}", state);
    dataRaw = dataRaw.replace("{{county}}", county);
    dataRaw = dataRaw.replace("{{street}}", street);
    dataRaw = dataRaw.replace("{{zip}}", zip);
    dataRaw = dataRaw.replace("{{noUnit}}", (noUnit) ? noUnit : null);
    dataRaw = dataRaw.replace("{{cpressValLot}}", (cpressValLot) ? cpressValLot : null);
    dataRaw = dataRaw.replace("{{camExtLiens}}", (camExtLiens) ? camExtLiens : null);
    dataRaw = dataRaw.replace("{{ramExtLiens}}", (ramExtLiens) ? ramExtLiens : null);
    dataRaw = dataRaw.replace("{{rorigCost}}", (rorigCost) ? rorigCost : null);
    dataRaw = dataRaw.replace("{{rYearLotAcq}}", (rYearLotAcq) ? rYearLotAcq : null);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);
    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
};

