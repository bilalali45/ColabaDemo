import axios from 'axios';
import { LocalDB } from '../../Utils/LocalDB';

export class UserActions {
    static async authorizeOnlyForDev() {
        try {
            let res : any = await axios.post(`https://alphamaingateway.rainsoftfn.com/api/Identity/token/authorize`, LocalDB.getCredentials());
            let {token, refreshToken} = res.data.data;
            LocalDB.storeAuthTokens(token, refreshToken);
            return res.data.data;
        } catch (error) {
            console.log(error);
        }
    }
}