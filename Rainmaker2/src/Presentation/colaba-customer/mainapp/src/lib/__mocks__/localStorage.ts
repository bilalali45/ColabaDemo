// import { ApplicationEnv } from "./appEnv";
export class LocalDB {
    static getCredentials(): {
        userName: string | null;
        password: string | null;
        employee: boolean;
    } {
        // const credentials = {
        //     userName: LocalDB.getLoginDevUserName(),
        //     password: LocalDB.getLoginDevPassword(),
        //     employee: true,
        // };

        return {userName: 'john', password: 'doe', employee: false};
    }

    static getCookie(cookieName: string): string {
        return cookieName
    }

    static setCookie(name: string, value: string, path: string): void {
        document.cookie = name + "=" + value + ";path=" + path;
    }



    //#region Local DB get methods
    static getAuthToken(): string | null {
        return this.decodeString(this.getCookie("Rainmaker2Token"));
    }

    static getRefreshToken(): string | null {
        return this.decodeString(this.getCookie("Rainmaker2RefreshToken"));
    }

    static getCookiePath(): string | null {
        return (
            this.decodeString(window.sessionStorage.getItem("CookiePath")) ||
            "/"
          );
    }

    static getLoginDevUserName(): string | null {
        return localStorage.getItem("devusername");
    }

    static getLoginDevPassword(): string | null {
        return localStorage.getItem("devuserpassword");
    }

    static getUserPayload(): any {
        const payload = this.decodeString(this.getCookie("TokenPayload"));

        if (payload) {
            return JSON.parse(payload);
        }

        return null;
    }

    //#endregion



    static storeAuthTokens(token: string, refreshToken: string, path: string): void {
        this.setCookie("Rainmaker2Token", this.encodeString(token), path);
        this.setCookie(
            "Rainmaker2RefreshToken",
            this.encodeString(refreshToken),
            path
        );
    }

    static storeCookiePath(path: string): void {
        window.sessionStorage.setItem(
            "CookiePath",
            this.encodeString(path)
          );
    }

    public static setlocalStorage(name: string, data: string): void {
        localStorage.setItem(name, this.encodeString(data));
    }
    //#endregion


    //#region Encode Decode
    public static encodeString(value: string): string {
        return value
        // Encode the String
        //const currentDate = Date.toString();
        // const string = value + "|" + ApplicationEnv.Encode_Key;
        // return btoa(unescape(encodeURIComponent(string)));
    }

    public static decodeString(value?: string | null): string | null {
        // Decode the String
        if (!value) {
            return "";
        }
        try {
            const decodedString = atob(value);
            return String(decodedString.split("|")[0]);
        } catch {
            return null;
        }
    }
    //#endregion

    public static async getcaptchaCode(callBack: Function, params: any) {
        let token = "03AGdBq25DfiGUnLUtIrKGQv-HRuhwHaXDoiQR_evktPcoMVNm4vbF0coDTLjm5KNdLmJn-TebM4FsgINeruDkS5gS4U7rtfPQN54GsLq7Dj1kfyiLAOfSeDyekYKYO5O-_BY0hMPUzQFy6gYOM8GpfXW34PP1XquYPSA-CgdK0T0zjY_Ptv2s7wapUS7KHoaCz2ofQQxUDTYoEupYP4rynLDnJ6yT6_lwq-VEzSLFc3r-17A27nnswQxuM0a7seveawlliq9tNz1_TmU4c6HTwX6C1MUmZmHUcACumv_OFGbHrsTPN29c_47b7aeEyXRNC8oAkp-Y2Thn_OkmPLP0Hc6YNyfy2LXMnAu-Mi1FS9OXEoiYK7VKsMtavEKqp0ZBNWl7qwCYapjwsnCIw6lLfAa7W2XSxAEnCOYZCB8eSceFputqTluU8tk"
         let test = callBack(params, token)
         return test
    }
}
