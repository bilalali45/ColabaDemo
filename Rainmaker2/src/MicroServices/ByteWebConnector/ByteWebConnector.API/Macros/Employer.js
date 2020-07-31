if (ev.TableAndFieldName == "Employer.City" || ev.TableAndFieldName == "Employer.Zip" || ev.TableAndFieldName == "Employer.State"
    || ev.TableAndFieldName == "Employer.Street" || ev.TableAndFieldName == "Employer.MoIncome" || ev.TableAndFieldName == "Employer.Name"
    || ev.TableAndFieldName == "Employer.Phone" || ev.TableAndFieldName == "Employer.Position" || ev.TableAndFieldName == "Employer.DateFrom"
    || ev.TableAndFieldName == "Employer.YearsOnJob" || ev.TableAndFieldName == "Employer.SelfEmp" || ev.TableAndFieldName == "Employer.OwnershipInterest" || ev.TableAndFieldName == "Employer.Status") {

    var city = ev.Table.GetFieldValue("City");
    var zip = ev.Table.GetFieldValue("Zip");
    var state = ev.Table.GetFieldValue("State");
    var street = ev.Table.GetFieldValue("Street");
    var moIncome = ev.Table.GetFieldValue("MoIncome");
    var name = ev.Table.GetFieldValue("Name");
    var phone = ev.Table.GetFieldValue("Phone");
    var position = ev.Table.GetFieldValue("Position");
    var dateFrom = ev.Table.GetFieldValue("DateFrom");
    var yearsOnJob = ev.Table.GetFieldValue("YearsOnJob");
    var selfEmp = ev.Table.GetFieldValue("SelfEmp");
    var ownershipInterest = ev.Table.GetFieldValue("OwnershipInterest");
    var status = ev.Table.GetFieldValue("Status");
    var borrowerId = ev.Table.GetFieldValue("BorrowerID");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");

    var useProxy = true;



    var dataRaw = " --data-raw \"{ \"\"city\"\":\"\"{{city}}\"\" ,\"\"zip\"\":\"\"{{zip}}\"\" , \"\"state\"\":\"\"{{state}}\"\" , \"\"street\"\":\"\"{{street}}\"\" , \"\"moIncome\"\":{{moIncome}} , \"\"name\"\":\"\"{{name}}\"\" , \"\"phone\"\":\"\"{{phone}}\"\" , \"\"position\"\":\"\"{{position}}\"\" , \"\"dateFrom\"\":\"\"{{dateFrom}}\"\" , \"\"yearsOnJob\"\":{{yearsOnJob}} , \"\"selfEmp\"\":{{selfEmp}} , \"\"ownershipInterest\"\":\"\"{{ownershipInterest}}\"\" , \"\"status\"\":\"\"{{status}}\"\" ,\"\"borrowerID\"\":{{borrowerId}} , \"\"fileDataId\"\":{{fileDataId}}     }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:5050/api/ByteWebConnector/BorrowerEmployer/update\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }
    dataRaw = dataRaw.replace("{{city}}", city);
    dataRaw = dataRaw.replace("{{zip}}", zip);
    dataRaw = dataRaw.replace("{{state}}", state);
    dataRaw = dataRaw.replace("{{street}}", street);
    dataRaw = dataRaw.replace("{{moIncome}}", (moIncome) ? moIncome : null);
    dataRaw = dataRaw.replace("{{name}}", name);
    dataRaw = dataRaw.replace("{{phone}}", phone);
    dataRaw = dataRaw.replace("{{position}}", position);
    dataRaw = dataRaw.replace("{{dateFrom}}", dateFrom);
    dataRaw = dataRaw.replace("{{yearsOnJob}}", (yearsOnJob) ? yearsOnJob : null);
    dataRaw = dataRaw.replace("{{selfEmp}}", selfEmp);
    dataRaw = dataRaw.replace("{{ownershipInterest}}", ownershipInterest);
    dataRaw = dataRaw.replace("{{status}}", status);
    dataRaw = dataRaw.replace("{{borrowerId}}", borrowerId);
    dataRaw = dataRaw.replace("{{fileDataId}}", fileDataId);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();
};
