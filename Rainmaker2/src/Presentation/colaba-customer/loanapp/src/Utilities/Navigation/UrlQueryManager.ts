
export class UrlQueryManager {

    static navigation;
    static queryData = {};
    static queryEncoded = '';

    static getQueryData() {
        return this.queryData;
    }

    static addQuery(key: string, value) {
        this.queryData[key] = value;
        this.encodeQuery64(this.queryData);
        this.navigation?.setQueryString(this.queryEncoded);
    }

    static getQuery(key: string) {
        return isNaN(this.queryData[key]) ? this.queryData[key] : Number(this.queryData[key]);
    }

    static deleteQuery(key: string) {
        delete this.queryData[key];
        this.encodeQuery64(this.queryData);
        this.navigation?.setQueryString(this.queryEncoded);
    }

    static deleteAllQueries() {
        this.queryData = {};
        this.encodeQuery64(this.queryData);
        this.navigation?.setQueryString(this.queryEncoded);
    }

    static encodeQuery64(query) {
        if (!query || !Object.keys(query)?.length) return '';
        this.queryEncoded = btoa(JSON.stringify(query));
    }

    static decodeQuery64(query) {
        this.queryData = JSON.parse(atob(query));

    }

    static extractQueryFromUrl(query) {
        if (query.split('=')[0] !== '?q') {
            return;
        }
        this.queryEncoded = query?.split('=')[1];
        this.extractQuery();
    }

    static exractQueryFromSavedState(query) {
        this.queryEncoded = query;
        this.extractQuery();
    }

    static extractQuery() {
        try {
            this.decodeQuery64(this.queryEncoded);
            console.log('this.queryData', this.queryData);
            this.navigation?.setQueryString(this.queryEncoded);
        } catch (error) {
            console.log('error', error)
        }
    }
}