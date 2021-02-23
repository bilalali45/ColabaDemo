using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using ByteWebConnector.SDK.Models;
using ByteWebConnector.SDK.Models.Rainmaker;

namespace ByteWebConnector.SDK.Mismo
{
    class MismoRelationShipModel
    {
        public string From { get; set; }
        public string To { get; set; }
    }

    enum OtherEmploymentIncomes
    {
        Overtime = 1,
        Bonus = 2,
        Commissions = 3
    }

    public enum EscrowEntityTypeEnum
    {
        [Description("Property Taxes")]
        PropertyTaxes = 1,
        [Description("Home Owners Insurance")]
        HomeOwnersInsurance = 2,
        [Description("School Tax")]
        SchoolTax = 3,
        [Description("Flood Insurance")]
        FloodInsurance = 4
    }

    public enum AccountTypeEnum
    {
        NotAssigned,
        Savings,
        Checking,
        CashDepositOnSalesContract,
        GiftNotDeposited,
        CertificateOfDeposit,
        MoneyMarketFund,
        MutualFunds,
        Stocks,
        Bonds,
        SecuredBorrowedFundsNotDeposited,
        BridgeLoanNotDeposited,
        RetirementFunds,
        NetWorthOfBusinessOwned,
        TrustFunds,
        OtherNonLiquidAsset,
        OtherLiquidAsset,
        NetProceedsFromSaleOfRealEstate,
        NetEquity,
        CashOnHand,
        GiftOfEquity,
        IndividualDevelopmentAccount,
        LifeInsuranceCashValue,
        ProceedsFromSaleOfNonRealEstateAsset,
        SecuredBorrowedFunds,
        StockOptions,
        UnsecuredBorrowedFunds
    }

    public enum PropertyUnitType : byte
    {
        TwoUnitBuilding = 4,
        ThreeUnitBuilding = 5,
        FourUnitBuilding = 6,
    }

    public class MismoConverter34 : IMismoConverter
    {
        private const string ASSET_ARC_VALUE = "urn:fdc:mismo.org:2009:residential/ASSET_IsAssociatedWith_ROLE";
        //Dictionary<string, string> relationships = new Dictionary<string, string>();
        List<MismoRelationShipModel> relationships = new List<MismoRelationShipModel>();
        private List<int?> _liabilitiesToSkip { get; set; }

        private MISMO34FORMAT _mismo = null;

        private MISMO34FORMAT mismo
        {
            get
            {
                if (_mismo == null)
                {
                    _mismo = new MISMO34FORMAT()
                    {
                        ABOUT_VERSIONS = new ABOUT_VERSIONS()
                        {
                            ABOUT_VERSION = new List<ABOUT_VERSION>()
                              {
                                  new ABOUT_VERSION()
                                  {
                                      SequenceNumber = 1
                                  }
                              }
                        },
                        DEAL_SETS = new DEAL_SETS()
                        {
                            DEAL_SET = new DEAL_SET()
                            {
                                DEALS = new DEALS()
                                {
                                    DEAL = new List<DEAL>()
                                      {
                                          new DEAL()
                                          {
                                              COLLATERALS = new COLLATERALS()
                                                {
                                                    COLLATERAL = new List<COLLATERAL>()
                                                },
                                              LOANS = new LOANS()
                                          }
                                      }
                                }
                            }
                        }
                    };
                }
                return _mismo;
            }
        }

        private ABOUT_VERSION _aboutVersion = null;

        private ABOUT_VERSION aboutVersion
        {
            get
            {
                if (_aboutVersion == null)
                {
                    _aboutVersion = this.mismo.ABOUT_VERSIONS.ABOUT_VERSION
                                        .Single();
                }
                return _aboutVersion;
            }
        }

        private DEAL _dealToAdd;

        private DEAL dealToAdd
        {
            get
            {
                if (_dealToAdd == null)
                {
                    _dealToAdd = this.mismo.DEAL_SETS.DEAL_SET.DEALS.DEAL.FirstOrDefault();
                }
                return this._dealToAdd;
            }
        }

        public string ConvertToMismo(LoanApplication loanApplication)
        {
            string xml = null;
            try
            {
                //mismo.ABOUT_VERSIONS = new ABOUT_VERSIONS();
                //mismo.ABOUT_VERSIONS.ABOUT_VERSION = new List<ABOUT_VERSION>();
                //mismo.ABOUT_VERSIONS.ABOUT_VERSION.Add(new ABOUT_VERSION()
                //{
                //    SequenceNumber = 1,
                //    CreatedDatetime = loanApplication.CreatedOnUtc.HasValue ? loanApplication.CreatedOnUtc.Value : DateTime.UtcNow
                //});
                this.aboutVersion.CreatedDatetime = loanApplication.CreatedOnUtc.HasValue ? loanApplication.CreatedOnUtc.Value : DateTime.UtcNow;
                //mismo.DEAL_SETS = new DEAL_SETS();
                //mismo.DEAL_SETS.DEAL_SET = new DEAL_SET();
                //mismo.DEAL_SETS.DEAL_SET.DEALS = new DEALS();
                //mismo.DEAL_SETS.DEAL_SET.DEALS.DEAL = new List<DEAL>();

                //DEAL dealToAdd = new DEAL();
                //dealToAdd.SequenceNumber = 1;
                dealToAdd.ASSETS = this.GetAssets(loanApplication);

                //dealToAdd.COLLATERALS = new COLLATERALS();
                //dealToAdd.COLLATERALS.COLLATERAL = new List<COLLATERAL>();
                COLLATERAL collateralToAdd = new COLLATERAL()
                {
                    SequenceNumber = 1,
                    SUBJECT_PROPERTY = this.GetSubjectProperty(loanApplication)
                };

                dealToAdd.COLLATERALS.COLLATERAL.Add(collateralToAdd);
                dealToAdd.EXPENSES = this.GetExpenseLiabilities((loanApplication));
                dealToAdd.LIABILITIES = this.GetLiabilities(loanApplication);
                //dealToAdd.LOANS = new LOANS()
                //{
                //    LOAN = this.GetLoanInfo(loanApplication)
                //};
                dealToAdd.LOANS.LOAN = this.GetLoanInfo(loanApplication);

                dealToAdd.PARTIES = new PARTIES();
                dealToAdd.PARTIES.PARTY = this.GetBorrowers(loanApplication);

                int relationshipIndex = 1;
                dealToAdd.RELATIONSHIPS = new RELATIONSHIPS();
                dealToAdd.RELATIONSHIPS.RELATIONSHIP = new List<RELATIONSHIP>();
                relationships = relationships.OrderBy(r => r.From).ToList();
                foreach (var item in relationships)
                {
                    RELATIONSHIP relationship = new RELATIONSHIP()
                    {
                        SequenceNumber = relationshipIndex++,
                        From = item.From,
                        To = item.To
                    };
                    if (relationship.From.ToUpper().StartsWith("ASSET_"))
                    {
                        if (relationship.To.ToUpper().StartsWith("BORROWER_"))
                        {
                            relationship.Arcrole = ASSET_ARC_VALUE;
                        }
                    }
                    dealToAdd.RELATIONSHIPS.RELATIONSHIP.Add(relationship);
                }


                //mismo.DEAL_SETS.DEAL_SET.DEALS.DEAL.Add(dealToAdd);

                using (var sww = new StringWriter())
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    //http://www.w3.org/1999/xlink
                    ns.Add("xlink", "http://www.w3.org/1999/xlink");
                    ns.Add("xmlns", "http://www.w3.org/2000/xmlns");
                    XmlSerializer serializer = new XmlSerializer(typeof(MISMO34FORMAT));
                    using (var ms2 = new MemoryStream())
                    {
                        using (var sw = new StreamWriter(ms2,
                                                         Encoding.UTF8))
                        {
                            using (var xmlWriter = XmlWriter.Create(sw,
                                                                    new XmlWriterSettings()
                                                                    {
                                                                        Encoding = Encoding.UTF8,
                                                                        OmitXmlDeclaration = true
                                                                    }))
                            {
                                serializer.Serialize(xmlWriter, mismo);
                                var utf8 = ms2.ToArray();
                                xml = Encoding.UTF8.GetString(utf8, 0, utf8.Length);
                            }
                        }
                    }
                }

                XmlWriterSettings settings = new XmlWriterSettings();
                settings.OmitXmlDeclaration = true;
                settings.Encoding = Encoding.UTF8;

                MemoryStream ms = new MemoryStream();
                XmlWriter writer = XmlWriter.Create(ms, settings);
                //writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                //writer.WriteAttributeString("xmlns", "ULAD", null, "http://www.datamodelextension.org/Schema/ULAD");

                XmlSerializerNamespaces names = new XmlSerializerNamespaces();
                names.Add("xlink", "http://www.w3.org/1999/xlink");
                names.Add("ULAD", "http://www.datamodelextension.org/Schema/ULAD");

                XmlSerializer cs = new XmlSerializer(typeof(MISMO34FORMAT));

                cs.Serialize(writer, mismo, names);

                ms.Flush();
                ms.Seek(0, SeekOrigin.Begin);
                StreamReader sr = new StreamReader(ms);
                xml = sr.ReadToEnd();


                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                //<MESSAGE xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:ULAD="http://www.datamodelextension.org/Schema/ULAD" xmlns:DU="http://www.datamodelextension.org/Schema/DU" xsi:schemaLocation="http://www.mismo.org/residential/2009/schemas DU_Wrapper_3.4.0_B324.xsd" MISMOReferenceModelIdentifier="3.4.032420160128">
                doc.DocumentElement.Attributes.Append(doc.CreateAttribute("xmlns:xsi")).Value = "http://www.w3.org/2001/XMLSchema-instance";
                doc.DocumentElement.Attributes.Append(doc.CreateAttribute("xmlns:ULAD")).Value = "http://www.datamodelextension.org/Schema/ULAD";
                doc.DocumentElement.Attributes.Append(doc.CreateAttribute("xmlns:DU")).Value = "http://www.datamodelextension.org/Schema/DU";
                //doc.DocumentElement.Attributes.Append(doc.CreateAttribute("xsi:schemaLocation")).Value = "http://www.mismo.org/residential/2009/schemas DU_Wrapper_3.4.0_B324.xsd";

                using (var stringWriter = new StringWriter())
                using (var xmlTextWriter = XmlWriter.Create(stringWriter, settings))
                {
                    doc.WriteTo(xmlTextWriter);
                    xmlTextWriter.Flush();
                    xml = stringWriter.GetStringBuilder().ToString();
                }

            }
            catch (Exception ex)
            {

            }

            return xml;
        }

        public void SetLiabilitiesToSkip(List<int?> liabilitiesToSkip)
        {
            this._liabilitiesToSkip = liabilitiesToSkip;
        }

        public List<Borrower> SortRainMakerBorrowers(LoanApplication loanApplication)
        {
            var rmBorrowers = new List<Borrower>();
            if (loanApplication.Borrowers != null)
            {
                rmBorrowers = loanApplication.Borrowers.OrderBy(b => b.OwnTypeId)
                                             .ToList();
                rmBorrowers.ForEach(b =>
                {
                    if (b.BorrowerAccountBinders != null)
                    {
                        b.BorrowerAccountBinders = b.BorrowerAccountBinders.OrderBy(br => br.BorrowerAccount.Balance).ToList();
                    }
                });
            }
            return rmBorrowers;
        }

        private string firstMortgageAssetLabel = string.Empty;
        private bool subjectPropertyAdded = false;

        public ASSETS GetAssets(LoanApplication loanApplication)
        {
            List<ASSET> assets = new List<ASSET>();
            var rmBorrowers = this.SortRainMakerBorrowers(loanApplication);
            List<int> assetAdded = new List<int>();
            int assetIndex = 1;
            Dictionary<int, string> jointAccounts = new Dictionary<int, string>();
            foreach (Borrower rmBorrower in rmBorrowers)
            {
                int borrowerIndex = rmBorrowers.IndexOf(rmBorrower) + 1;
                string borrowerLabel = $"BORROWER_{borrowerIndex}";
                if (rmBorrower.BorrowerAccountBinders != null)
                {
                    var accountBinders = rmBorrower.BorrowerAccountBinders.ToList();
                    {
                        foreach (var binder in accountBinders)
                        {
                            if (assetAdded.Contains((binder.BorrowerAccount.Id)))
                            {
                                if (jointAccounts.ContainsKey(binder.BorrowerAccount.Id))
                                {
                                    relationships.Add(new MismoRelationShipModel()
                                    {
                                        From = jointAccounts[binder.BorrowerAccount.Id],
                                        To = borrowerLabel
                                    });
                                }

                                continue;
                            }
                            assetAdded.Add(binder.BorrowerAccount.Id);

                            assetIndex = assets.Count() + 1;
                            //relationships.Add($"ASSET_{assetIndex}", $"BORROWER_{borrowerIndex}");
                            string assetLabel = $"ASSET_{assetIndex}";

                            ASSET assetToAdd = new ASSET()
                            {
                                Label = assetLabel,
                                SequenceNumber = assetIndex
                            };

                            ASSET_DETAIL assetDetail = new ASSET_DETAIL();

                            switch (binder.BorrowerAccount.AccountTypeId)
                            {
                                case 33: // Net Equity
                                    assetDetail.AssetType = Convert.ToString(AssetBase.Stock);
                                    assetDetail.AssetAccountIdentifier = "Net Equity";
                                    break;

                                case 32: // 32:  OtherNonLiquidAssets
                                    assetDetail.AssetTypeOtherDescription = "Other Non Liquid Asset";
                                    break;

                                case 18: // 18:  Other Liquid Assets
                                    assetDetail.AssetTypeOtherDescription = "OtherLiquidAsset";
                                    break;

                                case 25: // 25: Gift Not Deposited
                                    assetDetail.AssetType = null;
                                    assetDetail.AssetType = Convert.ToString(AssetBase.GiftOfCash);
                                    assetDetail.AssetTypeOtherDescription = "Gift Not Deposited";
                                    assetDetail.EXTENSION = new EXTENSION();
                                    assetDetail.EXTENSION.OTHER = new OTHER();
                                    assetDetail.EXTENSION.OTHER.ASSET_DETAIL_EXTENSION = new ASSET_DETAIL_EXTENSION();
                                    assetDetail.EXTENSION.OTHER.ASSET_DETAIL_EXTENSION.IncludedInAssetAccountIndicator = false;
                                    break;

                                case 26: // 26: Gift Of Equity
                                    assetDetail.AssetType = null;
                                    assetDetail.AssetType = Convert.ToString(AssetBase.GiftOfCash);
                                    assetDetail.AssetTypeOtherDescription = "Gift Of Equity";
                                    assetDetail.EXTENSION = new EXTENSION();
                                    assetDetail.EXTENSION.OTHER = new OTHER();
                                    assetDetail.EXTENSION.OTHER.ASSET_DETAIL_EXTENSION = new ASSET_DETAIL_EXTENSION();
                                    assetDetail.EXTENSION.OTHER.ASSET_DETAIL_EXTENSION.IncludedInAssetAccountIndicator = true;
                                    break;

                                case 28: // 26: Secured Borrowed Funds Not Deposited
                                    //assetDetail.AssetTypeOtherDescription = Convert.ToString(AssetBase.ProceedsFromSecuredLoan);
                                    assetDetail.AssetTypeOtherDescription = "Secured Borrowed Funds Not Deposited";
                                    break;

                                case 29: // 29: Cash Deposit On Sales Contract
                                    assetDetail.AssetTypeOtherDescription = "Cash Deposit On Sales Contract";
                                    break;

                                case 24: // 24: Gifts Total
                                    assetDetail.AssetTypeOtherDescription = "Gifts Total";
                                    break;

                                default:
                                    if (Enum.IsDefined(typeof(AssetBase),
                                                       (AssetBase)binder.BorrowerAccount.AccountTypeId))
                                    {
                                        assetDetail.AssetType = Convert.ToString((AssetBase)binder.BorrowerAccount.AccountTypeId);
                                    }
                                    else
                                    {
                                        assetDetail.AssetType = Convert.ToString(AssetBase.Other);
                                    }
                                    break;
                            }
                            assetDetail.AssetAccountIdentifier = binder.BorrowerAccount.AccountNumber;
                            assetDetail.AssetCashOrMarketValueAmount = binder.BorrowerAccount.Balance;



                            assetToAdd.ASSET_DETAIL = assetDetail;
                            assetToAdd.ASSET_HOLDER = new ASSET_HOLDER()
                            {
                                NAME = new NAME()
                                {
                                    FullName = binder.BorrowerAccount.Name
                                }
                            };


                            assets.Add((assetToAdd));
                            relationships.Add(new MismoRelationShipModel()
                            {
                                From = assetLabel,
                                To = borrowerLabel
                            });
                            if (!jointAccounts.ContainsKey(binder.BorrowerAccount.Id))
                            {
                                jointAccounts.Add(binder.BorrowerAccount.Id, assetLabel);
                            }
                        }
                    }
                }

                ASSET propertyAsset = null;
                if ((!subjectPropertyAdded) && (Enum.IsDefined(typeof(LoanPurposeBase), (LoanPurposeBase)loanApplication.LoanPurposeId) || (loanApplication.LoanPurposeId == 3)))
                {
                    if (((LoanPurposeBase)loanApplication.LoanPurposeId) == LoanPurposeBase.Refinance || (loanApplication.LoanPurposeId == 3)) // If refinance or cash out
                    {
                        propertyAsset = new ASSET()
                        {
                            Label = $"ASSET_{++assetIndex}",
                            SequenceNumber = assetIndex
                        };
                        firstMortgageAssetLabel = propertyAsset.Label;
                        assets.Add(propertyAsset);
                        relationships.Add(new MismoRelationShipModel()
                        {
                            From = propertyAsset.Label,
                            To = borrowerLabel
                        });
                        propertyAsset.OWNED_PROPERTY = new OWNED_PROPERTY();
                        propertyAsset.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL = new OWNED_PROPERTY_DETAIL();
                        propertyAsset.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyDispositionStatusType = Convert.ToString(OwnedPropertyDispositionStatusBase.Retain);
                        propertyAsset.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyLienUPBAmount = loanApplication.PropertyInfo.FirstMortgageBalance + loanApplication.PropertyInfo.SecondLienBalance;
                        propertyAsset.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyRentalIncomeGrossAmount = 0;
                        propertyAsset.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyRentalIncomeNetAmount = 0;
                        propertyAsset.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertySubjectIndicator = true;
                        propertyAsset.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyMaintenanceExpenseAmount = loanApplication.PropertyInfo.MonthlyDue;

                        propertyAsset.OWNED_PROPERTY.PROPERTY = new PROPERTY();
                        propertyAsset.OWNED_PROPERTY.PROPERTY.ADDRESS = new List<ADDRESS>();
                        ADDRESS propertyAddress = this.GetMismoAddress(loanApplication.PropertyInfo.AddressInfo);
                        propertyAsset.OWNED_PROPERTY.PROPERTY.ADDRESS.Add(propertyAddress);

                        propertyAsset.OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL = new PROPERTY_DETAIL()
                        {
                            PropertyEstimatedValueAmount = loanApplication.PropertyInfo.PropertyValue,
                        };
                        if (Enum.IsDefined(typeof(PropertyUsageBase),
                                           (PropertyUsageBase)loanApplication.PropertyInfo?.PropertyUsageId))
                        {
                            propertyAsset.OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL.PropertyUsageType = Convert.ToString((PropertyUsageBase)loanApplication.PropertyInfo?.PropertyUsageId);
                        }
                        else
                        {
                            propertyAsset.OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL.PropertyUsageType = Convert.ToString(PropertyUsageBase.Other);
                        }
                        subjectPropertyAdded = true;
                    }
                }

                #region Set REOs

                if (rmBorrower.BorrowerProperties != null)
                {
                    foreach (var rmProperty in rmBorrower.BorrowerProperties)
                    {
                        //assetIndex = assets.Count() + 1;
                        var isCurrentResidence = false;
                        isCurrentResidence = rmBorrower.BorrowerResidences.Any(br => rmProperty.PropertyInfo != null && br.LoanAddressId == rmProperty.PropertyInfo.AddressInfo?.Id);
                        if (rmProperty.PropertyInfo != null && rmProperty.PropertyInfo.AddressInfo != null)
                        {
                            //relationships.Add($"ASSET_{assetIndex}", $"BORROWER_{borrowerIndex}");
                            ASSET reo = new ASSET()
                            {
                                Label = $"ASSET_{++assetIndex}",
                                SequenceNumber = assetIndex
                            };
                            assets.Add(reo);
                            relationships.Add(new MismoRelationShipModel()
                            {
                                From = reo.Label,
                                To = borrowerLabel
                            });

                            #region Owned Property Detail
                            reo.OWNED_PROPERTY = new OWNED_PROPERTY();
                            reo.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL = new OWNED_PROPERTY_DETAIL();
                            reo.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyMaintenanceExpenseAmount = rmProperty.PropertyInfo.MonthlyDue;
                            reo.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertySubjectIndicator = false;

                            /*
                            if (rmProperty.PropertyInfo.IntentToSellPriorToPurchase == true)
                            {
                                reo.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyDispositionStatusType = Convert.ToString(OwnedPropertyDispositionStatusBase.PendingSale);
                            }
                            else
                            {
                                string propertyStatus = rmProperty.PropertyInfo.PropertyStatus;
                                if (!string.IsNullOrEmpty(propertyStatus))
                                {
                                    if (propertyStatus.IndexOf("rental",
                                                               StringComparison.InvariantCultureIgnoreCase) != -1)
                                    {
                                        reo.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyDispositionStatusType = Convert.ToString(OwnedPropertyDispositionStatusBase.Retain);
                                    }
                                }
                                else
                                {
                                    if(rmProperty.pr)
                                }
                            }
                            */
                            string propertyStatus = rmProperty.PropertyInfo.PropertyStatus?.Replace(" ", string.Empty);
                            string mismoStatus = string.Empty;
                            if (!string.IsNullOrEmpty(propertyStatus))
                            {
                                if (propertyStatus.IndexOf("rental",
                                                           StringComparison.InvariantCultureIgnoreCase) != -1)
                                {
                                    mismoStatus = Convert.ToString(OwnedPropertyDispositionStatusBase.Retain);
                                    reo.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyRentalIncomeGrossAmount = rmProperty.PropertyInfo.RentalIncome;
                                }
                                else
                                {
                                    mismoStatus = propertyStatus;
                                }
                            }
                            else
                            {
                                if (rmProperty.PropertyInfo.IntentToSellPriorToPurchase == true)
                                {
                                    mismoStatus = Convert.ToString(OwnedPropertyDispositionStatusBase.PendingSale);
                                }
                                else
                                {
                                    var isCurrent = rmBorrower.BorrowerResidences.Any(br => br.LoanAddressId == rmProperty.PropertyInfo.AddressInfo?.Id);
                                    if (isCurrent == true)
                                    {
                                        mismoStatus = Convert.ToString(OwnedPropertyDispositionStatusBase.Retain);
                                        reo.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyRentalIncomeGrossAmount = 0;
                                        reo.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyRentalIncomeNetAmount = 0;
                                    }
                                }
                            }

                            if (!string.IsNullOrEmpty(mismoStatus))
                            {
                                reo.OWNED_PROPERTY.OWNED_PROPERTY_DETAIL.OwnedPropertyDispositionStatusType = mismoStatus;
                            }
                            #endregion

                            #region Property Address

                            if (rmProperty.PropertyInfo != null && rmProperty.PropertyInfo.AddressInfo != null)
                            {
                                if (rmProperty.PropertyInfo.AddressInfo != null)
                                {
                                    reo.OWNED_PROPERTY.PROPERTY = new PROPERTY();
                                    reo.OWNED_PROPERTY.PROPERTY.ADDRESS = new List<ADDRESS>();
                                    ADDRESS propertyAddress = this.GetMismoAddress(rmProperty.PropertyInfo.AddressInfo);
                                    reo.OWNED_PROPERTY.PROPERTY.ADDRESS.Add(propertyAddress);
                                }

                                reo.OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL = new PROPERTY_DETAIL()
                                {
                                    FinancedUnitCount = null
                                };
                                reo.OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL.PropertyCurrentUsageType = Convert.ToString(PropertyCurrentUsageBase.Other);
                                if (rmProperty.PropertyInfo.PropertyUsageId.HasValue)
                                {
                                    if (Enum.IsDefined(typeof(PropertyCurrentUsageBase),
                                                       (PropertyCurrentUsageBase)rmProperty.PropertyInfo.PropertyUsageId))
                                    {
                                        reo.OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL.PropertyUsageType = Convert.ToString((PropertyCurrentUsageBase)rmProperty.PropertyInfo.PropertyUsageId);
                                        reo.OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL.PropertyCurrentUsageType = Convert.ToString((PropertyCurrentUsageBase)rmProperty.PropertyInfo.PropertyUsageId);
                                    }
                                }
                                reo.OWNED_PROPERTY.PROPERTY.PROPERTY_DETAIL.PropertyEstimatedValueAmount = rmProperty.PropertyInfo.PropertyValue;
                            }
                            #endregion

                        }
                    }
                }
                #endregion
            }
            return new ASSETS() { ASSET = assets };
        }

        public SUBJECT_PROPERTY GetSubjectProperty(LoanApplication loanApplication)
        {
            SUBJECT_PROPERTY property = new SUBJECT_PROPERTY();
            property.ADDRESS = new ADDRESS();
            property.ADDRESS.AddressLineText = $"{loanApplication.PropertyInfo?.AddressInfo?.StreetAddress} {this.GetAddressUnit(loanApplication.PropertyInfo?.AddressInfo?.UnitNo)}";
            property.ADDRESS.CityName = loanApplication.PropertyInfo?.AddressInfo?.CityName;
            property.ADDRESS.CountyName = loanApplication.PropertyInfo?.AddressInfo?.CountyName;
            property.ADDRESS.PostalCode = loanApplication.PropertyInfo?.AddressInfo?.ZipCode;
            property.ADDRESS.StateCode = loanApplication.PropertyInfo?.AddressInfo?.State.Abbreviation;

            property.PROJECT = new PROJECT();
            property.PROPERTY_DETAIL = new PROPERTY_DETAIL();

            if (loanApplication.PropertyInfo != null && loanApplication.PropertyInfo.DateAcquired.HasValue)
            {
                property.PROPERTY_DETAIL.PropertyAcquiredDate = loanApplication.PropertyInfo?.DateAcquired;
            }

            property.PROPERTY_DETAIL.FinancedUnitCount = 1;

            if (Enum.IsDefined(typeof(RainmakerPropertyUnitType),
                               (RainmakerPropertyUnitType)loanApplication.PropertyInfo?.PropertyTypeId))
            {
                PropertyUnitType type = (PropertyUnitType)loanApplication.PropertyInfo?.PropertyTypeId;
                switch (type)
                {
                    case PropertyUnitType.TwoUnitBuilding:
                        property.PROPERTY_DETAIL.FinancedUnitCount = 2;
                        break;

                    case PropertyUnitType.ThreeUnitBuilding:
                        property.PROPERTY_DETAIL.FinancedUnitCount = 3;
                        break;

                    case PropertyUnitType.FourUnitBuilding:
                        property.PROPERTY_DETAIL.FinancedUnitCount = 4;
                        break;
                }
            }

            property.PROPERTY_DETAIL.PropertyEstateType = Convert.ToString(PropertyEstateBase.FeeSimple);

            property.PROPERTY_DETAIL.PropertyEstimatedValueAmount = loanApplication.PropertyInfo?.PropertyValue;
            //property.PROPERTY_DETAIL.PropertyExistingCleanEnergyLienIndicator = null; // TODO
            //property.PROPERTY_DETAIL.PropertyInProjectIndicator = null; // TODO
            //property.PROPERTY_DETAIL.PropertyMixedUsageIndicator = null; // TODO
            if (Enum.IsDefined(typeof(PropertyUsageBase),
                               (PropertyUsageBase)loanApplication.PropertyInfo?.PropertyUsageId))
            {
                property.PROPERTY_DETAIL.PropertyUsageType = Convert.ToString((PropertyUsageBase)loanApplication.PropertyInfo?.PropertyUsageId);
                property.PROPERTY_DETAIL.PropertyCurrentUsageType = Convert.ToString((PropertyUsageBase)loanApplication.PropertyInfo?.PropertyUsageId);
            }
            else
            {
                property.PROPERTY_DETAIL.PropertyUsageType = Convert.ToString(PropertyUsageBase.Other);
                property.PROPERTY_DETAIL.PropertyCurrentUsageType = Convert.ToString(PropertyUsageBase.Other);
            }
            //property.PROPERTY_DETAIL.PUDIndicator = null; // TODO

            property.PROJECT = new PROJECT();
            property.PROJECT.PROJECT_DETAIL = new PROJECT_DETAIL();
            switch (loanApplication.PropertyInfo?.PropertyTypeId)
            {
                case 1: // Single Family Detached
                    property.PROPERTY_DETAIL.AttachmentType = Convert.ToString(AttachmentBase.Detached);
                    property.PROPERTY_DETAIL.ConstructionMethodType = Convert.ToString(ConstructionMethodBase.SiteBuilt);
                    property.PROPERTY_DETAIL.PropertyInProjectIndicator = false;
                    break;

                case 2: // Condominium
                    property.PROJECT.PROJECT_DETAIL.ProjectLegalStructureType = Convert.ToString(ProjectLegalStructureBase.Condominium);
                    property.PROPERTY_DETAIL.AttachmentType = Convert.ToString(AttachmentBase.Attached);
                    property.PROPERTY_DETAIL.ConstructionMethodType = Convert.ToString(ConstructionMethodBase.SiteBuilt);
                    property.PROPERTY_DETAIL.PropertyInProjectIndicator = true;
                    break;

                case 7: // Cooperative
                    property.PROJECT.PROJECT_DETAIL.ProjectLegalStructureType = Convert.ToString(ProjectLegalStructureBase.Cooperative);
                    property.PROPERTY_DETAIL.AttachmentType = Convert.ToString(AttachmentBase.Attached);
                    property.PROPERTY_DETAIL.ConstructionMethodType = Convert.ToString(ConstructionMethodBase.SiteBuilt);
                    break;

                case 9: // Manufactured
                    property.PROPERTY_DETAIL.AttachmentType = Convert.ToString(AttachmentBase.Detached);
                    property.PROPERTY_DETAIL.ConstructionMethodType = Convert.ToString(ConstructionMethodBase.Manufactured);
                    break;

                case 13: // Manufactured_Condo_PUD_COOP
                    property.PROPERTY_DETAIL.AttachmentType = Convert.ToString(AttachmentBase.Detached);
                    property.PROPERTY_DETAIL.ConstructionMethodType = Convert.ToString(ConstructionMethodBase.SiteBuilt);
                    property.PROPERTY_DETAIL.PropertyInProjectIndicator = true;
                    property.PROPERTY_DETAIL.PropertyMixedUsageIndicator = true;
                    property.PROPERTY_DETAIL.PUDIndicator = true;
                    break;
            }

            property.SALES_CONTRACTS = new SALES_CONTRACTS();
            property.SALES_CONTRACTS.SALES_CONTRACT = new SALES_CONTRACT();
            property.SALES_CONTRACTS.SALES_CONTRACT.SALES_CONTRACT_DETAIL = new SALES_CONTRACT_DETAIL();
            property.SALES_CONTRACTS.SALES_CONTRACT.SALES_CONTRACT_DETAIL.SalesContractAmount = loanApplication.LoanAmount;

            return property;
        }

        public EXPENSES GetExpenseLiabilities(LoanApplication loanApplication)
        {
            EXPENSES expensesObj = new EXPENSES();
            List<EXPENSE> expenses = new List<EXPENSE>();
            expensesObj.EXPENSE = expenses;

            if (loanApplication.Borrowers != null && this._liabilitiesToSkip != null)
            {
                var rmBorrowers = this.SortRainMakerBorrowers(loanApplication);
                int expenseIndex = 1;
                int borrowerIndex = 0;

                foreach (var rmBorrower in rmBorrowers)
                {
                    borrowerIndex = rmBorrowers.IndexOf(rmBorrower) + 1;
                    string borrowerLabel = $"BORROWER_{borrowerIndex}";
                    if (rmBorrower.BorrowerLiabilities != null)
                    {
                        var expenseLiabilities = rmBorrower.BorrowerLiabilities
                                                           .Where(liab => this._liabilitiesToSkip.Contains(liab.LiabilityTypeId))
                                                           .ToList();
                        if (expenseLiabilities != null && expenseLiabilities.Count > 0)
                        {
                            foreach (var expenseLiability in expenseLiabilities)
                            {
                                EXPENSE expenseToAdd = new EXPENSE();
                                expenses.Add(expenseToAdd);

                                expenseToAdd.SequenceNumber = expenseIndex;
                                expenseToAdd.Label = $"EXPENSE_{expenseIndex++}";
                                expenseToAdd.ExpenseMonthlyPaymentAmount = expenseLiability.MonthlyPayment;
                                expenseToAdd.ExpenseRemainingTermMonthsCount = expenseLiability.RemainingMonth;

                                if (Enum.IsDefined(typeof(ExpenseBase),
                                                   (ExpenseBase)expenseLiability.LiabilityTypeId))
                                {
                                    if (((ExpenseBase)expenseLiability.LiabilityTypeId) == ExpenseBase.ChildCare)
                                    {
                                        expenseToAdd.ExpenseType = Convert.ToString(ExpenseBase.Other);
                                        expenseToAdd.ExpenseTypeOtherDescription = "Child Care";
                                    }
                                    else
                                    {
                                        expenseToAdd.ExpenseType = Convert.ToString((ExpenseBase)expenseLiability.LiabilityTypeId);
                                    }
                                }
                                else
                                {
                                    expenseToAdd.ExpenseType = Convert.ToString(ExpenseBase.Other);
                                }


                                relationships.Add(new MismoRelationShipModel()
                                {
                                    To = borrowerLabel,
                                    From = expenseToAdd.Label
                                });
                            }
                        }

                        expenseIndex = expenses.Count() + 1;
                    }
                }
            }

            return expensesObj;
        }

        public LIABILITIES GetLiabilities(LoanApplication loanApplication)
        {
            LIABILITIES liabilityObj = new LIABILITIES();
            List<LIABILITY> liabilities = new List<LIABILITY>();
            liabilityObj.LIABILITY = liabilities;

            if (loanApplication.Borrowers != null)
            {
                var rmBorrowers = this.SortRainMakerBorrowers(loanApplication);
                int liabilityIndex = 1;
                int borrowerIndex = 0;

                #region First Mortgage
                if (Enum.IsDefined(typeof(LoanPurposeBase),
                                           (LoanPurposeBase)loanApplication.LoanPurposeId))
                {
                    LoanPurposeBase loanPurpose = (LoanPurposeBase)loanApplication.LoanPurposeId;
                    if (loanPurpose != LoanPurposeBase.Purchase)
                    {
                        if (loanApplication.PropertyInfo != null && loanApplication.PropertyInfo?.MortgageOnProperties != null)
                        {
                            var firstMortgage = loanApplication.PropertyInfo?.MortgageOnProperties.FirstOrDefault(mortgage => mortgage.IsFirstMortgage == true);
                            if (firstMortgage != null && firstMortgage.MonthlyPayment > 0)
                            {
                                liabilityIndex = liabilities.Count() + 1;
                                LIABILITY firstMortgageLiability = new LIABILITY();
                                //relationships.Add($"LIABILITY_{liabilityIndex}", "BORROWER_1");
                                firstMortgageLiability.SequenceNumber = liabilityIndex;
                                firstMortgageLiability.Label = $"LIABILITY_{liabilityIndex}";

                                LIABILITY_DETAIL firstLiabilityDetail = new LIABILITY_DETAIL();
                                firstLiabilityDetail.LiabilityExclusionIndicator = false; // TODO
                                firstLiabilityDetail.LiabilityMonthlyPaymentAmount = firstMortgage.MonthlyPayment;
                                firstLiabilityDetail.LiabilityPayoffStatusIndicator = true; // TODO
                                //firstLiabilityDetail.LiabilityType = Convert.ToString(LiabilityBase.Installment);
                                firstLiabilityDetail.LiabilityType = Convert.ToString(LiabilityBase.MortgageLoan);
                                firstLiabilityDetail.LiabilityUnpaidBalanceAmount = firstMortgage.MortgageBalance;

                                LIABILITY_HOLDER liabilityHolder = new LIABILITY_HOLDER()
                                {
                                    NAME = new NAME()
                                    {
                                        FullName = "First Mortgage"
                                    }
                                };
                                firstMortgageLiability.LIABILITY_HOLDER = liabilityHolder;

                                firstMortgageLiability.LIABILITY_DETAIL = firstLiabilityDetail;
                                liabilities.Add(firstMortgageLiability);
                                if (!string.IsNullOrEmpty(firstMortgageAssetLabel))
                                {
                                    firstMortgageLiability.LIABILITY_DETAIL.LiabilityPayoffStatusIndicator = false; // Byte sets this to true automatically if not passed
                                    relationships.Add(new MismoRelationShipModel()
                                    {
                                        From = firstMortgageAssetLabel,
                                        To = firstMortgageLiability.Label
                                    });
                                }
                                relationships.Add(new MismoRelationShipModel()
                                {
                                    From = firstMortgageLiability.Label,
                                    To = "BORROWER_1"
                                });
                            }
                        }
                    }
                }
                #endregion

                #region Second Mortgages
                // var secondMortgages = loanApplication.PropertyInfo?.MortgageOnProperties.Where(mortgage => mortgage.IsFirstMortgage == false).ToList();// todo clearfication required
                var secondMortgage = loanApplication.PropertyInfo?.MortgageOnProperties?.FirstOrDefault(mortgage => mortgage.IsFirstMortgage == false);
                if (secondMortgage != null && secondMortgage.MonthlyPayment > 0)
                {
                    liabilityIndex = liabilities.Count() + 1;
                    LIABILITY secondMortgageLiability = new LIABILITY();
                    //relationships.Add($"LIABILITY_{liabilityIndex}", "BORROWER_1");
                    secondMortgageLiability.SequenceNumber = liabilityIndex;
                    secondMortgageLiability.Label = $"LIABILITY_{liabilityIndex}";

                    relationships.Add(new MismoRelationShipModel()
                    {
                        From = firstMortgageAssetLabel,
                        To = secondMortgageLiability.Label
                    });

                    LIABILITY_DETAIL firstLiabilityDetail = new LIABILITY_DETAIL();
                    firstLiabilityDetail.LiabilityExclusionIndicator = false; // TODO
                    firstLiabilityDetail.LiabilityPaymentIncludesTaxesInsuranceIndicator = false;
                    firstLiabilityDetail.LiabilityMonthlyPaymentAmount = secondMortgage.MonthlyPayment;
                    firstLiabilityDetail.LiabilityPayoffStatusIndicator = false; // TODO
                    firstLiabilityDetail.LiabilityType = Convert.ToString(LiabilityBase.MortgageLoan);
                    firstLiabilityDetail.LiabilityUnpaidBalanceAmount = secondMortgage.MortgageBalance;

                    LIABILITY_HOLDER liabilityHolder = new LIABILITY_HOLDER()
                    {
                        NAME = new NAME()
                        {
                            FullName = "Second Mortgage"
                        }
                    };
                    secondMortgageLiability.LIABILITY_HOLDER = liabilityHolder;

                    secondMortgageLiability.LIABILITY_DETAIL = firstLiabilityDetail;
                    liabilities.Add(secondMortgageLiability);
                    relationships.Add(new MismoRelationShipModel()
                    {
                        From = secondMortgageLiability.Label,
                        To = "BORROWER_1"
                    });
                }
                #endregion

                #region Liabilities Other Than First Mortgage

                foreach (var rmBorrower in rmBorrowers)
                {
                    borrowerIndex = rmBorrowers.IndexOf(rmBorrower) + 1;
                    string borrowerLabel = $"BORROWER_{borrowerIndex}";

                    if (rmBorrower.BorrowerLiabilities != null)
                    {
                        liabilityIndex = liabilities.Count() + 1;
                        foreach (var liability in rmBorrower.BorrowerLiabilities)
                        {
                            if (this._liabilitiesToSkip != null)
                            {
                                if (this._liabilitiesToSkip.Contains(liability.LiabilityTypeId))
                                {
                                    continue;
                                }
                            }

                            //relationships.Add($"LIABILITY_{liabilityIndex}", $"BORROWER_{borrowerIndex}");
                            LIABILITY liabilityToAdd = new LIABILITY();
                            liabilityToAdd.SequenceNumber = liabilityIndex;
                            liabilityToAdd.Label = $"LIABILITY_{liabilityIndex}";

                            LIABILITY_DETAIL liabilityDetail = new LIABILITY_DETAIL();
                            liabilityDetail.LiabilityAccountIdentifier = liability.AccountNumber;
                            liabilityDetail.LiabilityExclusionIndicator = false; // TODO
                            liabilityDetail.LiabilityMonthlyPaymentAmount = liability.MonthlyPayment;
                            liabilityDetail.LiabilityPayoffStatusIndicator = liability.WillBePaidByThisLoan == true;
                            liabilityDetail.LiabilityRemainingTermMonthsCount = liability.RemainingMonth;
                            if (Enum.IsDefined(typeof(LiabilityBase),
                                               (LiabilityBase)liability.LiabilityTypeId))
                            {
                                liabilityDetail.LiabilityType = Convert.ToString((LiabilityBase)liability.LiabilityTypeId);
                            }
                            else
                            {
                                liabilityDetail.LiabilityType = Convert.ToString(LiabilityBase.Other);
                            }
                            //liabilityDetail.LiabilityType = Convert.ToString(LiabilityBase.Installment);
                            liabilityDetail.LiabilityUnpaidBalanceAmount = liability.Balance;

                            LIABILITY_HOLDER liabilityHolder = new LIABILITY_HOLDER()
                            {
                                NAME = new NAME()
                                {
                                    FullName = liability.CompanyName
                                }
                            };

                            liabilityToAdd.LIABILITY_HOLDER = liabilityHolder;

                            liabilityToAdd.LIABILITY_DETAIL = liabilityDetail;
                            liabilities.Add(liabilityToAdd);
                            relationships.Add(new MismoRelationShipModel()
                            {
                                From = liabilityToAdd.Label,
                                To = borrowerLabel
                            });
                        }
                    }
                }
                #endregion
            }
            return liabilityObj;
        }

        public List<LOAN> GetLoanInfo(LoanApplication loanApplication)
        {
            List<LOAN> loanList = new List<LOAN>();
            int loanSubjectIndex = 1;
            LOAN loanToAdd = new LOAN()
            {
                SequenceNumber = 1,
                LoanRoleType = Convert.ToString(LoanRoleBase.SubjectLoan),
                Label = $"SUBJECT_LOAN_{loanSubjectIndex}",
                CONSTRUCTION = new CONSTRUCTION()
            };
            //loanToAdd.CONSTRUCTION = new CONSTRUCTION()
            //{
            //    LandOriginalCostAmount = loanApplication.PropertyInfo?.OriginalPurchasePrice
            //};

            #region Add Amortization
            AMORTIZATION amortization = new AMORTIZATION();
            AMORTIZATION_RULE rule = new AMORTIZATION_RULE();
            if (loanApplication.ProductAmortizationTypeId != null)
            {
                if (Enum.IsDefined(typeof(AmortizationBase),
                                   (AmortizationBase)loanApplication.ProductAmortizationTypeId))
                {
                    rule.AmortizationType = Convert.ToString((AmortizationBase)loanApplication.ProductAmortizationTypeId);

                }
            }
            else
            {
                rule.AmortizationType = Convert.ToString(AmortizationBase.Other);
            }
            rule.LoanAmortizationPeriodCount = "360";
            rule.LoanAmortizationPeriodType = Convert.ToString(LoanAmortizationPeriodBase.Month);

            amortization.AMORTIZATION_RULE = rule;
            loanToAdd.AMORTIZATION = amortization;
            #endregion

            #region Document Specific Data Set

            loanToAdd.DOCUMENT_SPECIFIC_DATA_SETS = new DOCUMENT_SPECIFIC_DATA_SETS()
            {
                DOCUMENT_SPECIFIC_DATA_SET = new DOCUMENT_SPECIFIC_DATA_SET()
            };

            DOCUMENT_SPECIFIC_DATA_SET dataSetToAdd = new DOCUMENT_SPECIFIC_DATA_SET()
            {
                SequenceNumber = 1
            };
            URLA urlaData = new URLA();
            URLA_DETAIL urlaDetail = new URLA_DETAIL();


            urlaData.URLA_DETAIL = urlaDetail;
            dataSetToAdd.URLA = urlaData;

            //urlaDetail.ApplicationSignedByLoanOriginatorDate = loanApplication.CreatedOnUtc.Value.ToString("yyyy-MM-dd");
            urlaDetail.EstimatedClosingCostsAmount = 0; //TODO
            urlaDetail.MIAndFundingFeeFinancedAmount = 0; //TODO
            //urlaDetail.MIAndFundingFeeTotalAmount = loanApplication.LoanAmount; //TODO
            urlaDetail.PrepaidItemsEstimatedAmount = 0; //TODO

            loanToAdd.DOCUMENT_SPECIFIC_DATA_SETS.DOCUMENT_SPECIFIC_DATA_SET = (dataSetToAdd);
            #endregion

            #region HMDA Loan
            HMDA_LOAN hmdaLoan = new HMDA_LOAN();
            HMDA_LOAN_DETAIL hmdaLoanDetail = new HMDA_LOAN_DETAIL();
            hmdaLoanDetail.HMDA_HOEPALoanStatusIndicator = false; // TODO

            hmdaLoan.HMDA_LOAN_DETAIL = hmdaLoanDetail;
            loanToAdd.HMDA_LOAN = hmdaLoan;
            #endregion

            #region Housing Expense

            loanToAdd.HOUSING_EXPENSES = new HOUSING_EXPENSES();
            loanToAdd.HOUSING_EXPENSES.HOUSING_EXPENSE = new List<HOUSING_EXPENSE>();

            var homeOwnersInsurance =
                loanApplication.PropertyInfo?.PropertyTaxEscrows?.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.HomeOwnersInsurance);

            if (homeOwnersInsurance != null)
            {
                var housingExpense1 = new HOUSING_EXPENSE();
                housingExpense1.HousingExpensePaymentAmount = homeOwnersInsurance.AnnuallyPayment / 12;
                housingExpense1.HousingExpenseTimingType = HousingExpenseTimingBase.Proposed.ToString();
                housingExpense1.HousingExpenseType = HousingExpenseBase.HomeownersInsurance.ToString();
                loanToAdd.HOUSING_EXPENSES.HOUSING_EXPENSE.Add(housingExpense1);
            }

            var propertyTaxes = loanApplication.PropertyInfo?.PropertyTaxEscrows?.FirstOrDefault(x => x.EscrowEntityTypeId == (int)EscrowEntityTypeEnum.PropertyTaxes);

            if (propertyTaxes != null)
            {
                var housingExpense2 = new HOUSING_EXPENSE();
                housingExpense2.HousingExpensePaymentAmount = propertyTaxes.AnnuallyPayment / 12;
                housingExpense2.HousingExpenseTimingType = HousingExpenseTimingBase.Proposed.ToString();
                housingExpense2.HousingExpenseType = HousingExpenseBase.RealEstateTax.ToString();
                loanToAdd.HOUSING_EXPENSES.HOUSING_EXPENSE.Add(housingExpense2);
            }

            if (loanApplication?.PropertyInfo?.MortgageOnProperties != null)
            {
                var firstMortgage = loanApplication?.PropertyInfo?.MortgageOnProperties.FirstOrDefault(mort => mort.IsFirstMortgage == true);
                if (firstMortgage != null)
                {
                    var mortgageExpenses = new HOUSING_EXPENSE();
                    mortgageExpenses.HousingExpensePaymentAmount = firstMortgage.MonthlyPayment;
                    mortgageExpenses.HousingExpenseTimingType = Convert.ToString(HousingExpenseTimingBase.Present);
                    mortgageExpenses.HousingExpenseType = Convert.ToString(HousingExpenseBase.FirstMortgagePrincipalAndInterest);
                    loanToAdd.HOUSING_EXPENSES.HOUSING_EXPENSE.Add(mortgageExpenses);
                }
            }

            #endregion

            #region Loan Detail
            LOAN_DETAIL loanDetail = new LOAN_DETAIL();
            //loanDetail.ApplicationReceivedDate = loanApplication.CreatedOnUtc.Value.ToString("yyyy-MM-dd");
            loanDetail.BalloonIndicator = false; // TODO
            loanDetail.BelowMarketSubordinateFinancingIndicator = false; // TODO
            loanDetail.BuydownTemporarySubsidyFundingIndicator = false; // TODO
            loanDetail.ConstructionLoanIndicator = false; // TODO
            loanDetail.ConversionOfContractForDeedIndicator = false; // TODO
            loanDetail.EnergyRelatedImprovementsIndicator = false; // TODO
            loanDetail.InterestOnlyIndicator = false; // TODO
            loanDetail.NegativeAmortizationIndicator = false; // TODO
            loanDetail.PrepaymentPenaltyIndicator = false; // TODO
            loanDetail.RenovationLoanIndicator = false; // TODO

            loanToAdd.LOAN_DETAIL = loanDetail;
            #endregion

            #region Loan Identifier
            // SKIPPED. Contains Byte file name
            #endregion

            #region Loan Product
            LOAN_PRODUCT loanProduct = new LOAN_PRODUCT();
            LOAN_PRODUCT_DETAIL loanProductDetail = new LOAN_PRODUCT_DETAIL()
            {
                DiscountPointsTotalAmount = 0
            };

            loanProduct.LOAN_PRODUCT_DETAIL = loanProductDetail;
            loanToAdd.LOAN_PRODUCT = loanProduct;
            #endregion

            #region Origination Systems
            // SKIPPED. This section contains data about software haivng loan file detail saved like BYTE/Encompass
            #endregion

            #region Qualification

            #endregion

            #region Refinance



            if ((loanApplication.LoanPurposeId == 2 || loanApplication.LoanPurposeId == 3) // 2: Refinance, 3: Cashout
                && Enum.IsDefined(typeof(RefinancePrimaryPurposeBase),
                                  (RefinancePrimaryPurposeBase)loanApplication.LoanGoalId))
            {
                var cashoutType = RefinanceCashOutDeterminationBase.CashOut;
                var refiPurposeType = (RefinancePrimaryPurposeBase)loanApplication.LoanGoalId;
                if (refiPurposeType == RefinancePrimaryPurposeBase.InterestRateReduction)
                {
                    cashoutType = RefinanceCashOutDeterminationBase.NoCashOut;
                }
                else
                {
                    loanToAdd.REFINANCE = new REFINANCE()
                    {
                        RefinanceCashOutDeterminationType = Convert.ToString(RefinanceCashOutDeterminationBase.CashOut),
                        RefinancePrimaryPurposeType = Convert.ToString(RefinancePrimaryPurposeBase.Other)
                    };
                }
                loanToAdd.REFINANCE = new REFINANCE()
                {
                    //RefinanceCashOutDeterminationType = Convert.ToString(RefinanceCashOutDeterminationBase.CashOut),
                    //RefinancePrimaryPurposeType = Convert.ToString((RefinancePrimaryPurposeBase)loanApplication.LoanGoalId)
                    RefinanceCashOutDeterminationType = Convert.ToString(cashoutType),
                    RefinancePrimaryPurposeType = Convert.ToString(refiPurposeType)
                };
            }
            #endregion

            #region Terms Of Loan
            TERMS_OF_LOAN termsOfLoan = new TERMS_OF_LOAN();
            termsOfLoan.BaseLoanAmount = loanApplication.LoanAmount;
            termsOfLoan.LienPriorityType = Convert.ToString(LienPriorityBase.FirstLien); // TODO
            if (Enum.IsDefined(typeof(LoanPurposeBase), (LoanPurposeBase)loanApplication.LoanPurposeId))
            {
                termsOfLoan.LoanPurposeType = Convert.ToString((LoanPurposeBase)loanApplication.LoanPurposeId);
            }
            else
            {
                termsOfLoan.LoanPurposeType = Convert.ToString(LoanPurposeBase.Unknown);
            }

            if (loanApplication.LoanPurposeId == 3) // 3: Cashout
            {
                termsOfLoan.LoanPurposeType = Convert.ToString(LoanPurposeBase.Refinance);
            }

            if (loanApplication.LoanTypeId.HasValue)
            {
                if (Enum.IsDefined(typeof(MortgageBase),
                                   (MortgageBase)loanApplication.LoanTypeId))
                {
                    termsOfLoan.MortgageType = Convert.ToString((MortgageBase)loanApplication.LoanTypeId);
                }
                else
                {
                    termsOfLoan.MortgageType = Convert.ToString(MortgageBase.Other);
                }
            }
            else
            {
                termsOfLoan.MortgageType = Convert.ToString(MortgageBase.Conventional);
            }


            loanToAdd.TERMS_OF_LOAN = termsOfLoan;
            #endregion

            loanList.Add(loanToAdd);
            return loanList;
        }

        public List<PARTY> GetBorrowers(LoanApplication loanApplication)
        {
            List<PARTY> borrowers = new List<PARTY>();

            var rmBorrowers = this.SortRainMakerBorrowers(loanApplication);
            //int borrowerIndex = 1;
            //int currentIncomeItemIndex = 0;
            //int incomeEmployerIndex = 0;
            //int incomeCount = 1;
            //int employerCount = 1;
            //int employerIndex = 1;

            int borrowerIndex = 0;
            int totalEmployerCount = 0;
            int totalCurrentIncomesCount = 0;
            foreach (var rmBorrower in rmBorrowers)
            {
                borrowerIndex = rmBorrowers.IndexOf(rmBorrower) + 1;
                string borrowerLabel = $"BORROWER_{borrowerIndex}";
                PARTY borrowerToAdd = new PARTY()
                {
                    SequenceNumber = borrowerIndex,
                    TAXPAYER_IDENTIFIERS = new TAXPAYER_IDENTIFIERS()
                    {
                        TAXPAYER_IDENTIFIER = new TAXPAYER_IDENTIFIER()
                        {
                            SequenceNumber = 1,
                            TaxpayerIdentifierType = Convert.ToString(TaxpayerIdentifierBase.SocialSecurityNumber)
                        }
                    }
                };

                #region Individual Data
                INDIVIDUAL individual = new INDIVIDUAL();

                #region Contact Points
                CONTACT_POINTS borrowerContactPoints = new CONTACT_POINTS();
                List<CONTACT_POINT> contactPoints = new List<CONTACT_POINT>();
                borrowerContactPoints.CONTACT_POINT = contactPoints;

                #region Borrower's Email
                if (!string.IsNullOrEmpty(rmBorrower.LoanContact.EmailAddress))
                {
                    CONTACT_POINT emailContact = new CONTACT_POINT();
                    CONTACT_POINT_EMAIL email = new CONTACT_POINT_EMAIL()
                    {
                        ContactPointEmailValue = rmBorrower.LoanContact.EmailAddress
                    };
                    emailContact.CONTACT_POINT_EMAIL = email;

                    contactPoints.Add(emailContact);
                }
                #endregion

                #region Borrower's Home Phone

                if (!string.IsNullOrEmpty(rmBorrower.LoanContact.HomePhone))
                {
                    CONTACT_POINT homeContact = new CONTACT_POINT();
                    homeContact.CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE();
                    homeContact.CONTACT_POINT_TELEPHONE.ContactPointTelephoneValue = rmBorrower.LoanContact.HomePhone;
                    homeContact.CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL()
                    {
                        ContactPointRoleType = Convert.ToString(ContactPointRoleBase.Home)
                    };

                    contactPoints.Add(homeContact);
                }
                #endregion

                #region Borrower's Mobile Phone
                if (!string.IsNullOrEmpty(rmBorrower.LoanContact.CellPhone))
                {
                    CONTACT_POINT homeContact = new CONTACT_POINT();
                    homeContact.CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE();
                    homeContact.CONTACT_POINT_TELEPHONE.ContactPointTelephoneValue = rmBorrower.LoanContact.CellPhone;
                    homeContact.CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL()
                    {
                        ContactPointRoleType = Convert.ToString(ContactPointRoleBase.Mobile)
                    };

                    contactPoints.Add(homeContact);
                }
                #endregion

                #region Borrower's Work Phone

                if (!string.IsNullOrEmpty(rmBorrower.LoanContact.WorkPhone))
                {
                    CONTACT_POINT workContact = new CONTACT_POINT();
                    workContact.CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE();
                    workContact.CONTACT_POINT_TELEPHONE.ContactPointTelephoneValue = $"{rmBorrower.LoanContact.WorkPhone}-{rmBorrower.LoanContact.WorkPhoneExt}";
                    workContact.CONTACT_POINT_DETAIL = new CONTACT_POINT_DETAIL()
                    {
                        ContactPointRoleType = Convert.ToString(ContactPointRoleBase.Work)
                    };


                    contactPoints.Add(workContact);
                }
                #endregion

                individual.CONTACT_POINTS = borrowerContactPoints;
                #endregion

                #region Borrower's Name
                NAME borrowerName = new NAME();
                borrowerName.FirstName = rmBorrower.LoanContact.FirstName;
                borrowerName.LastName = rmBorrower.LoanContact.LastName;
                borrowerName.MiddleName = rmBorrower.LoanContact.MiddleName;
                borrowerName.SuffixName = rmBorrower.LoanContact.Suffix;
                individual.NAME = borrowerName;
                #endregion

                borrowerToAdd.INDIVIDUAL = individual;
                #endregion

                #region Roles Data
                ROLES roles = new ROLES();
                List<ROLE> borrowerRoles = new List<ROLE>();
                ROLE roleToAdd = new ROLE();
                roleToAdd.SequenceNumber = 1;
                if ((borrowerIndex % 2) == 0)
                {
                    relationships.Add(new MismoRelationShipModel()
                    {
                        From = $"BORROWER_{borrowerIndex}",
                        To = $"BORROWER_{borrowerIndex - 1}"
                    });
                }
                roleToAdd.Label = borrowerLabel;
                roleToAdd.ROLE_DETAIL = new ROLE_DETAIL()
                {
                    PartyRoleType = Convert.ToString(PartyRoleBase.Borrower)
                };

                BORROWER borrowerData = new BORROWER();


                borrowerData.EMPLOYERS = new EMPLOYERS();
                List<EMPLOYER> borrowerEmployers = new List<EMPLOYER>();
                borrowerData.EMPLOYERS.EMPLOYER = borrowerEmployers;

                #region Borrower's Detail Data
                borrowerData.BORROWER_DETAIL = this.GetBorrowerDetail(rmBorrower);
                #endregion

                #region Current Incomes

                CURRENT_INCOME currentIncomeData = new CURRENT_INCOME();
                CURRENT_INCOME_ITEMS currentIncomeItemsData = new CURRENT_INCOME_ITEMS();
                List<CURRENT_INCOME_ITEM> borrowerCurrentIncomeItems = new List<CURRENT_INCOME_ITEM>();

                var currentEmploymentInfos = rmBorrower.EmploymentInfoes?.Where(ei => ei.EndDate == null).OrderByDescending(ei => ei.StartDate).ThenBy(emp => emp.JobTypeId).ToList();
                var previousEmploymentInfos = rmBorrower.EmploymentInfoes?.Where(ei => ei.EndDate != null).ToList();

                //if (rmBorrower.OtherIncomes != null)
                //{
                //    foreach (var oi in rmBorrower.OtherIncomes)
                //    {
                //        IncomeBase? incomeType = null;

                //        CURRENT_INCOME_ITEM otherIncomeItem = new CURRENT_INCOME_ITEM()
                //        {
                //            SequenceNumber = borrowerCurrentIncomeItems.Count() + 1,
                //            Label = $"CURRENT_INCOME_ITEM_{++totalCurrentIncomesCount}",
                //        };
                //        if (Enum.IsDefined(typeof(IncomeBase),
                //                           (IncomeBase)oi.IncomeTypeId))
                //        {

                //        }
                //    }
                //}

                if (currentEmploymentInfos != null)
                {

                    foreach (var employmentInfo in currentEmploymentInfos)
                    {
                        string employerLabel = $"EMPLOYER_{++totalEmployerCount}";
                        int index = currentEmploymentInfos.IndexOf(employmentInfo);

                        EmploymentClassificationBase? employerType = null;

                        int arrayIndex = currentEmploymentInfos.IndexOf(employmentInfo);
                        //if (arrayIndex == (currentEmploymentInfos.Count - 1))
                        if (arrayIndex == 0)
                        {
                            employerType = EmploymentClassificationBase.Primary;
                        }
                        else
                        {
                            employerType = EmploymentClassificationBase.Secondary;
                        }

                        #region Borrower's Base Income

                        //if (employerType == EmploymentClassificationBase.Secondary)
                        {
                            CURRENT_INCOME_ITEM incomeItemToAdd = new CURRENT_INCOME_ITEM()
                            {
                                SequenceNumber = currentEmploymentInfos.IndexOf(employmentInfo) + 1,
                                Label = $"CURRENT_INCOME_ITEM_{++totalCurrentIncomesCount}"
                            };
                            CURRENT_INCOME_ITEM_DETAIL incomeItemDetail = new CURRENT_INCOME_ITEM_DETAIL()
                            {
                                CurrentIncomeMonthlyTotalAmount = employmentInfo.MonthlyBaseIncome,
                                IncomeType = Convert.ToString(IncomeBase.Base),
                                EmploymentIncomeIndicator = true,
                                //SeasonalIncomeIndicator = employmentInfo.JobTypeId == 3 // 3: SEASONAL
                            };

                            incomeItemToAdd.CURRENT_INCOME_ITEM_DETAIL = incomeItemDetail;
                            borrowerCurrentIncomeItems.Add(incomeItemToAdd);
                            relationships.Add(new MismoRelationShipModel()
                            {
                                From = incomeItemToAdd.Label,
                                To = employerLabel
                            });
                        }

                        #endregion

                        #region Borrower's Other Incomes
                        if (employmentInfo.OtherEmploymentIncomes != null)
                        {
                            var totalBonus = employmentInfo.OtherEmploymentIncomes.Where(x => x.OtherIncomeTypeId == ((byte)OtherEmploymentIncomes.Bonus)).Select(mo => mo.MonthlyIncome).FirstOrDefault();
                            var totalComission = employmentInfo.OtherEmploymentIncomes.Where(x => x.OtherIncomeTypeId == ((byte)OtherEmploymentIncomes.Commissions)).Select(mo => mo.MonthlyIncome).FirstOrDefault();
                            var totalOverTime = employmentInfo.OtherEmploymentIncomes.Where(x => x.OtherIncomeTypeId == ((byte)OtherEmploymentIncomes.Overtime)).Select(mo => mo.MonthlyIncome).FirstOrDefault();

                            #region Bonus Income
                            if (totalBonus.HasValue)
                            {
                                //currentIncomeItemIndex = borrowerCurrentIncomeItems.Count() + 1;
                                CURRENT_INCOME_ITEM incomeItem = new CURRENT_INCOME_ITEM()
                                {
                                    SequenceNumber = borrowerCurrentIncomeItems.Count() + 1,
                                    Label = $"CURRENT_INCOME_ITEM_{++totalCurrentIncomesCount}",
                                    CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                    {
                                        //IncomeType = Convert.ToString(IncomeBase.Bonus),
                                        IncomeType = "Bonus",
                                        CurrentIncomeMonthlyTotalAmount = totalBonus,
                                        EmploymentIncomeIndicator = true,
                                        SeasonalIncomeIndicator = employmentInfo.JobTypeId == 3 // 3: SEASONAL
                                    }
                                };
                                borrowerCurrentIncomeItems.Add(incomeItem);
                                relationships.Add(new MismoRelationShipModel()
                                {
                                    From = incomeItem.Label,
                                    To = employerLabel
                                });
                            }
                            #endregion

                            #region Commission Income
                            if (totalComission.HasValue)
                            {
                                //currentIncomeItemIndex = borrowerCurrentIncomeItems.Count() + 1;
                                CURRENT_INCOME_ITEM commissionItem = new CURRENT_INCOME_ITEM()
                                {
                                    SequenceNumber = borrowerCurrentIncomeItems.Count() + 1,
                                    Label = $"CURRENT_INCOME_ITEM_{++totalCurrentIncomesCount}",
                                    CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                    {
                                        //IncomeType = Convert.ToString(IncomeBase.Commissions),
                                        IncomeType = "Commissions",
                                        CurrentIncomeMonthlyTotalAmount = totalComission,
                                        EmploymentIncomeIndicator = true,
                                        SeasonalIncomeIndicator = employmentInfo.JobTypeId == 3 // 3: SEASONAL
                                    }
                                };
                                borrowerCurrentIncomeItems.Add(commissionItem);
                                relationships.Add(new MismoRelationShipModel()
                                {
                                    From = commissionItem.Label,
                                    To = employerLabel
                                });
                            }
                            #endregion

                            #region Overtime Income
                            if (totalOverTime.HasValue)
                            {
                                //currentIncomeItemIndex = borrowerCurrentIncomeItems.Count() + 1;
                                CURRENT_INCOME_ITEM overtimeIncome = new CURRENT_INCOME_ITEM()
                                {
                                    SequenceNumber = borrowerCurrentIncomeItems.Count() + 1,
                                    Label = $"CURRENT_INCOME_ITEM_{++totalCurrentIncomesCount}",
                                    CURRENT_INCOME_ITEM_DETAIL = new CURRENT_INCOME_ITEM_DETAIL()
                                    {
                                        //IncomeType = Convert.ToString(IncomeBase.Overtime),
                                        IncomeType = "Overtime",
                                        CurrentIncomeMonthlyTotalAmount = totalOverTime,
                                        EmploymentIncomeIndicator = true,
                                        SeasonalIncomeIndicator = employmentInfo.JobTypeId == 3 // 3: SEASONAL
                                    }
                                };
                                borrowerCurrentIncomeItems.Add(overtimeIncome);
                                relationships.Add(new MismoRelationShipModel()
                                {
                                    From = overtimeIncome.Label,
                                    To = employerLabel
                                });
                            }
                            #endregion
                        }
                        #endregion

                        #region Borrower's Employer Info
                        EMPLOYER employerToAdd = new EMPLOYER()
                        {
                            SequenceNumber = 1,
                            Label = employerLabel
                        };
                        employerToAdd.LEGAL_ENTITY = new LEGAL_ENTITY();

                        #region Employer Contact Phone
                        if (!string.IsNullOrEmpty(employmentInfo.Phone))
                        {
                            employerToAdd.LEGAL_ENTITY.CONTACTS = new CONTACTS();
                            employerToAdd.LEGAL_ENTITY.CONTACTS.CONTACT = new CONTACT();
                            employerToAdd.LEGAL_ENTITY.CONTACTS.CONTACT.CONTACT_POINTS = new CONTACT_POINTS();
                            CONTACT_POINT phoneContactPoint = new CONTACT_POINT()
                            {
                                CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE()
                                {
                                    ContactPointTelephoneValue = employmentInfo.Phone
                                }
                            };

                            employerToAdd.LEGAL_ENTITY.CONTACTS.CONTACT.CONTACT_POINTS.CONTACT_POINT = new List<CONTACT_POINT>() { phoneContactPoint };
                        }
                        #endregion

                        employerToAdd.LEGAL_ENTITY.LEGAL_ENTITY_DETAIL = new LEGAL_ENTITY_DETAIL()
                        {
                            FullName = employmentInfo.Name
                        };

                        #region Employer Address
                        employerToAdd.ADDRESS = this.GetMismoAddress(employmentInfo.AddressInfo);

                        #endregion

                        employerToAdd.EMPLOYMENT = new EMPLOYMENT();
                        employerToAdd.EMPLOYMENT.EmploymentBorrowerSelfEmployedIndicator = employmentInfo.IsSelfEmployed == true;
                        //EmploymentClassificationBase? employerType = null;
                        ////switch (employmentInfo.JobTypeId)
                        ////{
                        ////    case 1: // Full-Time
                        ////        employerType = EmploymentClassificationBase.Primary;
                        ////        break;
                        ////    case 2: // Part-Time
                        ////        employerType = EmploymentClassificationBase.Secondary;
                        ////        break;
                        ////}

                        //int arrayIndex = currentEmploymentInfos.IndexOf(employmentInfo);
                        //if (arrayIndex == (currentEmploymentInfos.Count - 1))
                        //{
                        //    employerType = EmploymentClassificationBase.Primary;
                        //}
                        //else
                        //{
                        //    employerType = EmploymentClassificationBase.Secondary;
                        //}

                        //var classificationBase = index == 0 ? EmploymentClassificationBase.Primary : EmploymentClassificationBase.Secondary;
                        if (employerType != null)
                        {
                            employerToAdd.EMPLOYMENT.EmploymentClassificationType = Convert.ToString(employerType);
                        }
                        //employerToAdd.EMPLOYMENT.EmploymentMonthlyIncomeAmount = employmentInfo.MonthlyBaseIncome;
                        employerToAdd.EMPLOYMENT.EmploymentPositionDescription = employmentInfo.JobTitle;
                        if (employmentInfo.StartDate.HasValue)
                        {
                            employerToAdd.EMPLOYMENT.EmploymentStartDate = employmentInfo.StartDate.Value.ToString("yyyy-MM-dd");
                        }

                        employerToAdd.EMPLOYMENT.EmploymentStatusType = Convert.ToString(EmploymentStatusBase.Current);

                        //var toDate = employmentInfo.EndDate ?? rmBorrower.LoanApplication?.CreatedOnUtc ?? DateTime.UtcNow;
                        var toDate = loanApplication.CreatedOnUtc ?? DateTime.UtcNow;

                        employerToAdd.EMPLOYMENT.EmploymentTimeInLineOfWorkYearsCount = (decimal?)Math.Round(Math.Abs((employmentInfo.StartDate.Value - toDate).TotalDays / 365), 2);
                        employerToAdd.EMPLOYMENT.EmploymentTimeInLineOfWorkMonthsCount = (decimal?)Math.Round(Math.Abs((employmentInfo.StartDate.Value - toDate).TotalDays / 365), 2);
                        employerToAdd.EMPLOYMENT.EmploymentTimeInLineOfWorkMonthsCount = ((toDate.Year - employmentInfo.StartDate.Value.Year) * 12) + (toDate.Month - employmentInfo.StartDate.Value.Month);
                        if ((employmentInfo.IsSelfEmployed == true) && employmentInfo.OwnershipPercentage != null)
                        {
                            if (employmentInfo.OwnershipPercentage >= 25)
                            {
                                employerToAdd.EMPLOYMENT.OwnershipInterestType = Convert.ToString(OwnershipInterestBase.GreaterThanOrEqualTo25Percent);
                            }
                            else
                            {
                                employerToAdd.EMPLOYMENT.OwnershipInterestType = Convert.ToString(OwnershipInterestBase.LessThan25Percent);
                            }
                        }

                        employerToAdd.EMPLOYMENT.SpecialBorrowerEmployerRelationshipIndicator = employmentInfo.IsEmployedByPartyInTransaction == true;

                        #endregion


                        borrowerEmployers.Add(employerToAdd);
                    }
                }

                currentIncomeItemsData.CURRENT_INCOME_ITEM = borrowerCurrentIncomeItems;
                currentIncomeData.CURRENT_INCOME_ITEMS = currentIncomeItemsData;
                borrowerData.CURRENT_INCOME = currentIncomeData;
                #endregion

                #region Previous Employers
                if (previousEmploymentInfos != null)
                {

                    foreach (var employmentInfo in previousEmploymentInfos)
                    {
                        #region Borrower's Employer Info
                        EMPLOYER employerToAdd = new EMPLOYER()
                        {
                            SequenceNumber = previousEmploymentInfos.IndexOf(employmentInfo) + 1,
                            Label = $"EMPLOYER_{++totalEmployerCount}"
                        };
                        employerToAdd.LEGAL_ENTITY = new LEGAL_ENTITY();

                        #region Employer Contact Phone
                        if (!string.IsNullOrEmpty(employmentInfo.Phone))
                        {
                            employerToAdd.LEGAL_ENTITY.CONTACTS = new CONTACTS();
                            employerToAdd.LEGAL_ENTITY.CONTACTS.CONTACT = new CONTACT();
                            employerToAdd.LEGAL_ENTITY.CONTACTS.CONTACT.CONTACT_POINTS = new CONTACT_POINTS();
                            CONTACT_POINT phoneContactPoint = new CONTACT_POINT()
                            {
                                CONTACT_POINT_TELEPHONE = new CONTACT_POINT_TELEPHONE()
                                {
                                    ContactPointTelephoneValue = employmentInfo.Phone
                                }
                            };

                            employerToAdd.LEGAL_ENTITY.CONTACTS.CONTACT.CONTACT_POINTS.CONTACT_POINT = new List<CONTACT_POINT>() { phoneContactPoint };
                        }
                        #endregion

                        employerToAdd.LEGAL_ENTITY.LEGAL_ENTITY_DETAIL = new LEGAL_ENTITY_DETAIL()
                        {
                            FullName = employmentInfo.Name
                        };

                        #region Employer Address
                        employerToAdd.ADDRESS = this.GetMismoAddress(employmentInfo.AddressInfo);
                        #endregion

                        employerToAdd.EMPLOYMENT = new EMPLOYMENT();
                        employerToAdd.EMPLOYMENT.EmploymentBorrowerSelfEmployedIndicator = employmentInfo.IsSelfEmployed == true;
                        //var classificationType = index == 0 ? EmploymentClassificationBase.Primary : EmploymentClassificationBase.Secondary;
                        //employerToAdd.EMPLOYMENT.EmploymentClassificationType = Convert.ToString(classificationType);
                        employerToAdd.EMPLOYMENT.EmploymentMonthlyIncomeAmount = employmentInfo.MonthlyBaseIncome;
                        employerToAdd.EMPLOYMENT.EmploymentPositionDescription = employmentInfo.JobTitle;
                        if (employmentInfo.StartDate.HasValue)
                        {
                            employerToAdd.EMPLOYMENT.EmploymentStartDate = employmentInfo.StartDate.Value.ToString("yyyy-MM-dd");
                        }

                        if (employmentInfo.EndDate.HasValue)
                        {
                            employerToAdd.EMPLOYMENT.EmploymentEndDate = employmentInfo.EndDate.Value.ToString("yyyy-MM-dd");
                        }

                        employerToAdd.EMPLOYMENT.EmploymentStatusType = Convert.ToString(EmploymentStatusBase.Previous);

                        var toDate = employmentInfo.EndDate ?? rmBorrower.LoanApplication?.CreatedOnUtc ?? DateTime.UtcNow;

                        employerToAdd.EMPLOYMENT.EmploymentTimeInLineOfWorkYearsCount = (decimal?)Math.Abs((employmentInfo.StartDate.Value - toDate).TotalDays / 365);
                        employerToAdd.EMPLOYMENT.EmploymentTimeInLineOfWorkMonthsCount = 0;
                        if (employmentInfo.OwnershipPercentage != null)
                        {
                            if (employmentInfo.OwnershipPercentage >= 25)
                            {
                                employerToAdd.EMPLOYMENT.OwnershipInterestType = Convert.ToString(OwnershipInterestBase.GreaterThanOrEqualTo25Percent);
                            }
                            else
                            {
                                employerToAdd.EMPLOYMENT.OwnershipInterestType = Convert.ToString(OwnershipInterestBase.LessThan25Percent);
                            }
                        }

                        #endregion


                        borrowerEmployers.Add(employerToAdd);
                    }
                }
                #endregion

                #region Borrower's Declaration
                DECLARATION declaration = new DECLARATION();
                DECLARATION_DETAIL declarationDetail = new DECLARATION_DETAIL();

                if (rmBorrower.BorrowerQuestionResponses != null)
                {
                    var LoanForeclosureOrJudgementIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 36)?.QuestionResponse.AnswerText;
                    var BankruptcyIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 37)?.QuestionResponse.AnswerText;
                    var PropertyForeclosedPastSevenYearsIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 38)?.QuestionResponse.AnswerText;
                    var PartyToLawsuitIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 39)?.QuestionResponse.AnswerText;
                    var OutstandingJudgementsIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 40)?.QuestionResponse.AnswerText;
                    var PresentlyDelinquentIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 41)?.QuestionResponse.AnswerText;
                    var BorrowedDownPaymentIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 42)?.QuestionResponse.AnswerText;
                    //var CoMakerEndorserOfNoteIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 43)?.QuestionResponse.AnswerText;
                    var UndisclosedComakerOfNoteIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 43)?.QuestionResponse.AnswerText;
                    var DeclarationsJIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 44)?.QuestionResponse.AnswerText;
                    var IntentToOccupyIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 45)?.QuestionResponse.AnswerText;
                    var AlimonyChildSupportObligationIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 46)?.QuestionResponse.AnswerText;
                    var HomeownerPastThreeYearsIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 47)?.QuestionResponse.AnswerText;
                    var PriorPropertyUsageType = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 49)?.QuestionResponse.AnswerText;
                    var PriorPropertyTitleType = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 50)?.QuestionResponse.AnswerText;
                    var DeclarationsKIndicator = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 54)?.QuestionResponse.AnswerText;


                    declarationDetail.AlimonyChildSupportObligationIndicator = AlimonyChildSupportObligationIndicator == "1";
                    declarationDetail.BankruptcyIndicator = BankruptcyIndicator == "1";
                    int isUsCitizen = (DeclarationsJIndicator == "1" || rmBorrower.LoanContact.ResidencyStateId == (int)CitizenshipResidencyBase.USCitizen) ? 1 : 0; // US Citizen
                    if (isUsCitizen == 1)
                    {
                        declarationDetail.CitizenshipResidencyType = Convert.ToString(CitizenshipResidencyBase.USCitizen);
                    }
                    else
                    {
                        if (DeclarationsKIndicator == "1")
                        {
                            declarationDetail.CitizenshipResidencyType = Convert.ToString(CitizenshipResidencyBase.PermanentResidentAlien);
                        }
                        else
                        {
                            var VisaResponse = rmBorrower.BorrowerQuestionResponses.SingleOrDefault(bqr => bqr.QuestionId == 57)?.QuestionResponse?.AnswerText;
                            if (string.IsNullOrEmpty(VisaResponse))
                            {
                                declarationDetail.CitizenshipResidencyType = Convert.ToString(CitizenshipResidencyBase.NonResidentAlien);
                            }
                            else
                            {
                                if (VisaResponse.Trim().Equals("3") // Valid work VISA (H1, L1 etc.)
                                    || VisaResponse.Trim().Equals("4") // Temporary workers (H -2A)
                                )
                                {
                                    declarationDetail.CitizenshipResidencyType = Convert.ToString(CitizenshipResidencyBase.NonPermanentResidentAlien);
                                }
                                else
                                {
                                    declarationDetail.CitizenshipResidencyType = Convert.ToString(CitizenshipResidencyBase.NonResidentAlien);
                                }
                            }
                        }
                        //if (Enum.IsDefined(typeof(CitizenshipResidencyBase),
                        //                   (CitizenshipResidencyBase)rmBorrower.LoanContact.ResidencyStateId))
                        //{
                        //    declarationDetail.CitizenshipResidencyType = Convert.ToString((CitizenshipResidencyBase)rmBorrower.LoanContact.ResidencyStateId);
                        //}
                        //else
                        //{
                        //    declarationDetail.CitizenshipResidencyType = Convert.ToString(CitizenshipResidencyBase.Unknown);
                        //}
                    }

                    declarationDetail.HomeownerPastThreeYearsType = HomeownerPastThreeYearsIndicator == "1" ? "Yes" : "No";
                    //declarationDetail.CoMakerEndorserOfNoteIndicator = CoMakerEndorserOfNoteIndicator == "1";
                    declarationDetail.IntentToOccupyType = IntentToOccupyIndicator == "1" ? "Yes" : "No";
                    declarationDetail.LoanForeclosureOrJudgmentIndicator = LoanForeclosureOrJudgementIndicator == "1";
                    declarationDetail.OutstandingJudgmentsIndicator = OutstandingJudgementsIndicator == "1";
                    declarationDetail.PartyToLawsuitIndicator = PartyToLawsuitIndicator == "1";
                    declarationDetail.PresentlyDelinquentIndicator = PresentlyDelinquentIndicator == "1";
                    declarationDetail.PriorPropertyForeclosureCompletedIndicator = PropertyForeclosedPastSevenYearsIndicator == "1";// TODO
                    if (!string.IsNullOrEmpty(PriorPropertyTitleType))
                    {
                        if (Enum.IsDefined(typeof(PriorPropertyTitleBase),
                                           (PriorPropertyTitleBase)Convert.ToByte(PriorPropertyTitleType)))
                        {
                            declarationDetail.PriorPropertyTitleType = Convert.ToString((PriorPropertyTitleBase)Convert.ToByte(PriorPropertyTitleType)); // TODO
                        }

                    }

                    if (!string.IsNullOrEmpty((PriorPropertyUsageType)))
                    {
                        byte usageType = 0;
                        if (byte.TryParse(PriorPropertyUsageType,
                                          out usageType))
                        {
                            if (Enum.IsDefined(typeof(PriorPropertyUsageBase),
                                               (PriorPropertyUsageBase)usageType))
                            {
                                declarationDetail.PriorPropertyUsageType = Convert.ToString((PriorPropertyUsageBase)usageType);
                            }
                        }
                    }
                    else
                    {
                        declarationDetail.PriorPropertyUsageType = Convert.ToString(PriorPropertyUsageBase.PrimaryResidence);
                    }



                    declarationDetail.UndisclosedBorrowedFundsIndicator = BorrowedDownPaymentIndicator == "1";
                    var borrowerAsset = rmBorrower.AssetBorrowerBinders?.FirstOrDefault(ab => ab.BorrowerAsset?.AssetTypeId == 11)?.BorrowerAsset;
                    if (borrowerAsset != null)
                    {
                        declarationDetail.UndisclosedBorrowedFundsAmount = borrowerAsset.UseForDownpayment;
                    }
                    //.BorrowerAsset.Value

                    declarationDetail.UndisclosedComakerOfNoteIndicator = UndisclosedComakerOfNoteIndicator == "1";
                }

                declaration.DECLARATION_DETAIL = declarationDetail;
                borrowerData.DECLARATION = declaration;
                #endregion

                #region Borrower's Dependents Age
                if (!string.IsNullOrEmpty(rmBorrower.DependentAge))
                {
                    var ages = rmBorrower.DependentAge.Split(',');
                    int dependentIndex = 1;
                    borrowerData.DEPENDENTS = new DEPENDENTS();
                    List<DEPENDENT> dependentList = new List<DEPENDENT>();
                    borrowerData.DEPENDENTS.DEPENDENT = dependentList;
                    foreach (var age in ages)
                    {
                        if (!string.IsNullOrEmpty(age))
                        {
                            dependentList.Add(new DEPENDENT()
                            {
                                SequenceNumber = dependentIndex++,
                                DependentAgeYearsCount = age.Trim()
                            });
                        }
                    }
                }
                #endregion

                if (rmBorrower.OtherIncomes != null)
                {
                    foreach (var income in rmBorrower.OtherIncomes)
                    {
                        CURRENT_INCOME_ITEM otherIncomeItem = new CURRENT_INCOME_ITEM()
                        {
                            Label = $"CURRENT_INCOME_ITEM_{++totalCurrentIncomesCount}"
                        };
                        CURRENT_INCOME_ITEM_DETAIL incomeItemDetail = new CURRENT_INCOME_ITEM_DETAIL()
                        {
                            CurrentIncomeMonthlyTotalAmount = income.MonthlyAmount,
                            EmploymentIncomeIndicator = false
                        };
                        otherIncomeItem.CURRENT_INCOME_ITEM_DETAIL = incomeItemDetail;
                        borrowerCurrentIncomeItems.Add(otherIncomeItem);
                        if (Enum.IsDefined(typeof(IncomeBase),
                                           (IncomeBase)income.IncomeTypeId))
                        {
                            incomeItemDetail.IncomeType = Convert.ToString(((IncomeBase)income.IncomeTypeId));
                        }
                        else
                        {
                            incomeItemDetail.IncomeType = Convert.ToString((IncomeBase.Other));
                            switch (income.IncomeTypeId)
                            {
                                case 25: // Seasonal Income
                                    incomeItemDetail.IncomeTypeOtherDescription = "Seasonal Income";
                                    break;
                            }
                        }
                    }
                }

                borrowerData.GOVERNMENT_MONITORING = this.GetGovernmentMonitoring(rmBorrower);
                roleToAdd.BORROWER = borrowerData;

                roleToAdd.BORROWER.RESIDENCES = new RESIDENCES();
                roleToAdd.BORROWER.RESIDENCES.RESIDENCE = this.GetBorrowerResidences(rmBorrower);


                borrowerRoles.Add(roleToAdd);
                //roles.ROLE = borrowerRoles;
                roles.ROLE = roleToAdd;
                borrowerToAdd.ROLES = roles;
                #endregion


                borrowers.Add(borrowerToAdd);
            }

            return borrowers;
        }

        public BORROWER_DETAIL GetBorrowerDetail(Borrower rmBorrower)
        {
            BORROWER_DETAIL borrowerDetailData = new BORROWER_DETAIL();
            if (rmBorrower != null)
            {
                borrowerDetailData.CommunityPropertyStateResidentIndicator = false; // TODO
                borrowerDetailData.SelfDeclaredMilitaryServiceIndicator = false;
                borrowerDetailData.DependentCount = rmBorrower.NoOfDependent;
                if (rmBorrower.LoanContact.MaritalStatusId.HasValue)
                {
                    if (Enum.IsDefined(typeof(MaritalStatusBase),
                                       (MaritalStatusBase)rmBorrower.LoanContact.MaritalStatusId))
                    {
                        borrowerDetailData.MaritalStatusType = Convert.ToString((MaritalStatusBase)rmBorrower.LoanContact.MaritalStatusId);
                    }
                }
            }
            return borrowerDetailData;
        }

        public GOVERNMENT_MONITORING GetGovernmentMonitoring(Borrower rmBorrower)
        {
            GOVERNMENT_MONITORING monitoring = new GOVERNMENT_MONITORING();
            monitoring.GOVERNMENT_MONITORING_DETAIL = new GOVERNMENT_MONITORING_DETAIL();

            monitoring.GOVERNMENT_MONITORING_DETAIL.HMDAEthnicityCollectedBasedOnVisualObservationOrSurnameIndicator = false; // TODO
            monitoring.GOVERNMENT_MONITORING_DETAIL.HMDAEthnicityRefusalIndicator = rmBorrower.LoanContact.LoanContactEthnicityBinders?.Any(b => b.EthnicityId == 3); // TODO; // TODO
            monitoring.GOVERNMENT_MONITORING_DETAIL.HMDAGenderCollectedBasedOnVisualObservationOrNameIndicator = false; // TODO
            monitoring.GOVERNMENT_MONITORING_DETAIL.HMDAGenderRefusalIndicator = rmBorrower.LoanContact.GenderId == 3; // TODO
            monitoring.GOVERNMENT_MONITORING_DETAIL.HMDARaceCollectedBasedOnVisualObservationOrSurnameIndicator = false; // TODO
            monitoring.GOVERNMENT_MONITORING_DETAIL.HMDARaceRefusalIndicator = rmBorrower.LoanContact.LoanContactRaceBinders?.Any(b => b.RaceId == 6); // TODO

            //rmBorrower.LoanContact.gen
            if (rmBorrower.LoanContact.GenderId.HasValue && Enum.IsDefined(typeof(GenderBase),
                                                    (GenderBase)rmBorrower.LoanContact.GenderId))
            {
                monitoring.GOVERNMENT_MONITORING_DETAIL.EXTENSION = new EXTENSION();
                monitoring.GOVERNMENT_MONITORING_DETAIL.EXTENSION.OTHER = new OTHER();
                monitoring.GOVERNMENT_MONITORING_DETAIL.EXTENSION.OTHER.GOVERNMENT_MONITORING_DETAIL_EXTENSION = new GOVERNMENT_MONITORING_DETAIL_EXTENSION();
                monitoring.GOVERNMENT_MONITORING_DETAIL.EXTENSION.OTHER.GOVERNMENT_MONITORING_DETAIL_EXTENSION.ApplicationTakenMethodType = Convert.ToString(ApplicationTakenMethodBase.Internet);
                monitoring.GOVERNMENT_MONITORING_DETAIL.EXTENSION.OTHER.GOVERNMENT_MONITORING_DETAIL_EXTENSION.HMDAGenderType = Convert.ToString((GenderBase)rmBorrower.LoanContact.GenderId);
            }

            monitoring.HMDA_ETHNICITY_ORIGINS = new HMDA_ETHNICITY_ORIGINS();
            monitoring.HMDA_ETHNICITY_ORIGINS.HMDA_ETHNICITY_ORIGIN = new List<HMDA_ETHNICITY_ORIGIN>();

            monitoring.EXTENSION = new EXTENSION();
            monitoring.EXTENSION.OTHER = new OTHER();
            monitoring.EXTENSION.OTHER.GOVERNMENT_MONITORING_EXTENSION = new GOVERNMENT_MONITORING_EXTENSION();
            monitoring.EXTENSION.OTHER.GOVERNMENT_MONITORING_EXTENSION.HMDA_ETHNICITIES = new HMDA_ETHNICITIES();
            monitoring.EXTENSION.OTHER.GOVERNMENT_MONITORING_EXTENSION.HMDA_ETHNICITIES.HMDA_ETHNICITY = new List<HMDA_ETHNICITY>();

            if (rmBorrower.LoanContact.LoanContactEthnicityBinders != null)
            {
                foreach (var ethnicityBinder in rmBorrower.LoanContact.LoanContactEthnicityBinders)
                {
                    if (Enum.IsDefined(typeof(HMDAEthnicityBase),
                                       (HMDAEthnicityBase)ethnicityBinder.EthnicityId))
                    {
                        monitoring.EXTENSION.OTHER.GOVERNMENT_MONITORING_EXTENSION.HMDA_ETHNICITIES.HMDA_ETHNICITY.Add(new HMDA_ETHNICITY()
                        {
                            HMDAEthnicityType = Convert.ToString((HMDAEthnicityBase)ethnicityBinder.EthnicityId)
                        });
                    }

                    if (ethnicityBinder.Ethnicity != null && ethnicityBinder.EthnicityDetail != null)
                    {
                        monitoring.HMDA_ETHNICITY_ORIGINS.HMDA_ETHNICITY_ORIGIN.Add(new HMDA_ETHNICITY_ORIGIN()
                        {
                            HMDAEthnicityOriginType = Convert.ToString((HMDAEthnicityOriginBase)ethnicityBinder.EthnicityDetail.Id)
                        });
                    }

                    //if (ethnicityBinder.Ethnicity != null && ethnicityBinder.Ethnicity.EthnicityDetails != null)
                    //{
                    //    int detailIndex = 1;
                    //    foreach (var ethDetail in ethnicityBinder.Ethnicity.EthnicityDetails)
                    //    {
                    //        if (Enum.IsDefined(typeof(HMDAEthnicityOriginBase),
                    //                           (HMDAEthnicityOriginBase)ethDetail.Id))
                    //        {
                    //            monitoring.HMDA_ETHNICITY_ORIGINS.HMDA_ETHNICITY_ORIGIN.Add(new HMDA_ETHNICITY_ORIGIN()
                    //            {
                    //                SequenceNumber = detailIndex++,
                    //                HMDAEthnicityOriginType = Convert.ToString((HMDAEthnicityOriginBase)ethDetail.Id)
                    //            });
                    //        }
                    //    }
                    //}
                }
            }





            var races = rmBorrower.LoanContact.LoanContactRaceBinders?.Select(binder => binder.Race).DistinctBy(r => r.Id).ToList();



            var hmdaRaces = new HMDA_RACES();
            hmdaRaces.HMDA_RACE = new List<HMDA_RACE>();

            if (races != null)
            {
                foreach (var race in races)
                {
                    var hmdaRace = new HMDA_RACE();
                    hmdaRace.HMDA_RACE_DETAIL = new HMDA_RACE_DETAIL();
                    hmdaRace.HMDA_RACE_DESIGNATIONS = new HMDA_RACE_DESIGNATIONS();
                    hmdaRace.HMDA_RACE_DETAIL.HMDARaceType = race.Name.Replace(" ",
                                                                               "");

                    var raceDetails = rmBorrower.LoanContact.LoanContactRaceBinders.Where(binder => binder.RaceDetail != null && binder.RaceDetail.RaceId == race.Id).Select(selector: b => b.RaceDetail).ToList();
                    hmdaRace.HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION = new List<HMDA_RACE_DESIGNATION>();

                    foreach (var raceDetail in raceDetails)
                    {
                        var hmdaRaceDesignation = new HMDA_RACE_DESIGNATION();
                        hmdaRaceDesignation.EXTENSION = new EXTENSION();
                        hmdaRaceDesignation.EXTENSION.OTHER = new OTHER();
                        hmdaRaceDesignation.EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION = new HMDA_RACE_DESIGNATION_EXTENSION();

                        if (Enum.IsDefined(typeof(HMDARaceDesignationBase),
                                           (HMDARaceDesignationBase)raceDetail.Id))
                        {
                            if (raceDetail.Id == 11)
                            {
                                hmdaRaceDesignation.EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION.HMDARaceDesignationType = Convert.ToString("OtherPacificIslander");
                            }
                            else
                            {
                                hmdaRaceDesignation.EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION.HMDARaceDesignationType = Convert.ToString((HMDARaceDesignationBase)raceDetail.Id);
                            }
                        }
                        else
                        {
                            //hmdaRaceDesignation.EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION.HMDARaceDesignationType = Convert.ToString(HMDARaceDesignationBase.Other);
                            hmdaRaceDesignation.EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION.HMDARaceDesignationType = Convert.ToString("OtherPacificIslander");
                        }
                        //hmdaRaceDesignation.EXTENSION.OTHER.HMDA_RACE_DESIGNATION_EXTENSION.HMDARaceDesignationType = raceDetail.Name.Replace(" ",
                        //                                                                                                                      "");

                        hmdaRace.HMDA_RACE_DESIGNATIONS.HMDA_RACE_DESIGNATION.Add(hmdaRaceDesignation);
                    }

                    hmdaRaces.HMDA_RACE.Add(hmdaRace);
                }
            }


            monitoring.HMDA_RACES = hmdaRaces;

            return monitoring;
        }

        public List<RESIDENCE> GetBorrowerResidences(Borrower rmBorrower)
        {
            List<RESIDENCE> residences = new List<RESIDENCE>();
            if (rmBorrower != null)
            {
                if (rmBorrower.BorrowerResidences != null)
                {
                    var rmBorrowerResidences = rmBorrower.BorrowerResidences.OrderByDescending(r => r.FromDate).ToList();
                    var isCurrentResidence = true;
                    foreach (var rmBorrowerResidence in rmBorrowerResidences)
                    {
                        var index = rmBorrowerResidences.IndexOf(rmBorrowerResidence);
                        RESIDENCE residenceToAdd = new RESIDENCE();
                        residences.Add(residenceToAdd);
                        //if (rmBorrowerResidence.LoanAddress != null)
                        {
                            residenceToAdd.ADDRESS = this.GetMismoAddress(rmBorrowerResidence.LoanAddress);
                            if (rmBorrowerResidence.OwnershipTypeId.HasValue)
                            {
                                if (Enum.IsDefined(typeof(BorrowerResidencyBasisBase),
                                                   (BorrowerResidencyBasisBase)rmBorrowerResidence.OwnershipTypeId))
                                {
                                    if (((BorrowerResidencyBasisBase)rmBorrowerResidence.OwnershipTypeId) == BorrowerResidencyBasisBase.Rent)
                                    {
                                        residenceToAdd.LANDLORD = new LANDLORD();
                                        residenceToAdd.LANDLORD.LANDLORD_DETAIL = new LANDLORD_DETAIL()
                                        {
                                            MonthlyRentAmount = rmBorrowerResidence.MonthlyRent
                                        };
                                    }
                                    residenceToAdd.RESIDENCE_DETAIL = new RESIDENCE_DETAIL();
                                    residenceToAdd.RESIDENCE_DETAIL.BorrowerResidencyBasisType = Convert.ToString((BorrowerResidencyBasisBase)rmBorrowerResidence.OwnershipTypeId);
                                }
                                else
                                {
                                    residenceToAdd.RESIDENCE_DETAIL = new RESIDENCE_DETAIL();
                                    residenceToAdd.RESIDENCE_DETAIL.BorrowerResidencyBasisType = Convert.ToString(BorrowerResidencyBasisBase.LivingRentFree);
                                }
                            }

                            if (residenceToAdd.RESIDENCE_DETAIL == null)
                            {
                                residenceToAdd.RESIDENCE_DETAIL = new RESIDENCE_DETAIL();
                            }

                            if (isCurrentResidence)
                            {
                                residenceToAdd.RESIDENCE_DETAIL.BorrowerResidencyType = Convert.ToString(BorrowerResidencyBase.Current);
                                isCurrentResidence = false;
                            }
                            else
                            {
                                residenceToAdd.RESIDENCE_DETAIL.BorrowerResidencyType = Convert.ToString(BorrowerResidencyBase.Prior);
                            }



                            var toDate = rmBorrowerResidence.ToDate == null ? DateTime.Now : rmBorrowerResidence.ToDate.Value;
                            ElapsedTimeSpan elapsedTime = rmBorrowerResidence.FromDate.Value.ElapsedTime(toDate);
                            residenceToAdd.RESIDENCE_DETAIL.BorrowerResidencyDurationMonthsCount = elapsedTime.months;
                            residenceToAdd.RESIDENCE_DETAIL.BorrowerResidencyDurationYearsCount = elapsedTime.years;
                        }
                    }
                }
            }
            return residences;
        }


        public ADDRESS GetMismoAddress(AddressInfo rmAddressInfo)
        {
            ADDRESS mismoAddress = null;
            if (rmAddressInfo != null)
            {
                mismoAddress = new ADDRESS()
                {
                    CityName = rmAddressInfo.CityName,
                    PostalCode = rmAddressInfo.ZipCode,
                    StateCode = rmAddressInfo?.State?.Abbreviation,
                    CountyName = rmAddressInfo.CountyName,
                    //CountryCode = (loanApplication.PropertyInfo.AddressInfo?.Country == null ? "US" : loanApplication.PropertyInfo.AddressInfo?.Country.TwoLetterIsoCode),
                    //CountryName = (loanApplication.PropertyInfo.AddressInfo?.Country == null ? "United States" : loanApplication.PropertyInfo.AddressInfo?.Country.Name),
                    AddressLineText = $"{rmAddressInfo.StreetAddress} {this.GetAddressUnit(rmAddressInfo.UnitNo)}".Trim()
                };
                mismoAddress.CountryName = "United States";
                mismoAddress.CountryCode = "US";
                if (rmAddressInfo.Country != null)
                {
                    mismoAddress.CountryName = rmAddressInfo.Country.Name;
                    mismoAddress.CountryCode = rmAddressInfo.Country.TwoLetterIsoCode;
                }
            }
            return mismoAddress;
        }

        private string GetAddressUnit(string unit)
        {
            if (!string.IsNullOrEmpty(unit))
            {
                unit = $"#{unit}";
            }

            return unit;
        }
    }
}
