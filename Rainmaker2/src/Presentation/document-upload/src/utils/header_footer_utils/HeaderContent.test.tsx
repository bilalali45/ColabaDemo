import React from 'react'
import HeaderContent, {} from './HeaderContent'

describe("App", () => {
    beforeEach(() => {
        window.open = jest.fn();
    });  

    test("should go to  Dashboard", async () => {
        HeaderContent.gotoDashboardHandler()
        expect(window.open).toBeCalled();
      });

      test("should go to  Manage Password", async () => {
        HeaderContent.changePasswordHandler()
        expect(window.open).toBeCalled();
      });


      test("should call sigout ", async () => {
        HeaderContent.signOutHandler()
        expect(window.open).toBeCalled();
      });
});