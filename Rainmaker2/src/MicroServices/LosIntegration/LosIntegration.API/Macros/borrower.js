// Because backslashes are escape characters in JScript, the full
// path to the executable would be entered in the following format:
// "C:\\myfolder\\myexe.exe". The full path is not required for notepad
// because notepad is in the Windows search path. 


if (ev.TableAndFieldName == "Borrower.FirstName"
    || ev.TableAndFieldName == "Borrower.MiddleName" || ev.TableAndFieldName == "Borrower.LastName" || ev.TableAndFieldName == "Borrower.Generation"
    || ev.TableAndFieldName == "Borrower.DOB" || ev.TableAndFieldName == "Borrower.HomePhone" || ev.TableAndFieldName == "Borrower.MobilePhone"
    || ev.TableAndFieldName == "Borrower.Email" || ev.TableAndFieldName == "Borrower.MaritalStatus" || ev.TableAndFieldName == "Borrower.NoDeps"
    || ev.TableAndFieldName == "Borrower.DepsAges" || ev.TableAndFieldName == "Borrower.OutstandingJudgements" || ev.TableAndFieldName == "Borrower.Bankruptcy"
    || ev.TableAndFieldName == "Borrower.PartyToLawsuit"
    || ev.TableAndFieldName == "Borrower.PropertyForeclosed" || ev.TableAndFieldName == "Borrower.LoanForeclosed" || ev.TableAndFieldName == "Borrower.AlimonyObligation"
    || ev.TableAndFieldName == "Borrower.DownPaymentBorrowed" || ev.TableAndFieldName == "Borrower.EndorserOnNote" || ev.TableAndFieldName == "Borrower.OwnershipInterest"
    || ev.TableAndFieldName == "Borrower.DelinquentFederalDebt" || ev.TableAndFieldName == "Borrower.OccupyAsPrimaryRes" || ev.TableAndFieldName == "Borrower.CitizenResidencyType"
    || ev.TableAndFieldName == "Borrower.PropertyType" || ev.TableAndFieldName == "Borrower.TitleHeld" || ev.TableAndFieldName == "Borrower.Ethnicity"
    || ev.TableAndFieldName == "Borrower.Ethnicity2" || ev.TableAndFieldName == "Borrower.Race2" || ev.TableAndFieldName == "Borrower.RacePacificIslander"
    || ev.TableAndFieldName == "Borrower.RaceWhite" || ev.TableAndFieldName == "Borrower.RaceAmericanIndian" || ev.TableAndFieldName == "Borrower.RaceAsian"
    || ev.TableAndFieldName == "Borrower.RaceBlack" || ev.TableAndFieldName == "Borrower.GovDoNotWishToFurnish" || ev.TableAndFieldName == "Borrower.Gender2"
) {
    var oldFirstName = "";
    var oldEmail = "";
    if (ev.TableAndFieldName == "Borrower.FirstName") {
        oldFirstName = ev.OldValue;
    }
    if (ev.TableAndFieldName == "Borrower.Email") {
        oldEmail = ev.OldValue;
    }
    var useProxy = true;
    var firstName = ev.Table.GetFieldValue("FirstName");
    var middleName = ev.Table.GetFieldValue("MiddleName");
    var lastName = ev.Table.GetFieldValue("LastName");
    var dob = ev.Table.GetFieldValue("DOB");
    var suffix = ev.Table.GetFieldValue("Generation");
    var homePhone = ev.Table.GetFieldValue("HomePhone");
    var mobilePhone = ev.Table.GetFieldValue("MobilePhone");
    var email = ev.Table.GetFieldValue("Email");
    var maritalStatus = ev.Table.GetFieldValue("MaritalStatus");
    var noDeps = ev.Table.GetFieldValue("NoDeps");
    var depsAges = ev.Table.GetFieldValue("DepsAges");
    var outstandingJudgements = ev.Table.GetFieldValue("OutstandingJudgements");
    var bankruptcy = ev.Table.GetFieldValue("Bankruptcy");
    var partyToLawsuit = ev.Table.GetFieldValue("PartyToLawsuit");
    var propertyForeclosed = ev.Table.GetFieldValue("PropertyForeclosed");
    var loanForeclosed = ev.Table.GetFieldValue("LoanForeclosed");
    var alimonyObligation = ev.Table.GetFieldValue("AlimonyObligation");
    var downPaymentBorrowed = ev.Table.GetFieldValue("DownPaymentBorrowed");
    var endorserOnNote = ev.Table.GetFieldValue("EndorserOnNote");
    var ownershipInterest = ev.Table.GetFieldValue("OwnershipInterest");
    var delinquentFederalDebt = ev.Table.GetFieldValue("DelinquentFederalDebt");
    var occupyAsPrimaryRes = ev.Table.GetFieldValue("OccupyAsPrimaryRes");
    var citizenResidencyType = ev.Table.GetFieldValue("CitizenResidencyType");
    var propertyType = ev.Table.GetFieldValue("PropertyType");
    var titleHeld = ev.Table.GetFieldValue("TitleHeld");
    var ethnicity = ev.Table.GetFieldValue("Ethnicity");
    var ethnicity2 = ev.Table.GetFieldValue("Ethnicity2");
    var raceAmericanIndian = ev.Table.GetFieldValue("Race2AmericanIndian");
    var raceAsian = ev.Table.GetFieldValue("Race2Asian");
    var raceBlack = ev.Table.GetFieldValue("Race2Black");
    var race2 = ev.Table.GetFieldValue("Race2");
    var racePacificIslander = ev.Table.GetFieldValue("Race2PacificIslander");
    var raceWhite = ev.Table.GetFieldValue("Race2White");
    var govDoNotWishToFurnish = ev.Table.GetFieldValue("Race2IDoNotWishToFurnish");
    var gender2 = ev.Table.GetFieldValue("Gender2");
    var borId = ev.Table.GetFieldValue("BorrowerID");
    var fileDataId = ev.Table.GetFieldValue("FileDataID");

    var dataRaw = " --data-raw \"{ \"\"firstName\"\":\"\"{{firstName}}\"\" ,\"\"oldFirstName\"\":\"\"{{oldFirstName}}\"\" ,\"\"middleName\"\":\"\"{{middleName}}\"\" ,\"\"lastName\"\":\"\"{{lastName}}\"\" ,\"\"generation\"\":\"\"{{suffix}}\"\" ,\"\"dob\"\":{{dob}} ,\"\"homePhone\"\":\"\"{{homePhone}}\"\" ,\"\"mobilePhone\"\":\"\"{{mobilePhone}}\"\" ,\"\"email\"\":\"\"{{email}}\"\",\"\"oldEmail\"\":\"\"{{oldEmail}}\"\" ,\"\"maritalStatus\"\":\"\"{{maritalStatus}}\"\" ,\"\"noDeps\"\":{{noDeps}} ,\"\"depsAges\"\":\"\"{{depsAges}}\"\"  ,\"\"outstandingJudgements\"\":\"\"{{outstandingJudgements}}\"\",\"\"bankruptcy\"\":\"\"{{bankruptcy}}\"\",\"\"partyToLawsuit\"\":\"\"{{partyToLawsuit}}\"\",\"\"propertyForeclosed\"\":\"\"{{propertyForeclosed}}\"\",\"\"loanForeclosed\"\":\"\"{{loanForeclosed}}\"\",\"\"alimonyObligation\"\":\"\"{{alimonyObligation}}\"\"  ,\"\"downPaymentBorrowed\"\":\"\"{{downPaymentBorrowed}}\"\" ,\"\"endorserOnNote\"\":\"\"{{endorserOnNote}}\"\" ,\"\"ownershipInterest\"\":\"\"{{ownershipInterest}}\"\" ,\"\"delinquentFederalDebt\"\":\"\"{{delinquentFederalDebt}}\"\",\"\"occupyAsPrimaryRes\"\":\"\"{{occupyAsPrimaryRes}}\"\" ,\"\"citizenResidencyType\"\":\"\"{{citizenResidencyType}}\"\" ,\"\"propertyType\"\":\"\"{{propertyType}}\"\",\"\"titleHeld\"\":\"\"{{titleHeld}}\"\",\"\"ethnicity\"\":\"\"{{ethnicity}}\"\" ,\"\"ethnicity2\"\":\"\"{{ethnicity2}}\"\" ,\"\"raceAmericanIndian\"\":\"{{raceAmericanIndian}}\" ,\"\"raceAsian\"\":\"{{raceAsian}}\",\"\"raceBlack\"\":\"{{raceBlack}}\",\"\"race2\"\":\"\"{{race2}}\"\",\"\"racePacificIslander\"\":\"{{racePacificIslander}}\" ,\"\"raceWhite\"\":\"{{raceWhite}}\",\"\"govDoNotWishToFurnish\"\":\"{{govDoNotWishToFurnish}}\" ,\"\"gender2\"\":\"\"{{gender2}}\"\", \"\"borrowerID\"\":{{borId}} , \"\"fileDataId\"\":{{fileDataId}}  }\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:5050/api/ByteWebConnector/Borrower/update\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }

    dataRaw = dataRaw.replace("{{oldFirstName}}", oldFirstName);
    dataRaw = dataRaw.replace("{{firstName}}", firstName);
    dataRaw = dataRaw.replace("{{middleName}}", middleName);
    dataRaw = dataRaw.replace("{{lastName}}", lastName);
    dataRaw = dataRaw.replace("{{suffix}}", suffix);
    dataRaw = dataRaw.replace("{{dob}}", (dob) ? dob : null);
    dataRaw = dataRaw.replace("{{homePhone}}", homePhone);
    dataRaw = dataRaw.replace("{{mobilePhone}}", mobilePhone);
    dataRaw = dataRaw.replace("{{email}}", email);
    dataRaw = dataRaw.replace("{{oldEmail}}", oldEmail);
    dataRaw = dataRaw.replace("{{maritalStatus}}", (maritalStatus) == 0 ? null : maritalStatus);
    dataRaw = dataRaw.replace("{{noDeps}}", (noDeps) ? noDeps : null);
    dataRaw = dataRaw.replace("{{depsAges}}", depsAges);
    dataRaw = dataRaw.replace("{{outstandingJudgements}}", outstandingJudgements);
    dataRaw = dataRaw.replace("{{bankruptcy}}", bankruptcy);
    dataRaw = dataRaw.replace("{{partyToLawsuit}}", partyToLawsuit);
    dataRaw = dataRaw.replace("{{propertyForeclosed}}", propertyForeclosed);
    dataRaw = dataRaw.replace("{{loanForeclosed}}", loanForeclosed);
    dataRaw = dataRaw.replace("{{alimonyObligation}}", alimonyObligation);
    dataRaw = dataRaw.replace("{{downPaymentBorrowed}}", downPaymentBorrowed);
    dataRaw = dataRaw.replace("{{endorserOnNote}}", endorserOnNote);
    dataRaw = dataRaw.replace("{{ownershipInterest}}", ownershipInterest);
    dataRaw = dataRaw.replace("{{delinquentFederalDebt}}", delinquentFederalDebt);
    dataRaw = dataRaw.replace("{{occupyAsPrimaryRes}}", occupyAsPrimaryRes);
    dataRaw = dataRaw.replace("{{citizenResidencyType}}", (citizenResidencyType) == 0 ? null : citizenResidencyType);
    dataRaw = dataRaw.replace("{{propertyType}}", propertyType);
    dataRaw = dataRaw.replace("{{titleHeld}}", titleHeld);
    dataRaw = dataRaw.replace("{{ethnicity}}", ethnicity);
    dataRaw = dataRaw.replace("{{ethnicity2}}", ethnicity2);
    dataRaw = dataRaw.replace("{{raceAmericanIndian}}", raceAmericanIndian);
    dataRaw = dataRaw.replace("{{raceAsian}}", raceAsian);
    dataRaw = dataRaw.replace("{{raceBlack}}", raceBlack);
    dataRaw = dataRaw.replace("{{race2}}", race2);
    dataRaw = dataRaw.replace("{{racePacificIslander}}", racePacificIslander);
    dataRaw = dataRaw.replace("{{raceWhite}}", raceWhite);
    dataRaw = dataRaw.replace("{{govDoNotWishToFurnish}}", govDoNotWishToFurnish);
    dataRaw = dataRaw.replace("{{gender2}}", (gender2) == 0 ? null : gender2);
    dataRaw = dataRaw.replace("{{borId}}", (borId) ? borId : null);
    dataRaw = dataRaw.replace("{{fileDataId}}", (fileDataId) ? fileDataId : null);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);


    var process = new System.Diagnostics.Process();
    process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
    process.StartInfo.CreateNoWindow = true;
    process.StartInfo.FileName = "curl.exe";
    process.StartInfo.Arguments = arguments;
    process.Start();

}//end if

