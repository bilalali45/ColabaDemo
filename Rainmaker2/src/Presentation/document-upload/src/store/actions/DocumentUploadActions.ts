import { Http } from "../../services/http/Http";

const httpClient = new Http();

export class DocumentUploadActions {

    static async uploadFile(files: File[], url: string, getUploadProgress: Function) {
        
        const data = new FormData();

        for (const file of files) {
            data.append('file', file, 'changed file name');
        }
        
        try {
            let res = await httpClient.fetch({
                method: httpClient.methods.POST,
                url,
                data,
                onUploadProgress: e => {
                    let p = (e.loaded / e.total * 100);
                    getUploadProgress(p);
                }
            });
            // setShowProgressBar(false);
        } catch (error) {
            console.log(error);
        }
    }


}