import { NeedListEndpoints } from "./NeedListEndpoints";
import { DocumentManagerEndpoints } from "./DocumentManagerEndpoints";
import { TemplateManagerEndpoints } from "./TemplateManagerEndpoints";
import { UserEndpoints } from "./UserEndpoints";
import { NewNeedListEndpoints } from "./NewNeedListEndpoints";
import { RequestEmailTemplateEndpoints } from "./RequestEmailTemplateEndpoints";

export class Endpoints {
  static NeedListManager = NeedListEndpoints;
  static NewNeedList = NewNeedListEndpoints;
  static TemplateManager = TemplateManagerEndpoints;
  static DocumentManager = DocumentManagerEndpoints;
  static User = UserEndpoints;
  static RequestEmailTemplateManager = RequestEmailTemplateEndpoints;
}
