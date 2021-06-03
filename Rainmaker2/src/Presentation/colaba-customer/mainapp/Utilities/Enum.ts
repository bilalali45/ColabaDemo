export enum Tenant2FaSetting {
    RequiredForAll = 1, // 2FA Required no skip
    InActiveForAll = 2, // 2FA not required don't show skip and no 2FA
    UserPrefrences = 3 // 2FA not required skip allowed
}