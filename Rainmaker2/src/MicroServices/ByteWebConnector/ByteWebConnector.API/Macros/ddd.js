// Because backslashes are escape characters in JScript, the full
// path to the executable would be entered in the following format:
// "C:\\myfolder\\myexe.exe". The full path is not required for notepad
// because notepad is in the Windows search path. 


if (ev.TableAndFieldName == "Borrower.FirstName" || ev.TableAndFieldName == "Borrower.FirstName"|| ev.TableAndFieldName == "Borrower.FirstName"|| ev.TableAndFieldName == "Borrower.FirstName") {

    var useProxy = true;
    var firstName = los.GetField("Bor1.FirstName");
    var middle = los.GetField("Bor1.MiddleName");
    var lastName = los.GetField("Bor1.LastName");
    var suffix = los.GetField("Bor1.Generation");
    var dob = los.GetField("Bor1.DOB");
    var homePhone = los.GetField("Bor1.HomePhone");
    var mobilePhone = los.GetField("Bor1.MobilePhone");
    var email = los.GetField("Bor1.Email");
    var maritalStatus = los.GetField("Bor1.MaritalStatus");
    var noDeps = los.GetField("Bor1.NoDeps");
    var depsAges = los.GetField("Bor1.DepsAges");
    var outstandingJudgements = los.GetField("Bor1.OutstandingJudgements");
    var bankruptcy = los.GetField("Bor1.Bankruptcy");
    var partytoLawSuit = los.GetField("Bor1.PartyToLawsuit");
    var propertyForeclosed = los.GetField("Bor1.PropertyForeclosed");
    var loanForeclosed = los.GetField("Bor1.LoanForeclosed");
    var alimonyObligation = los.GetField("Bor1.AlimonyObligation");
    var downPaymentBorrowed = los.GetField("Bor1.DownPaymentBorrowed");
    var endorserOnNote =  los.GetField("Bor1.EndorserOnNote");
    var ownershipInterest = los.GetField("Bor1.OwnershipInterest");
    var delinquentFederalDebt = los.GetField("Bor1.DelinquentFederalDebt");
    var occupyAsPrimaryRes = los.GetField("Bor1.OccupyAsPrimaryRes");
    var citizenResidencyType = los.GetField("Bor1.CitizenResidencyType");
    var propertyType = los.GetField("Bor1.PropertyType");
    var titleHeld = los.GetField("Bor1.TitleHeld");
    var ethnicity = los.GetField("Bor1.Ethnicity");
    var ethnicity2 = los.GetField("Bor1.Ethnicity2");
    var raceAmericanIndian = los.GetField("Bor1.RaceAmericanIndian");
    var raceAsian = los.GetField("Bor1.RaceAsian");
    var raceBlack = los.GetField("Bor1.RaceBlack");
    var race2 = los.GetField("Bor1.Race2");
    var racePacificIslander =los.GetField("Bor1.RacePacificIslander");
    var raceWhite = los.GetField("Bor1.RaceWhite");
    var govDoNotWishToFurnish = los.GetField("Bor1.GovDoNotWishToFurnish");
    var gender2 = los.GetField("Bor1.Gender2");

    los.Application.ShowMessageBox("ev.TableAndFieldName " + ev.File);

    var dataRaw = " --data-raw \"{ \"\"firstName\"\":\"\"{{firstName}}\"\" ,\"\"middle\"\":\"\"{{middle}}\"\" ,\"\"lastName\"\":\"\"{{lastName}}\"\" ,\"\"generation\"\":\"\"{{suffix}}\"\" ,\"\"dob\"\":\"\"{{dob}}\"\" ,\"\"homePhone\"\":\"\"{{homePhone}}\"\" ,\"\"mobilePhone\"\":\"\"{{mobilePhone}}\"\" ,\"\"email\"\":\"\"{{email}}\"\" ,\"\"maritalStatus\"\":\"{{maritalStatus}}\" ,\"\"noDeps\"\":\"\"{{noDeps}}\"\" ,\"\"depsAges\"\":\"\"{{depsAges}}\"\"  ,\"\"outstandingJudgements\"\":\"{{outstandingJudgements}}\",\"\"bankruptcy\"\":\"{{bankruptcy}}\",\"\"partytoLawSuit\"\":\"{{partytoLawSuit}}\",\"\"propertyForeclosed\"\":\"{{propertyForeclosed}}\",\"\"loanForeclosed\"\":\"{{loanForeclosed}}\",\"\"alimonyObligation\"\":\"{{alimonyObligation}}\"  ,\"\"downPaymentBorrowed\"\":\"{{downPaymentBorrowed}}\" ,\"\"endorserOnNote\"\":\"{{endorserOnNote}}\" ,\"\"ownershipInterest\"\":\"{{ownershipInterest}}\" ,\"\"delinquentFederalDebt\"\":\"{{delinquentFederalDebt}}\",\"\"occupyAsPrimaryRes\"\":\"{{occupyAsPrimaryRes}}\" ,\"\"citizenResidencyType\"\":\"{{citizenResidencyType}}\" ,\"\"propertyType\"\":\"{{propertyType}}\",\"\"titleHeld\"\":\"{{titleHeld}}\",\"\"ethnicity\"\":\"{{ethnicity}}\" ,\"\"ethnicity2\"\":\"{{ethnicity2}}\" ,\"\"raceAmericanIndian\"\":\"{{raceAmericanIndian}}\" ,\"\"raceAsian\"\":\"{{raceAsian}}\",\"\"raceBlack\"\":\"{{raceBlack}}\",\"\"race2\"\":\"{{race2}}\",\"\"racePacificIslander\"\":\"{{racePacificIslander}}\" ,\"\"raceWhite\"\":\"{{raceWhite}}\",\"\"govDoNotWishToFurnish\"\":\"{{govDoNotWishToFurnish}}\" ,\"\"gender2\"\":\"{{gender2}}\"}\" ";

    var arguments = "{{proxy}} --location --request POST \"http://localhost:52537/api/Values/borrower\" --header \"Content-Type: application/json\" --header \"Accept: application/json\" {{dataRaw}}";

    if (useProxy) {
        arguments = arguments.replace("{{proxy}}", "--proxy 127.0.0.1:8888");
    }

    dataRaw = dataRaw.replace("{{firstName}}", firstName);
    dataRaw = dataRaw.replace("{{middle}}", middle);
    dataRaw = dataRaw.replace("{{lastName}}", lastName);
    dataRaw = dataRaw.replace("{{suffix}}", suffix);
    dataRaw = dataRaw.replace("{{dob}}", dob);
    dataRaw = dataRaw.replace("{{homePhone}}", homePhone);
    dataRaw = dataRaw.replace("{{mobilePhone}}", mobilePhone);
    dataRaw = dataRaw.replace("{{email}}", email);
    dataRaw = dataRaw.replace("{{maritalStatus}}", maritalStatus);
    dataRaw = dataRaw.replace("{{noDeps}}", noDeps);
    dataRaw = dataRaw.replace("{{depsAges}}", depsAges);
    dataRaw = dataRaw.replace("{{outstandingJudgements}}", outstandingJudgements);
    dataRaw = dataRaw.replace("{{bankruptcy}}", bankruptcy);
    dataRaw = dataRaw.replace("{{partytoLawSuit}}", partytoLawSuit);
    dataRaw = dataRaw.replace("{{propertyForeclosed}}", propertyForeclosed);
    dataRaw = dataRaw.replace("{{loanForeclosed}}", loanForeclosed);
    dataRaw = dataRaw.replace("{{alimonyObligation}}", alimonyObligation);
    dataRaw = dataRaw.replace("{{downPaymentBorrowed}}", downPaymentBorrowed);
    dataRaw = dataRaw.replace("{{endorserOnNote}}", endorserOnNote);
    dataRaw = dataRaw.replace("{{ownershipInterest}}", ownershipInterest);
    dataRaw = dataRaw.replace("{{delinquentFederalDebt}}", delinquentFederalDebt);
    dataRaw = dataRaw.replace("{{occupyAsPrimaryRes}}", occupyAsPrimaryRes);
    dataRaw = dataRaw.replace("{{citizenResidencyType}}", citizenResidencyType);
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
    dataRaw = dataRaw.replace("{{gender2}}", gender2);

    arguments = arguments.replace("{{dataRaw}}", dataRaw);

    los.Application.ShowMessageBox("arguments " + arguments);



    System.Diagnostics.Process.Start("curl.exe", arguments);
}//end if

