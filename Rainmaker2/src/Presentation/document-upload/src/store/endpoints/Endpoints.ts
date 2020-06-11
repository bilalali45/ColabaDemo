import {LoanEndpoints} from './LoanEndpoints'
import {DocumentsEndpoints} from './DocumentsEndpoints'
import {UserEndpoints} from './UserEndpoints'

export class Endpoints {
    static user = UserEndpoints;
    static loan = LoanEndpoints;
    static documents = DocumentsEndpoints;
}