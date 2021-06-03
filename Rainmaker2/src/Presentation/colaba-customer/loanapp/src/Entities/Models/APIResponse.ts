export class APIResponse<T> {
    public statusCode: number;
    public data: T;

    constructor(statusCode: number, data: T) {
        this.statusCode = statusCode;
        this.data = data;
    }
}