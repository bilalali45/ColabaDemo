
if (ev.TableAndFieldName == "Asset.Name" || ev.TableAndFieldName == "Asset.AccountType1"  || ev.TableAndFieldName == "Asset.AccountNo1"|| ev.TableAndFieldName == "Asset.AccountBalance1"){
    var name = ev.Table.GetFieldValue("Name");
    var accountType1 = ev.Table.GetField("AccountType1");
    var accountNo1 = ev.Table.GetFieldValue("AccountNo1");
    var accountBalance1 = ev.Table.GetFieldValue("AccountBalance1");
    var borId = ev.Table.GetField("BorrowerID");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
 
 var useProxy = true;
 
 
 
     var dataRaw = " --data-raw \"{ \"\"name\"\":\"\"{{name}}\"\" ,\"\"accountType1\"\":\"{{accountType1}}\" , \"\"accountNo1\"\":\"\"{{accountNo1}}\"\" , \"\"accountBalance1\"\":\"\"{{accountBalance1}}\"\" , \"\"borrowerID\"\":\"{{borId}}\" , \"\"fileDataId\"\":\"{{fileDataId}}\"  }\" ";
 
     var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/asset\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";
 
     if (useProxy) {
         arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
     }
     dataRaw = dataRaw.replace("{{name}}", name);
     dataRaw = dataRaw.replace("{{accountType1}}", accountType1);
     dataRaw = dataRaw.replace("{{accountNo1}}", accountNo1);
     dataRaw = dataRaw.replace("{{accountBalance1}}", accountBalance1);
     dataRaw = dataRaw.replace("{{borId}}", borId);
     dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);
 
     arguments = arguments.replace("{{dataRaw}}", dataRaw);
     var process = new System.Diagnostics.Process();
     process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
     process.StartInfo.CreateNoWindow = true;
     process.StartInfo.FileName = "curl.exe";
     process.StartInfo.Arguments = arguments;
     process.Start();
 }