import { Document } from "./Document";
import { DocumentRequest } from "./DocumentRequest";

export class CategoryDocument {
    public catId: string;
    public catName: string;
    public documents: DocumentRequest[] = [];
    

    constructor(catId: string, catName: string, documents: DocumentRequest[]) {
        this.catId = catId;
        this.catName = catName;
        this.documents = documents;
    }
}