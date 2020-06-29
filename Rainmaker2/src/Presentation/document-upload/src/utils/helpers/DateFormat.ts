import moment from 'moment';
import * as momentDate from 'moment';


export function DateFormat(date: string, isTime: boolean){
 if(isTime){
   return moment(new Date(date)).format('MMM DD, YYYY hh:mm A')
 }else{
  return  moment(new Date(date)).format('MMM DD, YYYY')
 }
}