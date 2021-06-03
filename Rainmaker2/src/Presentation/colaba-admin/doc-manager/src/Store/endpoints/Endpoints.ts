import { DocumentEndpoints } from "./DocumentEndpoints";
import { TemplateManagerEndpoints } from "./TemplateManagerEndpoints";
import { TrashEndpoints } from "./TrashEndpoints";
import { UserEndpoints } from "./UserEndpoints";
import { WorkbenchEndpoints } from "./WorkbenchEndpoints";


export class Endpoints {
  static User = UserEndpoints;
  static Trash = TrashEndpoints;
  static TemplateManager = TemplateManagerEndpoints;
  static Document = DocumentEndpoints;
  static WorkBench = WorkbenchEndpoints;
}
