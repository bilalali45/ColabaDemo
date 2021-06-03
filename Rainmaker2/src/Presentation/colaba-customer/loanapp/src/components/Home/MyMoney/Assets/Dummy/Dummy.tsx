import React from "react";
import IncomeModal from "../../Income/IncomeModal/IncomeModal";
import { NavigationHandler } from '../../../../../Utilities/Navigation/NavigationHandler';
import IconRadioBox from "../../../../../Shared/Components/IconRadioBox";
import {
    IconCheckingAccount,
    IconSavingsAccount,
    Iconcheck,
    IconMutualFunds,
    IconStocks2,
    IconMoneyMarket,
    IconStockOptions,
    IconCertificateOfDeposit,
    IconBonds,
    IconCashGift,
    IconGiftOfEquity
} from "../../../../../Shared/Components/SVGs";
import InputField from "../../../../../Shared/Components/InputField";
import DropdownList from "../../../../../Shared/Components/DropdownList";
import Dropdown from "react-bootstrap/Dropdown";
import IconRadioSnipit from "../../../../../Shared/Components/IconRadioSnipit";

import InputRadioBox from "../../../../../Shared/Components/InputRadioBox";


const Dummy = () => {
    
    return (
        <IncomeModal
            closePath=""
            title={`Select your source of income / <a href="">Mutual Funds</a>`}
            className="nothaveFooter colaba-form"
            handlerCancel={() => { NavigationHandler.closeWizard('/loanApplication/MyMoney/Assets/AssetsHome') }} >

            <div className="form-group">
                <h3 className="h3">What type of account is it?</h3>
            </div>

            <div className="row">
                <div className="col-md-6">
                    <IconRadioBox
                        id={1}
                        className=""
                        name="radio1"
                        //checked={false}
                        value={"Checking Account"}
                        title="Checking Account"
                        Icon={<IconCheckingAccount />}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioBox
                        id={1}
                        className=""
                        name="radio1"
                        //checked={false}
                        value={"Savings Account"}
                        title="Savings Account"
                        Icon={<IconSavingsAccount />}
                    />
                </div>
            </div>

            <div className="row">
                <div className="col-md-6">
                    <InputField
                        name={`Financial Institution`}
                        label={`Financial Institution`}
                        placeholder={`Financial Institution Name`}
                    />
                </div>
                <div className="col-md-6">
                    <InputField
                        name={`Account Number`}
                        label={`Account Number`}
                        placeholder={`***-***-XXXX`}
                    />
                </div>
                <div className="col-md-6">
                    <InputField
                        icon={<i className="zmdi zmdi-money"></i>}
                        name={`Current Balance`}
                        label={`Current Balance`}
                        placeholder={`00.00`}
                        value={`50.00`}
                    />
                    <span className="msg-succeed">
                        <Iconcheck /> Your Account Has Been Saved!
                    </span>
                </div>
            </div>


            <div className="form-footer">
                <button
                    data-testid="saveBtn"
                    disabled={true}
                    className="btn btn-primary float-right"
                    onClick={() => { }}
                >
                    {"Save Assets"}
                </button>
            </div>

            <div className="clearfix"></div>

            <hr className="form-group" />

            {/* Stocks, Bonds, or Other Financial Assets */}
            <div className="form-group">
                <h3 className="h3">What type of financial asset is this?</h3>
            </div>

            <div className="row">
                <div className="col-md-6">
                    <IconRadioBox
                        id={1}
                        className=""
                        name="radio1"
                        //checked={false}
                        value={"Mutual Funds"}
                        title="Mutual Funds"
                        Icon={<IconMutualFunds />}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioBox
                        id={1}
                        className=""
                        name="radio1"
                        //checked={false}
                        value={"Stocks"}
                        title="Stocks"
                        Icon={<IconStocks2 />}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioBox
                        id={1}
                        className=""
                        name="radio1"
                        //checked={false}
                        value={"Money Market"}
                        title="Money Market"
                        Icon={<IconMoneyMarket />}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioBox
                        id={1}
                        className=""
                        name="radio1"
                        //checked={false}
                        value={"Stock Options"}
                        title="Stock Options"
                        Icon={<IconStockOptions />}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioBox
                        id={1}
                        className=""
                        name="radio1"
                        //checked={false}
                        value={"Certificate Of Deposit"}
                        title="Certificate Of Deposit"
                        Icon={<IconCertificateOfDeposit />}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioBox
                        id={1}
                        className=""
                        name="radio1"
                        //checked={false}
                        value={"Bonds"}
                        title="Bonds"
                        Icon={<IconBonds />}
                    />
                </div>
            </div>

            <div className="form-footer">
                <button
                    data-testid="AddAnotherAssetBtn"
                    disabled={false}
                    className="btn btn-primary float-right"
                    onClick={() => { }}>Add another asset</button>
                <button
                    data-testid="AddAnotherRetirementAccountBtn"
                    disabled={false}
                    className="btn btn-secondary float-right"
                    onClick={() => { }}
                >Add Another Retirement Account
                </button>

            </div>

            <div className="clearfix"></div>

            <hr className="form-group" />

            <div className="row">
                <div className="col-md-6">
                    <DropdownList
                        id="dropdown-item-button"
                        title="Dropdown button"
                        label={"What type of asset is this?"}
                        placeholder={`Account Type`}
                        onDropdownSelect={() => { }}>
                        <Dropdown.ItemText>Dropdown item text</Dropdown.ItemText>
                        <Dropdown.Item as="button">Action</Dropdown.Item>
                        <Dropdown.Item as="button">Another action</Dropdown.Item>
                        <Dropdown.Item as="button">Something else</Dropdown.Item>
                    </DropdownList>
                </div>
            </div>

            <div className="row">
                <div className="col-md-6">
                    <InputField
                        name={`Financial Institution`}
                        label={`Financial Institution`}
                        placeholder={`Business Name Here`}
                        value={``}
                    />
                </div>
                <div className="col-md-6">
                    <InputField
                        name={`Account Number`}
                        label={`Account Number`}
                        placeholder={`***-***-XXXX`}
                        value={``}
                    />
                </div>
                <div className="col-md-6">
                    <InputField
                        icon={<i className="zmdi zmdi-money"></i>}
                        name={`Cash or Market Value`}
                        label={`Cash or Market Value`}
                        placeholder={`Amount`}
                        value={``}
                    />
                </div>
            </div>

            <div className="form-footer">
                <button
                    data-testid="saveBtn"
                    disabled={true}
                    className="btn btn-primary float-right"
                    onClick={() => { }}
                >
                    {"Save Assets"}
                </button>
            </div>

            <div className="clearfix"></div>

            <hr className="form-group" />

            <div className="form-group">
                <h3 className="h3">Where Is This Gift From?</h3>
            </div>

            <div className="row">
                <div className="col-md-6">
                    <IconRadioSnipit
                        title={`Relative`}
                        id={0}
                        icon={<IconMutualFunds />}
                        handlerClick={()=>{}}
                        className={"active"}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioSnipit
                        title={`Unmarried Partner`}
                        id={0}
                        icon={<IconMutualFunds />}
                        handlerClick={()=>{}}
                        className={""}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioSnipit
                        title={`Federal Agency`}
                        id={0}
                        icon={<IconMutualFunds />}
                        handlerClick={()=>{}}
                        className={""}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioSnipit
                        title={`State Agency`}
                        id={0}
                        icon={<IconMutualFunds />}
                        handlerClick={()=>{}}
                        className={""}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioSnipit
                        title={`Local Agency`}
                        id={0}
                        icon={<IconMutualFunds />}
                        handlerClick={()=>{}}
                        className={""}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioSnipit
                        title={`Community Non Profit`}
                        id={0}
                        icon={<IconMutualFunds />}
                        handlerClick={()=>{}}
                        className={""}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioSnipit
                        title={`Employer`}
                        id={0}
                        icon={<IconMutualFunds />}
                        handlerClick={()=>{}}
                        className={""}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioSnipit
                        title={`Religious Non Profit`}
                        id={0}
                        icon={<IconMutualFunds />}
                        handlerClick={()=>{}}
                        className={""}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioSnipit
                        title={`Lender`}
                        id={0}
                        icon={<IconMutualFunds />}
                        handlerClick={()=>{}}
                        className={``}
                    />
                </div>
            </div>


            <div className="clearfix"></div>
            <hr className="form-group" />

            <div className="form-group">
                <h3 className="h3">What type of gift was this?</h3>
            </div>

            <div className="row">
                <div className="col-md-6">
                    <IconRadioBox
                        id={1}
                        className=""
                        name="radio1"
                        //checked={false}
                        value={"Cash Gift"}
                        title="Cash Gift"
                        Icon={<IconCashGift />}
                    />
                </div>
                <div className="col-md-6">
                    <IconRadioBox
                        id={1}
                        className=""
                        name="radio1"
                        //checked={false}
                        value={"Gift Of Equity"}
                        title="Gift Of Equity"
                        Icon={<IconGiftOfEquity />}
                    />
                </div>
            </div>

            <div className="row">
                <div className="col-md-6">
                    <InputField
                        icon={<i className="zmdi zmdi-money"></i>}
                        name={`Cash Value`}
                        label={`Cash Value`}
                        placeholder={`Amount`}
                        value={`50.00`}
                    />
                    <span className="msg-succeed">
                        <Iconcheck /> Your Account Has Been Saved!
                    </span>
                </div>
            </div>

            <div className="form-group">
                <h2>Has this gift been deposited already?</h2>
            </div>

            <div className="clearfix">
                <InputRadioBox
                    id=""
                    className=""
                    name="addressCheck"
                    //   register={register2}
                    value={"Yes"}
                    rules={{
                        required: "Please select one"
                    }}
                //   onChange={() => { setSelectedOption(true) }}
                //   errors={errors2}
                >Yes</InputRadioBox>
            </div>

            <div className="clearfix">
                <InputRadioBox
                    id=""
                    className=""
                    name="addressCheck"
                    //   register={register2}
                    //   onChange={() => { setSelectedOption(false) }}
                    value={"No"}
                    rules={{
                        required: "Please select one"
                    }}
                //   errors={errors2}
                >No</InputRadioBox>
            </div>

            <div className="form-group">
                <h2>Expected Date of Transfer</h2>
            </div>

            <div className={`intended-feild`}>
                            {/* {<Controller
                            //   control={control}
                              name="lastDateOfTourOrService"
                              render={({ onChange, onBlur, value }) => (
                                
                              )}
                            />} */}

{/* <InputDatepicker
                                  label={`Expected Date of Transfer`}
                                  dateFormat="MM/dd/yyyy"
                                  name="ExpectedDateofTransfer"
                                  handleOnChange={()=>{}}
                                  autoComplete={'off'}
                                  selected={``}
                                  isPreviousDateAllowed={false}
                                  handleOnChangeRaw={(e) => { e.preventDefault(); }}
                                //   errors={errors}
                                /> */}
                          </div>



        </IncomeModal>
    )
}

export default Dummy;