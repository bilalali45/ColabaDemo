export const sortList = (list: any, isDocMark: boolean, isDocMarkAsc: boolean, isStatusMark: boolean, isStatusMarkAsc: boolean) => {      
    return list.sort(function (a: any, b: any) {
        if(isStatusMark && isStatusMarkAsc){
            if (a["status"] < b["status"]) { return -1; }
            if (a["status"] > b["status"]) { return 1; }
        }else if (isStatusMark){
            if (a["status"] < b["status"]) { return 1; }
            if (a["status"] > b["status"]) { return -1; }
        }

        if(isDocMark && isDocMarkAsc){
            if (a["docName"] < b["docName"]) { return -1; }
            if (a["docName"] > b["docName"]) { return 1; }
        }else if(isDocMark){
            if (a["docName"] < b["docName"]) { return 1; }
            if (a["docName"] > b["docName"]) { return -1; }
        }
        
        return 0;
    })
       
}