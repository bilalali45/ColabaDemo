import React from "react";
import { fireEvent, render, waitFor } from "@testing-library/react";
import { MemoryRouter } from "react-router-dom";
import { act } from "react-dom/test-utils";
import { LoanAmountDetail } from "./LoanAmountDetail";
import { CommaFormatted } from "../../../../Utilities/helpers/CommaSeparteMasking";

jest.mock('react-datepicker');
jest.mock('react-datepicker/dist/react-datepicker');
jest.mock('../../../../store/actions/MyNewMortgageActions');
jest.mock('../../../../lib/LocalDB');


describe("My New Mortgage Steps ", () => {
  test("Should render loan Amount Detail", async () => {
    const { getByTestId, getAllByTestId, container } = render(
      <LoanAmountDetail />
    );

    await waitFor(() => {
        const header = getByTestId("head");
        expect(header).toHaveTextContent("Loan Information");

        let tooltip = getByTestId("tooltip")
        expect(tooltip).toHaveTextContent("Tell us about the loan you would like to obtain. If you don't know the exact amount, an estimate is fine.");
        
        expect(getByTestId("loanAmount-form")).toBeInTheDocument();       
        expect(getByTestId("purchase-price")).toBeInTheDocument();
        expect(getByTestId("down-payement")).toBeInTheDocument();
        expect(getByTestId("percent")).toBeInTheDocument();

        const purchaseInput: any = container.querySelector(
            "input[name='PurchasePrice']"
          );
          if(purchaseInput){         
            expect(purchaseInput.value).toEqual(CommaFormatted("500000"));
          }   
          
          const downPaymentInput: any = container.querySelector(
            "input[name='DownPayment']"
          );
          if(downPaymentInput){         
            expect(downPaymentInput.value).toEqual(CommaFormatted("150000"));
          }  

        });

        expect(getByTestId("downpaymentHasGift-question")).toBeInTheDocument();
        expect(getByTestId("downpaymentHasGift-question").children[0]).toHaveTextContent('Is any part of the down payment a gift?');
        expect(getByTestId("downpaymentHasGift-question-div")).toBeInTheDocument();
    
        let downpaymentHasGiftYesRadioBox = getByTestId('downpaymentHasGift-yes');
        expect(downpaymentHasGiftYesRadioBox).toBeInTheDocument();

        let downpaymentHasGiftNoRadioBox = getByTestId('downpaymentHasGift-no');
        expect(downpaymentHasGiftNoRadioBox).toBeInTheDocument();
        
        expect(downpaymentHasGiftYesRadioBox).toBeChecked();
        expect(downpaymentHasGiftNoRadioBox).not.toBeChecked();

        const purchaseInput: any = container.querySelector(
            "input[name='PurchasePrice']"
          );
          if(purchaseInput){
            fireEvent.input(purchaseInput, {
              target: {
                value: "5000000"
              }
            });
          }
       
          await waitFor(() => {
            const purchaseInput: any = container.querySelector(
                "input[name='PurchasePrice']"
              );
              if(purchaseInput){         
                expect(purchaseInput.value).toEqual(CommaFormatted("5000000"));
              }  
          })

          const downPaymentInput: any = container.querySelector(
            "input[name='DownPayment']"
          );  
          if(downPaymentInput){
            fireEvent.input(downPaymentInput, {
              target: {
                value: "80000"
              }
            });
          }

          await waitFor(() => {
            const purchaseInput: any = container.querySelector(
                "input[name='PurchasePrice']"
              );
              if(purchaseInput){         
                expect(purchaseInput.value).toEqual(CommaFormatted("5000000"));
              }  
          })


          const percentInput: any = container.querySelector(
            "input[name='percent']"
          );  
          if(percentInput){
            fireEvent.input(percentInput, {
              target: {
                value: "45"
              }
            });
          }

          let saveBtn = getByTestId('saveBtn');
          expect(saveBtn).toBeInTheDocument();

          fireEvent.click(saveBtn);

  });

       

});
