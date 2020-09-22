import { BeforeStartConfiguration } from "../../../../services/test_helpers/BeforeStart";

describe("Unit Testing Loan Status", () => {
  it("User should be able to login successfully", () => {
    beforeEach(() => {
      BeforeStartConfiguration();
    });

    // const { getByTestId, getByText } = render(<App />);
    // fireEvent.change(getByTestId("email"), {
    //   target: { value: "kane@wood.com" },
    // });
    // fireEvent.change(getByTestId("password"), { target: { value: "kane123" } });
    // fireEvent.click(getByTestId(`login-button`));
    // expect(getByText("Logout")).toBeInTheDocument();
  });
});
