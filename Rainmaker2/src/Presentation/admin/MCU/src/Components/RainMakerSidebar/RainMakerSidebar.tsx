import React from 'react'

export const RainMakerSidebar = () => {
    return (
        <div id="sidebar" className="sidebar ">
            <div className="sidebar-menu nav-collapse">
                <ul className="sidebar-main-ul">
                    <li>
                        <a href="/Admin/dashboard">
                            <i className="fas fa-file-alt"></i> <span className="menu-text">Dashboard</span>
                        </a>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fas fa-users"></i> <span className="menu-text">User Profile</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text Customer">
                                <a href="/Admin/Customer">
                                    <span className="menu-text">Customer</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text UserProfile">
                                <a href="/Admin/UserProfile">
                                    <span className="menu-text">Customer Login Account</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Employee">
                                <a href="/Admin/Employee">
                                    <span className="menu-text">Employee</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text EmployeeProfile">
                                <a href="/Admin/EmployeeProfile">
                                    <span className="menu-text">Employee Login Account</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fa fa-cogs"></i> <span className="menu-text">Setup</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Ads Setup</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text AdsGeoLocation">
                                        <a href="/Admin/AdsGeoLocation">
                                            <span className="menu-text">Ads Geo Location</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text AdsPageLocation">
                                        <a href="/Admin/AdsPageLocation">
                                            <span className="menu-text">Ads Page Location</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text AdsPromotion">
                                        <a href="/Admin/AdsPromotion">
                                            <span className="menu-text">Ads Promotion</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text AdsSize">
                                        <a href="/Admin/AdsSize">
                                            <span className="menu-text">Ads Size</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text AdsSource">
                                        <a href="/Admin/AdsSource">
                                            <span className="menu-text">Ads Source</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text AdsSourceMessage">
                                        <a href="/Admin/AdsSourceMessage">
                                            <span className="menu-text">Ads Source Messages</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text StringResource?resourceTypeId=2">
                                        <a href="/Admin/StringResource?resourceTypeId=2">
                                            <span className="menu-text">Ads Source String Resouce</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text AdsType">
                                        <a href="/Admin/AdsType">
                                            <span className="menu-text">Ads Type</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Bank Rate Setup</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text BankRateInstance">
                                        <a href="/Admin/BankRateInstance">
                                            <span className="menu-text">Bank Rate Instance</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text BankRatePoint">
                                        <a href="/Admin/BankRatePoint">
                                            <span className="menu-text">Bank Rate Point</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text BankRateProduct">
                                        <a href="/Admin/BankRateProduct">
                                            <span className="menu-text">Bank Rate Product</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text BankRateTier">
                                        <a href="/Admin/BankRateTier">
                                            <span className="menu-text">Bank Rate Tier</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text LoanToValue">
                                        <a href="/Admin/LoanToValue">
                                            <span className="menu-text">Loan To Value</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Company Setup</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text Branch">
                                        <a href="/Admin/Branch">
                                            <span className="menu-text">Branch</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text BusinessUnit">
                                        <a href="/Admin/BusinessUnit">
                                            <span className="menu-text">Business Unit</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text CompanyPhoneInfo">
                                        <a href="/Admin/CompanyPhoneInfo">
                                            <span className="menu-text">Company Phone</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text Department">
                                        <a href="/Admin/Department">
                                            <span className="menu-text">Department</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text Position">
                                        <a href="/Admin/Position">
                                            <span className="menu-text">Position</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text EmailAccount">
                                        <a href="/Admin/EmailAccount">
                                            <span className="menu-text">SMTP Email</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text Team">
                                        <a href="/Admin/Team">
                                            <span className="menu-text">Team</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Escrows Setup</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text EscrowEntityType">
                                        <a href="/Admin/EscrowEntityType">
                                            <span className="menu-text">Escrow Entity Type</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text Insurance">
                                        <a href="/Admin/Insurance">
                                            <span className="menu-text">Insurance</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text PropertyTax">
                                        <a href="/Admin/PropertyTax">
                                            <span className="menu-text">Property Tax</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">FeeSetup</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text BusinessPartner">
                                        <a href="/Admin/BusinessPartner">
                                            <span className="menu-text">Business Partner</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text BusinessPartnerType">
                                        <a href="/Admin/BusinessPartnerType">
                                            <span className="menu-text">Business Partner Type</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text FeeCategory">
                                        <a href="/Admin/FeeCategory">
                                            <span className="menu-text">Fee Category</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Lead Question</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text Question">
                                        <a href="/Admin/Question">
                                            <span className="menu-text">Question</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text QuestionSection">
                                        <a href="/Admin/QuestionSection">
                                            <span className="menu-text">Question Section</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Lead Setup</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text CreditRating">
                                        <a href="/Admin/CreditRating">
                                            <span className="menu-text">Credit Rating</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text DebtToIncomeRatio">
                                        <a href="/Admin/DebtToIncomeRatio">
                                            <span className="menu-text">Debt To Income Ratio</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text FollowUpPriority">
                                        <a href="/Admin/FollowUpPriority">
                                            <span className="menu-text">FollowUp Priority</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text FollowupPurpose">
                                        <a href="/Admin/FollowupPurpose">
                                            <span className="menu-text">Followup Purpose</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text LeadSource">
                                        <a href="/Admin/LeadSource">
                                            <span className="menu-text">Lead Source</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text LeadSourceType">
                                        <a href="/Admin/LeadSourceType">
                                            <span className="menu-text">Lead Source Type</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text LeadType">
                                        <a href="/Admin/LeadType">
                                            <span className="menu-text">Lead Type</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text LoanGoal">
                                        <a href="/Admin/LoanGoal">
                                            <span className="menu-text">Loan Goal</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text LoanIndexType">
                                        <a href="/Admin/LoanIndexType">
                                            <span className="menu-text">Loan Index Type</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text loanPurpose">
                                        <a href="/Admin/loanPurpose">
                                            <span className="menu-text">Loan Purpose</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text LoanType">
                                        <a href="/Admin/LoanType">
                                            <span className="menu-text">Loan Type</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text LockPeriod">
                                        <a href="/Admin/LockPeriod">
                                            <span className="menu-text">Lock Period</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text NoteTopic">
                                        <a href="/Admin/NoteTopic">
                                            <span className="menu-text">Note Topic</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text PropertyType">
                                        <a href="/Admin/PropertyType">
                                            <span className="menu-text">Property Type</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text PropertyUsage">
                                        <a href="/Admin/PropertyUsage">
                                            <span className="menu-text">Property Usage</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Product Setup</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text Agency">
                                        <a href="/Admin/Agency">
                                            <span className="menu-text">Agency</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text AmortizationType">
                                        <a href="/Admin/AmortizationType">
                                            <span className="menu-text">Amortization Type</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text AusProcessingType">
                                        <a href="/Admin/AusProcessingType">
                                            <span className="menu-text">Aus Processing Type</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text BenchMark">
                                        <a href="/Admin/BenchMark">
                                            <span className="menu-text">Bench Mark</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text BestEx">
                                        <a href="/Admin/BestEx">
                                            <span className="menu-text">BestEx</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text Investor">
                                        <a href="/Admin/Investor">
                                            <span className="menu-text">Investor</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text PrepayPenalty">
                                        <a href="/Admin/PrepayPenalty">
                                            <span className="menu-text">Prepay Penalty</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text Product">
                                        <a href="/Admin/Product">
                                            <span className="menu-text">Product</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text ProductClass">
                                        <a href="/Admin/ProductClass">
                                            <span className="menu-text">Product Class</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text ProductFamily">
                                        <a href="/Admin/ProductFamily">
                                            <span className="menu-text">Product Family</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text ProductQualifier">
                                        <a href="/Admin/ProductQualifier">
                                            <span className="menu-text">Product Qualifier</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text ProductType">
                                        <a href="/Admin/ProductType">
                                            <span className="menu-text">Product Type</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Rateupdate setup</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text MbsRate">
                                        <a href="/Admin/MbsRate">
                                            <span className="menu-text">Current Market Rate</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text MbsRateArchive">
                                        <a href="/Admin/MbsRateArchive">
                                            <span className="menu-text">Current Market Rate Archive</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text CurrentRate">
                                        <a href="/Admin/CurrentRate">
                                            <span className="menu-text">Current Rate</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text CurrentRateArchive">
                                        <a href="/Admin/CurrentRateArchive">
                                            <span className="menu-text">Current Rate Archive</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text DefaultLoanParameter">
                                        <a href="/Admin/DefaultLoanParameter">
                                            <span className="menu-text">Default Loan Parameter</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text RateServiceParameter">
                                        <a href="/Admin/RateServiceParameter">
                                            <span className="menu-text">Rate Service Parameter</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Region Setup</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text City">
                                        <a href="/Admin/City">
                                            <span className="menu-text">City</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text Country">
                                        <a href="/Admin/Country">
                                            <span className="menu-text">Country</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text County">
                                        <a href="/Admin/County">
                                            <span className="menu-text">County</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text Region">
                                        <a href="/Admin/Region">
                                            <span className="menu-text">Region</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text State">
                                        <a href="/Admin/State">
                                            <span className="menu-text">State</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text ZipCode">
                                        <a href="/Admin/ZipCode">
                                            <span className="menu-text">Zip Code</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Subscription Setup</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text Subscription">
                                        <a href="/Admin/Subscription">
                                            <span className="menu-text">Subscription</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text SubscriptionGroup">
                                        <a href="/Admin/SubscriptionGroup">
                                            <span className="menu-text">Subscription Group</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text SubscriptionSection">
                                        <a href="/Admin/SubscriptionSection">
                                            <span className="menu-text">Subscription Section</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>
                            <li className="has-sub-sub current">
                                <a href="javascript:;" className=""><span className="sub-menu-text">Work Flow</span><span className="arrow"></span></a>
                                <ul className="sub-sub" >
                                    <li className="sub-sub-menu-text Status">
                                        <a href="/Admin/Status">
                                            <span className="menu-text">Lead &amp; Loan Status</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text LockStatusCause">
                                        <a href="/Admin/LockStatusCause">
                                            <span className="menu-text">Lock Status Cause</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text LockStatusList">
                                        <a href="/Admin/LockStatusList">
                                            <span className="menu-text">Lock Status List</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text StatusCause">
                                        <a href="/Admin/StatusCause">
                                            <span className="menu-text">Status Cause</span>
                                            
                                        </a>
                                    </li>
                                    <li className="sub-sub-menu-text WorkFlowStatus">
                                        <a href="/Admin/WorkFlow/WorkFlowStatus">
                                            <span className="menu-text">WorkFlow</span>
                                            
                                        </a>
                                    </li>


                                </ul>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fa fa-unlock-alt"></i> <span className="menu-text">Permissions</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text RolePermissions">
                                <a href="/Admin/RolePermissions">
                                    <span className="menu-text">Assign Permission</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text UserPermission">
                                <a href="/Admin/UserPermission">
                                    <span className="menu-text">User Permission</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text UserRole">
                                <a href="/Admin/UserRole">
                                    <span className="menu-text">User Role</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fa fa-cog"></i> <span className="menu-text">System Setting</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text BlackListIp">
                                <a href="/Admin/BlackListIp">
                                    <span className="menu-text">BlackListIp</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Cache">
                                <a href="/Admin/Cache">
                                    <span className="menu-text">Cache</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text cmspage">
                                <a href="/Admin/cmspage">
                                    <span className="menu-text">CMS Page</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text rmelmah">
                                <a href="/rmelmah">
                                    <span className="menu-text">Error Log</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text GroupsSetting">
                                <a href="/Admin/Setting/GroupsSetting">
                                    <span className="menu-text">Group Setting</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text OfficeHoliday">
                                <a href="/Admin/OfficeHoliday">
                                    <span className="menu-text">Office Holidays</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Setting">
                                <a href="/Admin/Setting">
                                    <span className="menu-text">Setting</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text ">
                                <a href="/Admin/SiteMenu/">
                                    <span className="menu-text">Site Menu</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text StringResource?resourceTypeId=1">
                                <a href="/Admin/StringResource?resourceTypeId=1">
                                    <span className="menu-text">String Resource</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fa fa-bullhorn"></i> <span className="menu-text">Campaigns</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text Activity">
                                <a href="/Admin/Activity">
                                    <span className="menu-text">Activity</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Campaign">
                                <a href="/Admin/Campaign">
                                    <span className="menu-text">Campaign</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text CampaignQueue">
                                <a href="/Admin/CampaignQueue">
                                    <span className="menu-text">Campaign Queue</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Rule?RuleType=7">
                                <a href="/Admin/Rule?RuleType=7">
                                    <span className="menu-text">Campaign Rule</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text CampaignScheduler">
                                <a href="/Admin/CampaignScheduler">
                                    <span className="menu-text">Campaign Scheduler</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text TemplateAttachment">
                                <a href="/Admin/TemplateAttachment">
                                    <span className="menu-text">Template Attachment</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Template">
                                <a href="/Admin/Template">
                                    <span className="menu-text">Templates</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fa fa-crosshairs"></i> <span className="menu-text">Tracking</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text AuditTrail">
                                <a href="/Admin/AuditTrail">
                                    <span className="menu-text">Audit Trail</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text EmailTracking">
                                <a href="/Admin/EmailTracking">
                                    <span className="menu-text">Email Tracking</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Five9LeadPosting">
                                <a href="/Admin/Five9LeadPosting">
                                    <span className="menu-text">Five 9 Lead Posting</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text five9leadpostinglog">
                                <a href="/Admin/five9leadpostinglog">
                                    <span className="menu-text">Five 9 Lead Posting Log</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text ScheduleActivity">
                                <a href="/Admin/ScheduleActivity">
                                    <span className="menu-text">Schedule Activity</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text ScheduleActivityLog">
                                <a href="/Admin/ScheduleActivityLog">
                                    <span className="menu-text">Schedule Activity Log</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text SessionLog">
                                <a href="/Admin/SessionLog">
                                    <span className="menu-text">Session Log</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text SessionLogDetail">
                                <a href="/Admin/SessionLogDetail">
                                    <span className="menu-text">Session Log Detail</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Visitor">
                                <a href="/Admin/Visitor">
                                    <span className="menu-text">Visitor</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text WorkQueueStatus">
                                <a href="/Admin/WorkQueueStatus">
                                    <span className="menu-text">Work Queue Status Tracking</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fa fa-university"></i> <span className="menu-text">Lead</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text AllQuotes">
                                <a href="/Admin/AllQuotes">
                                    <span className="menu-text">AllQuotes Result</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Opportunity">
                                <a href="/Admin/Opportunity">
                                    <span className="menu-text">Leads</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text ThirdPartyLead">
                                <a href="/Admin/ThirdPartyLead">
                                    <span className="menu-text">Third Party Lead</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fa fa-gavel"></i> <span className="menu-text">Rule</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text Rule?RuleType=2">
                                <a href="/Admin/Rule?RuleType=2">
                                    <span className="menu-text">Fee Calculation Rule</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Rule?RuleType=4">
                                <a href="/Admin/Rule?RuleType=4">
                                    <span className="menu-text">Loan Request Eligibility Rule</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Rule?RuleType=3">
                                <a href="/Admin/Rule?RuleType=3">
                                    <span className="menu-text">Lock Day Rule</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Rule?RuleType=1">
                                <a href="/Admin/Rule?RuleType=1">
                                    <span className="menu-text">Opportunity Assignment Rule</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Adjustment">
                                <a href="/Admin/Adjustment">
                                    <span className="menu-text">Pricing Adjustment</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Rule?RuleType=8">
                                <a href="/Admin/Rule?RuleType=8">
                                    <span className="menu-text">Pricing Adjustment Rule</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text ProfitTable">
                                <a href="/Admin/ProfitTable">
                                    <span className="menu-text">Profit Table</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Rule?RuleType=6">
                                <a href="/Admin/Rule?RuleType=6">
                                    <span className="menu-text">Profit Table Rule</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Rule?RuleType=9">
                                <a href="/Admin/Rule?RuleType=9">
                                    <span className="menu-text">Rate Filter Rule</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text RuleMessage">
                                <a href="/Admin/RuleMessage">
                                    <span className="menu-text">Rule Message</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Rule?RuleType=5">
                                <a href="/Admin/Rule?RuleType=5">
                                    <span className="menu-text">Web Content Rule</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fa fa-file-text"></i> <span className="menu-text">Manage Fees</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text Fee">
                                <a href="/Admin/Fee">
                                    <span className="menu-text">Fee</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text FeeDetails">
                                <a href="/Admin/FeeDetails">
                                    <span className="menu-text">Fee Details</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text Formula">
                                <a href="/Admin/Formula">
                                    <span className="menu-text">Formula</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text RangeSet">
                                <a href="/Admin/RangeSet">
                                    <span className="menu-text">Range Set</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text TemplateFormPlot">
                                <a href="/Admin/TemplateFormPlot">
                                    <span className="menu-text">Template Plot Form</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub active">
                        <a href="javascript:;" className="">
                            <i className="fa fa-check-square-o"></i> <span className="menu-text">Review</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text ReviewComment">
                                <a href="/Admin/ReviewComment">
                                    <span className="menu-text">Customer comment</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text current ReviewSite">
                                <a href="/Admin/ReviewSite">
                                    <span className="menu-text">Review Site</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fa fa-money"></i> <span className="menu-text">Loan Application</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text Loanapplication">
                                <a href="/Admin/Loanapplication">
                                    <span className="menu-text">Loan Applications</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fas fa-ad"></i> <span className="menu-text">Marketing</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text LeadGroup">
                                <a href="/Admin/LeadGroup">
                                    <span className="menu-text">Lead Group</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text OpportunityMarketing">
                                <a href="/Admin/OpportunityMarketing">
                                    <span className="menu-text">Opportunity Marketing</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text PromotionalProgram">
                                <a href="/Admin/PromotionalProgram">
                                    <span className="menu-text">Promotional Program</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text RateWidget">
                                <a href="/Admin/RateWidget">
                                    <span className="menu-text">Rate Widget</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text SeoManagement">
                                <a href="/Admin/SeoManagement">
                                    <span className="menu-text">Seo Management</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>
                    <li className="has-sub">
                        <a href="javascript:;" className="">
                            <i className="fas fa-phone"></i> <span className="menu-text">Vortex</span>
                            <span className="arrow"></span>
                        </a>
                        <ul className="sub">
                            <li className="sub-sub-menu-text VortexConfiguration">
                                <a href="/Admin/VortexConfiguration">
                                    <span className="menu-text">Configuration</span>
                                    
                                </a>
                            </li>
                            <li className="sub-sub-menu-text VortexUserSession">
                                <a href="/Admin/VortexUserSession">
                                    <span className="menu-text">User Agent</span>
                                    
                                </a>
                            </li>


                        </ul>
                    </li>


                </ul>

            </div>
        </div>
    )
}
