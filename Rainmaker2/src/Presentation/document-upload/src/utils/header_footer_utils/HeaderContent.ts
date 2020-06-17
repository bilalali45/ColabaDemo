import { UserActions } from "../../store/actions/UserActions";


export default class HeaderContent {

      static gotoDashboardHandler = () => {
        window.open('/Dashboard', '_self');
      };
      static changePasswordHandler = () => {
        window.open('/Account/SendResetPasswordRequest', '_self');
      };
      static signOutHandler = () => {
        UserActions.logout();
        window.open('/Account/Login', '_self');
      };



      static headerDropdowmMenu = [
        { name: 'Dashboard', callback: HeaderContent.gotoDashboardHandler },
        { name: 'Change Password', callback: HeaderContent.changePasswordHandler },
        { name: 'Sign Out', callback: HeaderContent.signOutHandler }
      ]
}