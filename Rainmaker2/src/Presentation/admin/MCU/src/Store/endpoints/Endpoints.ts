import { NeedListEndpoints } from "./NeedListEndpoints";
import { DocumentManagerEndpoints } from "./DocumentManagerEndpoints";
import { TemplateManagerEndpoints } from "./TemplateManagerEndpoints";
import { UserEndpoints } from "./UserEndpoints";

export class Endpoints {
  static NeedList = NeedListEndpoints;
  static TemplateManager = TemplateManagerEndpoints;
  static DocumentManager = DocumentManagerEndpoints;
  static User = UserEndpoints;
}
