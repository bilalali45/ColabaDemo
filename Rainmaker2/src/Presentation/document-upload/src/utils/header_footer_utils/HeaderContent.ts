import { UserActions } from "../../store/actions/UserActions";

export default class HeaderContent {
  static gotoDashboardHandler = () => {
    window.open("/Dashboard", "_self");
  };
  static changePasswordHandler = () => {
    window.open("/Account/ManagePassword", "_self");
  };
  static signOutHandler = () => {
    UserActions.logout();
    window.open("/Account/LogOff?directLogOff=true", "_self");
  };

  static headerDropdowmMenu = [
    { name: "Dashboard", callback: HeaderContent.gotoDashboardHandler },
    { name: "Change Password", callback: HeaderContent.changePasswordHandler },
    { name: "Sign Out", callback: HeaderContent.signOutHandler },
  ];
}
