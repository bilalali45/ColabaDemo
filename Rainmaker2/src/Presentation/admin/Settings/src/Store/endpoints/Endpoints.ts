
import { UserEndpoints } from "./UserEndpoints";
import { TemplateManagerEndpoints } from "./TemplateManagerEndpoints"
import { DocumentManagerEndpoints } from "./DocumentManagerEndpoints";
import { NotificationEndpoints } from "./NotificationEndpoints";
import { AssignedRoleEndpoints } from "./AssignedRoleEndpoints";
import { RequestEmailTemplateEndpoints } from "./RequestEmailTemplateEndpoints";


export class Endpoints {

  static User = UserEndpoints;
  static TemplateManager = TemplateManagerEndpoints;
  static DocumentManager = DocumentManagerEndpoints;
  static NotificationManger = NotificationEndpoints;
  static AssignedRoleManager = AssignedRoleEndpoints;
  static RequestEmailTemplateManager = RequestEmailTemplateEndpoints;
}
