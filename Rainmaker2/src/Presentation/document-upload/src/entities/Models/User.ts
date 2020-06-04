import { LoanApplication } from "./LoanApplication";
import { Document } from "./Document";
import { Auth } from "../../services/auth/Auth";

interface UserInfo {
    name: string;
    userName: string;
    email: string
}

export class User {
    public auth: Auth | null = null;
    public info: UserInfo | null = null;
    public loanApplication: LoanApplication | null = null;
    public submittedDocuments: Document[] = [];

}