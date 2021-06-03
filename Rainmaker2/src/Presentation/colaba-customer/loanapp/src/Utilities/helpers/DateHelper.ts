import moment from "moment";

export function DateDiffInYears(date2:string, date1:string) {
    let dt1 = new Date(date1);
    let dt2 = new Date(date2);

    let diff = (dt2.getTime() - dt1.getTime()) / 1000;
    diff /= (60 * 60 * 24);
    return Math.abs(Math.round(diff / 365.25));

}

export class  DateServices {
    static convertDateToUTC(date: string) {
        return date?moment.utc(date).format():'';
    }

    // static convertUTCToFormat(date: string, format :string = 'dddd, MM YYYY') {
    //     return  moment.utc(new Date(date?date:null)).format(format)
    // }
}
