import { Auth } from "../services/auth/Auth";

const location = window.location;

export class ParamsService {
    static params = location.search.split('&');

    static getParam(param: string) {
       return this.params.find(p => p?.includes(param))?.split('=')[1];
    }

    static storeParams(params: string[]) {
        for (const param of params) {
            let extractedParam = this.getParam(param);
            if(extractedParam) {
                Auth.soreItem(param, extractedParam);
            }
        }
    }
}