import moment from 'moment';
import * as momentDate from 'moment';


export function DateFormat(date: string, isTime: boolean) {
    if (isTime) {
        return moment(new Date(date)).format('MM-DD-YYYY HH:mm')
    } else {
        return moment(new Date(date)).format('MM-DD-YYYY')
    }
}