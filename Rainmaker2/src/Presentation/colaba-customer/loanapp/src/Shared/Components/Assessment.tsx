import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router';
import { LocalDB } from '../../lib/LocalDB';
import { AssessmentData } from '../../Utilities/AssessmentEnum';
// import Accordion from 'react-bootstrap/Accordion';

const Assessment: React.FC = () => {

    const [stateHover, setStateHover] = useState<boolean>(false);
    const [loadAnim, setLoadAnim] = useState(true);
    const [assessment, setAssessment] = useState(true);
    const [assessmentData, setAssessmentData] = useState<string | null>("");
    const [isAllowed, setisAllowed] = useState(false);
    const location = useLocation();

    const allowedRoutes = [
        { path: "/loanApplication/GettingStarted/HowCanWeHelp", data: null },
        { path: "/loanApplication/GettingStarted/PurchaseProcessState", data: null },
        { path: "/loanApplication/GettingToKnowYou/AboutYourself", data: null },
        { path: "/loanApplication/GettingToKnowYou/AboutCurrentHome", data: null },
        { path: "/loanApplication/GettingToKnowYou/MaritalStatus", data: null },
        { path: "/loanApplication/GettingToKnowYou/MilitaryService", data: AssessmentData.militaryService },
        { path: "/loanApplication/GettingToKnowYou/CoApplicant", data: AssessmentData.anotherApplicant },
        { path: "/loanApplication/GettingToKnowYou/ApplicationReview", data: null },
        { path: "/loanApplication/GettingToKnowYou/SSN", data: AssessmentData.ssn },
        { path: "/loanApplication/GettingToKnowYou/Consent", data: AssessmentData.explicitConsent },
        { path: "/loanApplication/GettingToKnowYou/ApplicaitonReviewAfterAgreement", data: null },
        { path: "/loanApplication/MyNewMortgage/NewMortgageReview", data: null },
        { path: "/loanApplication/MyNewMortgage/SubjectPropertyNewHome", data: null },
        { path: "/loanApplication/MyNewMortgage/SubjectPropertyUse", data: null },
        { path: "/loanApplication/MyNewMortgage/SubjectPropertyIntend", data: null },
        { path: "/loanApplication/MyNewMortgage/LoanAmountDetail", data: null },
        { path: "/loanApplication/MyNewMortgage/SubjectPropertyAddress", data: null },

        { path: "/loanApplication/MyMoney/Income/IncomeHome", data: AssessmentData.criticalMorgage },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/IncomeSources", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Employment/EmploymentIncome", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Employment/EmployerAddress", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Employment/AdditionalIncome", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Employment/ModeOfEmploymentIncomePayment", data: null },

        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/SelfIncome/SelfEmploymentIncome", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/SelfIncome/SelfEmploymentAddress", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/SelfIncome/NetSelfEmploymentIncome", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Business/BusinessIncomeType", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Business/BusinessAddress", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Business/BusinessRevenue", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Military/MilitaryIncome", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Military/MilitaryServiceLocation", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Military/ModeOfMilitaryServicePayment", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Retirement/RetirementIncomeSource", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Other/OtherIncome", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/IncomeSourcesHome/Other/OtherIncomeDetails", data: null },
        { path: "/loanApplication/MyMoney/Assets/EarnestMoneyDeposit", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome", data: AssessmentData.assetsInfo },

        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/AssetSources", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/BankAccount/DetailsOfBankAccount", data: null },
        { path: "/loanApplication/MyMoney/Income/IncomeHome/EmploymentAlert", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/ProceedsFromTransaction/DetailsOfProceedsFromTransaction", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/GiftFunds/GiftFundsDetails", data: AssessmentData.assetsGifts },//AssetsGifts()
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/RetirementAccount/RetirementAccountDetails", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/RetirementAccount/RetirementAccountDetails", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/GiftFunds/GiftFundsSource", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/OtherFinancialAssets/TypeOfFinancialAssets", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/OtherFinancialAssets/FinancialAssetsDetail", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/OtherFinancialAssets/FinancialAssetsDetail", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/ProceedsFromTransaction/TypeOfProceedsFromTransaction", data: null },
        { path: "/loanApplication/MyMoney/Assets/AssetsHome/AssetSourcesHome/OtherAssets/OtherAssetsDetails", data: null },
        { path: "/loanApplication/MyProperties/CurrentResidence", data: null },
        { path: "/loanApplication/FinishingUp/BorrowerDependents", data: null },
        { path: "/loanApplication/GovernmentQuestions/BorrowerDeclarations", data: null },
        { path: "/loanApplication/Review/ReviewDetail", data: null },
        { path: "/loanApplication/MyProperties/CurrentResidenceDetails", data: null },
        { path: "/loanApplication/MyProperties/FirstCurrentResidenceMortgage", data: null },
        { path: "/loanApplication/MyProperties/FirstCurrentResidenceMortgageDetails", data: null },
        { path: "/loanApplication/MyProperties/SecondCurrentResidenceMortgage", data: null },
        { path: "/loanApplication/MyProperties/AllProperties", data: null },
        { path: "/loanApplication/MyProperties/AdditionalPropertyType", data: null }

    ];

    useEffect(() => {
        //setTimeout(()=>setLoadAnim(false),1000);
        assesmentMapping(location.pathname);

        // document.querySelector('#accordionAssessment .btn').addEventListener('click',(e)=>{
        //     console.log(e.target)
        // });


        return () => {

        }
    }, [location.pathname])

    const assesmentMapping = (pathname: string) => {
        const assesmentValue = allowedRoutes.find(x => x.path === pathname);

        if (assesmentValue) {
            setisAllowed(true);
            setAssessmentData(assesmentValue.data ? String(assesmentValue.data) : null)
        } else {
            setisAllowed(false);
        }
    }

    

    

    return (
        <div>
            { isAllowed &&
                <div data-testid="assessment" className="loanapp-c-assessment">

                    {(assessment && assessmentData) &&
                        <div className={`loanapp-c-assessment--popup animate__animated animate__faster msg 
                            ${loadAnim ? 'animate__fadeInUp' : 'animate__slideInDown'}`}>
                            <button onClick={() => setAssessment(false)} className="l-c-ap-close"><svg xmlns="http://www.w3.org/2000/svg" width="9.001" height="9" viewBox="0 0 9.001 9">
                                <g id="cancel" transform="translate(0 -0.016)" opacity="0.9">
                                    <g id="Group_1434" data-name="Group 1434" transform="translate(0 0.016)">
                                        <path id="Path_977" data-name="Path 977" d="M5.492,4.516,8.857,1.151a.493.493,0,0,0,0-.7L8.562.16a.494.494,0,0,0-.7,0L4.5,3.525,1.135.16a.493.493,0,0,0-.7,0l-.3.295a.493.493,0,0,0,0,.7L3.509,4.516.144,7.881a.494.494,0,0,0,0,.7l.295.295a.493.493,0,0,0,.7,0L4.5,5.507,7.866,8.872a.488.488,0,0,0,.348.144h0a.488.488,0,0,0,.348-.144l.295-.295a.494.494,0,0,0,0-.7Z" transform="translate(0 -0.016)" fill="#7e829e" />
                                    </g>
                                </g>
                            </svg>
                            </button>
                            <div dangerouslySetInnerHTML={{ __html: assessmentData }}></div>


                        </div>
                    }

                    {/* <div className={`loanapp-c-assessment--popup ques animate__animated animate__faster auto 
                            ${stateHover ? '' : 'arrow float-none'}
                            ${loadAnim ? 'animate__fadeInUp' : 'animate__slideInDown'}
                        `}>
                <span className="l-c-ap-text">Questions? Let us help.</span>
            </div> */}

                    <div className={`loanapp-c-assessment--popup ques animate__animated animate__faster auto 
                            ${loadAnim ? 'animate__fadeInUp arrow float-none' : 'animate__slideInDown'}
                        `}>
                        <span className="l-c-ap-text">Questions? Contact Us!</span>
                    </div>

                    {stateHover &&
                        <div className={`loanapp-c-assessment--popup details arrow animate__animated animate__faster 
            ${loadAnim ? 'animate__fadeInUp' : 'animate__slideInDown'}`}>
                            <ul>
                                <li>
                                    <span className="l-c-ap-icon">
                                        <svg className="colaba-icon-tel" xmlns="http://www.w3.org/2000/svg" width="15.966" height="16" viewBox="0 0 15.966 16">
                                            <g id="Group_1248" data-name="Group 1248" transform="translate(-242.857 -482.511)">
                                                <path id="Path_774" data-name="Path 774" d="M257.317,510.969l-1.814-1.825a1.7,1.7,0,0,0-1.227-.561h-.006a1.757,1.757,0,0,0-1.239.556l-.966.962-.054-.028-.1-.054c-.115-.058-.224-.112-.313-.167A11.209,11.209,0,0,1,248.9,507.4a6.565,6.565,0,0,1-.826-1.279c.22-.206.427-.416.628-.621l.061-.062.14-.142.135-.136a1.671,1.671,0,0,0,0-2.515l-.9-.9c-.1-.1-.207-.207-.306-.312-.228-.238-.432-.44-.626-.619a1.732,1.732,0,0,0-1.213-.534h-.006a1.763,1.763,0,0,0-1.23.535l-1.132,1.143a2.578,2.578,0,0,0-.764,1.641,6,6,0,0,0,.434,2.519,14.365,14.365,0,0,0,2.558,4.269,15.628,15.628,0,0,0,5.238,4.106,8.205,8.205,0,0,0,2.97.877h.015c.033,0,.066,0,.1,0s.073,0,.1,0h.064a2.652,2.652,0,0,0,1.964-.872l.039-.043v0a7.406,7.406,0,0,1,.546-.562c.166-.158.308-.3.434-.431a1.8,1.8,0,0,0,.544-1.249v-.007A1.753,1.753,0,0,0,257.317,510.969Zm-.833,1.647-.01.008-.008.009c-.1.109-.2.21-.315.317l-.082.08a8.873,8.873,0,0,0-.649.672,1.447,1.447,0,0,1-1.073.477h-.2a7.009,7.009,0,0,1-2.525-.76,14.466,14.466,0,0,1-4.839-3.788,13.255,13.255,0,0,1-2.357-3.926l0,0a4.6,4.6,0,0,1-.359-2,1.368,1.368,0,0,1,.413-.884l1.133-1.133a.6.6,0,0,1,.392-.187.553.553,0,0,1,.371.181l0,0,.016.016c.153.143.3.289.452.444l.138.139c.059.064.124.128.186.191l.131.133.913.913.01.007a.479.479,0,0,1,0,.779l-.01.007-.009.009-.1.1c-.062.064-.121.124-.181.18l-.006.006c-.255.26-.528.536-.824.8l0,0a.184.184,0,0,0-.028.027.831.831,0,0,0-.2.906l.011.031,0,.006a7.4,7.4,0,0,0,1.09,1.78,12.286,12.286,0,0,0,2.982,2.712,4.658,4.658,0,0,0,.419.228c.115.058.225.112.315.168l.005,0,.006,0,.008,0,.031.018a.874.874,0,0,0,.4.1h.005a.865.865,0,0,0,.609-.275l1.133-1.133.006-.006a.594.594,0,0,1,.388-.2.524.524,0,0,1,.358.187l0,.006,1.844,1.844.008.006a.5.5,0,0,1,.092.705A.509.509,0,0,1,256.484,512.615Z" transform="translate(0 -16.866)" fill="#7e829e" />
                                                <path id="Path_775" data-name="Path 775" d="M402.825,539.748l.01,0a4.119,4.119,0,0,1,3.347,3.346.6.6,0,0,0,.588.5h.006a.765.765,0,0,0,.1-.009.6.6,0,0,0,.493-.693,5.317,5.317,0,0,0-4.324-4.324.6.6,0,0,0-.692.486.594.594,0,0,0,.474.694Z" transform="translate(-151.391 -53.205)" fill="#7e829e" />
                                                <path id="Path_776" data-name="Path 776" d="M412.486,489.578c0-.007,0-.014,0-.02a8.656,8.656,0,0,0-7.045-7.039.607.607,0,0,0-.1-.008.6.6,0,0,0-.1,1.191,7.442,7.442,0,0,1,6.054,6.054.6.6,0,0,0,.588.5h.006a.733.733,0,0,0,.094-.008.59.59,0,0,0,.5-.667Z" transform="translate(-153.67)" fill="#7e829e" />
                                            </g>
                                        </svg>
                                    </span>
                                    <span className="l-c-ap-text">888-853-6141</span></li>
                                <li>
                                    <span className="l-c-ap-icon">
                                        <svg className="colaba-icon-email" id="Group_1247" data-name="Group 1247" xmlns="http://www.w3.org/2000/svg" width="17.455" height="12" viewBox="0 0 17.455 12">
                                            <path id="Path_773" data-name="Path 773" d="M17.443,81.032a.537.537,0,0,0-.022-.109A1.079,1.079,0,0,0,16.364,80H1.091a1.079,1.079,0,0,0-1.057.923.537.537,0,0,0-.022.109c0,.021-.012.038-.012.059v9.818c0,.021.011.039.012.06a.527.527,0,0,0,.022.108A1.079,1.079,0,0,0,1.091,92H16.364a1.079,1.079,0,0,0,1.057-.924.527.527,0,0,0,.022-.108c0-.02.012-.038.012-.059V81.091C17.455,81.07,17.444,81.053,17.443,81.032Zm-1.456.059-6.055,4.44-1.2.884-1.2-.883a0,0,0,0,1,0,0l-6.055-4.44Zm-14.9,1.077L6.319,86,1.091,89.833Zm.377,8.741,5.773-4.232,1.163.853a.546.546,0,0,0,.646,0l1.163-.853,5.773,4.232Zm14.9-1.076L11.136,86l5.228-3.833Z" transform="translate(0 -80)" fill="#7e829e" />
                                        </svg>
                                    </span>
                                    <span className="l-c-ap-text">kevin@texastrust.com</span>
                                </li>
                                <li>
                                    <span className="l-c-ap-icon">
                                        <svg className="colaba-icon-web" xmlns="http://www.w3.org/2000/svg" width="15.999" height="16" viewBox="0 0 15.999 16">
                                            <g id="global" transform="translate(-0.016 0.002)">
                                                <g id="Group_1249" data-name="Group 1249" transform="translate(0.016 -0.002)">
                                                    <path id="Path_777" data-name="Path 777" d="M14.537,3.38a.134.134,0,0,0,0-.015c-.02-.029-.045-.053-.065-.081Q14.246,2.979,14,2.7c-.046-.051-.091-.1-.138-.153q-.258-.276-.541-.526c-.035-.031-.067-.063-.1-.093A8.011,8.011,0,0,0,11.7.9L11.63.867a7.949,7.949,0,0,0-.743-.331C10.83.516,10.778.5,10.724.479q-.338-.122-.686-.213C9.969.247,9.9.229,9.831.213,9.6.159,9.373.119,9.14.086,9.069.075,9,.062,8.928.053a7.514,7.514,0,0,0-1.813,0C7.043.062,6.973.075,6.9.086c-.234.033-.465.074-.691.127C6.142.229,6.073.247,6,.266q-.35.091-.686.213L5.155.536a7.944,7.944,0,0,0-.743.331L4.345.9A8.013,8.013,0,0,0,2.823,1.924c-.035.03-.067.062-.1.093a7.434,7.434,0,0,0-.541.526c-.047.05-.093.1-.138.153q-.251.282-.474.588c-.021.028-.045.053-.065.081L1.5,3.38a7.95,7.95,0,0,0,0,9.236l.007.015c.02.029.045.053.065.081q.224.305.474.588c.046.051.091.1.138.153q.258.276.541.526c.035.031.067.063.1.093A8.011,8.011,0,0,0,4.34,15.1l.067.033a7.949,7.949,0,0,0,.743.331c.057.021.109.04.163.057q.338.122.686.213c.069.018.137.037.207.053.229.053.457.094.691.127.071.01.141.024.212.033a7.514,7.514,0,0,0,1.813,0c.071-.009.141-.022.212-.033.234-.033.465-.074.691-.127.07-.016.138-.035.207-.053q.351-.091.686-.213l.163-.057a7.944,7.944,0,0,0,.743-.331l.067-.033a8.013,8.013,0,0,0,1.517-1.024c.035-.03.067-.062.1-.093a7.277,7.277,0,0,0,.541-.526c.047-.05.093-.1.138-.153q.251-.282.474-.588c.021-.028.045-.053.065-.081l.007-.015A7.95,7.95,0,0,0,14.537,3.38Zm-.652,1.081a6.8,6.8,0,0,1,.964,2.965H11.425A10.666,10.666,0,0,0,11.1,5.41,10.271,10.271,0,0,0,13.884,4.462ZM9.473,1.3c.032.007.063.018.095.025a6.151,6.151,0,0,1,.606.171l.09.033q.295.1.58.229l.1.048q.273.13.533.281l.114.069q.247.151.48.323c.04.029.08.057.119.089a5.215,5.215,0,0,1,.438.368c.037.033.074.066.11.1.146.139.286.286.421.438.017.02.035.038.053.057a9.588,9.588,0,0,1-2.438.783A14.534,14.534,0,0,0,9.234,1.258C9.313,1.272,9.395,1.28,9.473,1.3Zm-3.72,6.13A9.537,9.537,0,0,1,6.067,5.6a16.379,16.379,0,0,0,1.951.114,16.416,16.416,0,0,0,1.953-.117,9.52,9.52,0,0,1,.313,1.832ZM10.285,8.57A9.537,9.537,0,0,1,9.97,10.4a16.729,16.729,0,0,0-3.9,0A9.522,9.522,0,0,1,5.753,8.57ZM8.019,1.512A13.805,13.805,0,0,1,9.612,4.484a15.129,15.129,0,0,1-1.593.085,15.243,15.243,0,0,1-1.591-.085A13.914,13.914,0,0,1,8.019,1.512ZM2.88,3.47c.134-.152.274-.3.421-.438.036-.034.074-.067.11-.1q.211-.193.438-.368c.039-.03.079-.057.119-.089q.233-.171.48-.323l.114-.069q.26-.152.533-.281l.1-.048q.285-.129.58-.229c.03-.011.057-.023.09-.033.2-.066.4-.121.606-.171.032-.007.063-.018.1-.025.078-.017.159-.025.239-.039A14.535,14.535,0,0,0,5.265,4.311a9.587,9.587,0,0,1-2.438-.783C2.845,3.508,2.863,3.49,2.88,3.47Zm-.727.992a10.27,10.27,0,0,0,2.779.948,10.666,10.666,0,0,0-.32,2.017H1.189A6.8,6.8,0,0,1,2.153,4.462Zm0,7.073A6.8,6.8,0,0,1,1.189,8.57H4.613a10.666,10.666,0,0,0,.32,2.017A10.269,10.269,0,0,0,2.153,11.535ZM6.565,14.7c-.032-.007-.063-.018-.095-.025a6.151,6.151,0,0,1-.606-.171l-.09-.033q-.295-.1-.58-.229l-.1-.048q-.273-.13-.533-.281l-.114-.069q-.247-.151-.48-.323c-.04-.029-.08-.057-.119-.089a5.215,5.215,0,0,1-.438-.368c-.037-.033-.074-.066-.11-.1-.146-.139-.286-.286-.421-.438-.017-.02-.035-.038-.053-.057a9.588,9.588,0,0,1,2.438-.783A14.534,14.534,0,0,0,6.8,14.739C6.724,14.725,6.643,14.717,6.565,14.7Zm1.454-.215a13.805,13.805,0,0,1-1.593-2.972,14.88,14.88,0,0,1,3.185,0h0A13.911,13.911,0,0,1,8.019,14.485Zm5.139-1.958c-.134.152-.274.3-.421.438-.036.034-.074.067-.11.1q-.211.193-.438.368l-.119.089q-.233.171-.48.323l-.114.069q-.26.151-.533.281l-.1.048q-.285.129-.58.229c-.03.011-.057.023-.09.033-.2.066-.4.121-.606.171-.032.007-.063.018-.1.025-.078.017-.159.025-.239.039a14.532,14.532,0,0,0,1.539-3.053,9.587,9.587,0,0,1,2.438.783C13.193,12.488,13.175,12.506,13.157,12.526Zm.727-.992a10.269,10.269,0,0,0-2.779-.948,10.666,10.666,0,0,0,.32-2.017h3.424A6.8,6.8,0,0,1,13.884,11.535Z" transform="translate(-0.016 0.002)" fill="#7e829e" />
                                                </g>
                                            </g>
                                        </svg>
                                    </span>
                                    <span className="l-c-ap-text">www.texastrust.com</span>
                                </li>
                            </ul>
                        </div>
                    }
                    <figure className="lca-figure"
                        // onMouseEnter={()=>{setLoadAnim(true);}} 
                        // onMouseLeave={()=>{setLoadAnim(false);}} 
                        onClick={() => { setStateHover(!stateHover); setLoadAnim(!loadAnim); }}
                    >
                        <img alt="" src={LocalDB.getLOImageUrl()} />
                    </figure>
                </div>
            }
        </div>
    )
}

export default Assessment



export const AssetsGifts:React.FC = () => {
    const [panel,setPanel] = useState<number>(0);
    return (
        <>
            <div className="accordion" id="accordionAssessment">
                <div className="card">
                    <div className="card-header" id="headingOne">
                        <h2 className="mb-0">
                            <button onClick={()=>setPanel(0)} className="btn btn-link btn-block text-left" type="button" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                What’s a “Cash Gift”?
                            </button>
                        </h2>
                    </div>
                    <div id="collapseOne" className={`collapse ${panel==0 && 'show'}`} aria-labelledby="headingOne" data-parent="#accordionAssessment">
                        <div className="card-body">
                            Grants are forms of down payment assistance from a government agency or private organization that homeowners never have to repay – essentially gifts of free money. The State of Texas, for example, has the Homes for Texas Heroes Home Loan Program which helps teachers, police officers, firefighters, and veterans purchase homes.
                        </div>
                    </div>
                </div>

                <div className="card">
                    <div className="card-header" id="headingTwo">
                        <h2 className="mb-0">
                            <button onClick={()=>setPanel(1)} className="btn btn-link btn-block text-left collapsed" type="button" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                What’s a “Grant”?
                            </button>
                        </h2>
                    </div>
                    <div id="collapseTwo" className={`collapse ${panel==1 && 'show'}`} aria-labelledby="headingTwo" data-parent="#accordionAssessment">
                        <div className="card-body">
                            A Gift of Equity occurs when a property is sold for a lower price than the current market value. This is common when family members or partners exchange property. Gifts of Equity can count towards your down payment, but require documentation.
                        </div>
                    </div>
                </div>
            </div>
        </>
    )
}