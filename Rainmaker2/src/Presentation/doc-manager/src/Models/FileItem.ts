export class FileItem {
    constructor(
        public fileName: string,
        public dateModified: string,
        public hidden: boolean,
        public visibleToBorrower: boolean,
        public syncStatus: string,
        public isSynching: boolean) { }

    toggleVisibleToBorrower(value: boolean) {
        this.visibleToBorrower = value;
    }

    toggleVisibility(value: boolean) {
        this.hidden = value;
    }

    toggleIsSynching(value: boolean) {
        this.isSynching = value;
    }

    updateSyncStatus(value: string) {
        this.syncStatus = value;
    }

}