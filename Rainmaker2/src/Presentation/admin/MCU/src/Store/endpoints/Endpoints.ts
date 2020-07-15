import { NeedListEndpoints } from "./NeedListEndpoints";
import { DocumentManagerEndpoints } from "./DocumentManagerEndpoints";
import { TemplateManagerEndpoints } from "./TemplateManagerEndpoints";
import { UserEndpoints } from "./UserEndpoints";

export class Endpoints {
  static NeedListManager = NeedListEndpoints;
  static TemplateManager = TemplateManagerEndpoints;
  static DocumentManager = DocumentManagerEndpoints;
  static User = UserEndpoints;
}
