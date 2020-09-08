using RainMaker.Common.Extensions;
using RainMaker.Entity.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace RainMaker.Common
{
    public class PlotFeeItem
    {
        public string FeeName { get; set; }
        public double Value { get; set; }
    }

    public class ItemizePrePaidEscrowDetail
    {
        public string Name { get; set; }
        public int MonthDays { get; set; }
        public string Rate { get; set; }
        public decimal Amount { get; set; }
        public int DisplayOrder { get; set; }
    }



    public static class ListExtensions
    {

        public static List<ItemSelectionList> GetListForDropDown<T>(this IList<T> items, Expression<Func<T, object>> display, Expression<Func<T, object>> value,
           Expression<Func<T, object>> isActiveField, Expression<Func<T, object>> concatenateDisplayWith = null) where T : class
        {
            var tempList = new List<ItemSelectionList>();


            foreach (var item in items)
            {

                var displayName = ((LambdaExpression)display).Compile().DynamicInvoke(item);
                if (concatenateDisplayWith != null)
                {
                    var otherDispay = ((LambdaExpression)concatenateDisplayWith).Compile().DynamicInvoke(item);


                    displayName += " (" + otherDispay + ")";
                }



                var tempitem = new ItemSelectionList { Value = ((LambdaExpression)value).Compile().DynamicInvoke(item), Name = displayName };

                if (isActiveField != null && (bool)((LambdaExpression)isActiveField).Compile().DynamicInvoke(item) == false)
                {
                    tempitem.Name += Constants.InActiveSymbol;
                }


                tempList.Add(tempitem);

            }


            return tempList;
        }

        public static List<ItemSelectionList> GetListForDropDownWithoutActive<T>(this IList<T> items, Expression<Func<T, object>> display, Expression<Func<T, object>> value,
           Expression<Func<T, object>> concatenateDisplayWith = null) where T : class
        {
            var tempList = new List<ItemSelectionList>();


            foreach (var item in items)
            {

                var displayName = ((LambdaExpression)display).Compile().DynamicInvoke(item);
                if (concatenateDisplayWith != null)
                {
                    var otherDispay = ((LambdaExpression)concatenateDisplayWith).Compile().DynamicInvoke(item);


                    displayName += " (" + otherDispay + ")";
                }



                var tempitem = new ItemSelectionList { Value = ((LambdaExpression)value).Compile().DynamicInvoke(item), Name = displayName };

                tempList.Add(tempitem);

            }


            return tempList;
        }


    }


    public class ItemSelectionList
    {
        public object Name { get; set; }
        public object Value { get; set; }

        [Obsolete("GetListForDropDown is deprecated, please use extension method GetListForDropDown with lambda expression instead.")]
        public static List<ItemSelectionList> GetListForDropDown1<T>(List<T> items, string display, string value, string activeCheckField, string concatenateDisplayWith = null) where T : class
        {
            var tempList = new List<ItemSelectionList>();
            foreach (var item in items)
            {
                var namepro = typeof(T).GetProperty(display);
                var displayName = namepro.GetValue(item);
                if (!string.IsNullOrEmpty(concatenateDisplayWith))
                {
                    var otherDispay = typeof(T).GetProperty(concatenateDisplayWith).GetValue(item);

                    displayName += " (" + otherDispay + ")";
                }
                var valuepro = typeof(T).GetProperty(value);
                var activePro = typeof(T).GetProperty(activeCheckField);

                var tempitem = new ItemSelectionList { Value = valuepro.GetValue(item), Name = displayName };

                if (activePro != null && (bool)activePro.GetValue(item) == false)
                {
                    tempitem.Name += Constants.InActiveSymbol;
                }


                tempList.Add(tempitem);

            }
            return tempList;
        }
        [Obsolete("GetListForDropDown is deprecated, please use extension method GetListForDropDown with lambda expression instead.")]
        public static List<ItemSelectionList> GetListForDropDown1<T>(List<T> items, string display, string value) where T : class
        {
            var tempList = new List<ItemSelectionList>();
            foreach (var item in items)
            {
                var namepro = typeof(T).GetProperty(display);
                var valuepro = typeof(T).GetProperty(value);

                var tempitem = new ItemSelectionList { Value = valuepro.GetValue(item), Name = namepro.GetValue(item) };

                tempList.Add(tempitem);

            }
            return tempList;
        }

        public static List<ItemSelectionList> GetBooleanList()
        {
            return new List<ItemSelectionList> { new ItemSelectionList { Name = "No", Value = false }, new ItemSelectionList { Name = "Yes", Value = true } };
        }
        public static List<ItemSelectionList> GetQuestionBooleanList()
        {
            return new List<ItemSelectionList> { new ItemSelectionList { Name = "Not Selected", Value = "null" }, new ItemSelectionList { Name = "No", Value = false }, new ItemSelectionList { Name = "Yes", Value = true } };
        }

        public static List<ItemSelectionList> GetPreferredNames()
        {
            //please enter any new record order by name
            return new List<ItemSelectionList> { new ItemSelectionList { Name = "--- Please Select ---", Value = "" },
                new ItemSelectionList { Name = "First Name", Value = (int)PreferredNameType.FirstName },
                new ItemSelectionList { Name = "First Last Name", Value = (int)PreferredNameType.FirstLastName },
                new ItemSelectionList { Name = "Last,First Name", Value = (int)PreferredNameType.LastFirstName },
                new ItemSelectionList { Name = "Last Name", Value = (int)PreferredNameType.LastName },
                new ItemSelectionList { Name = "Nick Name", Value = (int)PreferredNameType.NickName },
                new ItemSelectionList { Name = "Nick Last Name", Value = (int)PreferredNameType.NickNameLastName },
                new ItemSelectionList { Name = "Prefix Last Name", Value = (int)PreferredNameType.PrefixLastName }
            };
        }

        public static LoanGoalData GetLoanDescriptionbyId(int loanGoalId)
        {
            //please enter any new record order by name
            var loanGoalList = GetLoanGoalList();
            return loanGoalList.FirstOrDefault(rec => rec.Value == loanGoalId);
        }
        public static LoanGoalData GetLoanGoalByShortName(string shortName)
        {
            //please enter any new record order by name
            var loanGoalList = GetLoanGoalList();
            return loanGoalList.FirstOrDefault(rec => rec.ShortName.ToLower() == shortName);
        }

        private static List<LoanGoalData> GetLoanGoalList()
        {
            var loanGoalList = new List<LoanGoalData> {
                new LoanGoalData { ShortName = "Contract", Description = "Congratulations on the acceptance of your purchase contract! Home buyers who apply with us save up to $4,250. We will just ask some basic information of you, and we’ll be able to get you an accurate mortgage rate quote. No credit report or financial information necessary.<br/>"+
                    "Research from over 300 loan options, obtain your mortgage rate quote, research closing costs, and determine how much you need at closing. All in less than 5 minutes.", Value = (int)LoanGoalsEnum.HomeUnderContract },
                new LoanGoalData { ShortName = "LowerPayments", Description = "Save money on your mortgage by refinancing to lower interest rates, eliminate PMI, or shorten your payoff time. The process is fast and easy.<br/>"+
                    "Research from over 300 loan options, obtain your mortgage rate quote, research closing costs to determine how much you can save. All in less than 5 minutes.", Value = (int)LoanGoalsEnum.LowerMyRateTerm },
                new LoanGoalData { ShortName = "Debt-Consolidation", Description = "Tap into your home equity and refinance your home mortgage to consolidate your debt. Increase your monthly cash flow, and reduce your monthly payment obligations. Save money by consolidating high interest rate debts like credit cards and student loan debt at lower interest rates.<br/>"+
                    "Research from over 300 loan options, obtain your mortgage rate quote, and research closing costs to determine how much you can save each month. All in less than 5 minutes.", Value = (int)LoanGoalsEnum.DebtConsolidation },

                new LoanGoalData { ShortName = "Cash-Out", Description = "Enhance your lifestyle and add value to your home with a home improvement cash-out mortgage loan. Use the extra cash for personal expenses, to buyout a co-owner or spouse, or for any other reason. The process is fast and easy.<br/>"+
                    "Research from over 300 loan options, obtain your mortgage rate quote, and research closing costs to determine how much you will get at closing. All in less than 5 minutes.", Value = (int)LoanGoalsEnum.NeedCash },
                new LoanGoalData { ShortName = "Preapproval", Description = "We are honored to be part of your home ownership dream. The preapproval process is quick and easy. With our preapproval process, you will get your credit scores and a renewable preapproval certificate.<br/>"+
                    "Research from over 300 loan options, obtain your mortgage rate quote, research closing costs, and determine how much you need at closing. All in less than 5 minutes.\r\n", Value = (int)LoanGoalsEnum.NeedPreapproval },
                new LoanGoalData { ShortName = "Researching", Description = "Congratulations on taking the first step to realize the American dream. Our superior technology allows you to research and get accurate loan program options and details.<br/>"+
                    "Research from over 300 loan options, obtain your mortgage rate quote, research closing costs, determine how much you need at closing. All in less than 5 minutes.", Value = (int)LoanGoalsEnum.ResearchingRates }
            };

            return loanGoalList;
                 
        }

        public static List<ItemSelectionList> GetPrefixName()
        {
            return new List<ItemSelectionList> { new ItemSelectionList { Name = "--- Please Select ---", Value = "" }, new ItemSelectionList { Name = "Mr.", Value = "Mr." }, new ItemSelectionList { Name = "Mrs.", Value = "Mrs." }, new ItemSelectionList { Name = "Dr.", Value = "Dr." }, new ItemSelectionList { Name = "Miss.", Value = "Miss." } };
        }
        public static List<ItemSelectionList> GetSuffixName()
        {
            return new List<ItemSelectionList> { new ItemSelectionList { Name = "--- Please Select ---", Value = "" }, new ItemSelectionList { Name = "Jr", Value = "Jr" }, new ItemSelectionList { Name = "Sr", Value = "Sr" } };
        }
        public static List<ItemSelectionList> GetInfoTypes()
        {
            //return new List<ItemSelectionList> { new ItemSelectionList { Name = "Home", Value = "Home" }, new ItemSelectionList { Name = "Office", Value = "Office" } };
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(CustomerContactTypes)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = item.GetEnumDescription() });

            return items;
        }

        public static List<ItemSelectionList> GetEnumForRadioList()
        {
            //return new List<ItemSelectionList> { new ItemSelectionList { Name = "Home", Value = "Home" }, new ItemSelectionList { Name = "Office", Value = "Office" } };
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(CustomerContactTypes)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = item.ToInt() });

            return items;
        }

        public static List<ItemSelectionList> GetPhoneTypes()
        {
            //return new List<ItemSelectionList> { new ItemSelectionList { Name = "Home", Value = "Home" }, new ItemSelectionList { Name = "Office", Value = "Office" } };
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(PhoneTypes)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = item.GetEnumDescription() });

            return items;
        }
        public static List<ItemSelectionList> GetTemplateRefTypes()
        {

            return new List<ItemSelectionList> { new ItemSelectionList { Name = "Customer", Value = Constants.GetEntityType(typeof(Customer)) }, new ItemSelectionList { Name = "Employee", Value = Constants.GetEntityType(typeof(Employee)) }, new ItemSelectionList { Name = "Lead", Value = Constants.GetEntityType(typeof(Opportunity)) } };
        }

        public static List<ItemSelectionList> GetTemplatTypes()
        {
            return new List<ItemSelectionList> { new ItemSelectionList { Name = "Email", Value = 1 }, new ItemSelectionList { Name = "SMS", Value = 2 } };
        }

        public static List<ItemSelectionList> GetRuleTypes()
        {
            return new List<ItemSelectionList>
            {
                new ItemSelectionList { Name = "Opportunity Assignment", Value = (int)RuleType.OpportunityAssignment },
                new ItemSelectionList { Name = "Fees Calculation", Value = (int)RuleType.FeesCalculation},
                new ItemSelectionList { Name = "Lock Days", Value =(int)RuleType.LockDays },
                new ItemSelectionList { Name = "LoanRequest Eligibility", Value = (int)RuleType.LoanRequestEligibility },
                new ItemSelectionList { Name = "Web Content", Value = (int)RuleType.WebContent },
                new ItemSelectionList { Name = "Profit Table", Value = (int)RuleType.ProfitTable },
                new ItemSelectionList { Name = "Campaign", Value = (int)RuleType.Campaign },
                new ItemSelectionList { Name = "Adjustment", Value = (int)RuleType.Adjustment },
                 new ItemSelectionList { Name = "Rate Filter", Value = (int)RuleType.RateFilter },
                 new ItemSelectionList { Name = "Fees Paid By", Value = (int)RuleType.FeesPaidBy },
            }.OrderBy(x => x.Name).ToList(); ;
        }



        public static List<ItemSelectionList> GetRuleMessageTypes()
        {
            return new List<ItemSelectionList>
            {
                new ItemSelectionList { Name = "Content Message", Value = (int) RuleMessageType.ContentMessage },
                new ItemSelectionList { Name = "Eligibility Message", Value =(int) RuleMessageType.EligibilityMessage },

            }.OrderBy(x => x.Name).ToList(); ;
        }
        public static List<ItemSelectionList> GetRuleMessageViewFor()
        {
            return new List<ItemSelectionList>
            {
                new ItemSelectionList { Name = "Customer", Value = (int) RuleMessageFor.Customer },
                new ItemSelectionList { Name = "Different for Employee and Customer", Value = (int) RuleMessageFor.BothDifferent },
                new ItemSelectionList { Name = "Employee", Value = (int) RuleMessageFor.Employee },
                new ItemSelectionList { Name = "Same for Employee and Customer", Value =(int) RuleMessageFor.Same },
            }.OrderBy(x => x.Name).ToList(); ;
        }
        public static List<ItemSelectionList> GetValidityId()
        {
            return new List<ItemSelectionList> { new ItemSelectionList { Name = ValidityId.Unknown.ToString(), Value = (int)ValidityId.Unknown }, new ItemSelectionList { Name = ValidityId.Yes.ToString(), Value = (int)ValidityId.Yes }, new ItemSelectionList { Name = ValidityId.No.ToString(), Value = (int)ValidityId.No } };
        }

        public static object GetContactTypes()
        {
            return new List<ItemSelectionList>
            {
                new ItemSelectionList { Name = "Primary Customer", Value = Constants.PrimaryCustomer },
                new ItemSelectionList { Name = "Secondary Customer 1", Value = Constants.SecondaryCustomer1 },
                new ItemSelectionList { Name = "Secondary Customer 2", Value = Constants.SecondaryCustomer2 },
                new ItemSelectionList { Name = "Secondary Customer 3", Value = Constants.SecondaryCustomer3 },
                new ItemSelectionList { Name = "Assigned Lead Name", Value = Constants.OpportunityOriginatorCsr },
            }.OrderBy(x => x.Name).ToList();
        }
        public static object GetFromContactTypes()
        {
            return new List<ItemSelectionList>
            {
                //new ItemSelectionList { Name = "Default Setting Email", Value = Constants.DefaultSettingEmail },
                new ItemSelectionList { Name = "Assigned Lead Name", Value = Constants.DefaultSettingEmail },
                new ItemSelectionList { Name = "Branch Name", Value = Constants.OpportunityBranchEmail },
                new ItemSelectionList { Name = "Business Unit Name", Value = Constants.OpportunityBusinessChannelEmail },

            }.OrderBy(x => x.Name).ToList();
        }


        public static List<ItemSelectionList> GetSearchTypes()
        {
            return new List<ItemSelectionList>
            {
                new ItemSelectionList{Name ="Customer Id",Value="1"},
                new ItemSelectionList{Name ="PropertyValue",Value="2"},
                new ItemSelectionList{Name ="LoanAmount",Value="3"},
                new ItemSelectionList{Name ="ZipCode",Value="4"},
                new ItemSelectionList{Name ="State",Value="5"},
                new ItemSelectionList{Name ="OpportunityId",Value="6"},
            }.OrderBy(x => x.Name).ToList(); ;
        }

        public static List<ItemSelectionList> GetRateTypes()
        {
            var rateTypeList = new List<ItemSelectionList> { new ItemSelectionList { Name = RateType.LowestRate.GetEnumDescription(), Value = (int)RateType.LowestRate }, new ItemSelectionList { Name = RateType.NoClosingCost.GetEnumDescription(), Value = (int)RateType.NoClosingCost }, new ItemSelectionList { Name = RateType.NoLenderFees.GetEnumDescription(), Value = (int)RateType.NoLenderFees }, new ItemSelectionList { Name = RateType.ActualNoClosingCost.GetEnumDescription(), Value = (int)RateType.ActualNoClosingCost }, new ItemSelectionList { Name = RateType.ZeroPoints.GetEnumDescription(), Value = (int)RateType.ZeroPoints } };

            return new List<ItemSelectionList>(rateTypeList.OrderBy(o => o.Name));
        }

        public static object GetLockDayActionTypes()
        {
            return new List<ItemSelectionList> { new ItemSelectionList { Name = LockDayActionType.Adjustment.ToString(), Value = (int)LockDayActionType.Adjustment }, new ItemSelectionList { Name = LockDayActionType.Override.ToString(), Value = (int)LockDayActionType.Override } };
        }

        public static IEnumerable GetHours()
        {

            return new List<ItemSelectionList>
            {
                new ItemSelectionList { Name = "00", Value = 00},
                new ItemSelectionList { Name = "1", Value =1} ,
                new ItemSelectionList { Name = "2", Value =2 },
                new ItemSelectionList { Name = "3", Value = 3},
                new ItemSelectionList { Name = "4", Value = 4},
                new ItemSelectionList { Name = "5", Value =5},
                new ItemSelectionList { Name = "6", Value =6},
                new ItemSelectionList { Name = "7", Value = 7},
                new ItemSelectionList { Name = "8", Value = 8},
                new ItemSelectionList { Name = "9", Value = 9},
                new ItemSelectionList { Name = "10", Value = 10},
                new ItemSelectionList { Name = "11", Value =11},
                new ItemSelectionList { Name = "12", Value = 12},
                new ItemSelectionList { Name = "13", Value = 13},
                new ItemSelectionList { Name = "14", Value = 14},
                new ItemSelectionList { Name = "15", Value = 15},
                new ItemSelectionList { Name = "16", Value = 16},
                new ItemSelectionList { Name = "17", Value = 17},
                new ItemSelectionList { Name = "18", Value = 18},
                new ItemSelectionList { Name = "19", Value = 19},
                new ItemSelectionList { Name = "20", Value = 20},
                new ItemSelectionList { Name = "21", Value = 21},
                new ItemSelectionList { Name = "22", Value = 22},
                new ItemSelectionList { Name = "23", Value = 23},

            };
        }

        public static IEnumerable GetMinutes()
        {
            return new List<ItemSelectionList>
            {  new ItemSelectionList { Name = "00", Value = 0},
                new ItemSelectionList { Name = "1", Value = 1},
                new ItemSelectionList { Name = "2", Value = 2},
                new ItemSelectionList { Name = "3", Value = 3},
                new ItemSelectionList { Name = "4", Value = 4},
                new ItemSelectionList { Name = "5", Value = 5},
                new ItemSelectionList { Name = "6", Value = 6},
                new ItemSelectionList { Name = "7", Value = 7},
                new ItemSelectionList { Name = "8", Value = 8},
                new ItemSelectionList { Name = "9", Value = 9},
                new ItemSelectionList { Name = "10", Value = 10},
                new ItemSelectionList { Name = "11", Value = 11},
                new ItemSelectionList { Name = "12", Value = 12},
                new ItemSelectionList { Name = "13", Value = 13},
                new ItemSelectionList { Name = "14", Value = 14},
                new ItemSelectionList { Name = "15", Value = 15},
                new ItemSelectionList { Name = "16", Value = 16},
                new ItemSelectionList { Name = "17", Value = 17},
                new ItemSelectionList { Name = "18", Value = 18},
                new ItemSelectionList { Name = "19", Value = 19},
                new ItemSelectionList { Name = "20", Value = 20},
                new ItemSelectionList { Name = "21", Value = 21},
                new ItemSelectionList { Name = "22", Value = 22},
                new ItemSelectionList { Name = "23", Value = 23},
                new ItemSelectionList { Name = "24", Value = 24},
                new ItemSelectionList { Name = "25", Value = 25},
                new ItemSelectionList { Name = "26", Value = 26},
                new ItemSelectionList { Name = "27", Value = 27},
                new ItemSelectionList { Name = "28", Value = 28},
                new ItemSelectionList { Name = "29", Value = 29},
                new ItemSelectionList { Name = "30", Value = 30},
                new ItemSelectionList { Name = "31", Value = 31},
                new ItemSelectionList { Name = "32", Value = 32},
                new ItemSelectionList { Name = "33", Value = 33},
                new ItemSelectionList { Name = "34", Value = 34},
                new ItemSelectionList { Name = "35", Value = 35},
                new ItemSelectionList { Name = "36", Value = 36},
                new ItemSelectionList { Name = "37", Value = 37},
                new ItemSelectionList { Name = "38", Value = 38},
                new ItemSelectionList { Name = "39", Value = 39},
                new ItemSelectionList { Name = "40", Value = 40},
                new ItemSelectionList { Name = "41", Value = 41},
                new ItemSelectionList { Name = "42", Value = 42},
                new ItemSelectionList { Name = "43", Value = 43},
                new ItemSelectionList { Name = "44", Value = 44},
                new ItemSelectionList { Name = "45", Value = 45},
                new ItemSelectionList { Name = "46", Value = 46},
                new ItemSelectionList { Name = "47", Value = 47},
                new ItemSelectionList { Name = "48", Value = 48},
                new ItemSelectionList { Name = "49", Value = 49},
                new ItemSelectionList { Name = "50", Value = 50},
                new ItemSelectionList { Name = "51", Value = 51},
                new ItemSelectionList { Name = "52", Value = 52},
                new ItemSelectionList { Name = "53", Value = 53},
                new ItemSelectionList { Name = "54", Value = 54},
                new ItemSelectionList { Name = "55", Value = 55},
                new ItemSelectionList { Name = "56", Value = 56},
                new ItemSelectionList { Name = "57", Value = 57},
                new ItemSelectionList { Name = "58", Value = 58},
                new ItemSelectionList { Name = "59", Value = 59},

            };
        }


        public static IEnumerable GetWeekNames()
        {
            return new List<ItemSelectionList>
            {

                new ItemSelectionList { Name = "First", Value = "1"},
                new ItemSelectionList { Name = "Second", Value = "2"},
                new ItemSelectionList { Name = "Third", Value = "3"},
                new ItemSelectionList { Name = "Fourth", Value = "4"},


            };
        }


        public static IEnumerable GetDayNames()
        {
            return new List<ItemSelectionList>
            {

                new ItemSelectionList { Name = "Monday", Value = "MON"},
                new ItemSelectionList { Name = "Tuesday", Value = "TUE"},
                new ItemSelectionList { Name = "Wednesday", Value = "WED"},
                new ItemSelectionList { Name = "Thursday", Value = "THU"},
                new ItemSelectionList { Name = "Friday", Value = "FRI"},
                new ItemSelectionList { Name = "Saturday", Value = "SAT"},
                new ItemSelectionList { Name = "Sunday", Value = "SUN"},

            };
        }




        public static IEnumerable GetMonthNames()
        {
            return new List<ItemSelectionList>
            {

                new ItemSelectionList { Name = "January", Value = "1"},
                new ItemSelectionList { Name = "February", Value = "2"},
                new ItemSelectionList { Name = "March", Value = "3"},
                new ItemSelectionList { Name = "April", Value = "4"},
                new ItemSelectionList { Name = "May", Value = "5"},
                new ItemSelectionList { Name = "June", Value = "6"},
                new ItemSelectionList { Name = "July", Value = "7"},
                new ItemSelectionList { Name = "August", Value = "8"},
                new ItemSelectionList { Name = "September", Value = "9"},
                new ItemSelectionList { Name = "October", Value = "10"},
                new ItemSelectionList { Name = "November", Value = "11"},
                new ItemSelectionList { Name = "December", Value = "12"},


            };
        }


        public static IEnumerable GetActivityForTypes()
        {
            return new List<ItemSelectionList>
            {
                new ItemSelectionList {Name = "Campaign", Value = (int) ActivityForType.Campaign},
                new ItemSelectionList { Name = "Closing Page Direct Email", Value = (int) ActivityForType.ClosingDirectEmail },
                new ItemSelectionList { Name = "Compare Page Direct Email", Value = (int) ActivityForType.CompareDirectEmail },
                new ItemSelectionList { Name = "Rates Direct Email", Value = (int) ActivityForType.RatesDirectEmail },
                new ItemSelectionList { Name = "User Forgot Password", Value = (int) ActivityForType.CustomerForgotPassword },
                new ItemSelectionList { Name = "Email Customer For Loan Application", Value = (int) ActivityForType.EmailCustomerForLoanApplication},
                new ItemSelectionList { Name = "Email Customer For Non Loan Application", Value = (int) ActivityForType.EmailCustomerForNonLoanApplication},
                new ItemSelectionList { Name = "Email Customer For Loan Application With LO", Value = (int) ActivityForType.EmailCustomerForLoanApplicationWithLo },
                new ItemSelectionList { Name = "Email Customer For Non Loan Application With LO", Value = (int) ActivityForType.EmailCustomerForNonLoanApplicationWithLo },
                new ItemSelectionList { Name = "Email LO For Loan Application", Value = (int) ActivityForType.EmailLoForLoanApplication },
                new ItemSelectionList { Name = "Email LO For Non Loan Application", Value = (int) ActivityForType.EmailLoForNonLoanApplication }
            };
        }



        public static List<ItemSelectionList> GetLayoutType()
        {
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(LayoutType)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return items;
        }
        public static List<ItemSelectionList> GetCmsPageType()
        {
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(CmsPageType)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return items;
        }

        public static List<ItemSelectionList> GetCushions()
        {
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(Cushion)))
                items.Add(new ItemSelectionList { Name = (int)item, Value = (int)item });

            return items;
        }


        public static List<ItemSelectionList> GetControlOnpage()
        {
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(ControlOnpage)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return items;
        }

        public static List<ItemSelectionList> GetRequestTypes()
        {
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(LoanPurposeList)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return items;
        }

        public static List<ItemSelectionList> GetSuffixTypes()
        {
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(Suffix)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = item.GetEnumDescription() });

            return items;
        }

        public static List<ItemSelectionList> GetPartnerTypes()
        {
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(PartnerType)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return items;
        }


        public static List<ItemSelectionList> GetMaritalStatuses()
        {
            List<ItemSelectionList> items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(MaritalStatus)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return items;
        }

        public static List<ItemSelectionList> GetTemplateLocation()
        {
            var items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(TemplateLocation)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return new List<ItemSelectionList>(items.OrderBy(o => o.Name));
        }

        public static List<ItemSelectionList> LeadCreateFromList()
        {
            var items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(LeadCreatedFrom)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return new List<ItemSelectionList>(items.OrderBy(o => o.Name));
        }

        public static List<ItemSelectionList> GetFollowUpRemindValues()
        {
            var items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(RemindBefore)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return new List<ItemSelectionList>(items.OrderBy(o => o.Name));
        }

        public static List<ItemSelectionList> GetEnumValues(Type entity)
        {
            var itemList = new List<ItemSelectionList>();

            foreach (var item in Enum.GetValues(entity))
            {
                itemList.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });
            }

            return itemList.OrderBy(x => x.Name).ToList();
        }

        public static List<ItemSelectionList> GetSecondLoanObtainTimeValues()
        {
            return new List<ItemSelectionList>
            {
                new ItemSelectionList{Name ="Obtained simultaneously with the current 1st mortgage",Value="True"},
                new ItemSelectionList{Name ="Obtained after the current 1st mortgage",Value="False"}
            };
        }

        public static List<ItemSelectionList> GetMortgagePayOffOptionValues()
        {
            return new List<ItemSelectionList>
            {
                new ItemSelectionList{Name ="Payoff with this new loan",Value="True"},
                new ItemSelectionList{Name ="Don't payoff with this new loan",Value="False"}
            };
        }

        public static List<ItemSelectionList> GetCampaignDependOnType()
        {
            var items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(CampaignDependOnType)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return new List<ItemSelectionList>(items.OrderBy(o => o.Name));
        }

        public static List<ItemSelectionList> GetMessageLocationList()
        {
            var items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(MessageLocationEnum)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return new List<ItemSelectionList>(items.OrderBy(o => o.Name));
        }

        public static List<ItemSelectionList> GetMortgagePageTypes()
        {
            var items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(MortgagePageType)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return new List<ItemSelectionList>(items.OrderBy(o => o.Name));
        }

        public static List<ItemSelectionList> GetLoanPurposeTypes()
        {
            var items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(LoanPurposeType)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return new List<ItemSelectionList>(items.OrderBy(o => o.Name));
        }

        public static List<ItemSelectionList> GetCmsPageTypeList()
        {
            var items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(CmsPageFor)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return new List<ItemSelectionList>(items.OrderBy(o => o.Name));
        }

        public static List<ItemSelectionList> GetSiteMapFrequencyList()
        {
            var items = new List<ItemSelectionList>();
            foreach (var item in Enum.GetValues(typeof(CmsChangeFrequency)))
                items.Add(new ItemSelectionList { Name = item.GetEnumDescription(), Value = (int)item });

            return new List<ItemSelectionList>(items.OrderBy(o => o.Name));
        }

    }

    public class PropertySearchResult
    {
        public static string GetLastSearch(LoanRequest entity)
        {
            var lastSearch = string.Empty;
            if (entity != null)
            {
                if (entity.City != null)
                    lastSearch = entity.City.Name + ", ";

                if (entity.County != null)
                {
                    lastSearch += entity.County.Name + " ";
                    if (entity.County.CountyType != null)
                        lastSearch += entity.County.CountyType.Name + ", ";
                }

                if (entity.State != null)
                    lastSearch += entity.State.Abbreviation + " ";

                lastSearch += entity.ZipCode;
            }

            return lastSearch;
        }
        public static string GetDetailLastSearch(LoanRequest entity)
        {
            var lastSearch = string.Empty;
            if (entity != null)
            {
                if (entity.City != null)
                    lastSearch = entity.City.Name;

                if (entity.County != null)
                    lastSearch += " " + entity.County.Name;

                if (entity.State != null)
                    lastSearch += " County  ," + entity.State.Name;

                // lastSearch += entity.ZipCode;
            }

            return lastSearch;
        }
    }

    public class LoanGoalData
    {
        public int Value { get; set; }
        public string Description { get; set; }
        public string ShortName { get; set; }
    }
}
