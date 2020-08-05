export class DocumentRequest {
    constructor(
        public typeId: string, 
        public displayName: string, 
        public message: string,
        public docId: string,
        public requestId: string) {}
}