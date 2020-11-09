export class DashboardSetting {
    public userId: number;
    public pending: boolean;
    
    constructor(userId: number, pending: boolean) {
        this.userId = userId;
        this.pending = pending;
    }
}