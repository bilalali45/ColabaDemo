
import { UserEndpoints } from "./UserEndpoints";
import { TemplateManagerEndpoints } from "./TemplateManagerEndpoints"
import { DocumentManagerEndpoints } from "./DocumentManagerEndpoints";
import { NotificationEndpoints } from "./NotificationEndpoints";
import { AssignedRoleEndpoints } from "./AssignedRoleEndpoints";
import { RequestEmailTemplateEndpoints } from "./RequestEmailTemplateEndpoints";
import { OrganizationEndpoints } from "./OrganizationEndPoint";
import { LoanOfficersEndpoints } from "./LoanOfficersEndpoints";
import { ReminderEmailListEndpoints } from "./ReminderEmailListEndPoints";
import { LoanStatusUpdateEndPoints } from "./LoanStatusUpdateEndpoints";


export class Endpoints {

  static User = UserEndpoints;
  static TemplateManager = TemplateManagerEndpoints;
  static DocumentManager = DocumentManagerEndpoints;
  static NotificationManger = NotificationEndpoints;
  static AssignedRoleManager = AssignedRoleEndpoints;
  static RequestEmailTemplateManager = RequestEmailTemplateEndpoints;
  static LoanOfficersManager = LoanOfficersEndpoints
  static OrganizationManager = OrganizationEndpoints;
  static ReminderEmailListManager= ReminderEmailListEndpoints;
  static LoanStatusUpdateManager = LoanStatusUpdateEndPoints;
}
