export const sortList = (list: any, sortBy: string, fieldName: string) => {
    if (sortBy === 'asc') {
        return list.sort(function (a: any, b: any) {
            if (a[fieldName] < b[fieldName]) { return -1; }
            if (a[fieldName] > b[fieldName]) { return 1; }
            return 0;
        })
    } else {
        return list.sort(function (a: any, b: any) {
            if (a[fieldName] < b[fieldName]) { return 1; }
            if (a[fieldName] > b[fieldName]) { return -1; }
            return 0;
        })
    }
}