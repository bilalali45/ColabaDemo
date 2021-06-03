import { FileItem } from "./FileItem";

export class DocumentItem {
    constructor(public docName: string, public status: string, public expanded: boolean, public files: FileItem[]) {}

    toggleExpanded(value: boolean) {
        this.expanded = value;
    }

    addFiles(fileItems: FileItem[]) {
        this.files.push(...fileItems);
    }
}