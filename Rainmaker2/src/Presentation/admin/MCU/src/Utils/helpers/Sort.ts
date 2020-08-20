export const sortList = (list: any, fieldName: string, isAsc: boolean) => {  
    
    // return list.sort(function (a: any, b: any) {
    //     if(isStatusMark && isStatusMarkAsc){
    //         if (a["status"] < b["status"]) { return -1; }
    //         if (a["status"] > b["status"]) { return 1; }
    //     }else if (isStatusMark){
    //         if (a["status"] < b["status"]) { return 1; }
    //         if (a["status"] > b["status"]) { return -1; }
    //     }

    //     if(isDocMark && isDocMarkAsc){
    //         if (a["docName"] < b["docName"]) { return -1; }
    //         if (a["docName"] > b["docName"]) { return 1; }
    //     }else if(isDocMark){
    //         if (a["docName"] < b["docName"]) { return 1; }
    //         if (a["docName"] > b["docName"]) { return -1; }
    //     }
        
    //     return 0;
    // })
    if(isAsc){
        return list.sort(function(a: any, b: any){
            if(a[fieldName] < b[fieldName]) { return -1; }
            if(a[fieldName] > b[fieldName]) { return 1; }
            return 0;
        })
    }else{
        return list.sort(function(a: any, b: any){
            if(a[fieldName] < b[fieldName]) { return 1; }
            if(a[fieldName] > b[fieldName]) { return -1; }
            return 0;
        })
    }

   
       
}