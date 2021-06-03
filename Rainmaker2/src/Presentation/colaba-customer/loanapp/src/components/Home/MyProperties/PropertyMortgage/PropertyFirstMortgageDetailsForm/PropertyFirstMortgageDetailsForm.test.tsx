import React from "react";
import {
    act,
    fireEvent,
    render,
    waitFor
} from "@testing-library/react";
import '@testing-library/jest-dom' // https://github.com/testing-library/jest-dom
import { PropertyFirstMortgageDetailsForm } from "./PropertyFirstMortgageDetailsForm";
import { MockEnvConfig } from "../../../../../test_utilities/EnvConfigMock";
import { MockSessionStorage } from "../../../../../test_utilities/SessionStoreMock";
import { NavigationHandler } from "../../../../../Utilities/Navigation/NavigationHandler";
import { Store } from "../../../../../store/store";
import { CommonTestMethods } from "../../../../../test_utilities/CommonTestMethods";

// Defining state for component
const state = {
    leftMenu: {
        navigation: null,
        leftMenuItems: [],
        notAllowedItems: [],
    },
    error: {},
    loanManager: {
        primaryBorrowerInfo: {
            id: 10790,
            name: "Lisa",
        },
    },
    commonManager: {},
    employment: {},
    business: {},
    employmentHistory: {},
    militaryIncomeManager: {},
    otherIncomeManager: {},
    assetsManager: {}
};

const dispatch = jest.fn();

let propsData = {
    firstPayment: "some",
    setFirstPayment: (data) => {
        console.log("! Setting first payment")
        propsData.firstPayment = data
    },
    firstPaymentBalance: null,
    setFirstPaymentBalance: (data) => { console.log("! Setting first balance") },
    propTax: null,
    setPropTax: (data) => { console.log("! Setting prop tax") },
    isTaxIncInPayment: null,
    setIsTaxIncludedInPayment: (data) => { console.log("! Setting isTaxIncInPayment") },
    propInsurance: null,
    setPropInsurance: (data) => { console.log("! Setting propInsurance") },
    isInsuranceIncInPayment: null,
    setIsInsuranceIncludedInPayment: (data) => { console.log("! Setting isInsuranceIncInPayment") },
    isHELOC: true,
    setIsHELOC: (data) => { console.log("! Setting isHELOC") },
    creditLimit: null,
    setCreditLimit: (data) => { console.log("! Setting creditLimit") },
    onSave: (data) => { console.log("!! On Save invoked") },
    homeAddress: null,
    floodIns: null,
    setFloodIns: (data) => { console.log("! Setting floodIns") },
    showPaidOff: true,
    isPaidOff: null,
    setIsPaidOff: (data) => { console.log("! Setting isPaidOff") },
    isFloodInsuranceIncInPayment: null,
    setIsFloodInsuranceIncludedInPayment: (data) => { console.log("! Setting isFloodInsuranceIncInPayment") },
}

// Used to render component by loading data. Call on each it/test
const initiateComponent = async () => {
    var { getByTestId, getAllByTestId } = render(
        <Store.Provider value={{ state, dispatch }}>
            <PropertyFirstMortgageDetailsForm
                firstPayment={propsData.firstPayment}
                setFirstPayment={propsData.setFirstPayment}
                firstPaymentBalance={propsData.firstPaymentBalance}
                setFirstPaymentBalance={propsData.setFirstPaymentBalance}
                propTax={propsData.propTax}
                setPropTax={propsData.setPropTax}
                isTaxIncInPayment={propsData.isTaxIncInPayment}
                setIsTaxIncludedInPayment={propsData.setIsTaxIncludedInPayment}
                propInsurance={propsData.propInsurance}
                setPropInsurance={propsData.setPropInsurance}
                isInsuranceIncInPayment={propsData.isInsuranceIncInPayment}
                setIsInsuranceIncludedInPayment={propsData.setIsInsuranceIncludedInPayment}
                setIsHELOC={propsData.setIsHELOC}
                isHELOC={propsData.isHELOC}
                creditLimit={propsData.creditLimit}
                setCreditLimit={propsData.setCreditLimit}
                onSave={propsData.onSave}
                homeAddress={propsData.homeAddress}
                floodIns={propsData.floodIns}
                setFloodIns={propsData.setFloodIns}
                showPaidOff={propsData.showPaidOff}
                isPaidOff={propsData.isPaidOff}
                setIsPaidOff={propsData.setIsPaidOff}
                isFloodInsuranceIncInPayment={propsData.isFloodInsuranceIncInPayment}
                setIsFloodInsuranceIncludedInPayment={propsData.setIsFloodInsuranceIncludedInPayment}
            />
        </Store.Provider>
    );

    return { getByTestId, getAllByTestId };
};


// Used to render component by loading data. Call on each it/test
beforeEach(async () => {
    MockEnvConfig();
    MockSessionStorage();
    NavigationHandler.getNavigationStateAsString = jest.fn(() => "{MockNavigationString: true}");
    NavigationHandler.navigateToPath = jest.fn((path) => { console.log("Navigated to path: " + path) });
    NavigationHandler.moveNext = jest.fn(() => { console.log("Moved Next") });
    NavigationHandler.isFieldVisible = jest.fn((tenantConfigEnums) => {
        return true;
    });
});

describe("My Properties > Property First Mortgage Details Form", () => {
    it("Should Render and fill all fields", async () => {

        let { getByTestId, getAllByTestId } = await initiateComponent();
        await CommonTestMethods.RenderInputwithValue(getByTestId, "first_Payment", "600");
        await CommonTestMethods.RenderInputwithValue(getByTestId, "first_pay_bal", "600");
        await CommonTestMethods.RenderInputwithValue(getByTestId, "prop_tax", "600");

        await CommonTestMethods.RenderInputwithValue(getByTestId, "prop_insurance", "600");

        await CommonTestMethods.RenderInputwithValue(getByTestId, "flood_insurance", "600");
        await CommonTestMethods.RenderInputwithValue(getByTestId, "credit_limit", "600");

        await CommonTestMethods.RenderCheckboxandClick(getByTestId, "tax_inc_in_pay");
        await CommonTestMethods.RenderCheckboxandClick(getByTestId, "ins_inc_in_pay");
        await CommonTestMethods.RenderCheckboxandClick(getByTestId, "flood_ins_inc_in_pay");
        await CommonTestMethods.RenderCheckboxandClick(getByTestId, "heloc");

        await waitFor(() => {
            var paid_yes = getByTestId("paid_yes");
            act(() => {
                fireEvent.click(paid_yes);
            });
        });

        await waitFor(() => {
            var saveBtn = getByTestId("first_mortgage_save");
            act(() => {
                fireEvent.click(saveBtn);
            });
        });

    });
});