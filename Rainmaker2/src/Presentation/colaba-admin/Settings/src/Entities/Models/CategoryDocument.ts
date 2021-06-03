import { Template } from "./Template";
import { Document } from "./Document";

export class CategoryDocument {
    public catId: string;
    public catName: string;
    public documents: Document[] = [];

    constructor(catId: string, catName: string, documents: Document[]) {
        this.catId = catId;
        this.catName = catName;
        this.documents = documents;
    }
}