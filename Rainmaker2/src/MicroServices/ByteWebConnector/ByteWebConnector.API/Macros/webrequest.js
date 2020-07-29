if (ev.TableAndFieldName == "Application.ApplicationMethod" || ev.TableAndFieldName=="Application.LifeInsCashValue") {
    var lifeInsCashValue = los.GetField("1003App1.LifeInsCashValue");
    var ApplicationMethod = los.GetField("1003App1.ApplicationMethod");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");
    var applicationID = ev.Table.GetFieldValue("ApplicationID");
    var borrowerID = ev.Table.GetFieldValue("BorrowerID");
    var coBorrowerID = ev.Table.GetFieldValue("CoBorrowerID");
    
    
    
    var webRequest : System.Net.WebRequest = System.Net.WebRequest.Create("http://localhost:52537/api/Values/application")
    
    var httpRequest : System.Net.HttpWebRequest = System.Net.HttpWebRequest(webRequest);

​

  var stringData = '{"operationName":null,"variables":{},"query":"{sites {items { id } }}"}'; //place body here

  var data = System.Text.Encoding.ASCII.GetBytes(stringData); // or UTF8

​

  httpRequest.Method = "POST";

  httpRequest.ContentType = "application/json"; //place MIME type here

  httpRequest.ContentLength = data.Length;

​

  var newStream = httpRequest.GetRequestStream();

  newStream.Write(data, 0, data.Length);

  newStream.Close();

​

  var response : System.Net.HttpWebResponse = System.Net.HttpWebResponse(webRequest.GetResponse());

  var status = response.StatusDescription;

​

  var dataStream : System.IO.Stream = response.GetResponseStream();

  var reader : System.IO.StreamReader = new System.IO.StreamReader(dataStream);

  var responseFromServer : String = reader.ReadToEnd();
    
    
    
    
    
    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"lifeInsCashValue\"\":\"{{lifeInsCashValue}}\" ,\"\"ApplicationMethod\"\":\"{{ApplicationMethod}}\", \"\"fileDataId\"\":\"{{fileDataId}}\", \"\"applicationID\"\":\"{{applicationID}}\", \"\"borrowerID\"\":\"{{borrowerID}}\", \"\"coBorrowerID\"\":\"{{coBorrowerID}}\" }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/application\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{lifeInsCashValue}}", lifeInsCashValue);
    dataRaw = dataRaw.replace("{{ApplicationMethod}}", ApplicationMethod);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);
    dataRaw = dataRaw.replace("{{applicationID}}", applicationID);
    dataRaw = dataRaw.replace("{{borrowerID}}", borrowerID);
    dataRaw = dataRaw.replace("{{coBorrowerID}}", coBorrowerID);
    
    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    los.Application.ShowMessageBox("arguments " + arguments);
    System.Diagnostics.Process.Start("curl.exe", arguments);
};



