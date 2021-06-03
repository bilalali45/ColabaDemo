import { UrlQueryManager } from "../Utilities/Navigation/UrlQueryManager";
import { LocalDB } from "./LocalDB";


describe("LocalDB", () => {
  beforeEach(() => {
    Object.defineProperty(window, "localStorage", {
      value: {
        getItem: jest.fn(() => null),
        setItem: jest.fn((name) => name),
        removeItem: jest.fn(() => null)
      },
      writable: true,
    });

    Object.defineProperty(window, "sessionStorage", {
      value: {
        getItem: jest.fn(() => null),
        setItem: jest.fn(() => null),
        removeItem: jest.fn(() => null),
      },
      writable: true,
    });
  });



  test("should get loanApplicatin id from session storage", async () => {
    LocalDB.getLoanAppliationId();
    expect(UrlQueryManager.getQuery).toHaveBeenCalledWith("loanApplicationId");
  });

  // test("should set loanApplication is to session storage", async () => {
  //   const loanApplicationId = "1";
  //   LocalDB.setLoanAppliationId(loanApplicationId);
  //   expect(sessionStorage.setItem).toHaveBeenCalledWith(
  //     "loanApplicationId",
  //     "MXxSYWlubWFrZXJOb3RpZmljYXRpb24yMDIwfA=="
  //   );
  // });

  // test("should get borrower id from session storage", async () => {
  //   LocalDB.getBorrowerId();
  //   expect(sessionStorage.getItem).toHaveBeenCalledWith("borrowerId");
  // });

  // test("should set borrower is to session storage", async () => {
  //   const borrowerId = "1";
  //   LocalDB.setBorrowerId(borrowerId);
  //   expect(sessionStorage.setItem).toHaveBeenCalledWith(
  //     "borrowerId",
  //     "MXxSYWlubWFrZXJOb3RpZmljYXRpb24yMDIwfA=="
  //   );
  // });


  // test("should get loanPurpose id from session storage", async () => {
  //   LocalDB.getLoanPurposeId();
  //   expect(sessionStorage.getItem).toHaveBeenCalledWith("loanPurposeId");
  // });

  // test("should set loanPurpose is to session storage", async () => {
  //   const loanPurposeId = "1";
  //   LocalDB.setLoanPurposeId(loanPurposeId);
  //   expect(sessionStorage.setItem).toHaveBeenCalledWith(
  //     "loanPurposeId",
  //     "MXxSYWlubWFrZXJOb3RpZmljYXRpb24yMDIwfA=="
  //   );
  // });

  // test("should get loanGoalId id from session storage", async () => {
  //   LocalDB.getLoanGoalId();
  //   expect(sessionStorage.getItem).toHaveBeenCalledWith("loanGoalId");
  // });

  // test("should set loanGoal is to session storage", async () => {
  //   const loanGoalId = "1";
  //   LocalDB.setLoanGoalId(loanGoalId);
  //   expect(sessionStorage.setItem).toHaveBeenCalledWith(
  //     "loanGoalId",
  //     "MXxSYWlubWFrZXJOb3RpZmljYXRpb24yMDIwfA=="
  //   );
  // });


  // test("should remove loanApplicationId from local storage", async () => {
  //   LocalDB.clearLoanApplicationFromStorage();
  //   expect(sessionStorage.removeItem).toHaveBeenCalledTimes(1);
  // });

  // test("should remove loanPurposeId from session storage", async () => {
  //   LocalDB.clearLoanPurposeFromStorage();
  //   expect(sessionStorage.removeItem).toHaveBeenCalledTimes(1);
  // });

  // test("should remove loanGoalId from session storage", async () => {
  //   LocalDB.clearLoanGoalFromStorage();
  //   expect(sessionStorage.removeItem).toHaveBeenCalledTimes(1);
  // });

  // test("should remove borrowerId from session storage", async () => {
  //   LocalDB.clearBorrowerFromStorage();
  //   expect(sessionStorage.removeItem).toHaveBeenCalledTimes(1);
  // });

  // test("should remove all from session storage", async () => {
  //   LocalDB.clearSessionStorage();
  //   expect(sessionStorage.removeItem).toHaveBeenCalledTimes(4);
  // });


  // test("should set item is to local storage", async () => {
  //   const name = "test";
  //   const value = "test";
  //   LocalDB.storeItem(name, value);
  //   expect(localStorage.setItem).toHaveBeenCalledWith(
  //     "test",
  //     "dGVzdHxSYWlubWFrZXJOb3RpZmljYXRpb24yMDIwfA=="
  //   );
  // });

//   test("should remove LocalDB from local storage", async () => {
//     LocalDB.removeAuth();
//     expect(sessionStorage.removeItem).toHaveBeenCalledTimes(3);
//   });


});
